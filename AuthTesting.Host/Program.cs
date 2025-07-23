using AuthTesting.Application.Extensions;
using AuthTesting.Host.Extensions.Middleware;
using AuthTesting.Infrastructure.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
ConfigurationManager configuration = builder.Configuration;

// builder.WebHost.UseKestrel(options =>
// {
// 	
// 	options.Listen(IPAddress.Parse("10.122.82.16") , 7044);
// });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

services
	.AddAuthentication(configuration)
	.AddInfrastructure(configuration)
	.AddApplication();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseApiMiddlewares(configuration);
await app.RunAsync();