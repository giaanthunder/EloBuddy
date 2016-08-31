using System.Net.Configuration;
using EloBuddy;
using EloBuddy.SDK;
using Settings = AddonTemplate.Config.Modes.Misc;

namespace AddonTemplate.Modes
{
    public sealed class Flee : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee);
        }

        public override void Execute()
        {
        	foreach(var buff in Player.Instance.Buffs)
        	{
        		Chat.Print(buff.Name);
        	}
        	if (SpellManager.E.IsReady() && Settings.EFlee)
            {
                var tempPos = Game.CursorPos;
                if (tempPos.IsInRange(Player.Instance.ServerPosition, SpellManager.E.Range))
                {
                    SpellManager.E.Cast(tempPos);
                }
                else
                {
                    SpellManager.E.Cast(Player.Instance.ServerPosition.Extend(tempPos, 450).To3DWorld());
                }
            }
        }
    }
}
