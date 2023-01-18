using Base.Resources.Data;
using Base.Resources.Services;
using Godot;
using Test_App.BasicGames.GoldenFlutesGreatEscapes.Mars.CustomResources;
using Test_App.BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Bus;
using Test_App.BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Bus.Data;
using Test_App.BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.EnumeratedTypes;

namespace Test_App.BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Services
{
  public class MarsController : Node
    {
        /// <summary>
        /// Reference to the singleton instance.
        /// </summary>
        /// <value></value>
        public static MarsController Instance { get; private set; }
        /// <summary>
        /// The game's state.
        /// </summary>
        /// <value></value>
        public MarsGameStateResource GameState { get; set; }
        /// <summary>
        /// The character being created in the Training Grounds.
        /// </summary>
        /// <value></value>
        public MarsPcData MarsExplorer { get; set; }
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            base._Ready();
            Instance = this;
        }
        /// <summary>
        /// Begins the game.
        /// </summary>
        public void Begin()
        {
            if (GameVariablesDatabase.Instance.BoolVariable["mars_debugging_on"].RuntimeValue)
            {
                GD.Print("Test_App.BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Services.MarsController.Begin()");
            }
            GameState = MarsResourceDatabase.Instance.MarsGameStates[MarsGameStateEnum.MARS_GAME_START.ToString()];
            MainCycle();
        }
        /// <summary>
        /// Resets game settings.
        /// </summary>
        public void Reset()
        {
            if (GameVariablesDatabase.Instance.BoolVariable["mars_debugging_on"].RuntimeValue)
            {
                GD.Print("Test_App.BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Services.MarsController.Reset()");
            }
        }
        public void MainCycle()
        {
            if (GameVariablesDatabase.Instance.BoolVariable["mars_debugging_on"].RuntimeValue)
            {
                GD.Print("Test_App.BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Services.MarsController.MainCycle()");
            }
            string nextSceneToDisplay = null;
            switch (GameState.MarsGameStateEnum)
            {
                case MarsGameStateEnum.MARS_GAME_START:
                    nextSceneToDisplay = "res://scenes/directories/basic games/Golden Flutes and Great Escapes/Mars/intro/mars-intro.tscn";
                    break;
                case MarsGameStateEnum.MARS_GAME_NEW_GAME:
                    nextSceneToDisplay = "res://scenes/directories/basic games/Golden Flutes and Great Escapes/Mars/new_game/mars_new_game.tscn";
                    break;
                case MarsGameStateEnum.MARS_GAME_INIT:
                    nextSceneToDisplay = "res://scenes/directories/basic games/Golden Flutes and Great Escapes/Mars/init/mars-init.tscn";
                    break;
            }
            if (nextSceneToDisplay != null && nextSceneToDisplay.Length > 0)
            {
                if (GameVariablesDatabase.Instance.BoolVariable["mars_debugging_on"].RuntimeValue)
                {
                    GD.Print("\t switch scene ", nextSceneToDisplay);
                }
                GetTree().ChangeScene(nextSceneToDisplay);
            }
        }
    }
}
