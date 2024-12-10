namespace social_media_app.Response
{
    public class AuthResponse
    {
        public string token { get; set; }
        public string message { get; set; }

        public AuthResponse(string token, string message)
        {
            this.token = token;
            this.message = message;
        }
    }
}
