using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace NotBrand
{
    public static class SpellManager
    {
        public const int ConflagrationSpreadRange = 300;
        public const int PyroclasmSpreadRange = 600;

        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Skillshot W { get; private set; }
        public static Spell.Targeted E { get; private set; }
        public static Spell.Targeted R { get; private set; }

        static SpellManager()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1050, SkillShotType.Linear, 250, 1600, 70);
            W = new Spell.Skillshot(SpellSlot.W, 900, SkillShotType.Circular, 650, 0, 250);
            E = new Spell.Targeted(SpellSlot.E, 625);
            R = new Spell.Targeted(SpellSlot.R, 750);

            W.MinimumHitChance = HitChance.Medium;
            Q.MinimumHitChance = HitChance.Medium;
        }

        public static void Initialize()
        {
        }

        public static bool ShouldCast(bool allowAutos = true)
        {
            return !Player.Instance.Spellbook.IsCastingSpell ||
                   (!allowAutos || (Player.Instance.Spellbook.IsAutoAttacking && Orbwalker.CanBeAborted));
        }

        public static bool IsBlazed(this Obj_AI_Base target)
        {
            return target.HasBuff("BrandAblaze");
        }
    }
}
