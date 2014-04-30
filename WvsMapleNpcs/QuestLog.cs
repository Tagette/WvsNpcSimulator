using System.Collections.Generic;
namespace WvsGame.Maple.Scripting
{
    public class QuestLog
    {

        private List<int> _startedQuests;
        private List<int> _completedQuests;

        public QuestLog()
        {
            _startedQuests = new List<int>();
            _completedQuests = new List<int>();
        }

        public int[] StartedQuests
        {
            get { return _startedQuests.ToArray(); }
        }

        public int[] CompletedQuests
        {
            get { return _completedQuests.ToArray(); }
        }

        public bool HasStarted(int ID)
        {
            return _startedQuests.Contains(ID);
        }

        public bool HasCompleted(int ID)
        {
            return _completedQuests.Contains(ID);
        }

        public bool StartQuest(int ID)
        {
            bool started = false;
            if (!_startedQuests.Contains(ID))
            {
                _startedQuests.Add(ID);
                started = true;
            }
            return started;
        }

        public bool AbandonQuest(int ID)
        {
            bool existed = false;
            if (HasStarted(ID) && !HasCompleted(ID))
            {
                _startedQuests.Remove(ID);
            }
            return existed;
        }

        public bool CompleteQuest(int ID)
        {
            bool completed = false;
            if (HasStarted(ID) && !HasCompleted(ID))
            {
                _startedQuests.Remove(ID);
                _completedQuests.Add(ID);
                completed = true;
            }
            return completed;
        }

    }
}
