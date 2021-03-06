/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of ApplyMovement.
	/// </summary>
	public class ApplyMovement:Comando,IMovement
	{
		public const byte ID=0x4F;
		public new const int SIZE=Comando.SIZE+Word.LENGTH+OffsetRom.LENGTH;
		public const string NOMBRE="ApplyMovement";
		public const string DESCRIPCION="Aplica los movimientos al persoanje especificado";
		
		public ApplyMovement() { }
        public ApplyMovement(Word personajeAUsar,BloqueMovimiento datosMovimiento)
		{
			PersonajeAUsar=personajeAUsar;
			Movimiento=datosMovimiento;
			
		}
		
		public ApplyMovement(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager, rom,offset)
		{
		}
		public ApplyMovement(ScriptAndASMManager scriptManager, byte[] bytesScript,int offset):base(scriptManager, bytesScript,offset)
		{}
		public unsafe ApplyMovement(ScriptAndASMManager scriptManager, byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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
        public Word PersonajeAUsar { get; set; }
        public BloqueMovimiento Movimiento { get; set; }

        public override System.Collections.Generic.IList<Gabriel.Cat.S.Utilitats.Propiedad> GetParams()
		{
			return new Gabriel.Cat.S.Utilitats.Propiedad[]{ new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(PersonajeAUsar)), new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(Movimiento))};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			PersonajeAUsar=new Word(ptrRom,offsetComando);
			offsetComando+=Word.LENGTH;
			Movimiento=new BloqueMovimiento(ptrRom,new OffsetRom(ptrRom,offsetComando));

		}
		public override byte[] GetBytesTemp()
		{
			byte[] data = new byte[Size];
			data[0] = IdComando;
			Word.SetData(data,1, PersonajeAUsar);
			OffsetRom.Set(data,3, new OffsetRom(Movimiento.IdUnicoTemp));
			return data;
		}
	}
}
