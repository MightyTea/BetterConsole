//Thanks to Colorful.Console
using System;
using System.Collections.Concurrent;
using System.Drawing;

namespace BetterConsole
{
    public sealed class ColorStore
    {
        public ConcurrentDictionary<Color, ConsoleColor> Colors { get; private set; }
        public ConcurrentDictionary<ConsoleColor, Color> ConsoleColors { get; private set; }
        public ColorStore(ConcurrentDictionary<Color, ConsoleColor> colorMap, ConcurrentDictionary<ConsoleColor, Color> consoleColorMap)
        {
            Colors = colorMap;
            ConsoleColors = consoleColorMap;
        }
        public void Update(ConsoleColor oldColor, Color newColor)
        {
            Colors.TryAdd(newColor, oldColor);
            ConsoleColors[oldColor] = newColor;
        }
        public ConsoleColor Replace(Color oldColor, Color newColor)
        {
            bool oldColorExistedInColorStore = Colors.TryRemove(oldColor, out var consoleColorKey);

            if (!oldColorExistedInColorStore)
            {
                throw new ArgumentException("An attempt was made to replace a nonexistent color in the ColorStore!");
            }

            Colors.TryAdd(newColor, consoleColorKey);
            ConsoleColors[consoleColorKey] = newColor;

            return consoleColorKey;
        }
        public bool RequiresUpdate(Color color)
        {
            return !Colors.ContainsKey(color);
        }
    }
}