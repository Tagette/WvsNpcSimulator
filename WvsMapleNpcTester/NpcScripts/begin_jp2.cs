using System.Threading.Tasks;
using WvsGame.Maple.Scripting;

/* 
 * Name: Peter (9101001)
 * Location: Maple Road: Entrance - Mushroom Town Training Camp (3)
 *
 * Purpose: Teleports players from the Training Camp exit to the road to
 * Mushroom Town.
 *
 * Author: GoldenKevin, Tagette
 * Date: 4/26/2014 11:38:13 PM
 */

class begin_jp2 : NpcScript
{
    public override async Task Run()
    {
        AddText("You have finished all your trainings. Good job. You seem to be ready to start "
                + "with the journey right away! Good, I will let you on to the next place.");
        await SendNext();
        AddText("But remember, once you get out of here, you will enter a village full with "
                + "monsters. Well them, good bye!");
        await SendNext();
        SetField(40000);
    }
}