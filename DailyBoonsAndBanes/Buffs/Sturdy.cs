using StardewValley;
using StardewValley.Buffs;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class Sturdy : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.Sturdy";
        internal Sturdy(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffSturdy_Name(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 35,
                  duration: duration,
                  effects: new BuffEffects()
                  {
                      Defense = { 1 },
                  },
                  message: I18n.BuffSturdy_Message()

            )
        { }
    }
}
