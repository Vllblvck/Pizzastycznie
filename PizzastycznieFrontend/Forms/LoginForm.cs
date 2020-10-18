using System;
using System.Windows.Forms;
using PizzastycznieFrontend.Authentication;
using PizzastycznieFrontend.Authentication.DTO;

namespace PizzastycznieFrontend.Forms
{
    public partial class LoginForm : Form
    {
        private readonly IAuthenticationService _authService;
        
        public LoginForm(IAuthenticationService authService)
        {
            _authService = authService;
            InitializeComponent();
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            var userCredentials = new UserCredentialsObject
            {
                Email = textBoxEmail.Text,
                Password = textBoxPassword.Text
            };

            var result = await _authService.AuthenticateAsync(userCredentials);

            if (result)
            {
                DialogResult = DialogResult.OK;
                var mainForm = (MainForm) Owner;
                mainForm.HideAuthButtons(true);
                Close();
            }
            
            labelAuthenticationError.Visible = true;
        }
    }
}