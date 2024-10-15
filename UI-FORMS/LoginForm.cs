using SCANHUB___INVENTARIO_Y_CAJA.Database;
using SCANHUB___INVENTARIO_Y_CAJA.UI_FORMS;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SCANHUB___INVENTARIO_Y_CAJA
{
    public partial class LoginForm : Form
    {
        private TextBox txtEmail;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnRegister;
        private Label lblRegister;
        private PictureBox logoPictureBox;


        public LoginForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
            DatabaseConfig.InitializeDatabase();  // Inicializar la base de datos
        }

        private void InitializeCustomComponents()
        {
            // Establecer el tamaño del formulario
            this.ClientSize = new Size(500, 400); // Ajusta estos valores como necesites

            int centerPoint = this.ClientSize.Width / 2; // Punto medio del formulario horizontalmente

            // PictureBox para el logo
            logoPictureBox = new PictureBox();
            logoPictureBox.Image = Image.FromFile(@"Images\Logo.png");
            logoPictureBox.Size = new Size(200, 100); // Tamaño del logo
            logoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            logoPictureBox.Location = new Point((centerPoint - logoPictureBox.Width / 2), 20); // Centrar el logo en el formulario
            this.Controls.Add(logoPictureBox);

            // Configuración de la Label y TextBox para el usuario
            Label lblEmail = new Label();
            lblEmail.Text = "Usuario:";
            lblEmail.Font = new Font("Arial", 10);
            lblEmail.Size = new Size(100, 20);
            lblEmail.Location = new Point((centerPoint - 250 / 2), 130); // Ajusta para centrar
            this.Controls.Add(lblEmail);

            txtEmail = new TextBox();
            txtEmail.Size = new Size(250, 30);
            txtEmail.Font = new Font("Arial", 12);
            txtEmail.Location = new Point((centerPoint - txtEmail.Width / 2), 150); // Ajusta para centrar
            this.Controls.Add(txtEmail);

            // Configuración de la Label y TextBox para la contraseña
            Label lblPassword = new Label();
            lblPassword.Text = "Contraseña:";
            lblPassword.Font = new Font("Arial", 10);
            lblPassword.Size = new Size(100, 20);
            lblPassword.Location = new Point((centerPoint - 250 / 2), 190); // Ajusta para centrar
            this.Controls.Add(lblPassword);

            txtPassword = new TextBox();
            txtPassword.Size = new Size(250, 30);
            txtPassword.PasswordChar = '*';
            txtPassword.Font = new Font("Arial", 12);
            txtPassword.KeyUp += new KeyEventHandler(txtPassword_KeyUp); // Asigna el evento KeyUp
            txtPassword.Location = new Point((centerPoint - txtPassword.Width / 2), 210); // Ajusta para centrar
            this.Controls.Add(txtPassword);



            // Configuración del botón de acceso
            btnLogin = new Button();
            btnLogin.Size = new Size(250, 40);
            btnLogin.Text = "Acceder";
            btnLogin.Font = new Font("Arial", 12);
            btnLogin.BackColor = ColorTranslator.FromHtml("#136EBF");
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Location = new Point((centerPoint - btnLogin.Width / 2), 260); // Ajusta para centrar
            btnLogin.Click += new EventHandler(btnLogin_Click);
            this.Controls.Add(btnLogin);
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e); // Llama al evento click del botón login
            }
        }



        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtEmail.Text;
            string password = txtPassword.Text;

            if (DatabaseConfig.ValidateUser(username, password))
            {
                MessageBox.Show("Datos correctos. Bienvenido " + username + "!");
                this.Hide(); // Opcional: Oculta el formulario de login
                Dashboard dashboardForm = new Dashboard();
                dashboardForm.Show();
            }
            else
            {
                MessageBox.Show("Error en los datos. Verifica tu usuario y contraseña.");
            }
        }




        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
