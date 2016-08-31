using System;
using System.Linq;
using System.Threading;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using myPrediction;
using pred=myPrediction.myPrediction;


namespace AddonTemplate
{
    internal class GameEvent
    {   	
    	public static void ObjTurret_OnTurretDamage(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender is Obj_AI_Turret && sender.IsAlly && Player.Instance.ManaPercent > Config.Modes.Clear.ManaQ)
            {
                Obj_AI_Base cs = (Obj_AI_Base)ObjectManager.GetUnitByNetworkId((uint)args.Target.NetworkId);
                if (Player.Instance.GetAutoAttackDamage(cs) < cs.Health &&
                    Player.Instance.GetSpellDamage(cs, SpellSlot.Q) > cs.Health && Player.Instance.CanAttack)
                {
                    string[] innerTurret = {
                        "TurreT_T2_C_01_A",
                        "TurreT_T2_C_02_A",
                        "TurreT_T2_L_01_A",
                        "TurreT_T2_C_03_A",
                        "TurreT_T2_R_01_A",
                        "TurreT_T1_C_01_A",
                        "TurreT_T1_C_02_A",
                        "TurreT_T1_C_06_A",
                        "TurreT_T1_C_03_A",
                        "TurreT_T1_C_07_A"
                    };
                    if (!innerTurret.Any(sender.Name.Contains) && Config.Modes.Misc.UseQUnderTurret &&
                        Player.Instance.GetSpellDamage(cs, SpellSlot.Q) >= cs.Health)
                    {
                        SpellManager.Q.Cast(cs);
                    }
                }
            }
        }

        public static void SelfW_OnValueChanged(ValueBase<bool> sender, ValueBase<bool>.ValueChangeArgs args)
        {
            if (args.NewValue && SpellManager.W.IsReady() && SpellManager.E.IsReady())
            {
                var tempPos = Game.CursorPos;
                
                SpellManager.W.Cast(tempPos);
                if (tempPos.IsInRange(Player.Instance.ServerPosition, SpellManager.E.Range))
                {
                    Core.DelayAction(() => SpellManager.E.Cast(tempPos), 120); 
                }
                else
                {
                    Core.DelayAction(() => SpellManager.E.Cast(Player.Instance.ServerPosition.Extend(tempPos, 450).To3DWorld()), 500);
                }
                Config.Modes.Misc._SelfW.CurrentValue = false;
            }
        }

        public static void Gapcloser_OnGapCloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs gapcloser)
        {
            
        	string[] herogapcloser =
            {
                "Braum", "Ekko", "Elise", "Fiora", "Kindred", "Lucian", "Yi", "Nidalee", "Quinn", "Riven", "Shaco", "Sion", "Vayne", "Yasuo", "Graves", "Azir", "Gnar", "Irelia", "Kalista"
            };
            if (Config.Modes.Misc.EGapClos && sender.IsEnemy && sender.GetAutoAttackRange() >= ObjectManager.Player.Distance(gapcloser.End) && !herogapcloser.Any(sender.ChampionName.Contains))
            {
                var diffGapCloser = gapcloser.End - gapcloser.Start;
                SpellManager.E.Cast(ObjectManager.Player.ServerPosition + diffGapCloser);
            }
        }

        public static void OnDraw(EventArgs args)
        {
        	if ((Config.Modes.Draw.DrawQ && !Config.Modes.Draw.OnlyRdy) || (Config.Modes.Draw.OnlyRdy && !SpellManager.Q.IsOnCooldown && Config.Modes.Draw.DrawQ && SpellManager.Q.IsLearned))
                Circle.Draw(Color.Red, SpellManager.Q.Range, Player.Instance.ServerPosition);
            if ((Config.Modes.Draw.DrawW && !Config.Modes.Draw.OnlyRdy) || (Config.Modes.Draw.OnlyRdy && !SpellManager.W.IsOnCooldown && Config.Modes.Draw.DrawW && SpellManager.W.IsLearned))
                Circle.Draw(Color.BlueViolet, SpellManager.W.Range, Player.Instance.ServerPosition);
            if ((Config.Modes.Draw.DrawE && !Config.Modes.Draw.OnlyRdy) || (Config.Modes.Draw.OnlyRdy && !SpellManager.E.IsOnCooldown && Config.Modes.Draw.DrawE && SpellManager.E.IsLearned))
            	Circle.Draw(Color.Yellow, SpellManager.E.Range, Player.Instance.ServerPosition);
        }
    }
}
