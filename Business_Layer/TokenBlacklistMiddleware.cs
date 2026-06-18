
using Microsoft.AspNetCore.Http;


namespace Business_Layer
{
   
        public class TokenBlacklistMiddleware
        {
        // 1. Correct way to define as a Field (without get and set)
        private readonly RequestDelegate _next;

            public TokenBlacklistMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task Invoke(HttpContext context)
            {
            if (context.Request.Method == "OPTIONS")
            {
                await _next(context);
                return;
            }
            var path = context.Request.Path.Value ?? "";


            // This is a 'tank' rule, it will pass anything that has the word login or signup regardless of the letters
            if (path.Contains("login", StringComparison.OrdinalIgnoreCase) ||
                path.Contains("signup", StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            // Pull the token from the header
            var authHeader = context.Request.Headers["Authorization"].ToString();

                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                {
                    var token = authHeader.Substring("Bearer ".Length);

                // 2. Check the token (make sure LogoutS exists in Business_Layer)
                if (LogoutS.IsTokenBlacklisted(token))
                    {
                    context.Response.ContentType = "application/json";
                    var response = new { message = "The session has ended, please log in again" };
                    await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
                    return;
                    }
                }

                await _next(context);
            }
        }

    
}
