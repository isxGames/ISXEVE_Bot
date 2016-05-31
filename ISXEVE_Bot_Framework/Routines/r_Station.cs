using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISXEVE_Bot_Framework.Routines
{
    public class r_Station
    {
        /* Reference Main.cs */
        Main _Main;

        /* Variable Declaration */
        #region Variables

        /* How long should we wait before we undock? */
        public double MinWaitTime = 5;
        public double MaxWaitTime = 30;
        public DateTime ExitTime;

        #endregion

        public r_Station(Main m)
        {
            _Main = m;
        }

        public void Pulse()
        {
            /* Call once at routine start. */
            if (!_Main.m_RoutineController.r_Station_Running && _Main.d_Station.InStation())    Init();
            /* Confirm we're in station before we run routine. */
            if (_Main.d_Station.InStation()) Run();
        }

        public void Init()
        {
            _Main.LogDebugMessage("r_Station::Init()");

            /* Let m_RoutineController know that routine is running */
            _Main.m_RoutineController.r_Station_Running = true;
            _Main.m_RoutineController.Routine = "r_Station";

            /* Do we need to transfer ore to station? */
            if (_Main.d_Inventory.RequireDropOff())
            {
                /* Let's transfer it then! */
                _Main.d_Inventory.TransferOreHoldToStation();
            }

            /* Human Behaviour Emulation */
            Double Delay = RandWaitTime(MinWaitTime, MaxWaitTime);
            ExitTime = DateTime.Now.AddSeconds(Delay);
            _Main.LogDebugMessage("r_Station::ExitTime::" + Delay.ToString() + "s");
        }

        public void Run()
        {
            CanWeExitYet();
        }

        public void Exit()
        {
            /* Let's leave */
            _Main.d_EVECommands.ExitStation();

            /* Let m_RoutineController know that routine is not running */
            _Main.m_RoutineController.r_Station_Running = false;
        }

        /* Random double generator */
        public double RandWaitTime(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        /* Can we leave the routine yet? */
        public void CanWeExitYet()
        {
            if (DateTime.Now > ExitTime) Exit();
        }
    }
}
