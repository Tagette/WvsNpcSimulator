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

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public int Job
        {
            get { return _job; }
            set { _job = value; }
        }

        public int Meso
        {
            get { return _meso; }
            set { _meso = value; }
        }

        public int Field
        {
            get { return _field; }
            set { _field = value; }
        }

        public int Strength
        {
            get { return _str; }
            set { _str = value; }
        }

        public int Intelligence
        {
            get { return _int; }
            set { _int = value; }
        }

        public int Dexterity
        {
            get { return _dex; }
            set { _dex = value; }
        }

        public int Luck
        {
            get { return _luk; }
            set { _luk = value; }
        }

        public Inventory Inventory
        {
            get { return _inventory; }
        }

        public QuestLog QuestLog
        {
            get { return _questLog; }
        }

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
