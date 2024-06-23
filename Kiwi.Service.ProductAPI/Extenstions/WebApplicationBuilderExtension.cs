﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Kiwi.Service.ProductAPI.Extensions
{
	public static class WebApplicationBuilderExtension
	{
		public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder) 
		{

			var apiSettings = builder.Configuration.GetSection("ApiSettings");

			var secret = apiSettings.GetValue<string>("Secret");
			var issuer = apiSettings.GetValue<string>("Issuer");
			var audience = apiSettings.GetValue<string>("Audience");

			var key = Encoding.ASCII.GetBytes(secret);

			builder.Services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
			{
				x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = true,
					ValidIssuer = issuer,
					ValidateAudience = true,
					ValidAudience = audience,
				};
			});
			return builder;
		}
	}
}
