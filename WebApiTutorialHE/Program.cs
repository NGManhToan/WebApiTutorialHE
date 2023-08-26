using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using P2N_Pet_API.Models.UtilsProject;
using System.Configuration;
using System.Text;
using WebApiTutorialHE.Action;
using WebApiTutorialHE.Action.Interface;
using WebApiTutorialHE.Database;
using WebApiTutorialHE.Manager.Token;
using WebApiTutorialHE.Manager.Token.Interface;
using WebApiTutorialHE.Models.Mail;
using WebApiTutorialHE.Models.UtilsProject;
using WebApiTutorialHE.Module.AdminManager.Action;
using WebApiTutorialHE.Module.AdminManager.Action.Interface;
using WebApiTutorialHE.Module.AdminManager.Query;
using WebApiTutorialHE.Module.AdminManager.Query.Interface;
using WebApiTutorialHE.Module.AdminManager.Service;
using WebApiTutorialHE.Module.AdminManager.Service.Interface;
using WebApiTutorialHE.Query;
using WebApiTutorialHE.Query.Interface;
using WebApiTutorialHE.Service;
using WebApiTutorialHE.Service.Interface;
using WebApiTutorialHE.UtilsService;
using WebApiTutorialHE.UtilsService.Interface;
    

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.





builder.Services.AddControllers();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));
builder.Services.AddTransient<IMailService, MailService>();

builder.Services.AddDbContext<SharingContext>(options => options.UseMySql(configuration.GetConnectionString("SharingConnection"),
                ServerVersion.Parse("8.0.30-mysql")), ServiceLifetime.Scoped, ServiceLifetime.Scoped);

builder.Services.AddSingleton<ISharingDapper, SharingDapper>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryQuery, CategoryQuery>();

builder.Services.AddScoped<IUserQuery, UserQuery>();
builder.Services.AddScoped<IUserAction,UserAction>();

builder.Services.AddScoped<ILoginQuery, LoginQuery>();
builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddScoped<IRegistationAction, RegistationAction>();
builder.Services.AddScoped<IRegistrationQuery, RegistrationQuery>();
builder.Services.AddScoped<IRegistrationService,RegistrationSevice>();

builder.Services.AddScoped<IUserAction, UserAction>();
builder.Services.AddScoped<IUserService, UserService>();

//builder.Services.AddScoped<IAuthService,JWTService>();

builder.Services.AddScoped<ICloudMediaService, CloudMediaService>();

builder.Services.AddScoped<ICategoryAction, CategoryAction>();
builder.Services.AddScoped<ICategoryService,CategoryService>();

builder.Services.AddScoped<IItemfeedbackAction, ItemfeedbackAction>();
builder.Services.AddScoped<IItemfeedbackQuery, ItemfeedbackQuery>();
builder.Services.AddScoped<IItemFeedbackService, ItemfeedbackService>();

builder.Services.AddScoped<IPostQuery, PostQuery>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IPostAction, PostAction>();

builder.Services.AddScoped<IShowImageSevice, ShowImageSevice>();

builder.Services.AddScoped<IACategoryAction, ACategoryAction>();
builder.Services.AddScoped<IACategoryService,ACategorySevice>();

builder.Services.AddScoped<IARegistrationQuery,ARegistrationQuery>();
builder.Services.AddScoped<IARegistrationService, ARegistrationService>();
builder.Services.AddScoped<IARegistrationAction, ARegistrationAction>();

builder.Services.AddScoped<IAUserAction, AUserAction>();
builder.Services.AddScoped<IAUserService, AUserService>();
builder.Services.AddScoped<IAUserQuey,AUserQuery>();


///builder.Services.Configure<JWTSetting>(configuration.GetSection("JWTSetting"));


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiTutoriaLHE", Version = "v1.0" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "AuthorizationSwagger",
        In = ParameterLocation.Header,
        Description = "Insert JWT Token",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[]{}
            }
        });
});



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}


FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("./sharing.json"),
});


app.UseStaticFiles();
app.UseRouting();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                    .AllowCredentials()); // allow credentials
app.Run();
