namespace TimeLog.Library.Configuration
{
    /// <summary>
    /// Logic for handling personal application settings based on machine name.
    /// Used to keep individual settings for each machine in version control -
    /// or simple leave them out to ensure that credentials are not shared.
    /// </summary>
    public class PersonalConfigurationManager
    {
        public static AppSetting AppSettings
        {
            get
            {
                return new AppSetting();
            }
        }
    }
}