namespace RealEstateDirectory.Domain.Entities
{
    /// <summary>
    /// ��������
    /// </summary>
	public class Room : Apartment
	{
        /// <summary>
        /// ����� ������ � ������������
        /// </summary>
        public virtual int? RoomCount { get; set; }
	}
}
