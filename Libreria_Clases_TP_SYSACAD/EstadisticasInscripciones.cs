﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria_Clases_TP_SYSACAD
{
    public static class EstadisticasInscripciones
    {
        public static int CalcularInscripcionesTotales(List<RegistroDeInscripcion> listaRegistrosMostrados)
        {
            return listaRegistrosMostrados.Count;
        }

        public static DateTime CalcularFechaMasPopular(List<RegistroDeInscripcion> listaRegistrosMostrados)
        {
            // Creamos un diccionario para contar cuántas veces ocurre cada fecha
            Dictionary<DateTime, int> conteoFechas = new Dictionary<DateTime, int>();

            foreach (RegistroDeInscripcion registro in listaRegistrosMostrados)
            {
                // Obtenemos la fecha de la inscripción del registro iterado
                DateTime fecha = registro.FechaInscripcion;

                // Verificamos si la fecha ya existe en el diccionario
                if (conteoFechas.ContainsKey(fecha))
                {
                    // Si existe, le aumentamos el conteo
                    conteoFechas[fecha]++;
                }
                else
                {
                    // Si no existe, le inicializamos el conteo en 1
                    conteoFechas[fecha] = 1;
                }
            }


            // Inicializamos variables para rastrear la fecha más popular y el conteo más alto
            DateTime fechaPopular = DateTime.MinValue;
            int maxConteo = 0;

            // Iteramos a través del diccionario para encontrar la fecha más popular
            foreach (var kvp in conteoFechas)
            {
                if (kvp.Value > maxConteo)
                {
                    // Si encontramos un conteo más alto, actualizamos la fecha y el conteo más altos
                    fechaPopular = kvp.Key;
                    maxConteo = kvp.Value;
                }
            }

            return fechaPopular;
        }

        public static double CalcularMediaPorDia(List<RegistroDeInscripcion> listaRegistrosMostrados)
        {
            // Crear un diccionario para contar las inscripciones por día
            Dictionary<DateTime, int> inscripcionesPorDia = new Dictionary<DateTime, int>();

            // Recorrer la lista de registros y contar las inscripciones por día
            foreach (RegistroDeInscripcion registro in listaRegistrosMostrados)
            {
                DateTime fechaInscripcion = registro.FechaInscripcion.Date; // Ignorar la hora

                if (inscripcionesPorDia.ContainsKey(fechaInscripcion))
                {
                    inscripcionesPorDia[fechaInscripcion]++;
                }
                else
                {
                    inscripcionesPorDia[fechaInscripcion] = 1;
                }
            }

            // Calcular la cantidad total de inscripciones y la cantidad de días
            int totalInscripciones = inscripcionesPorDia.Values.Sum();
            int totalDias = inscripcionesPorDia.Count;

            // Calcular la media
            double mediaPorDia = totalInscripciones / (double)totalDias;

            return mediaPorDia;
        }
    }
}
