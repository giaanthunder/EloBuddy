using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace AddonTemplate.Modes
{
    public sealed class LastHit : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit);
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
            
        }
    }
}
