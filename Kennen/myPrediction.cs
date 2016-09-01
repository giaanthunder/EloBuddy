using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Constants;
using SharpDX;
using Colordx = SharpDX.Color;

namespace myPrediction
{
	static class myPrediction
	{
		public static void CastPredictedSpell(SpellSlot spellslot, float range, SkillShotType type, float delay, float speed, float width, Obj_AI_Base target, bool minioncollision, bool championcollision)
		{
			delay = (delay+(float)(Game.Ping/2.15))/1000;
			if(target.Path.First()==target.Path.Last())
			{
				if (minioncollision || championcollision)
				{
					var ap2=Player.Instance.ServerPosition.Distance(target);
					var MinionCollision = EntityManager.MinionsAndMonsters.EnemyMinions.Any(minion=>minion.IsValidTarget(ap2+200f) && CollisionDetection(minion, ObjectManager.Player, target.ServerPosition, ap2, delay, speed, width));
					var HeroesCollision = EntityManager.Heroes.Enemies.Any(hero=>hero != target && hero.IsValidTarget(ap2+200f) && CollisionDetection(hero, ObjectManager.Player, target.ServerPosition, ap2, delay, speed, width));
					if ((!minioncollision || !MinionCollision) && (!championcollision || !HeroesCollision))
					{
						Player.CastSpell(spellslot, target.ServerPosition);
					}
				}
				else
				{
					Player.CastSpell(spellslot, target.ServerPosition);
				}
			}
			if (speed > 1f)
			{
				float ax = ObjectManager.Player.ServerPosition.X;
				float ay = ObjectManager.Player.ServerPosition.Y;
				
				float bx = target.ServerPosition.X;
				float by = target.ServerPosition.Y;
				
				float cx = target.Path.LastOrDefault().X;
				float cy = target.Path.LastOrDefault().Y;
				
				float bc = (float)(Math.Sqrt((bx-cx)*(bx-cx)+(by-cy)*(by-cy)));
				float vx = ( cx - bx ) / bc;
				float vy = ( cy - by ) / bc;
				
				float bv = target.MoveSpeed;
				float ab = (float)(Math.Sqrt((ax-bx)*(ax-bx)+(ay-by)*(ay-by)));
				
				float hx = bx + ( vx * bv * ( delay + ab / speed ) );
				float hy = by + ( vy * bv * ( delay + ab / speed ) );
				float ah = (float)(Math.Sqrt((hx-ax)*(hx-ax)+(hy-ay)*(hy-ay)));
				
				float ix = bx + ( vx * bv * ( delay + ah / speed ) );
				float iy = by + ( vy * bv * ( delay + ah / speed ) );
				float ai = (float)(Math.Sqrt((ix-ax)*(ix-ax)+(iy-ay)*(iy-ay)));
				
				float jx = bx + ( vx * bv * ( delay + ai / speed ) );
				float jy = by + ( vy * bv * ( delay + ai / speed ) );
				
				float aj = (float)(Math.Sqrt((ax-jx)*(ax-jx)+(ay-jy)*(ay-jy)));
				float bj = (float)(Math.Sqrt((bx-jx)*(bx-jx)+(by-jy)*(by-jy)));
				
				float at = delay + aj / speed;
				float bt = bj / bv;
				if (at - bt > -0.05f && at - bt < 0.05f)
				{
					float x = jx - ( vx * ( width / 2f /*- Game.Ping / 2.15f*/ ) );
					float y = jy - ( vy * ( width / 2f /*- Game.Ping / 2.15f*/ ) );
					float bp = (float)(Math.Sqrt((bx-x)*(bx-x)+(by-y)*(by-y)));
					if (bp > bc)
					{
						x = cx;
						y = cy;
					}
					float ap = (float)(Math.Sqrt((ax-x)*(ax-x)+(ay-y)*(ay-y)));
					if (ap < range)
					{
						Vector3 pos = new Vector3 {X = x, Y = y};
						float pl = (float)(Math.Sqrt((target.Path.LastOrDefault().X-cx)*(target.Path.LastOrDefault().X-cx)+(target.Path.LastOrDefault().Y-cy)*(target.Path.LastOrDefault().Y-cy)));
						if (pl < 50f)
						{
							if (minioncollision || championcollision)
							{
								var MinionCollision = EntityManager.MinionsAndMonsters.EnemyMinions.Any(minion=>minion.IsValidTarget(ap+200f) && CollisionDetection(minion, ObjectManager.Player, pos, ap, delay, speed, width));
								var HeroesCollision = EntityManager.Heroes.Enemies.Any(hero=>hero != target && hero.IsValidTarget(ap+200f) && CollisionDetection(hero, ObjectManager.Player, pos, ap, delay, speed, width));
								if ((!minioncollision || !MinionCollision) && (!championcollision || !HeroesCollision))
								{
									Player.CastSpell(spellslot, pos);
								}
							}
							else
							{
								Player.CastSpell(spellslot, pos);
							}
						}
					}
				}
		  	}
			else// skill only have delay
			{
				float ax = ObjectManager.Player.ServerPosition.X;
				float ay = ObjectManager.Player.ServerPosition.Y;
				
				float bx = target.ServerPosition.X;
				float by = target.ServerPosition.Y;
				
				float cx = target.Path.LastOrDefault().X;
				float cy = target.Path.LastOrDefault().Y;
				
				float bc = (float)(Math.Sqrt((bx-cx)*(bx-cx)+(by-cy)*(by-cy)));
				float vx = ( cx - bx ) / bc;
				float vy = ( cy - by ) / bc;
				
				float x = bx + ( vx * target.MoveSpeed * delay );
				float y = by + ( vy * target.MoveSpeed * delay );
				
				float xx = x - vx * ( width / 2f /*- Game.Ping / 2.15f*/ );
				float yy = y - vy * ( width / 2f /*- Game.Ping / 2.15f*/ );
				
				float bp = (float)(Math.Sqrt((bx-xx)*(bx-xx)+(by-yy)*(by-yy)));
				if (bp > bc)
				{
					xx = cx;
					yy = cy;
				}
				float ap = (float)(Math.Sqrt((ax-xx)*(ax-xx)+(ay-yy)*(ay-yy)));
				if (ap < range)
				{
					Vector3 pos = new Vector3 {X = xx, Y = yy};
					float pl = (float)(Math.Sqrt((target.Path.LastOrDefault().X-cx)*(target.Path.LastOrDefault().X-cx)+(target.Path.LastOrDefault().Y-cy)*(target.Path.LastOrDefault().Y-cy)));
					if (pl < 50f)
					{
						Player.CastSpell(spellslot, pos);
					}
				}
			}
		}
		
		public static HitChance GetHitChance(SpellSlot spellslot, float range, SkillShotType type, float delay, float speed, float radius,Vector3 fromPoint, Obj_AI_Base target)
		{
			delay/=1000;
			// CAN'T MOVE SPELLS ///////////////////////////////////////////////////////////////////////////////////
		
		    if (UnitTracker.GetSpecialSpellEndTime(target) > 0 || target.HasBuff("recall") || (UnitTracker.GetLastStopMoveTime(target) < 0.1d && target.IsRooted))
		    {
		        return HitChance.High;
		    }
		
		    // PREPARE MATH ///////////////////////////////////////////////////////////////////////////////////
		
		    var tempHitchance = HitChance.Low;
		
		    var lastWaypiont = target.Path.Last();
		    var distanceUnitToWaypoint = lastWaypiont.Distance(target.ServerPosition);
		    var distanceFromToUnit = fromPoint.Distance(target.ServerPosition);
		    var distanceFromToWaypoint = lastWaypiont.Distance(fromPoint);
		    var getAngle = GetAngle(fromPoint, target);
		    float speedDelay = distanceFromToUnit / speed;
		
		    if (Math.Abs(speed - float.MaxValue) < float.Epsilon)
		        speedDelay = 0;
		
		    float totalDelay = speedDelay + delay;
		    float moveArea = target.MoveSpeed * totalDelay;
		    float fixRange = moveArea * 0.4f;
		    float pathMinLen = 900 + + moveArea;
		    double angleMove = 31;
		
		    if (radius > 70)
		        angleMove ++;
		    else if (radius <= 60)
		        angleMove--;
		    if (delay < 0.3)
		        angleMove++;
		
		    if (UnitTracker.GetLastNewPathTime(target) < 0.1d)
		    {
		        tempHitchance = HitChance.High;
		        pathMinLen = 700f + moveArea;
		        angleMove += 1.5;
		        fixRange = moveArea * 0.3f;
		    }
		
		    if (type == SkillShotType.Circular)
		    {
		        fixRange -= radius / 2;
		    }
		
		    // FIX RANGE ///////////////////////////////////////////////////////////////////////////////////
		    if (distanceFromToWaypoint <= distanceFromToUnit)
		    {
		        if (distanceFromToUnit > range - fixRange)
		        {
		            tempHitchance = HitChance.Medium;
		            // return tempHitchance;
		        }
		    }
		    else if (distanceUnitToWaypoint > 350)
		    {
		        angleMove += 1.5;
		    }
		
		    // SPAM CLICK ///////////////////////////////////////////////////////////////////////////////////
		
		    if (UnitTracker.PathCalc(target))
		    {
		        //OktwCommon.debug("PRED: SPAM CLICK");
		        if(distanceFromToUnit < range - fixRange)
		            tempHitchance = HitChance.High;
		        else
		            tempHitchance = HitChance.Medium;
		        // return tempHitchance;
		    }
		
		    // SPAM ServerPosition ///////////////////////////////////////////////////////////////////////////////////
		
		    if (UnitTracker.SpamSamePlace(target))
		    {
		        //OktwCommon.debug("PRED: SPAM ServerPosition");
		        return HitChance.High;
		    }
		
			
			
		    // SPECIAL CASES ///////////////////////////////////////////////////////////////////////////////////
		
		    if (distanceFromToUnit < 250)
		    {
		        //OktwCommon.debug("PRED: SPECIAL CASES NEAR");
		        return HitChance.High;
		    }
		    else if( target.MoveSpeed < 250)
		    {
		        //OktwCommon.debug("PRED: SPECIAL CASES SLOW");
		        return HitChance.High;
		    }
		    else if(distanceFromToWaypoint < 250)
		    {
		        //OktwCommon.debug("PRED: SPECIAL CASES ON WAY");
		        return HitChance.High;
		    }
		
		    // LONG CLICK DETECTION ///////////////////////////////////////////////////////////////////////////////////
		
		    if (distanceUnitToWaypoint > pathMinLen)
		    {
		        //OktwCommon.debug("PRED: LONG CLICK DETECTION");
		        return HitChance.High;
		    }
		
		    // RUN IN LANE DETECTION /////////////////////////////////////////////////////////////////////////////////// 
		
		    if (getAngle < angleMove && distanceUnitToWaypoint > 260)
		    {
		        //OktwCommon.debug(GetAngle(input.From, target) + " PRED: ANGLE " + angleMove + " DIS " + distanceUnitToWaypoint);
		        return HitChance.High;
		    }
		
		    // CIRCLE NEW PATH ///////////////////////////////////////////////////////////////////////////////////
		
		    if (type == SkillShotType.Circular)
		    {
		        if (UnitTracker.GetLastNewPathTime(target) < 0.1d && distanceUnitToWaypoint > fixRange)
		        {
		            //OktwCommon.debug("PRED: CIRCLE NEW PATH");
		            return HitChance.High;
		        }
		    }
			
		    // LOW HP DETECTION ///////////////////////////////////////////////////////////////////////////////////
		
		    if (target.HealthPercent < 20 || ObjectManager.Player.HealthPercent < 20)
		    {
		        tempHitchance = HitChance.Medium;
				// return HitChance.Medium;
		    }
			
		    // STOP LOGIC ///////////////////////////////////////////////////////////////////////////////////
		
		    if (target.Path.LastOrDefault() != target.ServerPosition)
		    {
		            if ((UnitTracker.GetLastAutoAttackTime(target) < 0.1 || UnitTracker.GetLastStopMoveTime(target) < 0.1) && totalDelay < 0.6)
		            {
		                //OktwCommon.debug("PRED: STOP LOGIC WINDING");
		                tempHitchance = HitChance.Medium;
		            }
		        else if (UnitTracker.GetLastStopMoveTime(target) < 0.5)
		        {
		            tempHitchance = HitChance.Medium;
		        }
		        else
		        {
		            //OktwCommon.debug("PRED: STOP LOGIC");
		            tempHitchance = HitChance.Medium;
		        }
		        return tempHitchance;
		    }
		    //Program.debug("PRED: NO DETECTION");
		    return tempHitchance;
		}
		
			
		static bool CollisionDetection(Obj_AI_Base unit, Obj_AI_Base startlinepoint, Vector3 endlinepoint, float distPlayertoT, float delay, float speed, float width)
		{
		  float sx = startlinepoint.ServerPosition.X;
		  float sy = startlinepoint.ServerPosition.Y;
		
		  float ex = endlinepoint.X;
		  float ey = endlinepoint.Y;
		
		  float ux = unit.ServerPosition.X;
		  float uy = unit.ServerPosition.Y;
		  float uv = unit.MoveSpeed;
		
		  float unx = unit.Path.LastOrDefault().X;
		  float uny = unit.Path.LastOrDefault().Y;
		
		  float eu = (float)Math.Sqrt((ex - ux) * ( ex - ux ) + ( ey - uy ) * ( ey - uy ));
		  float su = (float)Math.Sqrt(( sx - ux ) * ( sx - ux ) + ( sy - uy ) * ( sy - uy ));
		  float unu = (float)(Math.Sqrt((ux-unx)*(ux-unx)+(uy-uny)*(uy-uny)));
		
		  if (eu < distPlayertoT + 250f && su < distPlayertoT + 250f)
		  {
		    float a = ey - sy;
		    float b = ex - sx;
		    float d = Math.Abs(a * (sx - ux) + b * (uy - sy )) / (float)Math.Sqrt(a * a + b * b);
		    float r = width / 2 + unit.BoundingRadius / 2 + speed * ( delay + su / speed ) / unit.BoundingRadius / 2 + 25f;
		    if (eu <= distPlayertoT + 50f && su <= distPlayertoT + 50f && r >= d)
		    {
		      return true;
		    }
		    if (r < d)
		    {
		      float vx = ( unx - ux ) / unu;
		      float vy = ( uny - uy ) / unu;
		
		      float ab = (float)(Math.Sqrt((sx-ux)*(sx-ux)+(sy-uy)*(sy-uy)));
		
		      float hx = ux + ( vx * uv * ( delay + ab / speed ) );
		      float hy = uy + ( vy * uv * ( delay + ab / speed ) );
		      float ah = (float)(Math.Sqrt((hx-sx)*(hx-sx)+(hy-sy)*(hy-sy)));
		
		      float ix = ux + ( vx * uv * ( delay + ah / speed ) );
		      float iy = uy + ( vy * uv * ( delay + ah / speed ) );
		      float ai = (float)(Math.Sqrt((ix-sx)*(ix-sx)+(iy-sy)*(iy-sy)));
		
		      float jx = ux + ( vx * uv * ( delay + ai / speed ) );
		      float jy = uy + ( vy * uv * ( delay + ai / speed ) );
		
		      float sj = (float)(Math.Sqrt((sx-jx)*(sx-jx)+(sy-jy)*(sy-jy)));
		      float ej = (float)(Math.Sqrt((ex-jx)*(ex-jx)+(ey-jy)*(ey-jy)));
		      float uj = (float)(Math.Sqrt((ux-jx)*(ux-jx)+(uy-jy)*(uy-jy)));
		
		      float at = delay + sj / speed;
		      float bt = uj / uv;
		
		      if (at - bt > -0.05f && at - bt < 0.05f)
		      {
		        if (uj > unu)
		        {
		          jx = unx;
		          jy = uny;
		        }
		        if (sj < distPlayertoT + 50f && ej < distPlayertoT + 50f)
		        {
		          float pl = (float)(Math.Sqrt((unit.Path.LastOrDefault().X-unx)*(unit.Path.LastOrDefault().X-unx)+(unit.Path.LastOrDefault().Y-uny)*(unit.Path.LastOrDefault().Y-uny)));
		          if (pl < 25f)
		          {
		            float dd = Math.Abs(a * (sx - jx) + b * (jy - sy )) / (float)Math.Sqrt(a * a + b * b);
		            if (r >= dd)
		            {
		              return true;
		            }
		          }
		        }
		      }
		    }
		  }
		  return false;
		}

		
	    internal class PathInfo
	    {
	        public Vector2 ServerPosition { get; set; }
	        public float Time { get; set; }
	    }
	
	    internal class Spells
	    {
	        public string name { get; set; }
	        public double duration { get; set; }
	    }
	
	    internal class UnitTrackerInfo
	    {
	        public int NetworkId { get; set; }
	        public int AaTick { get; set; }
	        public int NewPathTick { get; set; }
	        public int StopMoveTick { get; set; }
	        public int LastInvisableTick { get; set; }
	        public int SpecialSpellFinishTick { get; set; }
	        public List<PathInfo> PathBank = new List<PathInfo>();
	    }
		
	    internal static class UnitTracker
	    {
	        public static List<UnitTrackerInfo> UnitTrackerInfoList = new List<UnitTrackerInfo>();
	        private static List<AIHeroClient> Champion = new List<AIHeroClient>();
	        private static List<PathInfo> PathBank = new List<PathInfo>();
	        private static List<Spells> spells = new List<Spells>();
	        static UnitTracker()
	        {
	            spells.Add(new Spells() { name = "katarinar", duration = 1 }); //Katarinas R
	            spells.Add(new Spells() { name = "drain", duration = 1 }); //Fiddle W
	            spells.Add(new Spells() { name = "crowstorm", duration = 1 }); //Fiddle R
	            spells.Add(new Spells() { name = "consume", duration = 0.5 }); //Nunu Q
	            spells.Add(new Spells() { name = "absolutezero", duration = 1 }); //Nunu R
	            spells.Add(new Spells() { name = "staticfield", duration = 0.5 }); //Blitzcrank R
	            spells.Add(new Spells() { name = "cassiopeiapetrifyinggaze", duration = 0.5 }); //Cassio's R
	            spells.Add(new Spells() { name = "ezrealtrueshotbarrage", duration = 1 }); //Ezreal's R
	            spells.Add(new Spells() { name = "galioidolofdurand", duration = 1 }); //Ezreal's R                                                                   
	            spells.Add(new Spells() { name = "luxmalicecannon", duration = 1 }); //Lux R
	            spells.Add(new Spells() { name = "reapthewhirlwind", duration = 1 }); //Jannas R
	            spells.Add(new Spells() { name = "jinxw", duration = 0.6 }); //jinxW
	            spells.Add(new Spells() { name = "jinxr", duration = 0.6 }); //jinxR
	            spells.Add(new Spells() { name = "missfortunebullettime", duration = 1 }); //MissFortuneR
	            spells.Add(new Spells() { name = "shenstandunited", duration = 1 }); //ShenR
	            spells.Add(new Spells() { name = "threshe", duration = 0.4 }); //ThreshE
	            spells.Add(new Spells() { name = "threshrpenta", duration = 0.75 }); //ThreshR
	            spells.Add(new Spells() { name = "threshq", duration = 0.75 }); //ThreshQ
	            spells.Add(new Spells() { name = "infiniteduress", duration = 1 }); //Warwick R
	            spells.Add(new Spells() { name = "meditate", duration = 1 }); //yi W
	            spells.Add(new Spells() { name = "alzaharnethergrasp", duration = 1 }); //Malza R
	            spells.Add(new Spells() { name = "lucianq", duration = 0.5 }); //Lucian Q
	            spells.Add(new Spells() { name = "caitlynpiltoverpeacemaker", duration = 0.5 }); //Caitlyn Q
	            spells.Add(new Spells() { name = "velkozr", duration = 0.5 }); //Velkoz R 
	            spells.Add(new Spells() { name = "jhinr", duration = 2 }); //Jhin R 
	
	            foreach (var hero in ObjectManager.Get<AIHeroClient>())
	            {
	            	UnitTrackerInfoList.Add(new UnitTrackerInfo() { 
	            	                        	NetworkId = hero.NetworkId, 
	            	                        	AaTick = TickCount, 
	            	                        	StopMoveTick = TickCount, 
	            	                        	NewPathTick = TickCount, 
	            	                        	SpecialSpellFinishTick = TickCount, 
	            	                        	LastInvisableTick = TickCount});
	            }
    			Obj_AI_Base.OnNewPath += OnNewPath;
				Dash.OnDash += OnDash;
				Obj_AI_Base.OnBasicAttack+= OnAttack;
				Obj_AI_Base.OnProcessSpellCast += OnProcessSpell;
				Obj_AI_Base.OnBuffGain += OnBuffGain;
				Obj_AI_Base.OnBuffLose += OnBuffLose;
	        }
	        
	        static void OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
	        {
	        
	        }
	        
	        static void OnBuffLose(Obj_AI_Base sender, Obj_AI_BaseBuffLoseEventArgs args)
	        {
	        
	        }
	        
			static void OnNewPath(Obj_AI_Base sender, GameObjectNewPathEventArgs args)
			{
				if (sender.Type != GameObjectType.AIHeroClient) return;
	
	            var info = UnitTrackerInfoList.Find(x => x.NetworkId == sender.NetworkId);
	
	            if (args.Path.Last() == args.Path.First()) // STOP MOVE DETECTION
	                info.StopMoveTick = TickCount;
	            else // SPAM CLICK LOGIC
	            {
	            	info.NewPathTick = TickCount;
	            	info.PathBank.Add(new PathInfo() { ServerPosition = args.Path.Last().To2D(), Time = Game.Time });
	            }
	            if (info.PathBank.Count > 3)
	                info.PathBank.Remove(info.PathBank.First());
	            
			}
			
			static void OnDash(Obj_AI_Base sender, Dash.DashEventArgs e)
			{
				if (sender.Type != GameObjectType.AIHeroClient) return;
			}
	
			static void OnAttack(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args) 
			{
				if (sender.Type != GameObjectType.AIHeroClient) return;
	               UnitTrackerInfoList.Find(x => x.NetworkId == sender.NetworkId).AaTick = TickCount;
			}
			
			static void OnProcessSpell(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
			{
                var foundSpell = spells.Find(x => args.SData.Name.ToLower() == x.name.ToLower());
                if (foundSpell != null)
                {
                    UnitTrackerInfoList.Find(x => x.NetworkId == sender.NetworkId).SpecialSpellFinishTick = TickCount + (int)(foundSpell.duration * 1000);
                }
			}
	        
	        public static bool SpamSamePlace(Obj_AI_Base unit)
	        {
	            var TrackerUnit = UnitTrackerInfoList.Find(x => x.NetworkId == unit.NetworkId);
	            if (TrackerUnit.PathBank.Count < 3)
	                return false;
	
	            if (TrackerUnit.PathBank[2].Time - TrackerUnit.PathBank[1].Time < 0.2f
	                && TrackerUnit.PathBank[2].Time + 0.1f < Game.Time
	                && TrackerUnit.PathBank[1].ServerPosition.Distance(TrackerUnit.PathBank[2].ServerPosition) < 100)
	            {
	                return true;
	            }
	            else
	                return false;
	        }
	
	        public static bool PathCalc(Obj_AI_Base unit)
	        {
	            var TrackerUnit = UnitTrackerInfoList.Find(x => x.NetworkId == unit.NetworkId);
	            if (TrackerUnit.PathBank.Count < 3)
	                return false;
	
	            if (TrackerUnit.PathBank[2].Time - TrackerUnit.PathBank[0].Time < 0.4f && Game.Time - TrackerUnit.PathBank[2].Time < 0.1
	                && TrackerUnit.PathBank[2].ServerPosition.Distance(unit.ServerPosition) < 300
	                && TrackerUnit.PathBank[1].ServerPosition.Distance(unit.ServerPosition) < 300
	                && TrackerUnit.PathBank[0].ServerPosition.Distance(unit.ServerPosition) < 300)
	            {
	                var dis = unit.Distance(TrackerUnit.PathBank[2].ServerPosition);
	                if (TrackerUnit.PathBank[1].ServerPosition.Distance(TrackerUnit.PathBank[2].ServerPosition) > dis && TrackerUnit.PathBank[0].ServerPosition.Distance(TrackerUnit.PathBank[1].ServerPosition) > dis)
	                    return true;
	                else
	                    return false;
	            }
	            else
	                return false;
	        }
	
	        public static List<Vector2> GetPathWayCalc(Obj_AI_Base unit)
	        {
	            var TrackerUnit = UnitTrackerInfoList.Find(x => x.NetworkId == unit.NetworkId);
	            List<Vector2> points = new List<Vector2>();
	            points.Add(unit.ServerPosition.To2D());
	            return points;
	        }
	        
	        public static double GetSpecialSpellEndTime(Obj_AI_Base unit)
	        {
	            var TrackerUnit = UnitTrackerInfoList.Find(x => x.NetworkId == unit.NetworkId);
	            return (TrackerUnit.SpecialSpellFinishTick - TickCount) / 1000d;
	        }
	
	        public static double GetLastAutoAttackTime(Obj_AI_Base unit)
	        {
	            var TrackerUnit = UnitTrackerInfoList.Find(x => x.NetworkId == unit.NetworkId);
	            return (TickCount - TrackerUnit.AaTick) / 1000d;
	        }
	
	        public static double GetLastNewPathTime(Obj_AI_Base unit)
	        {
	            var TrackerUnit = UnitTrackerInfoList.Find(x => x.NetworkId == unit.NetworkId);
	            return (TickCount - TrackerUnit.NewPathTick) / 1000d;
	        }
	
	        public static double GetLastVisableTime(Obj_AI_Base unit)
	        {
	            var TrackerUnit = UnitTrackerInfoList.Find(x => x.NetworkId == unit.NetworkId);
	
	            return (TickCount - TrackerUnit.LastInvisableTick) / 1000d;
	        }
	
	        public static double GetLastStopMoveTime(Obj_AI_Base unit)
	        {
	            var TrackerUnit = UnitTrackerInfoList.Find(x => x.NetworkId == unit.NetworkId);
	
	            return (TickCount - TrackerUnit.StopMoveTick) / 1000d;
	        }
	    }
	    
	    public static int TickCount{get{return Environment.TickCount & int.MaxValue;}}
		
        internal static double GetAngle(Vector3 from, Obj_AI_Base target)
        {
            var C = target.ServerPosition.To2D();
            var A = target.Path.LastOrDefault().To2D();

            if (C == A)
                return 60;

            var B = from.To2D();

            var AB = Math.Pow((double)A.X - (double)B.X, 2) + Math.Pow((double)A.Y - (double)B.Y, 2);
            var BC = Math.Pow((double)B.X - (double)C.X, 2) + Math.Pow((double)B.Y - (double)C.Y, 2);
            var AC = Math.Pow((double)A.X - (double)C.X, 2) + Math.Pow((double)A.Y - (double)C.Y, 2);

            return Math.Cos((AB + BC - AC) / (2 * Math.Sqrt(AB) * Math.Sqrt(BC))) * 180 / Math.PI;
        }

	}
}
