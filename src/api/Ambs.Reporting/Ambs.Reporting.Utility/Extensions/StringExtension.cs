using Microsoft.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Ambs.Reporting.Utility.Extensions;
public static class StringExtension
{
    public static string ToSentenceCase(this string str)
    {
        return Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToUpper(m.Value[1]));
    }

    public static SqlParameter[] ToSqlParameterVals(this string str) =>
        str.Split('|').Select(s => new SqlParameter(s.Split('#').First(), s.Split('#').Last())).ToArray();
}
