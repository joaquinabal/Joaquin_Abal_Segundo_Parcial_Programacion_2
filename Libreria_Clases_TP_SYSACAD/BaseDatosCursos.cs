﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria_Clases_TP_SYSACAD
{
    public class BaseDatosCursos
    {
        //Lista que contiene todos los cursos
        private List<Curso> _listaCursos = new List<Curso>();

        /// <summary>
        /// Constructor de la clase BaseDatosCursos.
        /// Inicializa la lista de cursos cargándola desde un archivo JSON.
        /// </summary>
        internal BaseDatosCursos() 
        {
            this._listaCursos = ArchivosJsonCursos.CargarCursosDesdeArchivoJson();
        }

        /// <summary>
        /// Busca un curso en la base de datos por código.
        /// </summary>
        /// <param name="codigo">El código del curso.</param>
        /// <returns>True si se encuentra un curso con el código proporcionado, False si no.</returns>
        internal bool BuscarCursoBD(string codigo)
        {
            bool resultadoBusqueda = false;

            foreach (Curso curso in _listaCursos)
            {
                if (curso.Codigo == codigo)
                {
                    resultadoBusqueda = true;
                }
            }
            return resultadoBusqueda;
        }

        ////////////////////////////////CRUD DEL CURSO://///////////////////////////////////////////////

        /// <summary>
        /// Agrega un nuevo curso a la base de datos.
        /// </summary>
        /// <param name="nuevoCurso">El curso a ser agregado.</param>
        internal void IngresarCursoBD(Curso nuevoCurso)
        {
            try
            {
                _listaCursos.Add(nuevoCurso);
                ArchivosJsonCursos.GuardarArchivoJSON(_listaCursos);
            }
            catch (IOException ex)
            {
                // Manejo específico de excepciones de entrada/salida al guardar el archivo JSON.
                Console.WriteLine("Error al guardar el archivo JSON: " + ex.Message);
            }
        }

        /// <summary>
        /// Edita un curso en la base de datos por su código.
        /// </summary>
        /// <param name="codigoABuscar">El código del curso a ser editado.</param>
        /// <param name="nombre">El nuevo nombre del curso.</param>
        /// <param name="codigo">El nuevo código del curso.</param>
        /// <param name="descripcion">La nueva descripción del curso.</param>
        /// <param name="cupo">El nuevo cupo máximo del curso.</param>
        /// <param name="turno">El nuevo turno del curso.</param>
        /// <param name="dia">El nuevo día del curso.</param>
        /// <param name="aula">La nueva aula del curso.</param>
        public void EditarCursoBD(string codigoABuscar, string nombre, string codigo, string descripcion, int cupo, string turno, string dia, string aula)
        {
            foreach (Curso curso in _listaCursos)
            {
                int cuposOcupados = curso.CupoMaximo - curso.CupoDisponible;

                if (curso.Codigo == codigoABuscar)
                {
                    curso.Nombre = nombre;
                    curso.Codigo = codigo;
                    curso.Descripcion = descripcion;
                    curso.CupoMaximo = cupo;
                    curso.CupoDisponible = cupo - cuposOcupados;
                    curso.Turno = turno;
                    curso.Dia = dia;
                    curso.Aula = aula;
                }
            }
            ArchivosJsonCursos.GuardarArchivoJSON(_listaCursos);
        }

        /// <summary>
        /// Elimina un curso de la base de datos por su código.
        /// </summary>
        /// <param name="codigoABuscar">El código del curso a ser eliminado.</param>
        public void EliminarCursoBD(string codigoABuscar)
        {
            List<Curso> cursosAEliminar = new List<Curso>();

            foreach (Curso curso in _listaCursos)
            {
                if (curso.Codigo == codigoABuscar)
                {
                    cursosAEliminar.Add(curso);
                }
            }

            foreach (Curso curso in cursosAEliminar)
            {
                _listaCursos.Remove(curso);
            }

            Sistema.BaseDatosEstudiantes.EliminarCursoAEstudiante(codigoABuscar);

            ArchivosJsonCursos.GuardarArchivoJSON(_listaCursos);
        }

        /// <summary>
        /// Resta 1 al cupo disponible de un determinado curso.
        /// </summary>
        /// <param name="cursoARestarCupo">El curso al que se le restará un cupo.</param>
        internal void RestarCupoDisponible(Curso cursoARestarCupo)
        {
            for (int i = 0; i < _listaCursos.Count; i++)
            {
                if (_listaCursos[i].Codigo == cursoARestarCupo.Codigo)
                {
                    // Restar 1 al CupoDisponible del objeto Curso
                    _listaCursos[i] -= 1;
                }
            }

            ArchivosJsonCursos.GuardarArchivoJSON(_listaCursos);
        }

        /// <summary>
        /// Devuelve una lista de cursos disponibles (con cupo disponible).
        /// </summary>
        /// <returns>Una lista de cursos disponibles.</returns>
        public List<Curso> DevolverCursosDisponibles()
        {
            List<Curso> listaCursosDisponibles = new List<Curso>();
            foreach (Curso curso in _listaCursos)
            {
                if (curso.CupoDisponible > 0)
                {
                    listaCursosDisponibles.Add(curso);
                }
            }
            return listaCursosDisponibles;
        }

        public void EliminarCursosRequeridosACursoExistente(Curso cursoAModificar, List<Curso> cursosAEliminar)
        {
            for (int i = 0; i < _listaCursos.Count; i++)
            {
                Curso curso = _listaCursos[i];

                if (curso.Codigo == cursoAModificar.Codigo)
                {
                    List<Curso> nuevasCorrelatividades = new List<Curso>();

                    foreach (Curso cursoRequerido in curso.Correlatividades)
                    {
                        if (!cursosAEliminar.Contains(cursoRequerido))
                        {
                            nuevasCorrelatividades.Add(cursoRequerido);
                        }
                    }

                    curso.Correlatividades = nuevasCorrelatividades;
                }
            }

            ArchivosJsonCursos.GuardarArchivoJSON(_listaCursos);
        }

        //Getters y setters
        public List<Curso> Cursos { get { return _listaCursos; } }
    }
}
