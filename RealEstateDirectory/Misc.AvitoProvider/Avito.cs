using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Misc.AvitoProvider
{
    public class Avito
    {
        public const int MaxVar = 6;
        public const string Room = "Комнаты";
        public const string Flat = "Квартиры";
        public const string House = "Дома, дачи, коттеджи";
        public const string Plot = "Земельные участки";
        //public const string Flat = "Гаражи и стоянки";
        public const string Residence = "Коммерческая недвижимость";

        public const string Sale = "Продам";
        public const string Old = "Вторичка";


        private readonly Ads _ad;
        public List<AdsAD> Ads { get; set; }
        private readonly string _fullPath;

        public Avito(bool needClear)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                    "RealEstateDirectory");
            _fullPath = Path.Combine(path, "data.xml");

            if (needClear)
            {
                Ads = new List<AdsAD>();
                _ad = new Ads
                    {
                        formatVersion = 1,
                        target = "Avito.ru"
                    };
            }
            else
            {
                //Deserialize the file into an instance of the class
                var xs = new XmlSerializer(typeof (Ads));
                using (var fs = new FileStream(_fullPath, FileMode.Open))
                {
                    _ad = (Ads) xs.Deserialize(fs);
                    fs.Close();
                    Ads = _ad.Ad.ToList();
                }
            }
        }

        public void AddAd(AdsAD ad, int variant)
        {
            var tempId = String.Format("{0}_{1}", ad.Id, variant);
            if (Ads.All(x => x.Id != tempId))
            {
                ad.Id = tempId;
                DoVariant(ad, variant);
                Ads.Add(ad);
            }
        }

        private void DoVariant(AdsAD ad, int variant)
        {
            ModeCommon(ad, variant);
            switch (ad.Category)
            {
                case Room:
                    ad.Square += variant;
                    //ad.Floors = (byte) ModuleSumWithMin(1, 16, ad.Floors, variant);
                    ad.Floor = (byte) ModuleSumWithMin(1, ad.Floors, ad.Floor, variant);

                    ad.Rooms = (byte) ModuleSumWithMin(1, 4, ad.Rooms, variant);
                    ad.SaleRooms = (byte) ModuleSumWithMin(1, ad.Rooms, ad.SaleRooms, variant);

                    break;

                case Flat:
                    ad.Square += variant;
                    //ad.Floors = (byte) ModuleSumWithMin(1, 16, ad.Floors, variant);
                    ad.Floor = (byte) ModuleSumWithMin(1, ad.Floors, ad.Floor, variant);
                    ad.Rooms = (byte) ModuleSumWithMin(1, 4, ad.Rooms, variant);

                    break;

                case House:
                    ad.Square += variant;

                    break;

                case Plot:
                    ad.Square += variant;
                    ad.LandArea += variant;

                    break;

                case Residence:
                    ad.Square += variant;
                    break;


            }
        }

        private void ModeCommon(AdsAD ad, int variant)
        {
            ad.Price = ModPrice(ad.Price, variant);
            ad.Street = ModStreet(ad.Street, variant);
        }

        private string ModStreet(string street, int variant)
        {
            var streetD = street.Split(',');
            if (streetD.Length > 0)
            {
                var num = streetD[streetD.Length - 1];
                if (!string.IsNullOrEmpty(num))
                {
                    int res;
                    if (Int32.TryParse(num, out res))
                    {
                        var newD = res + variant;
                        return street.Replace(res.ToString(), newD.ToString());
                    }
                }
            }

            return street;
        }

        private int ModuleSumWithMin(int min, int module, int current, int variant)
        {
            if (module == 0)
            {
                return 0;
            }
            var normalCurrent = current - min + variant;
            var result = normalCurrent%module;
            return result + min;
        }

        private decimal ModPrice(decimal price, int variant)
        {
            var pr = price + 50*(3 - variant);
            return pr;
        }

        public void Save()
        {
            _ad.Ad = Ads.ToArray();

            var xs = new XmlSerializer(typeof (Ads));
            using (var fs = new FileStream(_fullPath, FileMode.Create))
            {
                xs.Serialize(fs, _ad);
                fs.Close();
            }
        }
    }
}