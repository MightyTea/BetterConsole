//Thanks to Colorful.Console
using System.Runtime.InteropServices;
using System.Drawing;

namespace BetterConsole
{
    [StructLayout(LayoutKind.Sequential)]
    public struct COLORREF
    {
        private uint ColorDWORD;

        internal COLORREF(Color color)
        {
            ColorDWORD = (uint)color.R + (((uint)color.G) << 8) + (((uint)color.B) << 16);
        }

        internal COLORREF(uint r, uint g, uint b)
        {
            ColorDWORD = r + (g << 8) + (b << 16);
        }

        public override string ToString()
        {
            return ColorDWORD.ToString();
        }
    }
}