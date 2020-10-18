using System;

namespace PizzastycznieFrontend.Forms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonAddAdditive = new System.Windows.Forms.Button();
            this.listViewMenu = new System.Windows.Forms.ListView();
            this.columnFoodName = new System.Windows.Forms.ColumnHeader();
            this.columnFoodPrice = new System.Windows.Forms.ColumnHeader();
            this.buttonSignUp = new System.Windows.Forms.Button();
            this.buttonSignIn = new System.Windows.Forms.Button();
            this.listViewOrder = new System.Windows.Forms.ListView();
            this.columnFood = new System.Windows.Forms.ColumnHeader();
            this.columnAmount = new System.Windows.Forms.ColumnHeader();
            this.labelMenu = new System.Windows.Forms.Label();
            this.labelOrder = new System.Windows.Forms.Label();
            this.buttonRemoveFromOrder = new System.Windows.Forms.Button();
            this.buttonPlaceOrder = new System.Windows.Forms.Button();
            this.buttonAddFood = new System.Windows.Forms.Button();
            this.labelTotalPrice = new System.Windows.Forms.Label();
            this.foodAmount = new System.Windows.Forms.NumericUpDown();
            this.additiveAmmount = new System.Windows.Forms.NumericUpDown();
            this.comboBoxAdditives = new System.Windows.Forms.ComboBox();
            this.totalPriceText = new System.Windows.Forms.Label();
            this.labelAdditivePrice = new System.Windows.Forms.Label();
            this.additivePrice = new System.Windows.Forms.Label();
            this.textBoxAddress = new System.Windows.Forms.TextBox();
            this.textBoxPhoneNumber = new System.Windows.Forms.TextBox();
            this.radioButtonCard = new System.Windows.Forms.RadioButton();
            this.richTextBoxComments = new System.Windows.Forms.RichTextBox();
            this.labelDeliveryAddress = new System.Windows.Forms.Label();
            this.labelPhoneNumber = new System.Windows.Forms.Label();
            this.labelComments = new System.Windows.Forms.Label();
            this.labelPaymentMethod = new System.Windows.Forms.Label();
            this.radioButtonCash = new System.Windows.Forms.RadioButton();
            this.groupBoxPaymentMethod = new System.Windows.Forms.GroupBox();
            this.buttonOrderHistory = new System.Windows.Forms.Button();
            this.buttonOrderHistory.Click += new EventHandler(buttonOrderHistory_Click);
            ((System.ComponentModel.ISupportInitialize) (this.foodAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.additiveAmmount)).BeginInit();
            this.groupBoxPaymentMethod.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAddAdditive
            // 
            this.buttonAddAdditive.Location = new System.Drawing.Point(384, 214);
            this.buttonAddAdditive.Name = "buttonAddAdditive";
            this.buttonAddAdditive.Size = new System.Drawing.Size(64, 20);
            this.buttonAddAdditive.TabIndex = 1;
            this.buttonAddAdditive.Text = "Add";
            this.buttonAddAdditive.UseVisualStyleBackColor = true;
            this.buttonAddAdditive.Click += new System.EventHandler(this.buttonAddAdditive_Click);
            // 
            // listViewMenu
            // 
            this.listViewMenu.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {this.columnFoodName, this.columnFoodPrice});
            this.listViewMenu.FullRowSelect = true;
            this.listViewMenu.GridLines = true;
            this.listViewMenu.HideSelection = false;
            this.listViewMenu.Location = new System.Drawing.Point(11, 32);
            this.listViewMenu.MultiSelect = false;
            this.listViewMenu.Name = "listViewMenu";
            this.listViewMenu.Size = new System.Drawing.Size(301, 323);
            this.listViewMenu.TabIndex = 3;
            this.listViewMenu.UseCompatibleStateImageBehavior = false;
            this.listViewMenu.View = System.Windows.Forms.View.Details;
            this.listViewMenu.SelectedIndexChanged += new System.EventHandler(this.listViewMenu_SelectedIndexChanged);
            // 
            // columnFoodName
            // 
            this.columnFoodName.Text = "Food";
            this.columnFoodName.Width = 250;
            // 
            // columnFoodPrice
            // 
            this.columnFoodPrice.Text = "Price";
            this.columnFoodPrice.Width = 150;
            // 
            // buttonSignUp
            // 
            this.buttonSignUp.Location = new System.Drawing.Point(635, 12);
            this.buttonSignUp.Name = "buttonSignUp";
            this.buttonSignUp.Size = new System.Drawing.Size(95, 25);
            this.buttonSignUp.TabIndex = 5;
            this.buttonSignUp.Text = "Sign up";
            this.buttonSignUp.UseVisualStyleBackColor = true;
            // 
            // buttonSignIn
            // 
            this.buttonSignIn.Location = new System.Drawing.Point(736, 12);
            this.buttonSignIn.Name = "buttonSignIn";
            this.buttonSignIn.Size = new System.Drawing.Size(95, 25);
            this.buttonSignIn.TabIndex = 6;
            this.buttonSignIn.Text = "Sign in";
            this.buttonSignIn.UseVisualStyleBackColor = true;
            this.buttonSignIn.Click += new System.EventHandler(this.buttonSignIn_Click);
            // 
            // listViewOrder
            // 
            this.listViewOrder.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {this.columnFood, this.columnAmount});
            this.listViewOrder.FullRowSelect = true;
            this.listViewOrder.GridLines = true;
            this.listViewOrder.HideSelection = false;
            this.listViewOrder.Location = new System.Drawing.Point(455, 94);
            this.listViewOrder.Name = "listViewOrder";
            this.listViewOrder.Size = new System.Drawing.Size(376, 261);
            this.listViewOrder.TabIndex = 7;
            this.listViewOrder.UseCompatibleStateImageBehavior = false;
            this.listViewOrder.View = System.Windows.Forms.View.Details;
            // 
            // columnFood
            // 
            this.columnFood.Text = "Food";
            this.columnFood.Width = 250;
            // 
            // columnAmount
            // 
            this.columnAmount.Text = "Amount";
            this.columnAmount.Width = 150;
            // 
            // labelMenu
            // 
            this.labelMenu.AutoSize = true;
            this.labelMenu.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.labelMenu.Location = new System.Drawing.Point(125, 4);
            this.labelMenu.Name = "labelMenu";
            this.labelMenu.Size = new System.Drawing.Size(66, 25);
            this.labelMenu.TabIndex = 8;
            this.labelMenu.Text = "MENU";
            // 
            // labelOrder
            // 
            this.labelOrder.AutoSize = true;
            this.labelOrder.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.labelOrder.Location = new System.Drawing.Point(591, 66);
            this.labelOrder.Name = "labelOrder";
            this.labelOrder.Size = new System.Drawing.Size(124, 25);
            this.labelOrder.TabIndex = 9;
            this.labelOrder.Text = "YOUR ORDER";
            // 
            // buttonRemoveFromOrder
            // 
            this.buttonRemoveFromOrder.Location = new System.Drawing.Point(334, 268);
            this.buttonRemoveFromOrder.Name = "buttonRemoveFromOrder";
            this.buttonRemoveFromOrder.Size = new System.Drawing.Size(104, 25);
            this.buttonRemoveFromOrder.TabIndex = 10;
            this.buttonRemoveFromOrder.Text = "Remove";
            this.buttonRemoveFromOrder.UseVisualStyleBackColor = true;
            this.buttonRemoveFromOrder.Click += new System.EventHandler(this.buttonRemoveFromOrder_Click);
            // 
            // buttonPlaceOrder
            // 
            this.buttonPlaceOrder.Location = new System.Drawing.Point(466, 516);
            this.buttonPlaceOrder.Name = "buttonPlaceOrder";
            this.buttonPlaceOrder.Size = new System.Drawing.Size(365, 45);
            this.buttonPlaceOrder.TabIndex = 11;
            this.buttonPlaceOrder.Text = "PLACE ORDER";
            this.buttonPlaceOrder.UseVisualStyleBackColor = true;
            this.buttonPlaceOrder.Click += new System.EventHandler(this.buttonPlaceOrder_Click);
            // 
            // buttonAddFood
            // 
            this.buttonAddFood.Location = new System.Drawing.Point(384, 125);
            this.buttonAddFood.Name = "buttonAddFood";
            this.buttonAddFood.Size = new System.Drawing.Size(64, 20);
            this.buttonAddFood.TabIndex = 15;
            this.buttonAddFood.Text = "Add";
            this.buttonAddFood.UseVisualStyleBackColor = true;
            this.buttonAddFood.Click += new System.EventHandler(this.buttonAddFood_Click);
            // 
            // labelTotalPrice
            // 
            this.labelTotalPrice.Font = new System.Drawing.Font("Segoe UI", 24F);
            this.labelTotalPrice.Location = new System.Drawing.Point(466, 396);
            this.labelTotalPrice.Name = "labelTotalPrice";
            this.labelTotalPrice.Size = new System.Drawing.Size(180, 49);
            this.labelTotalPrice.TabIndex = 20;
            this.labelTotalPrice.Text = "Total price:";
            // 
            // foodAmount
            // 
            this.foodAmount.Location = new System.Drawing.Point(323, 125);
            this.foodAmount.Minimum = new decimal(new int[] {1, 0, 0, 0});
            this.foodAmount.Name = "foodAmount";
            this.foodAmount.Size = new System.Drawing.Size(42, 20);
            this.foodAmount.TabIndex = 21;
            this.foodAmount.Value = new decimal(new int[] {1, 0, 0, 0});
            // 
            // additiveAmmount
            // 
            this.additiveAmmount.Location = new System.Drawing.Point(323, 214);
            this.additiveAmmount.Minimum = new decimal(new int[] {1, 0, 0, 0});
            this.additiveAmmount.Name = "additiveAmmount";
            this.additiveAmmount.Size = new System.Drawing.Size(42, 20);
            this.additiveAmmount.TabIndex = 22;
            this.additiveAmmount.Value = new decimal(new int[] {1, 0, 0, 0});
            // 
            // comboBoxAdditives
            // 
            this.comboBoxAdditives.FormattingEnabled = true;
            this.comboBoxAdditives.Location = new System.Drawing.Point(334, 163);
            this.comboBoxAdditives.Name = "comboBoxAdditives";
            this.comboBoxAdditives.Size = new System.Drawing.Size(104, 21);
            this.comboBoxAdditives.TabIndex = 23;
            // 
            // totalPriceText
            // 
            this.totalPriceText.AutoSize = true;
            this.totalPriceText.Font = new System.Drawing.Font("Segoe UI", 24F);
            this.totalPriceText.Location = new System.Drawing.Point(652, 396);
            this.totalPriceText.Name = "totalPriceText";
            this.totalPriceText.Size = new System.Drawing.Size(68, 45);
            this.totalPriceText.TabIndex = 24;
            this.totalPriceText.Text = "0 zl";
            // 
            // labelAdditivePrice
            // 
            this.labelAdditivePrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.labelAdditivePrice.Location = new System.Drawing.Point(334, 187);
            this.labelAdditivePrice.Name = "labelAdditivePrice";
            this.labelAdditivePrice.Size = new System.Drawing.Size(53, 24);
            this.labelAdditivePrice.TabIndex = 25;
            this.labelAdditivePrice.Text = "Price:";
            // 
            // additivePrice
            // 
            this.additivePrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.additivePrice.Location = new System.Drawing.Point(396, 187);
            this.additivePrice.Name = "additivePrice";
            this.additivePrice.Size = new System.Drawing.Size(42, 21);
            this.additivePrice.TabIndex = 26;
            this.additivePrice.Text = "0 zl";
            // 
            // textBoxAddress
            // 
            this.textBoxAddress.Location = new System.Drawing.Point(144, 370);
            this.textBoxAddress.Name = "textBoxAddress";
            this.textBoxAddress.Size = new System.Drawing.Size(304, 20);
            this.textBoxAddress.TabIndex = 27;
            // 
            // textBoxPhoneNumber
            // 
            this.textBoxPhoneNumber.Location = new System.Drawing.Point(144, 396);
            this.textBoxPhoneNumber.Name = "textBoxPhoneNumber";
            this.textBoxPhoneNumber.Size = new System.Drawing.Size(304, 20);
            this.textBoxPhoneNumber.TabIndex = 28;
            // 
            // radioButtonCard
            // 
            this.radioButtonCard.Location = new System.Drawing.Point(68, 17);
            this.radioButtonCard.Name = "radioButtonCard";
            this.radioButtonCard.Size = new System.Drawing.Size(56, 19);
            this.radioButtonCard.TabIndex = 30;
            this.radioButtonCard.TabStop = true;
            this.radioButtonCard.Text = "Card";
            this.radioButtonCard.UseVisualStyleBackColor = true;
            // 
            // richTextBoxComments
            // 
            this.richTextBoxComments.Location = new System.Drawing.Point(13, 455);
            this.richTextBoxComments.Name = "richTextBoxComments";
            this.richTextBoxComments.Size = new System.Drawing.Size(436, 106);
            this.richTextBoxComments.TabIndex = 32;
            this.richTextBoxComments.Text = "";
            // 
            // labelDeliveryAddress
            // 
            this.labelDeliveryAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.labelDeliveryAddress.Location = new System.Drawing.Point(12, 370);
            this.labelDeliveryAddress.Name = "labelDeliveryAddress";
            this.labelDeliveryAddress.Size = new System.Drawing.Size(131, 19);
            this.labelDeliveryAddress.TabIndex = 33;
            this.labelDeliveryAddress.Text = "Delivery Address:";
            // 
            // labelPhoneNumber
            // 
            this.labelPhoneNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.labelPhoneNumber.Location = new System.Drawing.Point(13, 398);
            this.labelPhoneNumber.Name = "labelPhoneNumber";
            this.labelPhoneNumber.Size = new System.Drawing.Size(125, 17);
            this.labelPhoneNumber.TabIndex = 34;
            this.labelPhoneNumber.Text = "Phone Number:";
            // 
            // labelComments
            // 
            this.labelComments.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.labelComments.Location = new System.Drawing.Point(13, 426);
            this.labelComments.Name = "labelComments";
            this.labelComments.Size = new System.Drawing.Size(125, 26);
            this.labelComments.TabIndex = 35;
            this.labelComments.Text = "Comments:";
            // 
            // labelPaymentMethod
            // 
            this.labelPaymentMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.labelPaymentMethod.Location = new System.Drawing.Point(466, 468);
            this.labelPaymentMethod.Name = "labelPaymentMethod";
            this.labelPaymentMethod.Size = new System.Drawing.Size(134, 23);
            this.labelPaymentMethod.TabIndex = 36;
            this.labelPaymentMethod.Text = "Payment method:";
            // 
            // radioButtonCash
            // 
            this.radioButtonCash.Location = new System.Drawing.Point(6, 19);
            this.radioButtonCash.Name = "radioButtonCash";
            this.radioButtonCash.Size = new System.Drawing.Size(52, 17);
            this.radioButtonCash.TabIndex = 31;
            this.radioButtonCash.TabStop = true;
            this.radioButtonCash.Text = "Cash";
            this.radioButtonCash.UseVisualStyleBackColor = true;
            // 
            // groupBoxPaymentMethod
            // 
            this.groupBoxPaymentMethod.Controls.Add(this.radioButtonCash);
            this.groupBoxPaymentMethod.Controls.Add(this.radioButtonCard);
            this.groupBoxPaymentMethod.Location = new System.Drawing.Point(606, 452);
            this.groupBoxPaymentMethod.Name = "groupBoxPaymentMethod";
            this.groupBoxPaymentMethod.Size = new System.Drawing.Size(124, 50);
            this.groupBoxPaymentMethod.TabIndex = 37;
            this.groupBoxPaymentMethod.TabStop = false;
            // 
            // buttonOrderHistory
            // 
            this.buttonOrderHistory.Location = new System.Drawing.Point(674, 14);
            this.buttonOrderHistory.Name = "buttonOrderHistory";
            this.buttonOrderHistory.Size = new System.Drawing.Size(120, 23);
            this.buttonOrderHistory.TabIndex = 38;
            this.buttonOrderHistory.Text = "Order History";
            this.buttonOrderHistory.UseVisualStyleBackColor = true;
            this.buttonOrderHistory.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(843, 574);
            this.Controls.Add(this.buttonOrderHistory);
            this.Controls.Add(this.groupBoxPaymentMethod);
            this.Controls.Add(this.labelPaymentMethod);
            this.Controls.Add(this.labelComments);
            this.Controls.Add(this.labelPhoneNumber);
            this.Controls.Add(this.labelDeliveryAddress);
            this.Controls.Add(this.richTextBoxComments);
            this.Controls.Add(this.textBoxPhoneNumber);
            this.Controls.Add(this.textBoxAddress);
            this.Controls.Add(this.additivePrice);
            this.Controls.Add(this.labelAdditivePrice);
            this.Controls.Add(this.totalPriceText);
            this.Controls.Add(this.comboBoxAdditives);
            this.Controls.Add(this.additiveAmmount);
            this.Controls.Add(this.foodAmount);
            this.Controls.Add(this.labelTotalPrice);
            this.Controls.Add(this.buttonAddFood);
            this.Controls.Add(this.buttonPlaceOrder);
            this.Controls.Add(this.buttonRemoveFromOrder);
            this.Controls.Add(this.labelOrder);
            this.Controls.Add(this.labelMenu);
            this.Controls.Add(this.listViewOrder);
            this.Controls.Add(this.buttonSignIn);
            this.Controls.Add(this.buttonSignUp);
            this.Controls.Add(this.listViewMenu);
            this.Controls.Add(this.buttonAddAdditive);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(15, 15);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize) (this.foodAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.additiveAmmount)).EndInit();
            this.groupBoxPaymentMethod.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button buttonOrderHistory;

        private System.Windows.Forms.GroupBox groupBoxPaymentMethod;

        private System.Windows.Forms.Label labelPaymentMethod;
        private System.Windows.Forms.RadioButton radioButtonCard;
        private System.Windows.Forms.RadioButton radioButtonCash;

        private System.Windows.Forms.Label labelComments;
        private System.Windows.Forms.Label labelDeliveryAddress;
        private System.Windows.Forms.Label labelPhoneNumber;
        private System.Windows.Forms.RichTextBox richTextBoxComments;
        private System.Windows.Forms.TextBox textBoxAddress;
        private System.Windows.Forms.TextBox textBoxPhoneNumber;

        private System.Windows.Forms.Label additivePrice;
        private System.Windows.Forms.Label labelAdditivePrice;

        private System.Windows.Forms.ColumnHeader columnAmount;
        private System.Windows.Forms.ColumnHeader columnFood;
        private System.Windows.Forms.Label labelTotalPrice;

        private System.Windows.Forms.Button buttonAddAdditive;
        private System.Windows.Forms.Button buttonAddFood;
        private System.Windows.Forms.Button buttonRemoveFromOrder;

        private System.Windows.Forms.ColumnHeader columnFoodName;
        private System.Windows.Forms.ColumnHeader columnFoodPrice;

        private System.Windows.Forms.Button buttonSignIn;
        private System.Windows.Forms.Button buttonSignUp;

        private System.Windows.Forms.ListView listViewMenu;
        private System.Windows.Forms.ListView listViewOrder;

        private System.Windows.Forms.Button buttonPlaceOrder;
        private System.Windows.Forms.Label labelMenu;
        private System.Windows.Forms.Label labelOrder;

        #endregion

        private System.Windows.Forms.NumericUpDown foodAmount;
        private System.Windows.Forms.NumericUpDown additiveAmmount;
        private System.Windows.Forms.ComboBox comboBoxAdditives;
        private System.Windows.Forms.Label totalPriceText;
    }
}