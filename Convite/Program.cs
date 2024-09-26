using Convite.Data;
using Convite.Models;
using Convite.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


//envio de email
builder.Services.Configure<Settings>(builder.Configuration.GetSection("SendGridSettings"));
builder.Services.AddTransient<Convite.Services.Interfaces.IEmailSender, Convite.Services.EmailSender>();

//Add Identity
builder.Services.AddDefaultIdentity<ApplicationUser>(options => {
    options.SignIn.RequireConfirmedAccount = true;
    options.SignIn.RequireConfirmedEmail = true;

    //options.Password.RequireDigit = false; // Não exige dígito
    //options.Password.RequireLowercase = false; // Não exige letras minúsculas
    //options.Password.RequireUppercase = false; // Não exige letras maiúsculas
    //options.Password.RequireNonAlphanumeric = false; // Não exige caracteres especiais
    //options.Password.RequiredLength = 2; // Define o comprimento mínimo da senha (por exemplo, 6 caracteres)
    //options.Password.RequiredUniqueChars = 1; // Define o número mínimo de caracteres únicos (por exemplo, 1)

    options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;

}).AddEntityFrameworkStores<ApplicationDbContext>();

// Configurar autenticação do Google
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

    //options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    // Configurar rotas de login, logout, e acesso negado
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
}).AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.Lax;  // Define a política SameSite
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);  // Expiração do cookie
    options.SlidingExpiration = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;  // Apenas cookies via HTTPS
    
});

builder.Services.AddControllersWithViews();

builder.Services.ConfigureApplicationCookie(options =>
{
    // Configura a URL de login e logout
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";

    // Configura a expiração do cookie de autenticação
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // O cookie expira após 60 minutos
    options.SlidingExpiration = true; // Renova o cookie se ele estiver perto de expirar
    //options.Cookie.HttpOnly = true; // Reforça que o cookie só pode ser acessado por meio de HTTP
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Sempre antes de Authorization
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
