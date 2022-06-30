//Thanks to Colorful.Console
using System;
using System.Collections.Generic;
using System.Drawing;

namespace BetterConsole
{
    public static partial class ConsoleBack
    {
        private static ColorStore colorStore;
        private static ColorManagerFactory colorManagerFactory;
        private static ColorManager colorManager;
        private static Dictionary<string, COLORREF> defaultColorMap;
        private static bool isInCompatibilityMode;
        private static bool isWindows;

        private const int MAX_COLOR_CHANGES = 16;

        private const int INITIAL_COLOR_CHANGE_COUNT_VALUE = 1;

        private static readonly string WRITELINE_TRAILER = "\r\n";
        private static readonly string WRITE_TRAILER = "";

        private static void WriteInColor<T>(Action<T> action, T target, Color color)
        {
            var oldSystemColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = colorManager.GetConsoleColor(color);
            action.Invoke(target);
            System.Console.ForegroundColor = oldSystemColor;
        }
        private static void WriteInColor<T, U>(Action<T, U> action, T target0, U target1, Color color)
        {
            var oldSystemColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = colorManager.GetConsoleColor(color);
            action.Invoke(target0, target1);
            System.Console.ForegroundColor = oldSystemColor;
        }
        private static void WriteInColor<T, U>(Action<T, U, U> action, T target0, U target1, U target2, Color color)
        {
            var oldSystemColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = colorManager.GetConsoleColor(color);
            action.Invoke(target0, target1, target2);
            System.Console.ForegroundColor = oldSystemColor;
        }

        private static void WriteInColor<T, U>(Action<T, U, U, U> action, T target0, U target1, U target2, U target3, Color color)
        {
            var oldSystemColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = colorManager.GetConsoleColor(color);
            action.Invoke(target0, target1, target2, target3);
            System.Console.ForegroundColor = oldSystemColor;
        }
    }
}