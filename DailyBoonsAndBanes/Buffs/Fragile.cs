using StardewValley;
using StardewValley.Buffs;
using StardewValley.Monsters;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class Fragile : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.Fragile";
        internal Fragile(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffFragile_Name(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 15,
                  duration: duration,
                  description: I18n.BuffFragile_DescInGame(),
                  effects: new BuffEffects()
                  {
                      Defense = { -3 },
                      Immunity = { -3 },
                  },
                  message: I18n.BuffFragile_Message()

            )
        { }

        public override void OnAdded()
        {
            base.OnAdded();
            Game1.player.maxHealth -= 30;
            Game1.player.health = Math.Min(Game1.player.health, Game1.player.maxHealth);
        }

        public override void OnRemoved()
        {
            base.OnRemoved();
            Game1.player.maxHealth += 30;
        }

        public static void FarmerTakeDamage_prefix(Farmer __instance,ref int damage, bool overrideParry, Monster damager)
        {
            if (!Game1.player.hasBuff(ID))
                return;
            if (ModEntry.Instance.Random.Next(0, 100) < 5 && damager != null)
            {
                damage *= 2;
            }
        }
    }
}
