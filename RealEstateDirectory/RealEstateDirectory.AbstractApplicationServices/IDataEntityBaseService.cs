using System.Collections.Generic;
using RealEstateDirectory.AbstractApplicationServices.Common;
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
        /// ��������� ������������ �����/���������� ��������.
        /// ��������� ������������ ���������� ����� ��� ���������, ���� ������� ����� ��������, �� ������� � ������ ���� id ������.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id">id ���� ���� ��������� ��������� ��������</param>
        ValidationResult IsValid(T entity, int id = 0);

        /// <summary>
        /// �������� ����������� ������� ��������.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
		ValidationResult IsPossibilityToDelete(T entity);

        void StartSession();
        void StopSession();
    }
}