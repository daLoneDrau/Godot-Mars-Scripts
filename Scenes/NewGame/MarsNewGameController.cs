using Base.Pooled;
using Base.Resources.Events;
using Base.Resources.Services;
using Godot;
using BasicGames.GoldenFlutesGreatEscapes.Mars.CustomResources;
using BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Bus;
using BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Bus.Data;
using BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.EnumeratedTypes;
using BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Services;

namespace BasicGames.GoldenFlutesGreatEscapes.Mars.Scenes.NewGame
{
    public class MarsNewGameController : Node
    {
        /// <summary>
        /// Reference to the singleton instance.
        /// </summary>
        /// <value></value>
        public static MarsNewGameController Instance { get; private set; }
        /// <summary>
        /// the current scene state.
        /// </summary>
        /// <returns></returns>
        private MarsNewGameStateResource State { get; set; } = MarsResourceDatabase.Instance.MarsNewGameStates[MarsNewGameStateEnum.MARS_NEW_GAME_NEW_PC.ToString()];
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            base._Ready();
            if (GameVariablesDatabase.Instance.BoolVariable["mars_debugging_on"].RuntimeValue)
            {
                GD.Print("BasicGames.GoldenFlutesGreatEscapes.Mars.Scenes.NewGame.MarsNewGameController._Ready()");
            }
            PublicBroadcastService.Instance.PcEventChannel.AddSubscriber(HandlePlayerEvents);
            Instance = this;
            // initialize the scene
            // connect buttons
            Control dialog = GetNode<Control>("./character-creation-dialog-1");
            dialog.GetNode<Button>("./content/container/btn-accept").Connect("pressed", this, "OnAccept", null, (uint)ConnectFlags.ReferenceCounted);
            dialog.GetNode<Button>("./content/container/btn-decline").Connect("pressed", this, "OnDecline", null, (uint)ConnectFlags.ReferenceCounted);

            dialog = GetNode<Control>("./character-creation-dialog-2");
            dialog.GetNode<Button>("./content/container/btn-continue").Connect("pressed", this, "OnContinue", null, (uint)ConnectFlags.ReferenceCounted);
            StateCheck();
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
        public void HandlePlayerEvents(PcEventSignal signal)
        {
            if (GameVariablesDatabase.Instance.BoolVariable["mars_debugging_on"].RuntimeValue)
            {
                GD.Print("BasicGames.GoldenFlutesGreatEscapes.Mars.Scenes.NewGame.MarsNewGameController.HandlePlayerEvents(", signal.EventId);
            }

            PooledStringBuilder sb = StringBuilderPool.Instance.GetStringBuilder();

            Control dialog = GetNode<Control>("./character-creation-dialog-1");
            { // clear fields
                { // NAME
                    sb.Append("./content/stats/content//column/lbl-name");
                    dialog.GetNode<Label>(sb.ToString()).Text = "";
                    sb.Length = 0;
                }
                { // HEALTH
                    sb.Append("./content/stats/content//column/rows/col-values/health-value");
                    dialog.GetNode<Label>(sb.ToString()).Text = "";
                    sb.Length = 0;
                }
                { // POWER
                    sb.Append("./content/stats/content//column/rows/col-values/pow-value");
                    dialog.GetNode<Label>(sb.ToString()).Text = "";
                    sb.Length = 0;
                }
                { // SPEED
                    sb.Append("./content/stats/content//column/rows/col-values/spd-value");
                    dialog.GetNode<Label>(sb.ToString()).Text = "";
                    sb.Length = 0;
                }
                { // AIM
                    sb.Append("./content/stats/content//column/rows/col-values/aim-value");
                    dialog.GetNode<Label>(sb.ToString()).Text = "";
                    sb.Length = 0;
                }
            }

            {  // set fields
                { // NAME
                    sb.Append("./content/stats/content//column/lbl-name");
                    string path = sb.ToString();
                    sb.Length = 0;
                    sb.Append(MarsController.Instance.MarsExplorer.Rank);
                    sb.Append("\r\n");
                    sb.Append(MarsController.Instance.MarsExplorer.Name);
                    dialog.GetNode<Label>(path).Text = sb.ToString();
                    sb.Length = 0;
                }
                { // HEALTH
                    sb.Append("./content/stats/content//column/rows/col-values/health-value");
                    dialog.GetNode<Label>(sb.ToString()).Text = ((int)(MarsController.Instance.MarsExplorer.Attributes["MAX_HEALTH"].Base / 2)).ToString();
                    sb.Length = 0;
                }
                { // POWER
                    sb.Append("./content/stats/content//column/rows/col-values/pow-value");
                    dialog.GetNode<Label>(sb.ToString()).Text = ((int)(MarsController.Instance.MarsExplorer.Attributes["POW"].Base)).ToString();
                    sb.Length = 0;
                }
                { // SPEED
                    sb.Append("./content/stats/content//column/rows/col-values/spd-value");
                    dialog.GetNode<Label>(sb.ToString()).Text = ((int)(MarsController.Instance.MarsExplorer.Attributes["SPD"].Base)).ToString();
                    sb.Length = 0;
                }
                { // AIM
                    sb.Append("./content/stats/content//column/rows/col-values/aim-value");
                    dialog.GetNode<Label>(sb.ToString()).Text = ((int)(MarsController.Instance.MarsExplorer.Attributes["AIM"].Base)).ToString();
                    sb.Length = 0;
                }
            }
            dialog = GetNode<Control>("./character-creation-dialog-2");
            { // OUTRO
                sb.Append("./content/header");
                string path = sb.ToString();
                sb.Length = 0;
                sb.Append("GOOD LUCK, ");
                sb.Append(MarsController.Instance.MarsExplorer.Rank);
                sb.Append("\r\nPrepare for landing.");
                dialog.GetNode<Label>(path).Text = sb.ToString();
                sb.Length = 0;
            }
            
            sb.ReturnToPool();
        }
        /// <summary>
        /// Hides all dialogs.
        /// </summary>
        private void HideAllDialogs()
        {
            GetNode<Control>("./character-creation-dialog-1").Visible = false;
            GetNode<Control>("./character-creation-dialog-2").Visible = false;
        }
        /// <summary>
        /// Handler for the signal when the player accepts the explorer assigned.
        /// </summary>
        public void OnAccept()
        {
            // go to next state
            State = MarsResourceDatabase.Instance.MarsNewGameStates[MarsNewGameStateEnum.MARS_NEW_GAME_PC_FINISHED.ToString()];

            // re-check
            StateCheck();
        }
        /// <summary>
        /// Handler for the signal when the player is ready to continue.
        /// </summary>
        public void OnContinue()
        {
            if (GameVariablesDatabase.Instance.BoolVariable["mars_debugging_on"].RuntimeValue)
            {
                GD.Print("\tOnContinue");
            }
            MarsController.Instance.GameState = MarsResourceDatabase.Instance.MarsGameStates[MarsGameStateEnum.MARS_GAME_INIT.ToString()];
            MarsController.Instance.MainCycle();
        }
        /// <summary>
        /// Handler for the signal when the player declines the explorer assigned.
        /// </summary>
        public void OnDecline() { MarsController.Instance.MarsExplorer.NewPlayer(); }
        /// <summary>
        /// Checks the scene state and branches processing accordingly.
        /// </summary>
        private void StateCheck()
        {
            HideAllDialogs();
            switch (State.MarsNewGameStateEnum)
            {
                case MarsNewGameStateEnum.MARS_NEW_GAME_NEW_PC:
                    // create a PC
                    MarsPcData pc = MarsController.Instance.MarsExplorer;
                    if (MarsController.Instance.MarsExplorer == null)
                    {
                        MarsInteractiveObject io = (MarsInteractiveObject)MarsIoFactory.Instance.AddPc();
                        pc = (MarsPcData)io.Data;
                        MarsController.Instance.MarsExplorer = pc;
                    }
                    pc.NewPlayer();

                    // go to next state
                    State = MarsResourceDatabase.Instance.MarsNewGameStates[MarsNewGameStateEnum.MARS_NEW_GAME_ROLL_PC.ToString()];

                    // re-check
                    StateCheck();
                    break;
                case MarsNewGameStateEnum.MARS_NEW_GAME_ROLL_PC:
                    // switch dialogs
                    GetNode<Control>("./character-creation-dialog-1").Visible = true;
                    break;
                case MarsNewGameStateEnum.MARS_NEW_GAME_PC_FINISHED:
                    // switch dialogs
                    GetNode<Control>("./character-creation-dialog-2").Visible = true;
                    break;
            }
        }
    }
}