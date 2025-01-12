using ExperienceConfig.External;

namespace ExperienceConfig
{
    internal sealed class ModConfig
    {
        private static bool isRegistered = false;
        public bool IsEnabled { get; set; }
        public float[] VanillaSkillMultiplier { get; set; }
        public Dictionary<string, float> SpaceCoreSkilMultiplier { get; set; }
        public ModConfig()
        {
            this.IsEnabled = true;
            this.VanillaSkillMultiplier = new[]
            {
                1f, //Farming
                1f, //Fishing
                1f, //Foraging
                1f, //Mining
                1f, //Combat
            };
            InitSpacecoreDict(true);
        }

        private void InitSpacecoreDict(bool reset=false)
        {
            if (!ModEntry.Instance.Helper.ModRegistry.IsLoaded("spacechase0.SpaceCore") || ModEntry.Instance.SpaceCoreAPI is null)
                return;
            Dictionary<string, float> tempMultiplier = new Dictionary<string, float>();

            // Copy over existing config if any
            if (this.SpaceCoreSkilMultiplier != null && !reset) {
                foreach (string s in this.SpaceCoreSkilMultiplier.Keys)
                    tempMultiplier[s] = this.SpaceCoreSkilMultiplier[s];
            }

            foreach (string s in ModEntry.Instance.SpaceCoreAPI.GetCustomSkills())
                tempMultiplier[s] = tempMultiplier.GetValueOrDefault(s, 1f);

            this.SpaceCoreSkilMultiplier = tempMultiplier;
        }

        public void createMenu()
        {
            // get Generic Mod Config Menu's API (if it's installed)
            var configMenu = ModEntry.Instance.Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (configMenu is null)
                return;

            if (ModConfig.isRegistered)
                configMenu.Unregister(ModEntry.Instance.ModManifest);

            configMenu.Register(
                mod: ModEntry.Instance.ModManifest,
                reset: () => ModEntry.Instance.Config = new ModConfig(),
                save: () =>
                {
                    ModEntry.Instance.Helper.WriteConfig<ModConfig>(ModEntry.Instance.Config);
                }
            );

            isRegistered = true;

            configMenu.AddSectionTitle(
                mod: ModEntry.Instance.ModManifest,
                text: I18n.CfgGeneralSetting_Name
            );

            configMenu.AddBoolOption(
                mod: ModEntry.Instance.ModManifest,
                name: I18n.CfgEnable_Name,
                tooltip: I18n.CfgEnable_Desc,
                getValue: () => this.IsEnabled,
                setValue: value => this.IsEnabled = value
            );

            configMenu.AddSectionTitle(
                mod: ModEntry.Instance.ModManifest,
                text: I18n.CfgVanillaSkillMul_Name,
                tooltip: I18n.CfgVanillaSkillMul_Desc
            );

            configMenu.AddNumberOption(
                mod: ModEntry.Instance.ModManifest,
                name: I18n.CfgFarmingMult_Name,
                tooltip: I18n.CfgFarmingMult_Desc,
                getValue: () => this.VanillaSkillMultiplier[0],
                setValue: value => this.VanillaSkillMultiplier[0] = value,
                min: 5f / 100f,
                max: 50f / 1f,
                interval: 5f / 100f
            );

            configMenu.AddNumberOption(
                mod: ModEntry.Instance.ModManifest,
                name: I18n.CfgFishingMult_Name,
                tooltip: I18n.CfgFishingMult_Desc,
                getValue: () => this.VanillaSkillMultiplier[1],
                setValue: value => this.VanillaSkillMultiplier[1] = value,
                min: 5f / 100f,
                max: 50f / 1f,
                interval: 5f / 100f
            );

            configMenu.AddNumberOption(
                mod: ModEntry.Instance.ModManifest,
                name: I18n.CfgForagingMult_Name,
                tooltip: I18n.CfgForagingMult_Desc,
                getValue: () => this.VanillaSkillMultiplier[2],
                setValue: value => this.VanillaSkillMultiplier[2] = value,
                min: 5f / 100f,
                max: 50f / 1f,
                interval: 5f / 100f
            );

            configMenu.AddNumberOption(
                mod: ModEntry.Instance.ModManifest,
                name: I18n.CfgMiningMult_Name,
                tooltip: I18n.CfgMiningMult_Desc,
                getValue: () => this.VanillaSkillMultiplier[3],
                setValue: value => this.VanillaSkillMultiplier[3] = value,
                min: 5f / 100f,
                max: 50f / 1f,
                interval: 5f / 100f
            );

            configMenu.AddNumberOption(
                mod: ModEntry.Instance.ModManifest,
                name: I18n.CfgCombatMult_Name,
                tooltip: I18n.CfgCombatMult_Desc,
                getValue: () => this.VanillaSkillMultiplier[4],
                setValue: value => this.VanillaSkillMultiplier[4] = value,
                min: 5f / 100f,
                max: 50f / 1f,
                interval: 5f / 100f
            );

            // Spacecore Skills
            InitSpacecoreDict();
            if (ModEntry.Instance.Helper.ModRegistry.IsLoaded("spacechase0.SpaceCore"))
            {
                // Enable/disable menu
                configMenu.AddSectionTitle(
                    mod: ModEntry.Instance.ModManifest,
                    text: I18n.CfgSpacecoreSkillMul_Name,
                    tooltip: I18n.CfgSpacecoreSkillMul_Desc
                );

                foreach (string skill_name in SpaceCoreSkilMultiplier.Keys)
                {
                    // [0] contains mod author, [1] contains skill name
                    var skill_name_split = skill_name.Split(".");

                    if (skill_name_split[1] == "LoveOfCooking")
                        skill_name_split[1] = "Cooking";
                    else if (skill_name_split[1] == "SwordAndSorcery")
                        skill_name_split[1] = skill_name_split[2];

                    configMenu.AddNumberOption(
                        mod: ModEntry.Instance.ModManifest,
                        name: () => String.Format(I18n.CfgSpacecoreMult_Name(), skill_name_split[1]),
                        tooltip: () => String.Format(I18n.CfgSpacecoreMult_Desc(), skill_name_split[1]),
                        getValue: () => this.SpaceCoreSkilMultiplier[skill_name],
                        setValue: value => this.SpaceCoreSkilMultiplier[skill_name] = value,
                        min: 5f / 100f,
                        max: 50f / 1f,
                        interval: 5f / 100f
                    );
                }

            }
        }
    }
}
