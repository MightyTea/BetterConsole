//Thanks to Colorful.Console
using System;

namespace BetterConsole
{
    public sealed class ConsoleAccessException : Exception
    {
        public ConsoleAccessException()
            : base("Color conversion failed because a handle to the actual windows console was not found.")
        {
        }
    }
}