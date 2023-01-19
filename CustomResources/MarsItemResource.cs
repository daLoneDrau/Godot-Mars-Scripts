using Godot;
using BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.EnumeratedTypes;
using System.Collections.Generic;

namespace BasicGames.GoldenFlutesGreatEscapes.Mars.CustomResources
{
	public class MarsItemResource : Resource
	{
		/// <summary>
		/// The MarsItemResource's Title.
		/// <summary>
		/// <value></value>
		[Export]
		public string Title { get; set; }
		/// <summary>
		/// The MarsItemResource's DisplayName.
		/// <summary>
		/// <value></value>
		[Export]
		public string DisplayName { get; set; }
		/// <summary>
		/// The MarsItemResource's Description.
		/// <summary>
		/// <value></value>
		[Export]
		public string Description { get; set; }
		/// <summary>
		/// The MarsItemResource's ItemType.
		/// <summary>
		/// <value></value>
		[Export]
		public MarsItemTypeResource ItemType { get; set; }
	}
}
