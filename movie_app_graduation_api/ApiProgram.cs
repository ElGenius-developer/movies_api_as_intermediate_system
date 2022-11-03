using MoviesApi.Helper;
using System.Text;
using System.Text.Json.Serialization;

namespace movie_app_graduation_api
{
    public class MoviesApiApp
    {


        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            //Add default system Configuration for users and roles (inject identity tables into db context)
            builder.Services.AddIdentity<MoviesUser, IdentityRole>().AddDefaultTokenProviders()
                .AddEntityFrameworkStores<MoviesDbContext>();

            //Register MoviesContext into IServices
            builder.Services.AddDbContext<MoviesDbContext>(
                          options => options.UseSqlServer("Name=DefaultConnection"));
            builder.Services.AddDbContext<MoviesDbContext>();


            builder.Services.Configure<Jwt>(builder.Configuration.GetSection("JWT"));

            builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromMinutes(5);

            });
            builder.Services.AddControllersWithViews().AddJsonOptions(options =>
           options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            //Register authenticate class or service using dependency injection 
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                }
                ).AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))


                    };

                });

            builder.Services.AddScoped<ICalculatorServiceHelper, CalculatorServiceHelper>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var AppUrl = new ConstantsData().GetJson("MyConfig.json")!.AppUrl;

            builder.WebHost.UseUrls(AppUrl!);

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI();
                app.MapSwagger();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }

    }
}