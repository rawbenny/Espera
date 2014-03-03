using System.Linq;
using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;

namespace Espera.View
{
    public static class RegistryHelper
    {
        public static bool IsApplicationInstalled(string applicationName)
        {
            string displayName;

            // Search in: CurrentUser
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");

            foreach (RegistryKey subkey in key.GetSubKeyNames().Select(keyName => key.OpenSubKey(keyName)))
            {
                displayName = subkey.GetValue("DisplayName") as string;

                if (displayName != null && displayName.Contains(applicationName))
                {
                    return true;
                }
            }

            // Search in: LocalMachine_32
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");

            foreach (RegistryKey subkey in key.GetSubKeyNames().Select(keyName => key.OpenSubKey(keyName)))
            {
                displayName = subkey.GetValue("DisplayName") as string;

                if (displayName != null && displayName.Contains(applicationName))
                {
                    return true;
                }
            }

            // Search in: LocalMachine_64
            // This key onlys exist on 64 platform. Check this link for more details.
            // http://social.msdn.microsoft.com/Forums/vstudio/en-US/24792cdc-2d8e-454b-9c68-31a19892ca53/how-to-check-whether-the-system-is-32-bit-or-64-bit-?forum=csharpgeneral
            if (IntPtr.Size == 8)
            {
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");

                foreach (RegistryKey subkey in key.GetSubKeyNames().Select(keyName => key.OpenSubKey(keyName)))
                {
                    displayName = subkey.GetValue("DisplayName") as string;

                    if (displayName != null && displayName.Contains(applicationName))
                    {
                        return true;
                    }
                }
            }

            // Not found
            return false;
        }
    }
    

}