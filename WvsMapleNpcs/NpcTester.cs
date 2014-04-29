using System;
using System.Collections.Generic;
namespace WvsGame.Maple.Scripting
{
    public class NpcTester
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

        public void ListNpcs()
        {
            int width = _maxId.ToString().Length + 3;
            foreach (int id in _npcs.Keys)
            {
                Console.WriteLine(Width(id.ToString(), width) + "(" + _npcs[id].GetType() + ")");
            }
        }

        public void ShowNpcSelect()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Type 'exit' to return to the menu.");

                Player player = Player.Default;

                if (_players == null || _players.Count == 0)
                {
                    Console.WriteLine("Since no players have been created, a default will be used.");
                }
                else
                {
                    ListPlayers();
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
                    }
                }

                int npcSelection = GetIntInput("Enter an npc id> ", "exit");
                if(npcSelection == -1)
                {
                    exit = true;
                }
                else if (_npcs.ContainsKey(npcSelection))
                {
                    ExecuteNpc(npcSelection);
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Npc with ID of " + npcSelection + " could not be found.");
                }
            }
        }

        public void ExecuteNpc(int ID)
        {
            if (!_npcs.ContainsKey(ID))
                return;
            var npc = _npcs[ID];
            try
            {
                npc.Player = SelectedPlayer;
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

        public void ListPlayers()
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

        public void ShowModifyPlayers()
        {
            bool exit = false;
            while (!exit)
            {
                ListPlayers();
                if (_players == null || _players.Count == 0)
                    return;
                Console.Write("Choose> ");
                int selection = 0;
                if (int.TryParse(Console.ReadLine(), out selection))
                {
                    if (selection > 0 && selection <= _players.Count)
                    {
                        Player player = _players[selection - 1];
                        ShowModifyPlayer(player);
                        exit = true;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect choice.");
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect value.");
                }
            }
        }

        public void ShowModifyPlayer(Player player)
        {

            bool exit = false;
            while (!exit)
            {
                byte selection = ShowMenu(
                    Width("Name: ", 15) + player.Name,
                    Width("Level: ", 15) + player.Level,
                    Width("Job: ", 15) + player.Job,
                    Width("Strength: ", 15) + player.Strength,
                    Width("Intelligence: ", 15) + player.Intelligence,
                    Width("Dexterity: ", 15) + player.Dexterity,
                    Width("Luck: ", 15) + player.Luck,
                    "Inventory",
                    "Quests",
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
                        Console.WriteLine("Not yet implemented.");
                        break;
                    case 9:
                        Console.WriteLine("Not yet implemented.");
                        break;
                    case 10:
                        exit = true;
                        break;
                }
            }
        }

        public void ShowCreatePlayer()
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
            if (_players == null)
                _players = new List<Player>();
            _players.Add(player);
            Console.WriteLine("Player {0} [Lvl {1} | Job {2}] has been created.", player.Name, player.Level, player.Job);
        }

        #region Helper Methods

        private string GetStringInput(string message, string cancel)
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

        private int GetIntInput(string message, string cancel)
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
                Console.WriteLine();
            }
            return input;
        }

        private byte ShowMenu(params string[] options)
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
