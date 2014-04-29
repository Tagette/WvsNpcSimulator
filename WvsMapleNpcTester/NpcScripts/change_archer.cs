using System.Threading.Tasks;
using WvsGame.Maple.Scripting;

/* 
 * Name: Job Instructor (1072002)
 * Location: Warning Street: The road to the Dungeon. (10601000)
 *
 * Purpose: Admits bowmen into the second job advancement challenge.
 *
 * Author: GoldenKevin, Tagette
 * Date: 4/26/2014 11:50:50 PM
 */

class change_archer : NpcScript
{
    public override async Task Run()
    {
        if (HasItem(4031010, 1) && HasItem(4061013, 0))
        {
            AddText("Hmmm...it is definitely the letter from #b#p1012100##k...so you came all the "
                    + "way here to take the test and make the 2nd job advancement as the bowman. "
                    + "Alright, I'll explain the test to you. Don't sweat it much, though; it's "
                    + "not that complicated.");
            await SendNext();
            AddText("I'll send you to a hidden map. You'll see monsters not normally seen in "
                    + "normal fields. They look the same like the regular ones, but with a "
                    + "totally different attitude. They neither boost your experience level nor "
                    + "provide you with item.");
            await SendNext();
            AddText("You'll be able to acquire a marble called #b#t4031013##k while knocking down "
                    + "those monsters. It is a special marble made out of their sinister, evil "
                    + "minds. Collect 30 of those, then go talk to a colleague of mine in there. "
                    + "That's how you pass the test.");
            await SendNext();
            AddText("Once you go inside, you can't leave until you take care of your mission. If "
                    + "you die, your experience level will decrease...so you better really buckle "
                    + "up and get ready...well, do you want to go for it now?");
            bool yes = await SendYesNo();
            if (yes)
            {
                AddText("I don't think you are prepared for this. Find me when you ARE ready. "
                        + "There are neither portals nor stores inside, so you better get 100% "
                        + "ready for it.");
                await SendNext();
            }
            else
            {
                AddText("Alright I'll let you in! Defeat the monsters inside, collect 30 Dark "
                        + "Marbles, and then talk to my colleague inside. Then he'll award you "
                        + "the proof of passing the test, #b#t4031012##k. Good luck.");
                await SendNext();
                //TODO: MakeEvent("change_job", false, new [] { GetPlayer(), 108000102 });
            }
        }
        else if (HasItem(4031010, 1) && HasItem(4031013, 1))
        {
            AddText("So you've given up in the middle of this before. Don't worry about it, "
                    + "because you can always retake the test. Now...do you want to go back in "
                    + "and try again?");
            bool yes = await SendYesNo();
            if (yes)
            {
                AddText("You don't seem too prepared for this. Find me when you ARE ready. There "
                        + "are neither portals or stores inside, so you better get 100% ready for it.");
                await SendNext();
            }
            else
            {
                AddText("Alright! I'll let you in! Sorry to say this, but I have to take away all "
                        + "your marbles beforehand. Defeat the monsters inside, collect 30 Dark "
                        + "Marbles, then strike up a conversation with a colleague of mine inside. "
                        + "He'll give you the #b#t4031012##k, the proof that you've passed the test. "
                        + "Best of luck to you.");
                await SendNext();
                GainItem(4031013, -1);
                //TODO: MakeEvent("change_job", false, new [] { GetPlayer(), 108000102 }
            }
        }
        else if (GetJob() == 300 && GetLevel() >= 30)
        {
            AddText("Do you want to be a stronger bowman? Let me take care of that for you, then. "
                    + "You look definitely qualified for it. For now, go see #b#p1012100##k of "
                    + "Henesys first.");
            await SendNext();
        }
    }
}