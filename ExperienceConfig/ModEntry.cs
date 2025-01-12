using ExperienceConfig.External;
using ExperienceConfig.Patcher;
using HarmonyLib;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace ExperienceConfig
{
    internal sealed class ModEntry : Mod
    {
        internal ModConfig Config;
        internal static ModEntry Instance;
        internal ISpaceCoreApi SpaceCoreAPI;

        public override void Entry(IModHelper helper)
        {
            I18n.Init(helper.Translation);
            ModEntry.Instance = this;
            this.Config = Helper.ReadConfig<ModConfig>();

            helper.Events.GameLoop.GameLaunched += this.OnGameLaunch;
            helper.Events.GameLoop.SaveLoaded += this.OnSaveLoad;
            helper.Events.GameLoop.SaveCreated += this.OnSaveCreate;
            helper.Events.GameLoop.DayStarted += this.OnDayStart;

            var harmony = new Harmony(ModManifest.UniqueID);


            harmony.Patch(
               original: AccessTools.Method(typeof(Farmer), nameof(Farmer.gainExperience)),
               prefix: new HarmonyMethod(AccessTools.Method(typeof(ExperiencePatcher), nameof(ExperiencePatcher.gainExperience_Prefix)), 801)
            );

            if (helper.ModRegistry.IsLoaded("spacechase0.SpaceCore"))
            {
                harmony.Patch(
                   original: AccessTools.Method(AccessTools.TypeByName("SpaceCore.Skills"), "AddExperience"),
                   prefix: new HarmonyMethod(AccessTools.Method(typeof(ExperiencePatcher), nameof(ExperiencePatcher.AddExperience_Prefix)), 801)
                );
            }
        }

        private void OnGameLaunch(object sender, GameLaunchedEventArgs e)
        {
            this.SpaceCoreAPI = Instance.Helper.ModRegistry.GetApi<ISpaceCoreApi>("spacechase0.SpaceCore");

            this.Config.createMenu();
        }
        private void OnSaveLoad(object sender, SaveLoadedEventArgs e)
        {
            // Create config menu again, in case some Spacecore based mods are loaded after this mod
            this.Config.createMenu();
        }

        private void OnSaveCreate(object sender, SaveCreatedEventArgs e)
        {
            // Create config menu again, in case some Spacecore based mods are loaded after this mod
            this.Config.createMenu();
        }

        private void OnDayStart(object sender, DayStartedEventArgs e)
        {
            ExperiencePatcher.LeftOverVanillaEXP = new float[] { 0f, 0f, 0f, 0f, 0f, 0f };
            if (ModEntry.Instance.Helper.ModRegistry.IsLoaded("spacechase0.SpaceCore"))
            {
                foreach (string s in ExperiencePatcher.LeftOverSpaceCoreEXP.Keys)
                {
                    ExperiencePatcher.LeftOverSpaceCoreEXP[s] = 0f;
                }
            }
        }
    }
}