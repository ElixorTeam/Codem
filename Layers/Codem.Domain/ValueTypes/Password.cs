using Codem.Domain.Common;

namespace Codem.Domain.ValueTypes;

public class Password : IValueType<Password>
{
    public string Value { get; init; }
    
    public Password(string value)
    {
        Value = value;
    }
    
    public bool Equals(Password? other)
    {
        if (ReferenceEquals(null, other))
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return Value == other.Value;
    }
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj) || obj.GetType() != GetType())
            return false;
        return ReferenceEquals(this, obj) || Equals((Password)obj);
    }
    
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value;
    }
}