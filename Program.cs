using CadastroContatos.Filters;
using CadastroContatos.DataBase;
using CadastroContatos.Interfaces;
using CadastroContatos.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<HttpContextAccessor, HttpContextAccessor>();

// Adicionando o serviço de contêiner de injeção de dependência
builder.Services.AddScoped<IUsuario, UsuarioDb>();
builder.Services.AddScoped<ISessao, SessionService>();

builder.Services.AddScoped<ValidarSessaoAttribute>();

builder.Services.AddSession(
    o =>
    {
        o.IdleTimeout = TimeSpan.FromMinutes(30);
        o.Cookie.HttpOnly = true;
        o.Cookie.IsEssential = true;
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
