/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of ApplyMovementPos.
	/// </summary>
	public class ApplyMovementPos:Comando
	{
		public const byte ID=0x50;
		public const int SIZE=9;
		ushort personajeAUsar;
		OffsetRom datosMovimiento;
		Byte coordenadaX;
		Byte coordenadaY;
		
		public ApplyMovementPos(ushort personajeAUsar,OffsetRom datosMovimiento,Byte coordenadaX,Byte coordenadaY)
		{
			PersonajeAUsar=personajeAUsar;
			DatosMovimiento=datosMovimiento;
			CoordenadaX=coordenadaX;
			CoordenadaY=coordenadaY;
			
		}
		
		public ApplyMovementPos(RomGba rom,int offset):base(rom,offset)
		{
		}
		public ApplyMovementPos(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe ApplyMovementPos(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Mueve el personaje y luego establece las coordenadas X/Y";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "ApplyMovementPos";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public ushort PersonajeAUsar
		{
			get{ return personajeAUsar;}
			set{personajeAUsar=value;}
		}
		public OffsetRom DatosMovimiento
		{
			get{ return datosMovimiento;}
			set{datosMovimiento=value;}
		}
		public Byte CoordenadaX
		{
			get{ return coordenadaX;}
			set{coordenadaX=value;}
		}
		public Byte CoordenadaY
		{
			get{ return coordenadaY;}
			set{coordenadaY=value;}
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{personajeAUsar,datosMovimiento.Offset,coordenadaX,coordenadaY};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			personajeAUsar=Word.GetWord(ptrRom,offsetComando);
			offsetComando+=Word.LENGTH;
			datosMovimiento=new OffsetRom(ptrRom,new OffsetRom(ptrRom,offsetComando).Offset);
			offsetComando+=OffsetRom.LENGTH;
			coordenadaX=*(ptrRom+offsetComando);
			offsetComando++;
			coordenadaY=*(ptrRom+offsetComando);
			offsetComando++;
			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			Word.SetWord(ptrRomPosicionado,PersonajeAUsar);
			ptrRomPosicionado+=Word.LENGTH;
			OffsetRom.SetOffset(ptrRomPosicionado,datosMovimiento);
			ptrRomPosicionado+=OffsetRom.LENGTH;
			*ptrRomPosicionado=coordenadaX;
			++ptrRomPosicionado;
			*ptrRomPosicionado=coordenadaY;
			++ptrRomPosicionado;
			
		}
	}
}
