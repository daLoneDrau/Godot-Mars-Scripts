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
    public class MarsPcData : IoPcData
    {
        /// <summary>
        /// The PC's gender.
        /// </summary>
        /// <value></value>
        public GenderResource Gender { get; private set; }
        public string Name { get; set; }
        public string Rank { get; set; }
        public DateTime TimeCreated { get; set; }
        /// <summary>
        /// Creates a new CsPcData instance.
        /// </summary>
        public MarsPcData()
        {
            // load all attributes
            string[] attributes = GameVariablesDatabase.Instance.StringArrayVariable["mars_player_attributes"].RuntimeValue;
            for (int i = attributes.Length - 1; i >= 0; i--)
            {
                GD.Print("\t add pc attr " + attributes[i]);
                AttributeDescriptor ad = MarsResourceDatabase.Instance.AttributeDescriptors[attributes[i]];
                GD.Print(ad);
                GD.Print(ad.Abbreviation);
                Attributes.Add(new PlayerAttribute(ad, this));
            }
            this.equipped = new int[0];
        }
        public string Genderize(string s)
        {
            return s.Replace("[gender-noun]", this.Gender.Noun);
        }
        /// <summary>
        /// Resets the player's attributes and inventory.
        /// </summary>
        public void NewPlayer()
        {
            PooledStringBuilder sb = StringBuilderPool.Instance.GetStringBuilder();
            string[] ranks = GameVariablesDatabase.Instance.StringArrayVariable["mars_military_ranks"].RuntimeValue;
            string[] amFirstNames = GameVariablesDatabase.Instance.StringArrayVariable["mars_american_first_names"].RuntimeValue;
            string[] amLastNames = GameVariablesDatabase.Instance.StringArrayVariable["mars_american_last_names"].RuntimeValue;
            string[] ruFirstNames = GameVariablesDatabase.Instance.StringArrayVariable["mars_russian_first_names"].RuntimeValue;
            string[] ruLastNames = GameVariablesDatabase.Instance.StringArrayVariable["mars_russian_last_names"].RuntimeValue;
            int max = 0;
            for (int i = ranks.Length - 1; i >= 0; i--)
            {
                max = Math.Max(max, ranks[i].Length);
            }
            GD.Print("max rank ", max);
            max = 0;
            for (int i = amFirstNames.Length - 1; i >= 0; i--)
            {
                max = Math.Max(max, amFirstNames[i].Length);
            }
            GD.Print("max am fir ", max);
            for (int i = amLastNames.Length - 1; i >= 0; i--)
            {
                max = Math.Max(max, amLastNames[i].Length);
            }
            GD.Print("max am las ", max);
            for (int i = ruFirstNames.Length - 1; i >= 0; i--)
            {
                max = Math.Max(max, ruFirstNames[i].Length);
            }
            GD.Print("max ru fir ", max);
            for (int i = ruLastNames.Length - 1; i >= 0; i--)
            {
                max = Math.Max(max, ruLastNames[i].Length);
            }
            GD.Print("max ru las ", max);
            // reset everything
            if (GameVariablesDatabase.Instance.BoolVariable["mars_debugging_on"].RuntimeValue)
            {
                GD.Print("\tOnStart");
            }

            // step 1 - NAME
            this.Rank = (string)DiceRoller.Instance.GetRandomMember(ranks);
            switch (DiceRoller.Instance.ONE_D2.Roll())
            {
                case 1:
                    sb.Append(DiceRoller.Instance.GetRandomMember(amFirstNames));
                    sb.Append(" ");
                    sb.Append(DiceRoller.Instance.GetRandomMember(amLastNames));
                    break;
                case 2:
                    sb.Append(DiceRoller.Instance.GetRandomMember(ruFirstNames));
                    sb.Append(" ");
                    sb.Append(DiceRoller.Instance.GetRandomMember(ruLastNames));
                    break;
            }
            this.Name = sb.ToString();
            sb.Length = 0;
            sb.ReturnToPool();
            
            // step 1 - roll attributes
            this.Attributes["AIM"].Base = DiceRoller.Instance.RollDX(50) + 50;
            this.Attributes["MAX_HEALTH"].Base = DiceRoller.Instance.ONE_D100.Roll() + 100;
            this.Attributes["SPEED"].Base = DiceRoller.Instance.RollDX(50) + 50;
            this.Attributes["POWER"].Base = DiceRoller.Instance.RollDX(50) + 50;

            PublicBroadcastService.Instance.PcEventChannel.Broadcast(new PcEventSignal(Io.RefId, Globals.PLAYER_EVENT_UPDATE_ATTRIBUTES));
        }
        public override string ToString()
        {
            PooledStringBuilder sb = StringBuilderPool.Instance.GetStringBuilder();
            sb.Append("Gender\t");
            sb.Append(Gender.Title);
            sb.Append("\r\n");
            
            string s = sb.ToString();
            sb.ReturnToPool();
            return s;
        }
        public override void BecomesDead()
        {
        throw new NotImplementedException();
        }

        public override void ComputeStats()
        {
            // throw new NotImplementedException();
        }

        public override float DamagePlayer(float damage, FlagSet type, int source)
        {
        throw new NotImplementedException();
        }

        public override bool IsDead()
        {
        throw new NotImplementedException();
        }

        public override void PutInFrontOfPlayer(InteractiveObject itemIo, int flag)
        {
        throw new NotImplementedException();
        }

        public override void RecreateMesh()
        {
        throw new NotImplementedException();
        }

        public override void SetPlayerAttributes()
        {
        throw new NotImplementedException();
        }

        protected override void ApplyRulesModifiers()
        {
        throw new NotImplementedException();
        }

        protected override void ApplyRulesPercentModifiers()
        {
        throw new NotImplementedException();
        }
    }
}
