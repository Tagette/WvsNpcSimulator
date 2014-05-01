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

        /// <summary>
        /// Gets an integer array copy of the started quests.
        /// </summary>
        public int[] StartedQuests
        {
            get { return _startedQuests.ToArray(); }
        }

        /// <summary>
        /// Gets an integer array copy of the completed quests.
        /// </summary>
        public int[] CompletedQuests
        {
            get { return _completedQuests.ToArray(); }
        }

        /// <summary>
        /// Determines whether or not a quest has been started.
        /// </summary>
        public bool HasStarted(int ID)
        {
            return _startedQuests.Contains(ID);
        }

        /// <summary>
        /// Determines whether or not a quest has been completed.
        /// </summary>
        public bool HasCompleted(int ID)
        {
            return _completedQuests.Contains(ID);
        }

        /// <summary>
        /// Starts a quest for the player.
        /// </summary>
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

        /// <summary>
        /// Abandons a quest for a player.
        /// </summary>
        public bool AbandonQuest(int ID)
        {
            bool existed = false;
            if (HasStarted(ID) && !HasCompleted(ID))
            {
                _startedQuests.Remove(ID);
            }
            return existed;
        }

        /// <summary>
        /// Completes a quest for a player.
        /// </summary>
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
