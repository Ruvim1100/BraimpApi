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

    public async Task<Uri> UploadAsync(Stream stream, string containerName, string blobName, Encoding? encoding = null, CancellationToken cancellationToken = default)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.None, cancellationToken: cancellationToken).ConfigureAwait(false);


        var blobClient = containerClient.GetBlobClient(blobName);
        stream.Position = 0;

        var contentType = ContentTypeProvider.GetContentType(blobName, encoding);
        var headers = new BlobHttpHeaders { ContentType = contentType };

        await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = headers }, cancellationToken).ConfigureAwait(false);
        return blobClient.Uri;
    }

    public async Task DeleteAsync(string containerName, string blobName, CancellationToken cancellationToken)
    {
        var container = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = container.GetBlobClient(blobName);

        await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<Stream> DownloadAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(containerName)) throw new ArgumentException("Container name cannot be null or empty", nameof(containerName));
        if (string.IsNullOrWhiteSpace(blobName)) throw new ArgumentException("Blob name cannot be null or empty", nameof(blobName));

        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);

        var response = await blobClient.DownloadAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        return response.Value.Content;
    }


    public (string PreviewToken, string DownloadToken) GetDownloadTokens(string containerName, string blobName,
    string fileName, TimeSpan expiry)
    {
        var container = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = container.GetBlobClient(blobName);

        if (!blobClient.CanGenerateSasUri)
        {
            throw new InvalidOperationException("Cannot generate SAS URI for the blob.");
        }

        var expiryTime = DateTimeOffset.UtcNow.Add(expiry);

        var sasBuilder = new BlobSasBuilder
        {
            BlobContainerName = containerName,
            BlobName = blobName,
            Resource = "b",
            ExpiresOn = expiryTime
        };

        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        sasBuilder.ContentDisposition = $"inline; filename=\"{fileName}\"";
        var previewToken = blobClient.GenerateSasUri(sasBuilder).Query.TrimStart('?');
        var previewUrl = $"{blobClient.Uri}?{previewToken}";

        sasBuilder.ContentDisposition = $"attachment; filename=\"{fileName}\"";
        var downloadToken = blobClient.GenerateSasUri(sasBuilder).Query.TrimStart('?');
        var downloadUrl = $"{blobClient.Uri}?{downloadToken}";

        return (previewUrl, downloadUrl);
    }

}
