using System.Text;

namespace Braimp.Application.Abstraction;
public interface IBlobStorageService
{
    Task<string> UploadAsync(Stream stream, string containerName, string blobName, Encoding? encoding = null);
    Task<Stream> DownloadAsync(string containerName, string blobName);
    Task DeleteAsync(string containerName, string blobName);
    Task<Uri> GenerateSasUriAsync(string containerName, string blobName, TimeSpan expiry);
}
