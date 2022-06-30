//Thanks to Colorful.Console
using System;
using System.Drawing;

namespace BetterConsole
{
    public static class ColorExtensions
    {
        public static ConsoleColor ToNearestConsoleColor(this Color color)
        {
            ConsoleColor closestConsoleColor = 0;
            double delta = double.MaxValue;

            foreach (ConsoleColor consoleColor in Enum.GetValues(typeof(ConsoleColor)))
            {
                string consoleColorName = Enum.GetName(typeof(ConsoleColor), consoleColor);
                consoleColorName = string.Equals(consoleColorName, nameof(ConsoleColor.DarkYellow), StringComparison.Ordinal) ? nameof(Color.Orange) : consoleColorName;
                Color rgbColor = Color.FromName(consoleColorName);
                double sum = Math.Pow(rgbColor.R - color.R, 2.0) + Math.Pow(rgbColor.G - color.G, 2.0) + Math.Pow(rgbColor.B - color.B, 2.0);

                double epsilon = 0.001;
                if (sum < epsilon)
                {
                    return consoleColor;
                }

                if (sum < delta)
                {
                    delta = sum;
                    closestConsoleColor = consoleColor;
                }
            }

            return closestConsoleColor;
        }
    }
}