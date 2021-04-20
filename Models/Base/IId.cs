using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAPI_Template_v3_with_auth.Models
{
    public interface IId
    {
        public Guid Id { get; set; }
    }
}