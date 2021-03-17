using System;
using TShockAPI;
using TerrariaApi.Server;
using Terraria;
using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Collections.Generic;
using Terraria.DataStructures;
using TShockAPI.Hooks;


namespace ShrugPlugin.cs


{
    [ApiVersion(2, 1)]



    public class ShrugPlugin : TerrariaPlugin
    {

        public ShrugPlugin(Main game) : base(game) { }


        public override void Initialize()
        {
            ServerApi.Hooks.GameInitialize.Register(this, OnInitialize);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.GameInitialize.Deregister(this, OnInitialize);
            }
            base.Dispose(disposing);
        }

        private void OnInitialize(EventArgs args)
        {
            Commands.ChatCommands.Add(new Command("shrugcommand.shrug", Shrug, "shrug"));
            
        }

        private void Shrug(CommandArgs args)
        {
            string playerName = args.Parameters[0];

            var players = TSPlayer.FindByNameOrID(playerName);

            if (players.Count > 1)
            {
                args.Player.SendErrorMessage("Invalid player!");
            }

            if (players.Count > 1)
            {
                args.Player.SendMultipleMatchError(players.Select(p => p.Name));
            }

            else
            {

                TSPlayer.All.SendSuccessMessage($"{args.Player.Name} Shrugged.");
            }
        }

    }
}