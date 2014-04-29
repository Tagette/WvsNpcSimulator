using System.Threading.Tasks;
using WvsGame.Maple.Scripting;

/* 
 * Name: Heena (2101)
 * Location: Maple Road: Lower level of the Training Camp. (2)
 *
 * Purpose: Teleports players from inside the Mushroom Town Training Camp to the exit.
 *
 * Author: GoldenKevin, Tagette
 * Date: 4/26/2014 11:33:12 PM
 */

class begin_jp3 : NpcScript
{
    public override async Task Run()
    {
        AddText("Are you done with your training? If you wish, I will send you out from this "
                + "training camp.");
        bool yes = await SendYesNo();
        if (yes)
        {
            AddText("Then, I will send you out from here. Good job.");
            await SendNext();
            SetField(3);
        }
        else
        {
            AddText("Haven't you finish the training program yet? If you want to leave this "
                    + "place, please do not hesitate to tell me.");
            await SendOk();
        }
    }
}