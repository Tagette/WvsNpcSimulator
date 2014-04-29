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
        private int _amount;

        public Item(int id)
        {
            _id = id;
            _amount = 1;
        }

        public int ID
        {
            get { return _id; }
        }

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
    }
}
