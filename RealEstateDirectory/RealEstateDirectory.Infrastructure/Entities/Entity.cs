using System;

namespace RealEstateDirectory.Infrastructure.Entities
{
    public abstract class Entity<TId>
    {
        public virtual TId Id { get; protected set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Entity<TId>);
        }

        private static bool IsTransient(Entity<TId> obj)
        {
            return obj as object != null &&
            Equals(obj.Id, default(TId));
        }

        /// <summary>
        /// �������� ��� private ������ ��-��� CastleProxy. ��-��� LinFu ������� public (protected) virtual
        /// </summary>
        protected virtual Type GetUnproxiedType()
        {
            return GetType();
        }

        protected virtual bool Equals(Entity<TId> other)
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

        public override int GetHashCode()
        {
            if (Equals(Id, default(TId)))
                return base.GetHashCode();

            return Id.GetHashCode();
        }

        public static bool operator ==(Entity<TId> entity1, Entity<TId> entity2)
        {
            if (entity1 as object == null)
            {
                return (entity2 as object == null);
            }
            else
            {
                return entity1.Equals(entity2);
            }
        }

        public static bool operator !=(Entity<TId> object1, Entity<TId> object2)
        {
            return !(object1 == object2);
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
    }
}
