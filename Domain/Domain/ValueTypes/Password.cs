namespace Domain.ValueTypes;

public class Password
{
    public string Value { get; init; }
    
    public Password(string value)
    {
        // TODO: выдать ошибку при неправильном пароле
        Value = value;
    }
}