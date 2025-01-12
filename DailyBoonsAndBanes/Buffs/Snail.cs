using StardewValley;
using StardewValley.Buffs;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class Snail : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.Snail";
        internal Snail(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffSnail_Name(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 15,
                  duration: duration,
                  effects: new BuffEffects()
                  {
                      Speed = { -3 }
                  },
                  message: I18n.BuffSnail_Message()

            )
        { }
    }
}
