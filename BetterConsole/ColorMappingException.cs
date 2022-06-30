//Thanks to Colorful.Console
using System;

namespace BetterConsole
{
    public sealed class ColorMappingException : Exception
    {
        public int ErrorCode { get; private set; }
        public ColorMappingException(int errorCode)
            : base(string.Format("Color conversion failed with system error code {0}!", errorCode))
        {
            ErrorCode = errorCode;
        }
    }
}