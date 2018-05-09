/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of SetDoorOpened.
	/// </summary>
	public class SetDoorOpened:Comando
	{
		public const byte ID = 0xAC;
		public const int SIZE = 5;
		Word coordenadaX;
		Word coordenadaY;
 
		public SetDoorOpened(Word coordenadaX, Word coordenadaY)
		{
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
 
		}
   
		public SetDoorOpened(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public SetDoorOpened(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe SetDoorOpened(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Prepara la puerta para ser abierta";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "SetDoorOpened";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word CoordenadaX {
			get{ return coordenadaX; }
			set{ coordenadaX = value; }
		}
		public Word CoordenadaY {
			get{ return coordenadaY; }
			set{ coordenadaY = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ coordenadaX, coordenadaY };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			coordenadaX = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			coordenadaY = new Word(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetData(ptrRomPosicionado, CoordenadaX);
			ptrRomPosicionado += Word.LENGTH;
			Word.SetData(ptrRomPosicionado, CoordenadaY);
		}
	}
}
