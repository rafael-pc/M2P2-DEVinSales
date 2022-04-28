namespace DevInSales.Core.Data.Dtos
{
    public class ReadAddress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Cep { get; set; }
        public int Number { get; set; }
        public string Complement { get; set; }
        public ReadAddressCity City { get; set; }
        public ReadCityState State { get; set; }
    }
}