using StardewValley;
using StardewValley.Buffs;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class Unsteady : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.Unsteady";
        internal Unsteady(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffUnsteady_Name(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 39,
                  duration: duration,
                  effects: new BuffEffects()
                  {
                      Defense = { -1 }
                  },
                  message: I18n.BuffUnsteady_Message()

            )
        { }
    }
}
