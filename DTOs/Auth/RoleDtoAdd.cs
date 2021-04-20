using NetCoreAPI_Template_v3_with_auth.Validations;

namespace NetCoreAPI_Template_v3_with_auth.DTOs
{
    public class RoleDtoAdd
    {
        [FirstLetterUpperCase]
        public string RoleName { get; set; }
    }
}