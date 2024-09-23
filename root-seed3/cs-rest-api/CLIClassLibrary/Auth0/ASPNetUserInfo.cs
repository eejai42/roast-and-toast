namespace CLIClassLibrary.Auth0
{
    public class ASPNetUserInfo
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string EmailAddress { get; set; }
        public bool IsValidated { get; set; }
        public string ProfileImage { get; set; }
    }
}