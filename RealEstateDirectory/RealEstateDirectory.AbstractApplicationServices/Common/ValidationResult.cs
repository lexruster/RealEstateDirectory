﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateDirectory.AbstractApplicationServices.Common
{
	/// <summary>
	/// Класс для хранения резльутатов каких-либо проверок
	/// </summary>
	public class ValidationResult
	{
		public ValidationResult()
		{
			FailReasons = new List<string>();
		}

		/// <summary>
		/// Валидация успешна?
		/// </summary>
		public bool IsValid
		{
			get { return !FailReasons.Any(); }
		}

		/// <summary>
		/// Причины провала выалидации
		/// </summary>
		protected List<string> FailReasons { get; set; }

		/// <summary>
		/// "Провалить" валидацию с указанием причины
		/// </summary>
		/// <param name="reason"></param>
		public void FailValidation(string reason)
		{
			FailReasons.Add(reason);
		}

		public string GetReasons()
		{
			return String.Join(Environment.NewLine, FailReasons);
		}
	}
}
