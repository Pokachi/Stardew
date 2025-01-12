using StardewValley;

namespace DailyBoonsAndBanes.Buffs
{
    internal static class BuffHelper
    {
        internal static BuffWithMessage createBoon(BuffNames.MajorBoon majorBoon)
        {
            int duration = new Random().Next(ModEntry.Instance.Config.minBuffDuration, ModEntry.Instance.Config.maxBuffDuration + 1);
            
            if (duration >= 20)
                duration = Buff.ENDLESS;
            else
                duration = duration * 42 * 1000;

            switch (majorBoon)
            {
                case BuffNames.MajorBoon.BlazingFast:
                    return new BlazingFast(duration);
                case BuffNames.MajorBoon.Vicious:
                    return new Vicious(duration);
                case BuffNames.MajorBoon.RockSteady:
                    return new RockSteady(duration);
                case BuffNames.MajorBoon.GreenThumb:
                    return new GreenThumb(duration);
                case BuffNames.MajorBoon.Thalassophilia:
                    return new Thalassophilia(duration);
                case BuffNames.MajorBoon.NaturesGift:
                    return new NaturesGift(duration);
                case BuffNames.MajorBoon.RichVein:
                    return new RichVein(duration);
                default:
                    // Should not happen
                    return null;
            }
        }

        internal static BuffWithMessage createBoon(BuffNames.MinorBoon minorBoon)
        {
            int duration = new Random().Next(ModEntry.Instance.Config.minBuffDuration, ModEntry.Instance.Config.maxBuffDuration + 1);
            
            if (duration >= 20)
                duration = Buff.ENDLESS;
            else
                duration = duration * 42 * 1000;

            switch (minorBoon)
            {
                case BuffNames.MinorBoon.Speedy:
                    return new Speedy(duration);
                case BuffNames.MinorBoon.Strong:
                    return new Strong(duration);
                case BuffNames.MinorBoon.Sturdy:
                    return new Sturdy(duration);
                case BuffNames.MinorBoon.NatureTouched:
                    return new NatureTouched(duration);
                case BuffNames.MinorBoon.Angler:
                    return new Angler(duration);
                case BuffNames.MinorBoon.Bountiful:
                    return new Bountiful(duration);
                case BuffNames.MinorBoon.Digging:
                    return new Digging(duration);
                default:
                    // Should not happen
                    return null;
            }
        }

        internal static BuffWithMessage createBoon(BuffNames.MajorBane majorBane)
        {
            int duration = new Random().Next(ModEntry.Instance.Config.minBuffDuration, ModEntry.Instance.Config.maxBuffDuration + 1);
            
            if (duration >= 20)
                duration = Buff.ENDLESS;
            else
                duration = duration * 42 * 1000;

            switch (majorBane)
            {
                case BuffNames.MajorBane.Snail:
                    return new Snail(duration);
                case BuffNames.MajorBane.Feeble:
                    return new Feeble(duration);
                case BuffNames.MajorBane.Fragile:
                    return new Fragile(duration);
                case BuffNames.MajorBane.BlackThumb:
                    return new BlackThumb(duration);
                case BuffNames.MajorBane.Skunking:
                    return new Skunking(duration);
                case BuffNames.MajorBane.NaturesWrath:
                    return new NaturesWrath(duration);
                case BuffNames.MajorBane.DesolatedMine:
                    return new DesolatedMine(duration);
                default:
                    // Should not happen
                    return null;
            }
        }

        internal static BuffWithMessage createBoon(BuffNames.MinorBane minorBane)
        {
            int duration = new Random().Next(ModEntry.Instance.Config.minBuffDuration, ModEntry.Instance.Config.maxBuffDuration + 1);
            
            if (duration >= 20)
                duration = Buff.ENDLESS;
            else
                duration = duration * 42 * 1000;

            switch (minorBane)
            {
                case BuffNames.MinorBane.Sluggish:
                    return new Sluggish(duration);
                case BuffNames.MinorBane.Weak:
                    return new Weak(duration);
                case BuffNames.MinorBane.Unsteady:
                    return new Unsteady(duration);
                case BuffNames.MinorBane.Unearthly:
                    return new Unearthly(duration);
                case BuffNames.MinorBane.Aquaphobic:
                    return new Aquaphobic(duration);
                case BuffNames.MinorBane.EmptyHand:
                    return new EmptyHand(duration);
                case BuffNames.MinorBane.SparseDeposit:
                    return new SparseDesposite(duration);
                default:
                    // Should not happen
                    return null;
            }
        }
    }
}
