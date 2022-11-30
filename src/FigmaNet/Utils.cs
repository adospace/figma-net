using System.Diagnostics.CodeAnalysis;

namespace FigmaNet;

internal static class Utils
{
    [return: NotNull]
    public static T EnsureNotNull<T>(this T obj)
        => obj ?? throw new InvalidOperationException();
}
