using DesignPatternsWorkshop.Infrastructure.Factories;
using DesignPatternsWorkshop.Infrastructure.Services;
using DesignPatternsWorkshop.Presentation.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddSingleton<PurchaseService>();
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddScoped<DiscountStrategyFactory>();

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

app.UseStaticFiles();
app.MapControllers();
app.MapControllerRoute(name: "default", pattern: "/{controller=Home}/{action=Index}");
app.MapHub<PurchaseHub>("/purchase-hub");

app.Run();
