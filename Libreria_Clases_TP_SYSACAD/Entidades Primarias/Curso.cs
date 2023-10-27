﻿using Libreria_Clases_TP_SYSACAD.EntidadesSecundarias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Libreria_Clases_TP_SYSACAD.EntidadesPrimarias
{
    public class Curso
    {
        //Atributos que debe contener todo curso
        private string _nombre;
        private string _codigo;
        private string _descripcion;
        private int _cupoMaximo;
        private int _cupoDisponible;
        private string _turno;
        private string _aula;
        private string _dia;
        private Carrera _carrera;
        private List<string> _codigosCorrelatividades = new List<string>();
        private int _creditosRequeridos = 0;
        private double _promedioRequerido = 0;
        private string _codigoFamilia;
        private Dictionary<string, DateTime> _alumnosEnListaDeEspera = new Dictionary<string, DateTime>();

        public Curso(string nombre, string codigo, string descripcion, int cupoMaximo, string turno, string aula,
            string dia, Carrera carrera)
        {
            _nombre = nombre;
            _codigo = codigo;
            _descripcion = descripcion;
            _cupoMaximo = cupoMaximo;
            _cupoDisponible = cupoMaximo;
            _turno = turno;
            _aula = aula;
            _dia = dia;
            _carrera = carrera;
            _codigoFamilia = ObtenerCodigoDeFamilia();
        }

        public string ObtenerCodigoDeFamilia()
        {
            string codigoDeFamilia = _nombre.Trim().ToUpper();
            codigoDeFamilia = codigoDeFamilia.Replace(" ", "");

            return codigoDeFamilia;
        }

        /// <summary>
        /// Registra un nuevo curso en la base de datos.
        /// </summary>
        /// <param name="nuevoCurso">El curso a ser registrado.</param>
        public void RegistrarCurso()
        {
            Sistema.BaseDatosCursos.IngresarCursoBD(this);
        }

        /// <summary>
        /// Verifica si hay cupos disponibles en el curso.
        /// </summary>
        /// <returns>True si hay cupos disponibles, False si no.</returns>
        internal bool ChequearCuposDisponibles()
        {
            if (CupoDisponible > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Devuelve el horario del curso en formato de texto.
        /// </summary>
        /// <returns>El horario del curso.</returns>
        public string DevolverHorario()
        {
            string horarioADevolver = "";

            switch (_turno)
            {
                case "Mañana":
                    horarioADevolver = "8:00 - 12:00 hs";
                    break;
                case "Tarde":
                    horarioADevolver = "14:00 - 18:00 hs";
                    break;
                case "Noche":
                    horarioADevolver = "18:30 - 22:30 hs";
                    break;
            }

            return horarioADevolver;
        }

        /// <summary>
        /// Sobrecarga del operador '-' para reducir el cupo disponible del curso.
        /// </summary>
        /// <param name="curso">El curso al que se reduce el cupo.</param>
        /// <param name="numero">La cantidad a restar al cupo disponible.</param>
        /// <returns>El curso con el cupo actualizado.</returns>
        public static Curso operator -(Curso curso, int numero)
        {
            curso.CupoDisponible -= numero;
            return curso;
        }

        //Setters y getters
        public string Nombre { get { return _nombre; } internal set { _nombre = value; } }

        public string Codigo { get { return _codigo; } internal set { _codigo = value; } }

        public string Descripcion { get { return _descripcion; } internal set { _descripcion = value; } }

        public int CupoMaximo { get { return _cupoMaximo; } internal set { _cupoMaximo = value; } }

        public int CupoDisponible { get { return _cupoDisponible; } set { _cupoDisponible = value; } }

        public string Turno { get { return _turno; } internal set { _turno = value; } }

        public string Aula { get { return _aula; } internal set { _aula = value; } }

        public string Dia { get { return _dia; } internal set { _dia = value; } }

        public Carrera Carrera { get { return _carrera; } }

        public List<string> Correlatividades { get { return _codigosCorrelatividades; } internal set { _codigosCorrelatividades = value; } }

        public int CreditosRequeridos { get { return _creditosRequeridos; } set { _creditosRequeridos = value; } }

        public double PromedioRequerido { get { return _promedioRequerido; } set { _promedioRequerido = value; } }

        public string CodigoFamilia { get { return _codigoFamilia; } }

        public Dictionary<string, DateTime> ListaDeEspera { get { return _alumnosEnListaDeEspera; } internal set { _alumnosEnListaDeEspera = value; } }

    }
}