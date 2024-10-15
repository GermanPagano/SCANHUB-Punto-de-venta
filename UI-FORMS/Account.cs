using SCANHUB___INVENTARIO_Y_CAJA.Models;
using SCANHUB___INVENTARIO_Y_CAJA.Database;
using System.Windows.Forms;
using System;
using SCANHUB___INVENTARIO_Y_CAJA.EmailServices;
using System.Threading.Tasks;

namespace SCANHUB___INVENTARIO_Y_CAJA.UI_FORMS
{
    public partial class Account : Form
    {
        private UserAccount currentUser;

        public Account(UserAccount user)
        {
            InitializeComponent();
            currentUser = user;
            DisplayUserInfo();
        }

        private void LoadUserAccountData()
        {
            currentUser = DatabaseConfig.GetUserAccount();  // Cargar el objeto desde la base de datos

            if (currentUser != null)
            {
                DisplayUserInfo();  // Mostrar la información en el formulario
            }
            else
            {
                MessageBox.Show("No se encontraron datos de la cuenta.");
            }
        }

        private void DisplayUserInfo()
        {
            txtRazonSocial.Text = currentUser.RazonSocial;
            txtCuil.Text = currentUser.Cuil;
            txtDireccion.Text = currentUser.Direccion;
            txtTelefono.Text = currentUser.Telefono;
            txtEmail.Text = currentUser.Email;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            // Inicia el progreso
            progressBar.Value = 0;
            lblStatus.Text = "Guardando datos...";

            // Actualiza el objeto currentUser con los datos del formulario
            currentUser.RazonSocial = txtRazonSocial.Text;
            currentUser.Cuil = txtCuil.Text;
            currentUser.Direccion = txtDireccion.Text;
            currentUser.Telefono = txtTelefono.Text;
            currentUser.Email = txtEmail.Text;

            // Incrementa el progreso
            progressBar.Value = 20;

            // Llama al método para actualizar la base de datos
            bool isUpdated = DatabaseConfig.UpdateUserAccount(currentUser);

            // Actualiza el progreso
            progressBar.Value = 50;

            // Muestra el resultado de la operación
            if (isUpdated)
            {
                lblStatus.Text = "Datos almacenados correctamente.";

                // Incrementa el progreso
                progressBar.Value = 70;

                // Enviar correo de confirmación al usuario
                try
                {
                    EmailService emailService = new EmailService();
                    string subject = "Confirmación de actualización de datos";
                    string messageBody = $"Hola {currentUser.RazonSocial},\n\n" +
                                         "Tus datos han sido actualizados exitosamente en nuestro sistema.\n\n" +
                                         "Gracias por confiar en nosotros.\n\n" +
                                         "Saludos,\nEl equipo de SCANHUB.";

                    // Llama al método de envío de correo de forma asincrónica
                    await Task.Run(() => emailService.SendEmail(currentUser.Email, subject, messageBody));

                    lblStatus.Text = "Correo de confirmación enviado.";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al enviar el correo: {ex.Message}");
                    lblStatus.Text = "Error al enviar el correo.";
                }

                // Completa el progreso
                progressBar.Value = 100;
                lblStatus.Text = "Proceso completado.";

                // Esperar un segundo para que el usuario vea el mensaje y luego cerrar el formulario
                await Task.Delay(1000);
                this.Close();  // Cerrar el formulario automáticamente
            }
            else
            {
                MessageBox.Show("No se pudo actualizar la información.");
                lblStatus.Text = "Error al almacenar los datos.";
            }
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Account_Load(object sender, EventArgs e)
        {
            // Establecer pantalla completa
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;  // Evitar que se redimensione
            this.MaximizeBox = false;  // Desactivar el botón de maximizar
        }
    }
}


