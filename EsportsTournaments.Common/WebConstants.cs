namespace EsportsTournaments.Common
{
    public static class WebConstants
    {
        public const string TempDataErrorMessageKey = "ErrorMessage";
        public const string TempDataSuccessMessageKey = "SuccessMessage";
        public static class Areas
        {
            public const string Admin = "Admin";
            public const string Moderator = "Moderator";
        }

        public static class Roles
        {
            public const string Administrator = "Administrator";
            public const string TournamentModerator = "Tournament Moderator";
        }

        public static class ErrorMessages
        {
            public const string GameExists = "Game with name {0} already exists.";
        }


        public static class SuccessMessages
        {
            public const string AddedUserRole = "User {0} successfully added to the {1} role.";
            public const string AddedGame = "Game {0} added successfully!";
            public const string AddedTournament = "Successfully created tournament {0}";
        }

    }
}
