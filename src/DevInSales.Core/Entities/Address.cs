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

        public Address(string street, string cep, int number, string complement)
        {
            Street = street;
            Cep = cep;
            Number = number;
            Complement = complement;
        }

        public ReadAddress ToReadAddress(Address address)
        {
            return new ReadAddress
            {
                Id = address.Id,
                Street = address.Street,
                Cep = address.Cep,
                Number = address.Number,
                Complement = address.Complement,
                City = new ReadAddressCity { Id = address.City.Id, Name = address.City.Name },
                State = new ReadCityState
                {
                    Id = address.City.State.Id,
                    Name = address.City.State.Name,
                    Initials = address.City.State.Initials
                }
            };
        }
    }
}
