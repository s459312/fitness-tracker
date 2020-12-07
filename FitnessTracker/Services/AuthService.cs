using AutoMapper;
using FitnessTracker.Contracts.Request.Auth;
using FitnessTracker.Data;
using FitnessTracker.Helpers;
using FitnessTracker.Models;
using FitnessTracker.Options;
using FitnessTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTracker.Services
{

    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IMapper _mapper;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly DatabaseContext _context;
        private readonly IAuthHelper _authHelper;

        public AuthService(
            IMapper mapper,
            TokenValidationParameters tokenValidationParameters,
            JwtSettings jwtSettings,
            DatabaseContext context,
            IAuthHelper authHelper
        )
        {
            _mapper = mapper;
            _tokenValidationParameters = tokenValidationParameters;
            _jwtSettings = jwtSettings;
            _context = context;
            _authHelper = authHelper;
        }

        public async Task<AuthenticationResult> RegisterAsync(AuthRegisterRequest request)
        {
            request.Email = request.Email.ToLower();

            if (await UserExists(request.Email))
                return new AuthenticationResult { Error = "Użytkownik z takim adresem email już istnieje" };

            User newUser = _mapper.Map<User>(request);
            newUser.RoleId = 3;

            _authHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(newUser);
            int created = await _context.SaveChangesAsync();

            if (created <= 0)
                return new AuthenticationResult { Error = "Rejestracja nie powiodła się" };

            return await GenerateAuthenticationResultAsync(newUser);
        }

        public async Task<AuthenticationResult> LoginAsync(AuthLoginRequest request)
        {
            request.Email = request.Email.ToLower();
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return new AuthenticationResult { Error = "Użytkownik nie istnieje" };

            if (!_authHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                return new AuthenticationResult { Error = "Podano błędne hasło" };

            return await GenerateAuthenticationResultAsync(user);
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            var validatedToken = GetPrincipalFromToken(token);

            if (validatedToken == null)
                return new AuthenticationResult { Error = "Invalid Token" };

            var expiryDateUnix =
                long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
                return new AuthenticationResult { Error = "This Token hasnt expired yet" };

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshZToken = await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken);

            if (storedRefreshZToken == null)
                return new AuthenticationResult { Error = "This refresh token does not exists" };

            if (DateTime.UtcNow > storedRefreshZToken.ExpireDate)
                return new AuthenticationResult { Error = "This refresh token has expired" };

            if (storedRefreshZToken.Invalidated)
                return new AuthenticationResult { Error = "This refresh token has been invalidated" };

            if (storedRefreshZToken.Used)
                return new AuthenticationResult { Error = "This refresh token has been used" };

            if (storedRefreshZToken.JwtId != jti)
                return new AuthenticationResult { Error = "This refresh token does not match this JWT" };

            storedRefreshZToken.Used = true;
            _context.RefreshTokens.Update(storedRefreshZToken);
            await _context.SaveChangesAsync();

            var user = await _context.Users.FindAsync(validatedToken.Claims.Single(x => x.Type == "login").Value);
            return await GenerateAuthenticationResultAsync(user);
        }

        public async Task<AuthenticationResult> ChangePasswordAsync(AuthChangePasswordRequest request)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == _authHelper.GetAuthenticatedUserId());

            if (user == null)
                return new AuthenticationResult { Error = "Użytkownik nie istnieje" };

            if (!_authHelper.VerifyPasswordHash(request.OldPassword, user.PasswordHash, user.PasswordSalt))
                return new AuthenticationResult { Error = "Podano błędne stare hasło" };

            _authHelper.CreatePasswordHash(request.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Update(user);
            if (await _context.SaveChangesAsync() <= 0)
                return new AuthenticationResult { Error = "Zmiana hasła nie powiodła się" };

            return new AuthenticationResult
            {
                Success = true,
                Token = "",
                RefreshToken = ""
            };
        }

        private async Task<AuthenticationResult> GenerateAuthenticationResultAsync(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", user.Id.ToString())
            };

            Role userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == user.RoleId);
            if (userRole != null)
                claims.Add(new Claim(ClaimTypes.Role, userRole.Name));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpireDate = DateTime.UtcNow.AddMonths(6)
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token
            };
        }

        private async Task<bool> UserExists(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user != null;
        }

        private async Task<bool> RoleExists(int roleId)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
            return role != null;
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = _tokenValidationParameters.Clone();
                tokenValidationParameters.ValidateLifetime = false;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                return !IsJwtWithValidSecurityAlgorithm(validatedToken) ? null : principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return validatedToken is JwtSecurityToken jwtSecurityToken &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }
    }
}