using YoutubeDownloader.SharedUI.Interfaces;

namespace YoutubeDownloader.Web.Services
{
    public class WebDeviceService : IDeviceService
    {
        public bool Desktop => false;

        public Task OpenFileAsync(string filePath) => Task.CompletedTask;

        public Task<string?> PickFolderAsync()
        {
            throw new NotImplementedException();
        }
    }
}
