using Catel.Data;

namespace RealEstateDirectory.Data.Entities
{
	public class BaseEntity : ModelBase
	{
		public virtual int Id
		{
			get { return GetValue<int>(IdProperty); }
			set { SetValue(IdProperty, value); }
		}

		public static readonly PropertyData IdProperty = RegisterProperty<BaseEntity, int>(model => model.Id, -1);

	}
}