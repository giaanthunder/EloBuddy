using System;
using System.Configuration;
using System.Linq;
using System.Threading;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;

using SharpDX;
using Settings = AddonTemplate.Config.Modes.KillSteal;
using myPrediction;
using pred=myPrediction.myPrediction;
using EloBuddy.SDK.Enumerations;

namespace AddonTemplate.Modes
{
    public sealed class PermaActive : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return true;
        }
		
        
        public override void Execute()
        {
        	if(pred.TickCount - Config.lastTick < Config.tickInterval)
        	{
	        	Config.lastTick = pred.TickCount;
	        	if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
	                return;
	            if (!Player.Instance.IsRecalling())
	            {
	                KsChamp();
	                AutoCCed();
	                AutoHarass();
	                StackTear();
	            }
        	}
        }

        public static void StackTear()
        {
            if (Config.Modes.Misc.AutoTear && Player.Instance.IsInShopRange())
            {
            	var target = TargetSelector.GetTarget(3000, DamageType.Physical);
            	if (target != null)return;
            	if ( Config.Tear.IsOwned() || Config.Manamune.IsOwned() )
                {
            		if(Q.IsReady())
            		{
            			Q.Cast(Game.CursorPos);
            			Core.DelayAction(() => W.Cast(Game.CursorPos),2300);
            		}
                }
            }
        }
        
        private static void AutoHarass()
        {
        	if (!Orbwalker.CanMove || Player.Instance.IsUnderEnemyturret())
            {
                return;
            }

            if (Config.Modes.Harass.ToggleQ && Player.Instance.ManaPercent > Config.Modes.Harass.ManaQ && Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Q.Range - 50, DamageType.Physical);
                if (target != null)
                {
					var hitchance = pred.GetHitChance(SpellSlot.Q, 1100, SkillShotType.Linear, 250, 2050, 60,Player.Instance.ServerPosition,target);
		            if (hitchance >= SpellManager.PredQ() && Config.Modes.MenuHarass[target.ChampionName + "harass"].Cast<CheckBox>().CurrentValue)
		            {
		                pred.CastPredictedSpell(SpellSlot.Q, 1100, SkillShotType.Linear, 250, 2050, 60,target, true, false);
		            }
                }
            }
            if (Config.Modes.Harass.ToggleW && Player.Instance.ManaPercent > Config.Modes.Harass.ManaW && W.IsReady())
            {
                var target = TargetSelector.GetTarget(W.Range - 50, DamageType.Physical);
                if (target != null)
                {
                    var predW = W.GetPrediction(target);
                    if (target != null)
                    {
						var hitchance = pred.GetHitChance(SpellSlot.W, 1020, SkillShotType.Linear, 250, 1600, 80,Player.Instance.ServerPosition,target);
			            if (hitchance >= SpellManager.PredW() && Config.Modes.MenuHarass[target.ChampionName + "harass"].Cast<CheckBox>().CurrentValue)
			            {
			                pred.CastPredictedSpell(SpellSlot.W, 1020, SkillShotType.Linear, 250, 1600, 80,target, false, false);
			            }
                    }
                }
            }
        }
        
        public static void KsChamp()
        {

            foreach (var enemy in EntityManager.Heroes.Enemies)
            {
            	if(enemy.IsDead)continue;
            	if (Settings.KsQ && Q.IsReady() && enemy.IsKillable(SpellSlot.Q) && enemy.IsValidTarget(Q.Range))
                {
					var hitchance = pred.GetHitChance(SpellSlot.Q, 1100, SkillShotType.Linear, 250, 2000, 60,Player.Instance.ServerPosition,enemy);
		            if (hitchance >= SpellManager.PredQ())
		            {
		                pred.CastPredictedSpell(SpellSlot.Q, 1100, SkillShotType.Linear, 250, 2000, 60,enemy, true, false);
		            }
                }

                if (Settings.KsW && W.IsReady() && enemy.IsKillable(SpellSlot.W) && enemy.IsValidTarget(W.Range))
                {
					var hitchance = pred.GetHitChance(SpellSlot.W, 1020, SkillShotType.Linear, 250, 1600, 80,Player.Instance.ServerPosition,enemy);
		            if (hitchance >= SpellManager.PredW())
		            {
		                pred.CastPredictedSpell(SpellSlot.W, 1020, SkillShotType.Linear, 250, 1600, 80,enemy, false, false);
		            }
                }

                var dist = Player.Instance.Distance(enemy.ServerPosition);
                if (Settings.KsR && enemy.IsKillable(SpellSlot.R) && enemy.ServerPosition.CountAlliesInRange(1000) <= 1 && enemy.IsKillable(SpellSlot.R) && dist > Settings.MinRRange && dist < Settings.MaxRRange)
                {
					var hitchance = pred.GetHitChance(SpellSlot.R, 5000, SkillShotType.Linear, 1000, 2000, 160,Player.Instance.ServerPosition,enemy);
		            if (hitchance >= SpellManager.PredR())
		            {
		                pred.CastPredictedSpell(SpellSlot.R, 5000, SkillShotType.Linear, 1000, 2000, 160,enemy, false, false);
		            }
                }

            	if(R.IsReady() && enemy.HasBuff("recall") && enemy.IsKillable(SpellSlot.R))
            	{
            		if(dist > Settings.MinRRange && dist < 10000)
	            	{
	            		R.Cast(enemy.ServerPosition);
	            	}
            	}
            }
        }

        public static void AutoCCed()
        {
        	if (!Player.Instance.IsUnderEnemyturret())
            {
                string[] hardCc =
                {
                    "Charm", "Fear", "Flee", "Knockup", "Polymorph", "Sleep", "Slow", "Snare", "Stun", "Suppression", "Taunt"
                };
                foreach (var enemy in EntityManager.Heroes.Enemies)
                {
                    foreach (var debuff in hardCc.Where(debuff => enemy.HasBuffOfType((BuffType)Enum.Parse(typeof(BuffType), debuff)))                        )
                    {
                        if (Config.Modes.Misc.CcQ && Q.IsReady())
                        {
                            Q.Cast(enemy);
                        }
                        if (Config.Modes.Misc.CcW && W.IsReady())
                        {
                            W.Cast(enemy);
                        }
                    }
                }
            }
        }
        
    }

}

