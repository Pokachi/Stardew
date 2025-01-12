using StardewValley;
using StardewValley.Buffs;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class Bountiful : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.Bountiful";
        internal Bountiful(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffBountiful_Name(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 35,
                  duration: duration,
                  effects: new BuffEffects()
                  {
                      ForagingLevel = { 1 },
                  },
                  message: I18n.BuffBountiful_Message()

            )
        { }
    }
}
