using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace AddonTemplate
{
    public static class Config
    {
        public static Item Sheen = new Item(ItemId.Sheen);
        public static Item IceGaunlet = new Item(ItemId.Iceborn_Gauntlet);
        public static Item Trinity = new Item(ItemId.Trinity_Force);
        public static Item LichBane = new Item(ItemId.Lich_Bane);
        public static Item Youmuu = new Item(ItemId.Youmuus_Ghostblade);
        public static Item Botrk = new Item(ItemId.Blade_of_the_Ruined_King);
        public static Item Cutlass = new Item(ItemId.Bilgewater_Cutlass);
        public static Item Tear = new Item(ItemId.Tear_of_the_Goddess);
        public static Item Manamune = new Item(ItemId.Manamune);
        public static Item Muramana = new Item(ItemId.Muramana);

        public static float LastComboPressed = 0;

        private const string MenuName = "Ezreal";

        private static readonly Menu Menu;
        private static readonly Slider _TickLimit;
        public static int lastTick = 0;

        static Config()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            _TickLimit = Menu.Add("tick", new Slider("Tick Interval (High = more FPS, Low = fast KS)", 50, 1, 1000));
//            Menu.AddGroupLabel("Welcome to Hi I'm Ezreal addon!");
//            Menu.AddLabel("Made by GinjiBan");

            Modes.Initialize();
        }

       	public static int tickInterval
        {
            get { return _TickLimit.CurrentValue; }
        }
        
        public static void Initialize()
        {
        }

        public static class Modes
        {
            private static readonly Menu MenuCombo;
            public static readonly Menu MenuHarass;
            private static readonly Menu MenuKillsteal;
            private static readonly Menu MenuMisc;
            private static readonly Menu MenuDraw;
            private static readonly Menu MenuClear;

            static Modes()
            {
                MenuCombo = Menu.AddSubMenu("Combo");
                MenuHarass = Menu.AddSubMenu("Harass");
                MenuClear = Menu.AddSubMenu("Wave Clear");
                MenuKillsteal = Menu.AddSubMenu("KillSteal");
                MenuMisc = Menu.AddSubMenu("Global settings");
                MenuDraw = Menu.AddSubMenu("Visual");
                

                Combo.Initialize();
                Harass.Initialize();
                KillSteal.Initialize();
                Draw.Initialize();
                Misc.Initialize();
                Clear.Initialize();
            }

            public static void Initialize()
            {
            }


            public static class Combo
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly CheckBox _useRSeveral;
                private static readonly Slider _numberR;
                private static readonly CheckBox _useYoumuu;
                private static readonly CheckBox _useBotrk;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                public static bool UseRSeveral
                {
                    get { return _useRSeveral.CurrentValue; }
                }

 

                public static bool UseYoumuu
                {
                    get { return _useYoumuu.CurrentValue; }
                }
                public static bool useBotrk
                {
                    get { return _useBotrk.CurrentValue; }
                }

              	public static int NumberR
                {
                    get { return _numberR.CurrentValue; }
                }

                static Combo()
                {
                    MenuCombo.AddGroupLabel("Combo");
                    _useQ = MenuCombo.Add("comboUseQ", new CheckBox("Use Q"));
                    _useW = MenuCombo.Add("comboUseW", new CheckBox("Use W"));
                    _useE = MenuCombo.Add("comboUseE", new CheckBox("Use E", false));
                    _useRSeveral = MenuCombo.Add("comboUseRSeveral", new CheckBox("Use R to damage several enemies"));
                    _numberR = MenuCombo.Add("combonumberR", new Slider("Min enemy to use R", 3, 1, 5));
                    MenuCombo.AddSeparator();
                    _useYoumuu = MenuCombo.Add("useYoumuu", new CheckBox("Use Youmuu's Ghostblade"));
                    _useBotrk = MenuCombo.Add("useBotrk", new CheckBox("Use Blade of the ruined king"));
                }

                public static void Initialize()
                {
                }
            }

            public static class Harass
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _toggleQ;
                private static readonly CheckBox _toggleW;
                private static readonly CheckBox _DelayAutoHarass;
                private static readonly Slider _manaQ;
                private static readonly Slider _manaW;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static bool ToggleQ
                {
                    get { return _toggleQ.CurrentValue; }
                }
                public static bool ToggleW
                {
                    get { return _toggleW.CurrentValue; }
                }
                public static bool DelayAutoHarass
                {
                    get { return _DelayAutoHarass.CurrentValue; }
                }

                public static int ManaQ
                {
                    get { return _manaQ.CurrentValue; }
                }

                public static int ManaW
                {
                    get { return _manaW.CurrentValue; }
                }

                static Harass()
                {
                    MenuHarass.AddGroupLabel("Harass");
                    _useQ = MenuHarass.Add("harassUseQ", new CheckBox("Use Q"));
                    _useW = MenuHarass.Add("harassUseW", new CheckBox("Use W"));
                    MenuHarass.AddSeparator();
                    MenuHarass.AddGroupLabel("Auto Harass");
                    _toggleQ = MenuHarass.Add("toggleQ", new CheckBox("Auto Harass Q"));
                    _toggleW = MenuHarass.Add("toggleW", new CheckBox("Auto Harass W"));
                    MenuHarass.AddSeparator();
                    foreach (var source in ObjectManager.Get<AIHeroClient>().Where(a => a.IsEnemy))
                    {
                        MenuHarass.Add(source.ChampionName + "harass", new CheckBox("Harass " + source.ChampionName, true));
                    }
                    MenuHarass.AddSeparator();
                    MenuHarass.AddGroupLabel("Mana Management");
                    _manaQ = MenuHarass.Add("harassManaQ", new Slider("Minimum mana to use Q ({0}%)", 40));
                    _manaW = MenuHarass.Add("harassManaW", new Slider("Minimum mana to use W ({0}%)", 40));
                    _DelayAutoHarass = MenuHarass.Add("DelayAutoHarass", new CheckBox("Delay before auto Harass"));
                }

                public static void Initialize()
                {
                }
            }

            public static class Draw
            {
                private static readonly CheckBox _drawQ;
                private static readonly CheckBox _drawW;
                private static readonly CheckBox _drawE;
                private static readonly CheckBox _onlyRdy;
                public static readonly CheckBox _useHax;
                public static readonly Slider _skinhax;
                public static string[] skinName = { "Classic Ezreal", "Nottingham Ezreal", "Striker Ezreal", "Frosted Ezreal", "Explorer Ezreal", "Pulsefire Ezreal", "TPA Ezreal", "Debonair Ezreal", "Ace of Spades Ezreal" };

                public static bool DrawQ
                {
                    get { return _drawQ.CurrentValue; }
                }

                public static bool DrawW
                {
                    get { return _drawW.CurrentValue; }
                }

                public static bool DrawE
                {
                    get { return _drawE.CurrentValue; }
                }

                public static bool OnlyRdy
                {
                    get { return _onlyRdy.CurrentValue; }
                }
                public static bool UseHax
                {
                    get { return _useHax.CurrentValue; }
                }

                public static int SkinHax
                {
                    get { return _skinhax.CurrentValue; }
                }

                static Draw()
                {
                    MenuDraw.AddGroupLabel("Spells range");
                    _drawQ = MenuDraw.Add("drawQ", new CheckBox("Draw Q"));
                    _drawW = MenuDraw.Add("drawW", new CheckBox("Draw W"));
                    _drawE = MenuDraw.Add("drawE", new CheckBox("Draw E"));
                    _onlyRdy = MenuDraw.Add("onlyRdy", new CheckBox("Draw only when spell is not on cooldown"));
                }

                public static void Initialize()
                {
                }
            }
            public static class KillSteal
            {
                private static readonly CheckBox _KsQ;
                private static readonly CheckBox _KsW;
                private static readonly CheckBox _KsR;
                private static readonly Slider _MaxRRange;
                private static readonly Slider _MinRRange;
//                private static readonly CheckBox _RedSteal;
//                private static readonly CheckBox _BlueSteal;
//                private static readonly CheckBox _DragonSteal;
//                private static readonly CheckBox _BaronSteal;

                public static bool KsQ
                {
                    get { return _KsQ.CurrentValue; }
                }

                public static bool KsW
                {
                    get { return _KsW.CurrentValue; }
                }

                public static bool KsR
                {
                    get { return _KsR.CurrentValue; }
                }
                public static int MaxRRange
                {
                    get { return _MaxRRange.CurrentValue; }
                }
                public static int MinRRange
                {
                    get { return _MinRRange.CurrentValue; }
                }

//                public static bool RedSteal
//                {
//                    get { return _RedSteal.CurrentValue; }
//                }
//
//                public static bool BlueSteal
//                {
//                    get { return _BlueSteal.CurrentValue; }
//                }
//
//                public static bool DragonSteal
//                {
//                    get { return _DragonSteal.CurrentValue; }
//                }
//
//                public static bool BaronSteal
//                {
//                    get { return _BaronSteal.CurrentValue; }
//                }

                static KillSteal()
                {
                    MenuKillsteal.AddGroupLabel("KillSteal");
                    _KsQ = MenuKillsteal.Add("KsQ", new CheckBox("Use Q"));
                    _KsW = MenuKillsteal.Add("KsW", new CheckBox("Use W", false));
                    _KsR = MenuKillsteal.Add("KsR", new CheckBox("Use R"));
                    _MinRRange = MenuKillsteal.Add("MinRRange", new Slider("Min R Range", 350, 1, 15000));
                    _MaxRRange = MenuKillsteal.Add("RRange", new Slider("Max R Range", 4000, 1, 5000));
                    MenuKillsteal.AddLabel("R limited to 5000 for now (see if it still crashes) !");
//                    MenuKillsteal.AddSeparator();
//                    MenuKillsteal.AddGroupLabel("Jungle steal");
//                    MenuKillsteal.AddLabel("Disabled for now !");
//                    _RedSteal = MenuKillsteal.Add("RedS", new CheckBox("Use ult to KS enemy Red Buff", false));
//                    _BlueSteal = MenuKillsteal.Add("BlueS", new CheckBox("Use ult to KS enemy Blue Buff", false));
//                    _DragonSteal = MenuKillsteal.Add("DragonS", new CheckBox("Use ult to KS Dragon", false));
//                    _BaronSteal = MenuKillsteal.Add("BaronS", new CheckBox("Use ult to KS Baron", false));
                }

                public static void Initialize()
                {
                }
            }

            public static class Clear
            {
                private static readonly CheckBox _QLastHit;
                private static readonly CheckBox _WOnAlly;
                private static readonly Slider _NumberW;
                private static readonly Slider _manaQ;
                private static readonly Slider _manaW;

                public static bool UseQLastHit
                {
                    get { return _QLastHit.CurrentValue; }
                }

                public static bool UseWOnAlly
                {
                    get { return _WOnAlly.CurrentValue; }
                }

                public static int NumberW
                {
                    get { return _NumberW.CurrentValue; }
                }

                public static int ManaQ
                {
                    get { return _manaQ.CurrentValue; }
                }

                public static int ManaW
                {
                    get { return _manaW.CurrentValue; }
                }

                static Clear()
                {
                    MenuClear.AddGroupLabel("Wave / Jungle clear");
                    _QLastHit = MenuClear.Add("QLastHit", new CheckBox("Use Smart Q on wave clear"));
                    _WOnAlly = MenuClear.Add("WAlly", new CheckBox("Use W on allies"));
                    MenuClear.AddSeparator();
                    _NumberW = MenuClear.Add("NumberW", new Slider("Number of allies to use W", 2, 1, 4));
                    MenuClear.AddSeparator();
                    MenuClear.AddGroupLabel("Mana Management");
                    _manaQ = MenuClear.Add("clearManaQ", new Slider("Minimum mana to use Q ({0}%)", 40));
                    _manaW = MenuClear.Add("clearManaW", new Slider("Minimum mana to use W ({0}%)", 40));
                }

                public static void Initialize()
                {
                }
            }
            public static class Misc
            {
                private static readonly CheckBox _CcQ;
                private static readonly CheckBox _CcW;
                private static readonly CheckBox _UseQOnUnkillable;
                private static readonly CheckBox _EGapClos;
                private static readonly CheckBox _UseQUnderTurret;
                private static readonly CheckBox _AutoTear;
                private static readonly CheckBox _EFlee;
                private static readonly Slider _PredQ;
                private static readonly Slider _PredW;
                private static readonly Slider _PredR;
                public static KeyBind _SelfW;


                public static bool CcQ
                {
                    get { return _CcQ.CurrentValue; }
                }

                public static bool CcW
                {
                    get { return _CcW.CurrentValue; }
                }
                public static bool AutoTear
                {
                    get { return _AutoTear.CurrentValue; }
                }
                public static bool EFlee
                {
                    get { return _EFlee.CurrentValue; }
                }
                public static bool UseQOnUnkillable
                {
                    get { return _UseQOnUnkillable.CurrentValue; }
                }

                public static bool SelfW
                {
                    get { return _SelfW.CurrentValue; }
                }

                public static bool UseQUnderTurret
                {
                    get { return _UseQUnderTurret.CurrentValue; }
                }

                public static int PredQ
                {
                    get { return _PredQ.CurrentValue; }
                }

                public static int PredW
                {
                    get { return _PredW.CurrentValue; }
                }

                public static int PredR
                {
                    get { return _PredR.CurrentValue; }
                }
                public static bool EGapClos
                {
                    get { return _EGapClos.CurrentValue; }
                }

                static Misc()
                {
                    MenuMisc.AddGroupLabel("CCed target");
                    _CcQ = MenuMisc.Add("CCQ", new CheckBox("Use Q on CCed enemy"));
                    _CcW = MenuMisc.Add("CCW", new CheckBox("Use W on CCed enemy"));
                    MenuMisc.AddGroupLabel("Use Q to secure minion");
                    _UseQOnUnkillable = MenuMisc.Add("QUnkillable", new CheckBox("Use Q if can't kill minion with AA"));
                    _UseQUnderTurret = MenuMisc.Add("QUnderTurret", new CheckBox("Use Q if can't kill minion with AA under turret"));
                    MenuMisc.AddGroupLabel("Use W and E to get attack speed");
                    _SelfW = MenuMisc.Add("SelfW", new KeyBind("Self W", false, KeyBind.BindTypes.HoldActive, 'J'));
                    MenuMisc.AddGroupLabel("Use E to anti gap close");
                    _EGapClos = MenuMisc.Add("EGapClos", new CheckBox("Use E on Gap closer"));
                    MenuMisc.AddSeparator();
                    _AutoTear = MenuMisc.Add("AutoTear", new CheckBox("Enable Auto Tear at base"));
                    _EFlee = MenuMisc.Add("EFlee", new CheckBox("Use E to flee"));
                    MenuMisc.AddGroupLabel("Hit Chance");
                    MenuMisc.AddLabel("HitChance : 1 = Low, 2 = Medium, 3 = High");
                    _PredQ = MenuMisc.Add("PredQ", new Slider("Q HitChance", 3, 1, 3));
                    _PredW = MenuMisc.Add("PredW", new Slider("W HitChance", 3, 1, 3));
                    _PredR = MenuMisc.Add("PredR", new Slider("R HitChance", 3, 1, 3));
                }

                public static void Initialize()
                {
                }
            }
        }
    }
}
