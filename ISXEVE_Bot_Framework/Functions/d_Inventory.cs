using EVE.ISXEVE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISXEVE_Bot_Framework.Functions
{
    public class d_Inventory
    {
        /* Reference Main.cs */
        Main _Main;

        /* Variable Declaration */
        #region Variables
        public bool HasOreHold = false;
        public double OreHoldCapacity;
        public double OreHoldUsedCapacity;
        public int OreHoldUsedPercentage;

        /* What percentage of maximum capacity should we transfer to station at? */
        public int OreHoldFullThreshold = 90;
        #endregion

        public d_Inventory(Main m)
        {
            _Main = m;
        }

        public void FetchOreHoldCapacity()
        {
            _Main.LogMessage("d_Inventory::FetchOreHoldCapacity()");
            IEveInvWindow InvWindow = EVE.ISXEVE.EVEWindow.GetInventoryWindow();
            IEveInvChildWindow ShipOreHold = InvWindow.GetChildWindow("ShipOreHold");
            ShipOreHold.MakeActive();
            OreHoldCapacity = InvWindow.ActiveChild.Capacity;
            OreHoldUsedCapacity = InvWindow.ActiveChild.UsedCapacity;
            if (OreHoldUsedCapacity > 0)
            {
                OreHoldUsedPercentage = ((int)OreHoldUsedCapacity / ((int)OreHoldCapacity / 100));
            }
            else if (OreHoldUsedCapacity == 0) OreHoldUsedPercentage = 0;
            _Main._Form1.SetOreHoldStatusLabel(OreHoldUsedCapacity.ToString(), OreHoldCapacity.ToString(), OreHoldUsedPercentage.ToString() + "%");
        }

        public void TransferOreHoldToStation()
        {
            List<IItem> OreHoldItems = _Main._Ship.GetOreHoldCargo();
            foreach (IItem OreHoldItem in OreHoldItems)
            {
                OreHoldItem.MoveTo("MyStationHangar", "Hangar");
                _Main.LogMessage("d_Inventory::TransferOreHoldToStation()");
            }
        }

        public bool RequireDropOff()
        {
            /* Do we need to transfer ore to station? */
            _Main.d_Inventory.FetchOreHoldCapacity();
            if (_Main.d_Inventory.OreHoldUsedPercentage >= OreHoldFullThreshold)
            {
                _Main.LogMessage("d_Inventory::RequireDropOff()::TRUE");
                return true;
            }
            else
            {
                _Main.LogMessage("d_Inventory::RequireDropOff()::FALSE");
                return false;
            }
        }
    }
}
