using NHibernate;
using NHibernate.Cfg;

namespace RealEstateDirectory.DataAccess
{
    public interface IPersistenceContext
    {
        Configuration Configuration { get; }
        ISessionFactory SessionFactory { get; }
        ISession CurrentSession { get; }
    }
}
