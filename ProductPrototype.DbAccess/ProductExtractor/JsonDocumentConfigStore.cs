using ProductPrototype.DbAccess.Interfaces;
using System.Text.Json;

namespace ProductPrototype.DbAccess.ProductExtractor;

internal sealed class JsonDocumentConfigStore : IJsonConfigStore, IDisposable
{
    private readonly JsonDocument _document;
    private readonly JsonSerializerOptions _defaultOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public JsonDocumentConfigStore(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("Configuration file path is required.", nameof(filePath));
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Configuration file not found at '{filePath}'.", filePath);
        }

        using FileStream stream = File.OpenRead(filePath);
        _document = JsonDocument.Parse(stream);
    }

    public string SerializeSection(string path, bool indented = true)
    {
        JsonElement element = GetElementByPath(_document.RootElement, path);
        return JsonSerializer.Serialize(element, new JsonSerializerOptions
        {
            WriteIndented = indented
        });
    }

    public T DeserializeSection<T>(string path, JsonSerializerOptions? options = null)
    {
        JsonElement element = GetElementByPath(_document.RootElement, path);
        return element.Deserialize<T>(options ?? _defaultOptions)
            ?? throw new InvalidOperationException($"Failed to deserialize path '{path}' into {typeof(T).Name}.");
    }

    private static JsonElement GetElementByPath(JsonElement root, string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return root;
        }

        JsonElement current = root;
        string[] segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        foreach (string segment in segments)
        {
            if (current.ValueKind != JsonValueKind.Object || !current.TryGetProperty(segment, out JsonElement next))
            {
                throw new KeyNotFoundException($"JSON path '{path}' was not found (failed at '{segment}').");
            }

            current = next;
        }

        return current;
    }

    public void Dispose() => _document.Dispose();
}