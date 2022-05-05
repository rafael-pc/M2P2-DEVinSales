using DevInSales.Core.Data.Context;
using DevInSales.EFCoreApi.Core.Interfaces;
using RegexExamples;

namespace DevInSales.Core.Entities
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        public UserService (DataContext context)
        {
            _context = context; 
        }

         public int CriarUser(User user)
        {
            var EmailValido = new EmailValidate();

            if (!EmailValido.IsValidEmail(user.Email) || _context.Users.Any(x=> x.Email == user.Email))
                return 0;

            if (user.BirthDate.AddYears(18) > DateTime.Now)
                return 0;

            if (user.Password.Length < 4)
                return 0;
    
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.Id;
        } 

       private bool StringValida(string? text)
        {
            return !String.IsNullOrWhiteSpace(text);
        }

        public List<User> ObterUsers(string? name, string? DataMin, string? DataMax)
        {
           var result = _context.Users.Where(StringValida(name) && StringValida(DataMin) && StringValida(DataMax) ?
                        x => x.Name == name && x.BirthDate >= DateTime.Parse(DataMin) && x.BirthDate <= DateTime.Parse(DataMax) : // Se todos os campos forem validos, retorna todos os usuarios de mesmo nome entre a data minima e data maxima
                        StringValida(name) && StringValida(DataMin) ? x => x.Name == name && x.BirthDate >= DateTime.Parse(DataMin) ://  se o nome e data minima forem validos, retorna todos os usuarios de mesmo nome e maior ou igual a data minima
                        StringValida(name) && StringValida(DataMax) ? x => x.Name == name && x.BirthDate <= DateTime.Parse(DataMax) :// se o nome e data maxima forem validos, retorna todos os usuarios de mesmo nome e menor ou igual a data maxima
                        StringValida(DataMin) && StringValida(DataMax) ? x => x.BirthDate >= DateTime.Parse(DataMin) && x.BirthDate <= DateTime.Parse(DataMax) : // se data minima e data maxima forem validos, retorna todos os usuarios entre a data minima e data maxima
                        StringValida(name) ? x => x.Name == name :
                        StringValida(DataMin) ? x => x.BirthDate >= DateTime.Parse(DataMin) :
                        StringValida(DataMax) ? x => x.BirthDate <= DateTime.Parse(DataMax) :
                       x => true).ToList();
           return result;
        }
    }
}