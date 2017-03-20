﻿/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 10/03/2017
 * Time: 7:34
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Gabriel.Cat;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Edicion.
	/// </summary>
	public class Edicion
	{
		enum Variables
		{
			Idioma = Abreviacion + LongitudCampos.Abreviacion,
			Abreviacion=172,
			NombreCompleto=160
		}
		public enum LongitudCampos
		{
			Idioma = 1,
			Abreviacion = 3,
			NombreCompleto = 12
		}
		
		char idioma;
		string abreviacion;
		string nombreCompleto;
		
		public Edicion(char idioma,string abreviacion,string nombreCompleto)
		{
			InicialIdioma=idioma;
			Abreviacion=abreviacion;
			NombreCompleto=nombreCompleto;
		}
		private Edicion()
		{}

		public char InicialIdioma {
			get {
				
				return idioma;
			}
			set {
				
				idioma = value;
			}
		}

		public string Abreviacion {
			get {
				return abreviacion;
			}
			set {
				
				if(String.IsNullOrEmpty(value))
					value="GBA";
				else if(value.Length>(int)LongitudCampos.Abreviacion)
					value=value.Substring(0,(int)LongitudCampos.Abreviacion);
				
				abreviacion = value;
				
			}
		}

		public string NombreCompleto {
			get {
				return nombreCompleto;
			}
			set {
				
				if(String.IsNullOrEmpty(value))
					value="Rom GBA";
				else if(value.Length>(int)LongitudCampos.NombreCompleto)
					value=value.Substring(0,(int)LongitudCampos.NombreCompleto);
				
				nombreCompleto = value;
			}
		}
		public Edicion Clone()
		{
			Edicion clon=new Edicion();
			clon.InicialIdioma=InicialIdioma;
			clon.Abreviacion=Abreviacion;
			clon.NombreCompleto=NombreCompleto;
			return clon;
		}
		public override string ToString()
		{
			return string.Format("[Edicion NombreCompleto={0}]", nombreCompleto);
		}
		#region Equals and GetHashCode implementation
		public override bool Equals(object obj)
		{
			Edicion other = obj as Edicion;
			bool equals=other!=null;
			if (equals)
				equals= this.idioma == other.idioma && this.abreviacion == other.abreviacion && this.nombreCompleto == other.nombreCompleto;
			return equals;
		}

		public override int GetHashCode()
		{
			int hashCode = 0;
			unchecked {
				hashCode += 1000000007 * idioma.GetHashCode();
				if (abreviacion != null)
					hashCode += 1000000009 * abreviacion.GetHashCode();
				if (nombreCompleto != null)
					hashCode += 1000000021 * nombreCompleto.GetHashCode();
			}
			return hashCode;
		}

		public static bool operator ==(Edicion lhs, Edicion rhs) {
			bool equals;
			if (ReferenceEquals(lhs, rhs))
				equals= true;
			if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
				equals= false;
			else equals= lhs.Equals(rhs);
			return equals;
		}

		public static bool operator !=(Edicion lhs, Edicion rhs) {
			return !(lhs == rhs);
		}

		#endregion
		public static Edicion GetEdicion(RomGba rom)
		{
			if(rom==null)
				throw new ArgumentNullException();
			
			Edicion edicion=new Edicion();
			edicion.InicialIdioma=(char)rom[(int)Variables.Idioma];
			edicion.Abreviacion= System.Text.Encoding.ASCII.GetString(rom.Data.SubArray((int)Variables.Abreviacion,(int)LongitudCampos.Abreviacion));
			edicion.NombreCompleto=System.Text.Encoding.ASCII.GetString(rom.Data.SubArray((int)Variables.NombreCompleto,(int)LongitudCampos.NombreCompleto));
			
			return edicion;
		}
		public static void SetEdicion(RomGba rom,Edicion edicion)
		{
			if(rom==null||edicion==null)
				throw new ArgumentNullException();
			
			rom[(int)Variables.Idioma]=(byte)edicion.InicialIdioma;
			rom.Data.SetArray((int)Variables.Abreviacion,System.Text.Encoding.UTF8.GetBytes(edicion.Abreviacion.PadRight((int)LongitudCampos.Abreviacion)));
			rom.Data.SetArray((int)Variables.NombreCompleto,System.Text.Encoding.UTF8.GetBytes(edicion.NombreCompleto.PadRight((int)LongitudCampos.NombreCompleto)));
		}
	}
}
