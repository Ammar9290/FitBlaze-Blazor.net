using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClass
{
   
        public class ExercisePlanModel
        {
            public int ExerciseID { get; set; }
            public int MuscleID { get; set; }
            public string ExerciseName { get; set; }
            public string ExerciseImageReference { get; set; }
            public string Description { get; set; }
            public int Time { get; set; }
            public int Raps { get; set; }
        }
    }
