using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Media;

namespace Core.Models
{
    public class SampleOrder
    {
        public long OrderID { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime RequiredDate { get; set; }

        public DateTime ShippedDate { get; set; }

        public string ShipperName { get; set; }

        public string ShipperPhone { get; set; }

        public double Freight { get; set; }

        public string Company { get; set; }

        public string ShipTo { get; set; }

        public double OrderTotal { get; set; }

        public string Status { get; set; }

        public char Symbol => (char)SymbolCode;

        public int SymbolCode { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }
        
        public ImageSource ProfilePicture { get; set; }

        public ICollection<SampleOrderDetail> Details { get; set; }

        public override string ToString()
        {
            return $"{Company} {Status}";
        }

        public string ShortDescription => $"Order ID: {OrderID}";
    }
}
