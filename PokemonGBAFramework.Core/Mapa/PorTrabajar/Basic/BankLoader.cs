﻿using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class BankLoader
	{
		public OffsetRom OffsetMapNames { get; set; }
		public  List<OffsetRom>[] maps;
		public  List<OffsetRom> bankPointers = new List<OffsetRom>();
		public  bool banksLoaded = false;
		public  SortedList<int, String> mapNames = new SortedList<int, String>();

		public void reset(RomGba rom, int mapLabels, int numBanks)
		{

			OffsetMapNames = new OffsetRom(rom, mapLabels);
			maps = new List<OffsetRom>[numBanks];
			bankPointers.Clear();
			banksLoaded = false;

		}

		//public BankLoader(int tableOffset, RomGba rom, JLabel label, JTree tree, DefaultMutableTreeNode node)
		//{

		//	tblOffs = (int)new OffsetRom(rom, tableOffset);

		//	lbl = label;
		//	this.tree = tree;
		//	this.node = node;
		//	reset(rom);
		//}


		public List<MapTreeNode> run(RomGba rom,int offset,int numBanks,int[] mapBankSize,OffsetRom offsetMapLabels)
		{

			int mapNum = 0;
			int bankNum = 0;
			OffsetRom dataPtr;
			int miniMapNum;
			int mapName;
			int mapNamePokePtr;
			int tblOffs = offset;
			string convMapName;
			List<MapTreeNode> node = new List<MapTreeNode>();
			List<OffsetRom> mapList = new List<OffsetRom>();
			List<byte[]> preMapList = new List<byte[]>();
			List<byte[]> bankPointersPre = new List<byte[]>();

			for (int i = 0; i < 4; i++)
			{
			bankPointersPre.Add(rom.Data.SubArray(tblOffs, numBanks));
				tblOffs += OffsetRom.LENGTH;
			}

			for(int i=0; i<bankPointersPre.Count;i++)
			{
				bankPointers.Add(new OffsetRom(bankPointersPre[i]));
				bankNum++;
			}

	
			for(int i=0;i< bankPointers.Count;i++)
			{
				preMapList.Clear();
				for (int k = 0; k < 4; k++)
				{
					bankPointersPre.Add(rom.Data.SubArray(bankPointers[i], mapBankSize[mapNum]));
				}

				mapList.Clear();
				miniMapNum = 0;
				for(int j=0;j< preMapList.Count;j++)
				{
					try
					{
						dataPtr = new OffsetRom(preMapList[j]);
						mapList.Add(dataPtr);
						mapName = rom.Data.Bytes[ (int)((dataPtr - (8 << 24)) + 0x14)];
						//mapName -= 0x58; //TODO: Add Jambo51's map header hack
					    mapNamePokePtr = 0;
						convMapName = "";
						if (!rom.Edicion.EsRubiOZafiro)
						{
							if (!mapNames.ContainsKey(mapName))
							{
								mapNamePokePtr =new OffsetRom(rom,offsetMapLabels + ((mapName - 0x58) * 4)); //TODO use the actual structure
								convMapName = BloqueString.Get(rom, mapNamePokePtr);
								mapNames.Add(mapName, convMapName);
							}
							else
							{
								convMapName = mapNames[mapName];
							}
						}
						else
						{
							if (!mapNames.ContainsKey(mapName))
							{
								mapNamePokePtr =new OffsetRom( rom,offsetMapLabels + ((mapName * 8) + 4));
								convMapName = BloqueString.Get(rom, mapNamePokePtr);
								mapNames.Add(mapName, convMapName);
							}
							else
							{
								convMapName = mapNames[mapName];
							}
						}

						 node.Add(new MapTreeNode(convMapName + " (" + mapNum + "." + miniMapNum + ")", mapNum, miniMapNum)); //TODO: Pull PokeText from header
						miniMapNum++;
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message);
					}
				}
				maps[mapNum] = mapList;
				mapNum++;
			}


			return node;

		}

		public class MapTreeNode
		{
			public int bank;
			public int map;
			public string name;
			public MapTreeNode(string name, int bank2, int map2)
			{
				this.name = name;
				bank = bank2;
				map = map2;
			}
		}
	}

}
