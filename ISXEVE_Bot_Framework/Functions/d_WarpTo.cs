using EVE.ISXEVE;
using EVE.ISXEVE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISXEVE_Bot_Framework.Functions
{
    public class d_WarpTo
    {
        /* Reference Main.cs */
        Main _Main;

        /* Variable Declaration */
        #region Variables
        #endregion

        public d_WarpTo(Main m)
        {
            _Main = m;
        }

        public void Station(String StationName)
        {
            List<Entity> Entities = _Main.d_Entities.FetchStations();
            foreach (Entity e in Entities)
            {
                if (e.Name == StationName)
                {
                    _Main.LogDebugMessage("d_WarpTo::Warping to " + e.Name);
                    e.WarpToAndDock();
                }
            }
        }
        public void Station(Entity Station)
        {
            Station.WarpToAndDock();
            _Main.LogDebugMessage("d_WarpTo::Warping to " + Station.Name);
        }

        public void Belt(String BeltName)
        {
            List<Entity> Entities = _Main.d_Entities.FetchAsteroidBelts();
            foreach (Entity e in Entities)
            {
                if (e.Name == BeltName)
                {
                    _Main.LogDebugMessage("d_WarpTo::Warping to " + e.Name);
                    e.WarpToAndDock();
                }
            }
        }
        public void Belt(Entity Belt)
        {
            _Main.LogDebugMessage("d_WarpTo::Warping to " + Belt.Name);
            Belt.WarpTo();
        }
    }
}
