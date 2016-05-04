using System.Configuration;

namespace ActiveCitizenWeb.StaticContentCMS.Configuration
{
    public class AppSettings : IAppSettings
    {
        public bool CreateDefaultCredentialsOnStart
        {
            get { return ReadAppSetting("CreateDefaultCredentialsOnStart", false); }
        }

        protected virtual string ReadAppSetting(string key, string defaultValue)
        {
            var value = ConfigurationManager.AppSettings[key];
            return !string.IsNullOrEmpty(value) ? value : defaultValue;
        }

        protected bool ReadAppSetting(string key, bool defaultValue)
        {
            var strValue = ConfigurationManager.AppSettings[key];
            bool value;
            return (!string.IsNullOrEmpty(strValue) && bool.TryParse(strValue, out value)) ? value : defaultValue;
        }
    }
}