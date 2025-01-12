using StardewValley;

namespace ExperienceConfig.Patcher
{
    internal static class ExperiencePatcher
    {
        internal static float[] LeftOverVanillaEXP = new float[]
        {
            0f, 0f, 0f, 0f, 0f, 0f
        };
        internal static Dictionary<string, float> LeftOverSpaceCoreEXP = new Dictionary<string, float>();

        public static void gainExperience_Prefix(Farmer __instance, int which, ref int howMuch)
        {
            if (!ModEntry.Instance.Config.IsEnabled)
                return;
            if (howMuch <= 0)
                return;

            float multiplier = 1f;

            if (which < 5)
                multiplier = ModEntry.Instance.Config.VanillaSkillMultiplier[which];

            LeftOverVanillaEXP[which] += multiplier * howMuch;
            howMuch = (int)LeftOverVanillaEXP[which];
            LeftOverVanillaEXP[which] -= howMuch;

        }



        public static void AddExperience_Prefix(Farmer farmer, string skillName, ref int amt)
        {
            if (!ModEntry.Instance.Config.IsEnabled)
                return;
            if (amt <= 0)
                return;

            float leftover = LeftOverSpaceCoreEXP.GetValueOrDefault(skillName, 0f);
            leftover += ModEntry.Instance.Config.SpaceCoreSkilMultiplier.GetValueOrDefault(skillName, 1f) * amt;
            amt = (int)leftover;
            LeftOverSpaceCoreEXP[skillName] = leftover - amt;
        }
    }
}
