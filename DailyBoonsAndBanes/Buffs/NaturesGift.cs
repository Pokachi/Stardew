using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Buffs;
using StardewValley.GameData.Crops;
using StardewValley.TerrainFeatures;
using StardewValley.Tools;
using xTile.Dimensions;
using Object = StardewValley.Object;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class NaturesGift : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.NaturesGift";
        internal NaturesGift(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffNaturesgift_Name(),
                  description: I18n.BuffNaturesgift_DescInGame(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 59,
                  duration: duration,
                  effects: new BuffEffects()
                  {
                      ForagingLevel = { 3 },
                  },
                  message: I18n.BuffNaturesgift_Message()

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
                    int cropQuality = 4;
                    if (Game1.player.professions.Contains(16))
                    {
                        cropQuality = 4;
                    }
                    else if (ModEntry.Instance.Random.NextDouble() < (double)((float)Game1.player.ForagingLevel / 30f))
                    {
                        cropQuality = 2;
                    }
                    else if (ModEntry.Instance.Random.NextDouble() < (double)((float)Game1.player.ForagingLevel / 15f))
                    {
                        cropQuality = 1;
                    }
                    Game1.stats.ItemsForaged += 1;

                    Object o = ItemRegistry.Create<StardewValley.Object>("(O)399", cropQuality);

                    Game1.createItemDebris(o.getOne(), new Vector2(xTile * 64 + 32, yTile * 64 + 32), -1);
                }
            }
        }

        public static void TreeTickUpdate_prefix(Tree __instance)
        {
            if (!Game1.getFarmer(__instance.lastPlayerToHit.Value).hasBuff(ID))
                return;
            float shakeRotation = __instance.shakeRotation;
            shakeRotation += (__instance.shakeLeft.Value ? (0f - __instance.maxShake * __instance.maxShake) : (__instance.maxShake * __instance.maxShake));

            if (!__instance.destroy.Value && __instance.falling.Value && (double)Math.Abs(shakeRotation) > Math.PI / 2.0 && __instance.GetData() != null && __instance.GetData().DropWoodOnChop && ModEntry.Instance.Random.Next(0, 100) < 5)
            {
                int numToDrop = (int)((Game1.getFarmer(__instance.lastPlayerToHit.Value).professions.Contains(12) ? 1.25 : 1.0) * (double)(12 + extraWoodCalculator(__instance.Tile)));
                Game1.createRadialDebris(__instance.Location, 12, (int)__instance.Tile.X + (__instance.shakeLeft.Value ? (-4) : 4), (int)__instance.Tile.Y, numToDrop, resource: true);
            }
        }

        public static void TreePerformBushDestroy_prefix(Tree __instance)
        {
            if (!Game1.player.hasBuff(ID))
                return;

            if (__instance.GetData() != null && (__instance.GetData().DropWoodOnChop || __instance.GetData().DropHardwoodOnLumberChop) && ModEntry.Instance.Random.Next(0, 100) < 5)
            {
                Game1.createDebris(12, (int)__instance.Tile.X, (int)__instance.Tile.Y, (int)((Game1.getFarmer(__instance.lastPlayerToHit.Value).professions.Contains(12) ? 1.25 : 1.0) * 4.0), __instance.Location);
            }
        }

        public static void TreePerformTreeFall_prefix(Tree __instance, Tool t)
        {
            if (!Game1.player.hasBuff(ID))
                return;
            if (__instance.stump.Value && __instance.GetData() != null && __instance.GetData().DropWoodOnChop && t?.getLastFarmerToUse() != null && ModEntry.Instance.Random.Next(0, 100) < 5)
            {
                if (Game1.IsMultiplayer)
                    Game1.createRadialDebris(__instance.Location, 12, (int)__instance.Tile.X, (int)__instance.Tile.Y, (int)((Game1.getFarmer(__instance.lastPlayerToHit.Value).professions.Contains(12) ? 1.25 : 1.0) * 4.0), resource: true);
                else
                    Game1.createRadialDebris(__instance.Location, 12, (int)__instance.Tile.X, (int)__instance.Tile.Y, (int)((Game1.getFarmer(__instance.lastPlayerToHit.Value).professions.Contains(12) ? 1.25 : 1.0) * (double)(5 + extraWoodCalculator(__instance.Tile))), resource: true);
            }
        }

        public static void ObjectPerformToolAction_prefix(Object __instance, Tool t)
        {
            if (!Game1.player.hasBuff(ID))
                return;
            if (!__instance.isTemporarilyInvisible && __instance.IsTwig() && t != null && t is Axe && ModEntry.Instance.Random.Next(0, 100) < 5)
            {
                __instance.Location.debris.Add(new Debris(ItemRegistry.Create("(O)388"), __instance.TileLocation * 64f));
            }
        }

        public static void GameLocationCheckAction_prefix(GameLocation __instance, Location tileLocation, xTile.Dimensions.Rectangle viewport, Farmer who, ref bool __result)
        {
            if (!who.hasBuff(ID))
                return;

            Vector2 tilePos = new Vector2(tileLocation.X, tileLocation.Y);
            if (__instance.objects.TryGetValue(tilePos, out var obj))
            {
                bool isErrorItem = ItemRegistry.GetDataOrErrorItem(obj.QualifiedItemId).IsErrorItem;
                if ((obj.Type != null || isErrorItem) && (bool)obj.isSpawnedObject.Value && obj.isForage() && who.couldInventoryAcceptThisItem(obj) && ModEntry.Instance.Random.Next(0, 100) < 5)
                {
                    int cropQuality = 0;
                    if (Game1.player.professions.Contains(16))
                        cropQuality = 4;
                    else if (ModEntry.Instance.Random.NextDouble() < (double)((float)Game1.player.ForagingLevel / 30f))
                        cropQuality = 2;
                    else if (ModEntry.Instance.Random.NextDouble() < (double)((float)Game1.player.ForagingLevel / 15f))
                        cropQuality = 1;
                    Game1.stats.ItemsForaged += 1;

                    Item harvestedItem = ItemRegistry.Create(obj.ItemId, 1, cropQuality);
                    Game1.createItemDebris(harvestedItem.getOne(), new Vector2(tilePos.X * 64 + 32, tilePos.Y * 64 + 32), -1);
                }
            }
        }

        private static bool CanHarvest(Crop crop)
        {
            return
                !crop.dead.Value
                && crop.forageCrop.Value
                && crop.whichForageCrop.Value == "1";
        }

        private static int extraWoodCalculator(Vector2 tileLocation)
        {
            Random random = Utility.CreateRandom(Game1.uniqueIDForThisGame, Game1.stats.DaysPlayed, (double)tileLocation.X * 7.0, (double)tileLocation.Y * 11.0);
            int extraWood = 0;
            if (random.NextDouble() < Game1.player.DailyLuck)
            {
                extraWood++;
            }
            if (random.NextDouble() < (double)Game1.player.ForagingLevel / 12.5)
            {
                extraWood++;
            }
            if (random.NextDouble() < (double)Game1.player.ForagingLevel / 12.5)
            {
                extraWood++;
            }
            if (random.NextDouble() < (double)Game1.player.LuckLevel / 25.0)
            {
                extraWood++;
            }
            return extraWood;
        }
    }
}
