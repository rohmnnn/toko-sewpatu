﻿namespace TokoSepatuApp.View.FormCustomers
{
    partial class FormCustomers
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.title = new System.Windows.Forms.Label();
            this.listView = new System.Windows.Forms.ListView();
            this.textCari = new System.Windows.Forms.TextBox();
            this.btnCari = new System.Windows.Forms.Button();
            this.btnShow = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.title.Location = new System.Drawing.Point(12, 9);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(158, 39);
            this.title.TabIndex = 2;
            this.title.Text = "Customers";
            // 
            // listView
            // 
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(19, 77);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(751, 328);
            this.listView.TabIndex = 3;
            this.listView.UseCompatibleStateImageBehavior = false;
            // 
            // textCari
            // 
            this.textCari.Location = new System.Drawing.Point(19, 51);
            this.textCari.Name = "textCari";
            this.textCari.Size = new System.Drawing.Size(670, 20);
            this.textCari.TabIndex = 7;
            // 
            // btnCari
            // 
            this.btnCari.Location = new System.Drawing.Point(695, 48);
            this.btnCari.Name = "btnCari";
            this.btnCari.Size = new System.Drawing.Size(75, 23);
            this.btnCari.TabIndex = 8;
            this.btnCari.Text = "Cari";
            this.btnCari.UseVisualStyleBackColor = true;
            this.btnCari.Click += new System.EventHandler(this.btnCari_Click);
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(19, 415);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(75, 23);
            this.btnShow.TabIndex = 9;
            this.btnShow.Text = "Tampilkan Semua";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // FormCustomers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.btnCari);
            this.Controls.Add(this.textCari);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.title);
            this.Name = "FormCustomers";
            this.Text = "Form Customers";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.TextBox textCari;
        private System.Windows.Forms.Button btnCari;
        private System.Windows.Forms.Button btnShow;
    }
}