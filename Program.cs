using TMAWarehouse.Models;

var builder = WebApplication.CreateBuilder(args);

// Add API Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TmaDbContext>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add Razor Pages Services
builder.Services.AddRazorPages();

// Build
var app = builder.Build();

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
