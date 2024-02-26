using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComarchCwiczenia.Domain.Entities
{
    public class InvoiceItem
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public Customer Customer { get; set; }

        public decimal NetValue { get; set; }

        public decimal GrossValue { get; set; }

        public decimal TaxValue { get; set; }

        public string ItemName { get; set; }
    }
}
