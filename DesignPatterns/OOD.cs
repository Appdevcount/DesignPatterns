using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    //    The following table lists the accessibility keywords;
    //Keyword Description
    //public - Public class is visible in the current and referencing assembly.
    //private - Visible inside current class.
    //protected - Visible inside current and derived class.
    //Internal - Visible inside containing assembly.
    //Internal - protected Visible inside containing assembly and descendent of thecurrent class.

    //    Modifiers refine the declaration of a class. The list of all modifiers defined in the table are as follows;
    //Modifier Description
    //sealed - Class can't be inherited by a derived class.
    //static - Class contains only static members.
    //unsafe - The class that has some unsafe construct likes pointers.
    //Abstract - The instance of the class is not created if the Class is abstract.

    class OOD
    {
    }
    class customer
    {
        // Member Variables  
        public int x, y;
        //constuctor for  initializing fields  
        customer()
        {
            Console.WriteLine("Fields inititalized");
            x = 10;
        }
        //method for get field  
        public void getData()
        {
            y = x * x;
            Console.WriteLine(y);
        }
        //method to release resource explicitly  
        public void Dispose()
        {
            Console.WriteLine("Fields cleaned");
            x = 0;
            y = 0;
        }
        //destructor  
        ~customer()
        {
            Dispose();
        }
        //Entry point  
        static void mainprogrammethod(string[] args)
        {
            //instance created  
            customer obj = new customer();

            obj.getData();

        }
    }
    class Encapsulation
    {
        /// <summary>  
        /// Every member Variable and Function of the class are bind  
        /// with the Encapsulation class object only and safe with   
        /// the outside inference  
        /// </summary>  

        // Encapsulation Begin  
        int x;

        //class constructor  
        public Encapsulation(int iX)
        {
            this.x = iX;
        }

        //calculating the square  
        public void MySquare()
        {
            int Calc = x * x;
            Console.WriteLine(Calc);
        }

        // End of Encapsulation  

        //Entry point  
        static void mainprogrammethod(string[] args)
        {
            //instance created  
            //customer obj = new customer(20);

            //obj.MySquare();

        }

    }
    //    Accessibility sets the visibility of the member to outside assemblies or derived types.The following table describes member accessibility;
    //    Modifiers Outside Assembly Derived Class
    //private No No
    //public Yes Yes
    //protected No No
    //internal    Yes(this assembly only)   Yes(this assembly only)
    //internal protected  Yes(this assembly only)   Yes
    //Constructor in Inheritance


    class myBase
    {
        //virtual function  
        public virtual void VirtualMethod()
        {
            Console.WriteLine("virtual method defined in the base class");
        }
    }

    class myDerived : myBase
    {
        // redifing the implementation of base class method  
        public override void VirtualMethod()
        {
            Console.WriteLine("virtual method defined in the Derive class");
        }
    }
    class virtualClass
    {
        static void mainprogrammethod(string[] args)
        {
            // class instance  
            new myDerived().VirtualMethod();
            Console.ReadKey();
        }
    }

    //class myBase
    //{
    //    //virtual function  
    //    public virtual void VirtualMethod()
    //    {
    //        Console.WriteLine("virtual method defined in the base class");
    //    }
    //}
    //class myDerived : myBase
    //{
    //    // hiding the implementation of base class method  
    //    public new void VirtualMethod()
    //    {
    //        Console.WriteLine("virtual method defined in the Derive class");
    //        //still access the base class method  
    //        base.VirtualMethod();
    //    }
    //}
    //class virtualClass
    //{
    //    static void mainprogrammethod(string[] args)
    //    {
    //        // class instance  
    //        new myDerived().VirtualMethod();
    //        Console.ReadKey();
    //    }
    //}

    //abstract class  
    public abstract class Employess
    {
        //abstract method with no implementation  
        public abstract void displayData();
    }

    //derived class  
    public class test : Employess
    {
        //abstract class method implementation  
        public override void displayData()
        {
            Console.WriteLine("Abstract class method");
        }
    }
    class abstractClass
    {
        static void mainprogrammethod(string[] args)
        {
            // class instance  
            new test().displayData();
        }
    }


    ////Generics in C#
    //==================
    //Define a class with placeholders for the type of its fields, methods, parameters, etc.Generics 
    //replace these placeholders with some specific type at compile time.A generic class can be defined 
    //using angle brackets<>
    class MyGenericClass<T>
    {
        private T genericMemberVariable;

        public MyGenericClass(T value)
        {
            genericMemberVariable = value;
        }

        public T genericMethod(T genericParameter)
        {
            Console.WriteLine("Parameter type: {0}, value: {1}", typeof(T).ToString(), genericParameter);
            Console.WriteLine("Return type: {0}, value: {1}", typeof(T).ToString(), genericMemberVariable);

            return genericMemberVariable;
        }

        public T genericProperty { get; set; }
    }
    //    Instantiate generic class:

    //MyGenericClass<int> intGenericClass = new MyGenericClass<int>(10);

    //int val = intGenericClass.genericMethod(200);
    class MyGenericClass
    {
        private int genericMemberVariable;

        public MyGenericClass(int value)
        {
            genericMemberVariable = value;
        }

        public int genericMethod(int genericParameter)
        {
            Console.WriteLine("Parameter type: {0}, value: {1}", typeof(int).ToString(), genericParameter);
            Console.WriteLine("Return type: {0}, value: {1}", typeof(int).ToString(), genericMemberVariable);

            return genericMemberVariable;
        }

        public int genericProperty { get; set; }

    }
    //    Example: Generic class

    //    MyGenericClass<string> strGenericClass = new MyGenericClass<string>("Hello Generic World");

    //strGenericClass.genericProperty = "This is a generic property example.";
    //string result = strGenericClass.genericMethod("Generic Parameter");
    //If the generic base class has constraints, the derived class must use the same constraints.


    class MyGenericClasss<T> where T : class
    {
        // Implementation 
    }

    class MyDerivedClasss<U> : MyGenericClasss<U> where U : class
    {
        //implementation
    }

    //Generic Delegate

    class Prrogram
    {
        public delegate T add<T>(T param1, T param2);

        static void mainprogrammethod(string[] args)
        {
            add<int> sum = AddNumber;

            Console.WriteLine(sum(10, 20));

            add<string> conct = Concate;

            Console.WriteLine(conct("Hello", "World!!"));
        }

        public static int AddNumber(int val1, int val2)
        {
            return val1 + val2;
        }

        public static string Concate(string str1, string str2)
        {
            return str1 + str2;
        }
    }

    //C# Delegate:
    class Proogram
    {
        // declare delegate
        public delegate void Print(int value);

        static void mainprogrammethod(string[] args)
        {
            // Print delegate points to PrintNumber
            Print printDel = PrintNumber;

            printDel(100000);
            printDel(200);

            // Print delegate points to PrintMoney
            printDel = PrintMoney;

            printDel(10000);
            printDel(200);
        }

        public static void PrintNumber(int num)
        {
            Console.WriteLine("Number: {0,-12:N0}", num);
        }

        public static void PrintMoney(int money)
        {
            Console.WriteLine("Money: {0:C}", money);
        }
    }
    //Example: Delegate parameter

    class Prograam
    {
        public delegate void Print(int value);


        static void mainprogrammethod(string[] args)
        {
            PrintHelper(PrintNumber, 10000);
            PrintHelper(PrintMoney, 10000);
        }

        public static void PrintHelper(Print delegateFunc, int numToPrint)
        {
            delegateFunc(numToPrint);
        }

        public static void PrintNumber(int num)
        {
            Console.WriteLine("Number: {0,-12:N0}", num);
        }

        public static void PrintMoney(int money)
        {
            Console.WriteLine("Money: {0:C}", money);
        }
    }
    //Multicast delegate
    class MulticastD
    {
        public delegate void Print(int value);

        static void mainprogrammethod(string[] args)
        {
            Print printDel = PrintNumber;
            printDel += PrintHexadecimal;
            printDel += PrintMoney;

            printDel(100000);
        }
        public static void PrintNumber(int num)
        {
            Console.WriteLine("Number: {0,-12:N0}", num);
        }

        public static void PrintMoney(int money)
        {
            Console.WriteLine("Money: {0:C}", money);
        }

        public static void PrintHexadecimal(int dec)
        {
            Console.WriteLine("Hexadecimal: {0:X}", dec);
        }
    }

    //Anonymous method
    class AnonymousD

    {
        public delegate void Print(int value);

        static void mainprogrammethod(string[] args)
        {
            int i = 10;

            Print prnt = delegate (int val)
            {
                val += i;
                Console.WriteLine("Anonymous method: {0}", val);
            };

            prnt(100);
        }
    }
    //Func

    class Progrrram
    {
        static int Sum(int x, int y)
        {
            return x + y;
        }

        static void mainprogrammethod(string[] args)
        {
            Func<int, int, int> add = Sum;

            int result = add(10, 10);

            Console.WriteLine(result);
        }
        //Func with anonymous method

        Func<int> getRandomNumber = delegate ()
        {
            Random rnd = new Random();
            return rnd.Next(1, 100);
        };
        //Func with lambda expression

        Func<int> getRandommNumber = () => new Random().Next(1, 100);

        //Or 

        Func<int, int, int> Summ = (x, y) => x + y;
    }

    //Action
    class ActD
    {
        //Anonymous method with Action delegate

        static void mainprogrammethod(string[] args)
        {
            Action<int> printActionDel = delegate (int i)
            {
                Console.WriteLine(i);
            };

            printActionDel(10);
            //Lambda expression with Action delegate


            Action<int> printActionDDel = i => Console.WriteLine(i);

            printActionDDel(10);
        }


    }

    //https://www.codeproject.com/Articles/703634/SOLID-architecture-principles-using-simple-Csharp

}