/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of BufferNumber.
 /// </summary>
 public class BufferNumber:Comando
 {
  public const byte ID=0x83;
  public const int SIZE=4;
  Byte buffer;
 short variableToStore;
 
  public BufferNumber(Byte buffer,short variableToStore) 
  {
   Buffer=buffer;
 VariableToStore=variableToStore;
 
  }
   
  public BufferNumber(RomGba rom,int offset):base(rom,offset)
  {
  }
  public BufferNumber(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe BufferNumber(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Variable version on buffernumber.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "BufferNumber";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public Byte Buffer
{
get{ return buffer;}
set{buffer=value;}
}
 public short VariableToStore
{
get{ return variableToStore;}
set{variableToStore=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{buffer,variableToStore};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   buffer=*(ptrRom+offsetComando);
 offsetComando++;
 variableToStore=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   *ptrRomPosicionado=buffer;
 ++ptrRomPosicionado; 
 Word.SetWord(ptrRomPosicionado,VariableToStore);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
