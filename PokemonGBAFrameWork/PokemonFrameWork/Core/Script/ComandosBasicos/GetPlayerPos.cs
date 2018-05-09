﻿/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 15:32
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of GetPlayerPos.
	/// </summary>
	public class GetPlayerPos:Comando
	{
		public const byte ID = 0x42;

		public const int SIZE = 5;
		
		Word coordenadaX;
		Word coordenadaY;
		
		public GetPlayerPos(Word coordenadaX,Word coordenadaY)
		{
			CoordenadaX=coordenadaX;
			CoordenadaY=coordenadaY;
			
		}

		public GetPlayerPos(RomGba rom, int offset) : base(rom, offset)
		{
		}

		public GetPlayerPos(byte[] bytesScript, int offset) : base(bytesScript, offset)
		{
		}

		public unsafe GetPlayerPos(byte* ptRom, int offset) : base(ptRom, offset)
		{
		}

		public override string Nombre {
			get {
				return "GetPlayerPos";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Descripcion {
			get {
				return "Obtiene las coordenadas X,Y del jugador";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}

		public Word CoordenadaX {
			get {
				return coordenadaX;
			}
			set {
				coordenadaX = value;
			}
		}

		public Word CoordenadaY {
			get {
				return coordenadaY;
			}
			set {
				coordenadaY = value;
			}
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{CoordenadaX,CoordenadaY};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			coordenadaX=new Word(ptrRom,offsetComando);
			offsetComando+=Word.LENGTH;
			coordenadaY=new Word(ptrRom,offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetData(ptrRomPosicionado,coordenadaX);
			ptrRomPosicionado+=Word.LENGTH;
			Word.SetData(ptrRomPosicionado,coordenadaY);
		}
	}
}
