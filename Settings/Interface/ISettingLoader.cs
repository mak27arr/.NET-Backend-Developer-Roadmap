namespace Settings.Interface
{
    public interface ISettingLoader : IDisposable
    {
        ISetting Setting { get; }
        void Load();
        void Save();
    }
}
