using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using KA_Lux.DMGHandler;
using Color = System.Drawing.Color;

// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass

namespace KA_Lux
{
    public static class Config
    {
        private static readonly string MenuName = "KA " + Player.Instance.ChampionName;

        private static readonly Menu Menu;

        static Config()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("KA " + Player.Instance.ChampionName);
            Modes.Initialize();
        }

        public static void Initialize()
        {
        }

        public static class Modes
        {
            private static readonly Menu SpellsMenu, FarmMenu, MiscMenu, DrawMenu;
            public static readonly Menu SettingsMenu;

            static Modes()
            {
                SpellsMenu = Menu.AddSubMenu("::SpellsMenu::");
                Combo.Initialize();
                Harass.Initialize();

                FarmMenu = Menu.AddSubMenu("::FarmMenu::");
                LaneClear.Initialize();
                JungleClear.Initialize();
                LastHit.Initialize();

                MiscMenu = Menu.AddSubMenu("::Misc::");
                Misc.Initialize();

                DrawMenu = Menu.AddSubMenu("::Drawings::");
                Draw.Initialize();

                SettingsMenu = Menu.AddSubMenu("::Settings::");
                Settings.Initialize();
            }

            public static void Initialize()
            {
            }

            public static class Combo
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useE;
                private static readonly CheckBox _useESnared;
                private static readonly CheckBox _useR;
                private static readonly CheckBox _useRSnared;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                public static bool UseESnared
                {
                    get { return _useESnared.CurrentValue; }
                }

                public static bool UseR
                {
                    get { return _useR.CurrentValue; }
                }

                public static bool UseRSnared
                {
                    get { return _useRSnared.CurrentValue; }
                }

                static Combo()
                {
                    // Initialize the menu values
                    SpellsMenu.AddGroupLabel("Combo Spells:");
                    _useQ = SpellsMenu.Add("comboQ", new CheckBox("Use Q on Combo ?"));
                    _useE = SpellsMenu.Add("comboE", new CheckBox("Use E on Combo ?"));
                    _useESnared = SpellsMenu.Add("comboESnared", new CheckBox("Only use E if target is snared ?", false));
                    _useR = SpellsMenu.Add("comboR", new CheckBox("Use R on Combo ?"));
                    _useRSnared = SpellsMenu.Add("comboRSnared", new CheckBox("Only use R if target is snared ?"));
                }

                public static void Initialize()
                {
                }
            }

            public static class Harass
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly Slider _manaHarass;

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

                public static int ManaHarass
                {
                    get { return _manaHarass.CurrentValue; }
                }

                static Harass()
                {
                    SpellsMenu.AddGroupLabel("Harass Spells:");
                    _useQ = SpellsMenu.Add("harassQ", new CheckBox("Use Q on Harass ?"));
                    _useW = SpellsMenu.Add("harassW", new CheckBox("Use W on Harass ?"));
                    _useE = SpellsMenu.Add("harassE", new CheckBox("Use E on Harass ?"));
                    SpellsMenu.AddGroupLabel("Harass Settings:");
                    _manaHarass = SpellsMenu.Add("harassMana", new Slider("It will only cast any harass spell if the mana is greater than ({0}).", 30));
                }

                public static void Initialize()
                {
                }
            }

            public static class LaneClear
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useE;
                private static readonly Slider _laneMana;
                private static readonly Slider _eCount;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                public static int LaneMana
                {
                    get { return _laneMana.CurrentValue; }
                }

                public static int ECount
                {
                    get { return _eCount.CurrentValue; }
                }

                static LaneClear()
                {
                    FarmMenu.AddGroupLabel("LaneClear Spells:");
                    _useQ = FarmMenu.Add("laneclearQ", new CheckBox("Use Q on Laneclear ?"));
                    _useE = FarmMenu.Add("laneclearE", new CheckBox("Use E on Laneclear ?"));
                    FarmMenu.AddGroupLabel("LaneClear Settings:");
                    _laneMana = FarmMenu.Add("laneMana", new Slider("It will only cast any laneclear spell if the mana is greater than ({0}).", 30));
                    _eCount = FarmMenu.Add("eCountLaneClear", new Slider("It will only cast E if it`ll hit ({0}).", 3, 1, 6));
                }

                public static void Initialize()
                {
                }
            }

            public static class JungleClear
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useE;
                private static readonly Slider _laneMana;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                public static int LaneMana
                {
                    get { return _laneMana.CurrentValue; }
                }

                static JungleClear()
                {
                    FarmMenu.AddGroupLabel("JungleClear Spells:");
                    _useQ = FarmMenu.Add("jungleclearQ", new CheckBox("Use Q on Laneclear ?"));
                    _useE = FarmMenu.Add("jungleclearE", new CheckBox("Use E on Laneclear ?"));
                    FarmMenu.AddGroupLabel("JungleClear Settings:");
                    _laneMana = FarmMenu.Add("junglelaneMana", new Slider("It will only cast any jungleclear spell if the mana is greater than ({0}).", 30));
                }

                public static void Initialize()
                {
                }
            }

            public static class LastHit
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useE;
                private static readonly Slider _lastMana;
                private static readonly Slider _eCount;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                public static int LastMana
                {
                    get { return _lastMana.CurrentValue; }
                }

                public static int ECount
                {
                    get { return _eCount.CurrentValue; }
                }


                static LastHit()
                {
                    FarmMenu.AddGroupLabel("LastHit Spells:");
                    _useQ = FarmMenu.Add("lasthitQ", new CheckBox("Use Q on LastHit ?"));
                    _useE = FarmMenu.Add("lasthitE", new CheckBox("Use E on LastHit ?"));
                    FarmMenu.AddGroupLabel("LastHit Settings:");
                    _lastMana = FarmMenu.Add("lastMana", new Slider("It will only cast any lasthit spell if the mana is greater than ({0}).", 30));
                    _eCount = FarmMenu.Add("eCountLastHit", new Slider("It will only cast E spell if it`ll hit ({0}).", 2, 1, 6));
                }

                public static void Initialize()
                {
                }
            }

            public static class Misc
            {
                private static readonly CheckBox _interruptSpell;
                private static readonly CheckBox _antiGapCloserSpell;
                private static readonly Slider _miscMana;
                //KS
                private static readonly CheckBox _killStealQ;
                private static readonly CheckBox _killStealE;
                private static readonly CheckBox _killStealR;
                private static readonly Slider _ksMana;
                //W Settings
                private static readonly CheckBox _wDefense;
                private static readonly CheckBox _wDefenseAlly;
                private static readonly Slider _wMana;
                //JungleSteal Settings
                private static readonly CheckBox _jugSteal;
                private static readonly CheckBox _jugStealBlue;
                private static readonly CheckBox _jugStealRed;
                private static readonly CheckBox _jugStealDragon;
                private static readonly CheckBox _jugStealBaron;

                public static bool InterruptSpell
                {
                    get { return _interruptSpell.CurrentValue; }
                }

                public static bool AntiGapCloser
                {
                    get { return _antiGapCloserSpell.CurrentValue; }
                }

                public static int MiscMana
                {
                    get { return _miscMana.CurrentValue; }
                }
                //KS
                public static bool KillStealQ
                {
                    get { return _killStealQ.CurrentValue; }
                }

                public static bool KillStealE
                {
                    get { return _killStealE.CurrentValue; }
                }

                public static bool KillStealR
                {
                    get { return _killStealR.CurrentValue; }
                }

                public static int KillStealMana
                {
                    get { return _ksMana.CurrentValue; }
                }
                //W Settings
                public static bool WDefense
                {
                    get { return _wDefense.CurrentValue; }
                }

                public static int WMana
                {
                    get { return _wMana.CurrentValue; }
                }
                //JungleSteal Settings
                public static bool JungleSteal
                {
                    get { return _jugSteal.CurrentValue; }
                }

                public static bool JungleStealBlue
                {
                    get { return _jugStealBlue.CurrentValue; }
                }

                public static bool JungleStealRed
                {
                    get { return _jugStealRed.CurrentValue; }
                }

                public static bool JungleStealDrag
                {
                    get { return _jugStealDragon.CurrentValue; }
                }

                public static bool JungleStealBaron
                {
                    get { return _jugStealBaron.CurrentValue; }
                }

                static Misc()
                {
                    // Initialize the menu values
                    MiscMenu.AddGroupLabel("Miscellaneous Settings");
                    _interruptSpell = MiscMenu.Add("interruptQ", new CheckBox("Use Q to interrupt spells ?"));
                    _antiGapCloserSpell = MiscMenu.Add("gapcloserQ", new CheckBox("Use Q to antigapcloser spells ?"));
                    _miscMana = MiscMenu.Add("miscMana", new Slider("Min mana to use gapcloser/interrupt spells ?", 20));
                    MiscMenu.AddGroupLabel("KillSteal Settings");
                    _killStealQ = MiscMenu.Add("killstealQ", new CheckBox("Use Q to KS ?"));
                    _killStealE = MiscMenu.Add("killstealE", new CheckBox("Use E to KS ?"));
                    _killStealR = MiscMenu.Add("killstealR", new CheckBox("Use R to KS ?"));
                    _ksMana = MiscMenu.Add("killstealMana", new Slider("Min mana to use KillSteal spells ?", 15));
                    MiscMenu.AddGroupLabel("W Settings");
                    _wDefense = MiscMenu.Add("safetyW", new CheckBox("Use W when the player is receiving a spell ?"));
                    _wDefenseAlly = MiscMenu.Add("safetyWAlly", new CheckBox("Use W when an ally is receiving a spell ?"));
                    _wMana = MiscMenu.Add("wMana", new Slider("Min mana to use W ?", 10));
                    MiscMenu.AddGroupLabel("JungleStea; Settings");
                    _jugSteal = MiscMenu.Add("jungleSteal", new CheckBox("JungleSteal using R ?"));
                    MiscMenu.AddSeparator(1);
                    _jugStealBlue = MiscMenu.Add("junglestealBlue", new CheckBox("JungleSteal Blue ?"));
                    _jugStealRed = MiscMenu.Add("junglestealRed", new CheckBox("JungleSteal Red ?", false));
                    _jugStealDragon = MiscMenu.Add("junglestealDrag", new CheckBox("JungleSteal Dragon ?"));
                    _jugStealBaron = MiscMenu.Add("junglestealBaron", new CheckBox("JungleSteal Baron ?"));
                }

                public static void Initialize()
                {
                }
            }

            public static class Draw
            {
                private static readonly CheckBox _drawReady;
                private static readonly CheckBox _drawHealth;
                private static readonly CheckBox _drawPercent;
                private static readonly CheckBox _drawStatiscs;
                private static readonly CheckBox _drawQ;
                private static readonly CheckBox _drawW;
                private static readonly CheckBox _drawE;
                private static readonly CheckBox _drawR;
                //Color Config
                private static readonly ColorConfig _qColor;
                private static readonly ColorConfig _wColor;
                private static readonly ColorConfig _eColor;
                private static readonly ColorConfig _rColor;
                private static readonly ColorConfig _healthColor;

                //CheckBoxes
                public static bool DrawReady
                {
                    get { return _drawReady.CurrentValue; }
                }

                public static bool DrawHealth
                {
                    get { return _drawHealth.CurrentValue; }
                }

                public static bool DrawPercent
                {
                    get { return _drawPercent.CurrentValue; }
                }

                public static bool DrawStatistics
                {
                    get { return _drawStatiscs.CurrentValue; }
                }

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

                public static bool DrawR
                {
                    get { return _drawR.CurrentValue; }
                }
                //Colors
                public static Color HealthColor
                {
                    get { return _healthColor.GetSystemColor(); }
                }

                public static SharpDX.Color QColor
                {
                    get { return _qColor.GetSharpColor(); }
                }

                public static SharpDX.Color WColor
                {
                    get { return _wColor.GetSharpColor(); }
                }

                public static SharpDX.Color EColor
                {
                    get { return _eColor.GetSharpColor(); }
                }
                public static SharpDX.Color RColor
                {
                    get { return _rColor.GetSharpColor(); }
                }

                static Draw()
                {
                    DrawMenu.AddGroupLabel("Spell drawings Settings :");
                    _drawReady = DrawMenu.Add("drawOnlyWhenReady", new CheckBox("Draw the spells only if they are ready ?"));
                    _drawHealth = DrawMenu.Add("damageIndicatorDraw", new CheckBox("Draw damage indicator ?"));
                    _drawPercent = DrawMenu.Add("percentageIndicatorDraw", new CheckBox("Draw damage percentage ?"));
                    _drawStatiscs = DrawMenu.Add("statiscsIndicatorDraw", new CheckBox("Draw damage statistics ?"));
                    DrawMenu.AddSeparator(1);
                    _drawQ = DrawMenu.Add("qDraw", new CheckBox("Draw Q spell range ?"));
                    _drawW = DrawMenu.Add("wDraw", new CheckBox("Draw W spell range ?"));
                    _drawE = DrawMenu.Add("eDraw", new CheckBox("Draw E spell range ?"));
                    _drawR = DrawMenu.Add("rDraw", new CheckBox("Draw R spell range ?"));

                    _healthColor = new ColorConfig(DrawMenu, "healthColorConfig", Color.Orange, "Color Damage Indicator:");
                    _qColor = new ColorConfig(DrawMenu, "qColorConfig", Color.Blue, "Color Q:");
                    _wColor = new ColorConfig(DrawMenu, "wColorConfig", Color.Red, "Color W:");
                    _eColor = new ColorConfig(DrawMenu, "eColorConfig", Color.DeepPink, "Color E:");
                    _rColor = new ColorConfig(DrawMenu, "rColorConfig", Color.Yellow, "Color R:");
                }

                public static void Initialize()
                {
                }
            }

            public static class Settings
            {
                //Danger
                private static readonly Slider EnemyRange;
                private static readonly Slider EnemySlider;
                private static readonly CheckBox Spells;
                private static readonly CheckBox Skillshots;
                private static readonly CheckBox AAs;


                public static int RangeEnemy
                {
                    get { return EnemyRange.CurrentValue; }
                }

                public static int EnemyCount
                {
                    get { return EnemySlider.CurrentValue; }
                }

                public static bool ConsiderSpells
                {
                    get { return Spells.CurrentValue; }
                }

                public static bool ConsiderSkillshots
                {
                    get { return Skillshots.CurrentValue; }
                }

                public static bool ConsiderAttacks
                {
                    get { return AAs.CurrentValue; }
                }

                static Settings()
                {
                    SettingsMenu.AddGroupLabel("Danger Settings");
                    EnemySlider = SettingsMenu.Add("minenemiesinrange", new Slider("Min enemies in the range determined below", 1, 1, 5));
                    EnemyRange = SettingsMenu.Add("minrangeenemy", new Slider("Enemies must be in ({0}) range to be in danger", 1000, 600, 2500));
                    Spells = SettingsMenu.Add("considerspells", new CheckBox("Consider spells ?"));
                    Skillshots = SettingsMenu.Add("considerskilshots", new CheckBox("Consider SkillShots ?"));
                    AAs = SettingsMenu.Add("consideraas", new CheckBox("Consider Auto Attacks ?"));
                    SettingsMenu.AddSeparator();
                    SettingsMenu.AddGroupLabel("Dangerous Spells");
                    foreach (var spell in DangerousSpells.Spells.Where(x => EntityManager.Heroes.Enemies.Any(b => b.Hero == x.Hero)))
                    {
                        SettingsMenu.Add(spell.Hero.ToString() + spell.Slot, new CheckBox(spell.Hero + " - " + spell.Slot + ".", spell.DefaultValue));
                    }
                }

                public static void Initialize()
                {
                }
            }
        }
    }
}