﻿using Libreria_Clases_TP_SYSACAD.BaseDeDatos;
using Libreria_Clases_TP_SYSACAD.Entidades_Primarias;
using Libreria_Clases_TP_SYSACAD.EntidadesPrimarias;
using Libreria_Clases_TP_SYSACAD.Gestores;
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
    public partial class FormPanelAdmProf : FormPadre
    {

        private List<Profesor> _listaProfs = ConsultasProfesores.Profesores;
        private Profesor? _profesorSeleccionado;
        private BindingSource miBindingSource = new BindingSource();
        private GestorProfesores gestorProfesores = new GestorProfesores();
        public FormPanelAdmProf()
        {
            InitializeComponent();
            ConfigurarBindingSource();
            MostrarProfesores();
        }

        /// <summary>
        /// Devuelve el índice correspondiente a la especialización del profesor.
        /// </summary>
        /// <param name="profesor">El profesor del cual se desea obtener el índice de especialización.</param>
        /// <returns>El índice correspondiente a la especialización del profesor.</returns>
        private void ConfigurarBindingSource()
        {
            BindingList<Profesor> bindingListaProfs = new BindingList<Profesor>();
            // Aquí puedes agregar los cursos a la BindingList<Curso>

            // Asigna la BindingList<Curso> al BindingSource
            miBindingSource.DataSource = bindingListaProfs;

            // Enlaza el DataGridView al BindingSource
            profDGV.DataSource = miBindingSource;
        }

        /// <summary>
        /// Configura el enlace de datos entre una lista de profesores y un DataGridView mediante un BindingSource.
        /// </summary>
        private void MostrarProfesores()
        {
            // Obtenemos el BindingSource que enlaza el DataGridView
            BindingSource bindingSource = (BindingSource)profDGV.DataSource;

            // Obtenemos la BindingList<Curso> desde el BindingSource
            BindingList<Profesor> bindingListaProfs = (BindingList<Profesor>)bindingSource.List;

            foreach (Profesor profesor in ListaProfs)
            {
                bindingListaProfs.Add(profesor);
            }
        }


        private void backBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            FormPanelAdministrador formPanelAdministrador = new FormPanelAdministrador();
            formPanelAdministrador.Show();
        }

        private void agregarProfBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            FormAgregarProf FormAgregarProf = new FormAgregarProf();
            FormAgregarProf.Show();
        }

        private void profDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow filaSeleccionada = profDGV.CurrentRow;

            string correoProfesorSeleccionado = filaSeleccionada.Cells[3].Value.ToString();
           

            ProfesorSeleccionado = gestorProfesores.DevolverProfesor(correoProfesorSeleccionado);
        }

        private void editarProfBtn_Click(object sender, EventArgs e)
        {
            if (ProfesorSeleccionado != null)
            {
                this.Close();
                FormEditarProf FormEditarProf = new FormEditarProf(ProfesorSeleccionado);
                FormEditarProf.Show();
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningún profesor.");
            }
        }

        private void eliminarCursoBtn_Click(object sender, EventArgs e)
        {
            if (ProfesorSeleccionado != null)
            {
                this.Close();
                FormEliminarProf FormEliminarProf = new FormEliminarProf(ProfesorSeleccionado);
                FormEliminarProf.Show();
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningún profesor.");
            }
        }


        private void gestCursosBtn_Click_1(object sender, EventArgs e)
        {
            if (ProfesorSeleccionado != null)
            {
                this.Close();
                FormAsignarCursoAProf FormAsignarCursoAProf = new FormAsignarCursoAProf(ProfesorSeleccionado);
                FormAsignarCursoAProf.Show();
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningún profesor.");
            }

        }

        public List<Profesor> ListaProfs
        {
            get { return _listaProfs; }
            set { _listaProfs = value; }
        }

        public Profesor? ProfesorSeleccionado
        {
            get { return _profesorSeleccionado; }
            set { _profesorSeleccionado = value; }
        }

    }
}
