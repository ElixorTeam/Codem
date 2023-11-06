using Codem.Domain.Tests.Common;
using Codem.Domain.ValueTypes;
using Xunit.Abstractions;
namespace Codem.Domain.Tests.ValueTypes;


public class PasswordTests : IValueTypeTest
{
    private readonly ITestOutputHelper _output;

    public PasswordTests(ITestOutputHelper output)
    {
        _output = output;
    }
    
    [Fact]
    public void Equals_is_valid()
    {
        const string data = "Test";
        
        Password pwd = new(data);
        Password pwd2 = new(data);
        Password pwd3 = pwd2;
        
        _output.WriteLine($"{pwd} : {pwd2} : {pwd3}");
        Assert.True(pwd.Equals(pwd2));
        Assert.True(pwd.Equals(pwd3));
        Assert.True(pwd2.Equals(pwd3));
    }
    
    [Fact]
    public void Equals_is_not_valid()
    {
        Password pwd = new("Test2");
        Password pwd2 = new("Test");

        _output.WriteLine($"{pwd} : {pwd2}");
        Assert.False(pwd.Equals(pwd2));
    }
    
}