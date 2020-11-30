using System;
using System.Collections.Generic;
using System.Text;

namespace StreamDeckManager.Settings
{
    public class OBSConnectionSettings
    {
        public string? Url { get; set; }
        public string? Password { get; set; }
        public bool AutoConnect { get; set; }
    }
}
