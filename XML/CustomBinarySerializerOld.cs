//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Reflection;
//using Assets._Scripts.General.Characters.Player;
//using Assets._Scripts.General.Characters.Player.Stats;
//using Assets._Scripts.General.Characters.Player.Stats.Mutable;
//using Assets._Scripts.General.Systems.Stats;
//using Assets._Scripts.General.Systems.Stats.Base;
//using UnityEngine;
//using Time = TraderSimulator.Core.Systems.TimeSystem.Time;


//namespace Assets._Scripts.General.Data
//{
//    public class DataSaveLoadManager
//    {
//        public readonly int Id;

//        public readonly string FilePrefix;

//        public string SaveLocation 
//            => Path.Combine(Application.persistentDataPath, FilePrefix + Id + ".sav");

//        public bool FileExists 
//            => File.Exists(SaveLocation);

//        public static int SavesCount;

//        /// <summary>
//        /// Parametrized constructor with saveSlotId passed as a argument. Whole DataSaveLoadManager will work on one slot at once.
//        /// </summary>
//        /// <param name="id"></param>
//        /// <param name="fileName"></param>
//        public DataSaveLoadManager(int id, string fileName)
//        {
//            Id = id;
//            FilePrefix = fileName;
//        }

//        /// <summary>
//        /// Checks whether file exists, reads data using BinaryReader (at first compares header to prevent save corruption). 
//        /// </summary>
//        /// <returns>New instance of gamedata</returns>
//        public GameData LoadGameData()
//        {
//            if (!FileExists)
//                throw new FileNotFoundException();

//            using (Stream s = File.OpenRead(SaveLocation))
//            {
//                using (BinaryReader r = new BinaryReader(s))
//                {
//                    ReadHeader(r);

//                    int currentGameDataId = r.ReadInt32();
//                    int currentTime = r.ReadInt32();

//                    // Reads player data
//                    var playerData = ReadPlayerData(r);

//                    return new GameData
//                    {
//                        IsLoaded = true,
//                        Id = currentGameDataId,
//                        ElapsedMinutes = currentTime,
//                        PlayerData = playerData,
//                    };
//                }
//            }
//        }

//        /// <summary>
//        /// Saves gamedata to file using custom binary formatter.
//        /// </summary>
//        /// <param name="data"></param>
//        public void SaveGameData(GameData data)
//        {
//            using (FileStream stream = new FileStream(SaveLocation, FileMode.Create))
//            {
//                using (BinaryWriter w = new BinaryWriter(stream))
//                {
//                    var playerData = data.PlayerData;

//                    // writes custom header
//                    WriteHeader(w);

//                    w.Write(data.Id);
//                    w.Write(data.ElapsedMinutes);

//                    // writes PlayerData to file
//                    WritePlayerData(w, playerData);             
//                }
//            }
//        }

//        /// <summary>
//        /// Creates gamedata instance with default values, then saves it on disk.
//        /// </summary>
//        public void CreateNewGameData()
//        {
//            if (FileExists)
//            {
//                Debug.LogError("Slot " + Id + " is currently occupied");
//                return;
//            }

//            // initialize all data with default values
//            var playerData = new PlayerData
//            {
//                XPos = 0f,
//                YPos = 0f,
//                PlayerStats = TraderStats.Default.StatsList,
//            };

//            var gameData = new GameData
//            {
//                Id = Id,
//                ElapsedMinutes = Time.DefaultMinutes,
//                PlayerData = playerData,
//            };

//            SaveGameData(gameData);
//        }

//        /// <summary>
//        /// Deletes gamedata file.
//        /// </summary>
//        public void DeleteGameData()
//        {
//            if (!FileExists)
//                return;

//            File.Delete(SaveLocation);
//        }

//        #region Header

//        void WriteHeader(BinaryWriter writer)
//        {
//            writer.Write("TS".ToCharArray());
//        }

//        void ReadHeader(BinaryReader reader)
//        {
//            string header = new string(reader.ReadChars(2));

//            // checks if save file is not corrupted. I use combination of two chars to identify TraderSim save files.
//            if (header != "TS")
//                throw new FileLoadException();
//        }       

//        #endregion

//        #region PlayerData

//        /// <summary>
//        /// Reads PlayerData from custom save file. The steps should be repeated as in writing method.
//        /// </summary>
//        /// <param name="reader"></param>
//        PlayerData ReadPlayerData(BinaryReader reader)
//        {
//            List<Stat> statsCollection = new List<Stat>();
//            int statsCount = reader.ReadInt32();

//            // read stats differently depending on the type
//            for (int i = 0; i < statsCount; i++)
//            {
//                string typeName = reader.ReadString();
//                float statValue = reader.ReadSingle();

//                // get stat type and base type using reflection
//                Assembly mainAssembly = typeof(PlayerData).Assembly;

//                var type = mainAssembly.GetType(typeName);
//                var baseType = type.BaseType;

//                if (baseType == typeof(TimeDependentStat))
//                {
//                    TimeDependentStat timeDependendStat =
//                        new TimeDependentStat(0f, 0f, float.MaxValue, 0f, TimeDependency.Increasing);

//                    float minValue = reader.ReadSingle();
//                    float maxValue = reader.ReadSingle();
//                    float ratePerHour = reader.ReadSingle();
//                    TimeDependency timeDependency =
//                        (TimeDependency) Enum.Parse(typeof(TimeDependency), reader.ReadString());

//                    if (type == typeof(Focus))
//                    {
//                        timeDependendStat = new Focus(statValue, minValue, maxValue, ratePerHour, timeDependency);
//                    }
//                    else if (type == typeof(Stress))
//                    {
//                        timeDependendStat = new Stress(statValue, minValue, maxValue, ratePerHour, timeDependency);
//                    }
//                    else if (type == typeof(Energy))
//                    {
//                        timeDependendStat = new Energy(statValue, minValue, maxValue, ratePerHour, timeDependency);
//                    }

//                    statsCollection.Add(timeDependendStat);
//                }
//                else if (baseType == typeof(MutableStat))
//                {
//                    var mutableStat = new MutableStat(0f, 0f, float.MaxValue);

//                    float minValue = reader.ReadSingle();
//                    float maxValue = reader.ReadSingle(); // TODO I think that FLOAT.MAXVALUE can cause problems 

//                    if (type == typeof(Money))
//                    {
//                        mutableStat = new Money(statValue, minValue, maxValue);
//                    }
//                    else if (type == typeof(Exp))
//                    {
//                        mutableStat = new Exp(statValue, minValue, maxValue);
//                    }

//                    statsCollection.Add(mutableStat);
//                }
//                else if (baseType == typeof(ImmutableStat))
//                {
//                    var immutableStat = new ImmutableStat(0f);

//                    if (type == typeof(Stamina))
//                    {
//                        immutableStat = new Stamina(statValue);
//                    }
//                    else if (type == typeof(Charizma))
//                    {
//                        immutableStat = new Charizma(statValue);
//                    }
//                    else if (type == typeof(Inteligence))
//                    {
//                        immutableStat = new Inteligence(statValue);
//                    }

//                    statsCollection.Add(immutableStat);
//                }
//            }

//            var traderStats = new TraderStats(statsCollection);

//            float playerXPos = reader.ReadSingle();
//            float playerYPos = reader.ReadSingle();
//            float playerZPos = reader.ReadSingle();

//            return new PlayerData
//            {
//                XPos = playerXPos,
//                YPos = playerYPos,
//                ZPos = playerZPos,
//                PlayerStats = traderStats.StatsList
//            };
//        }

//        /// <summary>
//        /// Writes PlayerData to custom save file. Steps to deserialize should be repeated in reading method.
//        /// </summary>
//        /// <param name="writer"></param>
//        void WritePlayerData(BinaryWriter writer, PlayerData playerData)
//        {
//            // player data saving
//            writer.Write(playerData.PlayerStats.Count);

//            foreach (var stat in playerData.PlayerStats)
//            {
//                // writes actual name of stat (ImmutableStat, MutableStat, TimeDependendStat). Use reflection to get type.
//                writer.Write(stat.GetType().FullName);

//                // writes stat base value which all stat types have in common
//                writer.Write(stat.Value);

//                // writes different properties depending on stat type
//                if (stat is TimeDependentStat)
//                {
//                    writer.Write(((TimeDependentStat)stat).MinValue);
//                    writer.Write(((TimeDependentStat)stat).MaxValue);
//                    writer.Write(((TimeDependentStat)stat).PerHourRate);
//                    writer.Write(((TimeDependentStat)stat).TimeDependency.ToString());
//                }
//                else if (stat is MutableStat)
//                {
//                    writer.Write(((MutableStat)stat).MinValue);
//                    writer.Write(((MutableStat)stat).MaxValue);
//                }
//            }

//            writer.Write(playerData.XPos);
//            writer.Write(playerData.YPos);
//            writer.Write(playerData.ZPos);
//        }

//        #endregion      
//    }
//}