using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using Azure.Identity;

namespace CoreDemoApp;

public class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await DoWork(stoppingToken);
    }

    private async Task DoWork(CancellationToken stoppingToken)
    {
        logger.LogInformation("CoreDemoApp.Worker is working.");


        var blobServiceClient = new BlobServiceClient(
            new Uri("https://elviacoredev.blob.core.windows.net"),
            new DefaultAzureCredential());
        //Create a unique name for the container
        string containerName = "oot-tmp";

        // Create the container and return a container client object
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync(cancellationToken: stoppingToken);
        
        // Create a local file in the ./data/ directory for uploading and downloading
        string localPath = "data";
        var dir = Directory.CreateTempSubdirectory(localPath);
        string fileName = "quickstart" + Guid.NewGuid() + ".txt";
        string localFilePath = Path.Combine(dir.FullName, fileName);

        // Write text to the file
        await File.WriteAllTextAsync(localFilePath, "Hello, World!", stoppingToken);

        // Get a reference to a blob
        BlobClient blobClient = containerClient.GetBlobClient(fileName);

        Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

        // Upload data from the local file, overwrite the blob if it already exists
        await blobClient.UploadAsync(localFilePath, true, stoppingToken);
    }
}