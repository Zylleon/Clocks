using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace ZClocks
{

    public class CompClock : ThingComp
    {
        public CompProperties_Clock Props => (CompProperties_Clock)props;

        public override void CompTick()
        {
            base.CompTick();

            int hour = GenLocalDate.HourInteger(parent.Map);
            if (Props.bellRingTimes.Contains(hour) && Find.TickManager.TicksAbs % 2500 == 0)
            {
                if (parent.GetComp<CompPowerTrader>()?.PowerOn == false)
                {
                    return;
                }

                Props.bellSound.PlayOneShot(SoundInfo.InMap(new TargetInfo(parent.Position, parent.Map)));

                Room room = parent.GetRoom();

                List<Thing> roomThings = new List<Thing>(room?.ContainedAndAdjacentThings);
                if (roomThings.NullOrEmpty() == false)
                {
                    foreach (Thing t in roomThings)
                    {
                        if (t.def.race?.Humanlike == true && IntVec3Utility.DistanceTo(t.Position, parent.Position) <= Props.alarmRadius)
                        {
                            Pawn pawn = t as Pawn;
                            if (pawn?.CurJobDef == JobDefOf.LayDown)
                            {
                                pawn.drafter.Drafted = true;
                                pawn.drafter.Drafted = false;
                                PawnUtility.ForceWait(pawn, 200);
                            }
                        }
                    }
                }
                
            }

        }
    }

    public class CompProperties_Clock : CompProperties
    {
        public SoundDef bellSound;

        public List<int> bellRingTimes;

        public float alarmRadius = 4;

        public CompProperties_Clock()
        {
            compClass = typeof(CompClock);
        }

        
    }
}
