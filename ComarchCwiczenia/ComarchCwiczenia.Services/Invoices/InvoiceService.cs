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
        public InvoiceItem CreateItem(string name, decimal netValue, decimal taxValue)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Nazwa elementu nie może być pusta.", nameof(name));

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

        private decimal CalculateGross(decimal netValue, decimal taxValue)
        {
            return netValue + (netValue * (taxValue / 100m));
        }
    }
}
