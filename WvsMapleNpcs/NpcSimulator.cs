using System;
using System.Collections.Generic;
namespace WvsGame.Maple.Scripting
{
    public class NpcSimulator
    {
        private int _selectedPlayer;
        private List<Player> _players;
        private Dictionary<int, NpcScript> _npcs;
        private int _maxId;
        private ProgramState _state;

        private Player SelectedPlayer
        {
            get
            {
                if (_players == null)
                    return Player.Default;
                return _players[_selectedPlayer];
            }
        }

        public void Run()
        {
            _state = ProgramState.Root;

            if (_npcs == null || _npcs.Count == 0)
            {
                Console.WriteLine("No Npc's were loaded.");
                return;
            }

            bool exit = false;

            while (!exit)
            {
                bool stayInMenu = false;
                do
                {
                    stayInMenu = false;

                    byte selection = ShowMenu(
                        "List Npcs",
                        "Execute Npc",
                        "List Players",
                        "Modify Player",
                        "Create Player",
                        "Exit");
                    if (selection <= 0 || selection > 6)
                    {
                        Console.WriteLine("Incorrect value.");
                        stayInMenu = true;
                    }
                    else
                    {
                        switch (selection)
                        {
                            case 1:
                                ListNpcs();
                                break;
                            case 2:
                                ShowNpcSelect();
                                break;
                            case 3:
                                ListPlayers();
                                break;
                            case 4:
                                ShowModifyPlayers();
                                break;
                            case 5:
                                ShowCreatePlayer();
                                break;
                            case 6:
                                exit = true;
                                break;
                        }
                    }
                    Console.WriteLine();
                } while (stayInMenu);

            }
        }

        public void AddNpc(int id, NpcScript script)
        {
            if (_npcs == null)
                _npcs = new Dictionary<int, NpcScript>();

            _npcs.Add(id, script);
            if (id > _maxId)
                _maxId = id;
        }

        public void AddPlayer(Player player)
        {
            if (_players == null)
                _players = new List<Player>();
            _players.Add(player);
        }

        public void SelectPlayer(int index)
        {
            if (_players == null)
                return;
            if (index >= 0 && index < _players.Count)
            {
                _selectedPlayer = index;
            }
        }

        #region Npc

        protected void ListNpcs()
        {
            Console.WriteLine("Npcs");
            int width = _maxId.ToString().Length + 3;
            foreach (int id in _npcs.Keys)
            {
                Console.WriteLine(Width(id.ToString(), width) + "(" + _npcs[id].GetType() + ")");
            }
        }

        protected void ShowNpcSelect()
        {
            bool exit = false;
            while (!exit)
            {
                Player player = Player.Default;

                if (_players == null || _players.Count == 0)
                {
                    Console.WriteLine("Since no players have been created, a default will be used.");
                }
                else
                {
                    ListPlayers();
                    Console.WriteLine();
                    bool playerChoiceExit = false;
                    while (!playerChoiceExit)
                    {
                        Console.WriteLine("Type 'default' to use a default player.");
                        int playerSelection = GetIntInput("Choose a player> ", "default");
                        if (playerSelection == -1)
                        {
                            Console.WriteLine("Since no players have been choosen, a default will be used.");
                            playerChoiceExit = true;
                        }
                        else if (playerSelection > 0 && playerSelection <= _players.Count)
                        {
                            player = _players[playerSelection - 1];
                            playerChoiceExit = true;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect choice.");
                        }
                        Console.WriteLine();
                    }
                }

                Console.WriteLine("Type 'exit' to return to the menu.");
                int npcSelection = GetIntInput("Enter an npc id> ", "exit");
                if(npcSelection == -1)
                {
                    exit = true;
                }
                else if (_npcs.ContainsKey(npcSelection))
                {
                    Console.WriteLine();
                    ExecuteNpc(npcSelection);
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Npc with ID of " + npcSelection + " could not be found.");
                }
            }
        }

        protected void ExecuteNpc(int ID)
        {
            if (!_npcs.ContainsKey(ID))
                return;
            var npc = _npcs[ID];
            try
            {
                npc.Talker = SelectedPlayer;
                AsyncHelper.RunSync(npc.Run);
                Console.WriteLine("Npc executed successfully.");
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is NpcExitException)
                {
                    Console.WriteLine("Npc exited on command.");
                }
                else
                {
                    Console.WriteLine("An error occured while executing an npc.");
                    Console.WriteLine(ex.InnerException.GetType() + ": " + ex.InnerException.Message + "\n" + ex.InnerException.StackTrace);
                }
            }
        }

        #endregion // Npc

        #region Player

        private void ListPlayers()
        {
            if (_players == null || _players.Count == 0)
            {
                Console.WriteLine("There are no players.");
                return;
            }
            Console.WriteLine("Players");
            for (int i = 0; i < _players.Count; i++)
            {
                Player p = _players[i];
                Console.WriteLine("{0}) {1} [Lvl {2} | Job {3}]", i + 1, p.Name, p.Level, p.Job);
            }
        }

        protected void ShowModifyPlayers()
        {
            bool exit = false;
            while (!exit)
            {
                ListPlayers();
                if (_players == null || _players.Count == 0)
                    return;
                Console.WriteLine();
                Console.WriteLine("Type 'exit' to return to the menu.");
                int selection = GetIntInput("Choose> ", "exit");
                if (selection > 0 && selection <= _players.Count)
                {
                    Player player = _players[selection - 1];
                    ShowModifyPlayer(player);
                    exit = true;
                }
                else if (selection == -1)
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Incorrect choice.");
                }
            }
        }

        protected void ShowModifyPlayer(Player player)
        {

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Modify Player");
                byte selection = ShowMenu(
                    Width("Name: ", 15) + player.Name,
                    Width("Level: ", 15) + player.Level,
                    Width("Job: ", 15) + player.Job,
                    Width("Strength: ", 15) + player.Strength,
                    Width("Intelligence: ", 15) + player.Intelligence,
                    Width("Dexterity: ", 15) + player.Dexterity,
                    Width("Luck: ", 15) + player.Luck,
                    Width("Inventory:", 15) + (player.Inventory.IsEmpty() ? "[Empty]" : player.Inventory.TotalStacks() + " items"),
                    Width("Quests", 15) + player.QuestLog.StartedQuests.Length + " started, " + player.QuestLog.CompletedQuests.Length + " completed",
                    "Return to Menu");
                switch (selection)
                {
                    case 1:
                        player.Name = GetStringInput("Enter a new name> ", "exit");
                        break;
                    case 2:
                        player.Level = GetIntInput("Enter a new level> ", "exit");
                        break;
                    case 3:
                        player.Job = GetIntInput("Enter a new job> ", "exit");
                        break;
                    case 4:
                        player.Strength = GetIntInput("Enter a new strength> ", "exit");
                        break;
                    case 5:
                        player.Intelligence = GetIntInput("Enter a new intelligence> ", "exit");
                        break;
                    case 6:
                        player.Dexterity = GetIntInput("Enter a new dexterity> ", "exit");
                        break;
                    case 7:
                        player.Luck = GetIntInput("Enter a new luck> ", "exit");
                        break;
                    case 8:
                        ShowInventory(player.Inventory);
                        break;
                    case 9:
                        ShowQuestMenu(player.QuestLog);
                        break;
                    case 10:
                        exit = true;
                        break;
                }
            }
        }

        protected void ShowCreatePlayer()
        {
            Player player = new Player("Bob", 1, 0);
            player.Name = GetStringInput("Enter a new name> ", "exit");
            if (player.Name == null)
                return;
            player.Level = GetIntInput("Enter a new level> ", "exit");
            if (player.Level < 0)
                return;
            player.Job = GetIntInput("Enter a new job> ", "exit");
            if (player.Job < 0)
                return;
            player.Strength = GetIntInput("Enter a new strength> ", "exit");
            if (player.Strength < 0)
                return;
            player.Intelligence = GetIntInput("Enter a new intelligence> ", "exit");
            if (player.Intelligence < 0)
                return;
            player.Dexterity = GetIntInput("Enter a new dexterity> ", "exit");
            if (player.Dexterity < 0)
                return;
            player.Luck = GetIntInput("Enter a new luck> ", "exit");
            if (player.Luck < 0)
                return;
            int itemId = 0;
            Console.WriteLine();
            Console.WriteLine("Type 'done' when you are finished with the inventory.");
            while (itemId != -1)
            {
                itemId = GetIntInput("Enter an item id> ", "done");
                if (itemId > 0)
                {
                    bool getAmount = true;
                    while (getAmount)
                    {
                        int amount = GetIntInput("Enter an amount for (" + itemId + ")> ", "done");
                        if (amount > 0)
                        {
                            player.Inventory.GainItem(itemId, amount);
                            Console.WriteLine("{0} of item {1} has been added.", amount, itemId);

                            getAmount = false;
                        }
                        else if (amount == -1)
                        {
                            player.Inventory.GainItem(itemId, 1);
                            Console.WriteLine("1 of item {0} has been added.", itemId);

                            getAmount = false;
                            itemId = -1;
                        }
                        else
                        {
                            Console.WriteLine("That is an incorrect amount.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("That is an incorrect item id.");
                }
            }
            if (_players == null)
                _players = new List<Player>();
            _players.Add(player);
            Console.WriteLine("Player {0} [Lvl {1} | Job {2}] has been created.", player.Name, player.Level, player.Job);
        }

        #region Inventory

        protected void ShowInventory(Inventory inventory)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Modify Inventory");
                int selection = ShowMenu("List Items", 
                        "Add Items", 
                        "Remove Items", 
                        "Clear Items", 
                        "Return to Modify Player");
                switch (selection)
                {
                    case 1:
                        ListInventoryItems(inventory);
                        break;
                    case 2:
                        ShowAddToInventory(inventory);
                        break;
                    case 3:
                        ShowRemoveFromInventory(inventory);
                        break;
                    case 4:
                        ShowClearFromInventory(inventory);
                        break;
                    case 5:
                        exit = true;
                        break;
                }
                Console.WriteLine();
            }
        }

        protected void ListInventoryItems(Inventory inventory)
        {
            Console.WriteLine("Inventory Items");

            int columns = 4;
            int colWidth = 19;

            for (int row = 0; row < inventory.Items.Length / columns; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    int index = (row * columns) + column;
                    Item item = inventory.Items[index];
                    Console.Write(Width(string.Format("{0})", index + 1), 4));
                    if (item != null)
                    {
                        Console.Write(Width(string.Format("{0} x {1}", item.ID, item.Amount), colWidth - 4));
                    }
                    else
                    {
                        Console.Write(Width("[Empty]", colWidth - 4));
                    }
                }
                Console.WriteLine();
            }
        }

        protected void ShowAddToInventory(Inventory inventory)
        {
            int itemId = 0;
            while (itemId != -1)
            {
                ListInventoryItems(inventory);

                Console.WriteLine("Type 'done' when you are finished adding.");
                itemId = GetIntInput("Enter an item id> ", "done");
                if (itemId > 0)
                {
                    bool getAmount = true;
                    while (getAmount)
                    {
                        int amount = GetIntInput("Enter an amount for (" + itemId + ")> ", "done");
                        if (amount > 0)
                        {
                            if(inventory.GainItem(itemId, amount))
                                Console.WriteLine("{0} of item {1} has been added.", amount, itemId);
                            else
                                Console.WriteLine("Could not add {0} of item {1}.", amount, itemId);
                            getAmount = false;
                        }
                        else if (amount == -1)
                        {
                            if (inventory.GainItem(itemId, 1))
                                Console.WriteLine("1 of item {0} has been added.", itemId);
                            else
                                Console.WriteLine("Could not add 1 of item {0}.", itemId);

                            getAmount = false;
                            itemId = -1;
                        }
                        else
                        {
                            Console.WriteLine("That is an incorrect amount.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("That is an incorrect item id.");
                }
            }
        }

        protected void ShowRemoveFromInventory(Inventory inventory)
        {
            bool exit = false;
            while (!exit)
            {
                if (inventory.IsEmpty())
                {
                    Console.WriteLine("This inventory is empty.");
                    return;
                }

                ListInventoryItems(inventory);

                Console.WriteLine("Type 'done' when you are finished removing.");
                int itemId = GetIntInput("Enter item id> ", "done");
                if (itemId > 0)
                {
                    if (inventory.HasItem(itemId))
                    {
                        Console.WriteLine("Type 'all' to remove all.");
                        int amount = GetIntInput("Enter the amount to remove> ", "all");
                        if (amount > 0)
                        {
                            if (inventory.GainItem(itemId, -amount))
                                Console.WriteLine("{0} of item {1} has been removed.", amount, itemId);
                            else
                                Console.WriteLine("Could not remove {0} of item {1}.", amount, itemId);
                        }
                        else if (amount == -1)
                        {
                            amount = inventory.Count(itemId);
                            if (inventory.GainItem(itemId, -amount))
                                Console.WriteLine("All {0} of item {1} has been removed.", amount, itemId);
                            else
                                Console.WriteLine("Could not remove all of item {0}.", itemId);
                        }
                        else
                        {
                            Console.WriteLine("That is an invalid amount.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("This inventory does not contain any {0}.", itemId);
                    }
                }
                else if (itemId == -1)
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Incorrect item id.");
                }
                Console.WriteLine();
            }
        }

        protected void ShowClearFromInventory(Inventory inventory)
        {
            bool exit = false;
            while (!exit)
            {
                if (inventory.IsEmpty())
                {
                    Console.WriteLine("This inventory is empty.");
                    return;
                }
                ListInventoryItems(inventory);

                Console.WriteLine("Type 'done' when you are finished clearing.");
                int itemId = GetIntInput("Enter an item id> ", "done");
                if (itemId > 0)
                {
                    if (inventory.HasItem(itemId))
                    {
                        int count = inventory.Count(itemId);
                        inventory.ClearItem(itemId);
                        Console.WriteLine("All {0} of item {1} have been cleared.", count, itemId);
                    }
                    else
                    {
                        Console.WriteLine("This inventory does not contain any {0}.", itemId);
                    }
                }
                else if (itemId == -1)
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Incorrect item id.");
                }
                Console.WriteLine();
            }
        }

        #endregion // Inventory

        #region Quest

        public void ShowQuestMenu(QuestLog questLog)
        {
            bool exit = false;
            while (!exit)
            {
                byte selection = ShowMenu("Show Quests",
                                "Start a Quest",
                                "Abandon a Quest",
                                "Complete a Quest",
                                "Return to Player Menu");
                switch (selection)
                {
                    case 1:
                        ListQuests(questLog);
                        Console.WriteLine();
                        break;
                    case 2:
                        ShowStartQuest(questLog);
                        Console.WriteLine();
                        break;
                    case 3:
                        ShowAbandonQuest(questLog);
                        Console.WriteLine();
                        break;
                    case 4:
                        ShowCompleteQuest(questLog);
                        Console.WriteLine();
                        break;
                    case 5:
                        exit = true;
                        break;
                }
            }
        }

        public void ListQuests(QuestLog questLog)
        {
            int[] startedQuests = questLog.StartedQuests;
            int[] completedQuests = questLog.CompletedQuests;
            Console.WriteLine("Quest Log");
            if (startedQuests.Length > 0)
            {
                Console.WriteLine("In-Progress");
                Console.WriteLine(string.Join(", ", startedQuests));
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No quests are in progress.");
            }
            if (completedQuests.Length > 0)
            {
                Console.WriteLine("Completed");
                Console.WriteLine(string.Join(", ", completedQuests));
            }
            else
            {
                Console.WriteLine("No quests have been completed.");
            }
        }

        public void ShowStartQuest(QuestLog questLog)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Start a Quest");
                Console.WriteLine("Type 'done' when finished starting quests.");
                int questId = GetIntInput("Enter a questId> ", "done");
                if (questId > 0)
                {
                    if (!questLog.HasStarted(questId))
                    {
                        if (!questLog.HasCompleted(questId))
                        {
                            questLog.StartQuest(questId);
                            Console.WriteLine("Quest {0} has been started.", questId);
                        }
                        else
                        {
                            Console.WriteLine("Quest has already been completed.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Quest has already been started.");
                    }
                }
                else if (questId == -1)
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Invalid quest Id.");
                }
                Console.WriteLine();
            }
        }

        public void ShowAbandonQuest(QuestLog questLog)
        {
            bool exit = false;
            while (!exit)
            {
                if (questLog.StartedQuests.Length == 0)
                {
                    Console.WriteLine("No quests have been started.");
                    Console.WriteLine();
                    return;
                }

                ListQuests(questLog);
                Console.WriteLine();

                Console.WriteLine("Adbandon a Quest");
                Console.WriteLine("Type 'done' when finished abandoning quests.");
                int questId = GetIntInput("Enter a questId> ", "done");
                if (questId > 0)
                {
                    if (questLog.HasStarted(questId))
                    {
                        questLog.AbandonQuest(questId);
                        Console.WriteLine("Quest {0} has been abandoned.", questId);
                    }
                    else
                    {
                        Console.WriteLine("Quest has not been started.");
                    }
                }
                else if (questId == -1)
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Invalid quest Id.");
                }
            }
            Console.WriteLine();
        }

        public void ShowCompleteQuest(QuestLog questLog)
        {
            if (questLog.StartedQuests.Length == 0)
            {
                Console.WriteLine("No quests have been started.");
                Console.WriteLine();
                return;
            }

            bool exit = false;
            while (!exit)
            {
                ListQuests(questLog);
                Console.WriteLine();

                Console.WriteLine("Complete a Quest");
                Console.WriteLine("Type 'done' when finished completing quests.");
                int questId = GetIntInput("Enter a questId> ", "done");
                if (questId > 0)
                {
                    if (!questLog.HasCompleted(questId))
                    {
                        if (questLog.HasStarted(questId))
                        {
                            questLog.CompleteQuest(questId);
                            Console.WriteLine("Quest {0} has been completed.", questId);
                        }
                        else
                        {
                            Console.WriteLine("Quest has not been started.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Quest has already been completed.");
                    }
                }
                else if (questId == -1)
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Invalid quest Id.");
                }
            }
            Console.WriteLine();
        }

        #endregion // Quest

        #endregion // Player

        #region Helper Methods

        protected string GetStringInput(string message, string cancel)
        {
            string input = null;
            bool exit = false;
            while (!exit)
            {
                Console.Write(message);
                string raw = Console.ReadLine();
                if (raw.ToLower() == cancel.ToLower())
                {
                    exit = true;
                }
                else if (!string.IsNullOrWhiteSpace(raw))
                {
                    input = raw;
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Please enter a value.");
                }
            }
            return input;
        }

        protected int GetIntInput(string message, string cancel)
        {
            int input = -1;
            bool exit = false;
            while (!exit)
            {
                Console.Write(message);
                string raw = Console.ReadLine();
                if (raw.ToLower() == cancel.ToLower())
                {
                    exit = true;
                }
                else if (int.TryParse(raw, out input))
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Incorrect value.");
                }
            }
            return input;
        }

        protected byte ShowMenu(params string[] options)
        {
            byte selection = 0;
            if(options== null || options.Length == 0)
                return 0;
            bool exit = false;
            while (!exit)
            {
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine("{0}) {1}", i + 1, options[i]);
                }
                Console.Write("Choose> ");
                if (byte.TryParse(Console.ReadLine(), out selection))
                {
                    if (selection > 0 && selection <= options.Length)
                    {
                        exit = true;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect value.");
                    }
                }
                Console.WriteLine();
            }
            return selection;
        }

        private string Width(string text, int width)
        {
            while (text.Length < width)
            {
                text += " ";
            }
            return text;
        }

        #endregion // Helper Methods
    }
}
