using EVE.ISXEVE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISXEVE_Bot_Framework.Modules
{
    public class m_Cache
    {
        /* Reference Main.cs */
        Main _Main;

        /* Variable Declaration */
        #region Variables
        public List<Entity> AsteroidBelts = new List<Entity>();
        public SortedDictionary<double, Entity> Asteroids = new SortedDictionary<double, Entity>();
        #endregion

        public m_Cache(Main m)
        {
            _Main = m;
        }

        /* Asteroid Belt Cache */
        public void PopulateAsteroidBeltCache()
        {
            List<Entity> EntityQuery = _Main.d_Entities.FetchAsteroidBelts();
            AsteroidBelts = new List<Entity>();
            foreach(Entity e in EntityQuery)
            {
                AsteroidBelts.Add(e);
            }
            _Main.LogDebugMessage("m_Cache::PopulateAsteroidBeltCache()");
        }
        public void RemoveAsteroidBelt(Entity e)
        {
            AsteroidBelts.Remove(e);
            _Main.LogDebugMessage("m_Cache::DeclareAsteroidBeltEmpty()::" + e.Name);
        }

        /* Individual Asteroid Cache */
        public void PopulateAsteroidCache()
        {
            List<Entity> EntityQuery = _Main.d_Entities.FetchAsteroids();
            Asteroids = new SortedDictionary<double, Entity>();
            foreach (Entity e in EntityQuery)
            {
                double Range = _Main.d_Entities.DistanceFromPlayerToEntity(e);
                Asteroids.Add(Range, e);
            }
            _Main.LogDebugMessage("m_Cache::PopulateAsteroidCache()");
        }
    }
}
