using System.Collections.Generic;
using Catel.Data;

namespace RealEstateDirectory.Data.Entities
{
	/// <summary>
	/// Район, регион.
	/// </summary>
	public class District : BaseDictionary
	{
		/// <summary>
		/// Улицы района.
		/// </summary>
		public virtual List<Street> Streets
		{
			get { return GetValue<List<Street>>(StreetsProperty); }
			set { SetValue(StreetsProperty, value); }
		}

		public static readonly PropertyData StreetsProperty = RegisterProperty<District, List<Street>>(model => model.Streets, () => new List<Street>());
	}
}