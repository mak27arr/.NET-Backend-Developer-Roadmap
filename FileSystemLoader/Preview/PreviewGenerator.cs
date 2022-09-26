using FileSystemLoader.Service;
using myCloudDAL.DAL.Entities.File;
using Settings.Interface;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace FileSystemLoader.Preview
{
    public class PreviewGenerator : IPreviewGenerator
    {
        private readonly PreviewSetting _setting;
        private readonly IPathGenerator _pathGenerator;

        public PreviewGenerator(ISettingLoader settingLoader, IPathGenerator pathGenerator)
        {
            _setting = settingLoader.Setting.PreviewSetting;
            _pathGenerator = pathGenerator;
        }

        public PreviewFile<T> Generate<T>(UserFile<T> fileInfo) where T : struct
        {
            var previePath = _pathGenerator.GeneratePath(fileInfo, $"Preview{Path.GetFileNameWithoutExtension(fileInfo.FilePath)}");
            var created = WritePreview(fileInfo.FilePath, previePath);
            return new PreviewFile<T>
            {
                File = fileInfo,
                FileId = fileInfo.Id,
                FilePath = created ? previePath : string.Empty
            };
        }

        private bool WritePreview(string imgPath, string previewPath)
        {
            if (!File.Exists(imgPath))
                return false;

            using (var image = Image.Load(imgPath))
            { 
                image.Mutate(x => x.Resize(_setting.Width, _setting.Height));
                image.Save(previewPath);
            }

            return true;
        }
    }
}
