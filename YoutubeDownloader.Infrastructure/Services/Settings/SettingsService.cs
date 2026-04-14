using Microsoft.Maui.Storage;

namespace YoutubeDownloader.Infrastructure.Services.Settings
{
    public class SettingsService
    {
        private const string DownloadPathKey = "download_path";

        /// <summary>
        /// Retorna o caminho configurado para download
        /// </summary>
        public string GetDownloadPath()
        {
            return Preferences.Get(DownloadPathKey, GetDefaultPath());
        }

        /// <summary>
        /// Define o caminho de download
        /// </summary>
        public void SetDownloadPath(string path)
        {
            Preferences.Set(DownloadPathKey, path);
        }

        /// <summary>
        /// Retorna o caminho padrão da aplicação
        /// </summary>
        public string GetDefaultPath()
        {
            return FileSystem.AppDataDirectory;
        }

        /// <summary>
        /// Reseta para o padrão
        /// </summary>
        public void ResetDownloadPath()
        {
            Preferences.Remove(DownloadPathKey);
        }
    }
}
