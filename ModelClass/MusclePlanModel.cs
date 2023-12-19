using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClass
{
    
    public class MusclePlanModel
    {
        public int MuscleID { get; set; }
        public int PlanID { get; set; }
        public string MuscleName { get; set; }
        public string MuscleImageReference { get; set; }
        public string Description { get; set; }
        public string Day { get; set; }
    }
}
