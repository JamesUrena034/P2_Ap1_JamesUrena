using Microsoft.EntityFrameworkCore;
using P2_Ap1_JamesUrena.Components;
using P2_Ap1_JamesUrena.DAL;
using P2_Ap1_JamesUrena.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var ConStr = builder.Configuration.GetConnectionString("SqlConnection");
builder.Services.AddDbContextFactory<Contexto>(options => options.UseSqlite(ConStr));
builder.Services.AddScoped<RegistroServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
