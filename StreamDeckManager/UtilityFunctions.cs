using System;
using System.Collections.Generic;
using System.Text;

namespace StreamDeckManager
{
	static class UtilityFunctions
	{

		public static System.Drawing.Color ColorFromHex(string hex)
			=> System.Drawing.Color.FromArgb(int.Parse("FF" + hex, System.Globalization.NumberStyles.AllowHexSpecifier));

		public static System.Drawing.Color TextColorFromBackground(System.Drawing.Color backgroundColor)
			=> backgroundColor.GetBrightness() > .4 ? System.Drawing.Color.Black : System.Drawing.Color.White;
	}
}
