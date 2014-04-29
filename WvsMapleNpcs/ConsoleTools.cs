using System;
using System.Text.RegularExpressions;
public static class ConsoleTools
{
    /*
    #b = Blue text.
    #d = Purple text.
    #g = Green text.
    #k = Black text.
    #r = Red text.
    No Bold.
     */
    public static void PrintWithNpcColor(string text)
    {
        ConsoleColor originalColor = Console.ForegroundColor;
        var matches = Regex.Matches(text, "#[bkdrg]");

        if (matches.Count == 0)
        {
            Console.WriteLine(text);
            return;
        }

        if (matches[0].Index > 0)
        {
            Console.Write(text.Substring(0, matches[0].Index - 0));
        }

        for (int i = 0; i < matches.Count; i++)
        {
            int startIndex = matches[i].Index + 2;
            int endIndex = text.Length;
            if (i + 1 < matches.Count)
            {
                endIndex = matches[i + 1].Index;
            }
            string tag = text.Substring(matches[i].Index, 2);
            Console.ForegroundColor = GetConsoleColorFromNpcColorTag(tag, originalColor);

            Console.Write(text.Substring(startIndex, endIndex - startIndex));
        }
        Console.WriteLine();
        Console.ForegroundColor = originalColor;
    }

    public static ConsoleColor GetConsoleColorFromNpcColorTag(string tag, ConsoleColor defaultColor)
    {
        switch (tag)
        {
            case "#b":
                return ConsoleColor.Blue;
            case "#k":
                return defaultColor;
            case "#d":
                return ConsoleColor.Magenta;
            case "#r":
                return ConsoleColor.Red;
            case "#g":
                return ConsoleColor.Green;
        }
        return defaultColor;
    }
}