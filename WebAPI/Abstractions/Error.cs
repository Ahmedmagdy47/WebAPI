namespace WebAPI.Abstractions
{
    public record Error(string code, string message, int? StatusCode)
    {
        public static readonly Error None = new(string.Empty, string.Empty, null);
    }
}
