using System;
using System.Collections.Generic;
using System.Text;

namespace ExceptionsLibrary
{
   public class GeometryException: Exception
    {
        public GeometryException(): base() { }

        private int[] array= new int[4];
        private int[] setInt(params int[] sides) {
            this.array = sides;
            return array;  
        }

    }
}
