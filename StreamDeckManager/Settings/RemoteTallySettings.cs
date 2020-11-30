using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamDeckManager.Settings
{
	public class RemoteTallySettings
	{
		/// <summary>
		/// A list of the configured remote tally display units for the system.
		/// </summary>
		public List<RemoteDisplay> Displays { get; set; } = new List<RemoteDisplay>();

		/// <summary>
		/// The individual tally tracking lights. An individual light might be represented
		/// at zero or several difference <see cref="Displays"/>.  A light must be defined
		/// before something can be tracked as on/off.
		/// </summary>
		public List<TallyLight> Tallies { get; set; } = new List<TallyLight>();

		public List<Source> SourceTallies { get; set; } = new List<Source>();
	}

	public class RemoteDisplay
	{
		/// <summary>
		/// The name of the tally
		/// </summary>
		public string Name { get; set; } = string.Empty;
		/// <summary>
		/// A friendly description for the tally.
		/// </summary>
		public string Description { get; set; } = string.Empty;
		/// <summary>
		/// The remote IP address to connect to the tally.
		/// </summary>
		public string IPAddress { get; set; } = string.Empty;
	}

	public class TallyLight
	{
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string ID { get; set; } = string.Empty;
		public bool HasProgram { get; set; }
		public bool HasPreview { get; set; }
	}

	public class Source
	{
		public string Name { get; set; } = string.Empty;
		public string[] TallyIDs { get; set; } = Array.Empty<string>();
	}
}
