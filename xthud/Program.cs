using System;
using System.Linq;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
//using Color = System.Drawing.Color;



namespace xthud
{
	class Program
	{
		static Menu wardMenu;
		static List<WardInfo> wards = new List<WardInfo>();
		
		static void Main(string[] args)
		{
			Loading.OnLoadingComplete += OnLoadingComplete;
		}
		
		static void OnLoadingComplete(EventArgs args)
		{
			wardMenu = MainMenu.AddMenu("Anti Ward","ward1");
			wardMenu.AddGroupLabel("Welcome to Hi I'm Ezreal addon!");
            wardMenu.AddGroupLabel("Draw Option");
            wardMenu.Add("drawPos", new CheckBox("Show Wards Position"));
            wardMenu.Add("drawTimer", new CheckBox("Show Remaining Time"));
            Drawing.OnDraw += Drawing_OnDraw;
            Obj_AI_Base.OnBuffGain += ObjOnBuffGain;
            Obj_AI_Base.OnBuffLose += ObjOnBuffLose;
			
			Chat.Print("XT-HUD Loaded !!!");
		}
		
        static void ObjOnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
			//if (sender.IsAlly)return;
			//if (sender.Distance(Player.Instance.Position)<500)
			//Chat.Print(args.Buff.Name);
			switch (args.Buff.Name)
	        {
                case "VisionWard":
	                wards.Add(new WardInfo(sender.Name, true , args.Buff.EndTime, sender.Position, SharpDX.Color.DeepPink, sender.NetworkId));
	                break;
	            case "ItemGhostWard":
	                wards.Add(new WardInfo(sender.Name, false, args.Buff.EndTime, sender.Position, SharpDX.Color.Green   , sender.NetworkId));
	                break;
	            case "relicblueward":
	                wards.Add(new WardInfo(sender.Name, true , args.Buff.EndTime, sender.Position, SharpDX.Color.Blue    , sender.NetworkId));
	                break;
	            case "relicyellowward":
	                wards.Add(new WardInfo(sender.Name, false, args.Buff.EndTime, sender.Position, SharpDX.Color.Yellow  , sender.NetworkId));
	                break;
	            case "JhinETrap":
	                wards.Add(new WardInfo(sender.Name, false, args.Buff.EndTime, sender.Position, SharpDX.Color.Red     , sender.NetworkId));
	                break;
	            case "BantamTrap":
	                wards.Add(new WardInfo(sender.Name, false, args.Buff.EndTime, sender.Position, SharpDX.Color.Red     , sender.NetworkId));
	                break;
	            case "JackInTheBox":
	                wards.Add(new WardInfo(sender.Name, false, args.Buff.EndTime, sender.Position, SharpDX.Color.Red     , sender.NetworkId));
	                break;
	        }
        }
        static void ObjOnBuffLose(Obj_AI_Base sender, Obj_AI_BaseBuffLoseEventArgs args)
        {
        	for (int i = 0; i < wards.Count; i++)
            {
                if (wards[i].NetworkId == sender.NetworkId)
                {
					switch (args.Buff.Name)
			        {
		                case "VisionWard":
							wards.RemoveAt(i);
							break;
			            case "sharedwardbuff":
							wards.RemoveAt(i);
							break;
			            case "relicblueward":
							wards.RemoveAt(i);
							break;
			            case "relicyellowward":
							wards.RemoveAt(i);
							break;
			            case "JhinETrap":
							wards.RemoveAt(i);
							break;
			            case "BantamTrap":
							wards.RemoveAt(i);
							break;
			            case "JackInTheBox":
							wards.RemoveAt(i);
							break;
			        }
                    return;
                }
            }
        }
	
	    static void Drawing_OnDraw(EventArgs args)
	    {
            if (isChecked(wardMenu, "drawPos"))
		    	foreach (var wardInfo in wards)
	            {	
		    		var remainTime = wardInfo.DeleteTimer - Game.Time;
	                if (remainTime > 0)
	                {
	                    Circle.Draw(wardInfo.Color, 90, wardInfo.Position);
	                    //DrawWardMinimap(wardInfo.Position,System.Drawing.Color.White);
	                	if (isChecked(wardMenu, "drawTimer"))
	                    {
		                	var wPos = Drawing.WorldToScreen(wardInfo.Position);
	                    	if (wardInfo.NoTime)
			                {
		                        Drawing.DrawText(wPos.X-3, wPos.Y - 11,System.Drawing.Color.White, "?",350);
			                }
		                    else
		                    {
		                    	Drawing.DrawText(wPos.X-15, wPos.Y - 11,System.Drawing.Color.White, ((int)remainTime/60).ToString() + " : " + ((int)remainTime%60).ToString(),200);
		                    }
	                    }
	                }
		        }
//            //foreach (var enemy in EntityManager.Heroes.Allies)
//            {
//            	var miniPos1 = new Vector2(Player.Instance.Position.WorldToMinimap().X,Player.Instance.Position.WorldToMinimap().Y);
//            	var miniPos2 = new Vector2(Player.Instance.Position.WorldToMinimap().X,Player.Instance.Position.WorldToMinimap().Y+50);
//            	var miniPos3 = new Vector2(Player.Instance.Position.WorldToMinimap().X,Player.Instance.Position.WorldToMinimap().Y);
//            	var miniPos4 = new Vector2(Player.Instance.Position.WorldToMinimap().X,Player.Instance.Position.WorldToMinimap().Y+50);
//            	Drawing.DrawLine(miniPos1,miniPos2,5f,System.Drawing.Color.Red);
//            	Line.DrawLine(System.Drawing.Color.Red, 2f, miniPos1,miniPos2,miniPos3,miniPos4);
            	
 //           }
	    }
	    

//        private static void DrawWardMinimap(Vector3 center, System.Drawing.Color color)
//        {
//            var centerMap = center.WorldToMinimap();
//            var a = new Vector2(centerMap.X - 5, centerMap.Y);
//            var b = new Vector2(centerMap.X + 5, centerMap.Y);
//            var c = new Vector2(centerMap.X, centerMap.Y + 10);
//
//            Drawing.DrawLine(a, b, 2f, color);
//            Drawing.DrawLine(b, c, 2f, color);
//            Drawing.DrawLine(c, a, 2f, color);
//        }
	    
	    static bool isChecked(Menu obj, String value)
        {
            return obj[value].Cast<CheckBox>().CurrentValue;
        }
	}
	
	public class WardInfo
    {
        public WardInfo(string name, bool noTime, float deleteTimer, Vector3 position, SharpDX.Color color, int networkid)
        {
            Name = name;
            NoTime = noTime;
            DeleteTimer = deleteTimer;
            Position = position;
            Color = color;
            NetworkId = networkid;
        }

        public string Name { get; set; }
        public bool NoTime { get; set; }
        public SharpDX.Color Color { get; set; }
        public float DeleteTimer { get; set; }
        public Vector3 Position { get; set; }
        public int NetworkId { get; set; }
    }
}