using System;
using Elvia.Telemetry;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Starting core-demo-app");
var instrumentationKey = Elvia.Configuration.HashiVault.HashiVault.EnsureHasValue("core/kv/appinsights/core/instrumentation-key");
Console.WriteLine($"Instrumentation key: {instrumentationKey}");

await Host
    .CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services
            .AddStandardElviaTelemetryLoggingWorkerService(instrumentationKey);
    }).RunConsoleAsync();
    