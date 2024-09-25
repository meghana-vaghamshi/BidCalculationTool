using BidCalculationTool.Server.DbContext;
using BidCalculationTool.Server.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization; 

var builder = WebApplication.CreateBuilder(args);

// Configure CORS to allow any origin, method, and header, enabling cross-origin requests.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddControllersWithViews().AddNewtonsoftJson(
    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register Services
builder.Services.AddScoped<IVehiclePricingService, VehiclePricingService>();

//Register DbContext 
builder.Services.AddDbContext<BidCalcDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BidCalcDbConnection")));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

//Using Cors
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
