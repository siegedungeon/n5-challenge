using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.DTO
{
    public class PermissionType
    {
        private string descripcion;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        [Description("Unique ID")]
        public int Id { get; set; }

        [Required]
        [Description("Permission desciption")]
        public string Descripcion { get => descripcion; set => descripcion = value; }
    }
}
