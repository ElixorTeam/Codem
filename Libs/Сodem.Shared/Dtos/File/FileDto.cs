namespace Сodem.Shared.Dtos.File;

public class FileDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Data { get; set; }

    public FileDto()
    {
        Id = Guid.Empty;
        Name = string.Empty;
        Data = string.Empty;
    }
}