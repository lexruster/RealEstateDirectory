using System;

namespace RealEstateDirectory.Infrastructure.Entities
{
    public abstract class Entity<TId>
    {
        public virtual TId Id { get; protected set; }

        private static bool IsTransient(Entity<TId> obj)
        {
            return obj as object != null &&
                   Equals(obj.Id, default(TId));
        }

        /// <summary>
        /// �������� ��� private ������ ��-��� CastleProxy. ��-��� LinFu ������� public (protected) virtual
        /// </summary>
        public virtual Type GetUnproxiedType()
        {
            return GetType();
        }

        protected virtual bool EqualsById(Entity<TId> other)
        {
            if (other as object == null)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (!IsTransient(this) &&
                !IsTransient(other) &&
                Equals(Id, other.Id))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();

                return thisType.IsAssignableFrom(otherType) ||
                       otherType.IsAssignableFrom(thisType);
            }

            return false;
        }

        /// <summary>
        /// ���������� �������� ���������� ������ ������ (��� ��� ������, ���� ������ ���) � ��������� ����.
        /// �������� ���������� �������� as, �.�. ���������� null, ���� ���������� ����������.
        /// </summary>
        public virtual T As<T>() where T : class
        {
            return this as T;
        }

        /// <summary>
        /// ���������, �������� �� �������� ���������� ������ ������ (��� ��� ������, ���� ������ ���) � ��������� ����.
        /// </summary>
        public virtual bool Is<T>() where T : class
        {
            return this is T;
        }

        public override int GetHashCode()
        {
            int hash = 37;
            hash = hash*23 + base.GetHashCode();
            hash = hash*23 + Id.GetHashCode();
            return hash;
        }
    }
}