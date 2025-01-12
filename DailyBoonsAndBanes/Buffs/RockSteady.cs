using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Buffs;
using StardewValley.Monsters;
using StardewValley.Tools;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class RockSteady : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.RockSteady";
        internal RockSteady(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffRocksteady_Name(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 59,
                  duration: duration,
                  description: I18n.BuffRocksteady_DescInGame(),
                  effects: new BuffEffects()
                  {
                      Defense = { 3 },
                      Immunity = { 3 },
                  },
                  message: I18n.BuffRocksteady_Message()

            )
        { }

        public override void OnAdded()
        {
            base.OnAdded();
            Game1.player.health += 30;
            Game1.player.maxHealth += 30;
        }

        public override void OnRemoved()
        {
            base.OnRemoved();
            Game1.player.maxHealth -= 30;
            Game1.player.health = Math.Min(Game1.player.health, Game1.player.maxHealth);
        }

        public static void FarmerTakeDamage_postfix(Farmer __instance, int damage, bool overrideParry, Monster damager)
        {
            if (!Game1.player.hasBuff(ID))
                return;

            bool num = damager != null && !damager.isInvincible() && !overrideParry;
            bool monsterDamageCapable = (damager == null || !damager.isInvincible()) && (damager == null || (!(damager is GreenSlime) && !(damager is BigSlime)) || !__instance.isWearingRing("520"));
            bool playerParryable = __instance.CurrentTool is MeleeWeapon && ((MeleeWeapon)__instance.CurrentTool).isOnSpecial && (int)((MeleeWeapon)__instance.CurrentTool).type.Value == 3;
            bool playerDamageable = __instance.CanBeDamaged();
            if ((num && playerParryable) || !(monsterDamageCapable && playerDamageable))
                return;
            if (ModEntry.Instance.Random.Next(0, 100) < 5 && damager != null)
            {
                Rectangle monsterBox = damager.GetBoundingBox();
                Vector2 trajectory = Utility.getAwayFromPlayerTrajectory(monsterBox, __instance);
                trajectory /= 2f;
                damager.takeDamage(damage, (int)trajectory.X, (int)trajectory.Y, isBomb: false, 1.0, __instance);
                damager.currentLocation.debris.Add(new Debris(damage, new Vector2(monsterBox.Center.X + 16, monsterBox.Center.Y), new Color(255, 130, 0), 1f, damager));
            }
        }
    }
}
