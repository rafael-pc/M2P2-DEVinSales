namespace DevInSales.Core.Entities
{
    public class Address
    {
        public int Id { get; private set; }
        public int CityId { get; private set; }
        public string Street { get; private set; }
        public string Cep { get; private set; }
        public int Number { get; private set; }
        public string Complement { get; private set; }

        public Address(string street, string cep, int number, string complement)
        {
            Street = street;
            Cep = cep;
            Number = number;
            Complement = complement;
        }
    }
}