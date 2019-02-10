using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApplication
{
    public class StackItem<T>
    {
        public StackItem<T> Next;
        public StackItem<T> Previous;
        public T value { get; private set; }

        public StackItem(StackItem<T> next, StackItem<T> previous, T value)
        {
            Next = next;
            Previous = previous;
            this.value = value;
        }
    }

    public class LimitedSizeStack<T>
    {
        StackItem<T> Head;
        StackItem<T> Tail;
        public int Limit { get; private set; }
        public int Counter { get; private set; }

        public LimitedSizeStack(int limit)
        {
            Limit = limit;
        }

        public void Push(T item)
        {
            if (Head == null)
                Head = Tail = new StackItem<T>(null, null, item);
            else
            {
                Tail.Next = new StackItem<T>(null, Tail, item);
                Tail = Tail.Next;
                if (Counter == Limit)
                    DeleteFirstElement();
            }
            Counter++;
        }

        public void DeleteFirstElement()
        {
            if (Head.Next != null)
            {
                Head = Head.Next;
                Head.Previous = null;
            }
            else
                Head = Tail = null;
            Counter--;
        }

        public T Pop()
        {
            Counter--;
            T pop = Tail.value;
            if (Tail.Previous != null)
                Tail = Tail.Previous;
            else
                Head = Tail = null;
            return pop;
        }

        public int Count { get { return Counter; } }
    }
}
