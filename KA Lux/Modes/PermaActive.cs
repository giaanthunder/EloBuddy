using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using KA_Lux.DMGHandler;
using myPrediction;
using pred=myPrediction.myPrediction;

using Settings = KA_Lux.Config.Modes.Misc;

namespace KA_Lux.Modes
{
    public sealed class PermaActive : ModeBase
    {
        public static bool CastedE;
        public override bool ShouldBeExecuted()
        {
            return true;
        }

        public override void Execute()
        {
            if (CastedE)
            {
                if (Player.Instance.Spellbook.GetSpell(SpellSlot.E).ToggleState == 2 || Player.Instance.Spellbook.GetSpell(SpellSlot.E).ToggleState == 1)
                {
                    E.Cast(Player.Instance);
                    CastedE = false;
                }
                else
                {
                    CastedE = false;
                }
            }

            if (R.IsReady() && Settings.KillStealR && Player.Instance.ManaPercent >= Settings.KillStealMana)
            {
                var targetR = TargetSelector.GetTarget(R.Range, DamageType.Magical);
                if (targetR != null && !targetR.IsZombie && !targetR.HasUndyingBuff() && targetR.CountAlliesInRange(1000) < 3)
                {
                    var predHealth = Prediction.Health.GetPrediction(targetR, 1000);
                    if (predHealth <= SpellDamage.GetRealDamage(SpellSlot.R, targetR)
                        && predHealth >+ targetR.CountAlliesInRange(1000) * 50)
                    {
                        if (targetR.HasBuffOfType(BuffType.Snare) || targetR.HasBuffOfType(BuffType.Stun))
                        {
                            R.Cast(targetR.Position);
                        }
                        else
                        {
//                            var pred = R.GetPrediction(targetR);
//                            if (pred.HitChancePercent >= 85)
//                            {
//                                R.Cast(pred.CastPosition);
//                            }
						var hitchance = pred.GetHitChance(SpellSlot.R, 3300, SkillShotType.Circular, 1, 0, 110,Player.Instance.ServerPosition,targetR);
			            if (hitchance >= HitChance.High)
			            {
			                pred.CastPredictedSpell(SpellSlot.R, 3300, SkillShotType.Circular, 1, 0, 110,targetR, false, false);
			            }
                        }
                    }
                }
            }

            if (W.IsReady() && Settings.WDefense && Player.Instance.Mana >= Settings.WMana)
            {
                if (Player.Instance.InDanger(85))
                {
                    W.Cast(Player.Instance);
                }

                var ally = EntityManager.Heroes.Allies.FirstOrDefault(a => a.InDanger(45));
                if (ally != null)
                {
                    W.Cast(ally.Position);
                }
            }
            
            if (Q.IsReady() && Settings.KillStealQ && Player.Instance.ManaPercent >= Settings.KillStealMana)
            {
                var targetQ = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                if (targetQ != null && !targetQ.IsZombie && !targetQ.HasUndyingBuff())
                {

                    if (Prediction.Health.GetPrediction(targetQ, Q.CastDelay) <= SpellDamage.GetRealDamage(SpellSlot.Q, targetQ) &&
                        !targetQ.IsInAutoAttackRange(Player.Instance) && targetQ.Health > Player.Instance.GetAutoAttackDamage(targetQ))
                    {
//                        Q.Cast(Q.GetPrediction(targetQ).CastPosition);
				var hitchance = pred.GetHitChance(SpellSlot.Q, 1300, SkillShotType.Linear, .250f, 1200, 70,Player.Instance.ServerPosition,targetQ);
	            if (hitchance >= HitChance.High)
	            {
	                pred.CastPredictedSpell(SpellSlot.Q, 1300, SkillShotType.Linear, .250f, 1200, 70,targetQ, true, false);
	            }
                    }
                }
            }
            
            if (E.IsReady() && Settings.KillStealE && Player.Instance.ManaPercent >= Settings.KillStealMana)
            {
                var targetE = TargetSelector.GetTarget(E.Range, DamageType.Magical);
                if (targetE != null && !targetE.IsZombie && !targetE.HasUndyingBuff())
                {
                    if (Prediction.Health.GetPrediction(targetE, E.CastDelay) <= SpellDamage.GetRealDamage(SpellSlot.E, targetE) &&
                        !targetE.IsInAutoAttackRange(Player.Instance) && targetE.Health > Player.Instance.GetAutoAttackDamage(targetE))
                    {
//                        E.Cast(E.GetPrediction(targetE).CastPosition);
				var hitchance = pred.GetHitChance(SpellSlot.E, 1100, SkillShotType.Circular, .250f, 1400, 335,Player.Instance.ServerPosition,targetE);
	            if (hitchance >= HitChance.High)
	            {
	                pred.CastPredictedSpell(SpellSlot.E, 1100, SkillShotType.Circular, .250f, 1400, 335,targetE, false, false);
	            }
                    }
                }
            }
            //JungleSteal
            if (R.IsReady() && Settings.JungleSteal)
            {
                var targetR = TargetSelector.GetTarget(R.Range, DamageType.Magical);
                if (targetR != null)
                {
                    if (Settings.JungleStealBlue)
                    {
                        var blue =
                            EntityManager.MinionsAndMonsters.GetJungleMonsters()
                                .FirstOrDefault(
                                    m =>
                                        Prediction.Health.GetPrediction(m, R.CastDelay) <=
                                        SpellDamage.GetRealDamage(SpellSlot.R, m) &&
                                        m.IsValidTarget(R.Range) &&
                                        m.BaseSkinName == "SRU_Blue" && m.IsInRange(targetR, 1500) &&
                                        Prediction.Health.GetPrediction(m, 1000) > m.CountEnemiesInRange(1000) * 60);
                        if (blue != null)
                        {
                            var pred = R.GetPrediction(blue);
                            if (pred.HitChance >= HitChance.High)
                            {
                                R.Cast(pred.CastPosition);
                            }
                        }
                    }

                    if (Settings.JungleStealRed)
                    {
                        var red =
                            EntityManager.MinionsAndMonsters.GetJungleMonsters()
                                .FirstOrDefault(
                                    m =>
                                        Prediction.Health.GetPrediction(m, R.CastDelay) <
                                        SpellDamage.GetRealDamage(SpellSlot.R, m) &&
                                        m.IsValidTarget(R.Range) &&
                                        m.BaseSkinName == "SRU_Red" && m.IsInRange(targetR, 1500) &&
                                        Prediction.Health.GetPrediction(m, 1000) > m.CountEnemiesInRange(1000) * 60);
                        if (red != null)
                        {
                            var pred = R.GetPrediction(red);
                            if (pred.HitChance >= HitChance.High)
                            {
                                R.Cast(pred.CastPosition);
                            }
                        }
                    }

                    if (Settings.JungleStealDrag)
                    {
                        var drag =
                            EntityManager.MinionsAndMonsters.GetJungleMonsters()
                                .FirstOrDefault(
                                    m =>
                                        Prediction.Health.GetPrediction(m, R.CastDelay) <
                                        SpellDamage.GetRealDamage(SpellSlot.R, m) &&
                                        m.IsValidTarget(R.Range) &&
                                        m.BaseSkinName == "SRU_Dragon" && m.IsInRange(targetR, 1500) &&
                                        Prediction.Health.GetPrediction(m, 1000) > m.CountEnemiesInRange(1000) * 60);

                        if (drag != null)
                        {
                            var pred = R.GetPrediction(drag);
                            if (pred.HitChance >= HitChance.High)
                            {
                                R.Cast(pred.CastPosition);
                            }
                        }
                    }

                    if (Settings.JungleStealBaron)
                    {
                        var baron =
                            EntityManager.MinionsAndMonsters.GetJungleMonsters()
                                .FirstOrDefault(
                                    m =>
                                        Prediction.Health.GetPrediction(m, R.CastDelay) <
                                        SpellDamage.GetRealDamage(SpellSlot.R, m) &&
                                        m.IsValidTarget(R.Range) &&
                                        m.BaseSkinName == "SRU_Baron" && m.IsInRange(targetR, 1500) &&
                                        Prediction.Health.GetPrediction(m, 1000) > m.CountEnemiesInRange(1000) * 60);

                        if (baron != null)
                        {
                            var pred = R.GetPrediction(baron);
                            if (pred.HitChance >= HitChance.High)
                            {
                                R.Cast(pred.CastPosition);
                            }
                        }
                    }
                }
            }
            
            //====================================Auto Harass================================================
          	var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            if (target == null || target.IsZombie) return;

            if (Q.IsReady() && target.IsValidTarget(Q.Range) && KA_Lux.Config.Modes.Harass.UseQ)
            {
				var hitchance = pred.GetHitChance(SpellSlot.Q, 1300, SkillShotType.Linear, .250f, 1200, 70,Player.Instance.ServerPosition,target);
	            if (hitchance >= HitChance.High)
	            {
	                pred.CastPredictedSpell(SpellSlot.Q, 1300, SkillShotType.Linear, .250f, 1200, 70,target, true, false);
	            }
            }

            if (E.IsReady() && target.IsValidTarget(E.Range) && KA_Lux.Config.Modes.Harass.UseE)
            {
				var hitchance = pred.GetHitChance(SpellSlot.E, 1100, SkillShotType.Circular, .250f, 1400, 335,Player.Instance.ServerPosition,target);
	            if (hitchance >= HitChance.High)
	            {
	                pred.CastPredictedSpell(SpellSlot.E, 1100, SkillShotType.Circular, .250f, 1400, 335,target, false, false);
	            }
                PermaActive.CastedE = true;
            }
            //===============================================================================================
        }
    }
}
