using System.Net;
using System.Text;
namespace My_Movie.Middlewares
{
    public class HangfireBasicAuthMiddleware : IMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _username;
        private readonly string _password;

        public HangfireBasicAuthMiddleware(RequestDelegate next, string username, string password)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _username = username ?? throw new ArgumentNullException(nameof(username));
            _password = password ?? throw new ArgumentNullException(nameof(password));
        }

        // public async Task InvokeAsync(HttpContext context)
        // {
        //    
        // }

        private bool IsAuthorized(string username, string password)
        {
            return username.Equals(_username, StringComparison.InvariantCultureIgnoreCase)
                   && password.Equals(_password);
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string authHeader = context.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
                var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));
                var username = decodedUsernamePassword.Split(':', 2)[0];
                var password = decodedUsernamePassword.Split(':', 2)[1];

                if (IsAuthorized(username, password))
                {
                    await _next.Invoke(context);
                    return;
                }
            }

            // Not authorized, challenge for credentials
            context.Response.Headers["WWW-Authenticate"] = "Basic";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            
        }
    }
}