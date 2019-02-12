using System;
using System.Collections.Generic;
using System.Linq;

namespace Clones
{
	public class CloneVersionSystem : ICloneVersionSystem
	{
        private List<TheCLone<string>> clones;

        public CloneVersionSystem()
        {
            clones = new List<TheCLone<string>>() { new TheCLone<string>() };
        }

		public string Execute(string query)
		{
            var tmp = query.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var command = tmp[0];
            var num = int.Parse(tmp[1]) - 1;
            var programId = tmp.Length < 3 ? null : tmp[2];

            if (command == "learn")
                return clones[num].LearnClone(programId);
            else if (command == "rollback")
                return clones[num].RallBack();
            else if (command == "relearn")
                return clones[num].ReLearn();
            else if (command == "clone")
                return clones[num].Clone(clones);
            else
                return clones[num].GetProgrammId() != null ? clones[num].GetProgrammId() : "basic";
		}
	}

    class TheCLone <TValue>
    {
        public TValue ProgramId { get; set; }
        public MyStack<TValue> HistoryOfChanges { get; private set; }
        public MyStack<TValue> ReLearnHistory { get; private set; }

        public TheCLone()
        {
            HistoryOfChanges = new MyStack<TValue>(null);
            ReLearnHistory = new MyStack<TValue>(null);
        }

        public TValue LearnClone(TValue programId)
        {
            if (ProgramId != null)
                HistoryOfChanges.Push(ProgramId);
            ReLearnHistory = new MyStack<TValue>(null);
            ProgramId = programId;
            return default(TValue);
        }

        public TValue RallBack()
        {
            ReLearnHistory.Push(ProgramId);
            ProgramId = HistoryOfChanges.Pop();
            return default(TValue);
        }

        public TValue ReLearn()
        {
            if (ProgramId != null)
                HistoryOfChanges.Push(ProgramId);
            ProgramId = ReLearnHistory.Pop();
            return default(TValue);
        }

        public TValue Clone(List<TheCLone<TValue>> clones)
        {
            clones.Add(new TheCLone<TValue>()
            {
                ProgramId = this.ProgramId,
                HistoryOfChanges = new MyStack<TValue>(this.HistoryOfChanges.Head),
                ReLearnHistory = new MyStack<TValue>(this.ReLearnHistory.Head)
            });
            return default(TValue);
        }

        public TValue GetProgrammId()
        {
            return ProgramId;
        }
    }

    public class MyStack <TValue>
    {
        public MyStackItem<TValue> Head { get; private set; }

        public MyStack(MyStackItem<TValue> head)
        {
            Head = head;
        }

        public void Push(TValue value)
        {
            if (Head == null)
                Head = new MyStackItem<TValue>(value, null);
            else
                Head = new MyStackItem<TValue>(value, Head);
        }

        public TValue Pop()
        {
            if (Head == null)
                return default(TValue);
            var tmp = Head.Value;
            Head = Head.Next;
            return tmp;
        }
    }

    public class MyStackItem <TValue>
    {
        public TValue Value { get; private set; }
        public MyStackItem<TValue> Next { get; private set; }

        public MyStackItem(TValue value, MyStackItem<TValue> next)
        {
            this.Value = value;
            Next = next;
        }
    }
}