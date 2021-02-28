using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCoreExample.Models.DataModels
{
    public class CheckoutDataModel
    {
        public int CUSTOMER_ID { get; set; }

        public int BOOK_ID { get; set; }

        public DateTime CHECKOUT_DATE { get; set; }
    }
}
