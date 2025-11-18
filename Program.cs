namespace Why_.NET_has_wrong_implicit_conversations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            long IamLong = 0;
            ulong IamULong = 0;
            float IamFloat = 0;
            double IamDouble = 0;

            IamFloat = IamLong;    // Implicit conversion works
            IamFloat = IamULong;   // Implicit conversion works

            IamDouble = IamLong;    // Implicit conversion works
            IamDouble = IamULong;   // Implicit conversion works

            long IntegerInFloatMax = 0;

            // Lets try to find maximun integer value that float can hold
            Console.WriteLine("Checking for Float: maximum integer value without loss data:");
            for (long i = 0; i <= long.MaxValue; i++)
            {
                IamFloat = i;
                long tmp = (long)IamFloat;
                if (tmp != i)
                {
                    IntegerInFloatMax = i - 1;
                    Console.WriteLine($"{IntegerInFloatMax:D20}");
                    Console.WriteLine($"{long.MaxValue:D20}");
                    break;
                }
            }

            Console.WriteLine();
            Console.WriteLine("This is how a floating point number behaves when it loses precision:");
            PringSomeStuff(10, IntegerInFloatMax);

            Console.WriteLine();
            Console.WriteLine("This is how a floating point number behaves without loses precision:");
            PringSomeStuff(10, IntegerInFloatMax >> 1);

            // Lets try to find maximun integer value that double can hold
            Console.WriteLine("\nChecking for Double: maximum integer value without loss data:");
            for (long i = long.MaxValue; i >= 0; i--)
            {
                IamDouble = i;
                long tmp = (long)IamDouble;
                if (tmp == i)
                {
                    Console.WriteLine($"{i:D20}");
                    Console.WriteLine($"{long.MaxValue:D20}");
                    break;
                }
            }

            Console.WriteLine(
                "\nEverything seems fine, since the double type hasn't lost its maximum integer value." +
                "\nBut this physically can't happen, because both long and double have exactly 8 bytes of storage." +
                "\nLet's complicate the problem and try using the ulong type.");

            // Lets try to find maximun ULONG value that DOUBLE can hold
            Console.WriteLine("Checking for Double: maximum ULONG value without loss data:");
            for (ulong i = ulong.MaxValue; i >= 0; i--)
            {
                IamDouble = i;
                ulong tmp = (ulong)IamDouble;
                if (tmp == i)
                {
                    Console.WriteLine($"{i:D20}");
                    Console.WriteLine($"{ulong.MaxValue:D20}");
                    break;
                }
            }
            //
            Console.WriteLine(
                $"\nGreat, right? Everything works, but let's represent the double number in scientific notation:");
            PringSomeStuff(10, long.MaxValue - 10);
            Console.WriteLine();
            PringSomeStuffUlong(10, ulong.MaxValue - 10);

            /*Since the loss of precision is visible in exponential notation,
             * we can conclude that .NET uses optimizations in special cases, leading us to believe that no data loss occurs.

            * If this can somehow be explained or justified (though it shouldn't, as it will likely lead to errors in unexpected places),
            * then I consider the implicit conversion of an 8-byte integer to a 4-byte floating-point number
            * to be the height of impudence and a violation of all possible programming conventions.*/

            void PringSomeStuff(long delta, long value)
            {
                for (long i = -delta; i <= delta; i++)
                {
                    Console.WriteLine($"{(value + i):D20}\t\t{(float)(value + i):E25}");
                }
            }

            void PringSomeStuffUlong(ulong delta, ulong value)
            {
                for (ulong i = 0; i <= delta; i++)
                {
                    Console.WriteLine($"{(value + i):D20}\t\t{(float)(value + i):E25}");
                }
            }
        }
    }
}
