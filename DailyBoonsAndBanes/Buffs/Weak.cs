using StardewValley;
using StardewValley.Buffs;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class Weak : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.Weak";
        internal Weak(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffWeak_Name(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 39,
                  duration: duration,
                  effects: new BuffEffects()
                  {
                      Attack = { -1 }
                  },
                  message: I18n.BuffWeak_Message()

            )
        { }
    }
}
