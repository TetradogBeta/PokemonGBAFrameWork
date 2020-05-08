﻿/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 31/05/2017
 * Hora: 21:02
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of Nop.
	/// </summary>
	public class Nop:Comando
	{
		public const byte ID=0x0;
        public const string NOMBRE = "Nop";
        public const string DESCRIPCION = "No hace absolutamente nada";
		
		public Nop()
		{}
		public Nop(RomGba rom,int offset):base(rom,offset)
		{}
		public Nop(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe Nop(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return DESCRIPCION;
			}
		}
		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return NOMBRE;
			}
		}


		
	}
	public class Nop1:Nop
	{
		public new const byte ID=0x1;
        public new const string NOMBRE = "Nop1";
        public Nop1() { }
		public Nop1(RomGba rom,int offset):base(rom,offset)
		{}
		public Nop1(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe Nop1(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Nombre {
			get {
				return NOMBRE;
			}
		}
		public override byte IdComando {
			get {
				return ID;
			}
		}
	}
	
	
}