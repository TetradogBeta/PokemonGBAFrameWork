﻿/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 10/03/2017
 * Time: 10:33
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace PokemonGBAFrameWork
{
    public enum Idioma
    {
        Español = 'S',
        Ingles = 'E'
    }
    public enum AbreviacionCanon : ulong
    {
        /// <summary>
        ///Abreviación Rubi
        /// </summary>
        AXV,
        /// <summary>
        ///Abreviación Zafiro
        /// </summary>
        AXP,
        /// <summary>
        ///Abreviación Esmeralda
        /// </summary>
        BPE,
        /// <summary>
        ///Abreviación Rojo Fuego
        /// </summary>
        BPR,
        /// <summary>
        ///Abreviación Verde Hoja
        /// </summary>
        BPG

    }

    /// <summary>
    /// Description of EdicionPokemon.
    /// </summary>
    public class EdicionPokemon : Edicion, IComparable
    {
        //Ediciones canon usa
        public static readonly EdicionPokemon RubiUsa10 = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "AXV", "POKEMON RUBY"), Idioma.Ingles, AbreviacionCanon.AXV,CompilacionPokemon.Compilaciones[0]);
        public static readonly EdicionPokemon RubiUsa11 = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "AXV", "POKEMON RUBY"), Idioma.Ingles, AbreviacionCanon.AXV, CompilacionPokemon.Compilaciones[1]);
        public static readonly EdicionPokemon RubiUsa12 = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "AXV", "POKEMON RUBY"), Idioma.Ingles, AbreviacionCanon.AXV, CompilacionPokemon.Compilaciones[2]);


        public static readonly EdicionPokemon ZafiroUsa10 = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "AXP", "POKEMON SAPP"), Idioma.Ingles, AbreviacionCanon.AXP, CompilacionPokemon.Compilaciones[0]);
        public static readonly EdicionPokemon ZafiroUsa11 = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "AXP", "POKEMON SAPP"), Idioma.Ingles, AbreviacionCanon.AXP, CompilacionPokemon.Compilaciones[1]);
        public static readonly EdicionPokemon ZafiroUsa12 = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "AXP", "POKEMON SAPP"), Idioma.Ingles, AbreviacionCanon.AXP, CompilacionPokemon.Compilaciones[2]);
        public static readonly EdicionPokemon EsmeraldaUsa10 = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "BPE", "POKEMON EMER"), Idioma.Ingles, AbreviacionCanon.BPE, CompilacionPokemon.Compilaciones[0]);
        public static readonly EdicionPokemon RojoFuegoUsa10 = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "BPR", "POKEMON FIRE"), Idioma.Ingles, AbreviacionCanon.BPR, CompilacionPokemon.Compilaciones[0]);
        public static readonly EdicionPokemon RojoFuegoUsa11 = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "BPR", "POKEMON FIRE"), Idioma.Ingles, AbreviacionCanon.BPR, CompilacionPokemon.Compilaciones[1]);

        public static readonly EdicionPokemon VerdeHojaUsa10 = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "BPG", "POKEMON LEAF"), Idioma.Ingles, AbreviacionCanon.BPG, CompilacionPokemon.Compilaciones[0]);
        public static readonly EdicionPokemon VerdeHojaUsa11 = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "BPG", "POKEMON LEAF"), Idioma.Ingles, AbreviacionCanon.BPG, CompilacionPokemon.Compilaciones[1]);

        //Ediciones canon esp
        public static readonly EdicionPokemon RubiEsp10 = new EdicionPokemon(new Edicion((char)Idioma.Español, "AXV", "POKEMON RUBY"), Idioma.Español, AbreviacionCanon.AXV, CompilacionPokemon.Compilaciones[0]);
        public static readonly EdicionPokemon ZafiroEsp10 = new EdicionPokemon(new Edicion((char)Idioma.Español, "AXP", "POKEMON SAPP"), Idioma.Español, AbreviacionCanon.AXP, CompilacionPokemon.Compilaciones[0]);
        public static readonly EdicionPokemon EsmeraldaEsp10 = new EdicionPokemon(new Edicion((char)Idioma.Español, "BPE", "POKEMON EMER"), Idioma.Español, AbreviacionCanon.BPE, CompilacionPokemon.Compilaciones[0]);
        public static readonly EdicionPokemon RojoFuegoEsp10 = new EdicionPokemon(new Edicion((char)Idioma.Español, "BPR", "POKEMON FIRE"), Idioma.Español, AbreviacionCanon.BPR, CompilacionPokemon.Compilaciones[0]);
        public static readonly EdicionPokemon VerdeHojaEsp10 = new EdicionPokemon(new Edicion((char)Idioma.Español, "BPG", "POKEMON LEAF"), Idioma.Español, AbreviacionCanon.BPG, CompilacionPokemon.Compilaciones[0]);
        //todas las edicionesCanon
        public static readonly EdicionPokemon[] EdicionesCanon = new EdicionPokemon[] {
            RubiUsa10,
            RubiUsa11,
            RubiUsa12,
            ZafiroUsa10,
            ZafiroUsa11,
            ZafiroUsa12,
            EsmeraldaUsa10,
            RojoFuegoUsa10,
            RojoFuegoUsa11,
            VerdeHojaUsa10,
            VerdeHojaUsa11,
            RubiEsp10,
            ZafiroEsp10,
            EsmeraldaEsp10,
            RojoFuegoEsp10,
            VerdeHojaEsp10
        };
        public static readonly ulong[] IdsEdicionesCanon = new ulong[] {
            RubiUsa10.Id,
            RubiUsa11.Id,
            RubiUsa12.Id,
            ZafiroUsa10.Id,
            ZafiroUsa11.Id,
            ZafiroUsa12.Id,
            EsmeraldaUsa10.Id,
            RojoFuegoUsa10.Id,
            RojoFuegoUsa11.Id,
            VerdeHojaUsa10.Id,
            VerdeHojaUsa11.Id,
            RubiEsp10.Id,
            ZafiroEsp10.Id,
            EsmeraldaEsp10.Id,
            RojoFuegoEsp10.Id,
            VerdeHojaEsp10.Id
        };
        Idioma idioma;
        AbreviacionCanon abreviacionCanon;

        private EdicionPokemon(Edicion edicion)
            : base(edicion.InicialIdioma, edicion.Abreviacion, edicion.NombreCompleto)
        {
        }
        private EdicionPokemon(Edicion edicion, Idioma idioma, AbreviacionCanon abreviacionRomCanon, CompilacionPokemon compilacion = null)
            : base(edicion.InicialIdioma, edicion.Abreviacion, edicion.NombreCompleto)
        {
            Idioma = idioma;
            AbreviacionRom = abreviacionRomCanon;
            Compilacion = compilacion;
            if (compilacion != null)
                Id = GetId(this);
        }
        #region Propiedades
        public Idioma Idioma
        {
            get
            {
                return idioma;
            }
            private set { idioma = value; }
        }
        public AbreviacionCanon AbreviacionRom
        {
            get
            {
                return abreviacionCanon;
            }
            private set { abreviacionCanon = value; }
        }
        public bool RegionKanto
        {
            get { return this.AbreviacionRom == AbreviacionCanon.BPG || this.AbreviacionRom == AbreviacionCanon.BPR; }
        }
        public bool RegionHoenn
        {
            get { return !RegionKanto; }
        }
        public bool EstaEnEspañol
        {
            get { return Idioma == Idioma.Español; }
        }
        public bool EstaEnIngles
        {
            get { return Idioma == Idioma.Ingles; }
        }
        public bool EsZafiro
        {
            get { return AbreviacionRom == AbreviacionCanon.AXP; }
        }
        public bool EsRubi
        {
            get { return AbreviacionRom == AbreviacionCanon.AXV; }
        }
        public bool EsRojoFuego
        {
            get { return AbreviacionRom == AbreviacionCanon.BPR; }
        }
        public bool EsVerdeHoja
        {
            get { return AbreviacionRom == AbreviacionCanon.BPG; }
        }
        public bool EsEsmeralda
        {
            get { return AbreviacionRom == AbreviacionCanon.BPE; }
        }

        public bool EstaModificada
        {
            get { return (char)idioma != InicialIdioma || Abreviacion != AbreviacionRom.ToString(); }
        }

        public bool EsRubiOZafiro { get { return EsRubi || EsZafiro; } }
        #endregion
        #region Overrides
        public override bool Equals(object obj)
        {
            EdicionPokemon other = obj as EdicionPokemon;
            bool equals = other != null;
            if (equals)
                equals = this.idioma == other.idioma && this.abreviacionCanon == other.abreviacionCanon;
            return equals;
        }
        public override bool Compatible(Edicion edicion)
        {
            EdicionPokemon edicionPokemon = edicion as EdicionPokemon;
            bool compatible = edicionPokemon != null;
            if (compatible)
            {
                switch (AbreviacionRom)
                {
                    case AbreviacionCanon.AXV:
                    case AbreviacionCanon.AXP:
                        compatible = edicionPokemon.EsRubiOZafiro;
                        break;
                    case AbreviacionCanon.BPE:
                        compatible = AbreviacionRom == edicionPokemon.AbreviacionRom;
                        break;
                    case AbreviacionCanon.BPR:
                    case AbreviacionCanon.BPG:
                        compatible = edicionPokemon.RegionKanto;
                        break;
                }
            }
            return compatible;
        }
        #endregion
        public int CompareTo(object obj)
        {
            return ICompareTo(obj as Edicion);
        }
        protected override int ICompareTo(Edicion other)
        {
            EdicionPokemon edicion = other as EdicionPokemon;
            int compareTo;
            if (edicion != null)
            {
                compareTo = idioma.CompareTo(edicion.idioma);
                if (compareTo == (int)Gabriel.Cat.S.Utilitats.CompareTo.Iguals)
                    compareTo = abreviacionCanon.CompareTo(edicion.abreviacionCanon);

            }
            else compareTo = (int)Gabriel.Cat.S.Utilitats.CompareTo.Iguals;
            return compareTo;
        }
        public static EdicionPokemon GetEdicionPokemon(RomGba rom)
        {
            EdicionPokemon edicionPokemon = new EdicionPokemon(rom.Edicion);
            bool edicionValida;
            AbreviacionCanon abreviacionRom;
            edicionPokemon.Idioma = (Idioma)edicionPokemon.InicialIdioma;
            edicionValida = Enum.TryParse(edicionPokemon.Abreviacion, out abreviacionRom);
            edicionPokemon.AbreviacionRom = abreviacionRom;
            //compruebo que este bien
            if (edicionValida && edicionPokemon.Idioma == Idioma.Español || edicionPokemon.Idioma == Idioma.Ingles)
            {
                edicionValida = ValidaEdicion(rom, edicionPokemon);
            }
            else
                edicionValida = false;

            if (!edicionValida)
            {
                //si esta mal corrijo los campos Idioma y AbreviacionRom

                for (int i = 0; !edicionValida && i < EdicionesCanon.Length; i++)
                {
                    if (ValidaEdicion(rom, EdicionesCanon[i]))
                    { //tengo que saber que edicion y que idioma es
                        edicionPokemon.Idioma = EdicionesCanon[i].Idioma;
                        edicionPokemon.AbreviacionRom = EdicionesCanon[i].AbreviacionRom;
                        edicionValida = true;
                    }
                    //si no es una edicion canon es que ha sido muy modificada y no se leerla
                    if (!edicionValida)
                        throw new FormatoRomNoReconocidoException();

                }
            }
            edicionPokemon.Compilacion = CompilacionPokemon.GetCompilacion(rom, edicionPokemon);
            //pongo el id
            edicionPokemon.Id = GetId(edicionPokemon);
            return edicionPokemon;
        }

        public static ulong GetId(EdicionPokemon edicionPokemon)
        {
            ulong id = (ulong)edicionPokemon.AbreviacionRom * 100;
            id += (ulong)edicionPokemon.Compilacion.Version * 10;
            id += (ulong)edicionPokemon.Compilacion.SubVersion;
            return id;
        }

        static bool ValidaEdicion(RomGba rom, EdicionPokemon edicionPokemon)
        {
            bool valida = false;
            //tengo que encontrar si es verdad que sea su edicion...
            //diferenciar idioma,edicion
            try
            {
                switch (edicionPokemon.AbreviacionRom)
                {
                    case AbreviacionCanon.AXV:
                    case AbreviacionCanon.AXP:
                        valida = Zona.GetOffsetRom(AtaqueCompleto.ZonaAnimacion, rom, edicionPokemon, CompilacionPokemon.Compilaciones[0]).IsAPointer;
                        if (!valida)
                            valida = Zona.GetOffsetRom(AtaqueCompleto.ZonaAnimacion, rom, edicionPokemon, CompilacionPokemon.Compilaciones[1]).IsAPointer;
                        break;
                    case AbreviacionCanon.BPE:
                    case AbreviacionCanon.BPR:
                    case AbreviacionCanon.BPG:
                        valida = Zona.GetOffsetRom(Pokemon.Descripcion.ZonaDescripcion, rom, edicionPokemon, CompilacionPokemon.Compilaciones[0]).IsAPointer;
                        if (!valida && edicionPokemon.RegionKanto)
                            valida = Zona.GetOffsetRom(Pokemon.Descripcion.ZonaDescripcion, rom, edicionPokemon, CompilacionPokemon.Compilaciones[1]).IsAPointer;
                        break;

                }

            }
            catch (Exception ex)
            {
            }
            return valida;

        }
    }
}
