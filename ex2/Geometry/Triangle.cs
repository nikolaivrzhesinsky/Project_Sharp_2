using System;
using System.Collections.Generic;
using System.Text;
using ExceptionsLibrary;

namespace GeometryLibrary
{
   public class Triangle
    {
        private int side1;
        private int side2;
        private int side3;

        public Triangle(int side1, int side2, int side3)
        {
            if ((side1>side2+side3 || side2>side1+side3||side3>side1+side2)&&(side1 <=0 && side2<= 0 && side3<= 0))
            {
                throw new TriangleException();
                            
            }

            this.side1 = side1;
            this.side2 = side2;
            this.side3 = side3;
        }




    }
}
