/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of SetCatchLocation.
 /// </summary>
 public class SetCatchLocation:Comando
 {
  public const byte ID=0xD2;
  public const int SIZE=4;
  Word pokemon;
 Word catchLocation;
 
  public SetCatchLocation(Word pokemon,Word catchLocation) 
  {
   Pokemon=pokemon;
 CatchLocation=catchLocation;
 
  }
   
  public SetCatchLocation(RomGba rom,int offset):base(rom,offset)
  {
  }
  public SetCatchLocation(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe SetCatchLocation(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Changes the catch location for a specified Pokémon in player's party.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "SetCatchLocation";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public Word Pokemon
{
get{ return pokemon;}
set{pokemon=value;}
}
 public Word CatchLocation
{
get{ return catchLocation;}
set{catchLocation=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{pokemon,catchLocation};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   pokemon=new Word(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 catchLocation=new Word(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   Word.SetWord(ptrRomPosicionado,Pokemon);
 ptrRomPosicionado+=Word.LENGTH;
 Word.SetWord(ptrRomPosicionado,CatchLocation);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}
