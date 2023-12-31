﻿using Libreria_Clases_TP_SYSACAD.BaseDeDatos;
using Libreria_Clases_TP_SYSACAD.EntidadesPrimarias;
using Libreria_Clases_TP_SYSACAD.Gestores;
using Libreria_Clases_TP_SYSACAD.Interfaces_y_Enum;
using Libreria_Clases_TP_SYSACAD.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms_TP_Labo_2_Sysacad.Forms.Admin
{
    public partial class FormAgregarEstudianteAListaDeEspera : FormPadre
    {

        private List<Estudiante> _listaEstudiantes;
        private Curso _cursoSeleccionado;
        private Estudiante? _estudianteSeleccionado;
        private BindingSource miBindingSource = new BindingSource();
        private Dictionary<string, DateTime> _listaEsperaCurso;
        private bool _flagEstudianteSeleccionado = false;
        private GestorCursos gestorCursos = new GestorCursos();
        private GestorEstudiantes gestorEstudiantes = new GestorEstudiantes();
        private GestorListaDeEspera gestorListaDeEspera = new GestorListaDeEspera();
        public FormAgregarEstudianteAListaDeEspera(Curso curso)
        {
            InitializeComponent();
            CursoSeleccionado = curso;
            ListaEsperaCurso = DevolverListaEsperaSegunCurso(CursoSeleccionado);
            ListaEstudiantes = ConsultasEstudiantes.Estudiantes;
            ConfigurarBindingSource();
            MostrarEstudiantes();
        }

        /// <summary>
        /// Devuelve la lista de espera según el curso especificado.
        /// </summary>
        /// <param name="curso">El curso para el que se busca la lista de espera.</param>
        /// <returns>Un diccionario que contiene nombres y fechas para la lista de espera del curso.</returns>
        private Dictionary<string, DateTime> DevolverListaEsperaSegunCurso(Curso curso)
        {
            GestorReportes gestorReportes = new GestorReportes();
            Dictionary<string, DateTime> listaCurso = new Dictionary<string, DateTime>();
            Dictionary<Curso, Dictionary<string, DateTime>> dictCursosLista = gestorReportes.ObtenerCursosConListaDeEsperaSegunFechas(DateTime.MinValue, DateTime.MaxValue);
            foreach (Curso cursoLista in dictCursosLista.Keys)
            {
                if (cursoLista.Codigo == curso.Codigo)
                {
                    listaCurso = dictCursosLista[cursoLista];
                    break;
                }
            }
            return listaCurso;
        }


        /// <summary>
        /// Configura el enlace de datos entre una lista de estudiantes y un DataGridView mediante un BindingSource.
        /// </summary>
        private void ConfigurarBindingSource()
        {
            BindingList<Estudiante> bindingListaEstudiantes = new BindingList<Estudiante>();
            // Aquí puedes agregar los cursos a la BindingList<Curso>

            // Asigna la BindingList<Curso> al BindingSource
            miBindingSource.DataSource = bindingListaEstudiantes;

            // Enlaza el DataGridView al BindingSource
            estudiantesDGV.DataSource = miBindingSource;
        }

        /// <summary>
        /// Muestra los estudiantes enlazados al DataGridView.
        /// </summary>
        private void MostrarEstudiantes()
        {
            // Obtenemos el BindingSource que enlaza el DataGridView
            BindingSource bindingSource = (BindingSource)estudiantesDGV.DataSource;

            // Obtenemos la BindingList<Curso> desde el BindingSource
            BindingList<Estudiante> bindingListaEstudiantes = (BindingList<Estudiante>)bindingSource.List;

            foreach (Estudiante estudiante in ListaEstudiantes)
            {
                bindingListaEstudiantes.Add(estudiante);
            }
        }


        private void estudiantesDGV_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            FlagEstudianteSeleccionado = true;
            DataGridViewRow filaSeleccionada = estudiantesDGV.CurrentRow;

            string legajoEstudianteSelecionado = filaSeleccionada.Cells[1].Value.ToString();

            EstudianteSeleccionado = gestorEstudiantes.ObtenerEstudianteSegunLegajo(legajoEstudianteSelecionado);
        }

        private async void agregarEstBtn_Click(object sender, EventArgs e)
        {
            if (FlagEstudianteSeleccionado)
            {
                List<Curso> listaCursoSelecionado = new List<Curso>
                {
                    CursoSeleccionado
                };
                
                RespuestaValidacionInput validacionListaDeEspera = gestorListaDeEspera.ValidarInputListaEspera(EstudianteSeleccionado.Legajo, CursoSeleccionado);

                if (validacionListaDeEspera.ExistenciaEnListaDeEspera)
                {
                    MessageBox.Show("Este alumno ya se encuentra en la Lista de Espera de este Curso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (validacionListaDeEspera.ExistenciaErrores)
                {
                    MessageBox.Show(validacionListaDeEspera.MensajeErrores, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!validacionListaDeEspera.ExistenciaErrores)
                {
                    Sistema.IngresarEstudianteComoUsuarioActivo(EstudianteSeleccionado.Correo);
                    ValidacionInscripcionResultado validacionInscripcionResultado = await gestorCursos.GestionarInscripcionACurso(listaCursoSelecionado);
                    
                    if(validacionInscripcionResultado == ValidacionInscripcionResultado.NoCumpleNingunRequisito)
                    {
                        MessageBox.Show("El estudiante no cumple con los requisitos académicos.");
                    }
                    else
                    {
                    ListaEsperaCurso = DevolverListaEsperaSegunCurso(CursoSeleccionado);
                    await gestorListaDeEspera.GestionarActualizacionListaEspera(CursoSeleccionado, ListaEsperaCurso);
                    Console.WriteLine(CursoSeleccionado.ListaDeEspera);
                    MessageBox.Show("Alumno ingresado a la lista de espera.");
                    }
                    
                }

                this.Close();
                FormPanelListasEsperaEstudiantes FormPanelListasEsperaEstudiantes = new FormPanelListasEsperaEstudiantes(CursoSeleccionado);
                FormPanelListasEsperaEstudiantes.Show();
            }
            else
            {
                MessageBox.Show("Selecciona un estudiante.");
            }
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            FormPanelListasEsperaEstudiantes FormPanelListasEsperaEstudiantes = new FormPanelListasEsperaEstudiantes(CursoSeleccionado);
            FormPanelListasEsperaEstudiantes.Show();
        }

        public List<Estudiante> ListaEstudiantes
        {
            get { return _listaEstudiantes; }
            set { _listaEstudiantes = value; }
        }

        public Estudiante? EstudianteSeleccionado
        {
            get { return _estudianteSeleccionado; }
            set { _estudianteSeleccionado = value; }
        }

        public Dictionary<string, DateTime> ListaEsperaCurso
        {
            get { return _listaEsperaCurso; }
            set { _listaEsperaCurso = value; }
        }

        public bool FlagEstudianteSeleccionado
        {
            get { return _flagEstudianteSeleccionado; }
            set { _flagEstudianteSeleccionado = value; }
        }

        public Curso CursoSeleccionado
        {
            get { return _cursoSeleccionado; }
            set { _cursoSeleccionado = value; }
        }
    }
}

