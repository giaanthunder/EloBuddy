using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Settings = KA_Lux.Config.Modes.Harass;
using myPrediction;
using pred=myPrediction.myPrediction;

namespace KA_Lux.Modes
{
    public sealed class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute()
        {
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            if (target == null || target.IsZombie) return;

            if (Q.IsReady() && target.IsValidTarget(Q.Range) && Settings.UseQ)
            {
//                var pred = Q.GetPrediction(target);
//                if (pred.HitChancePercent >= 75)
//                {
//                    Q.Cast(pred.CastPosition);
//                }
				var hitchance = pred.GetHitChance(SpellSlot.Q, 1300, SkillShotType.Linear, .250f, 1200, 70,Player.Instance.ServerPosition,target);
	            if (hitchance >= HitChance.High)
	            {
	                pred.CastPredictedSpell(SpellSlot.Q, 1300, SkillShotType.Linear, .250f, 1200, 70,target, true, false);
	            }
            }

            if (E.IsReady() && target.IsValidTarget(E.Range) && Settings.UseE)
            {
//                E.Cast(E.GetPrediction(target).CastPosition);
				var hitchance = pred.GetHitChance(SpellSlot.E, 1100, SkillShotType.Circular, .250f, 1400, 335,Player.Instance.ServerPosition,target);
	            if (hitchance >= HitChance.High)
	            {
	                pred.CastPredictedSpell(SpellSlot.E, 1100, SkillShotType.Circular, .250f, 1400, 335,target, false, false);
	            }
                PermaActive.CastedE = true;
            }
        }
    }
}
