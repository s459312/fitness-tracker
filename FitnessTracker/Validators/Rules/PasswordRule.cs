using System;
using System.Linq;
using FluentValidation.Validators;

namespace FitnessTracker.Validators.Rules
{
    public class PasswordRule : PropertyValidator
    {

        public PasswordRule() : base("Hasło {Message}") 
        {
            
        }
        
        protected override bool IsValid(PropertyValidatorContext context)
        {
            string value = context.PropertyValue.ToString();
            bool lower = value.Any(ch => Char.IsLower(ch));
            bool upper = value.Any(ch => Char.IsUpper(ch));
            bool special = value.Any(ch => !Char.IsLetterOrDigit(ch));
            bool digit = value.Any(ch => Char.IsDigit(ch));
            
            if (String.IsNullOrEmpty(value) || value.Length < 8)
            {
                context.MessageFormatter.AppendArgument("Message", "musi zawierać przynajmniej 8 znaków");
                return false;
            }

            if (!lower)
            {
                context.MessageFormatter.AppendArgument("Message", "musi posiadać przynajmniej jedną małą literę");
                return false;
            }
            
            if (!upper)
            {
                context.MessageFormatter.AppendArgument("Message", "musi posiadać przynajmniej jedną wielką literę");
                return false;
            }
            
            if (!digit)
            {
                context.MessageFormatter.AppendArgument("Message", "musi posiadać przynajmniej jedną cyfrę");
                return false;
            }
            
            if (!special)
            {
                context.MessageFormatter.AppendArgument("Message", "musi posiadać przynajmniej jeden znak specjalny");
                return false;
            }

            return true;
        }
    }
}