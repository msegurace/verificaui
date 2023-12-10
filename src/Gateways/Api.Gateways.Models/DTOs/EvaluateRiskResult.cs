using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Api.Gateways.Models.DTOs
{
    public class EvaluateRiskResult
    {
        public int admin { get; set; }
        public int time { get; set; }
        public int origin { get; set; }
        public int classification { get; set; }
        public bool error { get; set; }
    }
}
