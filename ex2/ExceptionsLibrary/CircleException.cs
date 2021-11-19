using System;
using System.Collections.Generic;
using System.Text;

namespace ExceptionsLibrary
{
   public class CircleException: GeometryException
    {
        public CircleException() : base() { }

        public override string Message
        {
            get
            {
                return "Невозможно создать окружность";
            }
        }
    }
}
