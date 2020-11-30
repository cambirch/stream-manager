using System;
using System.Collections.Generic;
using System.Text;

namespace StreamDeckManager.Settings
{
	public class StreamDeckSettings
	{
		/// <summary>
		/// The number of buttons wide for the stream deck
		/// </summary>
		public int Width { get; set; }
		/// <summary>
		/// The number of buttons high for the stream deck
		/// </summary>
		public int Height { get; set; }

		public List<KeySettings> Keys { get; set; } = new List<KeySettings>();

		/// <summary>
		/// Automatically connect on start.
		/// </summary>
		public bool AutoConnect { get; set; }
	}

	public class KeySettings
	{
		public int KeyNumber { get; set; }
		public string Text { get; set; }
		public string Color { get; set; }

		public List<ActionSettings> Actions { get; set; }
	}

	public class ActionSettings
	{
		public string Name { get; set; }
		public Dictionary<string, string> Props { get; set; }
	}
}
