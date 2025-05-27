using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Braimp.Application.Abstraction;
using System.Text;

namespace Braimp.Infrastructure.BlobStorage;
public class BlobStorageService : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobStorageService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }


    public async Task<string> UploadAsync(Stream stream, string containerName, string blobName, Encoding? encoding = null)
    {
        var container = _blobServiceClient.GetBlobContainerClient(containerName);
        await container.CreateIfNotExistsAsync();

        var blobClient = container.GetBlobClient(blobName);

        var contentType = ContentTypeProvider.GetContentType(blobName, encoding);
        var blobHttpHeaders = new BlobHttpHeaders
        {
            ContentType = contentType
        };

        await blobClient.UploadAsync(stream, new BlobUploadOptions
        {
            HttpHeaders = blobHttpHeaders
        });

        return blobClient.Uri.ToString();
    }

    public async Task DeleteAsync(string containerName, string blobName)
    {
        var container = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = container.GetBlobClient(blobName);

        await blobClient.DeleteIfExistsAsync();
    }

    public async Task<Stream> DownloadAsync(string containerName, string blobName)
    {
        var container = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = container.GetBlobClient(blobName);

        var response = await blobClient.DownloadAsync();

        return response.Value.Content;
    }
    //GetDownloadUri
    public Task<Uri> GenerateSasUriAsync(string containerName, string blobName, TimeSpan expiry) // not async
    {
        var container = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = container.GetBlobClient(blobName);

        var sasUri = blobClient.GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.UtcNow.Add(expiry));
        return Task.FromResult(sasUri);
    }
}
