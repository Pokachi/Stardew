using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Buffs;
using StardewValley.Characters;
using StardewValley.Extensions;
using StardewValley.TerrainFeatures;
using StardewValley.Tools;
using xTile.Dimensions;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class NaturesWrath : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.NaturesWrath";
        internal NaturesWrath(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffNatureswrath_Name(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 15,
                  duration: duration,
                  description: I18n.BuffNatureswrath_DescInGame(),
                  effects: new BuffEffects()
                  {
                      ForagingLevel = { -3 },
                  },
                  message: I18n.BuffNatureswrath_Message()

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
                }
                if (!__instance.RegrowsAfterHarvest() && junimoHarvester == null)
                {
                    Game1.Multiplayer.broadcastSprites(Game1.currentLocation, new TemporaryAnimatedSprite(17, new Vector2(xTile * 64f, yTile * 64f), Color.White, 7, Game1.random.NextBool(), 125f));
                    Game1.Multiplayer.broadcastSprites(Game1.currentLocation, new TemporaryAnimatedSprite(14, new Vector2(xTile * 64f, yTile * 64f), Color.White, 7, Game1.random.NextBool(), 50f));
                }
                __result = true;
                return false;
            }

            return true;
        }

        public static bool TreeTickUpdate_prefix(Tree __instance)
        {
            if (!Game1.getFarmer(__instance.lastPlayerToHit.Value).hasBuff(ID))
                return true;
            float shakeRotation = __instance.shakeRotation;
            shakeRotation += (__instance.shakeLeft.Value ? (0f - __instance.maxShake * __instance.maxShake) : (__instance.maxShake * __instance.maxShake));

            if (!__instance.destroy.Value && __instance.falling.Value && (double)Math.Abs(shakeRotation) > Math.PI / 2.0 && __instance.GetData() != null && __instance.GetData().DropWoodOnChop && ModEntry.Instance.Random.Next(0, 100) < 5)
            {
                __instance.shakeRotation = shakeRotation;
                __instance.falling.Value = false;
                __instance.maxShake = 0f;
                __instance.Location.localSound("treethud");
                if (__instance.GetData().DropWoodOnChop)
                {
                    Game1.createMultipleObjectDebris("(O)92", (int)__instance.Tile.X + (__instance.shakeLeft.Value ? (-4) : 4), (int)__instance.Tile.Y, 5, __instance.lastPlayerToHit.Value, __instance.Location);

                    float seedOnChopChance = __instance.GetData().SeedOnChopChance;
                    if (Game1.getFarmer(__instance.lastPlayerToHit.Value).getEffectiveSkillLevel(2) >= 1 && __instance.GetData().SeedItemId != null && ModEntry.Instance.Random.NextDouble() < (double)seedOnChopChance)
                    {
                        Game1.createMultipleObjectDebris(__instance.GetData().SeedItemId, (int)__instance.Tile.X + (__instance.shakeLeft.Value ? (-4) : 4), (int)__instance.Tile.Y, ModEntry.Instance.Random.Next(1, 3), __instance.lastPlayerToHit.Value, __instance.Location);
                    }
                }
                return false;
            }
            return true;
        }

        public static bool TreePerformBushDestroy_prefix(Tree __instance)
        {
            if (!Game1.player.hasBuff(ID))
                return true;

            if (__instance.GetData() != null && (__instance.GetData().DropWoodOnChop || __instance.GetData().DropHardwoodOnLumberChop) && ModEntry.Instance.Random.Next(0, 100) < 5)
            {
                return false;
            }
            return true;
        }

        public static bool TreePerformTreeFall_prefix(Tree __instance, Tool t, int explosion, Vector2 tileLocation, ref bool __result)
        {
            if (!Game1.player.hasBuff(ID))
                return true;
            if (__instance.stump.Value && __instance.GetData() != null && __instance.GetData().DropWoodOnChop && t?.getLastFarmerToUse() != null && ModEntry.Instance.Random.Next(0, 100) < 5)
            {
                __instance.Location.objects.Remove(__instance.Tile);
                __instance.tapped.Value = false;
                __instance.health.Value = -100f;
                __instance.Location.playSound("treethud");
                if (!__instance.falling.Value)
                    __result = true;
                else
                    __result = false;

                return false;

            }
            return true;
        }

        public static bool ObjectPerformToolAction_prefix(StardewValley.Object __instance, Tool t, ref bool __result)
        {
            if (!Game1.player.hasBuff(ID))
                return true;

            if (!__instance.isTemporarilyInvisible && __instance.IsTwig() && t != null && t is Axe && ModEntry.Instance.Random.Next(0, 100) < 5)
            {
                __instance.fragility.Value = 2;
                __instance.playNearbySoundAll("axchop");
                Game1.createRadialDebris(__instance.Location, 12, (int)__instance.tileLocation.X, (int)__instance.tileLocation.Y, Game1.random.Next(4, 10), resource: false);
                Game1.Multiplayer.broadcastSprites(__instance.Location, new TemporaryAnimatedSprite(12, new Vector2(__instance.tileLocation.X * 64f, __instance.tileLocation.Y * 64f), Color.White, 8, Game1.random.NextBool(), 50f));
                __result = true;
                return false;
            }

            return true;
        }

        public static bool GameLocationCheckAction_prefix(GameLocation __instance, Location tileLocation, xTile.Dimensions.Rectangle viewport, Farmer who, ref bool __result)
        {
            if (!Game1.player.hasBuff(ID))
                return true;

            Vector2 tilePos = new Vector2(tileLocation.X, tileLocation.Y);
            if (__instance.objects.TryGetValue(tilePos, out var obj))
            {
                bool isErrorItem = ItemRegistry.GetDataOrErrorItem(obj.QualifiedItemId).IsErrorItem;
                if ((obj.Type != null || isErrorItem) && (bool)obj.isSpawnedObject.Value && obj.isForage() && who.couldInventoryAcceptThisItem(obj) && ModEntry.Instance.Random.Next(0, 100) < 5)
                {
                    if (who.IsLocalPlayer)
                    {
                        __instance.localSound("pickUpItem");
                        DelayedAction.playSoundAfterDelay("coin", 300);
                    }
                    __instance.objects.Remove(tilePos);
                    __result = true;
                    return false;
                }
            }
            return true;
        }

        private static bool CanHarvest(Crop crop)
        {
            return
                !crop.dead.Value
                && crop.forageCrop.Value
                && crop.whichForageCrop.Value == "1";
        }
    }
}
