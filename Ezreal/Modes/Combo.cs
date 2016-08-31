using System;
using System.Collections.Generic;
using System.Linq;
using AddonTemplate;
using AddonTemplate.Modes;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Constants;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using Settings = AddonTemplate.Config.Modes.Combo;
using myPrediction;
using pred=myPrediction.myPrediction;

namespace AddonTemplate.Modes
{


    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        private void ItemUsage()
        {
            var target = TargetSelector.GetTarget(550, DamageType.Physical); // 550 = Botrk.Range
            if (Settings.UseYoumuu && Config.Youmuu.IsOwned() && Config.Youmuu.IsReady())
            {
                Config.Youmuu.Cast();
            }
            if (target != null)
            {
                if (Settings.useBotrk && Item.HasItem(Config.Cutlass.Id) && Item.CanUseItem(Config.Cutlass.Id))
                {
                    Item.UseItem(Config.Cutlass.Id, target);
                }
                if (Settings.useBotrk && Item.HasItem(Config.Botrk.Id) && Item.CanUseItem(Config.Botrk.Id))
                {
                    Config.Botrk.Cast(target);
                }
            }
        }

        public override void Execute()
        {
            ItemUsage();
            if(!Orbwalker.CanMove)return;
            if (Settings.UseQ && Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Q.Range - 50, DamageType.Physical);
                if (target != null && target.IsValidTarget())
                {
					var hitchance = pred.GetHitChance(SpellSlot.Q, 1100, SkillShotType.Linear, 250, 2000, 60,Player.Instance.ServerPosition,target);
		            if (hitchance >= SpellManager.PredQ())
		            {
		                pred.CastPredictedSpell(SpellSlot.Q, 1100, SkillShotType.Linear, 250, 2000, 60,target, true, false);
		            }
                }
            }
            if (Settings.UseW && W.IsReady())
            {
                var target = TargetSelector.GetTarget(W.Range - 50, DamageType.Physical);
                if (target != null && target.IsValidTarget())
                {
					var hitchance = pred.GetHitChance(SpellSlot.W, 1020, SkillShotType.Linear, 250, 1600, 80,Player.Instance.ServerPosition,target);
		            if (hitchance >= SpellManager.PredW())
		            {
		                pred.CastPredictedSpell(SpellSlot.W, 1020, SkillShotType.Linear, 250, 1600, 80,target, false, false);
		            }
                }
            }
            if (Settings.UseE && E.IsReady())
            {
                var target = TargetSelector.GetTarget(E.Range, DamageType.Physical);
                if (target != null && target.IsValidTarget())
                {
                    E.Cast(Game.CursorPos);
                }
            }
            if (Settings.UseRSeveral)
            {
                foreach (var hero in EntityManager.Heroes.Enemies.Where(hero => hero.IsValidTarget(5000)))
                {
                    if (R.IsReady())
                    {
                        var collision = new List<AIHeroClient>();
                        var startPos = Player.Instance.ServerPosition.To2D();
                        var endPos = hero.ServerPosition.To2D();
                        collision.Clear();
                        foreach (
                            var colliHero in
                                EntityManager.Heroes.Enemies.Where(
                                    colliHero =>
                                        !colliHero.IsDead && colliHero.IsVisible &&
                                        colliHero.IsInRange(hero, 5000) && colliHero.IsValidTarget(5000)))
                        {
                            if (Prediction.Position.Collision.LinearMissileCollision(colliHero, startPos, endPos,
                                SpellManager.R.Speed, SpellManager.R.Width, SpellManager.R.CastDelay))
                            {
                                collision.Add(colliHero);
                            }
                            if (collision.Count >= Settings.NumberR)
                            {
                                R.Cast(hero);
                            }
                        }
                    }
                }
            }
        }
    }
}
