using Microsoft.Extensions.DependencyInjection;
using StreamDeckManager.Actions.Interfaces;
using StreamDeckManager.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StreamDeckManager.Actions
{
	public class ActionFactory
	{
		public const string TimeSpanFormatString = @"mm\:ss";

		private readonly IServiceProvider _serviceProvider;

		public ActionFactory(IServiceProvider serviceProvider)
			=> (_serviceProvider) = (serviceProvider);

		public IAction CreateAction(string actionName, Dictionary<string, string> props)
		{
			switch (actionName)
			{
				case nameof(PreviewSceneAction):
					return new PreviewSceneAction(_serviceProvider.GetRequiredService<ObsManager>(), props["sceneName"]);
				case nameof(ToggleItemVisibilityAction):
					return new ToggleItemVisibilityAction(_serviceProvider.GetRequiredService<ObsManager>(),
						props["sceneName"],
						props["itemName"]
						);
				case nameof(CountdownOnSceneActiveAction):
					return new CountdownOnSceneActiveAction(_serviceProvider.GetRequiredService<ObsManager>(),
						props["sceneName"],
						ReadTimeSpan(props["timespan"])
						);
				case nameof(SceneTransitionAction):
					return new SceneTransitionAction(_serviceProvider.GetRequiredService<ObsManager>(),
						props["transition"],
						Convert.ToInt32(props["duration"] ?? "0")
						);
				case nameof(ToggleSourceMuteAction):
					return new ToggleSourceMuteAction(_serviceProvider.GetRequiredService<ObsManager>(),
						props["sourceName"]
						);
				case nameof(SetSourceMuteAction):
					return new SetSourceMuteAction(_serviceProvider.GetRequiredService<ObsManager>(),
						props["sourceName"],
						Convert.ToBoolean(props["setMuted"])
						);
				//case nameof(TallyOnLive):
					//return new TallyOnLive(
					//	_serviceProvider.GetRequiredService<ObsManager>(),
					//	_serviceProvider.GetRequiredService<GetTallyInfo>(),
					//	_serviceProvider.GetRequiredService<TallyManager>(),
					//	props["camString"], props["tallyName"]
					//	);
				default:
					throw new NotImplementedException();
			}
		}

		private static readonly Dictionary<string, ActionConfigurationDetails> ActionDetails = new Dictionary<string, ActionConfigurationDetails>
		{
			{
				nameof(PreviewSceneAction),
				new ActionConfigurationDetails(
					nameof(PreviewSceneAction),
					typeof(PreviewSceneAction),
					new[] {
						("sceneName", "Scene Name:", "")
					}
				)
			},
			{
				nameof(ToggleItemVisibilityAction),
				new ActionConfigurationDetails(
					nameof(ToggleItemVisibilityAction),
					typeof(ToggleItemVisibilityAction),
					new[] {
						("sceneName", "Scene Name:", ""),
						("itemName", "Item Name:", ""),
					}
				)
			},
			{
				nameof(CountdownOnSceneActiveAction),
				new ActionConfigurationDetails(
					nameof(CountdownOnSceneActiveAction),
					typeof(CountdownOnSceneActiveAction),
					new[] {
						("sceneName", "Scene Name:", ""),
						("timespan", "Timepsan:", ""),
					}
				)
			},
			{
				nameof(SceneTransitionAction),
				new ActionConfigurationDetails(
					nameof(SceneTransitionAction),
					typeof(SceneTransitionAction),
					new[] {
						("transition", "Transition To Use:", ""),
						("duration", "Duration (ms):", (object)100),
					}
				)
			},
			{
				nameof(ToggleSourceMuteAction),
				new ActionConfigurationDetails(
					nameof(ToggleSourceMuteAction),
					typeof(ToggleSourceMuteAction),
					new[] {
						("sourceName", "Audio Source:", ""),
					}
				)
			},
			{
				nameof(SetSourceMuteAction),
				new ActionConfigurationDetails(
					nameof(SetSourceMuteAction),
					typeof(SetSourceMuteAction),
					new[] {
						( "sourceName", "Audio Source:", ""),
						( "setMuted", "Set To Mute:", ""),
					}
				)
			},
			//{
			//	nameof(TallyOnLive),
			//	new ActionConfigurationDetails(
			//		nameof(TallyOnLive),
			//		typeof(TallyOnLive),
			//		new[] {
			//			( "camString", "Camera String:", ""),
			//			( "tallyName", "Tally Name:", ""),
			//		}
			//	)
			//},
		};

		public IEnumerable<string> AvailableActions() => ActionDetails.Keys;

		public ActionConfigurationProperty[] PropertiesForActionType(string actionType) => ActionDetails[actionType].Properties;

		private static TimeSpan ReadTimeSpan(string sourceValue)
		{
			TimeSpan parsedTimeSpan;
			TimeSpan.TryParseExact(sourceValue, TimeSpanFormatString, null, out parsedTimeSpan);
			return parsedTimeSpan;
		}

		private readonly struct ActionConfigurationDetails
		{
			public ActionConfigurationDetails(string name, Type type, (string, string, string)[] properties)
				=> (Name, Type, Properties) = (name, type, properties.Select(f => new ActionConfigurationProperty(f.Item1, f.Item2, f.Item3)).ToArray());
			public ActionConfigurationDetails(string name, Type type, (string, string, object)[] properties)
				=> (Name, Type, Properties) = (name, type, properties.Select(f => new ActionConfigurationProperty(f.Item1, f.Item2, f.Item3)).ToArray());
			public ActionConfigurationDetails(string name, Type type, ActionConfigurationProperty[] properties)
				=> (Name, Type, Properties) = (name, type, properties);

			public string Name { get; }
			public Type Type { get; }
			public ActionConfigurationProperty[] Properties { get; }
		}

		public readonly struct ActionConfigurationProperty
		{
			public ActionConfigurationProperty(string property, string displayText, object defaultValue)
				=> (Property, DisplayText, DefaultValue) = (property, displayText, defaultValue);
			public ActionConfigurationProperty(string property, string displayText, string defaultValue)
				=> (Property, DisplayText, DefaultValue) = (property, displayText, (object)defaultValue);

			public string Property { get; }
			public string DisplayText { get; }
			public object DefaultValue { get; }

			public void Deconstruct(ref string property, ref string displayText, ref object defaultValue)
				=> (property, displayText, defaultValue) = (Property, DisplayText, DefaultValue);

			public static implicit operator ActionConfigurationProperty((string, string, object) v) => new ActionConfigurationProperty(v.Item1, v.Item2, v.Item3);
			public static implicit operator ActionConfigurationProperty((string, string, string) v) => new ActionConfigurationProperty(v.Item1, v.Item2, v.Item3);
		}
	}
}
