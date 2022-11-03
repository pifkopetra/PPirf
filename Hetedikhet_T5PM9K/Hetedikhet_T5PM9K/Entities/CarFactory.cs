using Hetedikhet_T5PM9K.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hetedikhet_T5PM9K.Entities
{
    public class CarFactory : Abstractions.IToyFactory
    {
        public Toy CreateNew()
        {
            return new Car();
        }
    }
}
