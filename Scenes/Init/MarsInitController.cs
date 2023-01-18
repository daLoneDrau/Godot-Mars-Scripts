using Base.Resources.Services;
using Godot;
using Test_App.BasicGames.GoldenFlutesGreatEscapes.Mars.CustomResources;
using Test_App.BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Bus;
using Test_App.BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Bus.Data;
using Test_App.BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Bus.Map;
using Test_App.BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.EnumeratedTypes;
using Test_App.BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Services;

namespace Test_App.BasicGames.GoldenFlutesGreatEscapes.Mars.Scenes.Init
{
  public class MarsInitController : Node
    {
        /// <summary>
        /// Reference to the singleton instance.
        /// </summary>
        /// <value></value>
        public static MarsInitController Instance { get; private set; }
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            base._Ready();
            if (GameVariablesDatabase.Instance.BoolVariable["mars_debugging_on"].RuntimeValue)
            {
                GD.Print("Test_App.BasicGames.GoldenFlutesGreatEscapes.Mars.Scenes.Intro.MarsInitController._Ready()");
            }
            Instance = this;
            // initialize the scene
            MarsMap.Instance.NewMap();
            // connect buttons
            GetNode<Button>("./bottom/container/btn-start").Connect("pressed", this, "OnStart", null, (uint)ConnectFlags.ReferenceCounted);
            GetNode<Button>("./bottom/container/btn-instructions").Connect("pressed", this, "OnInstructions", null, (uint)ConnectFlags.ReferenceCounted);
            GetNode<Button>("./bottom/container/btn-back").Connect("pressed", this, "OnBack", null, (uint)ConnectFlags.ReferenceCounted);
        }
        /// <summary>
        /// Handler for any Godot.InputEventKey events that weren't consumed by Godot.Node._Input(Godot.InputEvent) or any GUI.
        /// </summary>
        public override void _UnhandledKeyInput(InputEventKey @event)
        {
            if (@event.Pressed)
            {
            }
        }
    }
}