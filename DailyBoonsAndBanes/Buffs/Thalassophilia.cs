using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Buffs;
using StardewValley.Menus;
using StardewValley.Tools;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class Thalassophilia : BuffWithMessage
    {
        internal const string ID = "Reena.DailyBoonsAndBanes.Buffs.Thalassophilia";
        internal Thalassophilia(int duration)
            : base(
                  id: ID,
                  displayName: I18n.BuffThalassophilia_Name(),
                  description: I18n.BuffThalassophilia_DescInGame(),
                  iconTexture: Game1.emoteSpriteSheet,
                  iconSheetIndex: 59,
                  duration: duration,
                  effects: new BuffEffects()
                  {
                      FishingLevel = { 3 },
                  },
                  message: I18n.BuffThalassophilia_Message()

            )
        { }

        private static HashSet<long> speedyFish = new HashSet<long>();
        public static void calculateTimeUntilFishingBite_Postfix(FishingRod __instance, Vector2 bobberTile, bool isFirstCast, Farmer who, ref float __result)
        {
            if (!who.hasBuff(ID))
                return;
            if (__result > 0 && !speedyFish.Contains(who.UniqueMultiplayerID))
            {
                __result *= 0.95f;
                speedyFish.Add(who.UniqueMultiplayerID);
            }
        }

        public static void doDoneFishing_PostFix(FishingRod __instance)
        {
            if (!Game1.player.hasBuff(ID))
                return;
            speedyFish.Remove(Game1.player.UniqueMultiplayerID);
        }

        private static HashSet<long> bonusFishChance = new HashSet<long>();
        public static void getFish_PostFix(GameLocation __instance, float millisecondsAfterNibble, string bait, int waterDepth, Farmer who, double baitPotency, Vector2 bobberTile, string locationName, ref Item __result)
        {
            if (!who.hasBuff(ID))
                return;
            if ((__result == null || (167 <= Int32.Parse(__result.ItemId) && Int32.Parse(__result.ItemId) <= 173)) && ModEntry.Instance.Random.Next(0, 100) < 5 && !bonusFishChance.Contains(who.UniqueMultiplayerID))
            {
                bonusFishChance.Add(who.UniqueMultiplayerID);
                __result = __instance.getFish(millisecondsAfterNibble, bait, waterDepth, who, baitPotency, bobberTile, locationName);
                bonusFishChance.Remove(who.UniqueMultiplayerID);
            }
        }

        public static void modifyFishingDifficulty(BobberBar bobberBar)
        {
            if (!Game1.player.hasBuff(ID))
                return;
            IReflectedField<float> difficultyField = ModEntry.Instance.Helper.Reflection.GetField<float>(bobberBar, "difficulty", true);
            difficultyField.SetValue(difficultyField.GetValue() * 0.95f);


            IReflectedField<int> barHeightField = ModEntry.Instance.Helper.Reflection.GetField<int>(bobberBar, "bobberBarHeight", true);
            barHeightField.SetValue((int)(barHeightField.GetValue() * 1.05f));

            if (ModEntry.Instance.Random.Next(0, 100) < 5)
            {
                IReflectedField<bool> treasureField = ModEntry.Instance.Helper.Reflection.GetField<bool>(bobberBar, "treasure", true);
                treasureField.SetValue(true);
            }
        }

    }
}
