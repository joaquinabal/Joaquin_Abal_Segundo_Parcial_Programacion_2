﻿using Libreria_Clases_TP_SYSACAD;
using Libreria_Clases_TP_SYSACAD.BaseDeDatos;
using Libreria_Clases_TP_SYSACAD.EntidadesPrimarias;
using Libreria_Clases_TP_SYSACAD.EntidadesSecundarias;
using Libreria_Clases_TP_SYSACAD.Interfaces_y_Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms_TP_SYSACAD
{
    public partial class Form_Input_Reporte_ComboBox_Periodo : Form
    {
        private FuncionReporte _funcionSeleccionada;

        private Curso _cursoSeleccionado;
        private string _conceptoDePagoSeleccionado;
        private Carrera _carreraSeleccionada;

        private DateTime _fechaDesdeSeleccionada;
        private DateTime _fechaHastaSeleccionada;

        Form_Reportes_Principal _formOrigen;

        public Form_Input_Reporte_ComboBox_Periodo(FuncionReporte funcionSeleccionada, Form_Reportes_Principal formOrigen)
        {
            InitializeComponent();
            _funcionSeleccionada = funcionSeleccionada;
            ConfigurarComboBox();
            ConfigurarDateTimePickers();

            if (_funcionSeleccionada == FuncionReporte.InscripcionesPorCursoPeriodo)
            {
                lblInfo.Text = $"Seleccione el curso";
            }
            else if (_funcionSeleccionada == FuncionReporte.InscripcionesPorCarreraPeriodo)
            {
                lblInfo.Text = $"Seleccione la carrera";
            }
            else
            {
                lblInfo.Text = $"Seleccione el concepto de pago";
            }

            lblDesde.Text = "Desde:";
            lblHasta.Text = "Hasta:";
            btnContinuar.Text = "Continuar";
            btnCancelar.Text = "Cancelar";

            _formOrigen = formOrigen;
        }

        private void Form_Input_Reporte_Inscripciones_Curso_o_Carrera_Load_1(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Ingrese los datos";
        }

        private void ConfigurarComboBox()
        {
            switch (_funcionSeleccionada)
            {
                case FuncionReporte.InscripcionesPorCursoPeriodo:
                    List<Curso> cursosDelSistema = ConsultasCursos.Cursos;

                    foreach (Curso curso in cursosDelSistema)
                    {
                        cbOpciones.Items.Add($"{curso.Nombre} - {curso.Turno}");
                    }

                    cbOpciones.SelectedIndex = 0;

                    _cursoSeleccionado = ObtenerCursoDesdeOpcionSeleccionada(cbOpciones.Text);

                    break;
                case FuncionReporte.InscripcionesPorCarreraPeriodo:
                    string TUP = "TUP";
                    string TUSI = "TUSI";

                    cbOpciones.Items.Add(TUP);
                    cbOpciones.Items.Add(TUSI);
                    cbOpciones.SelectedIndex = 0;
                    _carreraSeleccionada = (Carrera)Enum.Parse(typeof(Carrera), cbOpciones.SelectedItem.ToString());
                    break;
                default:
                    string matriculaIngreso = "Matricula de Ingreso";
                    string primerCuatrimestre = "Matricula del Primer Cuatrimestre";
                    string cargosAdministrativosPrimerCuatrimestre = "Cargos Administrativos primer cuatrimestre";
                    string BibliografiaPrimerCuatrimestre = "Bibliografia Primer Cuatrimestre";

                    cbOpciones.Items.Add(matriculaIngreso);
                    cbOpciones.Items.Add(primerCuatrimestre);
                    cbOpciones.Items.Add(cargosAdministrativosPrimerCuatrimestre);
                    cbOpciones.Items.Add(BibliografiaPrimerCuatrimestre);
                    cbOpciones.SelectedIndex = 0;
                    _conceptoDePagoSeleccionado = cbOpciones.SelectedItem.ToString();
                    break;
            }
        }

        private static Curso ObtenerCursoDesdeOpcionSeleccionada(string textoDelCursoSeleccionado)
        {
            string[] partes = textoDelCursoSeleccionado.Split(new char[] { '-' });

            string nombreCurso = partes[0].Trim();
            string turno = partes[1].Trim();

            Curso cursoEncontrado = ConsultasCursos.ObtenerCursoAPartirDeNombreYTurno(nombreCurso, turno);

            return cursoEncontrado;
        }

        private void ConfigurarDateTimePickers()
        {
            // Establece la fecha "Desde" por defecto (ejemplo: 01 de enero de 2023)
            DateTime fechaDesdePorDefecto = new DateTime(2023, 9, 1);

            dtpDesde.Format = DateTimePickerFormat.Short;
            dtpDesde.CustomFormat = "dd/MM/yyyy";
            dtpDesde.MinDate = new DateTime(2023, 1, 1);
            dtpDesde.MaxDate = DateTime.Now;
            dtpDesde.Value = fechaDesdePorDefecto;
            _fechaDesdeSeleccionada = fechaDesdePorDefecto;

            // Establece la fecha "Hasta" por defecto (ejemplo: 20 de enero de 2023)
            DateTime fechaHastaPorDefecto = new DateTime(2023, 9, 20);

            dtpHasta.Format = DateTimePickerFormat.Short;
            dtpHasta.CustomFormat = "dd/MM/yyyy";
            dtpHasta.MinDate = dtpDesde.Value;
            dtpHasta.MaxDate = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            dtpHasta.Value = fechaHastaPorDefecto.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            _fechaHastaSeleccionada = fechaHastaPorDefecto.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
        }

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            _fechaDesdeSeleccionada = dtpDesde.Value;
        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            _fechaHastaSeleccionada = dtpHasta.Value;
        }

        private void cbOpciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (_funcionSeleccionada)
            {
                case FuncionReporte.InscripcionesPorCursoPeriodo:
                    _cursoSeleccionado = ObtenerCursoDesdeOpcionSeleccionada(cbOpciones.SelectedItem.ToString());
                    break;
                case FuncionReporte.InscripcionesPorCarreraPeriodo:
                    _carreraSeleccionada = (Carrera)Enum.Parse(typeof(Carrera), cbOpciones.SelectedItem.ToString());
                    break;
                default:
                    _conceptoDePagoSeleccionado = cbOpciones.SelectedItem.ToString();
                    break;
            }
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
            switch (_funcionSeleccionada)
            {
                case FuncionReporte.InscripcionesPorCursoPeriodo:
                    List<RegistroDeInscripcion> InscripcionesPorCursoPeriodo = ConsultasInscripciones.ObtenerInscripciones(_fechaDesdeSeleccionada, _fechaHastaSeleccionada, _cursoSeleccionado.Codigo);

                    if (InscripcionesPorCursoPeriodo.Count > 0)
                    {
                        this.Hide();
                        _formOrigen.Hide();
                        Form_Reporte_Inscripciones formReporteInscripciones = new Form_Reporte_Inscripciones(InscripcionesPorCursoPeriodo, _funcionSeleccionada, _fechaDesdeSeleccionada, _fechaHastaSeleccionada);
                        formReporteInscripciones.Show();
                    }
                    else
                    {
                        MessageBox.Show("No existen registros que cumplan con los datos ingresados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;
                case FuncionReporte.InscripcionesPorCarreraPeriodo:
                    List<RegistroDeInscripcion> InscripcionesPorCarreraPeriodo = ConsultasInscripciones.ObtenerInscripciones(_fechaDesdeSeleccionada, _fechaHastaSeleccionada, _carreraSeleccionada);

                    if (InscripcionesPorCarreraPeriodo.Count > 0)
                    {
                        this.Hide();
                        _formOrigen.Hide();
                        Form_Reporte_Inscripciones formReporteInscripciones = new Form_Reporte_Inscripciones(InscripcionesPorCarreraPeriodo, _funcionSeleccionada, _fechaDesdeSeleccionada, _fechaHastaSeleccionada);
                        formReporteInscripciones.Show();
                    }
                    else
                    {
                        MessageBox.Show("No existen registros que cumplan con los datos ingresados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;
                default:
                    List<RegistroDePago> IngresosPorConceptoPeriodo = ConsultasPagos.ObtenerIngresosSegunConceptoYFecha(_fechaDesdeSeleccionada, _fechaHastaSeleccionada, _conceptoDePagoSeleccionado);

                    if (IngresosPorConceptoPeriodo.Count > 0)
                    {
                        this.Hide();
                        _formOrigen.Hide();
                        Form_Reporte_Ingresos formIngresoConceptoPeriodo = new Form_Reporte_Ingresos(IngresosPorConceptoPeriodo, _fechaDesdeSeleccionada, _fechaHastaSeleccionada);
                        formIngresoConceptoPeriodo.Show();
                    }
                    else
                    {
                        MessageBox.Show("No existen registros que cumplan con los datos ingresados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
