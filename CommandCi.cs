using Rocket.API;
using Rocket.Core;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;
using System;

namespace ZaupClearInventoryLib
{
    public class CommandCi : IRocketCommand
    {
        public string Name
        {
            get
            {
                return "ci";
            }
        }
        public string Help
        {
            get
            {
                return "/ci [name/self] [true] will clears your or someone else's inventory and true will remove clothes too.";
            }
        }
        public string Syntax
        {
            get
            {
                return "/ci [name/self] [true]";
            }
        }
        public List<string> Aliases
        {
            get 
            { 
                return new List<string>()
                {
                    "clearinventory",
                    "cleari"
                }; 
            }
        }
        public List<string> Permissions
        {
            get
            {
                return new List<string>()
                {
                    "ci.other",
                    "ci"
                };
            }
        }

        public AllowedCaller AllowedCaller
        {
            get
            {
                return AllowedCaller.Player;
            }
        }

        public void Execute(IRocketPlayer caller, string[] msg)
        {
            UnturnedPlayer playerid = (UnturnedPlayer)caller;
            if (msg.Length > 2)
            {
                UnturnedChat.Say(playerid, "Invalid use of ci.");
                return;
            }
            UnturnedPlayer player = playerid;
            if (msg.Length >= 1)
            {
                if (msg[0].ToLower() != "self")
                {
                    bool hasp = R.Permissions.HasPermission(playerid, "ci.other");
                    if (!hasp && !playerid.IsAdmin)
                    {
                        UnturnedChat.Say(playerid, "You do not have permission to clear someone else's inventory.");
                        return;
                    }
                    player = UnturnedPlayer.FromName(msg[0]);
                }
            }
            bool done = ZaupClearInventoryLib.Instance.ClearInv(player);
            if (msg.Length == 2)
            {
                if (msg[1].ToLower() != "true")
                {
                    UnturnedChat.Say(playerid, "Invalid use of the command.  /ci [name/self] [true]");
                    return;
                }
                done = ZaupClearInventoryLib.Instance.ClearClothes(player);
            }
            if (!done)
            {
                UnturnedChat.Say(playerid, "There was an error.  Please look at your console error log.");
            }
            else
            {
                UnturnedChat.Say(playerid, player.CharacterName + "'s inventory has been cleared.");
            }
        }
    }
}
