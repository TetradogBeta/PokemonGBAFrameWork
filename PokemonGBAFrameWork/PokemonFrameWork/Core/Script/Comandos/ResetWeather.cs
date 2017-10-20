/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
 /// <summary>
 /// Description of ResetWeather.
 /// </summary>
 public class ResetWeather:Comando
 {
  public const byte ID=0xA3;
  public const int SIZE=1;
  
  public ResetWeather() 
  {
   
  }
   
  public ResetWeather(RomGba rom,int offset):base(rom,offset)
  {
  }
  public ResetWeather(byte[] bytesScript,int offset):base(bytesScript,offset)
  {}
  public unsafe ResetWeather(byte* ptRom,int offset):base(ptRom,offset)
  {}
  public override string Descripcion {
   get {
    return "Prepara la desaparición del tiempo al tiempo por defecto.";
   }
  }

  public override byte IdComando {
   get {
    return ID;
   }
  }
  public override string Nombre {
   get {
    return "ResetWeather";
   }
  }
  public override int Size {
   get {
    return SIZE;
   }
  }
                         
  protected override System.Collections.Generic.IList<object> GetParams()
  {
   return new Object[]{};
  }
  protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
  {
   
  }
  protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
  {
    base.SetComando(ptrRomPosicionado,parametrosExtra);
   
  }
 }
}
