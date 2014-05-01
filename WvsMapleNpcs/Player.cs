using System;
namespace WvsGame.Maple.Scripting
{
    public class Player
    {
        private ConsoleColor _alertColor = ConsoleColor.DarkYellow;

        public static Player Default = new Player("Bob", 100, 412)
        {
            Strength = 1628,
            Intelligence = 1254,
            Dexterity = 25654,
            Luck = 15348,
        };

        private string _name;
        private int _level;
        private int _job;
        private int _meso;
        private int _field;
        private int _str;
        private int _int;
        private int _dex;
        private int _luk;
        private Inventory _inventory;
        private QuestLog _questLog;

        public Player(string name, int level, int job)
        {
            _name = name;
            _level = level;
            _job = job;
            _inventory = new Inventory(this, 20);
            _questLog = new QuestLog();
        }

        /// <summary>
        /// The nam eof the player.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// The level of the player.
        /// </summary>
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        /// <summary>
        /// The job of the player represented using a number.
        /// </summary>
        public int Job
        {
            get { return _job; }
            set { _job = value; }
        }

        /// <summary>
        /// The amount of maple currency the player has.
        /// </summary>
        public int Meso
        {
            get { return _meso; }
            set { _meso = value; }
        }

        /// <summary>
        /// The maplestory field (map) id.
        /// </summary>
        public int Field
        {
            get { return _field; }
            set { _field = value; }
        }

        /// <summary>
        /// The strength of the player.
        /// </summary>
        public int Strength
        {
            get { return _str; }
            set { _str = value; }
        }

        /// <summary>
        /// The intelligence of the player.
        /// </summary>
        public int Intelligence
        {
            get { return _int; }
            set { _int = value; }
        }

        /// <summary>
        /// The dexterity of the player.
        /// </summary>
        public int Dexterity
        {
            get { return _dex; }
            set { _dex = value; }
        }

        /// <summary>
        /// The luck of the player.
        /// </summary>
        public int Luck
        {
            get { return _luk; }
            set { _luk = value; }
        }

        /// <summary>
        /// Access to the player's inventory.
        /// </summary>
        public Inventory Inventory
        {
            get { return _inventory; }
        }

        /// <summary>
        /// Access to the player's quest log.
        /// </summary>
        public QuestLog QuestLog
        {
            get { return _questLog; }
        }

        /// <summary>
        /// Sends a message to the player in various ways.
        /// 0 - Notice
        /// 1 - Popup
        /// 2 - Megaphone
        /// 3 - Super Megaphone
        /// 4 - Header
        /// 5 - Blue
        /// 6 - Pink
        /// </summary>
        public void Notify(string text, byte type = 5)
        {
            var origFore = Console.ForegroundColor;
            var origBack = Console.BackgroundColor;
            switch (type)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 1:
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    break;
                case 2:
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    break;
                case 3:
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case 6:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;

            }
            Console.WriteLine(text);
            Console.ForegroundColor = origFore;
            Console.BackgroundColor = origBack;
        }

    }
}
