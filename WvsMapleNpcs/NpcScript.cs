using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace WvsGame.Maple.Scripting
{
    public abstract class NpcScript
    {
        private string _textBuffer;

        public Npc Parent { get; internal set; }
        public Player Talker { get; internal set; }

        public abstract Task Run();

        /// <summary>
        /// Stops the Npc session. Must be written at the end of a script.
        /// </summary>
        public void Stop()
        {
            throw new NpcExitException();
        }

        /// <summary>
        /// Adds text to the buffer.
        /// </summary>
        /// <param name="text">The text to add to the buffer.</param>
        public void AddText(string text)
        {
            _textBuffer += text;
        }

        /// <summary>
        /// Adds text to the buffer.
        /// </summary>
        /// <param name="text">The text to add to the buffer.</param>
        public void AddText(string format, params object[] objs)
        {
            AddText(string.Format(format, objs));
        }

        /// <summary>
        /// Displays a dialog with text and an OK button.
        /// </summary>
        /// <returns>True if the ok button was pressed.</returns>
        public async Task<bool> SendOk()
        {
            return await SendMenu("Ok", "Exit") == 0;
        }

        /// <summary>
        /// Displays a dialog with text and a Next button.
        /// </summary>
        /// <returns>True if the next button was pressed.</returns>
        public async Task<bool> SendNext()
        {
            return await SendMenu("Next", "Exit") == 0;
        }

        /// <summary>
        /// Displays a dialog with text and Previous/OK buttons.
        /// </summary>
        public async Task<bool> SendPrev()
        {
            return await SendMenu("Prev", "Exit") == 0;
        }

        /// <summary>
        /// Displays a dialog with text and Next/Previous buttons.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SendNextPrev()
        {
            return await SendMenu("Next", "Prev", "Exit") == 0;
        }

        /// <summary>
        /// Displays a dialog with text and Yes/No buttons.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SendYesNo()
        {
            return await SendMenu("Yes", "No", "Exit") == 0;
        }

        /// <summary>
        /// Displays a dialog with text and Selections.
        /// </summary>
        /// <param name="selections">The selections for the menu. The first argument is 0.</param>
        public async Task<byte> SendMenu(params string[] selections)
        {
            ConsoleTools.PrintWithNpcColor(_textBuffer);
            ClearTextBuffer();

            if (selections == null || selections.Length == 0)
                return 0;

            byte selection = 0;
            await Task.Factory.StartNew(() =>
            {
                bool exit = false;
                while (!exit)
                {
                    for (int i = 0; i < selections.Length; i++)
                    {
                        ConsoleTools.PrintWithNpcColor((i + 1) + ") " + selections[i]);
                    }
                    Console.Write("Choose> ");
                    string input = Console.ReadLine();
                    if (byte.TryParse(input, out selection))
                    {
                        if (selection > 0 && selection <= selections.Length)
                        {
                            exit = true;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect choice.");
                        }
                    }
                    else if (input.ToLower() == "exit")
                    {
                        Stop();
                    }
                    else
                    {
                        Console.WriteLine("Incorrect value.");
                    }
                }
            });
            Console.WriteLine();
            return --selection;
        }

        /// <summary>
        /// Returns the talker's field.
        /// </summary>
        public int GetField() { return Talker.Field; }

        /// <summary>
        /// Sets the talker's field to the given field.
        /// </summary>
        /// <param name="ID">The id for the field.</param>
        public void SetField(int ID) { SetField(ID, 0); }

        /// <summary>
        /// Sets the talker's field to the given field and spawn point.
        /// </summary>
        /// <param name="ID">The id for the field.</param>
        /// <param name="spawnPoint">The spawn point in the field.</param>
        public void SetField(int ID, int spawnPoint)
        {
            Talker.Field = ID;
            Talker.Notify(string.Format("Player field set to {0}. ({1})", ID, spawnPoint));
        }

        /// <summary>
        /// Returns the talker's level.
        /// </summary>
        public int GetLevel() { return Talker.Level; }

        /// <summary>
        /// Returns the talker's job.
        /// </summary>
        public int GetJob() { return Talker.Job; }

        /// <summary>
        /// Returns the talker's strength.
        /// </summary>
        public int GetStrength() { return Talker.Strength; }

        /// <summary>
        /// Returns the talker's dexterity.
        /// </summary>
        public int GetDexterity() { return Talker.Dexterity; }

        /// <summary>
        /// Returns the talker's intelligence.
        /// </summary>
        /// <returns></returns>
        public int GetIntelligence() { return Talker.Intelligence; }

        /// <summary>
        /// Returns the talker's luck.
        /// </summary>
        /// <returns></returns>
        public int GetLuck() { return Talker.Luck; }

        /// <summary>
        /// If the amount is negative
        ///     Returns false if the talker doesn't have that amount.
        ///     Returns true if the talker have this amount and removes that amount.
        /// If the amount is positive
        ///     Adds the amount to the talker.
        /// </summary>
        /// <param name="amount">The amount to add.</param>
        public bool GainMeso(int amount)
        {
            Talker.Meso += amount;
            Talker.Notify(string.Format("Player {0} {1} mesos.", amount > 0 ? "gained" : "lost", Math.Abs(amount))); return amount > 0;
        }

        /// <summary>
        /// Returns false if the talker don't have enough slots to receive the item.
        /// Returns true if the talker has enough slots to receive the item and adds the item to the talker.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool GainItem(int ID) { return GainItem(ID, 1); }

        /// <summary>
        /// If the amount is negative
        ///     Returns false if the talker don't have the item.
        ///     Returns true if the talker has the item and removes that item with the specified quantity.
        /// If the amount is positive
        ///     Returns false if the talker don't have enough slots to receive the item.
        ///     Returns true if the talker has enough slots to receive the item and adds the item with the specified quantity to the talker.
        /// </summary>
        /// <param name="id">The maple id of the item.</param>
        /// <param name="amount">The amount of the item.</param>
        public bool GainItem(int ID, int amount)
        {
            Talker.Inventory.GainItem(ID, amount);
            Talker.Notify(string.Format("Player {0} {1} item. ({2})", amount > 0 ? "gained" : "lost", Math.Abs(amount), ID)); return amount > 0;
        }

        /// <summary>
        /// Returns true if the talker has the given item.
        /// </summary>
        /// <param name="ID">The maple id of the item.</param>
        public bool HasItem(int ID) { return HasItem(ID, 1); }

        /// <summary>
        /// Returns true if the talker has the given item with the specified quantity.
        /// </summary>
        /// <param name="ID">The maple id of the item.</param>
        /// <param name="amount">The amount of the item that is needed.</param>
        public bool HasItem(int ID, int amount) { return Talker.Inventory.HasItem(ID, amount); }

        /// <summary>
        /// Clears all items of the specified item ID in the talkers inventory.
        /// </summary>
        /// <param name="ID">The maple id of the item.</param>
        public void ClearItem(int ID)
        {
            Talker.Inventory.ClearItem(ID);
            Talker.Notify(string.Format("Cleared all {0}.", ID));
        }

        /// <summary>
        /// Returns true if the talker has enough open slots for the given item.
        /// </summary>
        /// <param name="ID">The maple id of the item.</param>
        public bool HasOpenSlotsFor(int ID) { return Talker.Inventory.HasRoomFor(ID, 1); }

        /// <summary>
        /// Returns the given item's quantity of the talker.
        /// </summary>
        /// <param name="ID">The maple id of the item.</param>
        public int GetItemAmount(int ID) { return Talker.Inventory.Count(ID); }

        /// <summary>
        /// Returns true if the talker has the given quest started.
        /// </summary>
        /// <param name="ID">The maple id of the quest.</param>
        public bool HasQuestStarted(int ID) { return Talker.QuestLog.HasStarted(ID); }

        /// <summary>
        /// Returns if the talker has the given quest completed.
        /// </summary>
        /// <param name="ID">The maple id of the quest.</param>
        /// <returns></returns>
        public bool HasQuestCompleted(int ID) { return Talker.QuestLog.HasCompleted(ID); }

        /// <summary>
        /// Returns the content of the given file.
        /// </summary>
        /// <param name="ID">The id of the file.</param>
        public string FileRef(string ID) { return "#F" + ID + "#"; }

        /// <summary>
        /// Returns the icon of the given item.
        /// </summary>
        /// <param name="ID">The id of the item.</param>
        public string ItemIcon(int ID) { return "#v" + ID + "#"; }

        /// <summary>
        /// Returns the name of the given mob.
        /// </summary>
        /// <param name="ID">The id of the mob.</param>
        public string MobRef(string ID) { return "#o" + ID + "#"; }

        /// <summary>
        /// Returns the name of the given field.
        /// </summary>
        /// <param name="ID">The id of the field.</param>
        public string FieldRef(int ID) { return "#m" + ID + "#"; }

        /// <summary>
        /// Returns the talker's name.
        /// </summary>
        public string PlayerRef() { return Talker.Name; }

        /// <summary>
        /// Returns the name of the given npc.
        /// </summary>
        /// <param name="ID">The id of the npc.</param>
        public string NpcRef(string ID) { return "Zoidberg"; }

        private void ClearTextBuffer()
        {
            _textBuffer = string.Empty;
        }
    }
}