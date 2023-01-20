using Base.Resources.Services;
using Godot;
using BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.EnumeratedTypes;
using BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Services;

namespace BasicGames.GoldenFlutesGreatEscapes.Mars.Scenes.Intro
{
  public class MarsIntroController : Node
    {
        /// <summary>
        /// Reference to the singleton instance.
        /// </summary>
        /// <value></value>
        public static MarsIntroController Instance { get; private set; }
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            base._Ready();
            if (GameVariablesDatabase.Instance.BoolVariable["mars_debugging_on"].RuntimeValue)
            {
                GD.Print("BasicGames.GoldenFlutesGreatEscapes.Mars.Scenes.Intro.MarsIntroController._Ready()");
            }
            Instance = this;
            // initialize the scene
            // connect buttons
            GetNode<Button>("./bottom/container/btn-start").Connect("pressed", this, "OnStart", null, (uint)ConnectFlags.ReferenceCounted);
            GetNode<Button>("./bottom/container/btn-instructions").Connect("pressed", this, "OnInstructions", null, (uint)ConnectFlags.ReferenceCounted);
            GetNode<Button>("./bottom/container/btn-back").Connect("pressed", this, "OnBack", null, (uint)ConnectFlags.ReferenceCounted);

            // Load the custom images for the mouse cursor.
            var arrow = ResourceLoader.Load("res://art/themes/commodore_64/widgets/cursor_arrow.png");
            var hand = ResourceLoader.Load("res://art/themes/commodore_64/widgets/cursor_hand.png");

            // Changes only the arrow shape of the cursor.
            // This is similar to changing it in the project settings.
            Input.SetCustomMouseCursor(arrow, Input.CursorShape.Arrow, new Vector2(11, 0));

            // Changes a specific shape of the cursor (here, the I-beam shape).
            Input.SetCustomMouseCursor(hand, Input.CursorShape.PointingHand);
        }
        /// <summary>
        /// Handler for the OnBack signal.
        /// </summary>
        private void OnBack()
        {
            if (GameVariablesDatabase.Instance.BoolVariable["mars_debugging_on"].RuntimeValue)
            {
                GD.Print("\tOnBack");
            }
            GetTree().ChangeScene("res://scenes/directories/basic games/Golden Flutes and Great Escapes/golden_flutes_great_escapes.tscn");
        }
        /// <summary>
        /// Handler for the OnInstructions signal.
        /// </summary>
        private void OnInstructions()
        {
            if (GameVariablesDatabase.Instance.BoolVariable["mars_debugging_on"].RuntimeValue)
            {
                GD.Print("\tOnInstructions");
            }
            GetNode<PopupPanel>("./instructions_popup").PopupCentered();
        }
        /// <summary>
        /// Handler for the OnStart signal.
        /// </summary>
        private void OnStart()
        {
            if (GameVariablesDatabase.Instance.BoolVariable["mars_debugging_on"].RuntimeValue)
            {
                GD.Print("\tOnStart");
            }
            MarsController.Instance.GameState = MarsResourceDatabase.Instance.MarsGameStates[MarsGameStateEnum.MARS_GAME_NEW_GAME.ToString()];
            MarsController.Instance.MainCycle();
        }
        /// <summary>
        /// Handler for any Godot.InputEventKey events that weren't consumed by Godot.Node._Input(Godot.InputEvent) or any GUI.
        /// </summary>
        public override void _UnhandledKeyInput(InputEventKey @event)
        {
            if (@event.Pressed)
            {
            }
            else
            {
                OnStart();
            }
        }
    }
}