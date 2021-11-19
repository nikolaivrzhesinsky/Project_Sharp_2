using System;
using System.Collections.Generic;
using System.Text;

namespace ExceptionsLibrary
{
    public class QuadrangleException: GeometryException
    {
        public QuadrangleException() : base() { }

        public override string Message
        {
            get
            {
                return "Невозможно создать четырехугольник для указанных длин сторон";
            }
        }
    }
}
