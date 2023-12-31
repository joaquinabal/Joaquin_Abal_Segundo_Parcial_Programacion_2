USE [master]
GO
/****** Object:  Database [TestSYSACAD]    Script Date: 26/11/2023 14:05:03 ******/
CREATE DATABASE [TestSYSACAD]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TestSYSACAD', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\TestSYSACAD.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TestSYSACAD_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\TestSYSACAD_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [TestSYSACAD] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TestSYSACAD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TestSYSACAD] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TestSYSACAD] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TestSYSACAD] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TestSYSACAD] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TestSYSACAD] SET ARITHABORT OFF 
GO
ALTER DATABASE [TestSYSACAD] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TestSYSACAD] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TestSYSACAD] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TestSYSACAD] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TestSYSACAD] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TestSYSACAD] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TestSYSACAD] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TestSYSACAD] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TestSYSACAD] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TestSYSACAD] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TestSYSACAD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TestSYSACAD] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TestSYSACAD] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TestSYSACAD] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TestSYSACAD] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TestSYSACAD] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TestSYSACAD] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TestSYSACAD] SET RECOVERY FULL 
GO
ALTER DATABASE [TestSYSACAD] SET  MULTI_USER 
GO
ALTER DATABASE [TestSYSACAD] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TestSYSACAD] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TestSYSACAD] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TestSYSACAD] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TestSYSACAD] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TestSYSACAD] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TestSYSACAD', N'ON'
GO
ALTER DATABASE [TestSYSACAD] SET QUERY_STORE = ON
GO
ALTER DATABASE [TestSYSACAD] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [TestSYSACAD]
GO
/****** Object:  Table [dbo].[Administrador]    Script Date: 26/11/2023 14:05:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Administrador](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[correo] [varchar](70) NULL,
	[contrasenia] [varchar](255) NULL,
 CONSTRAINT [PK_Administrador] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AlumnosEnListaDeEspera]    Script Date: 26/11/2023 14:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AlumnosEnListaDeEspera](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[legajoEstudiante] [varchar](50) NULL,
	[codigoCurso] [varchar](50) NULL,
	[fechaIngreso] [datetime] NULL,
 CONSTRAINT [PK_AlumnosEnListaDeEspera] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carrera]    Script Date: 26/11/2023 14:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carrera](
	[codigo] [varchar](10) NOT NULL,
	[nombre] [varchar](70) NULL,
 CONSTRAINT [PK_Carrera] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CodigoFamilia]    Script Date: 26/11/2023 14:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CodigoFamilia](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codigo] [varchar](70) NULL,
 CONSTRAINT [PK_CodigoFamilia] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConceptoDePago]    Script Date: 26/11/2023 14:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConceptoDePago](
	[nombre] [varchar](255) NOT NULL,
	[montoInicial] [float] NULL,
 CONSTRAINT [PK_ConceptoDePago] PRIMARY KEY CLUSTERED 
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConceptosDePagoDeEstudiante]    Script Date: 26/11/2023 14:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConceptosDePagoDeEstudiante](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[legajoEstudiante] [varchar](50) NULL,
	[conceptoNombre] [varchar](255) NULL,
	[montoPendiente] [float] NULL,
 CONSTRAINT [PK_ConceptosDePagoDeEstudiante] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Correlatividades]    Script Date: 26/11/2023 14:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Correlatividades](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idFamiliaCursoBase] [int] NULL,
	[codigoFamiliaCorrelatividad] [varchar](70) NULL,
 CONSTRAINT [PK_Correlatividades] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Curso]    Script Date: 26/11/2023 14:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Curso](
	[codigo] [varchar](50) NOT NULL,
	[nombre] [varchar](255) NULL,
	[descripcion] [text] NULL,
	[cupoMaximo] [int] NULL,
	[cupoDisponible] [int] NULL,
	[turnoId] [int] NULL,
	[aula] [varchar](20) NULL,
	[diaId] [int] NULL,
	[carreraCodigo] [varchar](10) NULL,
	[creditosRequeridos] [int] NULL,
	[promedioRequerido] [float] NULL,
	[codigoFamiliaId] [int] NULL,
 CONSTRAINT [PK_Curso] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dia]    Script Date: 26/11/2023 14:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dia](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](15) NULL,
 CONSTRAINT [PK_Dia] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estudiante]    Script Date: 26/11/2023 14:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estudiante](
	[legajo] [varchar](50) NOT NULL,
	[nombre] [varchar](50) NULL,
	[direccion] [varchar](100) NULL,
	[numeroTelefono] [varchar](20) NULL,
	[correo] [varchar](70) NULL,
	[contrasenia] [varchar](255) NULL,
	[identificadorUnico] [char](36) NULL,
	[debeCambiarContrasenia] [bit] NULL,
	[creditos] [int] NULL,
	[promedio] [float] NULL,
 CONSTRAINT [PK_Estudiante] PRIMARY KEY CLUSTERED 
(
	[legajo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstudianteCurso]    Script Date: 26/11/2023 14:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstudianteCurso](
	[legajoEstudiante] [varchar](50) NULL,
	[codigoCurso] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Profesores]    Script Date: 26/11/2023 14:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profesores](
	[nombre] [varchar](50) NULL,
	[direccion] [varchar](50) NULL,
	[telefono] [varchar](50) NULL,
	[correo] [varchar](70) NOT NULL,
	[especializacion] [varchar](70) NULL,
 CONSTRAINT [PK_Profesores] PRIMARY KEY CLUSTERED 
(
	[correo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProfesoresEnCursos]    Script Date: 26/11/2023 14:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfesoresEnCursos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[correoProfesor] [varchar](70) NULL,
	[codigoCurso] [varchar](50) NULL,
 CONSTRAINT [PK_ProfesoresEnCursos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegistroCursosCompletados]    Script Date: 26/11/2023 14:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegistroCursosCompletados](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[legajo] [varchar](50) NULL,
	[codigoFamiliaCurso] [varchar](70) NULL,
 CONSTRAINT [PK_RegistroCursosCompletados] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegistroDePago]    Script Date: 26/11/2023 14:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegistroDePago](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[legajoEstudiante] [varchar](50) NULL,
	[conceptoNombre] [varchar](255) NULL,
	[ingreso] [float] NULL,
	[fechaPago] [datetime] NULL,
 CONSTRAINT [PK_RegistroDePago] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegistroInscripcion]    Script Date: 26/11/2023 14:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegistroInscripcion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[legajo] [varchar](50) NULL,
	[codigoCurso] [varchar](50) NULL,
	[fechaInscripcion] [datetime] NULL,
 CONSTRAINT [PK_RegistroInscripcion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Turno]    Script Date: 26/11/2023 14:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Turno](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](20) NULL,
 CONSTRAINT [PK_Turno] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Administrador] ON 

INSERT [dbo].[Administrador] ([id], [correo], [contrasenia]) VALUES (1, N'johntravolta@hotmail.com', N'94edf28c6d6da38fd35d7ad53e485307f89fbeaf120485c8d17a43f323deee71')
SET IDENTITY_INSERT [dbo].[Administrador] OFF
GO
INSERT [dbo].[Carrera] ([codigo], [nombre]) VALUES (N'TUP', N'Tecnicatura Universitaria en Programacion')
INSERT [dbo].[Carrera] ([codigo], [nombre]) VALUES (N'TUSI', N'Tecnicatura Universitaria en Sistemas Informaticos')
GO
SET IDENTITY_INSERT [dbo].[CodigoFamilia] ON 

INSERT [dbo].[CodigoFamilia] ([id], [codigo]) VALUES (1, N'MATEMATICA')
INSERT [dbo].[CodigoFamilia] ([id], [codigo]) VALUES (2, N'SISTEMASDEPROCESAMIENTODEDATOS')
INSERT [dbo].[CodigoFamilia] ([id], [codigo]) VALUES (3, N'INGLES1')
INSERT [dbo].[CodigoFamilia] ([id], [codigo]) VALUES (4, N'PROGRAMACION1')
INSERT [dbo].[CodigoFamilia] ([id], [codigo]) VALUES (5, N'LABORATORIO1')
INSERT [dbo].[CodigoFamilia] ([id], [codigo]) VALUES (6, N'ARQUITECTURAYSISTEMASOPERATIVOS')
INSERT [dbo].[CodigoFamilia] ([id], [codigo]) VALUES (7, N'ESTADISTICA')
INSERT [dbo].[CodigoFamilia] ([id], [codigo]) VALUES (8, N'INGLES2')
INSERT [dbo].[CodigoFamilia] ([id], [codigo]) VALUES (9, N'PROGRAMACION2')
INSERT [dbo].[CodigoFamilia] ([id], [codigo]) VALUES (10, N'LABORATORIO2')
INSERT [dbo].[CodigoFamilia] ([id], [codigo]) VALUES (11, N'METODOLOGIADELAINVESTIGACION')
INSERT [dbo].[CodigoFamilia] ([id], [codigo]) VALUES (12, N'METODOLOGIADESISTEMAS1')
INSERT [dbo].[CodigoFamilia] ([id], [codigo]) VALUES (13, N'PROGRAMACIONAVANZADA1')
INSERT [dbo].[CodigoFamilia] ([id], [codigo]) VALUES (14, N'REDES')
INSERT [dbo].[CodigoFamilia] ([id], [codigo]) VALUES (15, N'INGLESTECNICOAVANZADO1')
INSERT [dbo].[CodigoFamilia] ([id], [codigo]) VALUES (16, N'PROYECTOSINFORMATICOS')
INSERT [dbo].[CodigoFamilia] ([id], [codigo]) VALUES (17, N'SEMINARIO')
SET IDENTITY_INSERT [dbo].[CodigoFamilia] OFF
GO
INSERT [dbo].[ConceptoDePago] ([nombre], [montoInicial]) VALUES (N'Bibliografia Primer Cuatrimestre', 50000)
INSERT [dbo].[ConceptoDePago] ([nombre], [montoInicial]) VALUES (N'Cargos Administrativos primer cuatrimestre', 50000)
INSERT [dbo].[ConceptoDePago] ([nombre], [montoInicial]) VALUES (N'Matricula de Ingreso', 200000)
INSERT [dbo].[ConceptoDePago] ([nombre], [montoInicial]) VALUES (N'Matricula del Primer Cuatrimestre', 150000)
GO
SET IDENTITY_INSERT [dbo].[ConceptosDePagoDeEstudiante] ON 

INSERT [dbo].[ConceptosDePagoDeEstudiante] ([id], [legajoEstudiante], [conceptoNombre], [montoPendiente]) VALUES (7, N'12543658', N'Matricula de Ingreso', 20000)
INSERT [dbo].[ConceptosDePagoDeEstudiante] ([id], [legajoEstudiante], [conceptoNombre], [montoPendiente]) VALUES (8, N'12543658', N'Matricula del Primer Cuatrimestre', 15000)
INSERT [dbo].[ConceptosDePagoDeEstudiante] ([id], [legajoEstudiante], [conceptoNombre], [montoPendiente]) VALUES (9, N'12543658', N'Cargos Administrativos primer cuatrimestre', 5000)
INSERT [dbo].[ConceptosDePagoDeEstudiante] ([id], [legajoEstudiante], [conceptoNombre], [montoPendiente]) VALUES (10, N'12543658', N'Bibliografia Primer Cuatrimestre', 5000)
SET IDENTITY_INSERT [dbo].[ConceptosDePagoDeEstudiante] OFF
GO
SET IDENTITY_INSERT [dbo].[Correlatividades] ON 

INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (1, 6, N'MATEMATICA')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (2, 6, N'SISTEMASDEPROCESAMIENTODEDATOS')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (3, 6, N'INGLES1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (4, 6, N'PROGRAMACION1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (5, 6, N'LABORATORIO1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (6, 7, N'MATEMATICA')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (7, 7, N'SISTEMASDEPROCESAMIENTODEDATOS')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (8, 7, N'INGLES1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (9, 7, N'PROGRAMACION1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (10, 7, N'LABORATORIO1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (11, 8, N'MATEMATICA')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (12, 8, N'SISTEMASDEPROCESAMIENTODEDATOS')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (13, 8, N'INGLES1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (14, 8, N'PROGRAMACION1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (15, 8, N'LABORATORIO1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (16, 9, N'MATEMATICA')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (17, 9, N'SISTEMASDEPROCESAMIENTODEDATOS')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (18, 9, N'INGLES1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (19, 9, N'PROGRAMACION1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (20, 9, N'LABORATORIO1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (21, 10, N'MATEMATICA')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (22, 10, N'SISTEMASDEPROCESAMIENTODEDATOS')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (23, 10, N'INGLES1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (24, 10, N'PROGRAMACION1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (25, 10, N'LABORATORIO1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (26, 11, N'MATEMATICA')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (27, 11, N'SISTEMASDEPROCESAMIENTODEDATOS')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (28, 11, N'INGLES1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (29, 11, N'PROGRAMACION1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (30, 11, N'LABORATORIO1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (31, 12, N'MATEMATICA')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (32, 12, N'SISTEMASDEPROCESAMIENTODEDATOS')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (33, 12, N'INGLES1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (34, 12, N'PROGRAMACION1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (35, 12, N'LABORATORIO1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (36, 12, N'ARQUITECTURAYSISTEMASOPERATIVOS')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (37, 12, N'ESTADISTICA')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (38, 12, N'INGLES2')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (39, 12, N'PROGRAMACION2')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (40, 12, N'LABORATORIO2')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (41, 12, N'METODOLOGIADELAINVESTIGACION')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (42, 13, N'MATEMATICA')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (43, 13, N'SISTEMASDEPROCESAMIENTODEDATOS')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (44, 13, N'INGLES1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (45, 13, N'PROGRAMACION1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (46, 13, N'LABORATORIO1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (47, 13, N'ARQUITECTURAYSISTEMASOPERATIVOS')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (48, 13, N'ESTADISTICA')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (49, 13, N'INGLES2')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (50, 13, N'PROGRAMACION2')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (51, 13, N'LABORATORIO2')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (52, 13, N'METODOLOGIADELAINVESTIGACION')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (53, 14, N'MATEMATICA')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (54, 14, N'SISTEMASDEPROCESAMIENTODEDATOS')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (55, 14, N'INGLES1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (56, 14, N'PROGRAMACION1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (57, 14, N'LABORATORIO1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (58, 14, N'ARQUITECTURAYSISTEMASOPERATIVOS')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (59, 14, N'ESTADISTICA')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (60, 14, N'INGLES2')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (61, 14, N'PROGRAMACION2')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (62, 14, N'LABORATORIO2')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (63, 14, N'METODOLOGIADELAINVESTIGACION')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (64, 15, N'MATEMATICA')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (65, 15, N'SISTEMASDEPROCESAMIENTODEDATOS')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (66, 15, N'INGLES1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (67, 15, N'PROGRAMACION1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (68, 15, N'LABORATORIO1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (69, 15, N'ARQUITECTURAYSISTEMASOPERATIVOS')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (70, 15, N'ESTADISTICA')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (71, 15, N'INGLES2')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (72, 15, N'PROGRAMACION2')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (73, 15, N'LABORATORIO2')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (74, 15, N'METODOLOGIADELAINVESTIGACION')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (75, 16, N'MATEMATICA')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (76, 16, N'SISTEMASDEPROCESAMIENTODEDATOS')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (77, 16, N'INGLES1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (78, 16, N'PROGRAMACION1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (79, 16, N'LABORATORIO1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (80, 16, N'ARQUITECTURAYSISTEMASOPERATIVOS')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (81, 16, N'ESTADISTICA')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (82, 16, N'INGLES2')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (83, 16, N'PROGRAMACION2')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (84, 16, N'LABORATORIO2')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (85, 16, N'METODOLOGIADELAINVESTIGACION')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (86, 17, N'MATEMATICA')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (87, 17, N'SISTEMASDEPROCESAMIENTODEDATOS')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (88, 17, N'INGLES1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (89, 17, N'PROGRAMACION1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (90, 17, N'LABORATORIO1')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (91, 17, N'ARQUITECTURAYSISTEMASOPERATIVOS')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (92, 17, N'ESTADISTICA')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (93, 17, N'INGLES2')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (94, 17, N'PROGRAMACION2')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (95, 17, N'LABORATORIO2')
INSERT [dbo].[Correlatividades] ([id], [idFamiliaCursoBase], [codigoFamiliaCorrelatividad]) VALUES (96, 17, N'METODOLOGIADELAINVESTIGACION')
SET IDENTITY_INSERT [dbo].[Correlatividades] OFF
GO
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'AySOM', N'Arquitectura y Sistemas Operativos', N'Curso de Arquitectura y Sistemas Operativos en turno Mañana', 105, 105, 1, N'101', 1, N'TUP', 150, 6.5, 6)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'AySON', N'Arquitectura y Sistemas Operativos', N'Curso de Arquitectura y Sistemas Operativos en turno Noche', 105, 105, 3, N'333', 1, N'TUP', 150, 6.5, 6)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'AySOT', N'Arquitectura y Sistemas Operativos', N'Curso de Arquitectura y Sistemas Operativos en turno Tarde', 105, 105, 2, N'222', 1, N'TUP', 150, 6.5, 6)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'EstaM', N'Estadistica', N'Curso de Estadistica en turno Mañana', 132, 132, 1, N'111', 2, N'TUP', 160, 4.8, 7)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'EstaN', N'Estadistica', N'Curso de Estadistica en turno Noche', 132, 132, 3, N'354', 2, N'TUP', 160, 4.8, 7)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'EstaT', N'Estadistica', N'Curso de Estadistica en turno Tarde', 132, 132, 2, N'220', 2, N'TUP', 160, 4.8, 7)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'Ingl1M', N'Ingles 1', N'Curso de Ingles 1 en turno Mañana', 125, 125, 1, N'121', 3, N'TUP', 0, 0, 3)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'Ingl1N', N'Ingles 1', N'Curso de Ingles 1 en turno Noche', 125, 124, 3, N'321', 3, N'TUP', 0, 0, 3)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'Ingl1T', N'Ingles 1', N'Curso de Ingles 1 en turno Tarde', 125, 125, 2, N'221', 3, N'TUP', 0, 0, 3)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'Ingl2M', N'Ingles 2', N'Curso de Ingles 2 en turno Mañana', 145, 145, 1, N'114', 3, N'TUP', 80, 5.9, 8)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'Ingl2N', N'Ingles 2', N'Curso de Ingles 2 en turno Noche', 145, 145, 3, N'332', 3, N'TUP', 80, 5.9, 8)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'Ingl2T', N'Ingles 2', N'Curso de Ingles 2 en turno Tarde', 145, 145, 2, N'252', 3, N'TUP', 80, 5.9, 8)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'ITA1M', N'Ingles Tecnico Avanzado 1', N'Curso de Ingles Tecnico Avanzado 1 en turno Mañana', 93, 93, 1, N'125', 5, N'TUSI', 100, 4.5, 15)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'ITA1N', N'Ingles Tecnico Avanzado 1', N'Curso de Ingles Tecnico Avanzado 1 en turno Noche', 93, 93, 3, N'392', 5, N'TUSI', 100, 4.5, 15)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'ITA1T', N'Ingles Tecnico Avanzado 1', N'Curso de Ingles Tecnico Avanzado 1 en turno Tarde', 93, 93, 2, N'265', 5, N'TUSI', 100, 4.5, 15)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'Labo1M', N'Laboratorio 1', N'Curso de Laboratorio 1 en turno Mañana', 135, 134, 1, N'105', 5, N'TUP', 0, 0, 5)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'Labo1N', N'Laboratorio 1', N'Curso de Laboratorio 1 en turno Noche', 135, 134, 3, N'352', 5, N'TUP', 0, 0, 5)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'Labo1T', N'Laboratorio 1', N'Curso de Laboratorio 1 en turno Tarde', 135, 133, 2, N'261', 5, N'TUP', 0, 0, 5)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'Labo2M', N'Laboratorio 2', N'Curso de Laboratorio 2 en turno Mañana', 120, 120, 1, N'116', 5, N'TUP', 160, 8.5, 10)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'Labo2N', N'Laboratorio 2', N'Curso de Laboratorio 2 en turno Noche', 120, 120, 3, N'392', 5, N'TUP', 160, 8.5, 10)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'Labo2T', N'Laboratorio 2', N'Curso de Laboratorio 2 en turno Tarde', 120, 120, 2, N'281', 5, N'TUP', 160, 8.5, 10)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'MateM', N'Matematica', N'Curso de Matematica en turno Mañana', 150, 149, 1, N'101', 1, N'TUP', 0, 0, 1)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'MateN', N'Matematica', N'Curso de Matematica en turno Noche', 150, 150, 3, N'310', 1, N'TUP', 0, 0, 1)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'MateT', N'Matematica', N'Curso de Matematica en turno Tarde', 150, 149, 2, N'205', 1, N'TUP', 0, 0, 1)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'MdlIM', N'Metodologia de la Investigacion', N'Curso de Metodologia de la Investigacion en turno Mañana', 133, 133, 1, N'126', 1, N'TUP', 95, 6.2, 11)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'MdlIN', N'Metodologia de la Investigacion', N'Curso de Metodologia de la Investigacion en turno Noche', 133, 133, 3, N'351', 1, N'TUP', 95, 6.2, 11)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'MdlIT', N'Metodologia de la Investigacion', N'Curso de Metodologia de la Investigacion en turno Tarde', 133, 133, 2, N'299', 1, N'TUP', 95, 6.2, 11)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'MdS1M', N'Metodologia de Sistemas 1', N'Curso de Metodologia de Sistemas 1 en turno Mañana', 123, 123, 1, N'115', 2, N'TUSI', 200, 7.2, 12)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'MdS1N', N'Metodologia de Sistemas 1', N'Curso de Metodologia de Sistemas 1 en turno Noche', 123, 123, 3, N'323', 2, N'TUSI', 200, 7.2, 12)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'MdS1T', N'Metodologia de Sistemas 1', N'Curso de Metodologia de Sistemas 1 en turno Tarde', 123, 123, 2, N'212', 2, N'TUSI', 200, 7.2, 12)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'PA1M', N'Programacion Avanzada 1', N'Curso de Programacion Avanzada 1 en turno Mañana', 143, 143, 1, N'155', 3, N'TUSI', 220, 7.6, 13)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'PA1N', N'Programacion Avanzada 1', N'Curso de Programacion Avanzada 1 en turno Noche', 143, 143, 3, N'353', 3, N'TUSI', 220, 7.6, 13)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'PA1T', N'Programacion Avanzada 1', N'Curso de Programacion Avanzada 1 en turno Tarde', 143, 143, 2, N'252', 3, N'TUSI', 220, 7.6, 13)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'PIM', N'Proyectos Informaticos', N'Curso de Proyectos Informaticos en turno Mañana', 85, 85, 1, N'156', 1, N'TUSI', 300, 7.1, 16)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'PIN', N'Proyectos Informaticos', N'Curso de Proyectos Informaticos en turno Noche', 85, 85, 3, N'365', 1, N'TUSI', 300, 7.1, 16)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'PIT', N'Proyectos Informaticos', N'Curso de Proyectos Informaticos en turno Tarde', 85, 85, 2, N'284', 1, N'TUSI', 300, 7.1, 16)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'Prog1M', N'Programacion 1', N'Curso de Programacion 1 en turno Mañana', 95, 95, 1, N'122', 4, N'TUP', 0, 0, 4)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'Prog1N', N'Programacion 1', N'Curso de Programacion 1 en turno Noche', 95, 95, 3, N'314', 4, N'TUP', 0, 0, 4)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'Prog1T', N'Programacion 1', N'Curso de Programacion 1 en turno Tarde', 95, 95, 2, N'235', 4, N'TUP', 0, 0, 4)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'Prog2M', N'Programacion 2', N'Curso de Programacion 2 en turno Mañana', 85, 85, 1, N'113', 4, N'TUP', 90, 6.4, 9)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'Prog2N', N'Programacion 2', N'Curso de Programacion 2 en turno Noche', 85, 85, 3, N'395', 4, N'TUP', 90, 6.4, 9)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'Prog2T', N'Programacion 2', N'Curso de Programacion 2 en turno Tarde', 85, 85, 2, N'261', 4, N'TUP', 90, 6.4, 9)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'RedeM', N'Redes', N'Curso de Redes en turno Mañana', 73, 73, 1, N'115', 4, N'TUSI', 210, 3.2, 14)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'RedeN', N'Redes', N'Curso de Redes en turno Noche', 73, 73, 3, N'354', 4, N'TUSI', 210, 3.2, 14)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'RedeT', N'Redes', N'Curso de Redes en turno Tarde', 73, 73, 2, N'219', 4, N'TUSI', 210, 3.2, 14)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'SdPdDM', N'Sistemas de Procesamiento de Datos', N'Curso de Sistemas de Procesamiento de Datos en turno Mañana', 110, 110, 1, N'115', 2, N'TUP', 0, 0, 2)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'SdPdDN', N'Sistemas de Procesamiento de Datos', N'Curso de Sistemas de Procesamiento de Datos en turno Noche', 110, 110, 3, N'121', 2, N'TUP', 0, 0, 2)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'SdPdDT', N'Sistemas de Procesamiento de Datos', N'Curso de Sistemas de Procesamiento de Datos en turno Tarde', 110, 110, 2, N'125', 2, N'TUP', 0, 0, 2)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'SemiM', N'Seminario', N'Curso de Seminario en turno Mañana', 98, 98, 1, N'165', 2, N'TUSI', 90, 6.2, 17)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'SemiN', N'Seminario', N'Curso de Seminario en turno Noche', 98, 98, 3, N'368', 2, N'TUSI', 90, 6.2, 17)
INSERT [dbo].[Curso] ([codigo], [nombre], [descripcion], [cupoMaximo], [cupoDisponible], [turnoId], [aula], [diaId], [carreraCodigo], [creditosRequeridos], [promedioRequerido], [codigoFamiliaId]) VALUES (N'SemiT', N'Seminario', N'Curso de Seminario en turno Tarde', 98, 98, 2, N'251', 2, N'TUSI', 90, 6.2, 17)
GO
SET IDENTITY_INSERT [dbo].[Dia] ON 

INSERT [dbo].[Dia] ([id], [nombre]) VALUES (1, N'Lunes')
INSERT [dbo].[Dia] ([id], [nombre]) VALUES (2, N'Martes')
INSERT [dbo].[Dia] ([id], [nombre]) VALUES (3, N'Miercoles')
INSERT [dbo].[Dia] ([id], [nombre]) VALUES (4, N'Jueves')
INSERT [dbo].[Dia] ([id], [nombre]) VALUES (5, N'Viernes')
SET IDENTITY_INSERT [dbo].[Dia] OFF
GO
INSERT [dbo].[Estudiante] ([legajo], [nombre], [direccion], [numeroTelefono], [correo], [contrasenia], [identificadorUnico], [debeCambiarContrasenia], [creditos], [promedio]) VALUES (N'12543658', N'Pepe Peposo', N'Av Santa Fe 1241', N'1132519841', N'aaaa@hotmail.com', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'00000000-0000-0000-0000-000000000000', 0, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[Turno] ON 

INSERT [dbo].[Turno] ([id], [nombre]) VALUES (1, N'Mañana')
INSERT [dbo].[Turno] ([id], [nombre]) VALUES (2, N'Tarde')
INSERT [dbo].[Turno] ([id], [nombre]) VALUES (3, N'Noche')
SET IDENTITY_INSERT [dbo].[Turno] OFF
GO
ALTER TABLE [dbo].[AlumnosEnListaDeEspera]  WITH CHECK ADD  CONSTRAINT [FK_AlumnosEnListaDeEspera_Curso] FOREIGN KEY([codigoCurso])
REFERENCES [dbo].[Curso] ([codigo])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AlumnosEnListaDeEspera] CHECK CONSTRAINT [FK_AlumnosEnListaDeEspera_Curso]
GO
ALTER TABLE [dbo].[ConceptosDePagoDeEstudiante]  WITH CHECK ADD  CONSTRAINT [FK_ConceptosDePagoDeEstudiante_ConceptoDePago] FOREIGN KEY([conceptoNombre])
REFERENCES [dbo].[ConceptoDePago] ([nombre])
GO
ALTER TABLE [dbo].[ConceptosDePagoDeEstudiante] CHECK CONSTRAINT [FK_ConceptosDePagoDeEstudiante_ConceptoDePago]
GO
ALTER TABLE [dbo].[ConceptosDePagoDeEstudiante]  WITH CHECK ADD  CONSTRAINT [FK_ConceptosDePagoDeEstudiante_Estudiante] FOREIGN KEY([legajoEstudiante])
REFERENCES [dbo].[Estudiante] ([legajo])
GO
ALTER TABLE [dbo].[ConceptosDePagoDeEstudiante] CHECK CONSTRAINT [FK_ConceptosDePagoDeEstudiante_Estudiante]
GO
ALTER TABLE [dbo].[Correlatividades]  WITH CHECK ADD  CONSTRAINT [FK_Correlatividades_CodigoFamilia] FOREIGN KEY([idFamiliaCursoBase])
REFERENCES [dbo].[CodigoFamilia] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Correlatividades] CHECK CONSTRAINT [FK_Correlatividades_CodigoFamilia]
GO
ALTER TABLE [dbo].[Curso]  WITH CHECK ADD  CONSTRAINT [FK_Curso_Carrera] FOREIGN KEY([carreraCodigo])
REFERENCES [dbo].[Carrera] ([codigo])
GO
ALTER TABLE [dbo].[Curso] CHECK CONSTRAINT [FK_Curso_Carrera]
GO
ALTER TABLE [dbo].[Curso]  WITH CHECK ADD  CONSTRAINT [FK_Curso_CodigoFamilia] FOREIGN KEY([codigoFamiliaId])
REFERENCES [dbo].[CodigoFamilia] ([id])
GO
ALTER TABLE [dbo].[Curso] CHECK CONSTRAINT [FK_Curso_CodigoFamilia]
GO
ALTER TABLE [dbo].[Curso]  WITH CHECK ADD  CONSTRAINT [FK_Curso_Dia] FOREIGN KEY([diaId])
REFERENCES [dbo].[Dia] ([id])
GO
ALTER TABLE [dbo].[Curso] CHECK CONSTRAINT [FK_Curso_Dia]
GO
ALTER TABLE [dbo].[Curso]  WITH CHECK ADD  CONSTRAINT [FK_Curso_Turno] FOREIGN KEY([turnoId])
REFERENCES [dbo].[Turno] ([id])
GO
ALTER TABLE [dbo].[Curso] CHECK CONSTRAINT [FK_Curso_Turno]
GO
ALTER TABLE [dbo].[EstudianteCurso]  WITH CHECK ADD  CONSTRAINT [FK_EstudianteCurso_Curso] FOREIGN KEY([codigoCurso])
REFERENCES [dbo].[Curso] ([codigo])
GO
ALTER TABLE [dbo].[EstudianteCurso] CHECK CONSTRAINT [FK_EstudianteCurso_Curso]
GO
ALTER TABLE [dbo].[EstudianteCurso]  WITH CHECK ADD  CONSTRAINT [FK_EstudianteCurso_Estudiante] FOREIGN KEY([legajoEstudiante])
REFERENCES [dbo].[Estudiante] ([legajo])
GO
ALTER TABLE [dbo].[EstudianteCurso] CHECK CONSTRAINT [FK_EstudianteCurso_Estudiante]
GO
ALTER TABLE [dbo].[ProfesoresEnCursos]  WITH CHECK ADD  CONSTRAINT [FK_ProfesoresEnCursos_Curso] FOREIGN KEY([codigoCurso])
REFERENCES [dbo].[Curso] ([codigo])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProfesoresEnCursos] CHECK CONSTRAINT [FK_ProfesoresEnCursos_Curso]
GO
ALTER TABLE [dbo].[ProfesoresEnCursos]  WITH CHECK ADD  CONSTRAINT [FK_ProfesoresEnCursos_Profesores] FOREIGN KEY([correoProfesor])
REFERENCES [dbo].[Profesores] ([correo])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProfesoresEnCursos] CHECK CONSTRAINT [FK_ProfesoresEnCursos_Profesores]
GO
ALTER TABLE [dbo].[RegistroCursosCompletados]  WITH CHECK ADD  CONSTRAINT [FK_RegistroCursosCompletados_Estudiante] FOREIGN KEY([legajo])
REFERENCES [dbo].[Estudiante] ([legajo])
GO
ALTER TABLE [dbo].[RegistroCursosCompletados] CHECK CONSTRAINT [FK_RegistroCursosCompletados_Estudiante]
GO
ALTER TABLE [dbo].[RegistroDePago]  WITH CHECK ADD  CONSTRAINT [FK_RegistroDePago_ConceptoDePago] FOREIGN KEY([conceptoNombre])
REFERENCES [dbo].[ConceptoDePago] ([nombre])
GO
ALTER TABLE [dbo].[RegistroDePago] CHECK CONSTRAINT [FK_RegistroDePago_ConceptoDePago]
GO
ALTER TABLE [dbo].[RegistroDePago]  WITH CHECK ADD  CONSTRAINT [FK_RegistroDePago_Estudiante] FOREIGN KEY([legajoEstudiante])
REFERENCES [dbo].[Estudiante] ([legajo])
GO
ALTER TABLE [dbo].[RegistroDePago] CHECK CONSTRAINT [FK_RegistroDePago_Estudiante]
GO
ALTER TABLE [dbo].[RegistroInscripcion]  WITH CHECK ADD  CONSTRAINT [FK_RegistroInscripcion_Curso] FOREIGN KEY([codigoCurso])
REFERENCES [dbo].[Curso] ([codigo])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RegistroInscripcion] CHECK CONSTRAINT [FK_RegistroInscripcion_Curso]
GO
ALTER TABLE [dbo].[RegistroInscripcion]  WITH CHECK ADD  CONSTRAINT [FK_RegistroInscripcion_Estudiante] FOREIGN KEY([legajo])
REFERENCES [dbo].[Estudiante] ([legajo])
GO
ALTER TABLE [dbo].[RegistroInscripcion] CHECK CONSTRAINT [FK_RegistroInscripcion_Estudiante]
GO
USE [master]
GO
ALTER DATABASE [TestSYSACAD] SET  READ_WRITE 
GO
