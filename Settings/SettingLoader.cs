using Settings.Interface;
using System.Text;
using System.Text.Json;

namespace Settings
{
    //need to make thread safe
    public class SettingLoader : ISettingLoader
    {
        private ISetting _setting;

        public ISetting Setting
        {
            get
            {
                if (_setting == null)
                    Load();

                return _setting;
            }
        }

        public void Load()
        {
            var path = GetSettingPath();

            if (File.Exists(path))
                _setting = JsonSerializer.Deserialize<ISetting>(File.ReadAllText(path, Encoding.UTF8));
            else
                _setting = new Setting();
        }

        public void Save()
        {
            var fileContent = JsonSerializer.Serialize(_setting);
            File.WriteAllText(GetSettingPath(), fileContent, Encoding.UTF8);
        }

        private string GetSettingPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appSetting.json");
        }

        public void Dispose()
        {
            Save();
            GC.SuppressFinalize(this);
        }
    }
}
