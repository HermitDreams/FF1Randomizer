﻿using System;
using System.Collections.Generic;
using System.Text;
using FF1Lib;

namespace Sandbox
{
	public class UsedText
	{
		public static void GetUsedText()
		{
			// Text F7 seems unused.

			FF1Rom rom = new("ff1.nes");

			const int MapObjJumpTableOffset = 0x390D3;
			const int JumpSize = 2;
			const int MapObjOffset = 0x395D5;
			const int MapObjSize = 4;
			const int MapObjCount = 0xD0;

			const int TalkReplace = 0x9495;
			const int TalkFight = 0x94AA;

			SortedSet<byte> usedText = new();

			var jumpTable = rom.Get(MapObjJumpTableOffset, JumpSize * MapObjCount).ToUShorts();
			var mapObjs = rom.Get(MapObjOffset, MapObjSize * MapObjCount).Chunk(MapObjSize);

			for (int i = 0; i < MapObjCount; i++)
			{
				usedText.Add(mapObjs[i][1]);
				usedText.Add(mapObjs[i][2]);
				if (jumpTable[i] != TalkReplace && jumpTable[i] != TalkFight)
				{
					usedText.Add(mapObjs[i][3]);
				}
			}

			Console.WriteLine("{0} entries", usedText.Count);
			foreach (byte text in usedText)
			{
				Console.WriteLine("{0}", text);
			}
		}
	}
}
