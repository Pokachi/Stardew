using System.Reflection;
using HarmonyLib;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Buffs;
using StardewValley.Extensions;
using StardewValley.Locations;
using StardewValley.Menus;
using StardewValley.Tools;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class RichVein : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.RichVein";
        internal RichVein(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffRichVein_Name(),
                  description: I18n.BuffRichVein_DescInGame(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 59,
                  duration: duration,
                  effects: new BuffEffects()
                  {
                      MiningLevel = { 3 },
                  },
                  message: I18n.BuffRichVein_Message()

            )
        { }
        public static void MineShaftCheckStoneForItems_postfix(MineShaft __instance, string stoneId, int x, int y, Farmer who)
        {
            if (!who.hasBuff(ID))
                return;
            if (ModEntry.Instance.Random.Next(0, 100) < 5)
            {
                double chanceForLadderDown = 0.02 + 1.0 / (double)Math.Max(1, __instance.stonesLeftOnThisLevel) + (double)(who?.LuckLevel ?? 0) / 100.0 + Game1.player.DailyLuck / 5.0;
                if (__instance.EnemyCount == 0)
                {
                    chanceForLadderDown += 0.04;
                }
                if (who != null && who.hasBuff("dwarfStatue_1"))
                {
                    chanceForLadderDown *= 1.25;
                }

                if (!__instance.ladderHasSpawned && !__instance.mustKillAllMonstersToAdvance() && __instance.shouldCreateLadderOnThisLevel())
                {
                    __instance.createLadderDown(x, y);
                }
            }
            if (ModEntry.Instance.Random.Next(0, 100) < 5)
            {
                MethodInfo methodInfo = AccessTools.Method(typeof(MineShaft), "breakStone");
                if ((bool) methodInfo.Invoke(__instance, new object[] { stoneId, x, y, who, ModEntry.Instance.Random }))
                    return;

                long farmerId = who?.UniqueMultiplayerID ?? 0;
                int farmerLuckLevel = who?.LuckLevel ?? 0;
                double num = who?.DailyLuck ?? 0.0;
                int farmerMiningLevel = who?.MiningLevel ?? 0;
                double chanceModifier = num / 2.0 + (double)farmerMiningLevel * 0.005 + (double)farmerLuckLevel * 0.001;
                double oreModifier = ((stoneId == 40.ToString() || stoneId == 42.ToString()) ? 1.2 : 0.8);

                if (stoneId == 44.ToString())
                {
                    int whichGem = ModEntry.Instance.Random.Next(59, 70);
                    whichGem += whichGem % 2;
                    bool reachedBottom = false;
                    foreach (Farmer allFarmer in Game1.getAllFarmers())
                    {
                        if (allFarmer.timesReachedMineBottom > 0)
                        {
                            reachedBottom = true;
                            break;
                        }
                    }
                    if (!reachedBottom)
                    {
                        if (__instance.mineLevel < 40 && whichGem != 66 && whichGem != 68)
                        {
                            whichGem = ModEntry.Instance.Random.Choose(66, 68);
                        }
                        else if (__instance.mineLevel < 80 && (whichGem == 64 || whichGem == 60))
                        {
                            whichGem = ModEntry.Instance.Random.Choose(66, 70, 68, 62);
                        }
                    }
                    Game1.createObjectDebris("(O)" + whichGem, x, y, farmerId, __instance);
                    Game1.stats.OtherPreciousGemsFound++;
                    return;
                }
                int excavatorMultiplier = ((who == null || !who.professions.Contains(22)) ? 1 : 2);
                double dwarfStatueMultiplier = ((who != null && who.hasBuff("dwarfStatue_4")) ? 1.25 : 1.0);
                if (ModEntry.Instance.Random.NextDouble() < 0.022 * (1.0 + chanceModifier) * (double)excavatorMultiplier * dwarfStatueMultiplier)
                {
                    string id = "(O)" + (535 + ((__instance.getMineArea() == 40) ? 1 : ((__instance.getMineArea() == 80) ? 2 : 0)));
                    if (__instance.getMineArea() == 121)
                    {
                        id = "(O)749";
                    }
                    if (who != null && who.professions.Contains(19) && ModEntry.Instance.Random.NextBool())
                    {
                        Game1.createObjectDebris(id, x, y, farmerId, __instance);
                    }
                    Game1.createObjectDebris(id, x, y, farmerId, __instance);
                    who?.gainExperience(5, 20 * __instance.getMineArea());
                }
                if (__instance.mineLevel > 20 && ModEntry.Instance.Random.NextDouble() < 0.005 * (1.0 + chanceModifier) * (double)excavatorMultiplier * dwarfStatueMultiplier)
                {
                    if (who != null && who.professions.Contains(19) && ModEntry.Instance.Random.NextBool())
                    {
                        Game1.createObjectDebris("(O)749", x, y, farmerId, __instance);
                    }
                    Game1.createObjectDebris("(O)749", x, y, farmerId, __instance);
                    who?.gainExperience(5, 40 * __instance.getMineArea());
                }

                if (ModEntry.Instance.Random.NextDouble() < 0.05 * (1.0 + chanceModifier) * oreModifier)
                {
                    int burrowerMultiplier = ((who == null || !who.professions.Contains(21)) ? 1 : 2);
                    double addedCoalChance = ((who != null && who.hasBuff("dwarfStatue_2")) ? 0.1 : 0.0);
                    if (ModEntry.Instance.Random.NextDouble() < 0.25 * (double)burrowerMultiplier + addedCoalChance)
                    {
                        Game1.createObjectDebris("(O)382", x, y, farmerId, __instance);
                        Game1.Multiplayer.broadcastSprites(__instance, new TemporaryAnimatedSprite(25, new Vector2(64 * x, 64 * y), Color.White, 8, Game1.random.NextBool(), 80f, 0, -1, -1f, 128));
                    }
                    Game1.createObjectDebris(__instance.getOreIdForLevel(__instance.mineLevel, ModEntry.Instance.Random), x, y, farmerId, __instance);
                    who?.gainExperience(3, 5);
                }
                else if (ModEntry.Instance.Random.NextBool())
                {
                    Game1.createDebris(14, x, y, 1, __instance);
                }
            }
        }
    }
}
