using System.Text;

namespace Braimp.Application.Abstraction;
public interface IBlobStorageService
{
    Task<Uri> UploadAsync(Stream stream, string containerName, string blobName, Encoding? encoding, CancellationToken cancellationToken);
    Task<Stream> DownloadAsync(string containerName, string blobName, CancellationToken cancellationToken);
    Task DeleteAsync(string containerName, string blobName, CancellationToken cancellationToken);
    (string PreviewToken, string DownloadToken) GetDownloadTokens(string containerName, string blobName,
    string fileName, TimeSpan expiry);
}
