using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.DTO
{
    public class Permission
    {
        private PermissionType tipoPermiso;
        private string apellidoEmpleado;
        private string nombreEmpleado;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        [Description("Unique ID")]
        public int Id { get; set; }

        [Required]
        [Description("Employee Forename")]
        public string NombreEmpleado { get => nombreEmpleado; set => nombreEmpleado = value; }

        [Required]
        [Description("Employee Surname")]
        public string ApellidoEmpleado { get => apellidoEmpleado; set => apellidoEmpleado = value; }

        [Required]
        [Description("Permission granted on Date")]
        public DateTime FechaPermiso { get; set; }

        [Required]
        [Description("Permission Type")]
        public PermissionType TipoPermiso { get => tipoPermiso; set => tipoPermiso = value; }
    }
}
