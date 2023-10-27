﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libreria_Clases_TP_SYSACAD.EntidadesPrimarias;
using Libreria_Clases_TP_SYSACAD.EntidadesSecundarias;

namespace Libreria_Clases_TP_SYSACAD.Validaciones
{
    public enum ValidacionInscripcionResultado
    {
        SinSeleccion,
        Exitoso,
        NoCumpleRequisitos,
        SinCupoAbsoluto,
        SinCupoParcial
    }

    public class ValidadorDeInscripciones
    {
        private List<string> _cursosSinCupo = new List<string>();
        private List<string> _cursosProhibidos = new List<string>();

        public ValidacionInscripcionResultado ValidarCursosSegunCupo(List<Curso> cursosSeleccionados)
        {
            if (cursosSeleccionados.Count == 0)
            {
                return ValidacionInscripcionResultado.SinSeleccion;
            }

            //Buscamos si hay cupos disponibles en aquellos cursos seleccionados
            //En caso de que no tenga cupo, se agrega a la lista de "cursos sin cupo"
            //En caso de que haya cupo, restamos un cupo al curso y agregamos el curso al estudiante
            foreach (Curso curso in cursosSeleccionados)
            {
                bool resultadoValidacionCupos = ComprobarSiHayCuposDisponiblesEnCurso(curso);
                bool resultadoValidacionRequisitosAcademicos = ComprobarSiCumpleRequisitosAcademicos(curso);

                //Si no cumplio con los requisitos
                if (resultadoValidacionRequisitosAcademicos == false)
                {
                    _cursosProhibidos.Add(curso.Nombre);
                }
                // Si no hay cupos
                else if (resultadoValidacionCupos == false)
                {
                    _cursosSinCupo.Add(curso.Nombre);
                    Sistema.BaseDatosCursos.AgregarEstudianteAListaDeEspera(Sistema.EstudianteLogueado, curso);
                }
                // Si cumple con los requisitos y hay cupos
                else
                {
                    GestionarInscripcion(curso);
                }
            }

            if (_cursosProhibidos.Count > 0)
            {
                return ValidacionInscripcionResultado.NoCumpleRequisitos; //SI NO CUMPLE REQUISITOS
            }
            else if (_cursosSinCupo.Count == cursosSeleccionados.Count)
            {
                return ValidacionInscripcionResultado.SinCupoAbsoluto; //SI NO HAY CUPO EN NINGUNO DE LOS SELECCIONADOS
            }
            else if (_cursosSinCupo.Count > 0)
            {
                return ValidacionInscripcionResultado.SinCupoParcial; //SI NO HAY CUPO EN ALGUNOS DE LOS SELECCIONADOS
            }
            else
            {
                return ValidacionInscripcionResultado.Exitoso;
            }
        }

        private static void GestionarInscripcion(Curso curso)
        {
            RegistroDeInscripcion nuevoRegistro = GenerarNuevoRegistroDeInscripcion(curso);

            Sistema.BaseDatosCursos.RestarCupoDisponible(curso);
            Sistema.BaseDatosEstudiantes.AgregarCursoAEstudiante(Sistema.EstudianteLogueado, curso);
            Sistema.BaseDatosInscripciones.IngresarNuevoRegistro(nuevoRegistro);
        }

        private static RegistroDeInscripcion GenerarNuevoRegistroDeInscripcion(Curso curso)
        {
            Estudiante estudianteInscripto = Sistema.EstudianteLogueado;
            Curso cursoAlCualSeInscribe = curso;
            DateTime fechaInscripcion = DateTime.Now;

            RegistroDeInscripcion nuevoRegistro = new RegistroDeInscripcion(estudianteInscripto.Legajo,
                estudianteInscripto.Nombre, cursoAlCualSeInscribe.Nombre, cursoAlCualSeInscribe.Codigo,
                cursoAlCualSeInscribe.Carrera, fechaInscripcion);

            return nuevoRegistro;
        }

        /// <summary>
        /// Comprueba si hay cupos disponibles en un curso seleccionado.
        /// </summary>
        /// <param name="cursoSeleccionado">El curso seleccionado.</param>
        /// <returns>True si hay cupos disponibles; de lo contrario, false.</returns>
        private static bool ComprobarSiHayCuposDisponiblesEnCurso(Curso cursoSeleccionado)
        {
            List<Curso> lista_cursos_disponibles = Sistema.BaseDatosCursos.Cursos;
            bool respuestaCuposDisponibles = false;

            foreach (Curso cursoDisponible in lista_cursos_disponibles)
            {
                if (cursoSeleccionado.Codigo == cursoDisponible.Codigo)
                {
                    if (cursoDisponible.ChequearCuposDisponibles() == true)
                    {
                        respuestaCuposDisponibles = true;
                    }
                }
            }

            return respuestaCuposDisponibles;
        }

        private static bool ComprobarSiCumpleRequisitosAcademicos(Curso cursoSeleccionado)
        {
            List<string> cursosRequeridosParaLaInscripción = cursoSeleccionado.Correlatividades;
            List<string> codigosDeFamiliaDeCursosCompletados = Sistema.EstudianteLogueado.CursosCompletados;

            bool cumpleRequisitosAcademicos = true;

            foreach (string cursoRequerido in cursosRequeridosParaLaInscripción)
            {
                // Verifico si el código de familia del curso requerido no se encuentra en la lista de cursos completados
                if (!codigosDeFamiliaDeCursosCompletados.Contains(cursoRequerido))
                {
                    // Si no se encuentra uno de los cursos requeridos en los cursos completados, se establece cumpleRequisitosAcademicos en false y salgo del bucle
                    cumpleRequisitosAcademicos = false;
                    break;
                }
                else if (cursoSeleccionado.CreditosRequeridos > Sistema.EstudianteLogueado.Creditos)
                {
                    cumpleRequisitosAcademicos = false;
                    break;
                }
                else if (cursoSeleccionado.PromedioRequerido > Sistema.EstudianteLogueado.Promedio)
                {
                    cumpleRequisitosAcademicos = false;
                    break;
                }
            }

            return cumpleRequisitosAcademicos;
        }

        //Getters y Setters
        public List<string> CursosSinCupo { get { return _cursosSinCupo; } }
    }
}
