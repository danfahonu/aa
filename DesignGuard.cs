using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    public static class DesignGuard
    {
        public static bool InDesigner =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime || IsVsProcess();

        private static bool IsVsProcess()
        {
            try { return Process.GetCurrentProcess().ProcessName.IndexOf("devenv", StringComparison.OrdinalIgnoreCase) >= 0; }
            catch { return false; }
        }
    }
}
