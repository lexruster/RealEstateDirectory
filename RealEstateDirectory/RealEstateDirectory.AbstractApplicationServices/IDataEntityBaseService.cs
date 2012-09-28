using System.Collections.Generic;
using System.Linq;
using RealEstateDirectory.Infrastructure.Entities;

namespace RealEstateDirectory.AbstractApplicationServices
{
    public interface IDataEntityBaseService<T> where T : Entity<int>
    {
        /// <summary>
        /// �������� ���
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

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
        void Save(T entity);

        /// <summary>
        /// ������� ��������
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// ��������� ������������ �����/���������� ��������
        /// </summary>
        /// <param name="entity"></param>
        bool IsValid(T entity);

        /// <summary>
        /// �������� ����������� ������� ��������.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool IsPossibilityToDelete(T entity);
	}
}