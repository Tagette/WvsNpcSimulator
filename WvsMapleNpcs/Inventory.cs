using System;
using System.Collections.Generic;
namespace WvsGame.Maple.Scripting
{
    public class Inventory
    {
        public const int MAX_STACK_COUNT = 100;

        private Player _player;

        private Item[] _items;

        public Inventory(Player player, int capacity)
        {
            _player = player;
            _items = new Item[capacity];
        }

        public Player Player
        {
            get { return _player; }
        }

        public bool HasItem(int ID)
        {
            return HasItem(ID, 1);
        }

        public bool HasItem(int ID, int amount)
        {
            return Count(ID) >= amount;
        }

        public bool HasRoomFor(int ID, int amount)
        {
            int room = 0;
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] == null)
                {
                    room += MAX_STACK_COUNT;
                    continue;
                }
                room += MAX_STACK_COUNT - _items[i].Amount;
                if (room >= amount)
                    break;
            }
            return room >= amount;
        }

        public int GetNextOpenSlot()
        {
            int slot = -1;
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] == null)
                {
                    slot = i;
                    break;
                }
            }
            return slot;
        }

        public bool GainItem(int ID, int amount)
        {
            if (amount > 0)
            {
                if (!HasRoomFor(ID, amount))
                    return false;
                Item[] items = GetItems(ID);
                for (int i = 0; i < items.Length; i++)
                {
                    Item item = items[i];
                    if (item.Amount < MAX_STACK_COUNT)
                    {
                        int dif = MAX_STACK_COUNT - item.Amount;
                        item.Amount += dif;
                        amount -= dif;
                    }
                }
                while (amount > 0)
                {
                    int newSlot = GetNextOpenSlot();
                    _items[newSlot] = new Item(ID, "RAWR");
                    if (amount >= MAX_STACK_COUNT)
                    {
                        _items[newSlot].Amount = MAX_STACK_COUNT;
                        amount -= MAX_STACK_COUNT;
                    }
                    else
                    {
                        _items[newSlot].Amount = amount;
                        amount = 0;
                    }
                }
            }
            else if (amount < 0)
            {
                amount = Math.Abs(amount);
                if (!HasItem(ID, amount))
                    return false;
                for (int i = _items.Length - 1; i >= 0; i--)
                {
                    Item item = _items[i];
                    if (item == null)
                        continue;
                    if (amount >= item.Amount)
                    {
                        amount -= item.Amount;
                        _items[i] = null;
                    }
                    else
                    {
                        item.Amount -= amount;
                        amount = 0;
                        break;
                    }
                }
            }
            return true;
        }

        public int Count(int ID)
        {
            int count = 0;
            Item[] items = GetItems(ID);
            for (int i = 0; i < items.Length; i++)
            {
                count += items[i].Amount;
            }
            return count;
        }

        public Item[] GetItems(int ID)
        {
            List<Item> items = new List<Item>();
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] != null && _items[i].ID == ID)
                {
                    items.Add(_items[i]);
                }
            }
            return items.ToArray();
        }

        public Item GetFirst(int ID)
        {
            Item item = null;
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] != null && _items[i].ID == ID)
                {
                    item = _items[i];
                    break;
                }
            }
            return item;
        }
    }
}
