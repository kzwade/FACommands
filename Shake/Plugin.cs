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


namespace ShakePlugin.cs


{
    [ApiVersion(2, 1)]


    public class ShakePlugin : TerrariaPlugin
    {

        public ShakePlugin(Main game) : base(game) { }


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
            Commands.ChatCommands.Add(new Command("shakecommand.shake", Shake, "shake"));
          
        }

        private void Shake(CommandArgs args)
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

                args.Player.SendInfoMessage($"You shook hands with {players[0].Name}.");

                TSPlayer.All.SendSuccessMessage($"{args.Player.Name} shook hands with {players[0].Name}!");
            }
        }

    }
}