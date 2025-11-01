using BepInEx;
using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;


namespace Johnathan
{
    [BepInDependency(ItemAPI.PluginGUID)]

    
    [BepInDependency(LanguageAPI.PluginGUID)]

    
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]

    public class Johnathan : BaseUnityPlugin
    {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "Dextzah";
        public const string PluginName = "JohnathanR";
        public const string PluginVersion = "1.0.0";

        //The item string key of johns item
        //WarBanner
        public const string JohnathansItem = "WardOnLevel";

        // The Awake() method is run at the very start when the game is initialized.
        public void Awake()
        {
            // Init our logging class so that we can properly log for debugging.
            Log.Init(Logger);
        }


        private void JohnVerification(CharacterMaster player) {
            var playerBody = player.GetBodyObject();

            var johnsCount = player.inventory.GetItemCount(ItemCatalog.FindItemIndex(JohnathansItem));

            //if johns count is larger than 1, 
            //you die
            if (johnsCount > 0)
            {

                Log.Info($"A Player has Johnathans item");
                //remove the item,
                player.inventory.RemoveItem(ItemCatalog.FindItemIndex(JohnathansItem));


                var playerBodyComponent = playerBody.GetComponent<CharacterBody>();

               

                var playerHealthComponent = playerBodyComponent.healthComponent;

                //Kill them.
                playerHealthComponent.Suicide();

            }

        }

        // The Update() method is run on every frame of the game.
        private void Update()
        {

            var players = PlayerCharacterMasterController.instances;

            //Preform different operations for 1 player vs 2, each player needs to be searched vs tons
            //If there is only 1 player, then only do this for the main player
            if (players.Count == 1)
            {
                //Solo Player
                var player = players[0].master;

                JohnVerification(player);
            }
            else if (players.Count > 1)
            {
                //Multiplayer
                
                //For each player, preform the check.
                foreach (PlayerCharacterMasterController player in players)
                {
                    JohnVerification(player.master);
                }

            }


            
        }
    }
}
