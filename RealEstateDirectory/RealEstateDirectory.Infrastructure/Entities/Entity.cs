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
			return obj != null && Equals(obj.Id, default(TId));
		}

		private Type GetUnproxiedType()
		{
			return GetType();
		}

		public virtual bool Equals(Entity<TId> other)
		{
			if (other == null) return false;
			if (ReferenceEquals(this, other)) return true;

			if (!IsTransient(this) && !IsTransient(this) && Equals(Id, other.Id))
			{
				var otherType = other.GetUnproxiedType();
				var thisType = GetUnproxiedType();
				return thisType.IsAssignableFrom(otherType) ||
				       otherType.IsAssignableFrom(thisType);
			}
			return false;
		}

		/// <summary>
		/// Попытаться привести внутренний объект прокси (или сам объект, если прокси нет) к заданному типу.
		/// Работает аналогично операции as, т.е. возвращает null, если приведение невозможно.
		/// </summary>
		public virtual T As<T>() where T : class
		{
			return this as T;
		}

		/// <summary>
		/// Проверить, возможно ли привести внутренний объект прокси (или сам объект, если прокси нет) к заданному типу.
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
	}
}