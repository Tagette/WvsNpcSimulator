using System.Threading.Tasks;
using WvsGame.Maple.Scripting;

/*
 * Name: Robin (2003)
 * Location: Maple Road: Snail Hunting Ground I (40000)
 *
 * Purpose: Answers preset questions to beginners.
 *
 * Author: GoldenKevin, Tagette
 * Date: 4/26/2014 11:08:39 AM
 */

public class begin5 : NpcScript
{
    public override async Task Run()
    {
        bool exit = false;
        while (!exit)
        {
            AddText("Now...ask me any questions you may have on traveling!#b");

            string[] selections = {
                "How do I move?",
                "How do I take down the monsters?",
                "How can I pick up an item?",
                "What happens when I die?",
                "When can I choose a job?",
                "Tell me more about this island!",
                "What should I do to become a Warrior?",
                "What should I do to become a Bowman?",
                "What should I do to become a Theif?",
                "What should I do to become a Magician?",
                "How do I raise the character stats?",
                "How do I check the items that I just picked up?",
                "How do I put on an item?",
                "How do I check out the items that I'm wearing?",
                "What are skills?",
                "How do I get to Victoria Island?",
                "What are mesos?",
                "I want to leave!"
            };
            byte selection = await SendMenu(selections);

            switch (selection)
            {
                case 0:
                    AddText("Alright this is how you move. Use #bleft, right arrow#k to move around the flatland and slanted roads, "
                        + "and press #bAlt#k to jump. A select number of shoes improve your speed and jumping abilities.");
                    await SendNext();
                    break;
                case 1:
                    AddText("Here's how to take down a monster. Every monster possesses an HP of its own and you'll take them down by "
                        + "attacking with either a weapon or through spells. Of course, the stronger they are, the harder it is to take "
                        + "them down.");
                    await SendNext();
                    AddText("In order to attack the monsters you'll need to be equipped with a weapon. When equipped, press #bCtrl#k "
                        + "to use the weapon. With the right timing, you'll be able to easily take down the monsters.");
                    await SendNext();
                    AddText("Once you make the job advancement, you'll acquire different kinds of skills, and you can assign them to "
                        + "HotKeys for easier access. If it's an attacking skill, you don't need to press Ctrl to attack; just press the "
                        + "button assigned as a HotKey.");
                    await SendNext();
                    break;
                case 2:
                    AddText("This is how you gather up an item. Once you take down a monster, an item will be dropped to the ground. "
                        + "When that happens, stand in front of the item and press #bZ#k or #b0 on the NumPad#k to acquire the item.");
                    await SendNext();
                    AddText("Remember, though, that if your item inventory is full, you won't be able to acquire more. So if you have "
                        + "an item you don't need, sell it so you can make something out of it. The inventory may expand once you make "
                        + "the job advancement.");
                    await SendNext();
                    break;
                case 3:
                    AddText("Curious to find out what happens when you die? You'll become a ghost when your HP reaches 0. There will be "
                        + "a tombstone in that place and you won't be able to move, although you still will be able to chat.");
                    await SendNext();
                    AddText("There isn't much to lose when you die if you are just a beginner. Once you have a job, however, it's a "
                        + "different story. You'll lose a portion of your EXP when you die, so make sure you avoid danger and death at "
                        + "all cost.");
                    await SendNext();
                    break;
                case 4:
                    AddText("When do you get to choose your job? Hahaha, take it easy, my friend. Each job has a requirement set for "
                        + "you to meet. Normally a level between 8 and 10 will do, so work hard.");
                    await SendNext();
                    AddText("Level isn't the only thing that determines the advancement, though. You also need to boost up the levels "
                        + "of a particular ability based on the occupation. For example, to be a warrior, your STR has to be over 35, "
                        + "and so forth, you know what I'm saying? Make sure you boost up the abilities that has direct implications to "
                        + "your job.");
                    await SendNext();
                    break;
                case 5:
                    AddText("Want to know about this island? It's called Maple Island and it floats in the air. It's been floating in "
                        + "the sky for a while so the nasty monsters aren't really around. It's a very peaceful island, perfect for beginners!");
                    await SendNext();
                    AddText("But, if you want to be a powerful player, better not think about staying here for too long. You won't be "
                        + "able to get a job anyway. Underneath this island lies an enormous island called Victoria Island. That place "
                        + "is so much bigger than here, it's not even funny.");
                    await SendNext();
                    AddText("How do you get to Victoria Island? On the east of this island there's an harbor called #m60000#. There, "
                        + "you'll find a ship that flies in the air. In front of the ship stands the captain. Ask him about it.");
                    await SendNext();
                    AddText("Oh yeah! One last piece of information before I go. If you are not sure where you are, always press #bW#k. "
                        + "The world map will pop up with the locator showing where you stand. You won't have to worry about getting lost "
                        + "with that.");
                    await SendNext();
                    break;
                case 6:
                    AddText("You want to become a #bwarrior#k? Hmmm, then I suggest you head over to Victoria Island. Head over to a "
                        + "warrior-town called #rPerion#k and see #bDances with Balrog#k. He'll teach you all about becoming a true warrior. "
                        + "Ohh, and one VERY important thing: You'll need to be at least level 10 in order to become a warrior!!");
                    await SendNext();
                    break;
                case 7:
                    AddText("You want to become a #bbowman#k? You'll need to go to Victoria Island to make the job advancement. Head "
                        + "over to a bowman-town called #rHenesys#k and talk to the beautiful #bAthene Pierce#k and learn the in's and "
                        + "out's of being a bowman. Ohh, and one VERY important thing: You'll need to be at least level 10 in order to "
                        + "become a bowman!!");
                    await SendNext();
                    break;
                case 8:
                    AddText("You want to become a #bthief#k? In order to become one, you'll have to head over to Victoria Island. Head "
                        + "over to a thief-town called #rKerning City#k, and on the shadier side of town, you'll see a thief's hideaway. "
                        + "There, you'll meet #bDark Lord#k who'll teach you everything about being a thief. Ohh, and one VERY important "
                        + "thing: You'll need to be at least level 10 in order to become a thief!!");
                    await SendNext();
                    break;
                case 9:
                    AddText("You want to become a #bmagician#k? For you to do that, you'll have to head over to Victoria Island. Head "
                        + "over to a magician-town called #rEllinia#k, and at the very top lies the Magic Library. Inside, you'll meet "
                        + "the head of all wizards, #bGrendel the Really Old#k, who'll teach you everything about becoming a wizard. ");
                    await SendNext();
                    AddText("Oh by the way, unlike other jobs, to become magician you only need to be at level 8. What comes with making "
                        + "the job advancement early also comes with the fact that it takes a lot to become a true powerful mage. Think "
                        + "long and carefully before choosing your path.");
                    await SendNext();
                    break;
                case 10:
                    AddText("You want to know how to raise your character's ability stats? First press S to check out the ability window. "
                        + "Every time you level up, you'll be awarded 5 ability points aka AP's. Assign those AP's to the ability of your "
                        + "choice. It's that simple.");
                    await SendNext();
                    AddText("Place your mouse cursor on top of all abilities for a brief explanation. For example, STR for warriors, DEX "
                        + "for bowman, INT for magician, and LUK for thief. That itself isn't everything you need to know, so you'll need "
                        + "to think long and hard on how to emphasize your character's strengths through assigning the points.");
                    await SendNext();
                    break;
                case 11:
                    AddText("You want to know how to check out the items you've picked up, huh? When you defeat a monster, it'll drop an "
                        + "item on the ground, and you may press Z to pick up the item. That item will then be stored in your item inventory, "
                        + "and you can take a look at it by simply press I.");
                    await SendNext();
                    break;
                case 12:
                    AddText("You want to know how to wear the items, right? Press I to check out your item inventory. Place your mouse cursor "
                        + "on top of an item and double-click on it to put it on your character. If you find yourself unable to wear the item, "
                        + "chances are your character does not meet the level & stat requirements. You can also put on the item by opening the "
                        + "equipment inventory (E) and dragging the item into it. To take off an item, double-click on the item at the equip. "
                        + "inventory.");
                    await SendNext();
                    break;
                case 13:
                    AddText("You want to check on the equipped items, right? Press E to open the equipment inventory, where you'll see exactly "
                        + "what you are wearing right at that moment. To take off an item, double-click on the item. The item will then be sent "
                        + "to the item inventory.");
                    await SendNext();
                    break;
                case 14:
                    AddText("The special 'abilities' you get after acquiring a job are called skills. You'll acquire skills that are specifically "
                        + "for that job. You're not at that stage yet, so you don't have any skills yet, but just remember that to check on your "
                        + "skills, press K to open the skill book. It'll help you down the road.");
                    await SendNext();
                    break;
                case 15:
                    AddText("You can head over to Victoria Island through a ship ride from Southperry that heads to Lith Harbor. Press W to see "
                        + "the World Map, and you'll see where you are on the island. Locate Southperry and that's where you'll need to go. You'll "
                        + "also need some mesos for the ride, so you may need to hunt some monsters around here.");
                    await SendNext();
                    break;
                case 16:
                    AddText("It's the currency used in MapleStory. You may purchase items through mesos. To earn them, you may either defeat the "
                        + "monsters, sell items at the store, or complete quests..");
                    await SendNext();
                    break;
                case 17:
                    exit = true;
                    break;
            }
        }
    }
}