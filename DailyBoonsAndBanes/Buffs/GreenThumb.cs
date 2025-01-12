using System.ComponentModel;
using HarmonyLib;
using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Buffs;
using StardewValley.Characters;
using StardewValley.GameData.Crops;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class GreenThumb : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.GreenThumb";
        internal GreenThumb(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffGreenthumb_Name(),
                  description: I18n.BuffGreenthumb_DescInGame(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 59,
                  duration: duration,
                  effects: new BuffEffects()
                  {
                      FarmingLevel = { 3 },
                  },
                  message: I18n.BuffGreenthumb_Message()

            )
        { }

        public static void CropHarvest_prefix(Crop __instance, int xTile, int yTile, HoeDirt soil)
        {
            if (!CanHarvest(__instance) || !Game1.player.hasBuff(ID))
                return;
            if (ModEntry.Instance.Random.Next(0, 100) < 5)
            {
                CropData data = __instance.GetData();
                if (data != null)
                {
                    int fertilizerQualityLevel = soil.GetFertilizerQualityBoostLevel();
                    double chanceForGoldQuality = 0.2 * ((double)Game1.player.FarmingLevel / 10.0) + 0.2 * (double)fertilizerQualityLevel * (((double)Game1.player.FarmingLevel + 2.0) / 12.0) + 0.01;
                    double chanceForSilverQuality = Math.Min(0.75, chanceForGoldQuality * 2.0);
                    int cropQuality = 0;
                    if (fertilizerQualityLevel >= 3 && ModEntry.Instance.Random.NextDouble() < chanceForGoldQuality / 2.0)
                    {
                        cropQuality = 4;
                    }
                    else if (ModEntry.Instance.Random.NextDouble() < chanceForGoldQuality)
                    {
                        cropQuality = 2;
                    }
                    else if (ModEntry.Instance.Random.NextDouble() < chanceForSilverQuality || fertilizerQualityLevel >= 3)
                    {
                        cropQuality = 1;
                    }
                    cropQuality = MathHelper.Clamp(cropQuality, data?.HarvestMinQuality ?? 0, data?.HarvestMaxQuality ?? cropQuality);

                    Item harvestedItem = (__instance.programColored.Value ? new ColoredObject(__instance.indexOfHarvest.Value, 1, __instance.tintColor.Value)
                    {
                        Quality = cropQuality
                    } : ItemRegistry.Create(__instance.indexOfHarvest.Value, 1, cropQuality));

                    Game1.createItemDebris(harvestedItem.getOne(), new Vector2(xTile * 64 + 32, yTile * 64 + 32), -1);
                }
            }
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
