﻿/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 13:35
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of Sound.
	/// </summary>
	public class Sound:Comando
	{
		public const byte ID=0x2F;
		public new const int SIZE=0x3;
		public const string DESCRIPCION= "Reproduce el sonido";
		public const string NOMBRE= "Sound";

		Word sonido;
		public Sound(Word sonido)
		{
			Sonido=sonido;
		}
		public Sound(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public Sound(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe Sound(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando=> ID;
		public override string Nombre => NOMBRE;

		public Word Sonido {
			get {
				return sonido;
			}
			set {
				if (value == default)
					value = new Word();

				sonido = value;
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		protected override IList<object> GetParams()
		{
			return new Object[]{Sonido};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			sonido=new Word(ptrRom,offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			 data[0]=IdComando;
			Word.SetData(data, ,sonido);
		}
	}
	public class FanFare:Sound
	{
		public new const byte ID=0x31;
		public new const string DESCRIPCION= "Reproduce una cancion Sappy como un fanfare";
		public new const string NOMBRE= "Fanfare";
		public FanFare(Word sonido):base(sonido)
		{}
		public FanFare(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public FanFare(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe FanFare(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando=> ID;
		public override string Nombre => NOMBRE;
	}
	public class PlaySong2:Sound
	{
		public new const byte ID=0x34;
		public new const string DESCRIPCION= "Cambia a otra cancion Sappy";
		public new const string NOMBRE= "PlaySong2";

		public PlaySong2(Word sonido):base(sonido)
		{}
		public PlaySong2(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public PlaySong2(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe PlaySong2(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando=> ID;
		public override string Nombre => NOMBRE;
	}
	public class FadeSong:Sound
	{
		public new const byte ID=0x36;
		public new const string DESCRIPCION = "Suavemente cambia a otra cancion Sappy";
		public new const string NOMBRE = "FadeSong";
		public FadeSong(Word sonido):base(sonido)
		{}
		public FadeSong(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public FadeSong(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe FadeSong(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;
	}
	public class PlaySong:Sound
	{
		public new const int ID=0x33;
		public new const int SIZE=Sound.SIZE+1;
		public new const string DESCRIPCION = "Cambia a otra cancion Sappy";
		public new const string NOMBRE = "PlaySong";

		public PlaySong(Word sonido,byte desconocido):base(sonido)
		{
			Desconocido=desconocido;
		}
		public PlaySong(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public PlaySong(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe PlaySong(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;
		public override int Size => SIZE;
		public byte Desconocido { get; set; }

		protected override IList<object> GetParams()
		{
			return new object[] { Sonido, Desconocido };
		}
		protected unsafe  override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			base.CargarCamando(ptrRom, offsetComando);
			Desconocido=ptrRom[offsetComando+Sound.SIZE];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado+=Sound.SIZE;
			*ptrRomPosicionado=Desconocido;
		}
	}
}
