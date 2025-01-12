using StardewModdingAPI;

namespace DailyBoonsAndBanes.Buffs
{
    internal sealed class BuffNames
    {
        internal sealed class BuffData
        {
            public Func<string> configName { get; set; }
            public Func<string> configDesc { get; set; }
        }

        internal static Dictionary<MajorBoon, BuffData> majorBoonData = new Dictionary<MajorBoon, BuffData> {
            { MajorBoon.BlazingFast, new BuffData{ configName = I18n.BuffBlazingfast_Name, configDesc = I18n.BuffBlazingfast_Desc } },
            { MajorBoon.Vicious, new BuffData{ configName = I18n.BuffVicious_Name, configDesc = I18n.BuffVicious_Desc } },
            { MajorBoon.RockSteady, new BuffData{ configName = I18n.BuffRocksteady_Name, configDesc = I18n.BuffRocksteady_Desc } },
            { MajorBoon.GreenThumb, new BuffData{ configName = I18n.BuffGreenthumb_Name, configDesc = I18n.BuffGreenthumb_Desc } },
            { MajorBoon.Thalassophilia, new BuffData{ configName = I18n.BuffThalassophilia_Name, configDesc = I18n.BuffThalassophilia_Desc } },
            { MajorBoon.NaturesGift, new BuffData{ configName = I18n.BuffNaturesgift_Name, configDesc = I18n.BuffNaturesgift_Desc } },
            { MajorBoon.RichVein, new BuffData{ configName = I18n.BuffRichVein_Name, configDesc = I18n.BuffRichVein_Desc } },
        };

        internal static Dictionary<MajorBane, BuffData> majorBaneData = new Dictionary<MajorBane, BuffData> {
            { MajorBane.Snail, new BuffData{ configName = I18n.BuffSnail_Name, configDesc = I18n.BuffSnail_Desc } },
            { MajorBane.Feeble, new BuffData{ configName = I18n.BuffFeeble_Name, configDesc = I18n.BuffFeeble_Desc } },
            { MajorBane.Fragile, new BuffData{ configName = I18n.BuffFragile_Name, configDesc = I18n.BuffFragile_Desc } },
            { MajorBane.BlackThumb, new BuffData{ configName = I18n.BuffBlackthumb_Name, configDesc = I18n.BuffBlackthumb_Desc } },
            { MajorBane.Skunking, new BuffData{ configName = I18n.BuffSkunking_Name, configDesc = I18n.BuffSkunking_Desc } },
            { MajorBane.NaturesWrath, new BuffData{ configName = I18n.BuffNatureswrath_Name, configDesc = I18n.BuffNatureswrath_Desc } },
            { MajorBane.DesolatedMine, new BuffData{ configName = I18n.BuffDesolatedmine_Name, configDesc = I18n.BuffDesolatedmine_Desc } },
        };

        internal static Dictionary<MinorBoon, BuffData> minorBoonData = new Dictionary<MinorBoon, BuffData> {
            { MinorBoon.Speedy, new BuffData{ configName = I18n.BuffSpeedy_Name, configDesc = I18n.BuffSpeedy_Desc } },
            { MinorBoon.Strong, new BuffData{ configName = I18n.BuffStrong_Name, configDesc = I18n.BuffStrong_Desc } },
            { MinorBoon.Sturdy, new BuffData{ configName = I18n.BuffSturdy_Name, configDesc = I18n.BuffSturdy_Desc } },
            { MinorBoon.NatureTouched, new BuffData{ configName = I18n.BuffNaturetouched_Name, configDesc = I18n.BuffNaturetouched_Desc } },
            { MinorBoon.Angler, new BuffData{ configName = I18n.BuffAngler_Name, configDesc = I18n.BuffAngler_Desc } },
            { MinorBoon.Bountiful, new BuffData{ configName = I18n.BuffBountiful_Name, configDesc = I18n.BuffBountiful_Desc } },
            { MinorBoon.Digging, new BuffData{ configName = I18n.BuffDigging_Name, configDesc = I18n.BuffDigging_Desc } },
        };

        internal static Dictionary<MinorBane, BuffData> minorBaneData = new Dictionary<MinorBane, BuffData> {
            { MinorBane.Sluggish, new BuffData{ configName = I18n.BuffSluggish_Name, configDesc = I18n.BuffSluggish_Desc } },
            { MinorBane.Weak, new BuffData{ configName = I18n.BuffWeak_Name, configDesc = I18n.BuffWeak_Desc } },
            { MinorBane.Unsteady, new BuffData{ configName = I18n.BuffUnsteady_Name, configDesc = I18n.BuffUnsteady_Desc } },
            { MinorBane.Unearthly, new BuffData{ configName = I18n.BuffUnearthly_Name, configDesc = I18n.BuffUnearthly_Desc } },
            { MinorBane.Aquaphobic, new BuffData{ configName = I18n.BuffAquaphobic_Name, configDesc = I18n.BuffAquaphobic_Desc } },
            { MinorBane.EmptyHand, new BuffData{ configName = I18n.BuffEmptyHand_Name, configDesc = I18n.BuffEmptyHand_Desc } },
            { MinorBane.SparseDeposit, new BuffData{ configName = I18n.BuffSparsedeposit_Name, configDesc = I18n.BuffSparsedeposit_Desc } },
        };

        internal enum MajorBoon
        {
            BlazingFast,
            Vicious,
            RockSteady,
            GreenThumb,
            Thalassophilia,
            NaturesGift,
            RichVein,
        }
        internal enum MinorBoon
        {
            Speedy,
            Strong,
            Sturdy,
            NatureTouched,
            Angler,
            Bountiful,
            Digging,

        }
        internal enum MinorBane
        {
            Sluggish,
            Weak,
            Unsteady,
            Unearthly,
            Aquaphobic,
            EmptyHand,
            SparseDeposit,
        }
        internal enum MajorBane
        {
            Snail,
            Feeble,
            Fragile,
            BlackThumb,
            Skunking,
            NaturesWrath,
            DesolatedMine,
        }
    }
}
