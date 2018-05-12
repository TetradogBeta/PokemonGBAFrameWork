﻿/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 11/03/2017
 * Time: 5:56
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of DescripcionPokedex.
	/// </summary>
	public class DescripcionPokedex
	{
		public enum LongitudCampos
		{
			TotalGeneral = 36,
			TotalEsmeralda = 32,
			NombreEspecie = 12,
			PaginasRubiZafiro = 2,
			PaginasGeneral = 1,
		}
		
		public static readonly Zona ZonaDescripcion;
		
		BloqueString blEspecie;
		BloqueString blDescripcion;//en el set si es Rubi o Zafiro se divide en dos paginas
		Word peso;
		Word altura;
		Word escalaPokemon;
		Word escalaEntrenador;
		//datos que desconozco
		Word numero;
		Word direccionPokemon;
		Word direccionEntrenador;
		Word numero2;
		
		static DescripcionPokedex()
		{
			ZonaDescripcion=new Zona("DescripcionPokedex");
			
			ZonaDescripcion.Add(EdicionPokemon.EsmeraldaEsp, 0xBFA48);
			ZonaDescripcion.Add(EdicionPokemon.EsmeraldaUsa, 0xBFA20);
			ZonaDescripcion.Add(EdicionPokemon.RojoFuegoEsp, 0x88FEC);
			ZonaDescripcion.Add(EdicionPokemon.RojoFuegoUsa, 0x88E34, 0x88E48);
			ZonaDescripcion.Add(EdicionPokemon.VerdeHojaEsp, 0x88FC0);
			ZonaDescripcion.Add(EdicionPokemon.VerdeHojaUsa, 0x88E08, 0x88E1C);
			ZonaDescripcion.Add(EdicionPokemon.RubiEsp, 0x8F998);
			ZonaDescripcion.Add(EdicionPokemon.ZafiroEsp, 0x8F998);
			ZonaDescripcion.Add(EdicionPokemon.RubiUsa, 0x8F508, 0x8F528);
			ZonaDescripcion.Add(EdicionPokemon.ZafiroUsa, 0x8F508, 0x8F528);
			
		}
		public DescripcionPokedex()
		{
			blDescripcion=new BloqueString();
			blEspecie=new BloqueString((int)LongitudCampos.NombreEspecie);
		}
		public string Especie
		{
			get{return blEspecie.Texto;}
			set{blEspecie.Texto=value;}
		}
		public string Descripcion
		{
			get{return blDescripcion.Texto;}
			set{blDescripcion.Texto=value;}
		}
		/// <summary>
		/// Se tiene que dividir entre 10 para obtener la medida en Kg
		/// </summary>
		public Word Peso {
			get {
				return peso;
			}
			set {
				peso = value;
			}
		}
		/// <summary>
		/// Se tiene que dividir entre 10 para obtener la medida en metros
		/// </summary>
		public Word Altura {
			get {
				return altura;
			}
			set {
				altura = value;
			}
		}

		public Word EscalaPokemon {
			get {
				return escalaPokemon;
			}
			set {
				escalaPokemon = value;
			}
		}

		public Word EscalaEntrenador {
			get {
				return escalaEntrenador;
			}
			set {
				escalaEntrenador = value;
			}
		}

		public Word Numero {
			get {
				return numero;
			}
			set {
				numero = value;
			}
		}

		public Word DireccionPokemon {
			get {
				return direccionPokemon;
			}
			set {
				direccionPokemon = value;
			}
		}

		public Word DireccionEntrenador {
			get {
				return direccionEntrenador;
			}
			set {
				direccionEntrenador = value;
			}
		}

		public Word Numero2 {
			get {
				return numero2;
			}
			set {
				numero2 = value;
			}
		}


		public static DescripcionPokedex GetDescripcionPokedex(RomGba rom,EdicionPokemon edicion,int ordenNacionalPokemon)
		{
			int offsetDescripcionPokemon=Zona.GetOffsetRom(ZonaDescripcion,rom, edicion).Offset+ordenNacionalPokemon*LongitudDescripcion(edicion);
			int posicionActual=offsetDescripcionPokemon;
			DescripcionPokedex descripcionPokemon=new DescripcionPokedex();
			descripcionPokemon.blEspecie=BloqueString.GetString(rom,posicionActual,(int)LongitudCampos.NombreEspecie);
			posicionActual+=(int)LongitudCampos.NombreEspecie;
			descripcionPokemon.Altura=new Word(rom,posicionActual);
			posicionActual+=Word.LENGTH;
			descripcionPokemon.Peso=new Word(rom,posicionActual);
			posicionActual+=Word.LENGTH;
			descripcionPokemon.blDescripcion=BloqueString.GetString(rom,new OffsetRom(rom,posicionActual).Offset);
			posicionActual+=OffsetRom.LENGTH;
			if(edicion.AbreviacionRom!=AbreviacionCanon.BPE)
			{//Esmeralda no tiene ese puntero y Rojo y Verde Apuntan a una pagina vacia asi que no hay problema
				descripcionPokemon.blDescripcion.Texto+="\n"+BloqueString.GetString(rom,new OffsetRom(rom,posicionActual).Offset).Texto;
				posicionActual+=OffsetRom.LENGTH;
			}
			descripcionPokemon.Numero=new Word(rom,posicionActual);
			posicionActual+=Word.LENGTH;
			descripcionPokemon.EscalaPokemon=new Word(rom,posicionActual);
			posicionActual+=Word.LENGTH;
			descripcionPokemon.DireccionPokemon=new Word(rom,posicionActual);
			posicionActual+=Word.LENGTH;
			descripcionPokemon.EscalaEntrenador=new Word(rom,posicionActual);
			posicionActual+=Word.LENGTH;
			descripcionPokemon.DireccionEntrenador=new Word(rom,posicionActual);
			posicionActual+=Word.LENGTH;
			descripcionPokemon.Numero2=new Word(rom,posicionActual);
			
			return descripcionPokemon;
			
			
		}

		public static void SetDescripcionPokedex(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,DescripcionPokedex descripcion,int ordenNacionalPokemon)
		{
			int offsetDescripcionPokemon=Zona.GetOffsetRom(ZonaDescripcion, rom, edicion, compilacion).Offset+ordenNacionalPokemon*LongitudDescripcion(edicion);
			int posicionActual=offsetDescripcionPokemon;
			int totalPagina=TotalText(edicion);
			BloqueString.Remove(rom,posicionActual);
			BloqueString.SetString(rom,posicionActual,descripcion.blEspecie);
			posicionActual+=(int)LongitudCampos.NombreEspecie;
			
			Word.SetData(rom,posicionActual,descripcion.Altura);
			posicionActual+=Word.LENGTH;
			Word.SetData(rom,posicionActual,descripcion.Peso);
			posicionActual+=Word.LENGTH;
			
			//pongo las paginas de la pokedex
			try{
			BloqueString.Remove(rom,new OffsetRom(rom,posicionActual).Offset);
			}catch{}
			rom.Data.SetArray(posicionActual,new OffsetRom(BloqueString.SetString(rom,descripcion.Descripcion.Substring(0,totalPagina))).BytesPointer);
			posicionActual+=OffsetRom.LENGTH;
			if(edicion.AbreviacionRom==AbreviacionCanon.AXV||edicion.AbreviacionRom==AbreviacionCanon.AXP)
			{
				try{
				BloqueString.Remove(rom,new OffsetRom(rom,posicionActual).Offset);
				}catch{}
				if(descripcion.Descripcion.Length>totalPagina)
					rom.Data.SetArray(posicionActual,new OffsetRom(BloqueString.SetString(rom,descripcion.Descripcion.Substring(totalPagina))).BytesPointer);
				posicionActual+=OffsetRom.LENGTH;
			}
			
			Word.SetData(rom,posicionActual,descripcion.Numero);
			posicionActual+=Word.LENGTH;
			Word.SetData(rom,posicionActual,descripcion.EscalaPokemon);
			posicionActual+=Word.LENGTH;
			Word.SetData(rom,posicionActual,descripcion.DireccionPokemon);
			posicionActual+=Word.LENGTH;
			Word.SetData(rom,posicionActual,descripcion.EscalaEntrenador);
			posicionActual+=Word.LENGTH;
			Word.SetData(rom,posicionActual,descripcion.DireccionEntrenador);
			posicionActual+=Word.LENGTH;
			Word.SetData(rom,posicionActual,descripcion.Numero2);
		}
		private static int TotalText(EdicionPokemon edicion)
		{
			int total;
			if (edicion.AbreviacionRom != AbreviacionCanon.AXP&&edicion.AbreviacionRom != AbreviacionCanon.AXV)
				total = (int)LongitudCampos.PaginasGeneral;
			else total = (int)LongitudCampos.PaginasRubiZafiro;
			return total;
		}
		public static int LongitudDescripcion(EdicionPokemon edicion)
		{
			int total;
			if (!edicion.EsEsmeralda)
				total = (int)LongitudCampos.TotalGeneral;
			else total = (int)LongitudCampos.TotalEsmeralda;
			return total;
		}

		public static int GetTotalEntradas(RomGba rom, EdicionPokemon edicion)
		{
			int total=0;
			int offsetInicio=Zona.GetOffsetRom(ZonaDescripcion, rom, edicion).Offset;
			while (ValidarIndicePokemon(rom, edicion,offsetInicio, total))
				total+=3;
			while (!ValidarIndicePokemon(rom, edicion,offsetInicio, total))
				total--;

			return total;
		}
		private static bool ValidarOffset(RomGba rom, EdicionPokemon edicion, int offsetInicioDescripcion)
		{
			int offsetValidador;
			bool valido=offsetInicioDescripcion>-1;//si el offset no es valido devuelve -1
			if (valido)
			{
				offsetValidador = offsetInicioDescripcion + (int)LongitudCampos.NombreEspecie + 4/*poner lo que es...*/ ;
				
				valido = new OffsetRom(rom, offsetValidador).IsAPointer;
				if (valido && (edicion.AbreviacionRom == AbreviacionCanon.AXP|| edicion.AbreviacionRom == AbreviacionCanon.AXV))
				{
					offsetValidador += OffsetRom.LENGTH;
					valido = new OffsetRom(rom, offsetValidador).IsAPointer;
				}
			}
			return valido;

		}
		private static bool ValidarIndicePokemon(RomGba rom, EdicionPokemon edicion,int offsetInicio ,int ordenGameFreak)
		{
			return ValidarOffset(rom, edicion,offsetInicio + ordenGameFreak * LongitudDescripcion(edicion));
		}

		public static void Remove(RomGba rom, EdicionPokemon edicion, int ordenNacionalPokemon)
		{
			int offsetDescripcionPokemon=Zona.GetOffsetRom(ZonaDescripcion, rom, edicion).Offset+ordenNacionalPokemon*LongitudDescripcion(edicion);
			int posicionActual=offsetDescripcionPokemon;

			posicionActual+=(int)LongitudCampos.NombreEspecie;
			posicionActual+=Word.LENGTH;
			posicionActual+=Word.LENGTH;
			//borro las paginas de la pokedex
			BloqueString.Remove(rom,new OffsetRom(rom,posicionActual).Offset);
			posicionActual+=OffsetRom.LENGTH;
			if(edicion.AbreviacionRom==AbreviacionCanon.AXV||edicion.AbreviacionRom==AbreviacionCanon.AXP)
			{
				BloqueString.Remove(rom,new OffsetRom(rom,posicionActual).Offset);
			}
			//borro los datos
			rom.Data.Remove(offsetDescripcionPokemon,LongitudDescripcion(edicion));
		}
	}
}
