using LIBRARYMANAGEMENT.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// 1. Configure DB Context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// 2. Add Identity with Roles
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>() // ✅ enable roles
.AddEntityFrameworkStores<ApplicationDbContext>();

// 3. Add Authorization Policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
        policy.RequireRole("Admin")); // ✅ this matches [Authorize(Policy = "Admin")]
});

// 4. Configure Razor Pages Access
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/"); // Protect all pages

    // Allow anonymous access to login/register
    options.Conventions.AllowAnonymousToPage("/Account/Login");
    options.Conventions.AllowAnonymousToPage("/Account/Register");
    options.Conventions.AllowAnonymousToPage("/Account/ForgotPassword");
    options.Conventions.AllowAnonymousToPage("/Account/ResetPassword");
});

// 5. Add MVC Controllers
builder.Services.AddControllersWithViews();

// 6. Configure Application Cookie Login Path (✅ set to Identity default login)
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login"; // ✅ correct default Identity login path
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

var app = builder.Build();

// 7. Middleware
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // 🔐 Enable authentication
app.UseAuthorization();  // 🔐 Enable authorization

// 8. Map Routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// 9. Optional: Seed Roles (Admin, Librarian, User)
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Librarian", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

app.Run();
