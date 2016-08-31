using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu.Values;
using Settings = AddonTemplate.Config.Modes.Harass;
using myPrediction;
using pred=myPrediction.myPrediction;


namespace AddonTemplate.Modes
{
    public sealed class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        { 
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute()
        {
            if(!Orbwalker.CanMove)return;
        	if (Settings.UseQ && Player.Instance.ManaPercent > Settings.ManaQ && Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Q.Range - 50, DamageType.Physical);
                if (target != null)
                {
					var hitchance = pred.GetHitChance(SpellSlot.Q, 1100, SkillShotType.Linear, 250, 2000, 60,Player.Instance.ServerPosition,target);
		            if (hitchance >= SpellManager.PredQ() && Config.Modes.MenuHarass[target.ChampionName + "harass"].Cast<CheckBox>().CurrentValue)
		            {
		                pred.CastPredictedSpell(SpellSlot.Q, 1100, SkillShotType.Linear, 250, 2000, 60,target, true, false);
		            }
                }
            }
            if (Settings.UseW && Player.Instance.ManaPercent > Settings.ManaW && W.IsReady())
            {
                var target = TargetSelector.GetTarget(W.Range - 50, DamageType.Physical);
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
}
