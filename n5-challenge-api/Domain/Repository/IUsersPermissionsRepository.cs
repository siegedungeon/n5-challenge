using Domain.Models.DTO;

namespace Domain.Repository
{
    public interface IUsersPermissionsRepository
    {
        Task<List<Permission>> GetPermissions();
        Task<bool> RequestPermission(Permission permission);
        Task<bool> ModifyPermission(Permission permission);
        Task<Permission> GetPermissionsByID(int Id);
        Task<PermissionType> GetPermissionTypeByID(int Id);

    }
}
