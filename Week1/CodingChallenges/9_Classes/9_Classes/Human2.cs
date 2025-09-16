using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("9_ClassesChallenge.Tests")]
namespace _9_ClassesChallenge
{
    internal class Human2: Human
    {
        private string eyeColor;
        private int age;
        private int weight;
        public int Weight
        {
            get => weight;
            set
            {
                if (value < 0 || value > 400)
                    weight = 0;
                else
                    weight = value;
            }
        }

        public Human2(): base() {}
        public Human2(string firstName, string lastName, string eyeColor, int age): base(firstName, lastName)
        {
            this.eyeColor = eyeColor;
            this.age = age;
        }

        public Human2(string firstName, string lastName, int age) : base(firstName, lastName)
        {
            this.age = age;
        }

        public Human2(string firstName, string lastName, string eyeColor) : base(firstName, lastName)
        {
            this.eyeColor = eyeColor;
        }

        public string AboutMe2()
        {
            string output = this.AboutMe();
            if (this.age != 0) output += $" My age is {this.age}.";
            if (this.eyeColor != null) output += $" My eye color is {this.eyeColor}.";
            return output;
        }
    }
}