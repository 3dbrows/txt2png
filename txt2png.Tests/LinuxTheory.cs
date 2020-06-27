using System.Runtime.InteropServices;
using Xunit;

namespace txt2png.Tests
{
    public sealed class LinuxTheory : TheoryAttribute
    {
        public LinuxTheory()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Skip = "These tests only run on Linux.";
            }
        }
    }
}