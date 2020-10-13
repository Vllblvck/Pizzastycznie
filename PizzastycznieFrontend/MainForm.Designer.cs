namespace PizzastycznieFrontend
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
            this.buttonAdd = new System.Windows.Forms.Button();
            this.textBoxAmount = new System.Windows.Forms.TextBox();
            this.listViewMenu = new System.Windows.Forms.ListView();
            this.labelAmount = new System.Windows.Forms.Label();
            this.buttonSignUp = new System.Windows.Forms.Button();
            this.buttonSignIn = new System.Windows.Forms.Button();
            this.listViewOrder = new System.Windows.Forms.ListView();
            this.labelMenu = new System.Windows.Forms.Label();
            this.labelOrder = new System.Windows.Forms.Label();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonPlaceOrder = new System.Windows.Forms.Button();
            this.columnHeaderFood = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderPrice = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(193, 375);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(95, 25);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            // 
            // textBoxAmount
            // 
            this.textBoxAmount.Location = new System.Drawing.Point(137, 378);
            this.textBoxAmount.Name = "textBoxAmount";
            this.textBoxAmount.Size = new System.Drawing.Size(50, 20);
            this.textBoxAmount.TabIndex = 2;
            // 
            // listViewMenu
            // 
            this.listViewMenu.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {this.columnHeaderFood, this.columnHeaderPrice});
            this.listViewMenu.HideSelection = false;
            this.listViewMenu.Location = new System.Drawing.Point(12, 94);
            this.listViewMenu.Name = "listViewMenu";
            this.listViewMenu.Size = new System.Drawing.Size(376, 261);
            this.listViewMenu.TabIndex = 3;
            this.listViewMenu.UseCompatibleStateImageBehavior = false;
            // 
            // labelAmount
            // 
            this.labelAmount.AutoSize = true;
            this.labelAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.labelAmount.Location = new System.Drawing.Point(166, 358);
            this.labelAmount.Name = "labelAmount";
            this.labelAmount.Size = new System.Drawing.Size(56, 17);
            this.labelAmount.TabIndex = 4;
            this.labelAmount.Text = "Amount";
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
            // 
            // listViewOrder
            // 
            this.listViewOrder.HideSelection = false;
            this.listViewOrder.Location = new System.Drawing.Point(455, 94);
            this.listViewOrder.Name = "listViewOrder";
            this.listViewOrder.Size = new System.Drawing.Size(376, 261);
            this.listViewOrder.TabIndex = 7;
            this.listViewOrder.UseCompatibleStateImageBehavior = false;
            // 
            // labelMenu
            // 
            this.labelMenu.AutoSize = true;
            this.labelMenu.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.labelMenu.Location = new System.Drawing.Point(156, 66);
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
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(611, 366);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(95, 25);
            this.buttonRemove.TabIndex = 10;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            // 
            // buttonPlaceOrder
            // 
            this.buttonPlaceOrder.Location = new System.Drawing.Point(352, 429);
            this.buttonPlaceOrder.Name = "buttonPlaceOrder";
            this.buttonPlaceOrder.Size = new System.Drawing.Size(133, 45);
            this.buttonPlaceOrder.TabIndex = 11;
            this.buttonPlaceOrder.Text = "PLACE ORDER";
            this.buttonPlaceOrder.UseVisualStyleBackColor = true;
            // 
            // columnHeaderFood
            // 
            this.columnHeaderFood.Text = "Foodr";
            // 
            // columnHeaderPrice
            // 
            this.columnHeaderPrice.Text = "Price";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 486);
            this.Controls.Add(this.buttonPlaceOrder);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.labelOrder);
            this.Controls.Add(this.labelMenu);
            this.Controls.Add(this.listViewOrder);
            this.Controls.Add(this.buttonSignIn);
            this.Controls.Add(this.buttonSignUp);
            this.Controls.Add(this.labelAmount);
            this.Controls.Add(this.listViewMenu);
            this.Controls.Add(this.textBoxAmount);
            this.Controls.Add(this.buttonAdd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Pizzastycznie";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ColumnHeader columnHeaderFood;
        private System.Windows.Forms.ColumnHeader columnHeaderPrice;

        private System.Windows.Forms.Button buttonSignIn;
        private System.Windows.Forms.Button buttonSignUp;

        private System.Windows.Forms.TextBox textBoxAmount;

        private System.Windows.Forms.ListView listViewMenu;
        private System.Windows.Forms.ListView listViewOrder;

        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonPlaceOrder;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Label labelAmount;
        private System.Windows.Forms.Label labelMenu;
        private System.Windows.Forms.Label labelOrder;

        #endregion
    }
}