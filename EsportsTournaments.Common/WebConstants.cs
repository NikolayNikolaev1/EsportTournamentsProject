namespace EsportsTournaments.Common
{
    public static class WebConstants
    {
        public const string TempDataErrorMessageKey = "ErrorMessage";
        public const string TempDataSuccessMessageKey = "SuccessMessage";
        public const int PaginationSize = 6;

        public static class AdminCredentials
        {
            public const string Email = "admin@esportstournaments.test";
            public const string Password = "admin12";
            public const string Username = "admin";
        }

        public static class Areas
        {
            public const string Admin = "Admin";
            public const string Moderator = "Moderator";
        }

        public static class Roles
        {
            public const string Administrator = "Administrator";
            public const string TournamentModerator = "TournamentModerator";
        }

        public static class ErrorMessages
        {
            public const string GameExists = "Game with name {0} already exists.";
            public const string InvalidImageFileExtension = "File extension is invalid. Only files with .jpeg, .jpg and .png are valid.";
            public const string InvalidImageFileLength = "The file is too large. Max image length is 2 MB.";
            public const string InvalidImageFileSignature = "File signature is invalid. Image can not be uploaded.";
            public const string InvalidPropertyMaxLength = "{0} max length can be {1} characters long.";
            public const string InvalidPropertyMinLength = "{0} length must be atleast {1} characters long.";
            public const string TeamNameExists = "Team with name {0} already exists";
            public const string TeamTagExists = "Team with tag {0} already exists";
            public const string TournamentStarted = "Tournament already started!";
        }


        public static class SuccessMessages
        {
            public const string AddedUserRole = "User {0} successfully added to the {1} role.";
            public const string AddedGame = "Game {0} added successfully!";
            public const string AddedTournament = "Successfully created tournament {0}";
            public const string CreateTeam = "Team {0} created.";
            public const string TeamJoined = "Successfully joined team!";
            public const string TeamLeft = "Successfully left team!";
            public const string TournamentStarted = "Successfully started tournament {0}!";
            public const string TournamentEnded = "Successfully ended tournament!";
            public const string RemovedUserRole = "Removed role from user {0} successfully.";
        }

    }
}
