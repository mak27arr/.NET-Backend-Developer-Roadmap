using Settings.Interface;

namespace Settings
{
    internal class Setting : ISetting
    {
        public PathGeneratorSetting PathGeneratorSetting { get; }

        public PreviewSetting PreviewSetting { get; }
    }
}