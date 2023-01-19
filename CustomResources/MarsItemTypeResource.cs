using Godot;
using BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.EnumeratedTypes;

namespace BasicGames.GoldenFlutesGreatEscapes.Mars.CustomResources
{
	public class MarsItemTypeResource : Resource
	{
		/// <summary>
		/// The MarsItemTypeResource's Title.
		/// <summary>
		/// <value></value>
		[Export]
		public string Title { get; set; }
		/// <summary>
		/// The MarsItemTypeResource's DisplayName.
		/// <summary>
		/// <value></value>
		[Export]
		public string DisplayName { get; set; }
		/// <summary>
		/// The MarsItemTypeResource's MarsItemTypeEnum.
		/// <summary>
		/// <value></value>
		[Export]
		public MarsItemTypeEnum MarsItemTypeEnum { get; set; }
	}
}
