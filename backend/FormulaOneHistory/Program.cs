using FormulaOneHistory.Data;
using FormulaOneHistory.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FormulaOneHistoryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("FormulaOneHistoryDbContext")));
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        corsPolicyBuilder => { corsPolicyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
});
// Add import services to the container.
builder.Services.AddScoped<DriverImportService>();
builder.Services.AddScoped<RaceImportService>();
builder.Services.AddScoped<RaceResultImportService>();
builder.Services.AddScoped<TeamImportService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();