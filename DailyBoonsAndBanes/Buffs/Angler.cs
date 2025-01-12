using StardewValley;
using StardewValley.Buffs;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class Angler : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.Angler";
        internal Angler(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffAngler_Name(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 35,
                  duration: duration,
                  effects: new BuffEffects()
                  {
                      FishingLevel = { 1 },
                  },
                  message: I18n.BuffAngler_Message()

            )
        { }
    }
}
