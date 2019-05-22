using System;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using SDG.Unturned;

namespace ZaupClearInventoryLib
{
    public class ZaupClearInventoryLib : RocketPlugin<ZaupClearInventoryLibConfiguration>
    {
        public static ZaupClearInventoryLib Instance;

        protected override void Load()
        {
            ZaupClearInventoryLib.Instance = this;

            if (Configuration.Instance.DeleteInventoryOnDeath)
            {
                Rocket.Unturned.Events.UnturnedPlayerEvents.OnPlayerDeath += UnturnedPlayerEvents_OnPlayerDeath;
            }
        }

        protected override void Unload()
        {
            if (Configuration.Instance.DeleteInventoryOnDeath)
            {
                Rocket.Unturned.Events.UnturnedPlayerEvents.OnPlayerDeath -= UnturnedPlayerEvents_OnPlayerDeath;
            }
        }

        void UnturnedPlayerEvents_OnPlayerDeath(Rocket.Unturned.Player.UnturnedPlayer player, SDG.Unturned.EDeathCause cause, SDG.Unturned.ELimb limb, Steamworks.CSteamID murderer)
        {
            ZaupClearInventoryLib.Instance.ClearInv(player);
            ZaupClearInventoryLib.Instance.ClearClothes(player);
        }

        public bool ClearInv(UnturnedPlayer player)
        {
            bool returnv = false;
            try
            {
                player.Player.equipment.dequip();
                for (byte p = 0; p < (PlayerInventory.PAGES -1); p++)
                {
                    byte itemc = player.Player.inventory.getItemCount(p);
                    if (itemc > 0)
                    {
                        for (byte p1 = 0; p1 < itemc; p1++)
                        {
                            player.Player.inventory.removeItem(p, 0);
                        }
                    }
                }
                player.Player.channel.send("tellSlot", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
		        {
			        (byte)0,
			        (byte)0,
			        new byte[0]
		        });
                player.Player.channel.send("tellSlot", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
		        {
			        (byte)1,
			        (byte)0,
			        new byte[0]
		        });
                returnv = true;
            }
            catch (Exception e)
            {
                Logger.Log("There was an error clearing " + player.CharacterName + "'s inventory.  Here is the error.");
                Console.Write(e);
            }
            return returnv;
        }

        public bool ClearClothes(UnturnedPlayer player)
        {
            bool returnv = false;
            try
            {
                player.Player.clothing.askWearBackpack(0, 0, new byte[0],true);
                for (byte p2 = 0; p2 < player.Player.inventory.getItemCount(2); p2++)
                {
                    player.Player.inventory.removeItem(2, 0);
                }
                player.Player.clothing.askWearGlasses(0, 0, new byte[0], true);
                for (byte p2 = 0; p2 < player.Player.inventory.getItemCount(2); p2++)
                {
                    player.Player.inventory.removeItem(2, 0);
                }
                player.Player.clothing.askWearHat(0, 0, new byte[0], true);
                for (byte p2 = 0; p2 < player.Player.inventory.getItemCount(2); p2++)
                {
                    player.Player.inventory.removeItem(2, 0);
                }
                player.Player.clothing.askWearMask(0, 0, new byte[0], true);
                for (byte p2 = 0; p2 < player.Player.inventory.getItemCount(2); p2++)
                {
                    player.Player.inventory.removeItem(2, 0);
                }
                player.Player.clothing.askWearPants(0, 0, new byte[0], true);
                for (byte p2 = 0; p2 < player.Player.inventory.getItemCount(2); p2++)
                {
                    player.Player.inventory.removeItem(2, 0);
                }
                player.Player.clothing.askWearShirt(0, 0, new byte[0], true);
                for (byte p2 = 0; p2 < player.Player.inventory.getItemCount(2); p2++)
                {
                    player.Player.inventory.removeItem(2, 0);
                }
                player.Player.clothing.askWearVest(0, 0, new byte[0], true);
                for (byte p2 = 0; p2 < player.Player.inventory.getItemCount(2); p2++)
                {
                    player.Player.inventory.removeItem(2, 0);
                }
                returnv = true;
            }
            catch (Exception e)
            {
                Logger.Log("There was an error clearing " + player.CharacterName + "'s inventory.  Here is the error.");
                Console.Write(e);
            }
            return returnv;
        }
    }
}
