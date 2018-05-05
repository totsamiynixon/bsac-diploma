using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Domain
{
    public class Criteria : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
