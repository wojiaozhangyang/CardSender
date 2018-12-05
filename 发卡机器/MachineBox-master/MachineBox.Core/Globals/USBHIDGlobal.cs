using MachineBox.Core.Hooks;

namespace MachineBox.Core.Globals
{
    public class USBHIDGlobal
    {
        private static object _lock                  = new object();
        private static string _text                  = string.Empty;
        private static bool   _wait                  = true;
        private static bool   _bypassCompabilityMode = true;

        public static readonly GlobalKeyboardHook KeyboardHook = new GlobalKeyboardHook();
        public static readonly int                END_CHAR     = 13;

        public static string Text
        {
            get { lock (_lock) return _text;  }
            set { lock (_lock) _text = value; }
        }

        public static bool   Wait
        {
            get { lock (_lock) return _wait;  }
            set { lock (_lock) _wait = value; }
        }

        public static bool BypassCompabilityMode
        {
            get { lock (_lock) return _bypassCompabilityMode;  }
            set { lock (_lock) _bypassCompabilityMode = value; }
        }
    }
}