using StardewValley;
using StardewValley.Buffs;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class NatureTouched : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.NatureTouched";
        internal NatureTouched(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffNaturetouched_Name(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 35,
                  duration: duration,
                  effects: new BuffEffects()
                  {
                      FarmingLevel = { 1 },
                  },
                  message: I18n.BuffNaturetouched_Message()

            )
        { }
    }
}
