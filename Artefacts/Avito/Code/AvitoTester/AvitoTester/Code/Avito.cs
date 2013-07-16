using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AvitoTester.Code
{
    public class Avito
    {

        public void Run()
        {

            var ads = new Ads();

            ads.formatVersion = 1;
            ads.target = "Avito.ru";

            ads.Ad = new AdsAD[3];

            ads.Ad[0] = GetAd(1);
            ads.Ad[1] = GetAd(2);
            ads.Ad[2] = GetAd(3);

            //Serialize the class into a file
            XmlSerializer xs = new XmlSerializer(typeof (Ads));
            FileStream fs = new FileStream(@"D:\Test\test.xml", FileMode.Create);
            xs.Serialize(fs, ads);
            fs.Close();




            //Deserialize the file into an instance of the class
            xs = new XmlSerializer(typeof (Ads));
            fs = new FileStream(@"D:\Test\test.xml", FileMode.Open);
            Ads mc = (Ads) xs.Deserialize(fs);
            fs.Close();

        }

        private AdsAD GetAd(int i)
        {
            var ad = new AdsAD
                {
                    AdStatus = "Free",
                    Category = "Комнаты",
                    City = "Волгоград",
                    ContactPhone = "+7 984 541 84 45",
                    Description = "Лучшая комната " + i,
                    Floor = 1,
                    FloorSpecified = true,
                    Floors = 2,
                    HouseType = "Кирпичный",
                    Id = "1_" + i,
                    OperationType = "Продам",
                    SaleRooms = 2,
                    Rooms = 3,
                    Square = 14.0M,

                    MarketType = "MarketType",
                    Region = "Вологодская область",
                    Street = "адрес",
                    Price = 57,

                };

            return ad;
        }
    }
}