using Braimp.Application.Abstraction;

namespace Braimp.Infrastructure.BlobStorage;
public class BlobStorageService : IBlobStorageService
{
    public Task DeleteAsync(string containerName, string blobName)
    {
        throw new NotImplementedException();
    }

    public Task<Stream> DownloadAsync(string containerName, string blobName)
    {
        throw new NotImplementedException();
    }

    public Task<Uri> GenerateSasUriAsync(string containerName, string blobName, TimeSpan expiry)
    {
        throw new NotImplementedException();
    }

    public Task<string> UploadAsync(Stream stream, string containerName, string blobName, string contentType)
    {
        throw new NotImplementedException();
    }
}
