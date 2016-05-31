using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISXEVE_Bot_Framework.Behaviours
{
    public class b_Mining
    {
        /* Reference Main.cs */
        Main _Main;

        /* Variable Declaration */
        #region Variables
        #endregion

        public b_Mining(Main m)
        {
            _Main = m;
        }

        public void Pulse()
        {
            /* Called once on behaviour start */
            if (_Main.m_RoutineController.Routine == "")
            {
                Init();
                return;
            }
            /* Call Station Routine */
            else if (_Main.d_Station.InStation())
            {
                _Main.r_Station.Pulse();
                return;
            }
            /* Called once if we just left a station*/
            else if (!_Main.d_Station.InStation() && _Main.m_RoutineController.Routine == "r_Station")
            {
                RecentlyLeftStation();
                return;
            }
            /* Called once if we're in space and idle */
            else if (!_Main.d_Station.InStation() && _Main.m_RoutineController.Routine == "r_Idle")
            {
                /* Do we need to drop off cargo? */
                if (_Main.d_Inventory.RequireDropOff()) _Main.r_ReturnToStation.Pulse();
                else
                {
                    /* Call TravelToBelt Routine */
                    _Main.r_TravelToBelt.Pulse();
                }
                return;
            }
            else if (!_Main.d_Station.InStation() && _Main.m_RoutineController.Routine == "r_IdleAtBelt")
            {
                /* Call ArrivedAtBelt Routine*/
                _Main.r_IdleAtBelt.Pulse();
                return;
            }
        }

        /* Behaviour Init */
        public void Init()
        {
            _Main.d_Modules.FetchAll();
            _Main.d_Modules.FetchMiningLasers();
            _Main.m_RoutineController.Routine = "r_Idle";
        }

        /* We've just left a station */
        public void RecentlyLeftStation()
        {
            _Main.m_RoutineController.Routine = "r_Idle";
            _Main.d_UI.UpdateStations();
        }
    }
}
