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

        public string GenerateInvoiceNumber()
        {
            var datePart = DateTime.Now.ToString("yyyyMM");
            var randomPart = new Random().Next(100, 999).ToString();

            return $"FV-{datePart}-{randomPart}";
        }

        public IEnumerable<InvoiceItem> GenerateInvoiceItems()
        {
            return new InvoiceItem[]
            {
                new InvoiceItem(){ ItemName = "Item 1", Quantity = 1, NetValue = 100, TaxValue = 23, GrossValue = 123},
                new InvoiceItem(){ ItemName = "Item 2", Quantity = 2, NetValue = 200, TaxValue = 23, GrossValue = 246},
                new InvoiceItem(){ ItemName = "Item 3", Quantity = 3, NetValue = 300, TaxValue = 23, GrossValue = 369}
            };
        }

        public Invoice CreateInvoice(IEnumerable<InvoiceItem> items)
        {

            Invoice invoice = new Invoice();
            InvoiceCreated?.Invoke(this, invoice);
            return invoice;
        }

        public event EventHandler<Invoice>? InvoiceCreated;
    }
}
