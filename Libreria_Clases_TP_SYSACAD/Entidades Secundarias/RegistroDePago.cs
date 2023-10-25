﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libreria_Clases_TP_SYSACAD.EntidadesPrimarias;

namespace Libreria_Clases_TP_SYSACAD.EntidadesSecundarias
{
    public class RegistroDePago : IRegistroEstadistico
    {
        private string _concepto;
        private string _legajo;
        private string _nombre;
        private double _ingreso;
        private DateTime _fechaPago;

        public RegistroDePago(Estudiante estudiante, string concepto, double ingreso,
            DateTime fechaPago)
        {
            _concepto = concepto;
            _legajo = estudiante.Legajo;
            _nombre = estudiante.Nombre;
            _ingreso = ingreso;
            _fechaPago = fechaPago;
        }

        //Getters y Setters
        public DateTime Fecha { get { return _fechaPago; } }

        public string Concepto { get { return _concepto; } }

        public string Legajo { get {  return _legajo; } }

        public string NombreEstudiante { get {  return _nombre; } }

        public double Ingreso { get { return _ingreso; } }
    }
}
