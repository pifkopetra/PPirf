using Hetedikhet_T5PM9K.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hetedikhet_T5PM9K.Entities
{
    public class BallFactory : Abstractions.IToyFactory
    {
        public  Color BallColor { get; set; }
        public Toy CreateNew()
        {
            return new Ball(BallColor);
        }
    }
}
