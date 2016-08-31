using EloBuddy;
using EloBuddy.SDK;

namespace AddonTemplate.Modes
{
    public sealed class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            foreach (var minion in EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.ServerPosition, Q.Range))
            {
                if (minion.IsValidTarget() && Player.Instance.ManaPercent > Config.Modes.Clear.ManaQ)
                {
                    Q.Cast(Q.GetPrediction(minion).CastPosition);
                }
            }
        }
    }
}
