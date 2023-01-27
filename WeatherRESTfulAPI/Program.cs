using Weather.Infrastructure.Extension;
using WeatherAPI.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterDbContext(builder.Configuration);
builder.Services.AddAuthenticationService(builder.Configuration);
builder.Services.AddDependencyInjection(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.RequestPipeline(builder.Environment);
app.Run();
