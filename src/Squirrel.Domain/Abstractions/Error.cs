namespace Squirrel.Domain.Abstractions;

public record Error(string Code, string Description)
{
    public static Error None= new("", "");
    public static Error NullValue = new("Error.NullValue","Null value was provided");
}