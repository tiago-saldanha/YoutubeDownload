using Windows.Storage.Pickers;
using YoutubeDownloader.SharedUI.Interfaces;

namespace YoutubeDownloader.Desktop.Services
{
    public class MauiDeviceService : IDeviceService
    {
        public bool Desktop => true;

        public async Task OpenFileAsync(string filePath)
        {
            await Launcher.Default.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(filePath)
            });
        }

        public async Task<string?> PickFolderAsync()
        {
#if WINDOWS
            var picker = new FolderPicker();

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.Current.Windows[0].Handler.PlatformView);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

            picker.FileTypeFilter.Add("*");

            var folder = await picker.PickSingleFolderAsync();

            return folder?.Path ?? null;
#endif
        }
    }
}
