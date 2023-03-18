using Tmf.Ecom.Api.Middleware;
using Tmf.Ecom.Api.Validations;
using Tmf.Ecom.Core.Options;
using Tmf.Ecom.Infrastructure.HttpServices;
using Tmf.Ecom.Infrastructure.Interfaces;
using Tmf.Ecom.Infrastructure.Services;
using Tmf.Ecom.Manager.Services;
using Tmf.Logs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHttpClient();

builder.Services.Configure<EcomOptions>(builder.Configuration.GetSection(EcomOptions.Ecom));
builder.Services.Configure<ConnectionStringsOptions>(builder.Configuration.GetSection(ConnectionStringsOptions.ConnectionStrings));



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<IEcomRepository, EcomRepository>();


builder.Services.AddScoped<IEcomManager, EcomManager>();

builder.Services.AddScoped<IValidator<GenerateManifestRequest>, GenerateManifestValidator>();
builder.Services.AddScoped<IValidator<RescheduleOrCancelAppointmentRequest>, RescheduleOrCancelAppointmentValidator>();
builder.Services.AddScoped<IValidator<PushShipmentStatusRequest>, PushShipmentStatusValidator>();
builder.Services.AddScoped<IValidator<PullShipmentStatusRequest>, PullShipmentStatusValidator>();

builder.Services.AddSingleton<ILog, Log>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<GlobalErrorHandlingMiddleware>();
app.UseMiddleware<AuthMiddleware>();
app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
