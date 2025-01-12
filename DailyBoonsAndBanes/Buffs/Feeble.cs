using StardewValley;
using StardewValley.Buffs;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class Feeble : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.Feeble";
        internal Feeble(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffFeeble_Name(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 15,
                  duration: duration,
                  description: I18n.BuffFeeble_DescInGame(),
                  effects: new BuffEffects()
                  {
                      Attack = { -3 },
                      CriticalChanceMultiplier = { -0.05f },
                      CriticalPowerMultiplier = { -0.05f },
                      WeaponSpeedMultiplier = { -0.05f },
                      WeaponPrecisionMultiplier = { -0.05f },
                      KnockbackMultiplier = { -0.05f }
                  },
                  message: I18n.BuffFeeble_Message()

            )
        { }
    }
}
