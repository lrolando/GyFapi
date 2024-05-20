using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.DTOs
{
    public class Response<T>
    {

        public dynamic status { get; set; }

        public T data { get; set; }

        public string message { get; set; }

    }
}
