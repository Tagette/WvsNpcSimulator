using System.Threading.Tasks;
using WvsGame.Maple.Scripting;

/* 
 * Name: Sera (2100)
 * Location: Maple Road: Entrance - Mushroom Town Training Camp (0),
 *   Maple Road: Upper level of the Training Camp (1)
 *
 * Purpose: Greets newly created players at the entrance of the Mushroom Town Training
 * Camp, and gives a player some help in Upper Level of the Training Camp.
 *
 * Author: GoldenKevin, Tagette
 * Date: 4/26/2014 11:41:46 PM
 */

class begin_jp1 : NpcScript
{
    public override async Task Run()
    {
        int map = GetField();
        if (map == 0 || map == 3)
        {
            AddText("Welcome to the world of MapleStory. The purpose of this training camp is to "
                    + "help beginners. Would you like to enter this training camp? Some people "
                    + "start their journey without taking the training program. But I strongly "
                    + "recommend you take the training program first.");
            bool enterCamp = await SendYesNo();
            if (enterCamp)
            {
                AddText("Ok then, I will let you enter the training camp. Please follow your "
                        + "instructor's lead.");
                await SendNext();
                SetField(1);
            }
            else
            {
                AddText("Do you really wanted to start your journey right away?");
                bool confirm = await SendYesNo();
                if (confirm)
                {
                    AddText("It seems like you want to start your journey without taking the "
                            + "training program. Then, I will let you move on the training ground."
                            + "Be careful~");
                    await SendNext();
                    SetField(40000);
                }
                else
                {
                    AddText("Please talk to me again when you finally made your decision.");
                    await SendNext();
                }
            }
        }
        else if(map == 1)
        {
            AddText("This is the image room where your first training program begins. In this "
                    + "room, you will have an advance look into the job of your choice.");
            await SendNext();
            AddText("Once you train hard enough, you will be entitled to occupy a job. You can "
                    + "become a Bowman in Henesys, a Magician in Ellinia, a Warrior in Perion, "
                    + "and a Thief in Kerning City..");
            await SendOk();
        }
    }
}