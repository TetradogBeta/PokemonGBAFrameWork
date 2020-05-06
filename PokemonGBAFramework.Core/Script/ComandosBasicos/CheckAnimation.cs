/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of CheckAnimation.
	/// </summary>
	public class CheckAnimation:Comando
	{
		public const byte ID=0x9E;
		public new const int SIZE=Comando.SIZE+Word.LENGTH;
		
		public const string NOMBRE="CheckAnimation";
		public const string DESCRIPCION="comprueba si una animación se está reproduciendo actualmente o no. De esta manera, se detendrá hasta que la animación se haya completado.";

        public CheckAnimation(Word animacion)
		{
			Animacion=animacion;
			
		}
		
		public CheckAnimation(RomGba rom,int offset):base(rom,offset)
		{
		}
		public CheckAnimation(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe CheckAnimation(byte* ptRom,int offset):base(ptRom,offset)
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
        public Word Animacion { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Animacion};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Animacion=new Word(ptrRom,offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado+=base.Size;
			Word.SetData(ptrRomPosicionado,Animacion);
		}
	}
}
