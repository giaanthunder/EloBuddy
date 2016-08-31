using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace AddonTemplate.Modes
{
    public sealed class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear));
        }

        public override void Execute()
        {
            if (Player.Instance.ManaPercent > Config.Modes.Clear.ManaQ && Q.IsReady())
            {
                foreach (var minion in EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy,
            	                                                                       Player.Instance.ServerPosition, SpellManager.Q.Range))
                {
                    if (
        				(
            				Config.Modes.Misc.UseQOnUnkillable &&
            				Prediction.Health.GetPrediction(minion, (int)(Player.Instance.AttackDelay * 1000)) <= 0	&&
							(!Orbwalker.CanAutoAttack || !Player.Instance.IsInAutoAttackRange(minion)) &&
							Orbwalker.CanMove && Orbwalker.LastTarget.NetworkId != minion.NetworkId
						)
            			||
            			(
            				Config.Modes.Clear.UseQLastHit && 
            				(Config.Tear.IsOwned() || Config.Manamune.IsOwned() || Config.Muramana.IsOwned())&&
            				Player.Instance.GetSpellDamage(minion,SpellSlot.Q)>minion.Health&&
            				Orbwalker.CanMove && (Orbwalker.LastTarget.NetworkId != minion.NetworkId || Orbwalker.ShouldWait)
            			)
            			||
            			(
            				Config.Modes.Clear.UseQLastHit && 
            				(Config.IceGaunlet.IsOwned() || Config.Trinity.IsOwned() || Config.LichBane.IsOwned())&&
            				Orbwalker.CanMove && Orbwalker.LastTarget.NetworkId != minion.NetworkId
            			)
            			||
            			(
            				Config.Modes.Clear.UseQLastHit && 
            				(minion.BaseSkinName.ToLower().Contains("siege") || minion.BaseSkinName.ToLower().Contains("super"))&&
            				Player.Instance.GetSpellDamage(minion,SpellSlot.Q)>minion.Health&&
            				Orbwalker.CanMove && Orbwalker.CanAutoAttack
            			)
                       )
                    {
        				var predMinion = Q.GetPrediction(minion);
        				if(predMinion.HitChance != HitChance.Collision || predMinion.HitChance != HitChance.Impossible)
        					Q.Cast(predMinion.UnitPosition);
                    }
                }
            }

            if (Config.Modes.Clear.UseWOnAlly && W.IsReady() && Player.Instance.ManaPercent > Config.Modes.Clear.ManaW)
            {
                var heroes = EntityManager.Heroes.Allies;
                var collision = new List<AIHeroClient>();

                var startPos = Player.Instance.ServerPosition.To2D();

                foreach (var hero in heroes.Where(hero => !hero.IsDead))
                {
                    if (hero.ServerPosition.Distance(Player.Instance.ServerPosition.To2D()) <= SpellManager.W.Range)
                    {
                        var endPos = startPos.Extend(hero.ServerPosition.To2D(), SpellManager.W.Range);
                        if (Prediction.Position.Collision.LinearMissileCollision(hero, startPos, endPos,
                            SpellManager.W.Speed, SpellManager.W.Width, SpellManager.W.CastDelay))
                        {
                            collision.Add(hero);
                        }
                        if (collision.Count - 1 >= Config.Modes.Clear.NumberW)
                            W.Cast(W.GetPrediction(hero).CastPosition);
                    }
                }
            }
        }
    }
}
