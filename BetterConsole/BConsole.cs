using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;

namespace BetterConsole
{
    public class BConsole
    {
        private static ColorManager colorManager;

        public static void Delete(int amount, int CursorX, int CursorY, int duration = 20)
        {
            for (int i = 0; i < amount; i++)
            {
                Console.SetCursorPosition(CursorX, CursorY);
                Console.Write("\b \b");
                Thread.Sleep(duration);
            }
        }

        public static void DeleteLine(int duration = 0)
        {
            while (CursorLeft != 0)
            {
                Console.Write("\b \b");
                if (duration > 0) Thread.Sleep(duration);
            }
        }

        public static void WriteGradient(string Text, Color startcolor, Color endcolor, bool newLine = true)
        {
            List<int> sRGB = new List<int>(), eRGB = new List<int>(), fRGB = new List<int>();

            sRGB.AddRange(new List<int>() { startcolor.R, startcolor.G, startcolor.B });
            eRGB.AddRange(new List<int>() { endcolor.R, endcolor.G, endcolor.B });

            Color finalcolor;

            fRGB.AddRange(new List<int>() { startcolor.R, startcolor.G, startcolor.B });

            Color invertcolor;
            try
            {
                for (int i = 0; i < Text.Length + 1; i++)
                {
                    bool stopr = false, stopg = false, stopb = false;

                    if (fRGB[0] < 10 || fRGB[0] > 245 && i > 2)
                        stopr = true;

                    if (fRGB[1] < 10 || fRGB[1] > 245 && i > 2)
                        stopg = true;

                    if (fRGB[2] < 10 || fRGB[2] > 245 && i > 2)
                        stopb = true;

                    if (stopr && stopg && stopb)
                    {
                        invertcolor = startcolor;
                        startcolor = endcolor;
                        endcolor = invertcolor;
                        fRGB.Clear();
                        fRGB.AddRange(new List<int>() { startcolor.R, startcolor.G, startcolor.B });
                        stopr = false;
                        stopg = false;
                        stopb = false;
                    }

                    if (startcolor.R > endcolor.R && !stopr)
                        fRGB[0] = fRGB[0] - 10;

                    if (startcolor.R < endcolor.R && !stopr)
                        fRGB[0] = fRGB[0] + 10;

                    if (startcolor.G > endcolor.G && !stopg)
                        fRGB[1] = fRGB[1] - 10;

                    if (startcolor.G < endcolor.G && !stopg)
                        fRGB[1] = fRGB[1] + 10;

                    if (startcolor.B > endcolor.B && !stopb)
                        fRGB[2] = fRGB[2] - 10;

                    if (startcolor.B < endcolor.B && !stopb)
                        fRGB[2] = fRGB[2] + 10;

                    finalcolor = Color.FromArgb(fRGB[0], fRGB[1], fRGB[2]);

                    Console.Write(Text[i].ToString().SetColor(Color.FromArgb(fRGB[0], fRGB[1], fRGB[2])));
                }

                if (newLine) Console.Write("\n");
            }
            catch { }
        }

        public static void TypeGradient(string Text, Color startcolor, Color endcolor, int duration, bool newLine = true)
        {
            List<int> sRGB = new List<int>(), eRGB = new List<int>(), fRGB = new List<int>();

            sRGB.AddRange(new List<int>() { startcolor.R, startcolor.G, startcolor.B });
            eRGB.AddRange(new List<int>() { endcolor.R, endcolor.G, endcolor.B });

            Color finalcolor;

            fRGB.AddRange(new List<int>() { startcolor.R, startcolor.G, startcolor.B });

            Color invertcolor;

            try
            {
                for (int i = 0; i < Text.Length + 1; i++)
                {
                    bool stopr = false, stopg = false, stopb = false;

                    if (fRGB[0] < 10 || fRGB[0] > 245 && i > 2)
                        stopr = true;

                    if (fRGB[1] < 10 || fRGB[1] > 245 && i > 2)
                        stopg = true;

                    if (fRGB[2] < 10 || fRGB[2] > 245 && i > 2)
                        stopb = true;

                    if (stopr && stopg && stopb)
                    {
                        invertcolor = startcolor;
                        startcolor = endcolor;
                        endcolor = invertcolor;
                        fRGB.Clear();
                        fRGB.AddRange(new List<int>() { startcolor.R, startcolor.G, startcolor.B });
                        stopr = false;
                        stopg = false;
                        stopb = false;
                    }

                    if (startcolor.R > endcolor.R && !stopr)
                        fRGB[0] = fRGB[0] - 10;

                    if (startcolor.R < endcolor.R && !stopr)
                        fRGB[0] = fRGB[0] + 10;

                    if (startcolor.G > endcolor.G && !stopg)
                        fRGB[1] = fRGB[1] - 10;

                    if (startcolor.G < endcolor.G && !stopg)
                        fRGB[1] = fRGB[1] + 10;

                    if (startcolor.B > endcolor.B && !stopb)
                        fRGB[2] = fRGB[2] - 10;

                    if (startcolor.B < endcolor.B && !stopb)
                        fRGB[2] = fRGB[2] + 10;

                    finalcolor = Color.FromArgb(fRGB[0], fRGB[1], fRGB[2]);

                    Console.Write(Text[i].ToString().SetColor(Color.FromArgb(fRGB[0], fRGB[1], fRGB[2])));

                    Thread.Sleep(duration);
                }

                if (newLine) Console.Write("\n");
            }
            catch { }
        }

        public static void Type(string value, int duration = 40, bool newLine = true)
        {
            for (int i = 0; i < value.Length; i++)
            {
                Console.Write(value[i]);
                Thread.Sleep(duration);
            }

            if (newLine) Console.Write("\n");
        }

        private static void WriteInColor<T>(Action<T> action, T target, Color color)
        {
            var oldSystemColor = Console.ForegroundColor;
            Console.ForegroundColor = colorManager.GetConsoleColor(color);
            action.Invoke(target);
            Console.ForegroundColor = oldSystemColor;
        }

        private static void WriteInColor<T, U>(Action<T, U> action, T target0, U target1, Color color)
        {
            var oldSystemColor = Console.ForegroundColor;
            Console.ForegroundColor = colorManager.GetConsoleColor(color);
            action.Invoke(target0, target1);
            Console.ForegroundColor = oldSystemColor;
        }

        private static void WriteInColor<T, U>(Action<T, U, U> action, T target0, U target1, U target2, Color color)
        {
            var oldSystemColor = Console.ForegroundColor;
            Console.ForegroundColor = colorManager.GetConsoleColor(color);
            action.Invoke(target0, target1, target2);
            Console.ForegroundColor = oldSystemColor;
        }

        private static void WriteInColor<T, U>(Action<T, U, U, U> action, T target0, U target1, U target2, U target3, Color color)
        {
            var oldSystemColor = Console.ForegroundColor;
            Console.ForegroundColor = colorManager.GetConsoleColor(color);
            action.Invoke(target0, target1, target2, target3);
            Console.ForegroundColor = oldSystemColor;
        }

        #region System.Console

        public static int BufferHeight { get => Console.BufferHeight; set => Console.BufferHeight = value; }

        public static int BufferWidth { get => Console.BufferWidth; set => Console.BufferWidth = value; }

        public static bool CapsLock => Console.CapsLock;

        public static int CursorLeft { get => Console.CursorLeft; set => Console.CursorLeft = value; }

        public static int CursorSize { get => Console.CursorSize; set => Console.CursorSize = value; }

        public static int CursorTop { get => Console.CursorTop; set => Console.CursorTop = value; }

        public static bool CursorVisible { get => Console.CursorVisible; set => Console.CursorVisible = value; }

        public static TextWriter Error => Console.Error;

        public static TextReader In => Console.In;

        public static Encoding InputEncoding { get => Console.InputEncoding; set => Console.InputEncoding = value; }

#if !NET40
        public static bool IsErrorRedirected => Console.IsErrorRedirected;

        public static bool IsInputRedirected => Console.IsInputRedirected;

        public static bool IsOutputRedirected => Console.IsOutputRedirected;
#endif

        public static bool KeyAvailable => Console.KeyAvailable;

        public static int LargestWindowHeight => Console.LargestWindowHeight;

        public static int LargestWindowWidth => Console.LargestWindowWidth;

        public static bool NumberLock => Console.NumberLock;

        public static TextWriter Out => Console.Out;

        public static Encoding OutputEncoding { get => Console.OutputEncoding; set => Console.OutputEncoding = value; }

        public static string Title { get => Console.Title; set => Console.Title = value; }

        public static bool TreatControlCAsInput { get => Console.TreatControlCAsInput; set => Console.TreatControlCAsInput = value; }

        public static int WindowHeight { get => Console.WindowHeight; set => Console.WindowHeight = value; }

        public static int WindowLeft { get => Console.WindowLeft; set => Console.WindowLeft = value; }

        public static int WindowTop { get => Console.WindowTop; set => Console.WindowTop = value; }

        public static int WindowWidth { get => Console.WindowWidth; set => Console.WindowWidth = value; }

        public static event ConsoleCancelEventHandler CancelKeyPress = delegate { };

        public static void Write(string value) => Console.Write(value);

        public static void Write(bool value) => Console.Write(value);

        public static void Write(char value) => Console.Write(value);

        public static void Write(char[] value) => Console.Write(value);

        public static void Write(decimal value) => Console.Write(value);

        public static void Write(double value) => Console.Write(value);

        public static void Write(float value) => Console.Write(value);

        public static void Write(int value) => Console.Write(value);

        public static void Write(long value) => Console.Write(value);

        public static void Write(object value) => Console.Write(value);

        public static void Write(uint value) => Console.Write(value);

        public static void Write(ulong value) => Console.Write(value);

        public static void Write(string format, object arg) => Console.Write(format, arg);

        public static void Write(string format, params object[] args) => Console.Write(format, args);

        public static void Write(char[] buffer, int index, int count) => Console.Write(buffer, index, count);

        public static void Write(string format, object arg0, object arg1) => Console.Write(format, arg0, arg1);

        public static void Write(string format, object arg0, object arg1, object arg2) => Console.Write(format, arg0, arg1, arg2);

        public static void Write(string format, object arg0, object arg1, object arg2, object arg3) => Console.Write(format, arg0, arg1, arg2, arg3);

        public static void WriteLine() => Console.WriteLine();

        public static void WriteLine(string value) => Console.WriteLine(value);

        public static void WriteLine(bool value) => Console.WriteLine(value);

        public static void WriteLine(char value) => Console.WriteLine(value);

        public static void WriteLine(char[] value) => Console.WriteLine(value);

        public static void WriteLine(decimal value) => Console.WriteLine(value);

        public static void WriteLine(double value) => Console.WriteLine(value);

        public static void WriteLine(float value) => Console.WriteLine(value);

        public static void WriteLine(int value) => Console.WriteLine(value);

        public static void WriteLine(long value) => Console.WriteLine(value);

        public static void WriteLine(object value) => Console.WriteLine(value);

        public static void WriteLine(uint value) => Console.WriteLine(value);

        public static void WriteLine(ulong value) => Console.WriteLine(value);

        public static void WriteLine(string format, object arg0) => Console.WriteLine(format, arg0);

        public static void WriteLine(string format, params object[] args) => Console.WriteLine(format, args);

        public static void WriteLine(char[] buffer, int index, int count) => Console.WriteLine(buffer, index, count);

        public static void WriteLine(string format, object arg0, object arg1) => Console.WriteLine(format, arg0, arg1);

        public static void WriteLine(string format, object arg0, object arg1, object arg2) => Console.WriteLine(format, arg0, arg1, arg2);

        public static void WriteLine(string format, object arg0, object arg1, object arg2, object arg3) => Console.WriteLine(format, arg0, arg1, arg2, arg3);

        public static int Read() => Console.Read();

        public static ConsoleKeyInfo ReadKey() => Console.ReadKey();

        public static ConsoleKeyInfo ReadKey(bool intercept) => Console.ReadKey(intercept);

        public static string ReadLine() => Console.ReadLine();

        public static void ResetColor() => Console.ResetColor();

        public static void SetBufferSize(int width, int height) => Console.SetBufferSize(width, height);

        public static void SetCursorPosition(int left, int top) => Console.SetCursorPosition(left, top);

        public static void SetError(TextWriter newError) => Console.SetError(newError);

        public static void SetIn(TextReader newIn) => Console.SetIn(newIn);

        public static void SetOut(TextWriter newOut) => Console.SetOut(newOut);

        public static void SetWindowPosition(int left, int top) => Console.SetWindowPosition(left, top);

        public static void SetWindowSize(int width, int height) => Console.SetWindowSize(width, height);

        public static Stream OpenStandardError() => Console.OpenStandardError();

#if !NETSTANDARD2_0
        public static Stream OpenStandardError(int bufferSiz) => Console.OpenStandardError(bufferSize);
#endif

        public static Stream OpenStandardInput() => Console.OpenStandardInput();

#if !NETSTANDARD2_0
        public static Stream OpenStandardInput(int bufferSize) => Console.OpenStandardInput(bufferSize);
#endif

        public static Stream OpenStandardOutput() => Console.OpenStandardOutput();

#if !NETSTANDARD2_0
        public static Stream OpenStandardOutput(int bufferSize) => Console.OpenStandardOutput(bufferSize);
#endif

        public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop) => Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop);

        public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor) => Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, sourceChar, sourceForeColor, sourceBackColor);

        public static void Clear() => Console.Clear();

        public static void Beep(int frequency, int duration) => Console.Beep(frequency, duration);

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e) => CancelKeyPress.Invoke(sender, e);

        public static void Write(bool value, Color color) => WriteInColor(Console.Write, value, color);

        public static void Write(char value, Color color) => WriteInColor(Console.Write, value, color);

        public static void Write(char[] value, Color color) => WriteInColor(Console.Write, value, color);

        public static void Write(decimal value, Color color) => WriteInColor(Console.Write, value, color);

        public static void Write(double value, Color color) => WriteInColor(Console.Write, value, color);

        public static void Write(float value, Color color) => WriteInColor(Console.Write, value, color);

        public static void Write(int value, Color color) => WriteInColor(Console.Write, value, color);

        public static void Write(long value, Color color) => WriteInColor(Console.Write, value, color);

        public static void Write(object value, Color color) => WriteInColor(Console.Write, value, color);

        public static void Write(string value, Color color) => WriteInColor(Console.Write, value, color);

        public static void Write(uint value, Color color) => WriteInColor(Console.Write, value, color);

        public static void Write(ulong value, Color color) => WriteInColor(Console.Write, value, color);

        public static void Write(string format, object arg0, Color color) => WriteInColor(Console.Write, format, arg0, color);

        public static void Write(string format, Color color, params object[] args) => WriteInColor(Console.Write, format, args, color);

        public static void Write(string format, object arg0, object arg1, Color color) => WriteInColor(Console.Write, format, arg0, arg1, color);

        public static void Write(string format, object arg0, object arg1, object arg2, Color color) => WriteInColor(Console.Write, format, arg0, arg1, arg2, color);

        public static void Write(string format, object arg0, object arg1, object arg2, object arg3, Color color) => WriteInColor(Console.Write, format, new[] { arg0, arg1, arg2, arg3 }, color);

        public static void WriteLine(bool value, Color color) => WriteInColor(Console.WriteLine, value, color);

        public static void WriteLine(char value, Color color) => WriteInColor(Console.WriteLine, value, color);

        public static void WriteLine(char[] value, Color color) => WriteInColor(Console.WriteLine, value, color);

        public static void WriteLine(decimal value, Color color) => WriteInColor(Console.WriteLine, value, color);

        public static void WriteLine(double value, Color color) => WriteInColor(Console.WriteLine, value, color);

        public static void WriteLine(float value, Color color) => WriteInColor(Console.WriteLine, value, color);

        public static void WriteLine(int value, Color color) => WriteInColor(Console.WriteLine, value, color);

        public static void WriteLine(long value, Color color) => WriteInColor(Console.WriteLine, value, color);

        public static void WriteLine(object value, Color color) => WriteInColor(Console.WriteLine, value, color);

        public static void WriteLine(string value, Color color) => WriteInColor(Console.WriteLine, value, color);

        public static void WriteLine(uint value, Color color) => WriteInColor(Console.WriteLine, value, color);

        public static void WriteLine(ulong value, Color color) => WriteInColor(Console.WriteLine, value, color);

        public static void WriteLine(string format, object arg0, Color color) => WriteInColor(Console.WriteLine, format, arg0, color);

        public static void WriteLine(string format, Color color, params object[] args) => WriteInColor(Console.WriteLine, format, args, color);

        public static void WriteLine(string format, object arg0, object arg1, Color color) => WriteInColor(Console.WriteLine, format, arg0, arg1, color);

        public static void WriteLine(string format, object arg0, object arg1, object arg2, Color color) => WriteInColor(Console.WriteLine, format, arg0, arg1, arg2, color);

        public static void WriteLine(string format, object arg0, object arg1, object arg2, object arg3, Color color) => WriteInColor(Console.WriteLine, format, new[] { arg0, arg1, arg2, arg3 }, color);
        #endregion
    }
}