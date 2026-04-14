using System.Text.Json;
using YoutubeDownloader.SharedUI.Interfaces;
using YoutubeDownloader.SharedUI.Models;

namespace YoutubeDownloader.Desktop.Services
{
    public class DownloadHistoryService : IDownloadHistoryService
    {
        private readonly SemaphoreSlim _lock = new(1, 1);
        private List<DownloadHistoryEntry>? _cache;

        private static string GetHistoryFilePath()
        {
            return Path.Combine(FileSystem.AppDataDirectory, "history.json");
        }

        public IReadOnlyList<DownloadHistoryEntry> GetAll()
        {
            _cache ??= LoadFromFile();
            return _cache.AsReadOnly();
        }

        public async Task AddAsync(DownloadHistoryEntry entry)
        {
            await _lock.WaitAsync();
            try
            {
                _cache ??= LoadFromFile();
                _cache.Insert(0, entry);

                if (_cache.Count > 100)
                    _cache = [.. _cache.Take(100)];

                await SaveToFileAsync(_cache);
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task RemoveAsync(string id)
        {
            await _lock.WaitAsync();
            try
            {
                _cache ??= LoadFromFile();
                _cache.RemoveAll(e => e.Id == id);
                await SaveToFileAsync(_cache);
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task ClearAsync()
        {
            await _lock.WaitAsync();
            try
            {
                _cache = [];
                await SaveToFileAsync(_cache);
            }
            finally
            {
                _lock.Release();
            }
        }

        private static List<DownloadHistoryEntry> LoadFromFile()
        {
            var path = GetHistoryFilePath();

            if (!File.Exists(path))
                return [];

            try
            {
                var json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<List<DownloadHistoryEntry>>(json) ?? [];
            }
            catch
            {
                return [];
            }
        }

        private static async Task SaveToFileAsync(List<DownloadHistoryEntry> entries)
        {
            var path = GetHistoryFilePath();
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(entries, options);
            await File.WriteAllTextAsync(path, json);
        }
    }
}
