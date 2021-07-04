// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using System;
// using System.Net;
// using System.Text;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.IdentityModel.Tokens;
// using Newtonsoft.Json;

// namespace AnimalShelter
// {
//   public class Startup
//   {
//     public static string SecretKey = "mysupersecret_secretkey!123";
//     public static string Issuer = "infologs.in";
//     public static string Audience = "global";
//     public Startup(IConfiguration configuration)
//     {
//       Configuration = configuration;
//     }

//     public IConfiguration Configuration { get; }

//     // This method gets called by the runtime. Use this method to add services to the container.
//     public void ConfigureServices(IServiceCollection services)
//     {

//       services.AddMvc(options =>
//                   {
//                     options.EnableEndpointRouting = false;
//                   });
//       services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//           .AddJwtBearer(options =>
//           {
//             options.TokenValidationParameters = new TokenValidationParameters()
//             {
//               ValidateIssuer = true,
//               ValidIssuer = Issuer,
//               ValidateAudience = true,
//               ValidAudience = Audience,
//               ValidateLifetime = true,
//               ValidateIssuerSigningKey = true,
//               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
//             };
//             options.Events = new JwtBearerEvents()
//             {
//               OnAuthenticationFailed = context =>
//                     {
//                       context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
//                       context.Response.ContentType = context.Request.Headers["Accept"].ToString();
//                       string _Message = "Authentication token is invalid.";
//                       if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
//                       {
//                         //context.Response.Headers.Add("Token-Expired", "true");
//                         //OR
//                         _Message = "Token has expired.";
//                         return context.Response.WriteAsync(JsonConvert.SerializeObject(new
//                         {
//                           StatusCode = (int)HttpStatusCode.Unauthorized,
//                           Message = _Message
//                         }));
//                       }
//                       return context.Response.WriteAsync(JsonConvert.SerializeObject(new
//                       {
//                         StatusCode = (int)HttpStatusCode.Unauthorized,
//                         Message = _Message
//                       }));
//                       //return Task.CompletedTask;
//                     }
//             };
//           });
//       services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
//           .AddXmlSerializerFormatters();
//     }
//     // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//     [Obsolete]
//     public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
//     {
//       if (env.IsDevelopment())
//       {
//         app.UseDeveloperExceptionPage();
//       }
//       app.UseAuthentication();
//       app.UseMvc(config =>
//       {
//         config.MapRoute("Defualt", "{controller=animals}/{action=get}/{id?}");
//       });
//     }
//   }
// }



using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using AnimalShelter.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace AnimalShelter
{
  public class Startup
  {
    public static string SecretKey = "mysupersecret_secretkey!123";
    public static string Issuer = "infologs.in";
    public static string Audience = "global";
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                      .AddJwtBearer(options =>
                      {
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                          ValidateIssuer = true,
                          ValidIssuer = Issuer,
                          ValidateAudience = true,
                          ValidAudience = Audience,
                          ValidateLifetime = true,
                          ValidateIssuerSigningKey = true,
                          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
                        };
                        options.Events = new JwtBearerEvents()
                        {
                          OnAuthenticationFailed = context =>
                    {
                      context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                      context.Response.ContentType = context.Request.Headers["Accept"].ToString();
                      string _Message = "Authentication token is invalid.";
                      if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                      {
                        //context.Response.Headers.Add("Token-Expired", "true");
                        //OR
                        _Message = "Token has expired.";
                        return context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                          StatusCode = (int)HttpStatusCode.Unauthorized,
                          Message = _Message
                        }));
                      }
                      return context.Response.WriteAsync(JsonConvert.SerializeObject(new
                      {
                        StatusCode = (int)HttpStatusCode.Unauthorized,
                        Message = _Message
                      }));
                      //return Task.CompletedTask;
                    }
                        };
                      });
      services.AddDbContext<AnimalShelterContext>(opt =>
          opt.UseMySql(Configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(Configuration["ConnectionStrings:DefaultConnection"])));
      services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      // app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}