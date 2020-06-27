using System.Runtime.InteropServices;
using Xunit;

namespace txt2png.Tests
{
    public sealed class WindowsTheory : TheoryAttribute
    {
        public WindowsTheory()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Skip = "These tests only run on Windows.";
            }
        }
    }
}