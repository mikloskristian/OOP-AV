using System.Globalization;

namespace AV5_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] items = new int[] { 1, 2, 3 };
            Console.WriteLine(string.Join(",", items));
            ArrayUtilities.Reverse<int>(items); // kod metoda moze i bez <>, prepozna i izgenerira metodu za int, parametarski polimorfizam
            Console.WriteLine(string.Join(",", items));
            int min = ArrayUtilities.FindMin(items);
            Console.WriteLine(min);
            try
            {
                Stack<int> numbers = new Stack<int>(3);
                numbers.Push(1);
                numbers.Push(2);
                numbers.Push(3);
                //stack1.Push(4); // stack overflow :(
                Console.WriteLine(numbers.Pop());
                Console.WriteLine(numbers.Pop());
                Console.WriteLine(numbers.Pop());
                //Console.WriteLine(stack1.Pop());

                Stack<string> names = new Stack<string>(3);
                names.Push("Matej");
                names.Push("Luka");
                names.Push("Marko");
            }
            catch (StackUnderflowException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (StackOverflowException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }

    class Stack<T> // <T> se odnosi na generic, odnosno opceniti tip podatka, tako da umjesto da radim IntStack, pa FloatStack, pa DoubleStack... imam jedan koji je generic i samo pri pozivanju u <> napisem tip podatka
    {
        int sp;
        T[] items;

        public Stack(int size)
        {
            this.items = new T[size];
            this.sp = -1; // prazan stog
        }

        public void Push(T item)
        {
            if(this.IsFull())
            {
                throw new StackOverflowException("Illegal action, cannot push to a full stack.");
            }
            sp++;
            this.items[sp] = item; // Tu moze i sp++
        }

        public T Pop()
        {
            if(this.IsEmpty())
            {

                throw new StackUnderflowException("Illegal action, cannot push an empty stack");

            }
            return this.items[sp--]; // Prvo izvrsi pa onda smanji, ako --sp onda prvo smanji pa izvrsi
        }

        private bool IsFull() => sp == items.Length - 1; // One-liner, kao u filmovima onako
        private bool IsEmpty() => sp == -1;
    }
    public class StackUnderflowException : Exception
    {
        public StackUnderflowException() { }

        public StackUnderflowException(string? message) : base(message) { }
    }
    class StackOverflowException : Exception // Exception ima 4 konstruktora koja mora imat, na ispitu treba 2
    {
        public StackOverflowException() : base() { }
        public StackOverflowException(string message) : base(message) { }
    }

    class ArrayUtilities
    {
        public static void Reverse<T>(T[] items)
        {
            for(int i = 0; i < items.Length/2; i++) 
            {
                int swapIndex = items.Length - 1 - i;
                T temp = items[i];
                items[i] = items[swapIndex];
                items[swapIndex] = temp;
            }
        }

        public static T FindMin<T>(T[] items) where T : IComparable<T>
        {
            int minIndex = 0;
            for(int i = 0; i < items.Length; i++)
            {
                //if (items[i] < items[minIndex]) 
                //    minIndex = i;
                if (items[i].CompareTo(items[minIndex]) < 0) 
                    minIndex = i;
            }
            return items[minIndex];
        }
    }
}
