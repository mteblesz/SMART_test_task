using TMAWarehouse.Data;
using TMAWarehouse.Models;
using TMAWarehouse.Services;

var builder = WebApplication.CreateBuilder(args);

// Add API Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TmaDbContext>();
builder.Services.AddScoped<DbSeeder>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IItemsService, ItemsService>();
builder.Services.AddScoped<IEnumsService, EnumsService>();
builder.Services.AddScoped<IRequestsService, RequestsService>();

// Add Razor Pages Services
builder.Services.AddRazorPages();

// Build
var app = builder.Build();

// Call services
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
seeder.Seed();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.MapRazorPages();

app.Run();
