using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GprcShareEvent
{
    public record UserRequest(string firstname, string lastname);
    public record UserResponse(string fullname);
}
