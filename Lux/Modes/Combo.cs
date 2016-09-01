using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Settings = Lux.Config.Modes.Combo;
using myPrediction;
using pred=myPrediction.myPrediction;


namespace Lux.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
            if (target == null || target.IsZombie) return;

            if (Q.IsReady() && target.IsValidTarget(Q.Range) && Settings.UseQ)
            {
//                var pred = Q.GetPrediction(target);
//                if (pred.HitChancePercent >= 80)
//                {
//                    Q.Cast(pred.CastPosition);
//                }
				var hitchance = pred.GetHitChance(SpellSlot.Q, 1300, SkillShotType.Linear, .250f, 1200, 70,Player.Instance.ServerPosition,target);
	            if (hitchance >= HitChance.High)
	            {
	                pred.CastPredictedSpell(SpellSlot.Q, 1300, SkillShotType.Linear, .250f, 1200, 70,target, true, false);
	            }

	
            }

            if (E.IsReady() && target.IsValidTarget(E.Range) && Settings.UseE && Settings.UseESnared
                ? target.HasBuffOfType(BuffType.Snare)
                : target.IsValidTarget(E.Range))
            {
//                var pred = E.GetPrediction(target);
//                if (pred.HitChancePercent >= 70)
//                {
//                    E.Cast(pred.CastPosition);
//                }
				var hitchance = pred.GetHitChance(SpellSlot.E, 1100, SkillShotType.Circular, .250f, 1400, 335,Player.Instance.ServerPosition,target);
	            if (hitchance >= HitChance.High)
	            {
	                pred.CastPredictedSpell(SpellSlot.E, 1100, SkillShotType.Circular, .250f, 1400, 335,target, false, false);
	            }
                PermaActive.CastedE = true;
            }

            if (R.IsReady() && Settings.UseR)
            {
                var targetR = TargetSelector.GetTarget(R.Range, DamageType.Magical);
                if(targetR == null)return;

                if (target.IsValidTarget(R.Range) && Settings.UseRSnared
                    ? target.HasBuffOfType(BuffType.Snare)
                    : target.IsValidTarget(R.Range) && Prediction.Health.GetPrediction(targetR, R.CastDelay) > 10)
                {
                    if (targetR.HasBuffOfType(BuffType.Snare) || targetR.HasBuffOfType(BuffType.Stun))
                    {
                        R.Cast(targetR.Position);
                    }
                    else
                    {
//                        var pred = R.GetPrediction(target);
//                        if (pred.HitChancePercent >= 95)
//                        {
//                            R.Cast(pred.CastPosition);
//                        }
						var hitchance = pred.GetHitChance(SpellSlot.R, 3300, SkillShotType.Circular, .1000f, 0, 110,Player.Instance.ServerPosition,target);
			            if (hitchance >= HitChance.High)
			            {
			                pred.CastPredictedSpell(SpellSlot.R, 3300, SkillShotType.Circular, .1000f, 0, 110,target, false, false);
			            }
                    }
                }
            }
        }
    }
}
