using StardewValley;
using StardewValley.Buffs;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class SparseDesposite : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.SparseDesposite";
        internal SparseDesposite(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffSparsedeposit_Name(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 39,
                  duration: duration,
                  effects: new BuffEffects()
                  {
                      MiningLevel = { -1 }
                  },
                  message: I18n.BuffSparsedeposit_Message()

            )
        { }
    }
}
