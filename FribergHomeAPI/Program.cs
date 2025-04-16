using FribergHomeAPI.Data;
using FribergHomeAPI.Data.Repositories;
using FribergHomeAPI.Data.Seeding;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Injected Services 
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors();

var app = builder.Build();

// Seeding
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    SeedData.SeedAsync(ctx).Wait();
}
//Cors middleware
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
