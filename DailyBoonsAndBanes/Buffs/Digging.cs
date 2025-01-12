using StardewValley;
using StardewValley.Buffs;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class Digging : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.Digging";
        internal Digging(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffDigging_Name(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 35,
                  duration: duration,
                  effects: new BuffEffects()
                  {
                      MiningLevel = { 1 },
                  },
                  message: I18n.BuffDigging_Message()

            )
        { }
    }
}
