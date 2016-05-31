using EVE.ISXEVE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISXEVE_Bot_Framework.Functions
{
    public class d_UI
    {
        /* Reference Main.cs */
        Main _Main;

        /* Variable Declaration */
        #region Variables
        #endregion

        public d_UI(Main m)
        {
            _Main = m;
        }

        public void UpdateStations()
        {
            List<String> StationNames = new List<String>();
            List<Entity> Stations = _Main.d_Entities.FetchStations();
            foreach (Entity e in Stations)
            {
                StationNames.Add(e.Name);
            }
            _Main.LogDebugMessage("d_UI::UpdateStations()");
            _Main._Form1.SetDeliveryStationsDropdown(StationNames);
        }
    }
}
