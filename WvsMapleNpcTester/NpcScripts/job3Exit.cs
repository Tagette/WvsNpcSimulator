using System.Threading.Tasks;
using WvsGame.Maple.Scripting;

/*
 * Name: Sparkling Crystal (1061010)
 * Location: Hidden Street: The Other Dimension (108010101),
 *           Hidden Street: The Other Dimension (108010201),
 *           Hidden Street: The Other Dimension (108010301),
 *           Hidden Street: The Other Dimension (108010401),
 *           Hidden Street: The Other Dimension (108010501)
 *
 * Purpose: Warps the player out of the other dimension.
 *
 * Author: GoldenKevin, Tagette
 * Date: 4/26/2014 10:48:41 AM
 */

public class job3Exit : NpcScript
{
    public override async Task Run()
    {
        AddText("You can use the Sparkling Crystal to go back to the real world. ");
        AddText("Are you sure you want to go back?");
        bool yes = await SendYesNo();
        if (yes)
        {
            int toMap = -1;
            switch (GetField())
            {
                case 108010101:
                    toMap = 100000000;
                    break;
                case 108010201:
                    toMap = 101000000;
                    break;
                case 108010301:
                    toMap = 102000000;
                    break;
                case 108010401:
                    toMap = 103000000;
                    break;
                case 108010501:
                    toMap = 120000000;
                    break;
            }
            if (toMap > 0)
            {
                SetField(toMap);
            }
        }
    }
}