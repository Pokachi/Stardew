using StardewValley;
using StardewValley.Buffs;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class Unearthly : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.Unearthly";
        internal Unearthly(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffUnearthly_Name(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 39,
                  duration: duration,
                  effects: new BuffEffects()
                  {
                      FarmingLevel = { -1 }
                  },
                  message: I18n.BuffUnearthly_Message()

            )
        { }
    }
}
