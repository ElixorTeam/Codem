using Сodem.Shared.Models;

namespace WebClient.Common;

public interface IUserService
{
    UserModel? GetUser();
}