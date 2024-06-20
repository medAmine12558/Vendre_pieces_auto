
namespace Vendre_pieces_auto.Service
{
    public interface IAccessTocken
    {
        public Task<string> GetManagementApiAccessToken();
    }
}
