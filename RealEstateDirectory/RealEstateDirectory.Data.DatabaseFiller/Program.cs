using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

using RealEstateDirectory.Infrastructure.Entities;
using RealEstateDirectory.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using RealEstateDirectory.Domain.Data.Config;
using RealEstateDirectory.Domain.Entities.Dictionaries;

namespace RealEstateDirectory.Data.DatabaseFiller
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var config = new Configuration();
			config.Configure("hibernate.cfg.xml");

			Configurator.InitProperties(config);
			Configurator.InitMapping(config);

			bool succes = true;
			try
			{
				using (var session = config.BuildSessionFactory().OpenSession())
				{
					var tran = session.BeginTransaction(IsolationLevel.Serializable);
					MoveData(session);
					tran.Commit();
					session.Flush();
				}
			}
			catch (Exception ex)
			{
				succes = false;
				Console.WriteLine(ex.Message);
				if (ex.InnerException != null)
				{
					Console.WriteLine(ex.InnerException.Message);
					if (ex.InnerException.InnerException != null)
						Console.WriteLine(ex.InnerException.InnerException.Message);
				}
			}

			Console.WriteLine("Результат инициализации: {0}", succes ? "успешен" : "ОШИБКА");
			Console.ReadKey();
		}

		private static void MoveData(ISession session)
		{
			var importDist = new Dictionary<int, District>();
			using (var sr = new StreamReader("district.txt", Encoding.UTF8))
			{
				while (!sr.EndOfStream)
				{
					var data = sr.ReadLine();
					var splits = data.Split(';');
					var distCode = Int32.Parse(splits[0]);
					var distName = splits[1];
					var streetName = splits[2];
					if (importDist.ContainsKey(distCode))
					{
						if(importDist[distCode].Streets.All(x=>x.Name.ToLower()!=streetName.ToLower()))
						{
							importDist[distCode].AddStreet(new Street(streetName));	
						}
					}
					else
					{
						var district = new District(distName);
						district.AddStreet(new Street(streetName));
						importDist.Add(distCode, district);
					}
				}
			}

			foreach (var district in importDist)
			{
				var districtVal = district.Value;
				var existDistr = session.Query<District>().FirstOrDefault(x => x.Name.ToLower() == districtVal.Name.ToLower());
				if (existDistr == null)
				{
					session.SaveOrUpdate(districtVal);
				}
				else
				{
					var distNeedSave = false;
					foreach (var street in districtVal.Streets)
					{
						var existStreet = existDistr.Streets.FirstOrDefault(x => x.Name.ToLower() == street.Name.ToLower());
						if (existStreet == null)
						{
							distNeedSave = true;
							existDistr.AddStreet(new Street(street.Name));
						}
					}
					if (distNeedSave)
					{
						session.SaveOrUpdate(districtVal);
					}
				}
			}
		}
	}
}