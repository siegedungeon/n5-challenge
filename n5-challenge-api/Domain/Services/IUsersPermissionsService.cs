using Domain.DTO;
using Domain.Models.DTO;

namespace Domain.Services
{
    public interface IUsersPermissionsService
    {
        Task<List<Permission>> GetPermissions();
        Task<bool> RequestPermission(PermissionDTOAssign permission);
        Task<bool> ModifyPermission(PermissionDTOModify permission);
    }
}
