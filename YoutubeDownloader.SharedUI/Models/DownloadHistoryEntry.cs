namespace YoutubeDownloader.SharedUI.Models
{
    public record DownloadHistoryEntry
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();
        public string VideoId { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public string ThumbnailUrl { get; init; } = string.Empty;
        public string FilePath { get; init; } = string.Empty;
        public string FileName { get; init; } = string.Empty;
        public string Format { get; init; } = string.Empty;
        public string Quality { get; init; } = string.Empty;
        public bool IsAudioOnly { get; init; }
        public double FileSizeMB { get; init; }
        public DateTime DownloadedAt { get; init; } = DateTime.Now;

        public bool FileExists => File.Exists(FilePath);
        public string DisplayDate => DownloadedAt.ToString("dd/MM/yyyy HH:mm");
        public string DisplaySize => FileSizeMB > 1024
            ? $"{FileSizeMB / 1024:F1} GB"
            : $"{FileSizeMB:F1} MB";
    }
}
