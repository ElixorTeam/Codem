namespace WebClient.Components.Profile;

public class SnippetTableModel
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public DateOnly PublicDate { get; init; } = DateOnly.FromDateTime(DateTime.Today);
    public string ExpireTime { get; init; } = string.Empty;
    public int Views { get; init; } = int.MinValue;
    public int Stars { get; init; } = int.MinValue;
    public string ProgramLanguage { get; init; } = string.Empty;
}