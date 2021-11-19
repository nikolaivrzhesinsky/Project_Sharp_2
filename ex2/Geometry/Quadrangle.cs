using System;
using System.Collections.Generic;
using System.Text;
using ExceptionsLibrary;

namespace GeometryLibrary
{
    public class Quadrangle
    {
        private int side1;
        private int side2;
        private int side3;
        private int side4;

        public Quadrangle(int side1, int side2, int side3, int side4)
        {
            if ((side1 <= 0 && side2 <= 0 && side3 <= 0 && side4<=0)&&
                (side1 > side2 + side3+ side4 || side2 > side1 + side3+ side4 || side3 > side1 + side2+ side4 ||side4>side1+ side2+ side3+ side4))
            {

                throw new QuadrangleException();
            }
           

            this.side1 = side1;
            this.side2 = side2;
            this.side3 = side3;
            this.side4 = side4;
        }
    }
}
