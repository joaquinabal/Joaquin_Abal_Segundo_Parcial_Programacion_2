﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libreria_Clases_TP_SYSACAD.EntidadesSecundarias;
using Libreria_Clases_TP_SYSACAD.Persistencia;

namespace Libreria_Clases_TP_SYSACAD.Tablas
{
    public class BaseDatosPagos
    {
        private List<RegistroDePago> _listaRegistrosPagos = new List<RegistroDePago>();

        internal BaseDatosPagos()
        {
            List<RegistroDePago>? registrosCargados = ArchivosJSONPagos.CargarPagosDesdeArchivoJson();

            if (registrosCargados != null)
            {
                _listaRegistrosPagos = registrosCargados;
            }
        }

        public void IngresarNuevoPago(List<RegistroDePago> nuevosPagos)
        {
            if (nuevosPagos.Count > 0)
            {
                foreach (RegistroDePago pago in nuevosPagos)
                {
                    _listaRegistrosPagos.Add(pago);
                }
                ArchivosJSONPagos.GuardarArchivoJSON(_listaRegistrosPagos);
            }
        }

        public List<RegistroDePago> ObtenerIngresosSegunConceptoYFecha(DateTime fechaDesde, DateTime fechaHasta, string concepto)
        {
            List<RegistroDePago> listaIngresosSegunConceptoYFecha = new List<RegistroDePago>();

            foreach (RegistroDePago registro in _listaRegistrosPagos)
            {
                if (registro.Fecha >= fechaDesde && registro.Fecha <= fechaHasta && registro.Concepto == concepto)
                {
                    listaIngresosSegunConceptoYFecha.Add(registro);
                }
            }

            return listaIngresosSegunConceptoYFecha;
        }

        //Getters y Setters
        public List<RegistroDePago> Pagos { get { return _listaRegistrosPagos; } }
    }
}
