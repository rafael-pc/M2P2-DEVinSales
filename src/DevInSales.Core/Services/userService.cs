using DevInSales.EFCoreApi.Core.Data.Context;
using DevInSales.EFCoreApi.Core.Entities;
using DevInSales.EFCoreApi.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevInSales.EFCoreApi.Core.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public List<User> ObterUsers(string name)
        {
            return _context.Users
                .Include(p => p.email)
                .Include(p => p.birthDate)
                .Where(p => string.IsNullOrWhiteSpace(name) || p.Name.Contains(Name))
                .ToList();
        }

        public User? ObterPorId(int id)
        {
            return _context.Users
                .Include(p => p.email)
                .Include(p => p.birthDate)
                .FirstOrDefault(p => p.Id == id);
        }

        public int CriarUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.Id;
        }

        public void AtualizarUser(User userOriginal, User userAlteracoes)
        {
            userOriginal.AlterarDados(userAlteracoes.Nome,
                                      userAlteracoes.Password,
                                      userAlteracoes.Email);
            _context.SaveChanges();
        }

        public void RemoverUser(int id)
        {
            var user = ObterPorId(id);
            if (user == null)
                throw new Exception("O usuario informado não existe");

            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}