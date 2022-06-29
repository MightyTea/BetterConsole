using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace BetterConsole
{
    public class BConsole
    {
        private const string ESC = "\x1b";
        private const string BEL = "\x07";
        private const string SUB = "\x1a";
        private const string DEL = "\x7f";
        private static readonly string[] BytesMap =
            Enumerable.Range(0, 256).Select(s => s.ToString()).ToArray();

        public static void Delete(int amount, int duration)
        {
            for (int i = 0; i < amount; i++)
            {
                Console.Write("\b");
                Console.Write(" ");
                Console.Write("\b");
                Thread.Sleep(duration);
            }
        }
        public static void Delete(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Console.Write("\b");
                Console.Write(" ");
                Console.Write("\b");
                Thread.Sleep(20);
            }
        }
        public static void Write(string value)
        {
            Console.Write(value);
        }
        public static void WriteGradient(string Text, Color startcolor, Color endcolor)
        {
            List<int> sRGB = new List<int>();
            List<int> eRGB = new List<int>();
            List<int> fRGB = new List<int>();
            sRGB.Add(startcolor.R);
            sRGB.Add(startcolor.G);
            sRGB.Add(startcolor.B);
            eRGB.Add(endcolor.R);
            eRGB.Add(endcolor.G);
            eRGB.Add(endcolor.B);
            Color finalcolor;
            fRGB.Add(startcolor.R);
            fRGB.Add(startcolor.G);
            fRGB.Add(startcolor.B);
            Color invertcolor;
            try
            {
                for (int i = 0; i < Text.Length + 1; i++)
                {
                    bool stopr = false;
                    bool stopg = false;
                    bool stopb = false;
                    if (fRGB[0] < 10 || fRGB[0] > 245 && i > 2)
                    {
                        stopr = true;
                    }
                    if (fRGB[1] < 10 || fRGB[1] > 245 && i > 2)
                    {
                        stopg = true;
                    }
                    if (fRGB[2] < 10 || fRGB[2] > 245 && i > 2)
                    {
                        stopb = true;
                    }
                    if (stopr && stopg && stopb)
                    {
                        invertcolor = startcolor;
                        startcolor = endcolor;
                        endcolor = invertcolor;
                        fRGB.Clear();
                        fRGB.Add(startcolor.R);
                        fRGB.Add(startcolor.G);
                        fRGB.Add(startcolor.B);
                        stopr = false;
                        stopg = false;
                        stopb = false;
                    }
                    if (startcolor.R > endcolor.R && !stopr)
                    {
                        fRGB[0] = fRGB[0] - 10;
                    }
                    if (startcolor.R < endcolor.R && !stopr)
                    {
                        fRGB[0] = fRGB[0] + 10;
                    }
                    if (startcolor.G > endcolor.G && !stopg)
                    {
                        fRGB[1] = fRGB[1] - 10;
                    }
                    if (startcolor.G < endcolor.G && !stopg)
                    {
                        fRGB[1] = fRGB[1] + 10;
                    }
                    if (startcolor.B > endcolor.B && !stopb)
                    {
                        fRGB[2] = fRGB[2] - 10;
                    }
                    if (startcolor.B < endcolor.B && !stopb)
                    {
                        fRGB[2] = fRGB[2] + 10;
                    }
                    finalcolor = Color.FromArgb(fRGB[0], fRGB[1], fRGB[2]);
                    ColorWrite(Text[i].ToString(), finalcolor);
                }
            }
            catch { }
        }
        public static void WriteGradientLine(string Text, Color startcolor, Color endcolor)
        {
            List<int> sRGB = new List<int>();
            List<int> eRGB = new List<int>();
            List<int> fRGB = new List<int>();
            sRGB.Add(startcolor.R);
            sRGB.Add(startcolor.G);
            sRGB.Add(startcolor.B);
            eRGB.Add(endcolor.R);
            eRGB.Add(endcolor.G);
            eRGB.Add(endcolor.B);
            Color finalcolor;
            fRGB.Add(startcolor.R);
            fRGB.Add(startcolor.G);
            fRGB.Add(startcolor.B);
            Color invertcolor;
            try
            {
                for (int i = 0; i < Text.Length + 1; i++)
                {
                    bool stopr = false;
                    bool stopg = false;
                    bool stopb = false;
                    if (fRGB[0] < 10 || fRGB[0] > 245 && i > 2)
                    {
                        stopr = true;
                    }
                    if (fRGB[1] < 10 || fRGB[1] > 245 && i > 2)
                    {
                        stopg = true;
                    }
                    if (fRGB[2] < 10 || fRGB[2] > 245 && i > 2)
                    {
                        stopb = true;
                    }
                    if (stopr && stopg && stopb)
                    {
                        invertcolor = startcolor;
                        startcolor = endcolor;
                        endcolor = invertcolor;
                        fRGB.Clear();
                        fRGB.Add(startcolor.R);
                        fRGB.Add(startcolor.G);
                        fRGB.Add(startcolor.B);
                        stopr = false;
                        stopg = false;
                        stopb = false;
                    }
                    if (startcolor.R > endcolor.R && !stopr)
                    {
                        fRGB[0] = fRGB[0] - 10;
                    }
                    if (startcolor.R < endcolor.R && !stopr)
                    {
                        fRGB[0] = fRGB[0] + 10;
                    }
                    if (startcolor.G > endcolor.G && !stopg)
                    {
                        fRGB[1] = fRGB[1] - 10;
                    }
                    if (startcolor.G < endcolor.G && !stopg)
                    {
                        fRGB[1] = fRGB[1] + 10;
                    }
                    if (startcolor.B > endcolor.B && !stopb)
                    {
                        fRGB[2] = fRGB[2] - 10;
                    }
                    if (startcolor.B < endcolor.B && !stopb)
                    {
                        fRGB[2] = fRGB[2] + 10;
                    }
                    finalcolor = Color.FromArgb(fRGB[0], fRGB[1], fRGB[2]);
                    ColorWrite(Text[i].ToString(), finalcolor);
                }
                Console.Write("\n");
            }
            catch { }
        }
        public static void TypeGradient(string Text, Color startcolor, Color endcolor, int duration)
        {
            List<int> sRGB = new List<int>();
            List<int> eRGB = new List<int>();
            List<int> fRGB = new List<int>();
            sRGB.Add(startcolor.R);
            sRGB.Add(startcolor.G);
            sRGB.Add(startcolor.B);
            eRGB.Add(endcolor.R);
            eRGB.Add(endcolor.G);
            eRGB.Add(endcolor.B);
            Color finalcolor;
            fRGB.Add(startcolor.R);
            fRGB.Add(startcolor.G);
            fRGB.Add(startcolor.B);
            Color invertcolor;
            try
            {
                for (int i = 0; i < Text.Length + 1; i++)
                {
                    bool stopr = false;
                    bool stopg = false;
                    bool stopb = false;
                    if (fRGB[0] < 10 || fRGB[0] > 245 && i > 2)
                    {
                        stopr = true;
                    }
                    if (fRGB[1] < 10 || fRGB[1] > 245 && i > 2)
                    {
                        stopg = true;
                    }
                    if (fRGB[2] < 10 || fRGB[2] > 245 && i > 2)
                    {
                        stopb = true;
                    }
                    if (stopr && stopg && stopb)
                    {
                        invertcolor = startcolor;
                        startcolor = endcolor;
                        endcolor = invertcolor;
                        fRGB.Clear();
                        fRGB.Add(startcolor.R);
                        fRGB.Add(startcolor.G);
                        fRGB.Add(startcolor.B);
                        stopr = false;
                        stopg = false;
                        stopb = false;
                    }
                    if (startcolor.R > endcolor.R && !stopr)
                    {
                        fRGB[0] = fRGB[0] - 10;
                    }
                    if (startcolor.R < endcolor.R && !stopr)
                    {
                        fRGB[0] = fRGB[0] + 10;
                    }
                    if (startcolor.G > endcolor.G && !stopg)
                    {
                        fRGB[1] = fRGB[1] - 10;
                    }
                    if (startcolor.G < endcolor.G && !stopg)
                    {
                        fRGB[1] = fRGB[1] + 10;
                    }
                    if (startcolor.B > endcolor.B && !stopb)
                    {
                        fRGB[2] = fRGB[2] - 10;
                    }
                    if (startcolor.B < endcolor.B && !stopb)
                    {
                        fRGB[2] = fRGB[2] + 10;
                    }
                    finalcolor = Color.FromArgb(fRGB[0], fRGB[1], fRGB[2]);
                    ColorWrite(Text[i].ToString(), finalcolor);
                    Thread.Sleep(duration);
                }
            }
            catch { }
        }
        public static void TypeGradientLine(string Text, Color startcolor, Color endcolor, int duration)
        {
            List<int> sRGB = new List<int>();
            List<int> eRGB = new List<int>();
            List<int> fRGB = new List<int>();
            sRGB.Add(startcolor.R);
            sRGB.Add(startcolor.G);
            sRGB.Add(startcolor.B);
            eRGB.Add(endcolor.R);
            eRGB.Add(endcolor.G);
            eRGB.Add(endcolor.B);
            Color finalcolor;
            fRGB.Add(startcolor.R);
            fRGB.Add(startcolor.G);
            fRGB.Add(startcolor.B);
            Color invertcolor;
            try
            {
                for (int i = 0; i < Text.Length + 1; i++)
                {
                    bool stopr = false;
                    bool stopg = false;
                    bool stopb = false;
                    if (fRGB[0] < 10 || fRGB[0] > 245 && i > 2)
                    {
                        stopr = true;
                    }
                    if (fRGB[1] < 10 || fRGB[1] > 245 && i > 2)
                    {
                        stopg = true;
                    }
                    if (fRGB[2] < 10 || fRGB[2] > 245 && i > 2)
                    {
                        stopb = true;
                    }
                    if (stopr && stopg && stopb)
                    {
                        invertcolor = startcolor;
                        startcolor = endcolor;
                        endcolor = invertcolor;
                        fRGB.Clear();
                        fRGB.Add(startcolor.R);
                        fRGB.Add(startcolor.G);
                        fRGB.Add(startcolor.B);
                        stopr = false;
                        stopg = false;
                        stopb = false;
                    }
                    if (startcolor.R > endcolor.R && !stopr)
                    {
                        fRGB[0] = fRGB[0] - 10;
                    }
                    if (startcolor.R < endcolor.R && !stopr)
                    {
                        fRGB[0] = fRGB[0] + 10;
                    }
                    if (startcolor.G > endcolor.G && !stopg)
                    {
                        fRGB[1] = fRGB[1] - 10;
                    }
                    if (startcolor.G < endcolor.G && !stopg)
                    {
                        fRGB[1] = fRGB[1] + 10;
                    }
                    if (startcolor.B > endcolor.B && !stopb)
                    {
                        fRGB[2] = fRGB[2] - 10;
                    }
                    if (startcolor.B < endcolor.B && !stopb)
                    {
                        fRGB[2] = fRGB[2] + 10;
                    }
                    finalcolor = Color.FromArgb(fRGB[0], fRGB[1], fRGB[2]);
                    ColorWrite(Text[i].ToString(), finalcolor);
                    Thread.Sleep(duration);
                }
                Console.Write("\n");
            }
            catch { }
        }
        private static string GetColorForegroundString(int r, int g, int b)
        {
            return string.Concat(ESC, "[38;2;", BytesMap[r], ";", BytesMap[g], ";", BytesMap[b], "m");
        }
        public static void SetColorForeground(Color color)
        {
            Console.Write(GetColorForegroundString(color.R, color.G, color.B));
        }
        public static void ColorWrite(string value, Color foreground)
        {
            SetColorForeground(foreground);
            Console.Write(value);
        }
        public static void WriteLine(string value)
        {
            Console.WriteLine(value);
        }
        public static void ColorWriteLine(string value, Color foreground)
        {
            SetColorForeground(foreground);
            WriteLine(value);
        }
        public static void Type(string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                Console.Write(value[i]);
                Thread.Sleep(40);
            }
        }
        public static void Type(string value, int duration)
        {
            for (int i = 0; i < value.Length; i++)
            {
                Console.Write(value[i]);
                Thread.Sleep(duration);
            }
        }
        public static void TypeLine(string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                Console.Write(value[i]);
                Thread.Sleep(40);
            }
            Console.Write("\n");
        }
        public static void TypeLine(string value, int duration)
        {
            for (int i = 0; i < value.Length; i++)
            {
                Console.Write(value[i]);
                Thread.Sleep(duration);
            }
            Console.Write("\n");
        }
        public static int BufferHeight
        {
            get => System.Console.BufferHeight;
            set => System.Console.BufferHeight = value;
        }

        public static int BufferWidth
        {
            get => System.Console.BufferWidth;
            set => System.Console.BufferWidth = value;
        }

        public static bool CapsLock => System.Console.CapsLock;

        public static int CursorLeft
        {
            get => System.Console.CursorLeft;
            set => System.Console.CursorLeft = value;
        }

        public static int CursorSize
        {
            get => System.Console.CursorSize;
            set => System.Console.CursorSize = value;
        }

        public static int CursorTop
        {
            get => System.Console.CursorTop;
            set => System.Console.CursorTop = value;
        }

        public static bool CursorVisible
        {
            get => System.Console.CursorVisible;
            set => System.Console.CursorVisible = value;
        }

        public static TextWriter Error => System.Console.Error;

        public static TextReader In => System.Console.In;

        public static Encoding InputEncoding
        {
            get => System.Console.InputEncoding;
            set => System.Console.InputEncoding = value;
        }

#if !NET40
        public static bool IsErrorRedirected => System.Console.IsErrorRedirected;

        public static bool IsInputRedirected => System.Console.IsInputRedirected;

        public static bool IsOutputRedirected => System.Console.IsOutputRedirected;
#endif

        public static bool KeyAvailable => System.Console.KeyAvailable;

        public static int LargestWindowHeight => System.Console.LargestWindowHeight;

        public static int LargestWindowWidth => System.Console.LargestWindowWidth;

        public static bool NumberLock => System.Console.NumberLock;

        public static TextWriter Out => System.Console.Out;

        public static Encoding OutputEncoding
        {
            get => System.Console.OutputEncoding;
            set => System.Console.OutputEncoding = value;
        }

        public static string Title
        {
            get => System.Console.Title;
            set => System.Console.Title = value;
        }

        public static bool TreatControlCAsInput
        {
            get => System.Console.TreatControlCAsInput;
            set => System.Console.TreatControlCAsInput = value;
        }

        public static int WindowHeight
        {
            get => System.Console.WindowHeight;
            set => System.Console.WindowHeight = value;
        }

        public static int WindowLeft
        {
            get => System.Console.WindowLeft;
            set => System.Console.WindowLeft = value;
        }

        public static int WindowTop
        {
            get => System.Console.WindowTop;
            set => System.Console.WindowTop = value;
        }

        public static int WindowWidth
        {
            get => System.Console.WindowWidth;
            set => System.Console.WindowWidth = value;
        }

        public static event ConsoleCancelEventHandler CancelKeyPress = delegate { };


        public static void Write(bool value)
        {
            System.Console.Write(value);
        }

        public static void Write(char value)
        {
            System.Console.Write(value);
        }
        public static void Write(char[] value)
        {
            System.Console.Write(value);
        }

        public static void Write(decimal value)
        {
            System.Console.Write(value);
        }
        public static void Write(double value)
        {
            System.Console.Write(value);
        }
        public static void Write(float value)
        {
            System.Console.Write(value);
        }

        public static void Write(int value)
        {
            System.Console.Write(value);
        }
        public static void Write(long value)
        {
            System.Console.Write(value);
        }
        public static void Write(object value)
        {
            System.Console.Write(value);
        }
        public static void Write(uint value)
        {
            System.Console.Write(value);
        }
        public static void Write(ulong value)
        {
            System.Console.Write(value);
        }
        public static void Write(string format, object arg0)
        {
            System.Console.Write(format, arg0);
        }
        public static void Write(string format, params object[] args)
        {
            System.Console.Write(format, args);
        }
        public static void Write(char[] buffer, int index, int count)
        {
            System.Console.Write(buffer, index, count);
        }

        public static void Write(string format, object arg0, object arg1)
        {
            System.Console.Write(format, arg0, arg1);
        }
        public static void Write(string format, object arg0, object arg1, object arg2)
        {
            System.Console.Write(format, arg0, arg1, arg2);
        }
        public static void Write(string format, object arg0, object arg1, object arg2, object arg3)
        {
            System.Console.Write(format, arg0, arg1, arg2, arg3);
        }
        public static void WriteLine()
        {
            System.Console.WriteLine();
        }
        public static void WriteLine(bool value)
        {
            System.Console.WriteLine(value);
        }
        public static void WriteLine(char value)
        {
            System.Console.WriteLine(value);
        }
        public static void WriteLine(char[] value)
        {
            System.Console.WriteLine(value);
        }
        public static void WriteLine(decimal value)
        {
            System.Console.WriteLine(value);
        }
        public static void WriteLine(double value)
        {
            System.Console.WriteLine(value);
        }
        public static void WriteLine(float value)
        {
            System.Console.WriteLine(value);
        }
        public static void WriteLine(int value)
        {
            System.Console.WriteLine(value);
        }
        public static void WriteLine(long value)
        {
            System.Console.WriteLine(value);
        }
        public static void WriteLine(object value)
        {
            System.Console.WriteLine(value);
        }
        public static void WriteLine(uint value)
        {
            System.Console.WriteLine(value);
        }
        public static void WriteLine(ulong value)
        {
            System.Console.WriteLine(value);
        }
        public static void WriteLine(string format, object arg0)
        {
            System.Console.WriteLine(format, arg0);
        }
        public static void WriteLine(string format, params object[] args)
        {
            System.Console.WriteLine(format, args);
        }
        public static void WriteLine(char[] buffer, int index, int count)
        {
            System.Console.WriteLine(buffer, index, count);
        }
        public static void WriteLine(string format, object arg0, object arg1)
        {
            System.Console.WriteLine(format, arg0, arg1);
        }
        public static void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            System.Console.WriteLine(format, arg0, arg1, arg2);
        }
        public static void WriteLine(string format, object arg0, object arg1, object arg2, object arg3)
        {
            System.Console.WriteLine(format, arg0, arg1, arg2, arg3);
        }
        public static int Read()
        {
            return System.Console.Read();
        }

        public static ConsoleKeyInfo ReadKey()
        {
            return System.Console.ReadKey();
        }

        public static ConsoleKeyInfo ReadKey(bool intercept)
        {
            return System.Console.ReadKey(intercept);
        }

        public static string ReadLine()
        {
            return System.Console.ReadLine();
        }

        public static void ResetColor()
        {
            System.Console.ResetColor();
        }

        public static void SetBufferSize(int width, int height)
        {
            System.Console.SetBufferSize(width, height);
        }

        public static void SetCursorPosition(int left, int top)
        {
            System.Console.SetCursorPosition(left, top);
        }

        public static void SetError(TextWriter newError)
        {
            System.Console.SetError(newError);
        }

        public static void SetIn(TextReader newIn)
        {
            System.Console.SetIn(newIn);
        }

        public static void SetOut(TextWriter newOut)
        {
            System.Console.SetOut(newOut);
        }

        public static void SetWindowPosition(int left, int top)
        {
            System.Console.SetWindowPosition(left, top);
        }

        public static void SetWindowSize(int width, int height)
        {
            System.Console.SetWindowSize(width, height);
        }

        public static Stream OpenStandardError()
        {
            return System.Console.OpenStandardError();
        }

#if !NETSTANDARD2_0
        public static Stream OpenStandardError(int bufferSize)
        {
            return System.Console.OpenStandardError(bufferSize);
        }
#endif

        public static Stream OpenStandardInput()
        {
            return System.Console.OpenStandardInput();
        }

#if !NETSTANDARD2_0
        public static Stream OpenStandardInput(int bufferSize)
        {
            return System.Console.OpenStandardInput(bufferSize);
        }
#endif

        public static Stream OpenStandardOutput()
        {
            return System.Console.OpenStandardOutput();
        }

#if !NETSTANDARD2_0
        public static Stream OpenStandardOutput(int bufferSize)
        {
            return System.Console.OpenStandardOutput(bufferSize);
        }
#endif

        public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight,
            int targetLeft, int targetTop)
        {
            System.Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop);
        }

        public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight,
            int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
        {
            System.Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop,
                sourceChar, sourceForeColor, sourceBackColor);
        }

        public static void Clear()
        {
            System.Console.Clear();
        }


        public static void Beep(int frequency, int duration)
        {
            System.Console.Beep(frequency, duration);
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            CancelKeyPress.Invoke(sender, e);
        }
    }
}