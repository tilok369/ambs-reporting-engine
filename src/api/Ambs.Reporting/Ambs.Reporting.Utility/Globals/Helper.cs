namespace Ambs.Reporting.Utility.Globals;
public static class Helper
{
    public static string ConstructCommand(string script, Dictionary<string, string> parameters)
    {
        script = script.ToLower();
        foreach (var p in parameters)
        {
            script = script.Replace(p.Key.ToLower(), p.Value);
        }
        return script;
    }
    public static string ConstructScriptForDropdown(string script)
    {
        script = script.Contains("@") ? script.Substring(0, script.IndexOf("@")) + " -1" : script;
        return script;
    }
}

