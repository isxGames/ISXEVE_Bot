using EVE.ISXEVE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISXEVE_Bot_Framework.Routines
{
    public class r_IdleAtBelt
    {
        /* Reference Main.cs */
        Main _Main;

        /* Variable Declaration */
        #region Variables
        #endregion

        public r_IdleAtBelt(Main m)
        {
            _Main = m;
        }

        public void Pulse()
        {
            /* Call once at routine start. */
            if (!_Main.m_RoutineController.r_IdleAtBelt_Running && !_Main.d_Station.InStation()) Init();
            /* Confirm we're in station before we run routine. */
            if (!_Main.d_Station.InStation()) Run();
        }

        public void Init()
        {
            _Main.LogDebugMessage("r_IdleAtBelt::Init()");

            /* Let m_RoutineController know that routine is running */
            _Main.m_RoutineController.r_TravelToBelt_Running = true;
            _Main.m_RoutineController.Routine = "r_IdleAtBelt";

            /* Populate Individual Asteroid Cache */
            _Main.m_Cache.PopulateAsteroidCache();

            /* How many Asteroids in range? */
            _Main.LogDebugMessage("r_IdleAtBelt::AsteroidsInRange::" + AsteroidsInRange().ToString());
            if (AsteroidsInRange() < 4)
            {
                /* Approach and refresh Asteroid dictionary */
                Entity NearestAsteroid = _Main.m_Cache.Asteroids[0];
                NearestAsteroid.Approach();
            }
        }

        public void Run()
        {
            CanWeExitYet();
        }

        public void Exit()
        {
            /* Let m_RoutineController know that routine is not running */
            _Main.m_RoutineController.r_IdleAtBelt_Running = false;
            _Main.LogDebugMessage("r_IdleAtBelt::Exit()");
            _Main.m_RoutineController.Routine = "r_Mining";
        }

        /* Can we leave the routine yet? */
        public void CanWeExitYet()
        {
            /* How many Asteroids in range? */
            if (AsteroidsInRange() >= 4)
            {
                if (_Main.d_Entities.GetEntityMode(_Main._Me.ToEntity) == "Approaching") _Main.d_EVECommands.StopShip();
                Exit();
            }
        }

        public int AsteroidsInRange()
        {
            /* Refresh Asteroid Cache */
            _Main.m_Cache.PopulateAsteroidCache();
            int InRange = 0;
            foreach (KeyValuePair<double, Entity> Asteroid in _Main.m_Cache.Asteroids)
            {
                if (Asteroid.Key < 15000) InRange++;
            }
            return InRange;
        }
    }
}
