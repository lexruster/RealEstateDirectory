using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using RealEstateDirectory.DataAccess;
using RealEstateDirectory.Domain.AbstractRepositories;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Infrastructure.Entities;
using RealEstateDirectory.Infrastructure.NHibernate.Repositories;
using RealEstateDirectory.Infrastructure.Repositories;

namespace RealEstateDirectory.Domain.Data.Repository
{
    public class RealEstateRepository : RepositoryWithTypedIdBase<int>, IRealEstateRepository
    {
        public RealEstateRepository(IPersistenceContext persistentContext)
            : base(persistentContext)
        {

        }

        public IEnumerable<Plot> GetAllPlot()
        {
            return CurrentSession.Query<Plot>().Where(x => x is Plot);
        }

		public IEnumerable<Flat> GetAllFlat()
		{
			return CurrentSession.Query<Flat>().Where(x => x is Flat);
		}

		public IEnumerable<Room> GetAllRoom()
		{
			return CurrentSession.Query<Room>().Where(x => x is Room);
		}

		public IEnumerable<Residence> GetAllResidence()
		{
			return CurrentSession.Query<Residence>().Where(x => x is Residence);
		}

		public IEnumerable<House> GetAllHouse()
		{
			return CurrentSession.Query<House>().Where(x => x is House);
		}
    }
}