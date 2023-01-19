using Godot;
using BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.EnumeratedTypes;

namespace BasicGames.GoldenFlutesGreatEscapes.Mars.CustomResources
{
	public class MarsNewGameStateResource : Resource
	{
		/// <summary>
		/// The MarsNewGameStateResource's Title.
		/// <summary>
		/// <value></value>
		[Export]
		public string Title { get; set; }
		/// <summary>
		/// The MarsNewGameStateResource's DisplayName.
		/// <summary>
		/// <value></value>
		[Export]
		public string DisplayName { get; set; }
		/// <summary>
		/// The MarsNewGameStateResource's MarsNewGameStateEnum.
		/// <summary>
		/// <value></value>
		[Export]
		public MarsNewGameStateEnum MarsNewGameStateEnum { get; set; }
	}
}
