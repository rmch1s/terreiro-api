namespace Terreiro.Application.Helpers;

public static class StringHelper
{
    public static string InsertParams(this string message, params object[] parameters) =>
        string.Format(message, parameters);
}
