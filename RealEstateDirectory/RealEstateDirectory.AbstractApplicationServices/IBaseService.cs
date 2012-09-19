using System.Collections.Generic;
using System.Linq;
using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.AbstractApplicationServices
{
    public interface IBaseService<T> where T : Entity<int>
    {
        /// <summary>
        /// �������� ���
        /// </summary>
        /// <returns></returns>
        IList<T> GetAll();

        IQueryable<T> GetQueryable();

        /// <summary>
        /// �������� �� ��
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);

        /// <summary>
        /// ��������� ��������
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Save(T entity);

        /// <summary>
        /// ������� ��������
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
	}
}