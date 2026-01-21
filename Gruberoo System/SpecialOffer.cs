using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gruberoo_System
{
    public class SpecialOffer
    {
        public string offerCode {  get; set; }
        public string offerDesc { get; set; }
        public double Discount { get; set; }
        public SpecialOffer(string offercode, string offerdesc, double discount)
        {
            offerCode = offercode;
            offerDesc = offerdesc;
            Discount = discount;
        }

        public override string ToString()
        {
            return $"{offerCode}: {offerDesc} - {Discount}% off";
        }
    }
}
