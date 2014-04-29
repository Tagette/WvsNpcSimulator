using System;
using System.Threading.Tasks;
using WvsGame.Maple.Scripting;

/* 
 * Name: A pile of flowers. (1043000)
 * Location: The Forest of Patience <Step 2> (101000101)
 *
 * Purpose: Completes Sabitrama and the Diet Medicine (Quest 2050) and gives jewel ores
 * as a reward if the quest is completed and the player successfully reached the
 * end.
 *
 * Author: GoldenKevin, Tagette
 * Date: 4/26/2014 10:29:18 PM
 */

class bush1 : NpcScript
{
    public override async Task Run()
    {
        int itemId, amount;
        if (HasQuestStarted(2050))
        {
            itemId = 4031020;
            amount = 1;
        }
        else
        {
            int[] rewards = new[] { 
                4020005, 4020006, 4020004, 
                4020001, 4020003, 4020000, 
                4020002
            };

            var rand = new Random();
            itemId = rewards[rand.Next(rewards.Length)];
            amount = 2;
        }

        if (GainItem(itemId, amount))
        {
            SetField(101000000);
        }
        else
        {
            //TODO: Make more GMS-like.
            Talker.Notify("Please check whether your ETC. inventory is full.");
        }
    }
}