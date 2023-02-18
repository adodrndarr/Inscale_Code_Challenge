using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace Booking.Source.Security
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> option,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
        ) : base(option, logger, encoder, clock)
        {

        }


        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var credentials = GetCredentials();

            if (string.IsNullOrWhiteSpace(credentials))
                return AuthenticateResult.Fail(new AuthenticationException("You're not authorized to access the API."));

            var credentialValues = credentials.Split(":");
            var username = credentialValues[0];
            var password = credentialValues[1];
            var canAccess = (username == "admin") && (password == "adminPwd");

            if (!canAccess)
                return AuthenticateResult.Fail(new AuthenticationException("You're not authorized to access the API."));

            return AuthenticateResult.Success(GetAuthenticationTicket(username));
        }

        private string GetCredentials()
        {
            const string AUTHORIZATION = "Authorization";

            if (!Request.Headers.ContainsKey(AUTHORIZATION))
                return string.Empty;

            var header = AuthenticationHeaderValue.Parse(Request.Headers[AUTHORIZATION]);
            var headerData = Convert.FromBase64String(header.Parameter);
            var credentials = Encoding.UTF8.GetString(headerData);

            var hasCredentials = !string.IsNullOrWhiteSpace(credentials) && (credentials.Split(":").Length == 2);
            if (!hasCredentials)
                return string.Empty;

            return credentials;
        }

        private AuthenticationTicket GetAuthenticationTicket(string username)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var authenticationTicket = new AuthenticationTicket(principal, Scheme.Name);

            return authenticationTicket;
        }
    }
}
