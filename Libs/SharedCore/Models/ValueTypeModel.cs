namespace SharedCore.Models;

public class ValueTypeModel<T>
{
    public string Name { get; }
    public T Value { get; }

    public ValueTypeModel(string name, T value)
    {
        Name = name;
        Value = value;
    }

    public override string ToString() => $"{Name} | {Value}";
}