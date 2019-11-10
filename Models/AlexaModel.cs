using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowFinder.Models
{
    public class AlexaModel
    {
        public Guid uid { get; set; }
        public DateTime updateDate { get; set; }
        public string titleText { get; set; }
        public string mainText { get; set; }
        public string redirectionUrl { get; set; }
    }
}
