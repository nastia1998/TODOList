using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOList.BLL.DTO
{
    public class EmailDTO
    {
        public string EmailFrom { get; set; }
        public string Password { get; set; }
        public string EmailTo { get; set; }
        public string Message { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }

    }
}
