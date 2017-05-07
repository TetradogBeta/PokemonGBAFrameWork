﻿/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 10/03/2017
 * Time: 9:03
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Gabriel.Cat;
using Gabriel.Cat.Extension;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Offset.
	/// </summary>
	public  class OffsetRom:IComparable
	{
		public const int LENGTH=4;
		public const int POSICIONIDENTIFICADOR=3;
		public const byte BYTEIDENTIFICADOR16MB=0x8;
		public const byte BYTEIDENTIFICADOR32MB=0x9;
		public const int DIECISEISMEGAS=16777215;
		public const int TREINTAYDOSMEGAS=DIECISEISMEGAS*2;
		
		byte[] bytesPointer;
		
		public OffsetRom(int offset)
		{
            bytesPointer = Serializar.GetBytes(offset);
            bytesPointer[POSICIONIDENTIFICADOR]=offset > DIECISEISMEGAS ? BYTEIDENTIFICADOR32MB : BYTEIDENTIFICADOR16MB;

		}
		
		public OffsetRom(RomData datos,int inicioPointer):this(datos.Rom,inicioPointer)
		{
			
		}
		public OffsetRom(RomGba datos,int inicioPointer):this(datos.Data,inicioPointer)
		{
			
		}
		public OffsetRom(BloqueBytes datos,int inicioPointer):this(datos.Bytes,inicioPointer)
		{
			
		}
		public OffsetRom(byte[] datos,int inicioPointer):this(datos.SubArray(inicioPointer,LENGTH))
		{
			
		}
		
		public OffsetRom(byte[] bytesPointer)
		{
			if(bytesPointer.Length<LENGTH)
				throw new ArgumentOutOfRangeException();
			this.bytesPointer=bytesPointer.SubArray(LENGTH);
		}

		public byte[] BytesPointer {
			get {
				return bytesPointer;
			}
		}
		public  bool IsAPointer
		{
			get{
				return bytesPointer[POSICIONIDENTIFICADOR]==BYTEIDENTIFICADOR16MB||bytesPointer[POSICIONIDENTIFICADOR]==BYTEIDENTIFICADOR32MB;
			}
		}

		public int Offset
		{
			get{
				
				if(!IsAPointer)
					throw new ArgumentException("No es un pointer valido...");
				
				int offset=Serializar.ToInt(new byte[]{bytesPointer[0],bytesPointer[1],bytesPointer[2],0x0});
				if(bytesPointer[POSICIONIDENTIFICADOR]==BYTEIDENTIFICADOR32MB)
					offset+=DIECISEISMEGAS;
				return offset;
			}
			set{
				
				if(value<0||value>TREINTAYDOSMEGAS)
					throw new ArgumentOutOfRangeException();
				
				byte identificado=(byte)(value>DIECISEISMEGAS?0x9:0x8);
				bytesPointer=Serializar.GetBytes(value);
				bytesPointer=new byte[]{bytesPointer[3],bytesPointer[2],bytesPointer[1],identificado};
			}
		}
		
		#region Equals and GetHashCode implementation
		public override bool Equals(object obj)
		{
			return CompareTo(obj)==0;
		}

		public override int GetHashCode()
		{
			int hashCode = 0;
			unchecked {
				if (bytesPointer != null)
					hashCode += 1000000007 * bytesPointer.GetHashCode();
			}
			return hashCode;
		}

		public static bool operator ==(OffsetRom lhs, OffsetRom rhs) {
			bool equals;
			if (ReferenceEquals(lhs, rhs))
				equals= true;
			if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
				equals= false;
			else equals= lhs.Equals(rhs);
			return equals;
		}

		public static bool operator !=(OffsetRom lhs, OffsetRom rhs) {
			return !(lhs == rhs);
		}

		#endregion

		#region IComparable implementation
		public int CompareTo(object obj)
		{
			OffsetRom other=obj as OffsetRom;
			int compareTo;
			if(other!=null)
				compareTo=Serializar.ToInt(bytesPointer).CompareTo(Serializar.ToInt(bytesPointer));
			else compareTo=(int)Gabriel.Cat.CompareTo.Inferior;
			return compareTo;
		}
		#endregion

		public static void SetOffset(RomGba rom,OffsetRom offsetPointerNombres, int offsetNombres)
		{
			OffsetRom offsetAPoner=new OffsetRom(offsetNombres);
			int posicion=0;
			do{
				posicion=rom.Data.SearchArray(posicion+1,offsetPointerNombres.BytesPointer);
				if(posicion>0)
					rom.Data.SetArray(posicion,offsetAPoner.BytesPointer);
			}while(posicion>0);
		}
	}
}