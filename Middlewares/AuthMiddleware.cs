using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace noticiasmvc.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public AuthMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path;

            if (!context.User.Identity.IsAuthenticated && 
                !path.StartsWithSegments("/Auth") && 
                !path.StartsWithSegments("/css") && 
                !path.StartsWithSegments("/js") && 
                !path.StartsWithSegments("/lib"))
            {
                var token = context.Request.Cookies["jwtToken"];

                if (token != null)
                {
                    if(!AttachUserToContext(context, token))
                    {
                        context.Response.Redirect("/Auth");
                        return;
                    }
                }
                else
                {
                    context.Response.Redirect("/Auth");
                    return;
                }
            }

            await _next(context);
        }

        private bool AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                context.User = principal;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}