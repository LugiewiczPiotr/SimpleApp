namespace SimpleApp.Core.Interfaces.Logics
{
    public interface IAccountService : ILogic
    {
        public string GenerateJwt(string login);
    }
}
