namespace Dima.Core.Categories;

public sealed record UserId
{
    public const int MaxLength = 255;

    private UserId(string value)
    {
        Value = value;
    }

    public string Value { get; init; }

    public static implicit operator string(UserId userId)
        => userId.Value;

    public static implicit operator UserId(string value)
        => Create(value);

    public static UserId Create(string value)
    {
        Ensure.NotEmpty(value, CategoryErrors.UserId.NullOrEmpty);
        Ensure.MaxLength(value, MaxLength, CategoryErrors.UserId.LongerThanAllowed(MaxLength));
        return new UserId(value);
    }
}

