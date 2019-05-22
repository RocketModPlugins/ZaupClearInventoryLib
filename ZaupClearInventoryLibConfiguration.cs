using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.API;

namespace ZaupClearInventoryLib
{
    public class ZaupClearInventoryLibConfiguration : IRocketPluginConfiguration
    {
        public bool DeleteInventoryOnDeath;
        public void LoadDefaults()
        {
            DeleteInventoryOnDeath = false;
        }
    }
}
