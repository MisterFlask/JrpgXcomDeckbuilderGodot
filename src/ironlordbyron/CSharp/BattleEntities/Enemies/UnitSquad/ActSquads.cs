using Assets.CodeAssets.BattleEntities.Enemies.Summer;
using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.ChessCourt;
using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.Efficiency;
using GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.Summer;
using System.Collections.Generic;
using System.Linq;

namespace GodotStsXcomalike.src.ironlordbyron.CSharp.BattleEntities.Enemies.UnitSquad
{

    public class GameAct
    {
        public int Level { get; set; }
        public List<Squad> Squads { get; set; }

        public List<Squad> EliteSquads { get; set; }

        public List<Squad> BossSquads { get; set; }

        public static Squad GetSquadForAct(int actNumber, SquadType squadType)
        {
            Require.NotNull(ACTS);
            Require.NotNull(squadType);

            var actsMatchingPred = ACTS.Where(item => item != null && item.Level == actNumber);
            var act = actsMatchingPred.FirstOrDefault();
            if (act == null)
            {
                Log.Error("Could not find act " + actNumber);
                act = ACT_ONE;
            }

            if (squadType == SquadType.BOSS)
            {
                return act.BossSquads.PickRandom();
            }
            if (squadType == SquadType.ELITE)
            {
                return act.EliteSquads.PickRandom();
            }
            else
            {
                return act.Squads.PickRandom();
            }
        }


        public static List<GameAct> ACTS = new List<GameAct>
        {
            ACT_ONE
        };

        public static GameAct ACT_ONE = new GameAct()
        {
            Level = 1,
            Squads = new List<Squad>
            {
                new Squad
                {
                    Members = new List<AbstractBattleUnit>
                    {
                        new ClockworkDrone(),
                        new ClockworkDrone(),
                        new ClockworkDrone(),
                    }
                },
                // The Ordo Silicon
                new Squad
                {
                    Members = new List<AbstractBattleUnit>
                    {
                        new EfficiencyAlarmDrone(),
                        new EfficiencyProselytizer()
                    }

                },
                new Squad
                {
                    Members = new List<AbstractBattleUnit>
                    {
                        new EfficiencySpotter(),
                        new EfficiencySpotter(),
                        new EfficiencySubduer()
                    }
                },
                new Squad
                {
                    Members = new List<AbstractBattleUnit>
                    {
                        new EfficiencyMobileRestockUnit(),
                        new EfficiencySubduer(),
                        new EfficiencySubduer()
                    }
                },
                // The Summer Incursion
                new Squad
                {
                    Members = new List<AbstractBattleUnit>
                    {
                        new DisdainfulNymph(),
                        new DisdainfulNymph(),
                    }
                },
                new Squad()
                {
                     Members = new List<AbstractBattleUnit>
                    {
                        new Naturalist(),
                    }
                },
                new Squad()
                {
                    Members = new List<AbstractBattleUnit>
                    {
                        new ParanoidSycophant()
                    }
                },
                // The Chesscourt
                new Squad()
                {
                    Members = new List<AbstractBattleUnit>()
                    {
                        new ConscriptedPawn(),
                        new ConscriptedPawn(),
                        new ConscriptedPawn(),
                        new ConscriptedPawn(),
                        new ConscriptedPawn(),
                    }
                },
                new Squad()
                {
                    Members = new List<AbstractBattleUnit>()
                    {
                        new ConscriptedPawn(),
                        new MeltingKnight()
                    }
                }
            },
            EliteSquads = new List<Squad>
            {
                new Squad()
                {
                    Members = new List<AbstractBattleUnit>
                    {
                        new EfficiencyMobileRestockUnit(),
                        new EfficiencyMobileRestockUnit(),
                        new EfficiencySubduer(),
                        new EfficiencyAlarmDrone(),
                        new EfficiencyAlarmDrone()
                    }
                }
            },
            BossSquads = new List<Squad>
            {
                new Squad()
                {
                    Members= new List<AbstractBattleUnit>
                    {
                         new EfficiencyOrganicAmbassador()
                    }
                },
                new Squad()
                {
                    Members = new List<AbstractBattleUnit>
                    {
                        new RedBishop(),
                        new BlackBishop()
                    }
                },
                new Squad()
                {
                    Members = new List<AbstractBattleUnit>
                    {
                        new Naturalist()
                    }
                }
            }
        };

    }
}

public enum SquadType
{
    ELITE, BOSS, NORMAL
}