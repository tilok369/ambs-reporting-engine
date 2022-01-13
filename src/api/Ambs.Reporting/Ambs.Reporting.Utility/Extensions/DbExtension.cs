using Microsoft.Data.SqlClient;

namespace Ambs.Reporting.Utility.Extensions
{
    public static class DbExtension
    {
        public static string GetStringValue(this SqlDataReader reader, string name)
        {
            return reader[name] == null ? string.Empty : reader[name].ToString();
        }
    }
}
