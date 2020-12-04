namespace FitnessTracker.Contracts
{

    public static class ApiRoutes
    {

        private const string Base = "api/";

        public static class Example
        {
            public const string GetAll = Base + "example";
            public const string Get = Base + "example/{exampleId}";
            public const string Create = Base + "example";
            public const string Update = Base + "example/{exampleId}";
            public const string Delete = Base + "example/{exampleId}";
        }

        public static class User
        {
            public const string GetAll = Base + "user/all";
            public const string Get = Base + "user";
            public const string Update = Base + "user";
            public const string UpdateRole = Base + "user/{userId}";
            public const string Delete = Base + "user/{userId}";
            public const string Test = Base + "user/Test";
        }

        public static class Role
        {
            public const string GetAll = Base + "role";
        }

        public static class Auth
        {
            public const string Login = Base + "auth/login";
            public const string Register = Base + "auth/register";
            public const string Refresh = Base + "auth/refresh";
            public const string ChangePassword = Base + "user/updatePassword";
        }

        public static class Coach
        {
            public const string Index = Base + "coach";
            public const string GetCoach = Base + "coach/{coachId}";
            public const string PostCoach = Base + "coach";
            public const string PutCoach = Base + "coach/{coachId}";
            public const string DeleteCoach = Base + "coach/{coachId}";
        }

    }

}
