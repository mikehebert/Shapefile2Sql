namespace Shapefile2Sql
{
    partial class MainForm
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
            this.ConnectionStringButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.connectionStringTextBox = new System.Windows.Forms.TextBox();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.OpenShapefileButton = new System.Windows.Forms.Button();
            this.FileNameTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ConnectionStringButton
            // 
            this.ConnectionStringButton.Location = new System.Drawing.Point(419, 10);
            this.ConnectionStringButton.Name = "ConnectionStringButton";
            this.ConnectionStringButton.Size = new System.Drawing.Size(25, 23);
            this.ConnectionStringButton.TabIndex = 0;
            this.ConnectionStringButton.Text = "...";
            this.ConnectionStringButton.UseVisualStyleBackColor = true;
            this.ConnectionStringButton.Click += new System.EventHandler(this.ConnectionStringButton_OnClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Database:";
            // 
            // connectionStringTextBox
            // 
            this.connectionStringTextBox.Location = new System.Drawing.Point(74, 12);
            this.connectionStringTextBox.Name = "connectionStringTextBox";
            this.connectionStringTextBox.ReadOnly = true;
            this.connectionStringTextBox.Size = new System.Drawing.Size(339, 20);
            this.connectionStringTextBox.TabIndex = 2;
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.Filter = "Shapefiles|*.shp|All files|*.*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Shapefile:";
            // 
            // OpenShapefileButton
            // 
            this.OpenShapefileButton.Location = new System.Drawing.Point(419, 36);
            this.OpenShapefileButton.Name = "OpenShapefileButton";
            this.OpenShapefileButton.Size = new System.Drawing.Size(25, 23);
            this.OpenShapefileButton.TabIndex = 11;
            this.OpenShapefileButton.Text = "...";
            this.OpenShapefileButton.UseVisualStyleBackColor = true;
            this.OpenShapefileButton.Click += new System.EventHandler(this.OpenShapefileButton_OnClick);
            // 
            // FileNameTextBox
            // 
            this.FileNameTextBox.Enabled = false;
            this.FileNameTextBox.Location = new System.Drawing.Point(74, 38);
            this.FileNameTextBox.Name = "FileNameTextBox";
            this.FileNameTextBox.ReadOnly = true;
            this.FileNameTextBox.Size = new System.Drawing.Size(339, 20);
            this.FileNameTextBox.TabIndex = 10;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 304);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.OpenShapefileButton);
            this.Controls.Add(this.FileNameTextBox);
            this.Controls.Add(this.connectionStringTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ConnectionStringButton);
            this.Name = "MainForm";
            this.Text = "Shapefile to SQL Import Tool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConnectionStringButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox connectionStringTextBox;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button OpenShapefileButton;
        private System.Windows.Forms.TextBox FileNameTextBox;
    }
}

