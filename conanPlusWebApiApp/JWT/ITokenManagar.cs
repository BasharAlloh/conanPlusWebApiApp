using System.Threading.Tasks;

namespace conanPlusWebApiApp.JWT
{
    public interface ITokenManager
    {

        string GenerateToken(string username, string role, int tokenVersion);


        bool ValidateToken(string token);

        void InvalidateCurrentToken();


        bool IsTokenInvalidated(string token);

        Task<int> GetUserTokenVersionAsync(string username);

        string GetCurrentToken();

    }
}
