using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using PizzastycznieFrontend.ApiHandler;
using PizzastycznieFrontend.ApiHandler.DTO;
using PizzastycznieFrontend.Authentication;

namespace PizzastycznieFrontend
{
    public partial class MainForm : Form
    {
        private readonly ILogger<MainForm> _logger;
        private readonly IApiHandler _apiHandler;
        private readonly IMemoryCache _cache;
        
        private IEnumerable<Food> _menuItems;
        private IList<OrderFood> _orderFood = new List<OrderFood>();
        private IList<OrderAdditive> _orderAdditives = new List<OrderAdditive>();
        private decimal _totalPrice;

        public MainForm(ILogger<MainForm> logger, IApiHandler apiHandler, IMemoryCache cache)
        {
            _logger = logger;
            _apiHandler = apiHandler;
            _cache = cache;
            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                await PopulateMenu();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while requesting menu items from api {ex.Message}");
            }
        }

        private async Task PopulateMenu()
        {
            _menuItems = await _apiHandler.GetMenuItemsAsync();

            listViewMenu.Items.Clear();
            foreach (var item in _menuItems)
            {
                var row = new[] {item.Name, item.Price.ToString()};
                var listViewItem = new ListViewItem(row) {Tag = item};

                foreach (var additive in item.Additives)
                {
                    listViewItem.SubItems.Add(additive.Name);
                    listViewItem.SubItems.Add(additive.Price.ToString());
                }

                listViewMenu.Items.Add(listViewItem);
            }

            listViewMenu.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listViewMenu.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void listViewMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxAdditives.Items.Clear();

            if (listViewMenu.SelectedItems.Count == 0)
                return;

            var menuItem = (Food) listViewMenu.SelectedItems[0].Tag;

            foreach (var additive in menuItem.Additives)
            {
                comboBoxAdditives.Items.Add(additive.Name);
                additivePrice.Text = additive.Price + " zl";
            }
        }

        private void buttonAddFood_Click(object sender, EventArgs e)
        {
            if (listViewMenu.SelectedItems.Count == 0)
                return;

            var menuItem = (Food) listViewMenu.SelectedItems[0].Tag;

            var orderFood = new OrderFood
            {
                Name = menuItem.Name,
                Amount = (int) foodAmount.Value,
                Price = menuItem.Price
            };

            _orderFood.Add(orderFood);

            var row = new[] {menuItem.Name, foodAmount.Text};
            listViewOrder.Items.Add(new ListViewItem(row) {Tag = orderFood});

            _totalPrice += menuItem.Price;
            totalPriceText.Text = _totalPrice + " zl";

            listViewOrder.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listViewOrder.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonAddAdditive_Click(object sender, EventArgs e)
        {
            var additiveName = (string) comboBoxAdditives.SelectedItem;

            var orderAdditive = new OrderAdditive
            {
                Name = additiveName,
                Amount = (int) additiveAmmount.Value,
                Price = decimal.Parse(additivePrice.Text.Trim(' ', 'z', 'l'))
            };

            _orderAdditives.Add(orderAdditive);

            var row = new[] {additiveName, additiveAmmount.Text};
            listViewOrder.Items.Add(new ListViewItem(row) {Tag = orderAdditive});

            _totalPrice += orderAdditive.Price;
            totalPriceText.Text = _totalPrice + " zl";

            listViewOrder.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listViewOrder.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonRemoveFromOrder_Click(object sender, EventArgs e)
        {
            if (listViewOrder.SelectedItems.Count == 0)
                return;

            var listViewItem = listViewOrder.SelectedItems[0];

            switch (listViewItem.Tag)
            {
                case OrderFood food:
                    _orderFood.Remove(food);
                    listViewOrder.Items.Remove(listViewItem);

                    _totalPrice -= food.Price;
                    totalPriceText.Text = _totalPrice + " zl";

                    break;

                case OrderAdditive additive:
                    _orderAdditives.Remove(additive);
                    listViewOrder.Items.Remove(listViewItem);

                    _totalPrice -= additive.Price;
                    totalPriceText.Text = _totalPrice + " zl";

                    break;
            }
        }

        private void buttonPlaceOrder_Click(object sender, EventArgs e)
        {
            var token = _cache.Get(AuthenticationDataEnum.Token);

            if (token == null)
            {
                
            }
            //TODO actually send request
            var order = new Order();
            order.Comments = richTextBoxComments.Text;
            order.DeliveryAddress = textBoxAddress.Text;
            order.CustomerPhone = textBoxPhoneNumber.Text;
            order.OrderFood = _orderFood;
            order.OrderAdditives = _orderAdditives;
        }
    }
}