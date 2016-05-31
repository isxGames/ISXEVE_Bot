using EVE.ISXEVE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISXEVE_Bot_Framework.Functions
{
    public class d_Entities
    {
        /* Reference Main.cs */
        Main _Main;

        /* Variable Declaration */
        #region Variables
        #endregion

        public d_Entities(Main m)
        {
            _Main = m;
        }

        public String GetEntityMode(Entity e)
        {
            int Mode = e.Mode;
            if (Mode == 0) /* Aligned */
            {
                return "Aligned";
            }
            else if (Mode == 1) /* Approaching */
            {
                return "Approaching";
            }
            else if(Mode == 2) /* Stopped */
            {
                return "Stopped";
            }
            else if(Mode == 3) /* Warping (In Warp) */
            {
                return "Warping";
            }
            else if(Mode == 4) /* Orbiting */
            {
                return "Orbiting";
            }
            else
            {
                return "NULL";
            }
        }

        public List<Entity> FetchStations()
        {
            _Main.LogDebugMessage("d_Entities::FetchStations()");
            return _Main._Eve.QueryEntities("GroupID = 15");
        }

        public List<Entity> FetchAsteroidBelts()
        {
            _Main.LogDebugMessage("d_Entities::FetchAsteroidBelts()");
            return _Main._Eve.QueryEntities("GroupID = 9");
        }

        public List<Entity> FetchAsteroids()
        {
            List<Entity> Asteroids = new List<Entity>();
            List<Entity> Entities = _Main._Eve.QueryEntities("CategoryID = 25");
            foreach (Entity e in Entities)
            {
                Asteroids.Add(e);
            }
            _Main.LogDebugMessage("d_Entities::FetchAsteroids()");
            return Asteroids;
        }

        public double DistanceFromPlayerToEntity(Entity e)
        {
            return _Main._Eve.DistanceBetween(_Main._Me.ToEntity.ID, e.ID);
        }
    }
}
