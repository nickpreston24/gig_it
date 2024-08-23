namespace gig_it;

/// <summary>
/// From: https://khalidabuhakmeh.com/how-to-use-embedded-resources-in-dotnet
/// </summary>
public static class EnumerableExtensions
{
    public static string ToOxfordComma(this string[]? items)
    {
        var result = items?.Length switch
        {
            // three or more items
            >= 3 => $"{string.Join(", ", items[..^1])}, and {items[^1]}",
            // 1 item or 2 items
            not null => string.Join(" and ", items),
            // null
            _ => string.Empty
        };

        return result;
    }
}