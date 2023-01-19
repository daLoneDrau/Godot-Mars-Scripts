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
using BasicGames.GoldenFlutesGreatEscapes.Mars.CustomResources;
using BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.EnumeratedTypes;
using BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Services;

namespace BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Bus.Data
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
