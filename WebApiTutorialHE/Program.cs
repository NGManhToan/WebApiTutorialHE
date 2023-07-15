using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WebApiTutorialHE.Action;
using WebApiTutorialHE.Action.Interface;
using WebApiTutorialHE.Database;
using WebApiTutorialHE.Manager.Token;
using WebApiTutorialHE.Manager.Token.Interface;
using WebApiTutorialHE.Models.Mail;
using WebApiTutorialHE.Models.UtilsProject;
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


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("./sharing.json"),
});



app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                    .AllowCredentials()); // allow credentials

app.Run();
