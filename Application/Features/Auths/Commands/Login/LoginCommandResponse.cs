namespace Application.Features.Auths.Commands.Login
{
    public class LoginCommandResponse
    {
        public string AccessToken { get; set; }

        public LoginCommandResponse(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}
