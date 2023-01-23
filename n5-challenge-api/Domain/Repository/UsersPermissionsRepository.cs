using Azure.Core;
using Domain.Models;
using Domain.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Security;

namespace Domain.Repository
{
    public class UsersPermissionsRepository : IUsersPermissionsRepository
    {
        private readonly UsersContext _context;

        public UsersPermissionsRepository(UsersContext context)
        {
            _context = context;
        }

        public async Task<List<Permission>> GetPermissions()
        {
            return await _context.Permissions.
                 Include(a => a.TipoPermiso)
                .ToListAsync();
        }
        public async Task<Permission> GetPermissionsByID(int Id)
        {
            var result = await _context.Permissions.Where(a => a.Id == Id).FirstOrDefaultAsync();
            
            return result!=null? result : throw new Exception("Permission Not Found");
        }
        public async Task<PermissionType> GetPermissionTypeByID(int Id)
        {
            var result = await _context.PermissionTypes.Where(a => a.Id == Id).FirstOrDefaultAsync();

            return result != null ? result : throw new Exception("Permission Not Found");
        }

        public async Task<bool> RequestPermission(Permission permission)
        {
            _context.Permissions.Add(permission);
            var result = await _context.SaveChangesAsync();

            return result > 0 ? true : false;
        }

        public async Task<bool> ModifyPermission(Permission permission)
        {
            _context.Entry(permission).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();

            return result > 0? true: false;
        }
    }
}
