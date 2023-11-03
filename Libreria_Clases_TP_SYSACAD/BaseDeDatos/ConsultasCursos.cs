﻿using Libreria_Clases_TP_SYSACAD.EntidadesPrimarias;
using Libreria_Clases_TP_SYSACAD.Persistencia;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libreria_Clases_TP_SYSACAD.Herramientas;
using Libreria_Clases_TP_SYSACAD.EntidadesSecundarias;
using System.Security.Cryptography;

namespace Libreria_Clases_TP_SYSACAD.BaseDeDatos
{
    public class ConsultasCursos : ConexionBD
    {
        private static List<Curso> _listaCursos = new List<Curso>();

        /////////////////// RECONSTRUCCION DE LA LISTA DE CURSOS A PARTIR DE BD
        private static List<string> DevolverListaDeCorrelatividadesSegunCFId(int CFid)
        {
            List<string> CFCorrelatividades = new List<string>();

            using (var connectionAlternativa = new SqlConnection(@"Server = .; Database = TestSYSACAD; Trusted_Connection = True; Encrypt=False; TrustServerCertificate=True;"))
            {
                using (var commandAlternativo = new SqlCommand("SELECT codigoFamiliaCorrelatividad FROM Correlatividades WHERE idFamiliaCursoBase = @CFid", connectionAlternativa))
                {
                    commandAlternativo.Parameters.AddWithValue("@CFid", CFid);

                    try
                    {
                        connectionAlternativa.Open();

                        using (var readerAlternativo = commandAlternativo.ExecuteReader())
                        {
                            while (readerAlternativo.Read())
                            {
                                string codigoFamiliaCorrelatividad = readerAlternativo["codigoFamiliaCorrelatividad"].ToString();
                                CFCorrelatividades.Add(codigoFamiliaCorrelatividad);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Error de conexión a la base de datos: " + ex.Message);
                    }
                }
            }

            return CFCorrelatividades;
        }

        private static Dictionary<string, DateTime> DevolverListaEsperaSegunCodigoCurso(string codigo)
        {
            Dictionary<string, DateTime> listaEspera = new Dictionary<string, DateTime>();

            using (var connectionAlternativa = new SqlConnection(@"Server = .; Database = TestSYSACAD; Trusted_Connection = True; Encrypt=False; TrustServerCertificate=True;"))
            {
                using (var commandAlternativo = new SqlCommand("SELECT legajoEstudiante, fechaIngreso FROM AlumnosEnListaDeEspera WHERE codigoCurso = @codigo", connectionAlternativa))
                {
                    commandAlternativo.Parameters.AddWithValue("@codigo", codigo);

                    try
                    {
                        connectionAlternativa.Open();

                        using (var readerAlternativo = commandAlternativo.ExecuteReader())
                        {
                            while (readerAlternativo.Read())
                            {
                                string legajoEstudiante = readerAlternativo["legajoEstudiante"].ToString();
                                DateTime fechaInscripcion = DateTime.Parse(readerAlternativo["fechaIngreso"].ToString());

                                listaEspera[legajoEstudiante] = fechaInscripcion;
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Error de conexión a la base de datos: " + ex.Message);
                    }
                }
            }

            return listaEspera;
        }

        internal static void CrearInstanciasDeCursoAPartirDeBD()
        {
            _listaCursos.Clear();

            try
            {
                connection.Open();

                command.CommandText = "SELECT C.*, T.nombre AS nombreTurno, D.nombre AS nombreDia, CF.codigo AS codigoCF FROM Curso C " +
                    "INNER JOIN Turno T ON C.turnoId = T.id " +
                    "INNER JOIN Dia D ON C.diaId = D.id " +
                    "INNER JOIN CodigoFamilia CF ON C.codigoFamiliaId = CF.id";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string nombre = reader["nombre"].ToString();
                        string codigo = reader["codigo"].ToString();
                        string descripcion = reader["descripcion"].ToString();
                        int cupoMaximo = Convert.ToInt32(reader["cupoMaximo"]);
                        int cupoDisponible = Convert.ToInt32(reader["cupoDisponible"]);
                        string turno = reader["nombreTurno"].ToString();
                        string aula = reader["aula"].ToString();
                        string dia = reader["nombreDia"].ToString();
                        Carrera carrera = (Carrera)Enum.Parse(typeof(Carrera), reader["carreraCodigo"].ToString());
                        int creditosRequeridos = Convert.ToInt32(reader["creditosRequeridos"]);
                        double promedioRequerido = Double.Parse(reader["promedioRequerido"].ToString());
                        string codigoFamilia = reader["codigoCF"].ToString();

                        List<string> codigosCorrelatividades = DevolverListaDeCorrelatividadesSegunCFId(Convert.ToInt32(reader["codigoFamiliaId"]));
                        Dictionary<string, DateTime> alumnosEnListaDeEspera = DevolverListaEsperaSegunCodigoCurso(codigo);

                        Curso cursoReconstruido = new Curso(nombre, codigo, descripcion,
                            cupoMaximo, cupoDisponible, turno, aula, dia, carrera, codigosCorrelatividades,
                            creditosRequeridos, promedioRequerido, codigoFamilia, alumnosEnListaDeEspera);

                        _listaCursos.Add(cursoReconstruido);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de conexión a la base de datos: " + ex.Message);
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        //////////////////////////////////CREATE
        private static int ObtenerTurnoIdSegunTexto(string nombreTurno)
        {
            using (var connectionAlternativa = new SqlConnection(@"Server = .; Database = TestSYSACAD; Trusted_Connection = True; Encrypt=False; TrustServerCertificate=True;"))
            {
                using (var commandAlternativo = new SqlCommand("SELECT * FROM Turno WHERE nombre = @nombre", connectionAlternativa))
                {
                    commandAlternativo.Parameters.AddWithValue("@nombre", nombreTurno);

                    try
                    {
                        connectionAlternativa.Open();

                        int idDelTurno = 0;

                        using (var readerAlternativo = commandAlternativo.ExecuteReader())
                        {
                            while (readerAlternativo.Read())
                            {
                                idDelTurno = Convert.ToInt32(readerAlternativo["id"]);
                            }
                        }

                        return idDelTurno;
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Error de conexión a la base de datos: " + ex.Message);
                    }
                }
            }
        }

        private static int ObtenerDiaIdSegunTexto(string nombreDia)
        {
            using (var connectionAlternativa = new SqlConnection(@"Server = .; Database = TestSYSACAD; Trusted_Connection = True; Encrypt=False; TrustServerCertificate=True;"))
            {
                using (var commandAlternativo = new SqlCommand("SELECT * FROM Dia WHERE nombre = @nombre", connectionAlternativa))
                {
                    commandAlternativo.Parameters.AddWithValue("@nombre", nombreDia);

                    try
                    {
                        connectionAlternativa.Open();

                        int idDelDia = 0;

                        using (var readerAlternativo = commandAlternativo.ExecuteReader())
                        {
                            while (readerAlternativo.Read())
                            {
                                idDelDia = Convert.ToInt32(readerAlternativo["id"]);
                            }
                        }

                        return idDelDia;
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Error de conexión a la base de datos: " + ex.Message);
                    }
                }
            }
        }

        private static int ObtenerIdDeCodigoFamilia(string codigoFamilia)
        {
            using (var connectionAlternativa = new SqlConnection(@"Server = .; Database = TestSYSACAD; Trusted_Connection = True; Encrypt=False; TrustServerCertificate=True;"))
            {
                using (var commandAlternativo = new SqlCommand("SELECT id FROM CodigoFamilia WHERE codigo = @codigo", connectionAlternativa))
                {
                    commandAlternativo.Parameters.AddWithValue("@codigo", codigoFamilia);

                    try
                    {
                        connectionAlternativa.Open();

                        using (var readerAlternativo = commandAlternativo.ExecuteReader())
                        {
                            if (readerAlternativo.Read())
                            {
                                return Convert.ToInt32(readerAlternativo["id"]);
                            }
                            else
                            {
                                // El código de familia no existe en la tabla, lo agrego y obtengo su ID.
                                return AgregarNuevoCodigoDeFamiliaYDevolverSuId(codigoFamilia);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Error de conexión a la base de datos: " + ex.Message);
                    }
                }
            }
        }

        private static int AgregarNuevoCodigoDeFamiliaYDevolverSuId(string codigoFamilia)
        {
            using (var connectionAlternativa2 = new SqlConnection(@"Server = .; Database = TestSYSACAD; Trusted_Connection = True; Encrypt=False; TrustServerCertificate=True;"))
            {
                using (var commandAlternativo2 = new SqlCommand("INSERT INTO CodigoFamilia (codigo) VALUES (@codigo); SELECT SCOPE_IDENTITY();", connectionAlternativa2))
                {
                    commandAlternativo2.Parameters.AddWithValue("@codigo", codigoFamilia);
                    connectionAlternativa2.Open();

                    try
                    {
                        // Ejecuta la consulta y devuelve el ID del código de familia recién agregado.
                        return Convert.ToInt32(commandAlternativo2.ExecuteScalar());
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Error de conexión a la base de datos: " + ex.Message);
                    }
                }
            }
        }

        internal static void IngresarCursoBD(Curso nuevoCurso)
        {
            try
            {
                connection.Open();

                // Secuencia INSERT
                command.CommandText = "INSERT INTO Curso (codigo, nombre, descripcion, cupoMaximo, cupoDisponible, turnoId, aula, diaId, carreraCodigo, creditosRequeridos, promedioRequerido, codigoFamiliaId) " +
                                        "VALUES (@codigo, @nombre, @descripcion, @cupoMaximo, @cupoDisponible, @turnoId, @aula, @diaId, @carreraCodigo, @creditosRequeridos, @promedioRequerido, @codigoFamiliaId)";

                command.Parameters.AddWithValue("@codigo", nuevoCurso.Codigo);
                command.Parameters.AddWithValue("@nombre", nuevoCurso.Nombre);
                command.Parameters.AddWithValue("@descripcion", nuevoCurso.Descripcion);
                command.Parameters.AddWithValue("@cupoMaximo", nuevoCurso.CupoMaximo);
                command.Parameters.AddWithValue("@cupoDisponible", nuevoCurso.CupoDisponible);
                command.Parameters.AddWithValue("@turnoId", ObtenerTurnoIdSegunTexto(nuevoCurso.Turno));
                command.Parameters.AddWithValue("@aula", nuevoCurso.Aula);
                command.Parameters.AddWithValue("@diaId", ObtenerDiaIdSegunTexto(nuevoCurso.Dia));
                command.Parameters.AddWithValue("@carreraCodigo", nuevoCurso.Carrera.ToString());
                command.Parameters.AddWithValue("@creditosRequeridos", nuevoCurso.CreditosRequeridos);
                command.Parameters.AddWithValue("@promedioRequerido", nuevoCurso.PromedioRequerido);
                command.Parameters.AddWithValue("@codigoFamiliaId", ObtenerIdDeCodigoFamilia(nuevoCurso.CodigoFamilia));

                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de conexión a la base de datos: " + ex.Message);
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }

            CrearInstanciasDeCursoAPartirDeBD();
        }

        /////////////////////////////////////READ
        internal static bool BuscarCursoBD(string codigo)
        {
            bool resultadoBusqueda = false;

            foreach (Curso curso in _listaCursos)
            {
                if (curso.Codigo == codigo)
                {
                    resultadoBusqueda = true;
                }
            }
            return resultadoBusqueda;
        }

        public static List<Curso> ObtenerUnCursoPorCadaCodigoDeFamilia()
        {
            // Almaceno un curso por cada código de familia dentro del diccionario
            Dictionary<string, Curso> cursosPorCodigoFamilia = new Dictionary<string, Curso>();

            // Recorro la lista de cursos
            foreach (Curso curso in _listaCursos)
            {
                // Si el diccionario no contiene el codigo de familia aun
                if (!cursosPorCodigoFamilia.ContainsKey(curso.CodigoFamilia))
                {
                    // Agrego el curso al diccionario junto a su codigo de familia
                    cursosPorCodigoFamilia[curso.CodigoFamilia] = curso;
                }
            }

            // Paso los valores del diccionario a una lista de cursos
            List<Curso> cursosUnicos = cursosPorCodigoFamilia.Values.ToList();

            // Devuelvo la lista de cursos únicos
            return cursosUnicos;
        }

        public static HashSet<string> ObtenerNombresDeCursosNoCorrelativos(Curso cursoSeleccionado)
        {
            HashSet<string> nombresAgregados = new HashSet<string>();

            foreach (Curso curso in _listaCursos)
            {
                // Si el curso iterado no se encuentra ya dentro de las correlatividades de la familia
                // del curso seleccionado y el curso iterado no se encuentra en la familia del
                // curso seleccionado.
                if (!cursoSeleccionado.Correlatividades.Contains(curso.CodigoFamilia) &&
                    curso.CodigoFamilia != cursoSeleccionado.CodigoFamilia)
                {
                    if (!nombresAgregados.Contains(curso.CodigoFamilia))
                    {
                        nombresAgregados.Add(curso.Nombre);
                    }
                }
            }

            return nombresAgregados;
        }

        public static Curso ObtenerCursoDesdeCodigo(string codigo)
        {
            foreach (Curso curso in _listaCursos)
            {
                if (curso.Codigo == codigo)
                {
                    return curso;
                }
            }

            return null;
        }

        public static string ObtenerCodigoDeFamiliaDesdeNombre(string nombre)
        {
            string codigoDeFamilia = "";

            foreach (Curso curso in _listaCursos)
            {
                if (curso.Nombre == nombre)
                {
                    codigoDeFamilia = curso.CodigoFamilia;
                }
            }

            return codigoDeFamilia;
        }

        public static List<Curso> ObtenerCursosDesdeCodigoDeFamilia(string codigoDeFamilia)
        {
            List<Curso> cursosEncontrados = new List<Curso>();

            foreach (Curso curso in _listaCursos)
            {
                if (curso.CodigoFamilia == codigoDeFamilia)
                {
                    cursosEncontrados.Add(curso);
                }
            }

            return cursosEncontrados;
        }

        public static Curso ObtenerCursoDesdeCodigoDeFamilia(string codigoDeFamilia)
        {
            foreach (Curso curso in _listaCursos)
            {
                if (curso.CodigoFamilia == codigoDeFamilia)
                {
                    return curso;
                }
            }

            return null;
        }

        public static Curso ObtenerCursoAPartirDeNombreYTurno(string nombre, string turno)
        {
            Curso cursoADevolver = null;

            foreach (Curso curso in _listaCursos)
            {
                if (curso.Nombre == nombre && curso.Turno == turno)
                {
                    cursoADevolver = curso;
                }
            }

            return cursoADevolver;
        }

        public static List<Curso> ObtenerListaCursosDesdeListaCodigos(List<string> listaCodigos)
        {
            List<Curso> cursos = new List<Curso>(); 

            foreach (Curso curso in _listaCursos)
            {
                if (listaCodigos.Contains(curso.Codigo))
                {
                    cursos.Add(curso);
                }
            }

            return cursos;
        }

        public static string ObtenerCodigoDeFamilia(string nombre)
        {
            string codigoDeFamilia = nombre.Trim().ToUpper();
            codigoDeFamilia = codigoDeFamilia.Replace(" ", "");

            return codigoDeFamilia;
        }

        ///////////////////////////////////UPDATE

        ///////UPDATE REQUISITOS
        private static void EliminarCorrelatividadesDeCursoSeleccionado(int idCodigoFamilia)
        {
            try
            {
                connection.Open();

                command.CommandText = @"DELETE FROM Correlatividades WHERE idFamiliaCursoBase = @CFId";

                command.Parameters.AddWithValue("@CFId", idCodigoFamilia);

                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de conexión a la base de datos: " + ex.Message);
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        private static void AgregarNuevasCorrelatividadesACursoSeleccionado(List<string> CFNuevasCorrelatividades)
        {
            foreach (string CF in CFNuevasCorrelatividades)
            {
                int idCodigoFamilia = ObtenerIdDeCodigoFamilia(CF);

                try
                {
                    connection.Open();

                    command.CommandText = @"INSERT INTO Correlatividades (idFamiliaCursoBase, codigoFamiliaCorrelatividad) VALUES (@id, @CF)";

                    command.Parameters.AddWithValue("@id", idCodigoFamilia);
                    command.Parameters.AddWithValue("@CF", CF);

                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error de conexión a la base de datos: " + ex.Message);
                }
                finally
                {
                    command.Parameters.Clear();
                    connection.Close();
                }
            }
        }

        public static void ActualizarRequisitosACursos(string CFCursoAModificar, List<string> CFcorrelatividades, int creditos, double promedio)
        {
            int idCodigoFamilia = ObtenerIdDeCodigoFamilia(CFCursoAModificar);

            try
            {
                connection.Open();

                command.CommandText = @"UPDATE Curso SET creditosRequeridos = @creditos, promedioRequerido = @promedio WHERE codigoFamiliaId = @CFId";

                command.Parameters.AddWithValue("@creditos", creditos);
                command.Parameters.AddWithValue("@promedio", promedio);
                command.Parameters.AddWithValue("@CFId", idCodigoFamilia);

                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de conexión a la base de datos: " + ex.Message);
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }

            EliminarCorrelatividadesDeCursoSeleccionado(idCodigoFamilia);
            AgregarNuevasCorrelatividadesACursoSeleccionado(CFcorrelatividades);

            CrearInstanciasDeCursoAPartirDeBD();
        }


        ///////UPDATE TABLA CURSOS

        public static void EditarCursoBD(string codigoABuscar, string nombre, string codigo, string descripcion, int cupoMaximo, string turno, string dia, string aula)
        {
            try
            {
                connection.Open();

                int idTurno = ObtenerTurnoIdSegunTexto(turno);
                int idDia = ObtenerDiaIdSegunTexto(dia);

                command.CommandText = "UPDATE Curso SET codigo = @codigo, nombre = @nombre, descripcion = @descripcion," +
                    "cupoMaximo = @cupoMaximo, cupoDisponible = @cupoMaximo, turnoId = @turnoId, aula = @aula, " +
                    "diaId = @diaId WHERE codigo = @codigoABuscar";

                command.Parameters.AddWithValue("@codigo", codigo);
                command.Parameters.AddWithValue("@nombre", nombre);
                command.Parameters.AddWithValue("@descripcion", descripcion);
                command.Parameters.AddWithValue("@cupoMaximo", cupoMaximo);
                command.Parameters.AddWithValue("@turnoId", idTurno);
                command.Parameters.AddWithValue("@aula", aula);
                command.Parameters.AddWithValue("@diaId", idDia);
                command.Parameters.AddWithValue("@codigoABuscar", codigoABuscar);

                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de conexión a la base de datos: " + ex.Message);
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }

            CrearInstanciasDeCursoAPartirDeBD();
        }

        public static void EditarCursoBD(string codigoABuscar, string nombre, string codigo, string descripcion, int cupoMaximo, string turno, string dia, string aula, string codigoFamilia)
        {
            try
            {
                connection.Open();

                int idTurno = ObtenerTurnoIdSegunTexto(turno);
                int idDia = ObtenerDiaIdSegunTexto(dia);
                int CFid = ObtenerIdDeCodigoFamilia(codigoFamilia);

                command.CommandText = "UPDATE Curso SET codigo = @codigo, nombre = @nombre, descripcion = @descripcion," +
                    "cupoMaximo = @cupoMaximo, cupoDisponible = @cupoMaximo, turnoId = @turnoId, aula = @aula, " +
                    "diaId = @diaId, codigoFamiliaId = @codigoFamilia WHERE codigo = @codigoABuscar";

                command.Parameters.AddWithValue("@codigo", codigo);
                command.Parameters.AddWithValue("@nombre", nombre);
                command.Parameters.AddWithValue("@descripcion", descripcion);
                command.Parameters.AddWithValue("@cupoMaximo", cupoMaximo);
                command.Parameters.AddWithValue("@turnoId", idTurno);
                command.Parameters.AddWithValue("@aula", aula);
                command.Parameters.AddWithValue("@diaId", idDia);
                command.Parameters.AddWithValue("@codigoFamilia", codigoFamilia);
                command.Parameters.AddWithValue("@codigoABuscar", codigoABuscar);

                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de conexión a la base de datos: " + ex.Message);
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }

            CrearInstanciasDeCursoAPartirDeBD();
        }

        ///////UPDATE TABLA ALUMNOS EN LISTA DE ESPERA
        private static void EliminarListaEsperaDeCursoSeleccionado(string codigoCurso)
        {
            try
            {
                connection.Open();

                command.CommandText = @"DELETE FROM AlumnosEnListaDeEspera WHERE codigoCurso = @codigoCurso";

                command.Parameters.AddWithValue("@codigoCurso", codigoCurso);

                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de conexión a la base de datos: " + ex.Message);
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        private static void AgregarNuevaListaEsperaACursoSeleccionado(string codigoCurso, Dictionary<string, DateTime> listaEsperaRecibida)
        {
            try
            {
                connection.Open();

                foreach (var parKeyValue in listaEsperaRecibida)
                {
                    command.CommandText = @"INSERT INTO AlumnosEnListaDeEspera (legajoEstudiante, codigoCurso, fechaIngreso) VALUES (@legajo, @codigoCurso, @fechaIngreso)";

                    command.Parameters.Clear(); // Limpia los parámetros anteriores

                    command.Parameters.AddWithValue("@legajo", parKeyValue.Key);
                    command.Parameters.AddWithValue("@codigoCurso", codigoCurso);
                    command.Parameters.AddWithValue("@fechaIngreso", parKeyValue.Value);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de conexión a la base de datos: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }




            //foreach (var parKeyValue in listaEsperaRecibida)
            //{
            //    try
            //    {
            //        connection.Open();

            //        command.CommandText = @"INSERT INTO AlumnosEnListaDeEspera (legajoEstudiante, codigoCurso, fechaIngreso) VALUES (@legajo, @codigoCurso, @fechaIngreso)";

            //        command.Parameters.AddWithValue("@legajo", listaEsperaRecibida.Keys);
            //        command.Parameters.AddWithValue("@codigoCurso", codigoCurso);
            //        command.Parameters.AddWithValue("@fechaIngreso", listaEsperaRecibida.Values);

            //        command.ExecuteNonQuery();
            //    }
            //    catch (SqlException ex)
            //    {
            //        throw new Exception("Error de conexión a la base de datos: " + ex.Message);
            //    }
            //    finally
            //    {
            //        command.Parameters.Clear();
            //        connection.Close();
            //    }
            //}
        }

        public static void ActualizarListaDeEsperaDeCurso(Curso cursoRecibido, Dictionary<string, DateTime> listaEsperaRecibida)
        {
            EliminarListaEsperaDeCursoSeleccionado(cursoRecibido.Codigo);
            AgregarNuevaListaEsperaACursoSeleccionado(cursoRecibido.Codigo, listaEsperaRecibida);

            CrearInstanciasDeCursoAPartirDeBD();
        }

        ////// AGREGAR ESTUDIANTE A LISTA DE ESPERA
        internal static void AgregarEstudianteAListaDeEspera(string codigoCurso, string legajoEstudiante)
        {
            try
            {
                connection.Open();

                command.CommandText = @"INSERT INTO AlumnosEnListaDeEspera (legajoEstudiante, codigoCurso, fechaIngreso) VALUES (@legajo, @codigoCurso, @fechaIngreso)";

                command.Parameters.AddWithValue("@legajo", legajoEstudiante);
                command.Parameters.AddWithValue("@codigoCurso", codigoCurso);
                command.Parameters.AddWithValue("@fechaIngreso", DateTime.Now);

                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de conexión a la base de datos: " + ex.Message);
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }

            CrearInstanciasDeCursoAPartirDeBD();
        }


        ///////UPDATE UNICAMENTE CUPOS DISPONIBLES
        internal static void RestarCupoDisponible(Curso cursoARestarCupo)
        {
            try
            {
                connection.Open();

                command.CommandText = "UPDATE Curso SET cupoDisponible = cupoDisponible - 1 WHERE codigo = @codigoCurso";

                command.Parameters.AddWithValue("@codigoCurso", cursoARestarCupo.Codigo);

                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de conexión a la base de datos: " + ex.Message);
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }

            CrearInstanciasDeCursoAPartirDeBD();
        }

        ///////////////////////DELETE
        private static void CorroborarSiHayCFSinCursosCorrespondientesYBorrarlos()
        {
            try
            {
                connection.Open();

                command.CommandText = "DELETE FROM CodigoFamilia WHERE id NOT IN (SELECT DISTINCT codigoFamiliaId FROM Curso)";

                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de conexión a la base de datos: " + ex.Message);
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }
        }

        /// <summary>
        /// Elimina un curso de la base de datos por su código.
        /// </summary>
        /// <param name="codigoABuscar">El código del curso a ser eliminado.</param>
        public static void EliminarCursoBD(string codigoABuscar)
        {
            try
            {
                connection.Open();

                command.CommandText = @"DELETE FROM Curso WHERE codigo = @codigoABuscar";

                command.Parameters.AddWithValue("@codigoABuscar", codigoABuscar);

                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de conexión a la base de datos: " + ex.Message);
            }
            finally
            {
                command.Parameters.Clear();
                connection.Close();
            }

            //ConsultasInscripciones.EliminarCursoATodosLosEstudiantes(codigoABuscar);
            CorroborarSiHayCFSinCursosCorrespondientesYBorrarlos();
            CrearInstanciasDeCursoAPartirDeBD();
            ConsultasInscripciones.RefrescarEstudianteLogueado();
        }

        //Getters y setters
        public static List<Curso> Cursos { get { return _listaCursos; } }
    }
}
