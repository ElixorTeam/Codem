namespace Codem.Infrastructure.Models;

public class SqlSettings
{
    public string DataSource { get; set; } = string.Empty;
    public string InitialCatalog { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool TrustServerCertificate { get; set; }

    public string GetConnectionString() =>
        $"Data Source={DataSource}; " +
        $"Initial Catalog={InitialCatalog}; " +
        $"User ID={UserId}; " +
        $"Password={Password}; " +
        $"TrustServerCertificate={TrustServerCertificate}; ";
}