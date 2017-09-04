/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of RemoveDecoration.
 /// </summary>
 public class RemoveDecoration:Comando
 {
  public const byte ID=0x4C;
  public const int SIZE=3;
  short decoracion;
 
  public RemoveDecoration(short decoracion) 
  {
   Decoracion=decoracion;
 
  }
   
  public RemoveDecoration(RomGba rom,int offset):base(rom,offset)
  {
  }
  public RemoveDecoration(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe RemoveDecoration(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Quita del pc del player la cantidad del objetodo decorativo";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "RemoveDecoration";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public short Decoracion
{
get{ return decoracion;}
set{decoracion=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{decoracion};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   decoracion=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   Word.SetWord(ptrRomPosicionado,Decoracion);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
