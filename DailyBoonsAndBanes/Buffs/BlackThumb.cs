using Microsoft.CodeAnalysis.Text;
using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Buffs;
using StardewValley.Characters;
using StardewValley.Extensions;
using StardewValley.GameData.Crops;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class BlackThumb : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.BlackThumb";
        internal BlackThumb(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffBlackthumb_Name(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 15,
                  duration: duration,
                  description: I18n.BuffBlackthumb_DescInGame(),
                  effects: new BuffEffects()
                  {
                      FarmingLevel = { -3 },
                  },
                  message: I18n.BuffBlackthumb_Message()

            )
        { }

        public static bool CropHarvest_prefix(Crop __instance, int xTile, int yTile, HoeDirt soil, JunimoHarvester junimoHarvester, bool isForcedScytheHarvest, ref bool __result)
        {
            if (!CanHarvest(__instance) || !Game1.player.hasBuff(ID))
                return true;

            if (ModEntry.Instance.Random.Next(0, 100) < 5)
            {
                if (junimoHarvester == null)
                {
                    Game1.player.currentLocation.playSound("harvest");
                    DelayedAction.playSoundAfterDelay("coin", 260, Game1.player.currentLocation);
                    Game1.player.canMove = false;
                }
                if (!__instance.RegrowsAfterHarvest() && junimoHarvester == null)
                {
                    Game1.Multiplayer.broadcastSprites(Game1.currentLocation, new TemporaryAnimatedSprite(17, new Vector2(xTile * 64f, yTile * 64f), Color.White, 7, Game1.random.NextBool(), 125f));
                    Game1.Multiplayer.broadcastSprites(Game1.currentLocation, new TemporaryAnimatedSprite(14, new Vector2(xTile * 64f, yTile * 64f), Color.White, 7, Game1.random.NextBool(), 50f));
                }
                int regrowDays = __instance.GetData()?.RegrowDays ?? -1;
                if (regrowDays <= 0) {
                    __result = true;
                    return false;
                }

                __instance.fullyGrown.Value = true;
                if (__instance.dayOfCurrentPhase.Value == regrowDays)
                {
                    __instance.updateDrawMath(__instance.tilePosition);
                }
                __instance.dayOfCurrentPhase.Value = regrowDays;
                __result = false;

                return false;
            }

            return true;
        }
        private static bool CanHarvest(Crop crop)
        {
            return
                !crop.dead.Value
                && !crop.forageCrop.Value
                && crop.currentPhase.Value >= crop.phaseDays.Count - 1
                && (!crop.fullyGrown.Value || crop.dayOfCurrentPhase.Value <= 0);
        }
    }
}
