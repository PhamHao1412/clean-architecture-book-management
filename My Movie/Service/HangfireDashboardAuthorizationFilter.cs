// using Hangfire.Dashboard;
// using Microsoft.AspNetCore.Http;
// using System;
// using System.Net;
// using System.Text;
// using System.Threading.Tasks;
//
// public class HangfireBasicAuthFilter : IDashboardAuthorizationFilter
// {
//     private readonly string _username;
//     private readonly string _password;
//
//     public HangfireBasicAuthFilter(string username, string password)
//     {
//         _username = username ?? throw new ArgumentNullException(nameof(username));
//         _password = password ?? throw new ArgumentNullException(nameof(password));
//     }
//
//     public bool Authorize(DashboardContext context)
//     {
//         var httpContext = context.GetHttpContext();
//         string authHeader = httpContext.Request.Headers["Authorization"];
//
//         if (authHeader != null && authHeader.StartsWith("Basic "))
//         {
//             var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
//             var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));
//             var username = decodedUsernamePassword.Split(':', 2)[0];
//             var password = decodedUsernamePassword.Split(':', 2)[1];
//
//             return IsAuthorized(username, password);
//         }
//
//         // Not authorized, challenge for credentials
//         httpContext.Response.Headers["WWW-Authenticate"] = "Basic";
//         httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
//         return false;
//     }
//
//     private bool IsAuthorized(string username, string password)
//     {
//         return username.Equals(_username, StringComparison.InvariantCultureIgnoreCase)
//                && password.Equals(_password);
//     }
// }