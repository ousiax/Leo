// MIT License

using System.Globalization;

namespace Leo.Wpf.App.Infrastructure
{
    public partial class Constants
    {
        public static readonly Dictionary<string, string> Genders = new()
        {
            {"Unknown", GetString("Unknown") },
            {"Male", GetString("Male") },
            {"Female", GetString("Female") },
        };

        // TODO i18n
        private static string GetString(string key)
        {
            var enUS = new Dictionary<string, string>()
            {
                {"Unknown", "Unknown" },
                {"Male", "Male" },
                {"Female", "Female" },
            };
            var zhCN = new Dictionary<string, string>()
            {
                {"Unknown", "未知" },
                {"Male", "男" },
                {"Female", "女" },
            };

            if (CultureInfo.CurrentUICulture.Name.Contains("zh"))
            {
                return zhCN[key];
            }
            return enUS[key];
        }
    }
}
