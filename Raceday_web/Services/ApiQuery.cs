namespace RaceDay_web.Services
{
    internal static class ApiQuery
    {
        public static string Build(params (string Key, string? Value)[] values)
        {
            return string.Join("&", values.Select(x =>
                $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value ?? "")}"));
        }
    }
}
