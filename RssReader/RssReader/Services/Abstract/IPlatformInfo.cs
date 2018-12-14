namespace RssReader.Services.Abstract
{
    /// <summary>Информация о платформе (Android, iOS)</summary>
    public interface IPlatformInfo
    {
        /// <summary>Путь к файлу базы данных</summary>
        string DbFileName { get; }
    }
}
