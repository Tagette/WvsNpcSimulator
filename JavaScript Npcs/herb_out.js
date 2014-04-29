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
 * Louis (NPC 1032004)
 * Hidden Street: The Forest of Patience <Step 1> (Map 101000100),
 *   Hidden Street: The Forest of Patience <Step 2> (Map 101000101),
 *   Hidden Street: The Forest of Patience <Step 3> (Map 101000102),
 *   Hidden Street: The Forest of Patience <Step 4> (Map 101000103),
 *   Hidden Street: The Forest of Patience <Step 5> (Map 101000104)
 *
 * Forfeits Sabitrama jump quests.
 *
 * @author GoldenKevin (content from KiniroMS r227)
 */

if (npc.askYesNo("Would you like to return to Ellinia?") == 1)
	player.changeMap(101000000);