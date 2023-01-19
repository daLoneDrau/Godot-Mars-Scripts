using Godot;
using BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.EnumeratedTypes;

namespace BasicGames.GoldenFlutesGreatEscapes.Mars.CustomResources
{
	public class MarsGameStateResource : Resource
	{
		/// <summary>
		/// The MarsGameStateResource's Title.
		/// <summary>
		/// <value></value>
		[Export]
		public string Title { get; set; }
		/// <summary>
		/// The MarsGameStateResource's DisplayName.
		/// <summary>
		/// <value></value>
		[Export]
		public string DisplayName { get; set; }
		/// <summary>
		/// The MarsGameStateResource's MarsGameStateEnum.
		/// <summary>
		/// <value></value>
		[Export]
		public MarsGameStateEnum MarsGameStateEnum { get; set; }
	}
}
