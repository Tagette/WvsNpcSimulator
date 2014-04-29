using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WvsGame.Maple.Scripting
{
    public class Item
    {
        private int _id;
        private string _name;
        private int _amount;

        public Item(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public int ID
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
    }
}
