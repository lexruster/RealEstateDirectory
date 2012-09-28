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
    }
}