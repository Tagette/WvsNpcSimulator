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
 * a pile of flowers (NPC 1043000)
 * Hidden Street: The Forest of Patience <Step 2> (Map 101000101)
 *
 * Completes Sabitrama and the Diet Medicine (Quest 2050) and gives jewel ores
 * as a reward if the quest is completed and the player successfully reached the
 * end.
 *
 * @author GoldenKevin (content from KiniroMS r227)
 */

let itemId, quantity;
if (player.isQuestActive(2050)) {
	itemId = 4031020;
	quantity = 1;
} else {
	let rewards = [4020005, 4020006, 4020004, 4020001, 4020003, 4020000, 4020002];
	itemId = rewards[Math.floor(Math.random() * rewards.length)];
	quantity = 2;
}

if (player.gainItem(itemId, quantity))
	player.changeMap(101000000);
else //TODO: GMS-like line
	npc.say("Please check whether your ETC. inventory is full.");