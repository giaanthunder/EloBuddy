﻿using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Enumerations;

namespace LelBlanc
{
    internal class Config
    {
        /// <summary>
        /// Contains all the Menu's
        /// </summary>
        public static Menu ConfigMenu,
            ComboMenu,
            HarassMenu,
            LaneClearMenu,
            JungleClearMenu,
            KillStealMenu,
            DrawingMenu,
            MiscMenu;

        /// <summary>
        /// Contains Different Modes
        /// </summary>
        private static readonly string[] LogicModes = {"Burst Logic", "Two Chain(TM) Logic", "Chase Burst"};

        /// <summary>
        /// Creates the Menu
        /// </summary>
        public static void Initialize()
        {
            ConfigMenu = MainMenu.AddMenu("LeBlanc", "LeBlanc");

            ComboMenu = ConfigMenu.AddSubMenu("Combo Menu", "cMenu");
            ComboMenu.AddLabel("Spell Settings");
            ComboMenu.Add("useQ", new CheckBox("Use Q"));
            ComboMenu.Add("useW", new CheckBox("Use W"));
            ComboMenu.Add("useReturn", new CheckBox("Use W Return"));
            ComboMenu.Add("useE", new CheckBox("Use E"));
            ComboMenu.AddLabel("R Settings");
            ComboMenu.Add("useQR", new CheckBox("Use QR"));
            ComboMenu.Add("useWR", new CheckBox("Use WR", false));
            ComboMenu.Add("useReturn2", new CheckBox("Use WR Return", false));
            ComboMenu.Add("useER", new CheckBox("Use ER", false));
            ComboMenu.AddLabel("Extra Settings");
            ComboMenu.Add("mode", new ComboBox("Combo Modes", 0, LogicModes));
            ComboMenu.AddLabel("Burst Logic Settings");
            ComboMenu.Add("minRange", new CheckBox("Use Q -> R only if W is in range", false));

            HarassMenu = ConfigMenu.AddSubMenu("Harass Menu", "hMenu");
            HarassMenu.AddLabel("Spell Settings");
            HarassMenu.Add("useQ", new CheckBox("Use Q"));
            HarassMenu.Add("useW", new CheckBox("Use W"));
            HarassMenu.Add("useReturn", new CheckBox("Use W Return"));
            HarassMenu.Add("useE", new CheckBox("Use E"));
            HarassMenu.AddLabel("R Settings");
            HarassMenu.Add("useQR", new CheckBox("Use QR"));
            HarassMenu.Add("useWR", new CheckBox("Use WR", false));
            HarassMenu.Add("useReturn2", new CheckBox("Use WR Return"));
            HarassMenu.Add("useER", new CheckBox("Use ER", false));
            HarassMenu.AddLabel("Extra Settings");
            HarassMenu.Add("mode", new ComboBox("Harass Modes", 1, LogicModes));
            HarassMenu.AddLabel("Burst Logic Settings");
            HarassMenu.Add("minRange", new CheckBox("Use Q -> R only if W is in range", false));

            LaneClearMenu = ConfigMenu.AddSubMenu("Laneclear Menu", "lcMenu");
            LaneClearMenu.AddLabel("Spell Settings");
            LaneClearMenu.Add("useQ", new CheckBox("Use Q", false));
            LaneClearMenu.Add("useW", new CheckBox("Use W"));
            LaneClearMenu.Add("sliderW", new Slider("Use W if Kill {0} Minions", 3, 1, 5));
            LaneClearMenu.AddLabel("R Settings");
            LaneClearMenu.Add("useQR", new CheckBox("Use QR", false));
            LaneClearMenu.Add("useWR", new CheckBox("Use WR"));
            LaneClearMenu.Add("sliderWR", new Slider("Use WR if Kill {0} Minions", 5, 1, 5));

            JungleClearMenu = ConfigMenu.AddSubMenu("Jungleclear Menu", "jcMenu");
            JungleClearMenu.AddLabel("Spell Settings");
            JungleClearMenu.Add("useQ", new CheckBox("Use Q"));
            JungleClearMenu.Add("useW", new CheckBox("Use W"));
            JungleClearMenu.Add("useE", new CheckBox("Use E"));
            JungleClearMenu.Add("sliderW", new Slider("Use W if Hit {0} Minions", 3, 1, 5));
            JungleClearMenu.AddLabel("R Settings");
            JungleClearMenu.Add("useQR", new CheckBox("Use QR"));
            JungleClearMenu.Add("useWR", new CheckBox("Use WR"));
            JungleClearMenu.Add("useER", new CheckBox("Use ER"));
            JungleClearMenu.Add("sliderWR", new Slider("Use WR if Hit {0} Minions", 5, 1, 5));

            KillStealMenu = ConfigMenu.AddSubMenu("Killsteal Menu", "ksMenu");
            KillStealMenu.AddLabel("Spell Settings");
            KillStealMenu.Add("useQ", new CheckBox("Use Q"));
            KillStealMenu.Add("useW", new CheckBox("Use W"));
            KillStealMenu.Add("useReturn", new CheckBox("Use W Return"));
            KillStealMenu.Add("useE", new CheckBox("Use E"));
            KillStealMenu.AddLabel("R Settings");
            KillStealMenu.Add("useQR", new CheckBox("Use QR"));
            KillStealMenu.Add("useWR", new CheckBox("Use WR"));
            KillStealMenu.Add("useReturn2", new CheckBox("Use WR Return"));
            KillStealMenu.Add("useER", new CheckBox("Use ER"));
            KillStealMenu.AddLabel("Misc Settings");
            KillStealMenu.Add("useIgnite", new CheckBox("Use Ignite"));
            KillStealMenu.Add("usePrediction", new CheckBox("Use Health Prediction", false));
            KillStealMenu.Add("toggle", new CheckBox("Enable Kill Steal"));

            DrawingMenu = ConfigMenu.AddSubMenu("Drawing Menu", "dMenu");
            DrawingMenu.AddLabel("Range Drawings");
            DrawingMenu.Add("drawQ", new CheckBox("Draw Q Range", false));
            DrawingMenu.Add("drawW", new CheckBox("Draw W Range", false));
            DrawingMenu.Add("drawE", new CheckBox("Draw E Range", false));
            DrawingMenu.AddLabel("DamageIndicator");
            DrawingMenu.Add("draw.Damage", new CheckBox("Draw Damage"));
            DrawingMenu.Add("draw.Q", new CheckBox("Calculate Q Damage"));
            DrawingMenu.Add("draw.W", new CheckBox("Calculate W Damage"));
            DrawingMenu.Add("draw.E", new CheckBox("Calculate E Damage"));
            DrawingMenu.Add("draw.R", new CheckBox("Calculate R Damage"));
            DrawingMenu.Add("draw.Ignite", new CheckBox("Calculate Ignite Damage"));

            MiscMenu = ConfigMenu.AddSubMenu("Misc Menu", "mMenu");
            MiscMenu.AddLabel("Miscellaneous");
            MiscMenu.Add("pet", new CheckBox("Automatic Clone Movement"));
            MiscMenu.AddLabel("HitChance : 1 = Low, 2 = Medium, 3 = High");
            MiscMenu.Add("PredW", new Slider("W HitChance", 3, 1, 3));
            MiscMenu.Add("PredE", new Slider("E HitChance", 3, 1, 3));
        }
        
        public static HitChance PredW()
        {
        	var mode = Config.MiscMenu["PredW"].Cast<Slider>().CurrentValue;
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
        
        public static HitChance PredE()
        {
            var mode = Config.MiscMenu["PredE"].Cast<Slider>().CurrentValue;;
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