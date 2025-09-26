using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetDS.Contract
{
    public record CreateLocationRequest(string region, string city, string strit, string namberHouse, string name);
}
