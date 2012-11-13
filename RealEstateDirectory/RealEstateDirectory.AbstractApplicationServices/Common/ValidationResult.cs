using System;
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
        /// <summary>
        /// Валидация успешна?
        /// </summary>
        public bool IsValid { get; protected set; }

        /// <summary>
        /// Причины провала выалидации
        /// </summary>
        public List<string> FailReasons { get; set; }

        /// <summary>
        /// "Провалить" валидацию с указанием причины
        /// </summary>
        /// <param name="reason"></param>
        public void FailValidation(string reason)
        {
            IsValid = false;
            FailReasons.Add(reason);
        }

        public string GetReasons()
        {
            return string.Join(";", FailReasons);
        }

        public ValidationResult()
        {
            FailReasons=new List<string>();
            IsValid = true;
        }
    }
}
