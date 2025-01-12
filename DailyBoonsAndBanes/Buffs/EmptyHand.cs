using StardewValley;
using StardewValley.Buffs;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class EmptyHand : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.EmptyHand";
        internal EmptyHand(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffEmptyHand_Name(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 39,
                  duration: duration,
                  effects: new BuffEffects()
                  {
                      FishingLevel = { -1 }
                  },
                  message: I18n.BuffEmptyHand_Message()

            )
        { }
    }
}
