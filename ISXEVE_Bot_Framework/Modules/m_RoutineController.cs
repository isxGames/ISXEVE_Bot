using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISXEVE_Bot_Framework.Modules
{
    public class m_RoutineController
    {
        /* Reference Main.cs */
        Main _Main;

        /* Variable Declaration */
        #region Variables
        #endregion

        public m_RoutineController(Main m)
        {
            _Main = m;
        }

        /* Routine Status Variables */
        #region Variables
        public string Routine = "";
        public bool r_IdleAtBelt_Running = false;
        public bool r_Station_Running = false;
        public bool r_ReturnToStation_Running = false;
        public bool r_TravelToBelt_Running = false;
        #endregion
    }
}
