//Thanks to Colorful.Console
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace BetterConsole
{
    public sealed class ColorMapper
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct COORD
        {
            internal short X, Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SMALL_RECT
        {
            internal short Left, Top, Right, Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CONSOLE_SCREEN_BUFFER_INFO_EX
        {
            internal int cbSize;
            internal COORD dwSize, dwCursorPosition;
            internal ushort wAttributes;
            internal SMALL_RECT srWindow;
            internal COORD dwMaximumWindowSize;
            internal ushort wPopupAttributes;
            internal bool bFullscreenSupported;
            internal COLORREF black, darkBlue, darkGreen, darkCyan, darkRed, darkMagenta, darkYellow, gray, darkGray, blue, green, cyan, red, magenta, yellow, white;
        }

        private const int STD_OUTPUT_HANDLE = -11;
        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetConsoleScreenBufferInfoEx(IntPtr hConsoleOutput, ref CONSOLE_SCREEN_BUFFER_INFO_EX csbe);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleScreenBufferInfoEx(IntPtr hConsoleOutput, ref CONSOLE_SCREEN_BUFFER_INFO_EX csbe);

        public void MapColor(ConsoleColor oldColor, Color newColor)
        {
            // NOTE: The default console colors used are gray (foreground) and black (background).
            MapColor(oldColor, newColor.R, newColor.G, newColor.B);
        }

        public Dictionary<string, COLORREF> GetBufferColors()
        {
            Dictionary<string, COLORREF> colors = new Dictionary<string, COLORREF>();
            IntPtr hConsoleOutput = GetStdHandle(STD_OUTPUT_HANDLE);    // 7
            CONSOLE_SCREEN_BUFFER_INFO_EX csbe = GetBufferInfo(hConsoleOutput);

            colors.Add("black", csbe.black);
            colors.Add("darkBlue", csbe.darkBlue);
            colors.Add("darkGreen", csbe.darkGreen);
            colors.Add("darkCyan", csbe.darkCyan);
            colors.Add("darkRed", csbe.darkRed);
            colors.Add("darkMagenta", csbe.darkMagenta);
            colors.Add("darkYellow", csbe.darkYellow);
            colors.Add("gray", csbe.gray);
            colors.Add("darkGray", csbe.darkGray);
            colors.Add("blue", csbe.blue);
            colors.Add("green", csbe.green);
            colors.Add("cyan", csbe.cyan);
            colors.Add("red", csbe.red);
            colors.Add("magenta", csbe.magenta);
            colors.Add("yellow", csbe.yellow);
            colors.Add("white", csbe.white);

            return colors;
        }

        public void SetBatchBufferColors(Dictionary<string, COLORREF> colors)
        {
            IntPtr hConsoleOutput = GetStdHandle(STD_OUTPUT_HANDLE); // 7
            CONSOLE_SCREEN_BUFFER_INFO_EX csbe = GetBufferInfo(hConsoleOutput);

            csbe.black = colors["black"];
            csbe.darkBlue = colors["darkBlue"];
            csbe.darkGreen = colors["darkGreen"];
            csbe.darkCyan = colors["darkCyan"];
            csbe.darkRed = colors["darkRed"];
            csbe.darkMagenta = colors["darkMagenta"];
            csbe.darkYellow = colors["darkYellow"];
            csbe.gray = colors["gray"];
            csbe.darkGray = colors["darkGray"];
            csbe.blue = colors["blue"];
            csbe.green = colors["green"];
            csbe.cyan = colors["cyan"];
            csbe.red = colors["red"];
            csbe.magenta = colors["magenta"];
            csbe.yellow = colors["yellow"];
            csbe.white = colors["white"];

            SetBufferInfo(hConsoleOutput, csbe);
        }

        private CONSOLE_SCREEN_BUFFER_INFO_EX GetBufferInfo(IntPtr hConsoleOutput)
        {
            CONSOLE_SCREEN_BUFFER_INFO_EX csbe = new CONSOLE_SCREEN_BUFFER_INFO_EX();
            csbe.cbSize = (int)Marshal.SizeOf(csbe); // 96 = 0x60

            if (hConsoleOutput == INVALID_HANDLE_VALUE)
                throw CreateException(Marshal.GetLastWin32Error());

            bool brc = GetConsoleScreenBufferInfoEx(hConsoleOutput, ref csbe);

            if (!brc)
                throw CreateException(Marshal.GetLastWin32Error());

            return csbe;
        }

        private void MapColor(ConsoleColor color, uint r, uint g, uint b)
        {
            IntPtr hConsoleOutput = GetStdHandle(STD_OUTPUT_HANDLE); // 7
            CONSOLE_SCREEN_BUFFER_INFO_EX csbe = GetBufferInfo(hConsoleOutput);

            switch (color)
            {
                case ConsoleColor.Black:
                    csbe.black = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.DarkBlue:
                    csbe.darkBlue = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.DarkGreen:
                    csbe.darkGreen = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.DarkCyan:
                    csbe.darkCyan = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.DarkRed:
                    csbe.darkRed = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.DarkMagenta:
                    csbe.darkMagenta = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.DarkYellow:
                    csbe.darkYellow = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.Gray:
                    csbe.gray = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.DarkGray:
                    csbe.darkGray = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.Blue:
                    csbe.blue = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.Green:
                    csbe.green = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.Cyan:
                    csbe.cyan = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.Red:
                    csbe.red = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.Magenta:
                    csbe.magenta = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.Yellow:
                    csbe.yellow = new COLORREF(r, g, b);
                    break;
                case ConsoleColor.White:
                    csbe.white = new COLORREF(r, g, b);
                    break;
            }

            SetBufferInfo(hConsoleOutput, csbe);
        }

        private void SetBufferInfo(IntPtr hConsoleOutput, CONSOLE_SCREEN_BUFFER_INFO_EX csbe)
        {
            csbe.srWindow.Bottom++;
            csbe.srWindow.Right++;

            bool brc = SetConsoleScreenBufferInfoEx(hConsoleOutput, ref csbe);
            if (!brc)
                throw CreateException(Marshal.GetLastWin32Error());
        }

        private Exception CreateException(int errorCode)
        {
            const int ERROR_INVALID_HANDLE = 6;
            if (errorCode == ERROR_INVALID_HANDLE)
                return new ConsoleAccessException();

            return new ColorMappingException(errorCode);
        }
    }
}