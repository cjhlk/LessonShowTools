using System;
using System.IO;
using System.Windows;

using Pastel;

namespace LessonShowTools.Services;

public class ConsoleService
{
    public static string AsciiLogo = "";
    public static HWND ConsoleHWnd { get; private set; }

    public static void InitializeConsole()
    {
#if DEBUG
        if (ConsoleHWnd == nint.Zero)
        {
            AllocConsole();
        }
        ConsoleHWnd = GetConsoleWindow();
        SetWindowText(ConsoleHWnd, "LessonShowTools 输出");
#endif
        PrintAppInfo();
    }

    public static void PrintAppInfo()
    {
        var s = Application.GetResourceStream(new Uri("/Assets/AsciiLogo.txt", UriKind.RelativeOrAbsolute))?.Stream;
        if (s != null)
        {
            AsciiLogo = new StreamReader(s).ReadToEnd();
        }
        Console.WriteLine(AsciiLogo.Pastel("#00bfff"));
        Console.WriteLine($"LessonShowTools {App.AppVersionLong}");
        Console.WriteLine("LessonShowTools Debug Version. For testing purposes only.".Pastel("#48C0F8"));
        Console.WriteLine();
    }

    public ConsoleService()
    {
    }
}