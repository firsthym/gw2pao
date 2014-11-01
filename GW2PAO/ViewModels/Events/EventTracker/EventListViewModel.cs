﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using GW2PAO.API.Data;
using GW2PAO.Controllers.Interfaces;
using GW2PAO.Data;
using GW2PAO.Data.UserData;
using GW2PAO.PresentationCore;
using NLog;

namespace GW2PAO.ViewModels.Events.EventTracker
{
    /// <summary>
    /// Primatry Event Tracker view model class
    /// </summary>
    public class EventTrackerViewModel : NotifyPropertyChangedBase
    {
        /// <summary>
        /// Default logger
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The Event Tracker controller
        /// </summary>
        private IEventsController controller;

        /// <summary>
        /// Collection of all World Events
        /// </summary>
        public ObservableCollection<EventViewModel> WorldEvents { get { return this.controller.WorldEvents; } }

        /// <summary>
        /// Command to reset all hidden events
        /// </summary>
        public DelegateCommand ResetHiddenEventsCommand { get { return new DelegateCommand(this.ResetHiddenEvents); } }

        /// <summary>
        /// Event Tracker user data
        /// </summary>
        public EventsUserData UserData { get { return this.controller.UserData; } }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="eventTrackerController">The event tracker controller</param>
        public EventTrackerViewModel(IEventsController eventTrackerController)
        {
            this.controller = eventTrackerController;
        }

        /// <summary>
        /// Resets all hidden events
        /// </summary>
        private void ResetHiddenEvents()
        {
            logger.Debug("Resetting hidden events");
            this.UserData.HiddenEvents.Clear();
        }
    }
}