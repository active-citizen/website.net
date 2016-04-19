using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveCitizen.Model.Shared;

namespace ActiveCitizen.Model.User.Badge
{
    abstract class BaseBadge
    {
        public Image Image { get; set; }
        public string Title { get; set; }

        //future badge, that user have a chance to aquire
        public bool IsNext { get; set; }
    }
}
