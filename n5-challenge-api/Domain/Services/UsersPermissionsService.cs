using Domain.DTO;
using Domain.Models.DTO;
using Domain.Repository;
using Nest;

namespace Domain.Services
{
    public class UsersPermissionsService : IUsersPermissionsService
    {
        private readonly IUsersPermissionsRepository _repo;
        private readonly IElasticClient _elasticClient;

        public UsersPermissionsService(IUsersPermissionsRepository repository, IElasticClient elasticClient)
        {
            _repo = repository;
            _elasticClient = elasticClient;
        }

        public async Task<List<Permission>> GetPermissions()
        {
            return await _repo.GetPermissions();
        }

        public async Task<bool> ModifyPermission(PermissionDTOModify permission)
        {      
            var currentPermission = await _repo.GetPermissionsByID(permission.Id);

            await _elasticClient.IndexDocumentAsync(currentPermission);

            var newPermission = await _repo.GetPermissionTypeByID(permission.TipoPermiso);
       
            currentPermission.TipoPermiso = newPermission;
         
            var result = await _repo.ModifyPermission(currentPermission);
    
            return result;
        }

        public async Task<bool> RequestPermission(PermissionDTOAssign permission)
        {
            var newPermissionType = await _repo.GetPermissionTypeByID(permission.TypoPermiso);

            var newPermission = new Permission() { 
                NombreEmpleado = permission.NombreEmpleado,
                ApellidoEmpleado = permission.ApellidoEmpleado,
                TipoPermiso = newPermissionType,
                FechaPermiso = DateTime.Now
            };

            await _elasticClient.IndexDocumentAsync(newPermission);

            return await _repo.RequestPermission(newPermission);
        }
    }
}
