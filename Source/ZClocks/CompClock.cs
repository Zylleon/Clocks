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
                //Log.Message("Ringing the bell");
                if (parent.GetComp<CompPowerTrader>()?.PowerOn == false)
                {
                    return;
                }
                Props.bellSound.PlayOneShot(SoundInfo.InMap(new TargetInfo(parent.Position, parent.Map)));
            }
        }
    }

    public class CompProperties_Clock : CompProperties
    {
        public SoundDef bellSound;

        public List<int> bellRingTimes;

        public CompProperties_Clock()
        {
            compClass = typeof(CompClock);
        }

        
    }
}
