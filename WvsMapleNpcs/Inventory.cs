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

        /// <summary>
        /// Gets the player that owns the inventory.
        /// </summary>
        public Player Player
        {
            get { return _player; }
        }

        /// <summary>
        /// Returns the array containing the inventory's items.
        /// </summary>
        public Item[] Items
        {
            get { return _items; }
        }

        /// <summary>
        /// Adds up the total amount from each stack of items.
        /// </summary>
        public int TotalItems()
        {
            int total = 0;
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] != null)
                {
                    total += _items[i].Amount;
                }
            }
            return total;
        }

        /// <summary>
        /// Counts how many stacks of items there is.
        /// </summary>
        public int TotalStacks()
        {
            int total = 0;
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] != null)
                {
                    total++;
                }
            }
            return total;
        }

        /// <summary>
        /// Determines wwhether or not the inventory is empty.
        /// </summary>
        public bool IsEmpty()
        {
            bool empty = true;
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] != null)
                {
                    empty = false;
                    break;
                }
            }
            return empty;
        }

        /// <summary>
        /// Determines if the inventory has an item.
        /// </summary>
        public bool HasItem(int ID)
        {
            return HasItem(ID, 1);
        }

        /// <summary>
        /// Determines if the inventory has a certain amount of an item.
        /// </summary>
        public bool HasItem(int ID, int amount)
        {
            return Count(ID) >= amount;
        }

        /// <summary>
        /// Determines if the inventory has room for a certain amount of items.
        /// </summary>
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
                if (_items[i].ID == ID)
                    room += MAX_STACK_COUNT - _items[i].Amount;
                if (room >= amount)
                    break;
            }
            return room >= amount;
        }

        /// <summary>
        /// Gets the next slot available in the inventory.
        /// </summary>
        /// <returns>Returns -1 if no slot is available.</returns>
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

        /// <summary>
        /// Adds items when the amount is positive. Removes items when the amount is negative.
        /// </summary>
        /// <returns>Returns true if the item was added or removed successfully.</returns>
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
                        item.Amount += Math.Min(dif, amount);
                        amount -= dif;
                    }
                }
                while (amount > 0)
                {
                    int newSlot = GetNextOpenSlot();
                    _items[newSlot] = new Item(ID);
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

        /// <summary>
        /// Clears all items in the inventory with the specified id.
        /// </summary>
        public void ClearItem(int ID)
        {
            for (int i = 0; i < _items.Length; i++)
                if (_items[i] != null && _items[i].ID == ID)
                    _items[i] = null;
        }

        /// <summary>
        /// Counts the total amount of items that have the specified id in the inventory.
        /// </summary>
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

        /// <summary>
        /// Gets an array of item stacks from the inventory matching the specified id.
        /// </summary>
        /// <returns>Returns an empty array if no items exist.</returns>
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

        /// <summary>
        /// Gets the first itemstack in the inventory with the specified id.
        /// </summary>
        /// <returns>Returns null if no item is found.</returns>
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
