using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using RealEstateDirectory.Domain.Entities.Dictionaries;
using RealEstateDirectory.Infrastructure.Entities;

namespace DataTransfer
{
	public class DataResolveHelper : IDisposable
	{
		public Dictionary<Type, List<IdMap>> IdDictionaryMaps
		{
			get
			{
				if (_idDictionaryMaps == null)
				{
					throw new Exception("Не установлена коллекция мапинга.");
				}
				return _idDictionaryMaps;
			}
			set { _idDictionaryMaps = value; }
		}

		private readonly ISession _hbSession;
		private Dictionary<Type, List<IdMap>> _idDictionaryMaps;

		public DataResolveHelper(ISession hbSession)
		{
			_hbSession = hbSession;
		}

		public T ResolveHbEntity<T>(Variant entity) where T : BaseDictionary
		{
			if(typeof(T) != typeof(DealVariant))
				throw new Exception("Не верный тип.");
			if (entity != null)
				return ResolveHbEntity<T>(entity.idVariant);

			return null;
		}

		public T ResolveHbEntity<T>(District entity) where T : BaseDictionary
		{
			if (typeof(T) != typeof(RealEstateDirectory.Domain.Entities.Dictionaries.District))
				throw new Exception("Не верный тип.");
			if (entity != null)
				return ResolveHbEntity<T>(entity.idDistrict);

			return null;
		}

		public T ResolveHbEntity<T>(Balcony entity) where T : BaseDictionary
		{
			if (typeof(T) != typeof(Terrace))
				throw new Exception("Не верный тип.");
			if (entity != null)
				return ResolveHbEntity<T>(entity.idBalcony);

			return null;
		}

		public T ResolveHbEntity<T>(Rielter entity) where T : BaseDictionary
		{
			if (typeof(T) != typeof(Realtor))
				throw new Exception("Не верный тип.");
			if (entity != null)
				return ResolveHbEntity<T>(entity.idRielter);

			return null;
		}

		public T ResolveHbEntity<T>(Street entity) where T : BaseDictionary
		{
			if (typeof(T) != typeof(RealEstateDirectory.Domain.Entities.Dictionaries.Street))
				throw new Exception("Не верный тип.");
			if (entity != null)
				return ResolveHbEntity<T>(entity.idStreet);

			return null;
		}

		public T ResolveHbEntity<T>(SanUsel entity) where T : BaseDictionary
		{
			if (typeof(T) != typeof(ToiletType))
				throw new Exception("Не верный тип.");
			if (entity != null)
				return ResolveHbEntity<T>(entity.idSanUsel);

			return null;
		}

		public T ResolveHbEntity<T>(WallMatherial entity) where T : BaseDictionary
		{
			if (typeof(T) != typeof(Material))
				throw new Exception("Не верный тип.");
			if (entity != null)
				return ResolveHbEntity<T>(entity.idWallMatherial);

			return null;
		}

		public T ResolveHbEntity<T>(Planing entity) where T : BaseDictionary
		{
			if (typeof(T) != typeof(Layout))
				throw new Exception("Не верный тип.");
			if (entity != null)
				return ResolveHbEntity<T>(entity.idPlaning);

			return null;
		}

		private T ResolveHbEntity<T>(int lId) where T : BaseDictionary
		{
			var hbId = ResolveHbId<T>(lId);
			var en = _hbSession.Get<T>(hbId);

			return en;
		}

		public int ResolveHbId<T>(int linqId) where T : BaseDictionary
		{
			return IdDictionaryMaps[typeof(T)].Single(x => x.LinqIds.Contains(linqId)).HbId;
		}

		public void Dispose()
		{
			IdDictionaryMaps = null;
		}
	}
}