using System;
using System.Threading.Tasks;
using WvsGame.Maple.Scripting;

/* 
 * Name: A pile of herbs. (1043001)
 * Location: The Forest of Patience <Step 5> (101000104)
 *
 * Purpose: Completes Sabitrama's Anti-Aging Medicine (Quest 2051) and gives rare jewel
 * or mineral ores or Red-Hearted Earrings as a reward if the quest is completed
 * and the player successfully reached the end.
 *
 * Author: GoldKevin, Tagette
 * Date: 4/26/2014 10:53:23 PM
 */

class bush2 : NpcScript
{
    public override async Task Run()
    {
        int itemId, amount;
        if (HasQuestStarted(2051))
        {
            itemId = 4031032;
            amount = 1;
        }
        else
        {
            int[] rewards = new[] {
                4020007, 2,
                4020008, 2,
                4010006, 2,
                1032013, 1
            };
            var rand = new Random();
            int index = rand.Next(rewards.Length / 2);
            itemId = rewards[index];
            amount = rewards[index + 1];
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