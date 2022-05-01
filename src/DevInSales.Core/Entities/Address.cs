using DevInSales.Core.Data.Dtos;

namespace DevInSales.Core.Entities
{
    public class Address : Entity
    {
        public int CityId { get; private set; }
        public string Street { get; private set; }
        public string Cep { get; private set; }
        public int Number { get; private set; }
        public string Complement { get; private set; }
        public City City { get; set; }

        public Address(string street, string cep, int number, string complement, int cityId)
        {
            Street = street;
            Cep = cep;
            Number = number;
            Complement = complement;
            CityId = cityId;
        }

        public void UpdateStreet(string street)
        {
            Street = street;
        }
        public void UpdateCep(string cep)
        {
            Cep = cep;
        }
        public void UpdateNumber(int number)
        {
            Number = number;
        }
        public void UpdateComplement(string complement)
        {
            Complement = complement;
        }
    }
}
