using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.BLL.DTO.HelpDTO
{
    public class EmployeeQueryParameters
    {
        public string? FullName { get; set; }
        public string? Position { get; set; }
        public string? SortBy { get; set; }
        public bool Descending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

}
