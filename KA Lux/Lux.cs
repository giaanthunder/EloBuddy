using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using KA_Lux;
using KA_Lux.DMGHandler;
using Settings = KA_Lux.Config.Modes.Draw;
using myPrediction;
using pred=myPrediction.myPrediction;
using SharpDX;
using Colordx = SharpDX.Color;
using Colors = System.Drawing.Color;

namespace KA_Lux
{
    internal class Lux
    {
        public static void Initialize()
        {
            if(Player.Instance.ChampionName != "Lux")return;

            SpellManager.Initialize();
            DamageHandler.Initialize();
            Config.Initialize();
            ModeManager.Initialize();
            DamageIndicator.Initialize(SpellDamage.GetTotalDamage);
            EventsManager.Initialize();

            Drawing.OnDraw += OnDraw;
        }

        private static void OnDraw(EventArgs args)
        {
            if (Settings.DrawQ && Settings.DrawReady ? SpellManager.Q.IsReady() : Settings.DrawQ)
            {
                Circle.Draw(Settings.QColor, SpellManager.Q.Range, 1f, Player.Instance);
            }

            if (Settings.DrawW && Settings.DrawReady ? SpellManager.W.IsReady() : Settings.DrawW)
            {
                Circle.Draw(Settings.WColor, SpellManager.W.Range, 1f, Player.Instance);
            }

            if (Settings.DrawE && Settings.DrawReady ? SpellManager.E.IsReady() : Settings.DrawE)
            {
                Circle.Draw(Settings.EColor, SpellManager.E.Range, 1f, Player.Instance);
            }

            if (Settings.DrawR && Settings.DrawReady ? SpellManager.R.IsReady() : Settings.DrawR)
            {
                Circle.Draw(Settings.RColor, SpellManager.R.Range, 1f, Player.Instance);
            }
//				foreach(var champ in ObjectManager.Get<AIHeroClient>())
//				{
//					var info = pred.UnitTracker.UnitTrackerInfoList.Find(x => x.NetworkId == champ.NetworkId);
//					Circle.Draw(Colordx.Blue,70,champ.Position);
//					Drawing.DrawText(champ.ServerPosition.WorldToScreen(), Colors.NavajoWhite, "NetworkId="+info.NetworkId+" -> "+champ.NetworkId, 10);
//					
//					Drawing.DrawText(champ.ServerPosition.WorldToScreen().X,champ.ServerPosition.WorldToScreen().Y+12, Colors.NavajoWhite, "AaTick="+info.AaTick, 10);
//					Drawing.DrawText(champ.ServerPosition.WorldToScreen().X,champ.ServerPosition.WorldToScreen().Y+24, Colors.NavajoWhite, "StopMoveTick="+info.StopMoveTick, 10);
//					Drawing.DrawText(champ.ServerPosition.WorldToScreen().X,champ.ServerPosition.WorldToScreen().Y+36, Colors.NavajoWhite, "NewPathTick="+info.NewPathTick, 10);
//					
//					Circle.Draw(Colordx.Red,30,info.PathBank[0].Position.To3D());
//					Circle.Draw(Colordx.Red,65,info.PathBank[1].Position.To3D());
//					Circle.Draw(Colordx.Red,100,info.PathBank[2].Position.To3D());
//				}
        }
    }
}
