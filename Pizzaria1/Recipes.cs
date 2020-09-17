using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pizzaria1
{
    public class Recipes_
    {
        public int currentPage { get; set; }
        public string Title { get; set; }
        public string Review { get; set; }
        public string Avatar { get; set; }
        public string Icon { get; set; }
        public string Youtube { get; set; }
        public string Color { get; set; } 
        public string Stepr { get; set; }
        public string Description { get; set; }
        public BindingList<string> Imagess { get; set; }
    }
}
