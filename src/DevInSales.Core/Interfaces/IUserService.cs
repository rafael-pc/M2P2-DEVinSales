using DevInSales.EFCoreApi.Core.Entities;

namespace DevInSales.EFCoreApi.Core.Interfaces
{
    public interface IUserService
    {
        public List<User> ObterUsers(string name);

        public User? ObterPorId(int id);

        public int CriarUser(User user);

        public void AtualizarUser(User userOriginal, User userAlteracoes);

        public void RemoverUser(int id);
    }
}