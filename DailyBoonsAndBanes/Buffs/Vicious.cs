using StardewValley;
using StardewValley.Buffs;
using StardewValley.Monsters;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class Vicious : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.Vicious";
        internal Vicious(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffVicious_Name(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 59,
                  description: I18n.BuffVicious_DescInGame(),
                  duration: duration,
                  effects: new BuffEffects()
                  {
                      Attack = { 3 },
                      CriticalChanceMultiplier = { 0.05f },
                      CriticalPowerMultiplier = { 0.05f },
                      WeaponSpeedMultiplier = { 0.05f },
                      WeaponPrecisionMultiplier = { 0.05f },
                      KnockbackMultiplier = { 0.05f }
                  },
                  message: I18n.BuffVicious_Message()

            )
        { }
    }
}
