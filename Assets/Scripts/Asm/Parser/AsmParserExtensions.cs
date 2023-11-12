using System.Linq;
using JetBrains.Annotations;

public static class AsmParserExtensions
{
    [Pure] public static bool IsNumber(this char c) => char.IsDigit(c) || c == '-';
    [Pure] public static bool IsReg(this char c) => c == 'r';
    [Pure] public static bool IsMem(this char c) => c == '$';
    [Pure] public static bool IsNewLine(this char c) => c == '\n';
    [Pure] public static bool IsEof(this char c) => c == '\0';
    [Pure] public static int CountLines(this string s, int len) => s[..len].Count(x => x.IsNewLine()) + 1;
    [Pure] public static bool IsComment(this string s) => s.Trim().StartsWith(';');

    [Pure] public static bool IsLineOfSpaces(this string s)
    {
        var indexOf = s.IndexOf('\n');
        return indexOf != -1 && s[..indexOf].All(char.IsWhiteSpace);
    }
}