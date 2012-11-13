using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RealEstateDirectory.Domain.Entities;
using RealEstateDirectory.Infrastructure.Entities;
using RealEstateDirectory.Infrastructure.Repositories;

namespace RealEstateDirectory.Domain.AbstractRepositories
{
    public interface IRealEstateRepository : IRepositoryWithTypedId<int>
    {
        IEnumerable<Plot> GetAllPlot();
    }
}
