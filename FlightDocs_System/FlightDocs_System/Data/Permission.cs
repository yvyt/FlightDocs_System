﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace FlightDocs_System.Data
{
    [Table("Permission")]
    public class Permission
    {
            [Key]
            [StringLength(255)]
            [Required]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public string Id { get; set; }
            [StringLength(100)]
            [Required]
            public string PermissionName { get; set; }
            public virtual ICollection<RolePermission> RolePermissions { get; set; }
        }
   
}
