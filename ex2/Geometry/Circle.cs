using System;
using System.Collections.Generic;
using System.Text;
using ExceptionsLibrary;

namespace GeometryLibrary
{
   public class Circle
    {
        private int radius;

        public Circle(int radius)
        {
            if (radius <= 0)
            {
                throw new CircleException();
            }
            this.radius = radius;
        }
    }
}
