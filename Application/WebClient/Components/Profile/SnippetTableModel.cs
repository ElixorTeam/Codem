namespace WebClient.Components.Profile;

public class SnippetTableModel
{
    public String Title { get; set; } = String.Empty;
    public DateOnly PublicDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public String ExpireTime { get; set; } = String.Empty;
    public Int32 Views { get; set; } = Int32.MinValue;
    public Int32 Stars { get; set; } = Int32.MinValue;
    public String ProgramLanguage { get; set; } = String.Empty;
}