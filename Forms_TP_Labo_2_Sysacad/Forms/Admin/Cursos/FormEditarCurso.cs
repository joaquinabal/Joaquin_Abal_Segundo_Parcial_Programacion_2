﻿using Libreria_Clases_TP_SYSACAD;
using Libreria_Clases_TP_SYSACAD.EntidadesPrimarias;
using Libreria_Clases_TP_SYSACAD.BaseDeDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Libreria_Clases_TP_SYSACAD.Validaciones;
using Libreria_Clases_TP_SYSACAD.Interfaces_y_Enum;
using Libreria_Clases_TP_SYSACAD.Gestores;

namespace Forms_TP_Labo_2_Sysacad
{
    public partial class FormEditarCurso : FormPadre
    {
        private string _nombre;
        private string _codigo;
        private string _descripcion;
        private string _cupo;
        private string _turno;
        private string _aula;
        private string _dia;

        private bool _cambioCurso;

        private string _codigoOriginal;

        private GestorCursos gestorCursos = new GestorCursos();

        public FormEditarCurso(Curso curso)
        {
            InitializeComponent();

            _cambioCurso = false;

            nombreTextbox.Text = curso.Nombre;
            codigoTextbox.Text = curso.Codigo;
            descripcionTextbox.Text = curso.Descripcion;
            cupoMaxTextbox.Text = curso.CupoMaximo.ToString();
            turnoCombobox.SelectedIndex = devolverIndiceTurno(curso);
            aulaTextbox.Text = curso.Aula;
            diaCombobox.SelectedIndex = devolverIndiceDia(curso);

            _codigoOriginal = curso.Codigo;

            _nombre = curso.Nombre;
            _codigo = curso.Codigo;
            _descripcion = curso.Descripcion;
            _cupo = curso.CupoMaximo.ToString();
            _turno = curso.Turno;
            _aula = curso.Aula;
            _dia = curso.Dia;

        }

        /// <summary>
        /// Devuelve el índice correspondiente al día de la semana en el arreglo de días.
        /// </summary>
        /// <param name="curso">El curso del cual se quiere obtener el índice del día.</param>
        /// <returns>El índice del día de la semana (0 para Lunes, 1 para Martes, etc.).</returns>
        private int devolverIndiceDia(Curso curso)
        {
            int indice = 0;
            switch (curso.Dia)
            {
                case "Lunes":
                    indice = 0;
                    break;

                case "Martes":
                    indice = 1;
                    break;

                case "Miércoles":
                    indice = 2;
                    break;

                case "Jueves":
                    indice = 3;
                    break;

                case "Viernes":
                    indice = 4;
                    break;
            }

            return indice;
        }

        /// <summary>
        /// Devuelve el índice correspondiente al turno en el arreglo de turnos (Mañana, Tarde, Noche).
        /// </summary>
        /// <param name="curso">El curso del cual se quiere obtener el índice del turno.</param>
        /// <returns>El índice del turno (0 para Mañana, 1 para Tarde, 2 para Noche).</returns>
        private int devolverIndiceTurno(Curso curso)
        {
            int indice = 0;
            switch (curso.Turno)
            {
                case "Mañana":
                    indice = 0;
                    break;

                case "Tarde":
                    indice = 1;
                    break;

                case "Noche":
                    indice = 2;
                    break;
            }

            return indice;
        }


        private void nombreTextbox_TextChanged(object sender, EventArgs e)
        {
            _nombre = nombreTextbox.Text;
        }

        private void codigoTextbox_TextChanged(object sender, EventArgs e)
        {
            _codigo = codigoTextbox.Text;
        }

        private void cupoMaxTextbox_TextChanged(object sender, EventArgs e)
        {
            _cupo = cupoMaxTextbox.Text;
        }

        private void descripcionTextbox_TextChanged(object sender, EventArgs e)
        {
            _descripcion = descripcionTextbox.Text;
        }

        private void aulaTextbox_TextChanged(object sender, EventArgs e)
        {
            _aula = aulaTextbox.Text;
        }


        private async void editarBtn_Click(object sender, EventArgs e)
        {

            Dictionary<string, string> dictCampos = new Dictionary<string, string>()
            {

                {"nombre", _nombre },
                {"codigo", _codigo },
                {"aula", _aula },
                {"descripcion", _descripcion },
                {"cupoMax", _cupo}

            };
            if (_codigoOriginal != _codigo)
            {
                _cambioCurso = true;
            }


            RespuestaValidacionInput validacionCurso = gestorCursos.ValidarInputsCursos(dictCampos);// GestionarEdicionCurso(dictCampos, _codigoOriginal, _nombre, _codigo, _descripcion, _cupo, _turno, _dia, _aula);

            if (!validacionCurso.AusenciaCamposVacios)
            {
                MessageBox.Show("Complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (validacionCurso.ExistenciaErrores)
            {
                MessageBox.Show(validacionCurso.MensajeErrores, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                bool validacionDuplicado = await gestorCursos.GestionarEdicionCursoEnBaseADuplicados(_cambioCurso, _codigoOriginal, _nombre, _codigo, _descripcion, _cupo, _turno, _dia, _aula);

                if (!validacionDuplicado)
                {
                    MessageBox.Show("Se ha editado correctamente.");
                    this.Hide();
                    FormPanelAdmCursos formPanelAdmCursos = new FormPanelAdmCursos();
                    formPanelAdmCursos.Show();
                }
                else
                {
                    MessageBox.Show("Ya existe un curso con ese código.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }         
        }


        private void turnoCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _turno = turnoCombobox.SelectedItem.ToString();
        }

        private void diaCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _dia = diaCombobox.SelectedItem.ToString();
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            FormPanelAdmCursos formPanelAdmCursos = new FormPanelAdmCursos();
            formPanelAdmCursos.Show();
        }
    }
}
