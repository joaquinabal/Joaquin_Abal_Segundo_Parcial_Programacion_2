﻿using System.Net.Mail;
using System.Net;
using Libreria_Clases_TP_SYSACAD.Persistencia;
using Libreria_Clases_TP_SYSACAD.EntidadesPrimarias;
using Libreria_Clases_TP_SYSACAD.BaseDeDatos;

namespace Libreria_Clases_TP_SYSACAD.Herramientas
{

    public class GestorEventosNotificaciones
    {
        public delegate void DelegadoEnviarMail(object EmisorCorreoElectronico, ElementosCorreoElectronicoArgs infoEmail);

        public event DelegadoEnviarMail InicioPeriodoAcademico;
        public event DelegadoEnviarMail FechaLimiteInscripcion;
        public event DelegadoEnviarMail FechaLimiteDePago;

        public async void EjecutarInicioPeriodoAcademico(string semestre, string fechaInicio)
        {
            string asuntoMail = "SYSACAD - Inicio Periodo Academico";
            string cuerpoMail = $"Estimado estudiante, se le notifica oficialmente mediante el presente email, el " +
                $"inicio del {semestre} semestre. El mismo comenzará el {fechaInicio}.";

            ElementosCorreoElectronicoArgs infoEmail = new ElementosCorreoElectronicoArgs(asuntoMail, cuerpoMail);

            if (InicioPeriodoAcademico is not null)
            {
                //InicioPeriodoAcademico.Invoke(this, infoEmail);
                
                await Task.Run(() =>
                {
                    InicioPeriodoAcademico.Invoke(this, infoEmail);
                });
            }
        }

        public async void EjecutarFechaLimiteInscripcion(string fechaLimite)
        {
            string asuntoMail = "SYSACAD - Fecha Limite de Inscripcion";
            string cuerpoMail = $"Estimado estudiante, se le notifica oficialmente mediante el presente email, que la " +
                $"fecha limite de incripcion para el proximo semestre es el {fechaLimite}.";

            ElementosCorreoElectronicoArgs infoEmail = new ElementosCorreoElectronicoArgs(asuntoMail, cuerpoMail);

            if (FechaLimiteInscripcion is not null)
            {
                await Task.Run(() =>
                {
                    FechaLimiteInscripcion.Invoke(this, infoEmail);
                });
            }
        }

        public async void EjecutarFechaLimiteDePago(string fechaLimitePago)
        {
            string asuntoMail = "SYSACAD - Fecha Limite de Pagos";
            string cuerpoMail = $"Estimado estudiante, se le notifica oficialmente mediante el presente email, que la " +
                $"fecha limite para efectuar los pagos totales correspondientes al presente semestre corresponde " +
                $"al {fechaLimitePago}.";

            ElementosCorreoElectronicoArgs infoEmail = new ElementosCorreoElectronicoArgs(asuntoMail, cuerpoMail);

            if (FechaLimiteDePago is not null)
            {
                await Task.Run(() =>
                {
                    FechaLimiteDePago.Invoke(this, infoEmail);
                });
            }
        }
    }
}
