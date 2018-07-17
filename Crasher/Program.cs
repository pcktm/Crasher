using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Crasher
{
    static class Crasher
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern IntPtr RtlAdjustPrivilege(int Privilege, bool bEnablePrivilege,
                bool IsThreadPrivilege, out bool PreviousValue);

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern void NtRaiseHardError(uint errorStatus,
                int a, int b, int c, /* Unused */
                int responseOption,
                out int response);

        static void Main()
        {
            bool x; int y;
            RtlAdjustPrivilege(19 /* SeShutdownPrivilege */, true, false, out x);
            NtRaiseHardError(0xc0000022, 0, 0, 0, 6 /* OptionShutdownSystem */, out y);
        }
    }
}