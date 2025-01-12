using StardewValley;
using StardewValley.Buffs;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class Aquaphobic : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.Aquaphobic";
        internal Aquaphobic(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffAquaphobic_Name(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 39,
                  duration: duration,
                  effects: new BuffEffects()
                  {
                      FishingLevel = { -1 }
                  },
                  message: I18n.BuffWeak_Message()

            )
        { }
    }
}
