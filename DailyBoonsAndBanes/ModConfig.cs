using System.Linq;
using DailyBoonsAndBanes.Buffs;
using ExperienceConfig.External;

namespace DailyBoonsAndBanes
{
    internal class ModConfig
    {
        public bool isEnabled { get; set; }
        public bool showDailyMessage { get; set; }
        public bool useDailyLuck { get; set; }
        public List<BuffNames.MajorBane> enabledMajorBanes { get; set; }
        public List<BuffNames.MajorBoon> enabledMajorBoons { get; set; }
        public List<BuffNames.MinorBane> enabledMinorBanes { get; set; }
        public List<BuffNames.MinorBoon> enabledMinorBoons { get; set; }
        public int minBuffDuration;
        public int maxBuffDuration;
        public int[] weight {  get; set; }

        public ModConfig()
        {
            reset();
        }

        public void reset()
        {
            this.isEnabled = true;
            this.showDailyMessage = true;
            this.useDailyLuck = true;
            enabledMajorBanes = Enum.GetValues(typeof(BuffNames.MajorBane)).Cast<BuffNames.MajorBane>().ToList();
            enabledMajorBoons = Enum.GetValues(typeof(BuffNames.MajorBoon)).Cast<BuffNames.MajorBoon>().ToList();
            enabledMinorBanes = Enum.GetValues(typeof(BuffNames.MinorBane)).Cast<BuffNames.MinorBane>().ToList();
            enabledMinorBoons = Enum.GetValues(typeof(BuffNames.MinorBoon)).Cast<BuffNames.MinorBoon>().ToList();
            weight = new int[] { 5, 20, 25, 40, 10 };
            minBuffDuration = 12;
            maxBuffDuration = 20;
        }

        public void createMenu()
        {
            // get Generic Mod Config Menu's API (if it's installed)
            var configMenu = ModEntry.Instance.Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (configMenu is null)
                return;

            configMenu.Register(
                mod: ModEntry.Instance.ModManifest,
                reset: () => ModEntry.Instance.Config.reset(),
                save: () =>
                {
                    ModEntry.Instance.Helper.WriteConfig<ModConfig>(ModEntry.Instance.Config);
                }
            );

            // General Config
            configMenu.AddSectionTitle(
                mod: ModEntry.Instance.ModManifest,
                text: I18n.CfgGeneralSetting_Name
            );
            configMenu.AddBoolOption(
                mod: ModEntry.Instance.ModManifest,
                name: I18n.CfgEnable_Name,
                tooltip: I18n.CfgEnable_Desc,
                getValue: () => this.isEnabled,
                setValue: value => this.isEnabled = value
            );
            configMenu.AddBoolOption(
                mod: ModEntry.Instance.ModManifest,
                name: I18n.CfgShowDailyMessage_Name,
                tooltip: I18n.CfgShowDailyMessage_Desc,
                getValue: () => this.showDailyMessage,
                setValue: value => this.showDailyMessage = value
            );
            configMenu.AddBoolOption(
                mod: ModEntry.Instance.ModManifest,
                name: I18n.CfgDailyLuck_Name,
                tooltip: I18n.CfgDailyLuck_Desc,
                getValue: () => this.useDailyLuck,
                setValue: value => this.useDailyLuck = value
            );
            configMenu.AddNumberOption(
                mod: ModEntry.Instance.ModManifest,
                name: I18n.CfgMinDuration_Name,
                tooltip: I18n.CfgMinDuration_Desc,
                getValue: () => minBuffDuration,
                setValue: value => minBuffDuration = value,
                min: 1,
                max: 20,
                interval: 1
            );
            configMenu.AddNumberOption(
                mod: ModEntry.Instance.ModManifest,
                name: I18n.CfgMaxDuration_Name,
                tooltip: I18n.CfgMaxDuration_Desc,
                getValue: () => maxBuffDuration,
                setValue: value => maxBuffDuration = value,
                min: 1,
                max: 20,
                interval: 1
            );

            // Weight Config
            configMenu.AddSectionTitle(
                mod: ModEntry.Instance.ModManifest,
                text: I18n.CfgCategoryWeight_Name
            );

            configMenu.AddNumberOption(
                mod: ModEntry.Instance.ModManifest,
                name: I18n.CfgMajorBoonWeight_Name,
                tooltip: I18n.CfgMajorBoonWeight_Desc,
                getValue: () => weight[4],
                setValue: value => weight[4] = value,
                min: 0,
                max: 50,
                interval: 1
            );

            configMenu.AddNumberOption(
                mod: ModEntry.Instance.ModManifest,
                name: I18n.CfgMinorBoonWeight_Name,
                tooltip: I18n.CfgMinorBoonWeight_Desc,
                getValue: () => weight[3],
                setValue: value => weight[3] = value,
                min: 0,
                max: 50,
                interval: 1
            );

            configMenu.AddNumberOption(
                mod: ModEntry.Instance.ModManifest,
                name: I18n.CfgNothingWeight_Name,
                tooltip: I18n.CfgNothingWeight_Desc,
                getValue: () => weight[2],
                setValue: value => weight[2] = value,
                min: 0,
                max: 50,
                interval: 1
            );

            configMenu.AddNumberOption(
                mod: ModEntry.Instance.ModManifest,
                name: I18n.CfgMinorBaneWeight_Name,
                tooltip: I18n.CfgMinorBaneWeight_Desc,
                getValue: () => weight[1],
                setValue: value => weight[1] = value,
                min: 0,
                max: 50,
                interval: 1
            );

            configMenu.AddNumberOption(
                mod: ModEntry.Instance.ModManifest,
                name: I18n.CfgMajorBaneWeight_Name,
                tooltip: I18n.CfgMajorBaneWeight_Desc,
                getValue: () => weight[0],
                setValue: value => weight[0] = value,
                min: 0,
                max: 50,
                interval: 1
            );

            // Toggle on Specific Buff and Debuff
            configMenu.AddSectionTitle(
                mod: ModEntry.Instance.ModManifest,
                text: I18n.CfgMajorBoonSetting_Name
            );

            foreach (BuffNames.MajorBoon majorBoon in Enum.GetValues(typeof(BuffNames.MajorBoon)).Cast<BuffNames.MajorBoon>().ToList())
            {
                configMenu.AddBoolOption(
                    mod: ModEntry.Instance.ModManifest,
                    name: BuffNames.majorBoonData[majorBoon].configName,
                    tooltip: BuffNames.majorBoonData[majorBoon].configDesc,
                    getValue: () => enabledMajorBoons.Contains(majorBoon),
                    setValue: value =>
                    {
                        if (!value && enabledMajorBoons.Contains(majorBoon))
                            enabledMajorBoons.Remove(majorBoon);
                        else if (value && !enabledMajorBoons.Contains(majorBoon))
                            enabledMajorBoons.Add(majorBoon);
                    }
                );
            }

            configMenu.AddSectionTitle(
                mod: ModEntry.Instance.ModManifest,
                text: I18n.CfgMinorBoonSetting_Name
            );

            foreach (BuffNames.MinorBoon minorBoon in Enum.GetValues(typeof(BuffNames.MinorBoon)).Cast<BuffNames.MinorBoon>().ToList())
            {
                configMenu.AddBoolOption(
                    mod: ModEntry.Instance.ModManifest,
                    name: BuffNames.minorBoonData[minorBoon].configName,
                    tooltip: BuffNames.minorBoonData[minorBoon].configDesc,
                    getValue: () => enabledMinorBoons.Contains(minorBoon),
                    setValue: value =>
                    {
                        if (!value && enabledMinorBoons.Contains(minorBoon))
                            enabledMinorBoons.Remove(minorBoon);
                        else if (value && !enabledMinorBoons.Contains(minorBoon))
                            enabledMinorBoons.Add(minorBoon);
                    }
                );
            }

            configMenu.AddSectionTitle(
                mod: ModEntry.Instance.ModManifest,
                text: I18n.CfgMinorBaneSetting_Name
            );

            foreach (BuffNames.MinorBane minorBane in Enum.GetValues(typeof(BuffNames.MinorBane)).Cast<BuffNames.MinorBane>().ToList())
            {
                configMenu.AddBoolOption(
                    mod: ModEntry.Instance.ModManifest,
                    name: BuffNames.minorBaneData[minorBane].configName,
                    tooltip: BuffNames.minorBaneData[minorBane].configDesc,
                    getValue: () => enabledMinorBanes.Contains(minorBane),
                    setValue: value =>
                    {
                        if (!value && enabledMinorBanes.Contains(minorBane))
                            enabledMinorBanes.Remove(minorBane);
                        else if (value && !enabledMinorBanes.Contains(minorBane))
                            enabledMinorBanes.Add(minorBane);
                    }
                );
            }

            configMenu.AddSectionTitle(
                mod: ModEntry.Instance.ModManifest,
                text: I18n.CfgMajorBaneSetting_Name
            );

            foreach (BuffNames.MajorBane majorBane in Enum.GetValues(typeof(BuffNames.MajorBane)).Cast<BuffNames.MajorBane>().ToList())
            {
                configMenu.AddBoolOption(
                    mod: ModEntry.Instance.ModManifest,
                    name: BuffNames.majorBaneData[majorBane].configName,
                    tooltip: BuffNames.majorBaneData[majorBane].configDesc,
                    getValue: () => enabledMajorBanes.Contains(majorBane),
                    setValue: value =>
                    {
                        if (!value && enabledMajorBanes.Contains(majorBane))
                            enabledMajorBanes.Remove(majorBane);
                        else if (value && !enabledMajorBanes.Contains(majorBane))
                            enabledMajorBanes.Add(majorBane);
                    }
                );
            }

        }
    }
}
