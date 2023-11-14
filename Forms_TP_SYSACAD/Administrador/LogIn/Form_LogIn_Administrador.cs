using Libreria_Clases_TP_SYSACAD.EntidadesPrimarias;
using Libreria_Clases_TP_SYSACAD.Interfaces_y_Enum;
using Libreria_Clases_TP_SYSACAD.Validaciones;
using System.Diagnostics;

namespace Forms_TP_SYSACAD
{
    public partial class Form_LogIn_Administrador : Form
    {
        private string _correoLogIn;
        private string _contrase�aLogIn;

        public Form_LogIn_Administrador()
        {
            InitializeComponent();
            lblBienvenida.Text = "Bienvenido a SYSACAD ADMINISTRADOR";
            lblMail.Text = "Ingrese su direccion de email:";
            lblContrase�a.Text = "Ingrese su contrase�a:";
            lblCodigoAcceso.Text = $"Codigo de acceso para admins: {Sistema.CodigoAccesoAdmins}";

            btnIngresar.Text = "Ingresar";
            btnAtras.Text = "Atras";
            btnAutoCompletar.Text = "Autocompletar";
        }

        private void Inicio_Sesion_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Iniciar Sesion ADMINISTRADOR";
        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {
            _correoLogIn = textBoxEmail.Text;
        }

        private void textBoxContrase�a_TextChanged(object sender, EventArgs e)
        {
            _contrase�aLogIn = textBoxContrase�a.Text;
        }

        /// <summary>
        /// Maneja el evento de hacer clic en el bot�n "Ingresar" para el inicio de sesi�n.
        /// </summary>
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            // Crear una instancia del validador de credenciales y valido las credenciales
            ValidadorCredenciales validadorCredenciales = new ValidadorCredenciales();
            MensajeRespuestaValidacionCredencialesContrase�a resultadoValidacion = validadorCredenciales.ValidarCredenciales(_correoLogIn, _contrase�aLogIn, Log.Admin);

            switch (resultadoValidacion)
            {
                case MensajeRespuestaValidacionCredencialesContrase�a.OK:
                    this.Hide();
                    Form_Panel_Administracion formPanelAdministracion = new Form_Panel_Administracion();
                    formPanelAdministracion.ShowDialog();
                    break;
                case MensajeRespuestaValidacionCredencialesContrase�a.camposVacios:
                    MessageBox.Show("Aseg�rese de completar los campos solicitados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case MensajeRespuestaValidacionCredencialesContrase�a.noEncontrado:
                    MessageBox.Show("Credenciales de inicio incorrectas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form_Inicio formInicio = new Form_Inicio();
            formInicio.Show();
        }

        private void btnAutoCompletar_Click(object sender, EventArgs e)
        {
            textBoxEmail.Text = "johntravolta@hotmail.com";
            _correoLogIn = "johntravolta@hotmail.com";
            textBoxContrase�a.Text = "666666";
            _contrase�aLogIn = "666666";
        }
    }
}