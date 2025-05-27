using System.Text;

namespace Braimp.Infrastructure.BlobStorage;
internal class ContentTypeProvider
{
    private static readonly Dictionary<string, string> Types = new()
    {
        {".txt", "text/plain"},
        {".pdf", "application/pdf"},
        {".doc", "application/vnd.ms-word"},
        {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
        {".xls", "application/vnd.ms-excel"},
        {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
        {".png", "image/png"},
        {".jpg", "image/jpeg"},
        {".jpeg", "image/jpeg"},
        {".gif", "image/gif"},
        {".csv", "text/csv"}
    };

    public static string GetContentType(string fileName, Encoding? encoding = null)
    {
        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        var type = Types.TryGetValue(ext, out var value) ? value : "application/octet-stream";
        if (ext == ".txt" && encoding != null)
        {
            type = $"{type}; charset={encoding.WebName}";
        }
        return type;
    }
}
