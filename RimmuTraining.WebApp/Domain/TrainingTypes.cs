using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimmuTraining.WebApp.Domain
{
    public static class TrainingTypes
    {
        public static List<string> Get()
        {
            return new List<string> { "Fighting", "Archery" };
        }
    }
}
