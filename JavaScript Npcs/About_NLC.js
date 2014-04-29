/*
 * ArgonMS MapleStory server emulator written in Java
 * Copyright (C) 2011-2013  GoldenKevin
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as
 * published by the Free Software Foundation, either version 3 of the
 * License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 *
 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

/**
 * Icebyrd Slimm: NLC Mayor (NPC 9201050)
 * New Leaf City Town Street: New Leaf City - Town Center (Map 600000000)
 *
 * Related to Welcome to New Leaf City Quiz (Quest 4900).
 *
 * @author GoldenKevin
 */

let loop = true;
while (loop) {
	let selection = npc.askMenu("What up! Name's Icebyrd Slimm, mayor of New Leaf City! Happy to see you accepted my invite. So, what can I do for you?	\r\n#b"
			+ "#L0#What is this place? #l\r\n"
			+ "#L1#Who is Professor Foxwit? #l\r\n"
			+ "#L2#What's a Foxwit Door? #l\r\n"
			+ "#L3#Where are the MesoGears? #l\r\n"
			+ "#L4#What is the Krakian Jungle? #l\r\n"
			+ "#L5#What's a Gear Portal? #l\r\n"
			+ "#L6#What do the street signs mean? #l\r\n"
			+ "#L7#What's the deal with Jack Masque? #l\r\n"
			+ "#L8#Lita Lawless looks like a tough cookie, what's her story?#l\r\n"
			+ "#L9#When will new boroughs open up in the city? #l\r\n"
			+ "#L10#I want to take the quiz!#l#k");

	switch (selection) {
		case 0:
			npc.sayNext("I've always dreamed of building a city. Not just any city, but one where everyone was welcome. I used to live in Kerning City, so I decided to see if I could create a city. As I went along in finding the means to do so, I encountered many people, some of whom I've come to regard as friends. Like Professor Foxwit-he's our resident genius; saved him from a group of man-eating plants. Jack Masque is an old hunting buddy from Amoria-almost too smooth of a talker for his own good. Lita and I are old friends from Kerning City-she's saved me a few times with that weapon of hers; so I figured she was a perfect choice for Town Sheriff. It took a bit of persuasion, but she came to believe her destiny lies here. About our resident explorer, Barricade came searching for something; he agreed to bring whatever he found to the museum. I'd heard stories about him and his brother when I was still in Kerning City. And Elpam...well, let's just say he's not from around here. At all. We've spoken before, and he seems to mean well, so I've allowed him to stay. I just realized that I've rambled quite a bit! What else would you like to know?");
			break;
		case 1:
			npc.sayNext("A pretty spry guy for being 97. He' s a time-traveler I ran into outside the city one day. Old guy had a bit of trouble with some jungle creatures-like they tried to eat him. In return for me saving him, he agreed to build a time museum. I get the feeling that he's come here for another reason, as he's mentioned more than a few times that New Leaf City has an interesting role to play in the future. Maybe you can find out a bit more... ");
			break;
		case 2:
			npc.sayNext("Heh, I asked the same thing when I saw the Professor building them. They're warp points. Pressing Up will warp you to another location. I recommend getting the hang of them, they're our transport system.");
			break;
		case 3:
			npc.sayNext("The MesoGears are beneath Bigger Ben. It's a monster-infested section of Bigger Ben that Barricade discovered. It seems to reside in a separate section of the tower-quite strange if you ask me. I hear he needs a bit of help exploring it, you should see him. Be careful though, the Wolf Spiders in there are no joke.");
			break;
		case 4:
			npc.sayNext("Ah...well. The Krakian Jungle is located on the outskirts of New Leaf City. Many new and powerful creatures roam those areas, so you'd better be prepared to fight if you head out there. It's at the left end of town. Rumors abound that the Jungle leads to a lost city, but we haven't found anything yet.");
			break;
		case 5:
			npc.sayNext("Well, when John found himself in the MesoGears portion of Bigger Ben, he stood on one and went to another location. However, he could only head back and forth-they don't cycle through like the Foxwit Door. Ancient tech for you.");
			break;
		case 6:
			npc.sayNext("Well, you'll see them just about everywhere. They're areas under construction. The Red lights mean it's not finished, but the Green lights mean it's open. Check back often, we're always building!");
			break;
		case 7:
			npc.sayNext("Ah, Jack. You know those guys that are too cool for school? The ones who always seem to get away with everything? AND get the girl? Well, that's Jack, but without the girl. He thinks he blew his chance, and began wearing that mask to hide his true identity. My lips are sealed about who he is, but he's from Amoria. He might tell you a bit more if you ask him.");
			break;
		case 8:
			npc.sayNext("I've known Lita for a while, thought we've just recently rekindled our friendship. I didn't see her for a quite a bit, but I understand why. She trained for a very, very long time as a Thief. Matter of fact, that's how we first met? I was besieged a group of wayward Mushrooms, and she jumped in to help. When it was time to a pick a sheriff, it was a no-brainer. She's made a promise to help others in their training and protect the city, so if you're interested in a bit of civic duty, speak with her. ");
			break;
		case 9:
			npc.sayNext("Soon, my friend. Even though you can't see them, the city developers are hard at work. When they're ready, we'll open them. I know you're looking forward to it and so am I!");
			break;
		case 10:
			if (player.getLevel() >= 15) {
				if (!player.isQuestCompleted(4900)) {
					player.startQuest(4900, npc.getNpcId());
					npc.say("No problem. I'll give you a something nice if you answer them correctly!");
					loop = false;
				} else {
					npc.sayNext("You've already solved my questions. Enjoy your trip in NLC!!!");
				}
			} else {
				npc.sayNext("Sorry but this quiz is only available for level 15 and above. Please come back to me when you are ready to take this quiz.");
			}
			break;
	}
}