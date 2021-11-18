using System;

namespace ex_1._5
{
    public interface IToolKit
    {
        public string[] GetTools();
    }
    public interface IParts
    {
        public string[] GetParts();
    }

    class Table:IToolKit,IParts
    {
       
        private string[] listTool = {"напильник","пила" };
        private string[] listParts = { "ножки","колесики "};
        public string[] GetTools()
        {
            return listTool;
        }
        public string[] GetParts()
        {
            return listParts;
        }
    }

    class Chair : IToolKit, IParts
    {
       
        private string[] listTool = { "отвертка", "инструкция из Икеи" };
        private string[] listParts = { "спинка", "ножки ", " сидушка " };
        public string[] GetTools()
        {
            return listTool;
        }
        public string[] GetParts()
        {
            return listParts;
        }
    }
    

    class FurnitureKit<T1, T2> where T1 : Table, new() where T2 : Chair, new()
    {
        private string name;
        private string color;
        T1 table = new T1();
        T2 chair = new T2();
        public FurnitureKit()
        {

        }
        public FurnitureKit(string name, string color)
        {
            this.name = name;
            this.color = color;
        }
        public void fPrint()
        {
            Console.WriteLine("\n");
            Console.WriteLine( $"Name {name} Цвет {color}");
            foreach(string temp in table.GetParts())
            {
                Console.Write(temp+" ");
            }
            foreach(string temp in table.GetTools())
            {
                Console.Write(temp+" ");
            }
        }
    }


     

        class Program
    {
        static void Main(string[] args)
        {
            FurnitureKit<Table, Chair> obj1 = new FurnitureKit<Table, Chair>("Душевный","Розовый");
            obj1.fPrint();
        }
        
    }
}
