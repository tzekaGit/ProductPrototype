using System.Text.Json;

namespace ProductPrototype.DbAccess.Interfaces;

public interface IJsonConfigStore
{
    string SerializeSection(string path, bool indented = true);
    T DeserializeSection<T>(string path, JsonSerializerOptions? options = null);
}