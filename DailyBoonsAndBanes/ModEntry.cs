using StardewModdingAPI.Events;
using StardewModdingAPI;
using StardewValley;
using DailyBoonsAndBanes.Buffs;
using HarmonyLib;
using StardewValley.Tools;
using StardewValley.Menus;
using StardewValley.TerrainFeatures;
using Object = StardewValley.Object;
using StardewValley.Locations;

namespace DailyBoonsAndBanes
{
    internal sealed class ModEntry : Mod
    {
        internal ModConfig Config;
        internal Random Random;
        internal static ModEntry Instance;

        public override void Entry(IModHelper helper)
        {
            I18n.Init(helper.Translation);
            Instance = this;
            Config = Helper.ReadConfig<ModConfig>();
            Random = new Random();

            helper.Events.GameLoop.GameLaunched += this.OnGameLaunch;
            helper.Events.GameLoop.DayStarted += this.OnDayStart;
            helper.Events.Display.MenuChanged += this.OnMenuChange;

            Harmony harmony = new Harmony(this.ModManifest.UniqueID);

            harmony.Patch(
                original: AccessTools.Method(typeof(MineShaft), nameof(MineShaft.checkStoneForItems)),
                prefix: new HarmonyMethod(typeof(DesolatedMine), nameof(DesolatedMine.MineShaftCheckStoneForItems_prefix))
                );
            harmony.Patch(
                original: AccessTools.Method(typeof(MineShaft), nameof(MineShaft.checkStoneForItems)),
                postfix: new HarmonyMethod(typeof(RichVein), nameof(RichVein.MineShaftCheckStoneForItems_postfix))
                );

            harmony.Patch(
                original: AccessTools.Method(typeof(Farmer), nameof(Farmer.takeDamage)),
                prefix: new HarmonyMethod(typeof(RockSteady), nameof(RockSteady.FarmerTakeDamage_postfix))
                );
            harmony.Patch(
                original: AccessTools.Method(typeof(Farmer), nameof(Farmer.takeDamage)),
                prefix: new HarmonyMethod(typeof(Fragile), nameof(Fragile.FarmerTakeDamage_prefix))
                );

            harmony.Patch(
                original: AccessTools.Method(typeof(Crop), nameof(Crop.harvest)),
                prefix: new HarmonyMethod(typeof(GreenThumb), nameof(GreenThumb.CropHarvest_prefix))
                );
            harmony.Patch(
                original: AccessTools.Method(typeof(Crop), nameof(Crop.harvest)),
                prefix: new HarmonyMethod(typeof(BlackThumb), nameof(BlackThumb.CropHarvest_prefix))
                );
            harmony.Patch(
                original: AccessTools.Method(typeof(Crop), nameof(Crop.harvest)),
                prefix: new HarmonyMethod(typeof(NaturesGift), nameof(NaturesGift.CropHarvest_prefix))
                );
            harmony.Patch(
                original: AccessTools.Method(typeof(Crop), nameof(Crop.harvest)),
                prefix: new HarmonyMethod(typeof(NaturesWrath), nameof(NaturesWrath.CropHarvest_prefix))
                );
            harmony.Patch(
                original: AccessTools.Method(typeof(GameLocation), nameof(GameLocation.checkAction)),
                prefix: new HarmonyMethod(typeof(NaturesGift), nameof(NaturesGift.GameLocationCheckAction_prefix))
                );
            harmony.Patch(
                original: AccessTools.Method(typeof(GameLocation), nameof(GameLocation.checkAction)),
                prefix: new HarmonyMethod(typeof(NaturesWrath), nameof(NaturesWrath.GameLocationCheckAction_prefix))
                );
            harmony.Patch(
                original: AccessTools.Method(typeof(Tree), nameof(Tree.tickUpdate)),
                prefix: new HarmonyMethod(typeof(NaturesGift), nameof(NaturesGift.TreeTickUpdate_prefix))
                );
            harmony.Patch(
                original: AccessTools.Method(typeof(Tree), nameof(Tree.tickUpdate)),
                prefix: new HarmonyMethod(typeof(NaturesWrath), nameof(NaturesWrath.TreeTickUpdate_prefix))
                );
            harmony.Patch(
                original: AccessTools.Method(typeof(Tree), "performBushDestroy"),
                prefix: new HarmonyMethod(typeof(NaturesGift), nameof(NaturesGift.TreePerformBushDestroy_prefix))
                );
            harmony.Patch(
                original: AccessTools.Method(typeof(Tree), "performBushDestroy"),
                prefix: new HarmonyMethod(typeof(NaturesWrath), nameof(NaturesWrath.TreePerformBushDestroy_prefix))
                );
            harmony.Patch(
                original: AccessTools.Method(typeof(Tree), "performTreeFall"),
                prefix: new HarmonyMethod(typeof(NaturesGift), nameof(NaturesGift.TreePerformTreeFall_prefix))
                );
            harmony.Patch(
                original: AccessTools.Method(typeof(Tree), "performTreeFall"),
                prefix: new HarmonyMethod(typeof(NaturesWrath), nameof(NaturesWrath.TreePerformTreeFall_prefix))
                );
            harmony.Patch(
                original: AccessTools.Method(typeof(Object), nameof(Object.performToolAction)),
                prefix: new HarmonyMethod(typeof(NaturesGift), nameof(NaturesGift.ObjectPerformToolAction_prefix))
                );
            harmony.Patch(
                original: AccessTools.Method(typeof(Object), nameof(Object.performToolAction)),
                prefix: new HarmonyMethod(typeof(NaturesWrath), nameof(NaturesWrath.ObjectPerformToolAction_prefix))
                );

            harmony.Patch(
                original: AccessTools.Method(typeof(FishingRod), "calculateTimeUntilFishingBite"),
                postfix: new HarmonyMethod(typeof(Thalassophilia), nameof(Thalassophilia.calculateTimeUntilFishingBite_Postfix))
                );

            harmony.Patch(
                original: AccessTools.Method(typeof(FishingRod), "calculateTimeUntilFishingBite"),
                postfix: new HarmonyMethod(typeof(Skunking), nameof(Skunking.calculateTimeUntilFishingBite_Postfix))
                );

            harmony.Patch(
                original: AccessTools.Method(typeof(GameLocation), "getFish"),
                postfix: new HarmonyMethod(typeof(Thalassophilia), nameof(Thalassophilia.getFish_PostFix))
                );

            harmony.Patch(
                original: AccessTools.Method(typeof(GameLocation), "getFish"),
                postfix: new HarmonyMethod(typeof(Skunking), nameof(Skunking.getFish_PostFix))
                );

            harmony.Patch(
                original: AccessTools.Method(typeof(FishingRod), "doDoneFishing"),
                postfix: new HarmonyMethod(typeof(Thalassophilia), nameof(Thalassophilia.doDoneFishing_PostFix))
                );

            harmony.Patch(
                original: AccessTools.Method(typeof(FishingRod), "doDoneFishing"),
                postfix: new HarmonyMethod(typeof(Skunking), nameof(Skunking.doDoneFishing_PostFix))
                );
        }

        private void OnMenuChange(object sender, MenuChangedEventArgs e)
        {
            if (e.NewMenu is BobberBar bar) {
                if (Game1.player.hasBuff(Thalassophilia.ID))
                    Thalassophilia.modifyFishingDifficulty(bar);
                    Skunking.modifyFishingDifficulty(bar);
            }
        }

        private void OnGameLaunch(object sender, GameLaunchedEventArgs e)
        {
            Config.createMenu();
        }

        private void OnDayStart(object sender, DayStartedEventArgs e)
        {
            if (!Config.isEnabled)
                return;

            //calculate weight
            float totalWeight = 0;
            foreach (int weight in Config.weight)
                totalWeight += weight;

            float majorBaneRate = Config.weight[0] / totalWeight;
            float minorBaneRate = Config.weight[1] / totalWeight + majorBaneRate;
            float nothingRate = Config.weight[2] / totalWeight + minorBaneRate;
            float minorBoonRate = Config.weight[3] / totalWeight + nothingRate;
            float majorBoonRate = Config.weight[4] / totalWeight + minorBoonRate;

            float rng = (float) Random.NextDouble();

            if (Config.useDailyLuck)
            {
                rng += (float)Game1.player.DailyLuck;
            }

            BuffWithMessage? chosenBuff = null;

            if (rng <= majorBaneRate && Config.weight[0] != 0 && Config.enabledMajorBanes.Count != 0)
                chosenBuff = BuffHelper.createBoon(Config.enabledMajorBanes.ElementAt(Random.Next(0, Config.enabledMajorBanes.Count)));
            else if (rng > majorBaneRate && rng <= minorBaneRate && Config.weight[1] != 0 && Config.enabledMinorBanes.Count != 0)
                chosenBuff = BuffHelper.createBoon(Config.enabledMinorBanes.ElementAt(Random.Next(0, Config.enabledMinorBanes.Count)));
            else if (rng > nothingRate && rng <= minorBoonRate && Config.weight[3] != 0 && Config.enabledMinorBoons.Count != 0)
                chosenBuff = BuffHelper.createBoon(Config.enabledMinorBoons.ElementAt(Random.Next(0, Config.enabledMinorBoons.Count)));
            else if (rng > minorBoonRate && Config.weight[4] != 0 && Config.enabledMajorBoons.Count != 0)
                chosenBuff = BuffHelper.createBoon(Config.enabledMajorBoons.ElementAt(Random.Next(0, Config.enabledMajorBoons.Count)));

            if (chosenBuff != null)
            {
                Game1.player.applyBuff(chosenBuff);
                if (Config.showDailyMessage)
                {
                    //Game1.addHUDMessage(new HUDMessage(chosenBuff.message) { noIcon = true
                    Game1.addHUDMessage(HUDMessage.ForCornerTextbox(chosenBuff.message));
                }
            }

        }
    }
}
