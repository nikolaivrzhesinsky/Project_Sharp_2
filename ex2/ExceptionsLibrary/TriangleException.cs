using System;
using System.Collections.Generic;
using System.Text;

namespace ExceptionsLibrary
{
   public class TriangleException: GeometryException {

        public TriangleException() : base() { }

        public override string Message
        {
            get
            {
                return "Невозможно создать треугольник для указанных длин сторон";
            }
        }
    }

    
}
