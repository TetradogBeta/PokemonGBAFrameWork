/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of VirtualCall.
 /// </summary>
 public class VirtualCall:Comando
 {
  public const byte ID=0xBA;
  public const int SIZE=5;
  OffsetRom funcionPersonalizada;
 
  public VirtualCall(OffsetRom funcionPersonalizada) 
  {
   FuncionPersonalizada=funcionPersonalizada;
 
  }
   
  public VirtualCall(RomGba rom,int offset):base(rom,offset)
  {
  }
  public VirtualCall(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe VirtualCall(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Llama a la función.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "VirtualCall";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public OffsetRom FuncionPersonalizada
{
get{ return funcionPersonalizada;}
set{funcionPersonalizada=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{funcionPersonalizada.Offset};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   funcionPersonalizada=new OffsetRom(ptrRom,new OffsetRom(ptrRom,offsetComando).Offset);
 offsetComando+=OffsetRom.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   OffsetRom.SetOffset(ptrRomPosicionado,funcionPersonalizada);
 ptrRomPosicionado+=OffsetRom.LENGTH;
 
  }
 }
}