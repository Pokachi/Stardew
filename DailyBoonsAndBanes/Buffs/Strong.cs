using StardewValley;
using StardewValley.Buffs;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class Strong : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.Strong";
        internal Strong(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffStrong_Name(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 35,
                  duration: duration,
                  effects: new BuffEffects()
                  {
                      Attack = { 1 },
                  },
                  message: I18n.BuffStrong_Message()

            )
        { }
    }
}
