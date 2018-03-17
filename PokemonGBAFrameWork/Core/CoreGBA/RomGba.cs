﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PokemonGBAFrameWork
{
    /// <summary>
    /// Es la rom cargada en la ram
    /// </summary>
    public class RomGba 
    {
        public const string EXTENSION = ".gba";

        string path;
        Edicion edicion;
        BloqueBytes romData;
        string nombre;
        public event EventHandler UnLoaded;
        #region Constructores
        public RomGba(string pathRom) : this(new FileInfo(pathRom))
        {


        }
        public RomGba(FileInfo romFile)
        {


            if (romFile == null)
                throw new ArgumentNullException();

            nombre = System.IO.Path.GetFileNameWithoutExtension(romFile.FullName);
            path = romFile.FullName.Substring(0, romFile.FullName.Length - System.IO.Path.GetFileName(romFile.FullName).Length);

        }
        private RomGba()
        { }
        #endregion
        #region propiedades
        public Edicion Edicion
        {
            get
            {
                //no se porque si pongo if(edicion==null) pasa del if olimpicamente...
                this.edicion = Edicion.GetEdicion(this);
                return this.edicion;
            }
        }

        public BloqueBytes Data
        {
            get
            {
                if (romData == null)
                    Load();
                return romData;
            }
        }

        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                string pathAnterior = FullPath;

                if (Data != null)
                {
                    if (String.IsNullOrEmpty(value))//null or ""
                        nombre = "Hack " + edicion.NombreCompleto;
                    else nombre = value;
                }
                System.IO.File.Move(pathAnterior, FullPath);
            }
        }

        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                if (!Directory.Exists(value))
                    throw new ArgumentException();
                path = value;
            }
        }

        public string FullPath
        {
            get
            {
                string nombreArchivoConExtension = Nombre + EXTENSION;
                return System.IO.Path.Combine(path, nombreArchivoConExtension);
            }
        }
        public byte this[int index]
        {
            get { return Data[index]; }
            set { Data[index] = value; }
        }
        #endregion

        #region Metodos

        /// <summary>
        /// Pone la edicion en los datos en memoria
        /// </summary>
        public void SaveEdicion()
        {
            Edicion.SetEdicion(this, Edicion);
        }
        public void LoadEdicion()
        {
            edicion = null;//asi cuando la lean por la propiedad la cargaran...
        }
        public void Save()
        {
            if (File.Exists(FullPath))
                File.Delete(FullPath);

            SaveEdicion();
            Data.Bytes.Save(FullPath);
        }
        public void Load()
        {
            romData = new BloqueBytes(File.ReadAllBytes(FullPath));
        }
        /// <summary>
        /// Descarga los datos de la memoria ram
        /// </summary>
        public void UnLoad()
        {
            romData = null;
            edicion = null;
            if (UnLoaded != null)
                UnLoaded(this, new EventArgs());
        }
        /// <summary>
        /// Crea una backup de los datos en memoria
        /// </summary>
        /// <returns>Ruta backup</returns>
        public string BackUp()
        {
            string path = Path + "BackUp." + DateTime.Now.Ticks + "." + Nombre + EXTENSION;
            File.WriteAllBytes(path, Data.Bytes);
            return path;

        }
        /// <summary>
        /// Devuelve una copia de la rom
        /// </summary>
        /// <returns></returns>
        public RomGba Clone()
        {
            RomGba rom = new RomGba();
            rom.Path = Path;
            rom.Nombre = Nombre;
            rom.edicion = this.Edicion.Clone();
            rom.romData = rom.Data.Clon();
            return rom;
        }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return string.Format("[RomGba Nombre={0}]", Nombre);
        }

        #endregion
    }
}