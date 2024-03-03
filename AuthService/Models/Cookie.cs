namespace AuthService.Models
{
    public class Cookie
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public CookieOptions Options { get; set; }

        public Cookie(string name, string value, CookieOptions options)
        {
            Name = name;
            Value = value;
            Options = options;
        }
    }
}