//Thanks to Colorful.Console
using System;
using System.Linq;
using System.Drawing;
using System.Runtime.InteropServices;

namespace BetterConsole
{
    public sealed class ColorManager
    {
        public bool IsInCompatibilityMode { get; private set; }

        private ColorMapper colorMapper;
        private ColorStore colorStore;
        private int colorChangeCount;
        private int maxColorChanges;
        public ColorManager(ColorStore colorStore, ColorMapper colorMapper, int maxColorChanges, int initialColorChangeCountValue, bool isInCompatibilityMode)
        {
            this.colorStore = colorStore;
            this.colorMapper = colorMapper;

            colorChangeCount = initialColorChangeCountValue;
            this.maxColorChanges = maxColorChanges;
            IsInCompatibilityMode = isInCompatibilityMode;
        }
        public Color GetColor(ConsoleColor color)
        {
            return colorStore.ConsoleColors[color];
        }
        public void ReplaceColor(Color oldColor, Color newColor)
        {
            if (IsInCompatibilityMode || !IsWindows()) return;
            ConsoleColor consoleColor = colorStore.Replace(oldColor, newColor);
            colorMapper.MapColor(consoleColor, newColor);
        }
        public ConsoleColor GetConsoleColor(Color color)
        {
            if (IsInCompatibilityMode)
            {
                return color.ToNearestConsoleColor();
            }

            try
            {
#if NETSTANDARD2_0
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
#endif
                    return GetConsoleColorNative(color);

#if NETSTANDARD2_0
                }
                else
                {
                    return color.ToNearestConsoleColor();
                }
#endif
            }
            catch
            {
                return color.ToNearestConsoleColor();
            }
        }
        public static bool IsWindows()
        {
            bool isWindows = true;

#if NETSTANDARD2_0
            isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
#endif

            return isWindows;
        }

        private ConsoleColor GetConsoleColorNative(Color color)
        {
            if (CanChangeColor() && colorStore.RequiresUpdate(color))
            {
                ConsoleColor oldColor = (ConsoleColor)colorChangeCount;

                colorMapper.MapColor(oldColor, color);
                colorStore.Update(oldColor, color);

                colorChangeCount++;
            }

            ConsoleColor nativeColor;
            return colorStore.Colors.TryGetValue(color, out nativeColor) ? nativeColor : colorStore.Colors.Last().Value;
        }

        private bool CanChangeColor()
        {
            return colorChangeCount < maxColorChanges;
        }
    }
}