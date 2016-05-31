using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISXEVE_Bot_Framework.Routines
{
    public class r_ReturnToStation
    {
        /* Reference Main.cs */
        Main _Main;

        /* Variable Declaration */
        #region Variables

        #endregion

        public r_ReturnToStation(Main m)
        {
            _Main = m;
        }

        public void Pulse()
        {
            /* Call once at routine start. */
            if (!_Main.m_RoutineController.r_ReturnToStation_Running && !_Main.d_Station.InStation()) Init();
            /* Confirm we're in space before we run routine. */
            if (!_Main.d_Station.InStation()) Run();
        }

        public void Init()
        {
            _Main.LogDebugMessage("r_ReturnToStation::Init()");

            /* Let m_RoutineController know that routine is running */
            _Main.m_RoutineController.r_ReturnToStation_Running = true;
            _Main.m_RoutineController.Routine = "r_ReturnToStation";

            /* Warp to Station */
            _Main.d_WarpTo.Station(_Main._Form1.GetDeliveryStation());
        }

        public void Run()
        {
            /* Nothing to do here */
        }

        public void Exit()
        {
            /* Let m_RoutineController know that routine is not running */
            _Main.m_RoutineController.r_ReturnToStation_Running = false;
        }
    }
}
