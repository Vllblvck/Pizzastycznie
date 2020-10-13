using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PizzastycznieFrontend.ApiHandler;
using PizzastycznieFrontend.ApiHandler.DTO;

namespace PizzastycznieFrontend
{
    public partial class MainForm : Form
    {
        private readonly IApiHandler _apiHandler;
        private IEnumerable<Food> _menuItems;

        public MainForm(IApiHandler apiHandler)
        {
            _apiHandler = apiHandler;
            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await PopulateMenu();
        }

        private async Task PopulateMenu()
        {
            _menuItems = await _apiHandler.GetMenuItemsAsync();
            
            foreach (var item in _menuItems)
            {
                listViewMenu.Items.Add(item.Name);
            }
        }
    }
}