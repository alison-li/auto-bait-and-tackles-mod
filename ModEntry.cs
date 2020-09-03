using System;
using System.Collections.Generic;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Tools;
using Object = StardewValley.Object;

namespace AutoBaitAndTackles
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        private AutoBaitAndTacklesConfig config;

        /*********
        ** Public methods
        *********/

        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            this.config = helper.ReadConfig<AutoBaitAndTacklesConfig>();
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
        }

        /*********
        ** Private methods
        *********/

        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button == this.config.ActivationKey)
            {
                this.AttachBaitAndTackles();
            }
        }

        private void AttachBaitAndTackles()
        {
            if (Game1.player.CurrentTool is FishingRod rod)
            {
                IList<Item> items = Game1.player.Items;

                // Check the bait slot.
                if (rod.attachments[0] == null)
                {
                    foreach (Item item in items)
                    {
                        // Category value for bait is -21.
                        // Source: https://github.com/veywrn/StardewValley/blob/master/StardewValley/Item.cs
                        if (item != null && item.Category == -21)
                        {
                            rod.attachments[0] = (Object)item;
                            Game1.player.removeItemFromInventory(item);
                            Game1.showGlobalMessage($"{item.Name} has been automatically attached");
                            break;
                        }
                    }
                }

                // Check the tackle slot.
                if (rod.attachments[1] == null)
                {
                    foreach (Item item in items)
                    {
                        // Category value for tackle is -22.
                        // Source: https://github.com/veywrn/StardewValley/blob/master/StardewValley/Item.cs
                        if (item != null && item.Category == -22)
                        {
                            rod.attachments[1] = (Object)item;
                            Game1.player.removeItemFromInventory(item);
                            Game1.showGlobalMessage($"{item.Name} has been automatically attached");
                            break;
                        }
                    }
                }
            }
        }
    }
}