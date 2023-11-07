namespace Сodem.Shared.Dtos.File;

public class FileCreateDto 
{
    public string Name { get; set; }
    public string Data { get; set; }

    public FileCreateDto()
    {
        Name = string.Empty;
        Data = string.Empty;
    }
}
