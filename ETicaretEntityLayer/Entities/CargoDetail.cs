using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretEntityLayer.Entities
{
    public class CargoDetail
    {
        public  int CargoDetailID { get; set; }
        public string SenderCustomer { get; set; }
        public string ReceiverCustomer {  get; set; }
        public int CargoCompanyID {  get; set; }
        public CargoCompany CargoCompany {  get; set; }
        public int Barcode {  get; set; }
    }
}
