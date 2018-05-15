/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of CheckAttack.
	/// </summary>
	public class CheckAttack:Comando
	{
		public const byte ID=0x7C;
		public const int SIZE=3;		
		public const string NOMBRE="CheckAttack";
		public const string DESCRIPCION="Comprueba que haya un pokemon en el equipo con un ataque en particular";
		Word ataqueAComprobar;
		
		public CheckAttack(Word ataqueAComprobar)
		{
			AtaqueAComprobar=ataqueAComprobar;
			
		}
		
		public CheckAttack(RomGba rom,int offset):base(rom,offset)
		{
		}
		public CheckAttack(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe CheckAttack(byte* ptRom,int offset):base(ptRom,offset)
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
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word AtaqueAComprobar
		{
			get{ return ataqueAComprobar;}
			set{ataqueAComprobar=value;}
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ataqueAComprobar};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			ataqueAComprobar=new Word(ptrRom,offsetComando);			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado++;
			Word.SetData(ptrRomPosicionado,AtaqueAComprobar);			
		}
	}
}