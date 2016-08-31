using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using SharpDX;
using Color = System.Drawing.Color;

namespace Humanizer
{
    internal class Program
    {
//fappa
		private static Menu _menu;
        public static int lastAtt = 0;
        public static int lastMove = 0;
        public static int AttCount = 0;
        public static float DistanceMove = 0;
        public static bool isMoving = false;
        
        public static int TickCount
        {
            get { return (int) (Game.Time*1000); }
        }

        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += GameLoaded;
        }

        private static void GameLoaded(EventArgs args)
        {
            _menu = MainMenu.AddMenu("Humanizer", "humanizer");                  
            _menu.Add("attDelay", new Slider("Min Att Waiting (ms)", 1000, 1000, 2000));
            _menu.Add("minMove", new Slider("Min Distance Move (unit)", 250, 1, 1000));
//            _menu.Add("attRangePercent", new Slider("Attack Range Stop Moving (%)", 70, 1, 100));

            Game.OnTick += OnTick;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
            Orbwalker.OnAttack += OnAttack;
//            Orbwalker.OnPostAttack += OnPostAttack;
        }        

        private static void OnTick(EventArgs args)
        {
        	var myHero = Player.Instance;
        	DistanceMove = myHero.AttackDelay*myHero.MoveSpeed;
        	if(DistanceMove < _menu["minMove"].Cast<Slider>().CurrentValue && AttCount < 2 && AttCount > 0 && TickCount - lastAtt < _menu["attDelay"].Cast<Slider>().CurrentValue)
        	{
        		Orbwalker.DisableMovement = true;
        	}
        	else
        	{
        		Orbwalker.DisableMovement = false;
        		AttCount = 0;
        	}
        	
        	if(((float)(TickCount - lastMove)/1000)*myHero.MoveSpeed < _menu["minMove"].Cast<Slider>().CurrentValue 
        	  && !Orbwalker.ShouldWait && myHero.CanMove && myHero.IsMoving)
        	{
        		Orbwalker.DisableAttacking = true;
        	}
        	else
        	{
        		Orbwalker.DisableAttacking = false;
        	}
        	
        	if(myHero.IsMoving && !isMoving)
        	{
        		isMoving = true;
        		lastMove = TickCount;
//        		Chat.Print("move");
        	}
        	if(!myHero.IsMoving && isMoving)
        	{
        		isMoving = false;
//        		Chat.Print("stop");
        	}
        }
      
        
        public static void OnAttack(AttackableUnit target, EventArgs args)
        {
    		lastAtt = TickCount;
    		AttCount++;
        }
        
//        public static void OnPostAttack(AttackableUnit target, EventArgs args)
//        {
//        	lastMove = TickCount;
//        }
        
        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe) return;
        }
        
    }
}