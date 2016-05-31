using EVE.ISXEVE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISXEVE_Bot_Framework.Routines
{
    public class r_TravelToBelt
    {
        /* Reference Main.cs */
        Main _Main;

        /* Variable Declaration */
        #region Variables

        /* How long should we wait before we travel to belt? */
        public double MinWaitTime = 5;
        public double MaxWaitTime = 30;
        public DateTime WarpTime;

        public Entity AsteroidBeltIndexZero;

        #endregion

        public r_TravelToBelt(Main m)
        {
            _Main = m;
        }

        public void Pulse()
        {
            /* Call once at routine start. */
            if (!_Main.m_RoutineController.r_TravelToBelt_Running && !_Main.d_Station.InStation()) Init();
            /* Confirm we're in station before we run routine. */
            if (!_Main.d_Station.InStation()) Run();
        }

        public void Init()
        {
            _Main.LogDebugMessage("r_TravelToBelt::Init()");

            /* Let m_RoutineController know that routine is running */
            _Main.m_RoutineController.r_TravelToBelt_Running = true;
            _Main.m_RoutineController.Routine = "r_TravelToBelt";

            /* Populate Asteroid Belt Cache */
            _Main.m_Cache.PopulateAsteroidBeltCache();

            AsteroidBeltIndexZero = _Main.m_Cache.AsteroidBelts[0];
            _Main.d_WarpTo.Belt(AsteroidBeltIndexZero);
        }

        public void Run()
        {
            CanWeExitYet();
        }

        public void Exit()
        {
            /* Let m_RoutineController know that routine is not running */
            _Main.m_RoutineController.r_TravelToBelt_Running = false;
            _Main.LogDebugMessage("r_TravelToBelt::Exit()");
            _Main.m_RoutineController.Routine = "r_IdleAtBelt";
        }

        /* Can we leave the routine yet? */
        public void CanWeExitYet()
        {
            String OurStatus = _Main.d_Entities.GetEntityMode(_Main._Me.ToEntity);
            double Distance = _Main.d_Entities.DistanceFromPlayerToEntity(AsteroidBeltIndexZero);
            if (OurStatus == "Warping" && Distance < 5000) return;
            else if (OurStatus != "Warping" && Distance < 5000) Exit();
        }
    }
}
