using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibanonProject.Models
{
    public class State
    {
        public int StateId { get; set; }       
        public bool StateBorrow { get; set; } = false;
        public bool StateIsBorrow { get; set; } = false;

        public virtual Book Book { get; set; }
    }
}