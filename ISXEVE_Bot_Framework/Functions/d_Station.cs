using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISXEVE_Bot_Framework.Functions
{
    public class d_Station
    {
        /* Reference Main.cs */
        Main _Main;

        /* Variable Declaration */
        #region Variables
        #endregion

        public d_Station(Main m)
        {
            _Main = m;
        }

        public bool InStation()
        {
            return _Main._Me.InStation;
        }
    }
}
