namespace RealEstateDirectory.Domain.Entities
{
    /// <summary>
    /// Команата
    /// </summary>
	public class Room : Apartment
	{
        /// <summary>
        /// Число комнат в собсвенности
        /// </summary>
        public virtual int? RoomCount { get; set; }
	}
}
