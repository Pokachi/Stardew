using StardewValley;
using StardewValley.Buffs;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class Speedy : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.Speedy";
        internal Speedy(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffSpeedy_Name(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 35,
                  duration: duration,
                  effects: new BuffEffects()
                  {
                      Speed = { 1 }
                  },
                  message: I18n.BuffSpeedy_Message()

            )
        { }
    }
}
