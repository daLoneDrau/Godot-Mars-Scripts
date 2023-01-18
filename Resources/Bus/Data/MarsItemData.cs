using Base.Bus;
using Base.Pooled;
using Base.Resources.Bus;
using Base.Resources.Data;
using Base.Resources.Events;
using Base.Resources.Services;
using Bus.Services;
using Godot;
using System;
using System.Collections.Generic;
using Test_App.BasicGames.GoldenFlutesGreatEscapes.Mars.CustomResources;
using Test_App.BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.EnumeratedTypes;
using Test_App.BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Services;

namespace Test_App.BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Bus.Data
{
    public class MarsItemData : IoItemData
    {
        public MarsItemResource ItemResource { get; set; }
        public DateTime TimeCreated { get; set; }
        /// <summary>
        /// Creates a new CsPcData instance.
        /// </summary>
        public MarsItemData()
        {
        }
    }
}
