using BookManagementApplication.DTO;
using BookManagementApplication.Models;

namespace BookManagementApplication.Interfaces
{
    public interface IJwtService
    {
        public Task<GeneralResponseInternalDTO> GenerateJWT(UserModel user);
    }
}
