using StardewValley;
using StardewValley.Buffs;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class BlazingFast : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.BlazingFast";
        internal BlazingFast(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffBlazingfast_Name(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 59,
                  duration: duration,
                  effects: new BuffEffects()
                  {
                      Speed = { 3 }
                  },
                  message: I18n.BuffBlazingfast_Message()

            )
        { }
    }
}
