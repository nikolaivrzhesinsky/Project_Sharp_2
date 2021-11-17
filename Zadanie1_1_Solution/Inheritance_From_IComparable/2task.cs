﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace Inheritance_From_IComparable
{
    public class Animal : IComparable
    {
        public int age { get; set; }
        public string name { get; set; }

        public Animal(int age, string name)
        {
            this.age = age;
            this.name = name;

        }
        public override string ToString()
        {
            return name + " " + age;
        }
        public int CompareTo(object o)
        {
            Animal a = o as Animal;
            if (a != null)
                return this.age.CompareTo(a.age);
            else
                throw new Exception("Невозможно сравнить два объекта");
            
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Animal> animals = new List<Animal>();
            var a1 = new Animal(3, "Lion");
            var a2 = new Animal(2, "Cat");
            var a3 = new Animal(5, "Dog");
            var a4 = new Animal(1, "Parrot");

            animals.Add(a1);
            animals.Add(a2);
            animals.Add(a3);
            animals.Add(a4);

            foreach (object i in animals)
            {
                Console.WriteLine(i);
            }

            animals.Sort();

            foreach (object i in animals)
            {
                Console.WriteLine(i);
            }

        }
    }
}
