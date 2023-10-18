namespace WebClient.Models;

public sealed class EnumTypeModel<T>
{
    public string Name { get; }
    public T Value { get; }

    public EnumTypeModel(string name, T value)
    {
        Name = name;
        Value = value;
    }

    public override string ToString() => $"{Name} | {Value}";
}