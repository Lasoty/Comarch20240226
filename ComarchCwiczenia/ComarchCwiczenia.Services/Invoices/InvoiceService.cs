using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComarchCwiczenia.Domain.Entities;

namespace ComarchCwiczenia.Services.Invoices
{
    public class InvoiceService
    {
        public InvoiceItem CreateItem(string name, int netValue, int taxValue)
        {
            InvoiceItem item = new()
            {
                ItemName = name,
                NetValue = netValue,
                TaxValue = taxValue,
                GrossValue = CalculateGross(netValue, taxValue),
                Customer = null
            };

            return item;
        }

        private decimal CalculateGross(int netValue, int taxValue)
        {
            return netValue + (netValue * (taxValue / 100m));
        }
    }
}
