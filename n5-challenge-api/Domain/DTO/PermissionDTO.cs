using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Domain.DTO
{
    public class PermissionDTOModify
    {     
        public int Id { get; set; }
        public int TipoPermiso { get; set; }
    }

    public class PermissionDTOAssign
    {
        public string NombreEmpleado { get; set; }
        public string ApellidoEmpleado { get; set; }
        public int TypoPermiso { get; set; }
    }
}
