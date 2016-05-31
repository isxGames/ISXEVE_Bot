using EVE.ISXEVE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISXEVE_Bot_Framework.Functions
{
    public class d_EVECommands
    {
        /* Reference Main.cs */
        Main _Main;

        /* Variable Declaration */
        #region Variables
        #endregion

        public d_EVECommands(Main m)
        {
            _Main = m;
        }

        public void ExitStation()
        {
            _Main._Eve.Execute(ExecuteCommand.CmdExitStation);
            _Main.LogMessage("d_EVECommands::ExitStation()::" + _Main._Station.Name.ToString());
        }

        public void StopShip()
        {
            _Main._Eve.Execute(ExecuteCommand.CmdStopShip);
            _Main.LogMessage("d_EVECommands::StopShip()");
        }
    }
}
