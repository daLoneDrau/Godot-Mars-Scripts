using Base.Resources.Services;
using Godot;
using System;
using System.Collections.Generic;
using BasicGames.GoldenFlutesGreatEscapes.Mars.CustomResources;
using BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Bus.Data;
using BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.EnumeratedTypes;
using BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Services;

namespace BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Bus.Map
{
    public class MarsMap : Node
    {
        
        /// <summary>
        /// Reference to the singleton instance.
        /// </summary>
        /// <value></value>
        public static MarsMap Instance { get; private set; }
        /// <summary>
        /// the rocket's location.
        /// </summary>
        /// <value></value>
        public Vector2 Rocket { get; set; }
        /// <summary>
        /// a flag indicating whether the player is in the rocket.
        /// </summary>
        /// <value></value>
        public bool InRocket { get; set; } = true;
        /// <summary>
        /// the list of locations.
        /// </summary>
        private MarsLocation[] locations;
        /// <summary>
        /// Gets a specific location by its coordinates.
        /// </summary>
        /// <value></value>
        public MarsLocation this[Vector2 location]
        {
            get
            {
                MarsLocation loc = null;
                for (int i = locations.Length - 1; i >= 0; i--)
                {
                    if (locations[i].Location.Equals(location))
                    {
                        loc = locations[i];
                        break;
                    }
                }
                return loc;
            }
        }
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            base._Ready();
            if (GameVariablesDatabase.Instance.BoolVariable["mars_debugging_on"].RuntimeValue)
            {
                GD.Print("BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Bus.Map.MarsMap._Ready()");
            }
            if (Instance == null)
            {
                // no instance created yet
                Instance = this;
                locations = new MarsLocation[100];
                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        locations = ArrayUtilities.Instance.ExtendArray(new MarsLocation(x, y), locations);
                    }
                }
            }
        }
        /// <summary>
        /// Creates a new map.
        /// </summary>
        /// <value></value>
        public void NewMap()
        {
            // reset the map
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    MarsLocation loc = this[new Vector2(x, y)];
                    loc.Set(x, y);
                    if (DiceRoller.Instance.RollDX(17) >= 12)
                    {
                        // place obstacles
                    }
                }
            }

            //  set the rocket's location
            Rocket = new Vector2(DiceRoller.Instance.RollDXPlusY(10, -1), DiceRoller.Instance.RollDXPlusY(10, -1));
            InRocket = true;

            List<string> itemList = MarsResourceDatabase.Instance.MarsItems.Keys;
            for (int i = itemList.Count - 1; i >= 0; i--)
            {
                MarsItemResource itemResource = MarsResourceDatabase.Instance.MarsItems[itemList[i]];
                MarsInteractiveObject io = (MarsInteractiveObject)MarsIoFactory.Instance.AddItem();
                MarsItemData item = (MarsItemData)io.Data;
                item.ItemResource = itemResource;
                
                //  put all supplies at the rocket.
                if (itemResource.ItemType.MarsItemTypeEnum == MarsItemTypeEnum.MARS_ITEM_TYPE_SUPPLY)
                {
                    io.Location = new Vector2(Rocket);
                }
                else //  randomly place junk and treasure
                {
                    io.Location = new Vector2(DiceRoller.Instance.RollDXPlusY(10, -1), DiceRoller.Instance.RollDXPlusY(10, -1));
                }
            }
        }
    }
}