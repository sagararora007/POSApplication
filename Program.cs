using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string order = "ABCD";
            PointOfSaleTerminal terminal = new PointOfSaleTerminal();
            char[] arr = order.ToCharArray();
            terminal.orderList = order.ToCharArray().Select(c => c.ToString()).ToList();
            terminal.pricingObj = new List<PricingOfItems>();
            terminal.pricingObj = terminal.SetPricing();
            double result = terminal.CalculateTotal(terminal);
        }
        class ItemCountRelation {         
            public string Code { get; set; }
            public int Count { get; set; }
        }

        class PointOfSaleTerminal
        {

            public List<String> orderList { get; set; }
            public double TotalPrice { get; set; }

            public List<PricingOfItems> pricingObj { get; set; }

            public List<PricingOfItems> SetPricing()
            {
                List<PricingOfItems> objList = new List<PricingOfItems>();
                //Hard coding given values We can make this as a input from the admin to set rates accordingly.
                PricingOfItems obj = new PricingOfItems();
                //A
                obj.Item_Code = "A";
                obj.Item_Price = 1.25;
                obj.Item_Volume = 3;
                obj.Item_Volumne_Price = 3.00;
                objList.Add(obj);

                // B
                obj = new PricingOfItems();
                obj.Item_Code = "B";
                obj.Item_Price = 4.25;
                obj.Item_Volume = 0;
                obj.Item_Volumne_Price = 0;
                objList.Add(obj);

                // C
                obj = new PricingOfItems();
                obj.Item_Code = "C";
                obj.Item_Price = 1;
                obj.Item_Volume = 6;
                obj.Item_Volumne_Price = 5;
                objList.Add(obj);


                // D
                obj = new PricingOfItems();
                obj.Item_Code = "D";
                obj.Item_Price = 0.75;
                obj.Item_Volume = 0;
                obj.Item_Volumne_Price = 0;
                objList.Add(obj);

                return objList;
            }

            public double CalculateTotal(PointOfSaleTerminal terminal)
            {
                double total = 0.00;
                var itemCountRelation = terminal.orderList.GroupBy(a => a).Select(z => new ItemCountRelation{Code = z.Key,Count = z.Count() });
                foreach (var item in itemCountRelation)
                {
                    int itemVol = terminal.pricingObj.Where(x => x.Item_Code == item.Code).Select(z => z.Item_Volume).FirstOrDefault();
                    while (itemVol > 0 && item.Count >= itemVol)
                    {
                        total += terminal.pricingObj.Where(x => x.Item_Code == item.Code).Select(z => z.Item_Volumne_Price).FirstOrDefault();
                        item.Count = item.Count - itemVol;
                    }
                    if (item.Count > 0)
                    {
                        total += (terminal.pricingObj.Where(x => x.Item_Code == item.Code).Select(z => z.Item_Price).FirstOrDefault())* item.Count;
                    }
                }
                return total;
            }

        }

        class PricingOfItems
        {
            public string Item_Code { get; set; }
            public double  Item_Price { get; set; }
            public Int32 Item_Volume { get; set; }
            public double Item_Volumne_Price { get; set; }
        }

        

    }
}
