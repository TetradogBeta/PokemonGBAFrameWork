/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of PokeMart.
 /// </summary>
 public class PokeMart:Comando
 {
  public const byte ID=0x86;
  public const int SIZE=5;
  OffsetRom listaObjetos;
 
  public PokeMart(OffsetRom listaObjetos) 
  {
   ListaObjetos=listaObjetos;
 
  }
   
  public PokeMart(RomGba rom,int offset):base(rom,offset)
  {
  }
  public PokeMart(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe PokeMart(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Abre la tienda pokemon con la lista de objetos/precios especificada.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "PokeMart";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public OffsetRom ListaObjetos
{
get{ return listaObjetos;}
set{listaObjetos=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{listaObjetos.Offset};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   listaObjetos=new OffsetRom(ptrRom,new OffsetRom(ptrRom,offsetComando).Offset);
 offsetComando+=OffsetRom.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   OffsetRom.SetOffset(ptrRomPosicionado,listaObjetos);
 ptrRomPosicionado+=OffsetRom.LENGTH;
 
  }
 }
}
