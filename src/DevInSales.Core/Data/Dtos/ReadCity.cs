namespace DevInSales.Core.Data.Dtos
{
    public class ReadCity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ReadCityState State { get; set; }
    }
}