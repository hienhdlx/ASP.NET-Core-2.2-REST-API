using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using TweetBook2.Options;

namespace TweetBook2.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallerServices(IServiceCollection services, IConfiguration configuration)
        {
            var JwtSettings = new JwtSettings();
            configuration.Bind(nameof(JwtSettings), JwtSettings);
            services.AddSingleton(JwtSettings);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //configuration to add json web token support 
            // create authentication 
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtSettings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };
            });

            //add services for swagger 
            services.AddSwaggerGen(x => {
                x.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "TweetBook", Version = "v1" });

                //add security type 
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[0]}
                };

                //add security definition
                x.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the bearer scheme",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                x.AddSecurityRequirement(security);
            });


        }
    }
}
