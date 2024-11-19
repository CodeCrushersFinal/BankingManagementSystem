using System.ComponentModel.DataAnnotations;

namespace BMSWebApi.DTO
{
    public class RoleDTO
    {
        [Required]
        public int RoleId { get; set; }

        [Required]
        public string RoleName {  get; set; }

    }
}
