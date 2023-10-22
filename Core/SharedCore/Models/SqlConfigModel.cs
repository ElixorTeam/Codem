namespace SharedCore.Models;

public class SqlConfigModel
{
    public string DataSource { get; set; }
    public string InitialCatalog { get; set; }
    public bool PersistSecurityInfo { get; set; }
    public bool IntegratedSecurity { get; set; }
    public string UserId { get; set; }
    public string Password { get; set; }
    public string Schema { get; set; }
    public ushort ConnectTimeout { get; set; }
    public bool Encrypt { get; set; }
    public bool TrustServerCertificate { get; set; }
    
    
    public SqlConfigModel()
    {
        DataSource = string.Empty;
        InitialCatalog = string.Empty;
        PersistSecurityInfo = false;
        IntegratedSecurity = false;
        UserId = string.Empty;
        Password = string.Empty;
        Schema = string.Empty;
        ConnectTimeout = 15;
        Encrypt = false;
        TrustServerCertificate = false;
    }

}