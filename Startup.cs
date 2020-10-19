using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using blogApi.Context;
using Microsoft.EntityFrameworkCore;
using blogApi.repositories.user;
using blogApi.repositories.user.UserRepository;
using blogApi.repositories.post;
using blogApi.repositories.post.PostRepository;
using blogApi.repositories.comment;
using blogApi.repositories.comment.PostComment;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using blogApi.Models;
using Microsoft.AspNetCore.Cors;



namespace blogApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MainContext>(option => option.UseSqlServer(connectionString));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostComment, PostComment>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => { options.RequireHttpsMetadata = false; options.SaveToken = true; options.TokenValidationParameters = new TokenValidationParameters { ValidateIssuer = false, ValidateAudience = false, ValidateLifetime = true, ValidateIssuerSigningKey = true, ValidIssuer = Configuration["Jwt:Issuer"], ValidAudience = Configuration["Jwt:Audience"], IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])), ClockSkew = TimeSpan.Zero }; });
            services.AddAuthorization(config => { config.AddPolicy(Policies.Admin, Policies.AdminPolicy()); config.AddPolicy(Policies.User, Policies.UserPolicy()); });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());  

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
