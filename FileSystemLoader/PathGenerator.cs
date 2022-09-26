using FileSystemLoader.Service;
using myCloudDAL.DAL.Entities.File;
using Settings;
using Settings.Interface;

namespace FileSystemLoader
{
    public class PathGenerator : IPathGenerator
    {
        private readonly PathGeneratorSetting _setting;

        public PathGenerator(ISettingLoader settingLoader)
        {
            _setting = settingLoader.Setting.PathGeneratorSetting;
        }

        public string GeneratePath<T>(UserFile<T> fileInfo, string fileNamePrefix) where T : struct
        {
            var path = GetBasePathForUser(fileInfo);
            var attemp = 10;

            do
            {
                var guid = Guid.NewGuid();
                var filePath = Path.Combine(path, $"{fileNamePrefix}{guid.ToString()}.{fileInfo.FileExtension}");

                if (!File.Exists(filePath))
                    return filePath;

                attemp++;
            } while (attemp > 0);

            return GetFilePathByFileInfo(path, fileInfo);
        }

        private string GetFilePathByFileInfo<T>(string basePath, UserFile<T> fileInfo) where T : struct
        {
            return string.Join("", Path.Combine(basePath, $"{fileInfo.Owner}{fileInfo.Created:dd:MM:yyyy:HH:mm:ss:FFFFF}").Split(Path.GetInvalidFileNameChars()));
        }

        private string GetBasePathForUser<T>(UserFile<T> fileInfo) where T : struct
        {
            var baseFilePath = Path.Combine(_setting.BasePath, fileInfo.Owner.ToString());

            if (!Directory.Exists(baseFilePath))
                Directory.CreateDirectory(baseFilePath);

            return baseFilePath;
        }
    }
}
