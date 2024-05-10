using CoreDemoApp;
using Elvia.Telemetry;
using Microsoft.Extensions.Hosting;
using Elvia.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddHashiVaultSecrets();
var instrumentationKey = Elvia.Configuration.HashiVault.HashiVault.EnsureHasValue("core/kv/appinsights/core/instrumentation-key");
builder.Services
    .AddStandardElviaTelemetryLoggingWorkerService(instrumentationKey);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
