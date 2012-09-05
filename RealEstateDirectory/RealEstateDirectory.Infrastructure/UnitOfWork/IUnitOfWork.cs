using System;

namespace RealEstateDirectory.Infrastructure.UnitOfWork
{
    ///<summary>
    /// Единица работы
    ///</summary>
    public interface IUnitOfWork : IDisposable
    {
        ///<summary>
        /// Сохранить изменения в базу
        ///</summary>
        void Commit();
    }
}