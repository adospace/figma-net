using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigmaNet;

internal static class Utils
{
    [return: NotNull]
    public static T EnsureNotNull<T>(this T obj)
        => obj ?? throw new InvalidOperationException();
}
