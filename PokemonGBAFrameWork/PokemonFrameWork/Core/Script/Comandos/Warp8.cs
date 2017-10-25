/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of Warp8.
 /// </summary>
 public class Warp8:Comando
 {
  public const byte ID=0xE0;
  public const int SIZE=8;
  Byte bancoAIr;
 Byte mapaAIr;
 Byte salidaAIr;
 short coordenadaX;
 short coordenadaY;
 
  public Warp8(Byte bancoAIr,Byte mapaAIr,Byte salidaAIr,short coordenadaX,short coordenadaY) 
  {
   BancoAIr=bancoAIr;
 MapaAIr=mapaAIr;
 SalidaAIr=salidaAIr;
 CoordenadaX=coordenadaX;
 CoordenadaY=coordenadaY;
 
  }
   
  public Warp8(RomGba rom,int offset):base(rom,offset)
  {
  }
  public Warp8(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe Warp8(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "bajo investigación.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "Warp8";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         public Byte BancoAIr
{
get{ return bancoAIr;}
set{bancoAIr=value;}
}
 public Byte MapaAIr
{
get{ return mapaAIr;}
set{mapaAIr=value;}
}
 public Byte SalidaAIr
{
get{ return salidaAIr;}
set{salidaAIr=value;}
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
 
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{bancoAIr,mapaAIr,salidaAIr,coordenadaX,coordenadaY};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   bancoAIr=*(ptrRom+offsetComando);
 offsetComando++;
 mapaAIr=*(ptrRom+offsetComando);
 offsetComando++;
 salidaAIr=*(ptrRom+offsetComando);
 offsetComando++;
 coordenadaX=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 coordenadaY=Word.GetWord(ptrRom,offsetComando);
 offsetComando+=Word.LENGTH;
 
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   *ptrRomPosicionado=bancoAIr;
 ++ptrRomPosicionado; 
 *ptrRomPosicionado=mapaAIr;
 ++ptrRomPosicionado; 
 *ptrRomPosicionado=salidaAIr;
 ++ptrRomPosicionado; 
 Word.SetWord(ptrRomPosicionado,CoordenadaX);
 ptrRomPosicionado+=Word.LENGTH;
 Word.SetWord(ptrRomPosicionado,CoordenadaY);
 ptrRomPosicionado+=Word.LENGTH;
 
  }
 }
}