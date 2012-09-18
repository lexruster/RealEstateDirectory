using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateDirectory.Infrastructure.Entities;
using RealEstateDirectory.Infrastructure.Repositories;

namespace RealEstateDirectory.Domain.AbstractRepositories
{
    public interface IDictionaryRepository<T> : IRepositoryBaseDictionary<T> where T : BaseDictionary
    {
    }
}
