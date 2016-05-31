using EVE.ISXEVE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISXEVE_Bot_Framework.Functions
{
    public class d_Modules
    {
        /* Reference Main.cs */
        Main _Main;

        /* Variable Declaration */
        #region Variables
        List<IModule> FittedModules = new List<IModule>();
        List<IModule> MiningLasers = new List<IModule>();
        #endregion

        public d_Modules(Main m)
        {
            _Main = m;
        }

        public void FetchAll()
        {
            /* Clear FittedModules list before we update it! */
            FittedModules = new List<IModule>();
            _Main.LogDebugMessage("Cleared List<FittedModules>");

            List<IModule> ModulesFitted = _Main._Ship.GetModules();
            foreach (IModule Module in ModulesFitted)
            {
                FittedModules.Add(Module);
                _Main.LogDebugMessage("d_Modules: Added [" + Module.ToItem.Slot + "] " + Module.ToItem.Name.ToString() + " List<FittedModules>");
            }
        }

        public void FetchMiningLasers()
        {
            /* Clear MiningLasers list before we update it! */
            MiningLasers = new List<IModule>();
            _Main.LogDebugMessage("Cleared List<MiningLasers>");

            List<IModule> ModulesFitted = _Main._Ship.GetModules();
            foreach (IModule Module in ModulesFitted)
            {
                if(Module.MiningAmount > 0)
                {
                    FittedModules.Add(Module);
                    _Main.LogDebugMessage("d_Modules: Added [" + Module.ToItem.Slot + "] " + Module.ToItem.Name.ToString() + " List<MiningLasers>");
                }
            }
        }
    }
}
