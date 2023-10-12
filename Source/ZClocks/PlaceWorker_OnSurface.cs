using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace ZClocks
{
    class PlaceWorker_OnSurface : PlaceWorker
    {
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map, Thing thingToIgnore = null, Thing thing = null)
        {
            List<Thing> atLoc = map.thingGrid.ThingsAt(loc).ToList();

            if (atLoc == null)
            {
                //return "BMT_PlaceOnDisplay".Translate();
                return true;
            }

            foreach (Thing t in atLoc)
            {
                if (t.def.building != null)
                {
                    if (t.def.surfaceType == SurfaceType.None && t.def.IsEdifice())
                    {
                        return false;
                    }
                }
            }

            return true;
        }

    }
}
