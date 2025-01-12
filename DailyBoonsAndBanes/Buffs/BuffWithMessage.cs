using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Buffs;

namespace DailyBoonsAndBanes.Buffs
{
    internal class BuffWithMessage : Buff
    {
        internal string message;

        internal BuffWithMessage(
            string message,
            string id,
            string? source = null,
            string? displaySource = null,
            string? description = null,
            int duration = -1,
            Texture2D? iconTexture = null,
            int iconSheetIndex = -1,
            BuffEffects? effects = null,
            bool isDebuff = false,
            string? displayName = null)
            : base(
                  id,
                  source,
                  displaySource,
                  duration,
                  iconTexture,
                  iconSheetIndex,
                  effects,
                  isDebuff,
                  displayName,
                  description)
        {
            this.message = message;
        }
        
    }
}
