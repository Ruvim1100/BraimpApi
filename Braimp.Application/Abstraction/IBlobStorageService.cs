namespace Braimp.Application.Abstraction;
public interface IBlobStorageService
{
    Task<string> UploadAsync(Stream stream, string containerName, string blobName, string contentType);
    Task<Stream> DownloadAsync(string containerName, string blobName);
    Task DeleteAsync(string containerName, string blobName);
    Task<Uri> GenerateSasUriAsync(string containerName, string blobName, TimeSpan expiry);
}
