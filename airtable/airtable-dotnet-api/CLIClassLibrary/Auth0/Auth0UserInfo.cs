namespace SSoTme.Default.Lib.CLIHandler
{
    public class Auth0UserInfo
    {
        public string Sub { get; set; }
        public string Name { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string MiddleName { get; set; }
        public string Nickname { get; set; }
        public string PreferredUsername { get; set; }
        public string Profile { get; set; }
        public string Picture { get; set; }
        public string Email { get; set; }
        public bool Email_Verified { get; set; }
    }
}