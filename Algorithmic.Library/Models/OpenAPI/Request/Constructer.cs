using System.Reflection;

namespace ShareInvest.Models.OpenAPI.Request;

public static class Constructer
{
    public static TR? GetInstance(string name)
    {
        var typeName = string.Concat(typeof(Constructer).Namespace, '.', name);

        return Assembly.GetExecutingAssembly()
                       .CreateInstance(typeName, true) as TR;
    }
}