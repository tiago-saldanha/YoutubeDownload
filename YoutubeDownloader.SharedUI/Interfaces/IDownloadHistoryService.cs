using YoutubeDownloader.SharedUI.Models;

namespace YoutubeDownloader.SharedUI.Interfaces
{
    public interface IDownloadHistoryService
    {
        IReadOnlyList<DownloadHistoryEntry> GetAll();
        Task AddAsync(DownloadHistoryEntry entry);
        Task RemoveAsync(string id);
        Task ClearAsync();
    }
}
