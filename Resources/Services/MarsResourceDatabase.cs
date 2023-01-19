using Base.Bus;
using Base.Exceptions;
using Base.Pooled;
using Base.Resources.Bus;
using Base.Resources.Services;
using Godot;
using System;
using System.Collections.Generic;
using BasicGames.GoldenFlutesGreatEscapes.Mars.CustomResources;
using Base.Resources.Variables;

namespace BasicGames.GoldenFlutesGreatEscapes.Mars.Resources.Services
{
    public class MarsResourceDatabase : Node
    {
        /// <summary>
        /// Reference to the singleton instance.
        /// </summary>
        /// <value></value>
        public static MarsResourceDatabase Instance { get; private set; }
        private const string RESOURCE_BASE = "res://resources";
        #region VARIABLE INDEXER DICTIONARIES
        /// <summary>
        /// The dictionary of player attributes.
        /// </summary>
        /// <typeparam name="string">the name of the player attribute</typeparam>
        /// <typeparam name="AttributeDescriptor">the player attribute instance</typeparam>
        /// <returns></returns>
        private Dictionary<string, AttributeDescriptor> attributeDescriptorConstants = new Dictionary<string, AttributeDescriptor>();
        /// <summary>
        /// The dictionary of GenderResources
        /// </summary>
        /// <typeparam name="string">the name of the GenderResource</typeparam>
        /// <typeparam name="GenderResource">the GenderResource instance</typeparam>
        /// <returns></returns>
        private Dictionary<string, GenderResource> genderresourceDictionary = new Dictionary<string, GenderResource>();
        /// <summary>
        /// The dictionary of MarsGameStateResources
        /// </summary>
        /// <typeparam name="string">the name of the MarsGameStateResource</typeparam>
        /// <typeparam name="MarsGameStateResource">the MarsGameStateResource instance</typeparam>
        /// <returns></returns>
        private Dictionary<string, MarsGameStateResource> marsgamestateresourceDictionary = new Dictionary<string, MarsGameStateResource>();
        /// <summary>
        /// The dictionary of MarsItemTypeResources
        /// </summary>
        /// <typeparam name="string">the name of the MarsItemTypeResource</typeparam>
        /// <typeparam name="MarsItemTypeResource">the MarsItemTypeResource instance</typeparam>
        /// <returns></returns>
        private Dictionary<string, MarsItemTypeResource> marsitemtyperesourceDictionary = new Dictionary<string, MarsItemTypeResource>();
        /// <summary>
        /// The dictionary of MarsNewGameStateResources
        /// </summary>
        /// <typeparam name="string">the name of the MarsNewGameStateResource</typeparam>
        /// <typeparam name="MarsNewGameStateResource">the MarsNewGameStateResource instance</typeparam>
        /// <returns></returns>
        private Dictionary<string, MarsNewGameStateResource> marsnewgamestateresourceDictionary = new Dictionary<string, MarsNewGameStateResource>();
        /// <summary>
        /// The dictionary of MarsItemResources
        /// </summary>
        /// <typeparam name="string">the name of the MarsItemResource</typeparam>
        /// <typeparam name="MarsItemResource">the MarsItemResource instance</typeparam>
        /// <returns></returns>
        private Dictionary<string, MarsItemResource> marsitemresourceDictionary = new Dictionary<string, MarsItemResource>();
        #endregion
        #region VARIABLE INDEXERS
        /// <summary>
        /// The VariableIndexer for AttributeDescriptors
        /// </summary>
        /// <value></value>
        public VariableIndexer<string, AttributeDescriptor> AttributeDescriptors { get; private set; }
        /// <summary>
        /// The VariableIndexer for GenderResources
        /// </summary>
        /// <value></value>
        public VariableIndexer<string, GenderResource> Genders { get; private set; }
        /// <summary>
        /// The VariableIndexer for MarsGameStateResources
        /// </summary>
        /// <value></value>
        public VariableIndexer<string, MarsGameStateResource> MarsGameStates { get; private set; }
        /// <summary>
        /// The VariableIndexer for MarsItemTypeResources
        /// </summary>
        /// <value></value>
        public VariableIndexer<string, MarsItemTypeResource> MarsItemTypes { get; private set; }
        /// <summary>
        /// The VariableIndexer for MarsNewGameStateResources
        /// </summary>
        /// <value></value>
        public VariableIndexer<string, MarsNewGameStateResource> MarsNewGameStates { get; private set; }
        /// <summary>
        /// The VariableIndexer for MarsItemResources
        /// </summary>
        /// <value></value>
        public VariableIndexer<string, MarsItemResource> MarsItems { get; private set; }
        #endregion
        #region TABLES
        /// <summary>
        /// The GenderTableResource.
        /// </summary>
        /// <value></value>
        public GenderTableResource GenderTable { get; private set; }
        #endregion
        /// <summary>
        /// Recursively loads a directory, storing all identifiable resources in variable indexers.
        /// </summary>
        /// <param name="path"></param>
        private void LoadDirectory(params string[] path)
        {
            PooledStringBuilder sb = StringBuilderPool.Instance.GetStringBuilder();
            sb.Append(RESOURCE_BASE);
            for (int i = 0, li = path.Length; i < li; i++) {
                sb.Append("/");
                sb.Append(path[i]);
            }
            Directory dir = new Directory();
            if (dir.Open(sb.ToString()) == Godot.Error.Ok)
            {
                dir.ListDirBegin(skipNavigational: true);
                string fileName = dir.GetNext();
                while (!fileName.Equals("", StringComparison.OrdinalIgnoreCase))
                {
                    string[] arr = ArrayUtilities.Instance.CopyArray(path);
                    arr = ArrayUtilities.Instance.ExtendArray(fileName, arr);
                    if (dir.CurrentIsDir())
                    {
                        GD.Print("Found directory:", fileName);
                        if (!fileName.Equals(".git", StringComparison.OrdinalIgnoreCase))
                        {
                            LoadDirectory(arr);
                        }
                    }
                    else
                    {
                        GD.Print("Found file:", fileName);

                        string key = fileName.Substr(0, fileName.Length - 5);
                        
                        sb.Length = 0;
                        sb.Append(RESOURCE_BASE);
                        for (int i = 0, li = arr.Length; i < li; i++) {
                            sb.Append("/");
                            sb.Append(arr[i]);
                        }
                        GD.Print("load:", sb.ToString());
                        Resource res = GD.Load(sb.ToString());
                        if (res is AttributeDescriptor)
                        {
                            key = ((AttributeDescriptor)res).Abbreviation;
                            if (attributeDescriptorConstants.ContainsKey(key))
                            {
                                throw new RPGException(ErrorMessage.INTERNAL_ERROR, "Duplicate Constant defined: " + key);
                            }
                            attributeDescriptorConstants.Add(key, (AttributeDescriptor)res);
                        }
                        else if (res is GenderResource)
                        {
                            key = ((GenderResource)res).Title;
                            if (genderresourceDictionary.ContainsKey(key))
                            {
                                throw new RPGException(ErrorMessage.INTERNAL_ERROR, "Duplicate Constant defined: " + key);
                            }
                            genderresourceDictionary.Add(key, (GenderResource)res);
                        }
                        else if (res is GenderResource)
                        {
                            key = ((GenderResource)res).Title;
                            if (genderresourceDictionary.ContainsKey(key))
                            {
                                throw new RPGException(ErrorMessage.INTERNAL_ERROR, "Duplicate Constant defined: " + key);
                            }
                            genderresourceDictionary.Add(key, (GenderResource)res);
                        }
                        else if (res is MarsGameStateResource)
                        {
                            key = ((MarsGameStateResource)res).Title;
                            if (marsgamestateresourceDictionary.ContainsKey(key))
                            {
                                throw new RPGException(ErrorMessage.INTERNAL_ERROR, "Duplicate Constant defined: " + key);
                            }
                            marsgamestateresourceDictionary.Add(key, (MarsGameStateResource)res);
                        }
                        else if (res is MarsItemTypeResource)
                        {
                            key = ((MarsItemTypeResource)res).Title;
                            if (marsitemtyperesourceDictionary.ContainsKey(key))
                            {
                                throw new RPGException(ErrorMessage.INTERNAL_ERROR, "Duplicate Constant defined: " + key);
                            }
                            marsitemtyperesourceDictionary.Add(key, (MarsItemTypeResource)res);
                        }
                        else if (res is MarsNewGameStateResource)
                        {
                            key = ((MarsNewGameStateResource)res).Title;
                            if (marsnewgamestateresourceDictionary.ContainsKey(key))
                            {
                                throw new RPGException(ErrorMessage.INTERNAL_ERROR, "Duplicate Constant defined: " + key);
                            }
                            marsnewgamestateresourceDictionary.Add(key, (MarsNewGameStateResource)res);
                        }
                        else if (res is MarsItemResource)
                        {
                            key = ((MarsItemResource)res).Title;
                            if (marsitemresourceDictionary.ContainsKey(key))
                            {
                                throw new RPGException(ErrorMessage.INTERNAL_ERROR, "Duplicate Constant defined: " + key);
                            }
                            marsitemresourceDictionary.Add(key, (MarsItemResource)res);
                        }
                        else if (res is GenderTableResource)
                        {
                            GenderTable = (GenderTableResource)res;
                        }
                        else if (res is BoolVariable
                                || res is IntVariable
                                || res is FloatVariable
                                || res is StringVariable
                                || res is StringArrayVariable)
                        {
                            GameVariablesDatabase.Instance.AddResource(key, res, true);
                        }
                        else
                        {
                            throw new RPGException(ErrorMessage.INTERNAL_ERROR, "unexpected resource file " + res.GetType());
                        }
                    }
                    fileName = dir.GetNext();
                }
            }
            sb.ReturnToPool();

            AttributeDescriptors = new VariableIndexer<string, AttributeDescriptor>(attributeDescriptorConstants);
            Genders = new VariableIndexer<string, GenderResource>(genderresourceDictionary);
            MarsGameStates = new VariableIndexer<string, MarsGameStateResource>(marsgamestateresourceDictionary);
            MarsItemTypes = new VariableIndexer<string, MarsItemTypeResource>(marsitemtyperesourceDictionary);
            MarsNewGameStates = new VariableIndexer<string, MarsNewGameStateResource>(marsnewgamestateresourceDictionary);
            MarsItems = new VariableIndexer<string, MarsItemResource>(marsitemresourceDictionary);
        }
        /// <summary>
        /// Called when the node enters the scene tree for the first time.
        /// </summary>
        public override void _Ready()
        {
            base._Ready();
            Instance = this;
            LoadDirectory("BasicGames/GoldenFlutesGreatEscapes/Mars");
        }
    }
}
