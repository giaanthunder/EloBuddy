using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace AddonTemplate
{
    public static class SpellManager
    {
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Skillshot W { get; private set; }
        public static Spell.Skillshot E { get; private set; }
        public static Spell.Skillshot R { get; private set; }

        static SpellManager()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1100, SkillShotType.Linear, 250, 2000, 60);
            W = new Spell.Skillshot(SpellSlot.W, 1020, SkillShotType.Linear, 250, 1600, 80);
            E = new Spell.Skillshot(SpellSlot.E, 475, SkillShotType.Linear, 250, 2000, 80);
            R = new Spell.Skillshot(SpellSlot.R, 5000, SkillShotType.Linear, 1000, 2000, 160);
            Q.AllowedCollisionCount = 0;
            W.AllowedCollisionCount = Int32.MaxValue;
            R.AllowedCollisionCount = Int32.MaxValue;
        }

        public static void Initialize()
        {
        }

        public static HitChance PredQ()
        {
            var mode = Config.Modes.Misc.PredQ;
            switch (mode)
            {
                case 1:
                    return HitChance.Low;
                case 2:
                    return HitChance.Medium;
                case 3:
                    return HitChance.High;
            }
            return HitChance.Medium;
        }
        public static HitChance PredW()
        {
            var mode = Config.Modes.Misc.PredW;
            switch (mode)
            {
                case 1:
                    return HitChance.Low;
                case 2:
                    return HitChance.Medium;
                case 3:
                    return HitChance.High;
            }
            return HitChance.Medium;
        }
        public static HitChance PredR()
        {
            var mode = Config.Modes.Misc.PredR;
            switch (mode)
            {
                case 1:
                    return HitChance.Low;
                case 2:
                    return HitChance.Medium;
                case 3:
                    return HitChance.High;
            }
            return HitChance.Medium;
        }
    }
}
