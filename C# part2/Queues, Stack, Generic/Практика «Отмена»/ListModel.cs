using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;

namespace TodoApplication
{
    public class ListModel<TItem>
    {
        public List<TItem> Items { get; }
        public int Limit;
        LimitedSizeStack<ICommand<TItem>> stack;

        public ListModel(int limit)
        {
            Items = new List<TItem>();
            Limit = limit;
            stack = new LimitedSizeStack<ICommand<TItem>>(Limit);
        }

        public void AddItem(TItem item)
        {
            Items.Add(item);
            stack.Push(new CancelAddItemm<TItem>(Items.Count - 1, Items));
        }

        public void RemoveItem(int index)
        {
            stack.Push(new CancelRemoveItemm<TItem>(index, Items));
            Items.RemoveAt(index);
        }

        public bool CanUndo()
        {
            return stack.Count > 0;
        }

        public void Undo()
        {
            stack.Pop().Execute();
        }
    }

    public interface ICommand<T>
    {
        int Index { get; }
        void Execute();
    }

    class CancelRemoveItemm<T> : ICommand<T>
    {
        public int Index { get; private set; }
        public T Value { get; private set; }
        public List<T> List;

        public CancelRemoveItemm(int index, List<T> list)
        {
            Index = index;
            Value = list[index];
            List = list;
        }

        public void Execute()
        {
            List.Insert(Index, Value);
        }
    }

    class CancelAddItemm<T> : ICommand<T>
    {
        public int Index { get; private set; }
        public List<T> List;

        public CancelAddItemm(int index, List<T> list)
        {
            Index = index;
            List = list;
        }

        public void Execute()
        {
            List.RemoveAt(Index);
        }
    }
}
