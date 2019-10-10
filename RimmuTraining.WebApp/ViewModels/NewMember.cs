using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RimmuTraining.WebApp.ViewModels
{
    public class NewMember
    {
        [Required]
        public string Name { get; set; }
    }
}
