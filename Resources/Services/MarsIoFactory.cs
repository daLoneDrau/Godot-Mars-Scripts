using Base.Resources.Bus;
using Base.Resources.Services;
using Bus.Services;
using Godot;
using System;
using Test_App.BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Bus;
using Test_App.BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Bus.Data;

namespace Test_App.BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Services
{
    public class MarsIoFactory : IoFactory
    {
        /// <summary>
        /// the reference Ids.
        /// </summary>
        private int refIds = 0;
        public override InteractiveObject AddItem()
        {
            // create a new IO with a valid RefId
            MarsInteractiveObject io = new MarsInteractiveObject();
            io.RefId = refIds++;
            // register the IO
            AddIo(io);
            // add player flag and Data component
            io.IoFlags.Add(Globals.IO_ITEM);
            io.Data = new MarsItemData();
            return io;
        }
        public override InteractiveObject AddNpc()
        {
            throw new NotImplementedException();
        }
        public override InteractiveObject AddPc()
        {
            // create a new IO with a valid RefId
            MarsInteractiveObject io = new MarsInteractiveObject();
            io.RefId = refIds++;
            // register the IO
            AddIo(io);
            // add player flag and Data component
            io.IoFlags.Add(Globals.IO_PC);
            io.Data = new MarsPcData();
            /*
            // add script
            io.Script = CreateInstance<AkalabethPlayer>();
            this.SendInitScriptEvent(io);
            this.playerIo = io;
            */
            return io;
        }

        public override int GenerateRefId()
        {
            throw new NotImplementedException();
        }
        public override int GetLastId()
        {
            throw new NotImplementedException();
        }
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            base._Ready();
            IoFactory.Instance = this;
        }
        public void ReInstantiate()
        {
            IoFactory.Instance = this;
        }
    }
}
