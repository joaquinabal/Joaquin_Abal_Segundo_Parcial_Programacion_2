﻿using Libreria_Clases_TP_SYSACAD.EntidadesPrimarias;
using Libreria_Clases_TP_SYSACAD.EntidadesSecundarias;
using Libreria_Clases_TP_SYSACAD.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms_TP_SYSACAD
{
    public partial class Form_Pagos_Principal : Form
    {
        private double _montoAAbonar = 0;
        private string _medioPago = "Tarjeta de Credito";
        private Dictionary<string, double> _conceptosSeleccionados = new Dictionary<string, double>();

        public Form_Pagos_Principal()
        {
            InitializeComponent();

            lblTotalAPagar.Text = "Total a Pagar: 0";
            lblMedioDePago.Text = "Medio de pago";

            rbCredito.Text = "Tarjeta de Credito";
            rbDebito.Text = "Tarjeta de Debito";
            rbTransferencia.Text = "Transferencia Bancaria";
            rbCredito.Checked = true;

            btnContinuar.Text = "Continuar";
            btnAtras.Text = "Atras";

            MostrarConceptosDePagoDGV(Sistema.EstudianteLogueado);
        }

        private void Form_Pagos_Principal_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.Text = "Realizar Pagos";
        }

        //Muestro los conceptos de pago
        private void MostrarConceptosDePagoDGV(Estudiante estudianteLogeado)
        {
            dgvPagos.Rows.Clear();

            // Obtengo la lista de conceptos de pago del estudiante
            List<ConceptoDePago> ListaConceptosDePago = estudianteLogeado.ConceptosDePago;

            // Itero a través de la lista de conceptos de pago y los agrego al DataGridView
            foreach (ConceptoDePago concepto in ListaConceptosDePago)
            {
                if (concepto.MontoPendiente > 0)
                {
                    dgvPagos.Rows.Add(concepto.Nombre, concepto.MontoPendiente.ToString(), 0);
                }
            }
        }

        //Actualizo el monto total ante cada cambio
        private void DataGridViewCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 2)
            {
                // Calculo el total en base a los montos ingresados
                _montoAAbonar = 0;
                foreach (DataGridViewRow row in dgvPagos.Rows)
                {
                    if (row.Cells[2].Value != null)
                    {
                        double monto;
                        if (double.TryParse(row.Cells[2].Value.ToString(), out monto))
                        {
                            _montoAAbonar += monto;
                        }
                    }
                }

                // Muestro el total en el Label (o en cualquier otro control deseado)
                lblTotalAPagar.Text = "Total a Pagar: $" + _montoAAbonar.ToString("0.00");
            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form_Panel_Estudiante formPanelEstudiante = new Form_Panel_Estudiante();
            formPanelEstudiante.Show();
        }

        /// <summary>
        /// Maneja el evento de hacer clic en el botón "Continuar" para continuar con el proceso de pago.
        /// </summary>
        private void btnContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                //Verifico si se ingresó un monto a abonar
                if (_montoAAbonar == 0)
                {
                    throw new Exception("No ha ingresado un monto a abonar. Por favor, ingrese el monto a abonar.");
                }

                //Creo 2 listas. Una para los montos a abonar y otra para los montos originales
                List<double> listaDeMontosAAbonar = new List<double>();
                List<double> listaDeMontosOriginales = new List<double>();

                //Lleno las listas con los montos ingresados
                foreach (DataGridViewRow row in dgvPagos.Rows)
                {
                    if (row.Cells[2].Value != null)
                    {
                        double monto;
                        if (double.TryParse(row.Cells[2].Value.ToString(), out monto))
                        {
                            listaDeMontosAAbonar.Add(monto);
                        }
                    }
                }

                foreach (DataGridViewRow row in dgvPagos.Rows)
                {
                    if (row.Cells[1].Value != null)
                    {
                        double monto;
                        if (double.TryParse(row.Cells[1].Value.ToString(), out monto))
                        {
                            listaDeMontosOriginales.Add(monto);
                        }
                    }
                }

                //Valido que los montos no sean negativos ni excesivos
                ValidadorMontoAAbonar validadorMontosAAbonar = new ValidadorMontoAAbonar(listaDeMontosAAbonar);
                ValidadorMontoAAbonar validadorMontosOriginales = new ValidadorMontoAAbonar(listaDeMontosAAbonar, listaDeMontosOriginales);

                bool numeroNegativo = validadorMontosAAbonar.VerificarSiHayNumeroNegativo();
                bool valorExcesivo = validadorMontosOriginales.VerificarSiHayValorExcesivo();

                //Si no hay errores en los campos
                if (!numeroNegativo && !valorExcesivo)
                {
                    //Creo el diccionario
                    foreach (DataGridViewRow row in dgvPagos.Rows)
                    {
                        if (row.Cells[2].Value != null && !string.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            double monto;
                            if (double.TryParse(row.Cells[2].Value.ToString(), out monto) && monto > 0)
                            {
                                _conceptosSeleccionados[row.Cells[0].Value.ToString()] = monto;
                            }
                        }
                    }

                    // Envio al usuario al formulario de acuerdo al medio de pago seleccionado
                    if (_medioPago == "Tarjeta de Credito" || _medioPago == "Tarjeta de Debito")
                    {
                        this.Hide();
                        Form_Pago_Tarjetas formPagoTarjetas = new Form_Pago_Tarjetas(_montoAAbonar, _conceptosSeleccionados);
                        formPagoTarjetas.Show();
                    }
                    else
                    {
                        this.Hide();
                        Form_Pago_Transferencia formPagoTransferencia = new Form_Pago_Transferencia(_montoAAbonar, _conceptosSeleccionados);
                        formPagoTransferencia.Show();
                    }
                }

                // Si hay errores en los campos, muestro mensajes de error
                else if (numeroNegativo && !valorExcesivo)
                {
                    throw new Exception("No se permite ingresar números negativos en el abono.");
                }
                else if (!numeroNegativo && valorExcesivo)
                {
                    throw new Exception("Ha ingresado un valor a abonar superior al correspondiente.");
                }
                else
                {
                    throw new Exception("No se permite ingresar números negativos en el abono.\nHa ingresado un valor a abonar superior al correspondiente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Configuro los radio button para que solo se pueda seleccionar uno
        private void rbCredito_Click(object sender, EventArgs e)
        {
            rbCredito.Checked = true;
            rbDebito.Checked = false;
            rbTransferencia.Checked = false;
            _medioPago = rbCredito.Text;
        }

        private void rbDebito_Click(object sender, EventArgs e)
        {
            rbCredito.Checked = false;
            rbDebito.Checked = true;
            rbTransferencia.Checked = false;
            _medioPago = rbDebito.Text;
        }

        private void rbTransferencia_Click(object sender, EventArgs e)
        {
            rbCredito.Checked = false;
            rbDebito.Checked = false;
            rbTransferencia.Checked = true;
            _medioPago = rbTransferencia.Text;
        }
    }
}
