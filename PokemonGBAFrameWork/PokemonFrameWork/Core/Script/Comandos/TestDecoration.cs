/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of TestDecoration.
 /// </summary>
 public class TestDecoration:Comando
 {
  public const byte ID=0x4D;
  public const int SIZE=3;
  short decoracion;
 
  public TestDecoration(short decoracion) 
  {
   Decoracion=decoracion;
 
  }
   
  public TestDecoration(RomGba rom,int offset):base(rom,offset)
  {
  }
  public TestDecoration(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe TestDecoration(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Prueba un objeto decorativo especifico para ver si hay espacio sufieciente para almacenarla";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "TestDecoration";
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
