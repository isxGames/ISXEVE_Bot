namespace ISXEVE_Bot_Framework
{
    using global::ISXEVE_Bot_Framework.Behaviours;
    using global::ISXEVE_Bot_Framework.Functions;
    using global::ISXEVE_Bot_Framework.Modules;
    using global::ISXEVE_Bot_Framework.Routines;
    using LavishScriptAPI;
    using LavishVMAPI;
    using System;

    public class Main
    {
        /* Class References */
        public Form1 _Form1;

        /* Behaviour References */
        public b_Mining b_Mining;

        /* Function References */
        public d_Entities d_Entities;
        public d_EVECommands d_EVECommands;
        public d_Inventory d_Inventory;
        public d_Modules d_Modules;
        public d_Station d_Station;
        public d_Target d_Target;
        public d_UI d_UI;
        public d_WarpTo d_WarpTo;

        /* Module References */
        public m_Cache m_Cache;
        public m_RoutineController m_RoutineController;

        /* Routine References */
        public r_Station r_Station;
        public r_ReturnToStation r_ReturnToStation;
        public r_TravelToBelt r_TravelToBelt;
        public r_IdleAtBelt r_IdleAtBelt;

        /* EventHandlers for Pulse */
        event EventHandler<LSEventArgs> Frame;

        /* ISXEVE References */
        public EVE.ISXEVE.EVE _Eve;
		public EVE.ISXEVE.Me _Me;
        public EVE.ISXEVE.Ship _Ship;
        public EVE.ISXEVE.Station _Station;

        /* Other References */

        #region Pulse Variables
        private DateTime NextPulse;
        private int PulseRate = 1;
        #endregion

        public Main(Form1 f)
        {
            _Form1 = f;

            /* Attach Pulse to our Frame event handler */
            Frame += new EventHandler<LSEventArgs>(Pulse);
        }

        ~Main()
        {
            /* Detach our Frame event handler from the OnFrame event */
            DetachEvent();
            /* Detach Pulse from our Frame event handler */
            Frame -= new EventHandler<LSEventArgs>(Pulse);
        }

        /* Attach to start the bot */
        public void Start()
        {
            /* Load Behaviours, Functions, Modules and Routines */
            LoadBehaviours();
            LoadFunctions();
            LoadModules();
            LoadRoutines();

            AttachEvent();
            NextPulse = DateTime.Now.AddSeconds(PulseRate);
        }

        /* Attach our frame event handler to the OnFrame event */
        internal void AttachEvent()
        {
            LavishScript.Events.AttachEventTarget("ISXEVE_OnFrame", Frame);
            _Form1.NewLogMessage("AttachEvent(): Attaching Frame to ISXEVE_OnFrame");
        }

        /* Overload for AttachEvent */
        internal void AttachEvent(object sender, EventArgs e)
        {
            AttachEvent();
        }

        /* Detach our Frame event handler from the OnFrame event */
        internal void DetachEvent()
        {
			LavishScript.Events.DetachEventTarget("ISXEVE_OnFrame", Frame);
            _Form1.NewLogMessage("DetachEvent(): Detaching Frame from ISXEVE_OnFrame");
        }

        /* Load Behaviours */
        internal void LoadBehaviours()
        {
            b_Mining = new b_Mining(this);
            LogMessage("Main::LoadBehaviours()");
        }
        /* Load Functions */
        internal void LoadFunctions()
        {
            d_Entities = new d_Entities(this);
            d_EVECommands = new d_EVECommands(this);
            d_Inventory = new d_Inventory(this);
            d_Modules = new d_Modules(this);
            d_Station = new d_Station(this);
            d_Target = new d_Target(this);
            d_UI = new d_UI(this);
            d_WarpTo = new d_WarpTo(this);
            LogMessage("Main::LoadFunctions()");
        }
        /* Load Modules */
        internal void LoadModules()
        {
            m_Cache = new m_Cache(this);
            m_RoutineController = new m_RoutineController(this);
            LogMessage("Main::LoadModules()");
        }
        /* Load Routines */
        internal void LoadRoutines()
        {
            r_IdleAtBelt = new r_IdleAtBelt(this);
            r_Station = new r_Station(this);
            r_ReturnToStation = new r_ReturnToStation(this);
            r_TravelToBelt = new r_TravelToBelt(this);
            LogMessage("Main::LoadRoutines()");
        }

        /* The method that will execute OnFrame */
        private void Pulse(object sender, LSEventArgs e)
        {
            if(DateTime.Now > NextPulse)
            {
                NextPulse = DateTime.Now.AddSeconds(PulseRate);
                /* We have to use a FrameLock to do anything, even OnFrame */
                using (new FrameLock(true))
                {
                    /* ISXEVE Reference Declaration */
                    #region ISXEVE References
                    _Eve = new EVE.ISXEVE.EVE();
                    _Me = new EVE.ISXEVE.Me();
                    _Ship = new EVE.ISXEVE.Ship();
                    if (d_Station.InStation()) _Station = new EVE.ISXEVE.Station();
                    #endregion

                    /* Set UI Form Title */
                    #region SetFormTitle
                    _Form1.Text = "EVEMINER - " + _Me.Name + " [" + m_RoutineController.Routine + "]";
                    #endregion

                    b_Mining.Pulse();
                }
            return;
            }
        }

        /* Log Message Handling */
        public void LogMessage(string message)
        {
            _Form1.NewLogMessage(message);
        }
        public void LogDebugMessage(string message)
        {
            _Form1.NewLogMessage("DEBUG: " + message);
        }
    }
}
