namespace Logic
{
    public class Password
    {
        public string Website { get; set; }
        public string Login { get; set; }
        public string Pwd { get; set; }

        public Password(string site, string login, string pwd)
        {
            Website = site;
            Login = login;
            Pwd = pwd;
        }
    }
}
