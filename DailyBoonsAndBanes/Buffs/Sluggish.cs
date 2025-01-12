using StardewValley;
using StardewValley.Buffs;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class Sluggish : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.Sluggish";
        internal Sluggish(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffSluggish_Name(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 39,
                  duration: duration,
                  effects: new BuffEffects()
                  {
                      Speed = { -1 }
                  },
                  message: I18n.BuffSluggish_Message()

            )
        { }
    }
}
