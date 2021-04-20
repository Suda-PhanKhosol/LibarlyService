using System.Collections.Generic;

namespace NetCoreAPI_Template_v3_with_auth.DTOs
{
    public class AssignRoleDto
    {
        public string Username { get; set; }
        public List<RoleDtoAdd> Roles { get; set; }
    }
}