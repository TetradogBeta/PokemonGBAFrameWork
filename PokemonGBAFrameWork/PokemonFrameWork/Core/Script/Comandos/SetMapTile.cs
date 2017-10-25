/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of SetMapTile.
 /// </summary>
 public class SetMapTile:Comando
 {
  public const byte ID=0xA2;
  public const int SIZE=9;
  short coordenadaX;
 short coordenadaY;
 short tile;
 short atributoTile;
 
  public SetMapTile(short coordenadaX,short coordenadaY,short tile,short atributoTile) 
  {
   CoordenadaX=coordenadaX;
 CoordenadaY=coordenadaY;
 Tile=tile;
 AtributoTile=atributoTile;
 
  }
   
  public SetMapTile(RomGba rom,int offset):base(rom,offset)
  {
  }
  public SetMapTile(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe SetMapTile(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Pone la tile en el mapa. De alguna manera tienes que refescar esa parte.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "SetMapTile";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public short CoordenadaX
{
get{ return coordenadaX;}
set{coordenadaX=value;}
}
 public short CoordenadaY
{
get{ return coordenadaY;}
set{coordenadaY=value;}
}
 public short Tile
{
get{ return tile;}
set{tile=value;}
}
 public short AtributoTile
{
get{ return atributoTile;}
set{atributoTile=value;}
}
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{coordenadaX,coordenadaY,tile,atributoTile};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   coordenadaX=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 coordenadaY=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 tile=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 atributoTile=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   Word.SetWord(ptrRomPosicionado,CoordenadaX);
 ptrRomPosicionado+=Word.LENGTH;
 Word.SetWord(ptrRomPosicionado,CoordenadaY);
 ptrRomPosicionado+=Word.LENGTH;
 Word.SetWord(ptrRomPosicionado,Tile);
 ptrRomPosicionado+=Word.LENGTH;
 Word.SetWord(ptrRomPosicionado,AtributoTile);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}