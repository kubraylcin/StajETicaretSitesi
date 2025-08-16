using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretEntityLayer.Entities
{
    public class CargoOperation
    {
        public int CargoOperationID { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public DateTime OperationsDate { get; set; }
    }
}
