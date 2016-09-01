using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using EloBuddy.SDK.Enumerations;


namespace Kennen
{
    public class Config
    {     
        public static Menu Menu { get; private set; }
        public static Menu ComboMenu { get; private set; }
        public static Menu HarassMenu { get; private set; }
        public static Menu LaneClear { get; private set; }
        public static Menu JungleClear { get; private set; }
        public static Menu LasthitMenu { get; private set; }
        public static Menu MiscMenu { get; private set; }
        public static Menu DrawMenu { get; private set; }     

        public static void Dattenosa()
        {
            // Menu
            Menu = MainMenu.AddMenu("Kennen", "Kennen");          
            Menu.AddGroupLabel("Made by Uzumaki Boruto");
            Menu.AddLabel("Dattenosa");

            //ComboMenu
            ComboMenu = Menu.AddSubMenu("Combo");
            {
                ComboMenu.AddGroupLabel("Combo Settings");
                ComboMenu.Add("useQCombo", new CheckBox("Use Q"));
                ComboMenu.Add("useWCombo", new CheckBox("Use W"));
                ComboMenu.Add("WHitCombo", new Slider("Only Use W if hit {0} enemy", 1, 1, 5));
                ComboMenu.Add("useECombo", new CheckBox("Use E", false));                
                ComboMenu.Add("useRCombo", new CheckBox("Use R"));
                ComboMenu.Add("RHitCombo", new Slider("Least enemy to use R", 2, 1, 5));
                ComboMenu.Add("sep", new Separator());
                ComboMenu.AddLabel("Use spells");
                ComboMenu.Add("useIg", new CheckBox("Use Ignite"));
                ComboMenu.Add("useheal", new CheckBox("Use Heal"));
                ComboMenu.Add("manageheal", new Slider("If my HP below {0}% use Heal", 15, 1, 80));
                ComboMenu.Add("usehealally", new CheckBox("Use heal to save ally"));
                ComboMenu.Add("managehealally", new Slider("If ally's HP below {0}% use Heal to save", 10, 1, 80));
            }

            //HarassMenu
            HarassMenu = Menu.AddSubMenu("Harass");
            {
                HarassMenu.AddGroupLabel("Harass Settings");
                HarassMenu.Add("useQ", new CheckBox("Use Q"));
                HarassMenu.Add("useW", new CheckBox("Use W"));
                HarassMenu.Add("HrEnergyManager", new Slider("If energy below {0} stop harass", 0, 0, 200));
            }

            //LaneJungleClear Menu
            LaneClear = Menu.AddSubMenu("LaneClear");
            {
                LaneClear.AddGroupLabel("Laneclear Settings");
                LaneClear.Add("useQLc", new CheckBox("Use Q to laneclear", false));
                LaneClear.Add("useWLc", new CheckBox("Use W to laneclear", false));
                LaneClear.Add("WHitLc", new Slider("Only Use W if hit {0} minion(s)", 5, 1, 30));
                LaneClear.Add("useELc", new CheckBox("Use E to laneclear", false));
                LaneClear.Add("sep4", new Separator());
                LaneClear.Add("EnergyManager", new Slider("If energy below {0} stop use skill to laneclear", 0, 0, 200));
                LaneClear.Add("sep5", new Separator(40));
            }
            //JungleClear Menu
            JungleClear =Menu.AddSubMenu("JungleClear");
            {
                JungleClear.AddGroupLabel("Jungleclear Settings");
                JungleClear.Add("useQJc", new CheckBox("Use Q to jungleclear"));
                JungleClear.Add("useWJc", new CheckBox("Use W to jungleclear"));
                JungleClear.Add("WHitJc", new Slider("Use W if hit {0} monster(s)", 2, 1, 4));
                JungleClear.Add("useEJc", new CheckBox("Use E to jungleclear", false));
                JungleClear.Add("JcEnergyManager", new Slider("if energy below {0} stop Use skill to jungleclear", 0, 0, 200));
            }

            //LasthitMenu
            LasthitMenu = Menu.AddSubMenu("Lasthit");
            {
                LasthitMenu.Add("useQLh", new CheckBox("Use Q to lasthit"));
                LasthitMenu.Add("useWLh", new CheckBox("Use W to lasthit"));
            }

            //DrawMenu
            DrawMenu = Menu.AddSubMenu("Drawings");
            {
                DrawMenu.Add("drawQ", new CheckBox("Draw Q", false));
                DrawMenu.Add("drawW", new CheckBox("Draw W", false));
                DrawMenu.Add("drawR", new CheckBox("Draw R", false));
            }

            //MiscMenu          
            MiscMenu = Menu.AddSubMenu("MiscMenu");
            {
                MiscMenu.AddGroupLabel("Misc Settings");
                MiscMenu.AddLabel("Anti Gapcloser");
                MiscMenu.Add("useQAG", new CheckBox("Use Q to anti GapCloser"));
                MiscMenu.Add("useWAG", new CheckBox("Use W to anti Gapcloser"));
                MiscMenu.Add("useEAG", new CheckBox("Use E to anti Gapcloser"));

                MiscMenu.AddLabel("Killsteal Settings");
                MiscMenu.Add("useQKS", new CheckBox("Use Q to KS"));
                MiscMenu.Add("useWKS", new CheckBox("Use W to KS"));

                MiscMenu.AddLabel("Activator Item");
                MiscMenu.Add("item.1", new CheckBox("Auto use Bilgewater Cutlass"));
                MiscMenu.Add("item.1MyHp", new Slider("My HP less than {0}%", 95));
                MiscMenu.Add("item.1EnemyHp", new Slider("Enemy HP less than {0}%", 70));
                MiscMenu.Add("item.sep", new Separator());

                MiscMenu.Add("item.2", new CheckBox("Auto use Blade of the Ruined King"));
                MiscMenu.Add("item.2MyHp", new Slider("My HP less than {0}%", 80));
                MiscMenu.Add("item.2EnemyHp", new Slider("Enemy HP less than {0}%", 70));
                MiscMenu.Add("sep7", new Separator());

                MiscMenu.Add("item.3", new CheckBox("Auto use Zhonya's Hourglass"));
                MiscMenu.Add("item.3MyHp", new Slider("My HP lower than {0}%", 50));
                MiscMenu.Add("sep8", new Separator());

                MiscMenu.Add("item.4", new CheckBox("Use R and immediately Zhonya's Hourglass"));
                MiscMenu.Add("item.4mng", new Slider("Do it if hit {0} enemy", 3, 1, 5));
                MiscMenu.Add("sep9", new Separator());
	            MiscMenu.AddLabel("HitChance : 1 = Low, 2 = Medium, 3 = High");
	            MiscMenu.Add("PredQ", new Slider("Q HitChance", 2, 1, 3));
            }         
        }             
        public static HitChance PredQ()
        {
        	var mode = Config.MiscMenu["PredQ"].Cast<Slider>().CurrentValue;
            switch (mode)
            {
                case 1:
                    return HitChance.Low;
                case 2:
                    return HitChance.Medium;
                case 3:
                    return HitChance.High;
            }
            return HitChance.Medium;
        }        
    }
}     
