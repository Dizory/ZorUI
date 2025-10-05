using System.Text.Json;

namespace ZorUI.Core.Storage;

/// <summary>
/// Local storage for persisting data.
/// </summary>
public class LocalStorage
{
    private readonly string _basePath;

    public LocalStorage(string? basePath = null)
    {
        _basePath = basePath ?? Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "ZorUI"
        );

        if (!Directory.Exists(_basePath))
        {
            Directory.CreateDirectory(_basePath);
        }
    }

    /// <summary>
    /// Save data to storage.
    /// </summary>
    public void Save<T>(string key, T value)
    {
        var json = JsonSerializer.Serialize(value, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        var path = GetPath(key);
        File.WriteAllText(path, json);
    }

    /// <summary>
    /// Load data from storage.
    /// </summary>
    public T? Load<T>(string key, T? defaultValue = default)
    {
        var path = GetPath(key);

        if (!File.Exists(path))
            return defaultValue;

        try
        {
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json);
        }
        catch
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// Check if key exists.
    /// </summary>
    public bool Has(string key)
    {
        return File.Exists(GetPath(key));
    }

    /// <summary>
    /// Remove data from storage.
    /// </summary>
    public void Remove(string key)
    {
        var path = GetPath(key);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    /// <summary>
    /// Clear all storage.
    /// </summary>
    public void Clear()
    {
        if (Directory.Exists(_basePath))
        {
            Directory.Delete(_basePath, recursive: true);
            Directory.CreateDirectory(_basePath);
        }
    }

    /// <summary>
    /// Get all keys.
    /// </summary>
    public List<string> GetAllKeys()
    {
        return Directory.GetFiles(_basePath, "*.json")
            .Select(Path.GetFileNameWithoutExtension)
            .Where(k => k != null)
            .Select(k => k!)
            .ToList();
    }

    private string GetPath(string key) => Path.Combine(_basePath, $"{key}.json");
}

/// <summary>
/// Extension methods for state persistence.
/// </summary>
public static class StorageExtensions
{
    /// <summary>
    /// Save component state to storage.
    /// </summary>
    public static void SaveState(this ZorComponent component, LocalStorage storage, string key)
    {
        // Implementation would need access to internal state
        // For now, components need to implement their own SaveState
    }

    /// <summary>
    /// Load component state from storage.
    /// </summary>
    public static void LoadState(this ZorComponent component, LocalStorage storage, string key)
    {
        // Implementation would need access to internal state
    }
}
