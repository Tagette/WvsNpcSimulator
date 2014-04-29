using System.Threading.Tasks;
using WvsGame.Maple.Scripting;

/*
 * Name: Bowman Job Instructor (1072006)
 * Location: Hidden Street: Ant Tunnel For Bowman (108000100),
 *           Hidden Street: Ant Tunnel For Bowman (108000101),
 *           Hidden Street: Ant Tunnel For Bowman (108000102)
 *
 * Purpose: Lets players out of bowmen 2nd job advancement quest, whether it be for
 * forfeit or completion.
 *
 * Author: Tagette
 * Date: 4/26/2014 11:53:44 AM
 */

public class inside_archer : NpcScript
{
    private const int REQUIRED_ITEM = 4031013;
    private const int REQUIRED_AMOUNT = 30;
    private const int REWARD_ITEM = 4031012;
    private const int REWARD_AMOUNT = 1;
    private const int EXIT_MAP = 106010000;

    public override async Task Run()
    {
        if (HasItem(REQUIRED_ITEM, REQUIRED_AMOUNT))
        {
            AddText("Ohhhhh...you collected all {0} #b{1}#k!! Wasn't it difficult?? ", REQUIRED_AMOUNT, ItemIcon(REQUIRED_ITEM));
            AddText("Alright. You've passed the test and for that, I'll reward you #b{0}#k. ", ItemIcon(REQUIRED_ITEM));
            AddText("Take that item and go back to Henesys.");
            await SendNext();

            ClearItem(REQUIRED_ITEM);
            ClearItem(4031010);

            GainItem(REWARD_ITEM, 1);
            SetField(100000000);
        }
        else
        {
            AddText("What's going on? Doesn't look like you have collected {0} #b{1}#k, yet..."
                + "If you're having problems with it, then you can leave, come back and try it again. "
                + "So...wanna give up and get out of here?", REQUIRED_AMOUNT, ItemIcon(REQUIRED_ITEM));
            bool leave = await SendYesNo();
            if (leave)
            {
                AddText("Really... alright, I'll let you out. Please don't give up, though. You can always try again, so do not give up. Until then, bye...");
                SetField(EXIT_MAP);
            }
            else
            {
                AddText("That's right! Stop acting weak and start collecting the marbles. Talk to me when you have collected {0} #b{1}#k.", REQUIRED_AMOUNT, ItemIcon(REQUIRED_ITEM));
                await SendNext();
            }
        }
    }
}