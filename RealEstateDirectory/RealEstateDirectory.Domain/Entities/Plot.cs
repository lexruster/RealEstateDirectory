namespace RealEstateDirectory.Domain.Entities
{
    /// <summary>
    /// ��������� �������
    /// </summary>
    public class Plot : RealEstate
	{
        /// <summary>
        /// ����� �������
        /// </summary>
        public virtual decimal? PlotSquare { get; set; }
	}
}
