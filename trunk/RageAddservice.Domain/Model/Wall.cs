using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rage.Addservice.Domain.Model
{
    public class Wall
    {
        public long? Id { get; set; }
        public string Name     { get; set; }
        public string Description { get; set; }

        public byte[] Image    { get; set; }
    }
}
