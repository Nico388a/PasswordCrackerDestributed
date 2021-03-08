using System;

namespace PasswordCrackerDestributed
{
    class Program
    {
        static void Main(string[] args)
        {
            Master masetr = new Master();
            masetr.Listener(6789);
            Console.ReadKey();
        }
    }
}
