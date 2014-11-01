﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GW2PAO.PresentationCore;
using NLog;

namespace GW2PAO.Data.UserData
{
    /// <summary>
    /// User data for the Dungeons Tracker
    /// </summary>
    [Serializable]
    public class DungeonUserData : UserData<DungeonUserData>
    {
        /// <summary>
        /// Default logger
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The default settings filename
        /// </summary>
        public const string Filename = "DungeonsUserData.xml";

        private DateTime lastResetDateTime;
        private ObservableCollection<Guid> hiddenDungeons = new ObservableCollection<Guid>();
        private ObservableCollection<Guid> completedPaths = new ObservableCollection<Guid>();

        /// <summary>
        /// The last recorded server-reset date/time
        /// </summary>
        public DateTime LastResetDateTime
        {
            get { return this.lastResetDateTime; }
            set { SetField(ref this.lastResetDateTime, value); }
        }

        /// <summary>
        /// Collection of user-configured Hidden Dungeons
        /// </summary>
        public ObservableCollection<Guid> HiddenDungeons { get { return this.hiddenDungeons; } }

        /// <summary>
        /// Collection of user-configured completed dungeon paths
        /// </summary>
        public ObservableCollection<Guid> CompletedPaths { get { return this.completedPaths; } }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DungeonUserData()
        {
            this.LastResetDateTime = DateTime.UtcNow;
        }

        /// <summary>
        /// Enables auto-save of settings. If called, whenever a setting is changed, this settings object will be saved to disk
        /// </summary>
        public override void EnableAutoSave()
        {
            logger.Info("Enabling auto save");
            this.PropertyChanged += (o, e) => DungeonUserData.SaveData(this, DungeonUserData.Filename);
            this.HiddenDungeons.CollectionChanged += (o, e) => DungeonUserData.SaveData(this, DungeonUserData.Filename);
            this.CompletedPaths.CollectionChanged += (o, e) => DungeonUserData.SaveData(this, DungeonUserData.Filename);
        }
    }
}