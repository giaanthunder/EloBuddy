using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;

namespace AddonTemplate
{
    public static class Program
    {
        public const string ChampName = "Ezreal";

        public static void Main(string[] args)
        {
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != ChampName)
            {
                Chat.Print(Player.Instance.ChampionName);
                return;
            }
            Config.Initialize();
            SpellManager.Initialize();
            ModeManager.Initialize();
           
            Drawing.OnDraw += GameEvent.OnDraw;
//			Orbwalker.OnUnkillableMinion += GameEvent.On_Unkillable_Minion;
            Config.Modes.Misc._SelfW.OnValueChange += GameEvent.SelfW_OnValueChanged;
            Gapcloser.OnGapcloser += GameEvent.Gapcloser_OnGapCloser;
            Obj_AI_Base.OnBasicAttack += GameEvent.ObjTurret_OnTurretDamage;
        }
    }
}