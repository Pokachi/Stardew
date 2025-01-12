using StardewModdingAPI;
using StardewValley;
using StardewValley.Buffs;
using StardewValley.Locations;
using StardewValley.Objects;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class DesolatedMine : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.DesolatedMine";
        internal DesolatedMine(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffDesolatedmine_Name(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 15,
                  duration: duration,
                  description: I18n.BuffDesolatedmine_DescInGame(),
                  effects: new BuffEffects()
                  {
                      MiningLevel = { -3 },
                  },
                  message: I18n.BuffDesolatedmine_Message()

            )
        { }

        public static bool MineShaftCheckStoneForItems_prefix(MineShaft __instance, string stoneId, int x, int y, Farmer who)
        {
            if (!who.hasBuff(ID))
                return true;

            if (ModEntry.Instance.Random.Next(0, 100) < 5)
            {
                __instance.stonesLeftOnThisLevel--;
                if (!__instance.ladderHasSpawned && !__instance.mustKillAllMonstersToAdvance() && __instance.stonesLeftOnThisLevel == 0)
                    __instance.createLadderDown(x, y);
                return false;
            }

            return true;
        }
    }
}
