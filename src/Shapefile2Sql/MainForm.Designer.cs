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
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.attributeMappingPanel = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.parallelCheckBox = new System.Windows.Forms.CheckBox();
            this.geometryOption = new System.Windows.Forms.RadioButton();
            this.geographyOption = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.sridTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.createIndexCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.shapeDataColumnNameTextBox = new System.Windows.Forms.TextBox();
            this.tableNameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.importButton = new System.Windows.Forms.Button();
            this.shapefileLoaderWorker = new System.ComponentModel.BackgroundWorker();
            this.shapeCountLabel = new System.Windows.Forms.Label();
            this.optionsPanel = new System.Windows.Forms.Panel();
            this.importProgressBar = new System.Windows.Forms.ProgressBar();
            this.importWorker = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.optionsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConnectionStringButton
            // 
            this.ConnectionStringButton.Location = new System.Drawing.Point(504, 3);
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
            this.label1.Location = new System.Drawing.Point(270, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Database:";
            // 
            // connectionStringTextBox
            // 
            this.connectionStringTextBox.Location = new System.Drawing.Point(332, 3);
            this.connectionStringTextBox.Name = "connectionStringTextBox";
            this.connectionStringTextBox.ReadOnly = true;
            this.connectionStringTextBox.Size = new System.Drawing.Size(166, 20);
            this.connectionStringTextBox.TabIndex = 2;
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.Filter = "Shapefiles|*.shp|All files|*.*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Shapefile:";
            // 
            // OpenShapefileButton
            // 
            this.OpenShapefileButton.Location = new System.Drawing.Point(236, 3);
            this.OpenShapefileButton.Name = "OpenShapefileButton";
            this.OpenShapefileButton.Size = new System.Drawing.Size(25, 23);
            this.OpenShapefileButton.TabIndex = 11;
            this.OpenShapefileButton.Text = "...";
            this.OpenShapefileButton.UseVisualStyleBackColor = true;
            this.OpenShapefileButton.Click += new System.EventHandler(this.OpenShapefileButton_OnClick);
            // 
            // FileNameTextBox
            // 
            this.FileNameTextBox.Location = new System.Drawing.Point(64, 3);
            this.FileNameTextBox.Name = "FileNameTextBox";
            this.FileNameTextBox.ReadOnly = true;
            this.FileNameTextBox.Size = new System.Drawing.Size(166, 20);
            this.FileNameTextBox.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Attribute / Column Mapping";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.attributeMappingPanel);
            this.panel1.Location = new System.Drawing.Point(5, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(256, 177);
            this.panel1.TabIndex = 17;
            // 
            // attributeMappingPanel
            // 
            this.attributeMappingPanel.AutoSize = true;
            this.attributeMappingPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.attributeMappingPanel.ColumnCount = 3;
            this.attributeMappingPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.attributeMappingPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.attributeMappingPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.attributeMappingPanel.Location = new System.Drawing.Point(3, 3);
            this.attributeMappingPanel.Name = "attributeMappingPanel";
            this.attributeMappingPanel.RowCount = 1;
            this.attributeMappingPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.attributeMappingPanel.Size = new System.Drawing.Size(20, 0);
            this.attributeMappingPanel.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.parallelCheckBox);
            this.groupBox1.Controls.Add(this.geometryOption);
            this.groupBox1.Controls.Add(this.geographyOption);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.sridTextBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.createIndexCheckBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.shapeDataColumnNameTextBox);
            this.groupBox1.Controls.Add(this.tableNameTextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(267, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 193);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // parallelCheckBox
            // 
            this.parallelCheckBox.AutoSize = true;
            this.parallelCheckBox.Location = new System.Drawing.Point(79, 167);
            this.parallelCheckBox.Name = "parallelCheckBox";
            this.parallelCheckBox.Size = new System.Drawing.Size(92, 17);
            this.parallelCheckBox.TabIndex = 10;
            this.parallelCheckBox.Text = "Parallel Import";
            this.parallelCheckBox.UseVisualStyleBackColor = true;
            this.parallelCheckBox.CheckedChanged += new System.EventHandler(this.ParallelCheckBox_OnCheckedChanged);
            // 
            // geometryOption
            // 
            this.geometryOption.AutoSize = true;
            this.geometryOption.Location = new System.Drawing.Point(79, 69);
            this.geometryOption.Name = "geometryOption";
            this.geometryOption.Size = new System.Drawing.Size(70, 17);
            this.geometryOption.TabIndex = 9;
            this.geometryOption.Text = "Geometry";
            this.geometryOption.UseVisualStyleBackColor = true;
            // 
            // geographyOption
            // 
            this.geographyOption.AutoSize = true;
            this.geographyOption.Location = new System.Drawing.Point(79, 46);
            this.geographyOption.Name = "geographyOption";
            this.geographyOption.Size = new System.Drawing.Size(77, 17);
            this.geographyOption.TabIndex = 8;
            this.geographyOption.Text = "Geography";
            this.geographyOption.UseVisualStyleBackColor = true;
            this.geographyOption.CheckedChanged += new System.EventHandler(this.GeographyOption_OnCheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Shape Type:";
            // 
            // sridTextBox
            // 
            this.sridTextBox.Location = new System.Drawing.Point(79, 118);
            this.sridTextBox.Name = "sridTextBox";
            this.sridTextBox.Size = new System.Drawing.Size(72, 20);
            this.sridTextBox.TabIndex = 6;
            this.sridTextBox.TextChanged += new System.EventHandler(this.SridTextBox_OnTextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "SRID:";
            // 
            // createIndexCheckBox
            // 
            this.createIndexCheckBox.AutoSize = true;
            this.createIndexCheckBox.Location = new System.Drawing.Point(79, 144);
            this.createIndexCheckBox.Name = "createIndexCheckBox";
            this.createIndexCheckBox.Size = new System.Drawing.Size(121, 17);
            this.createIndexCheckBox.TabIndex = 4;
            this.createIndexCheckBox.Text = "Create Spatial Index";
            this.createIndexCheckBox.UseVisualStyleBackColor = true;
            this.createIndexCheckBox.CheckedChanged += new System.EventHandler(this.CreateIndexCheckBox_OnCheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Data Column:";
            // 
            // shapeDataColumnNameTextBox
            // 
            this.shapeDataColumnNameTextBox.Location = new System.Drawing.Point(79, 92);
            this.shapeDataColumnNameTextBox.Name = "shapeDataColumnNameTextBox";
            this.shapeDataColumnNameTextBox.Size = new System.Drawing.Size(176, 20);
            this.shapeDataColumnNameTextBox.TabIndex = 2;
            this.shapeDataColumnNameTextBox.TextChanged += new System.EventHandler(this.ShapeDataColumnNameTextBox_OnTextChanged);
            // 
            // tableNameTextBox
            // 
            this.tableNameTextBox.Location = new System.Drawing.Point(79, 16);
            this.tableNameTextBox.Name = "tableNameTextBox";
            this.tableNameTextBox.Size = new System.Drawing.Size(177, 20);
            this.tableNameTextBox.TabIndex = 1;
            this.tableNameTextBox.TextChanged += new System.EventHandler(this.TableNameTextBox_OnTextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Table Name:";
            // 
            // importButton
            // 
            this.importButton.Enabled = false;
            this.importButton.Location = new System.Drawing.Point(454, 234);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(75, 23);
            this.importButton.TabIndex = 19;
            this.importButton.Text = "Import...";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.ImportButton_OnClick);
            // 
            // shapefileLoaderWorker
            // 
            this.shapefileLoaderWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ShapefileLoaderWorker_OnDoWork);
            this.shapefileLoaderWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ShapefileLoaderWorker_OnRunWorkerCompleted);
            // 
            // shapeCountLabel
            // 
            this.shapeCountLabel.AutoSize = true;
            this.shapeCountLabel.Location = new System.Drawing.Point(12, 239);
            this.shapeCountLabel.Name = "shapeCountLabel";
            this.shapeCountLabel.Size = new System.Drawing.Size(68, 13);
            this.shapeCountLabel.TabIndex = 20;
            this.shapeCountLabel.Text = "Shape count";
            this.shapeCountLabel.Visible = false;
            // 
            // optionsPanel
            // 
            this.optionsPanel.Controls.Add(this.FileNameTextBox);
            this.optionsPanel.Controls.Add(this.ConnectionStringButton);
            this.optionsPanel.Controls.Add(this.label1);
            this.optionsPanel.Controls.Add(this.groupBox1);
            this.optionsPanel.Controls.Add(this.connectionStringTextBox);
            this.optionsPanel.Controls.Add(this.panel1);
            this.optionsPanel.Controls.Add(this.OpenShapefileButton);
            this.optionsPanel.Controls.Add(this.label3);
            this.optionsPanel.Controls.Add(this.label2);
            this.optionsPanel.Location = new System.Drawing.Point(0, 0);
            this.optionsPanel.Name = "optionsPanel";
            this.optionsPanel.Size = new System.Drawing.Size(538, 230);
            this.optionsPanel.TabIndex = 21;
            // 
            // importProgressBar
            // 
            this.importProgressBar.Location = new System.Drawing.Point(5, 263);
            this.importProgressBar.Name = "importProgressBar";
            this.importProgressBar.Size = new System.Drawing.Size(524, 23);
            this.importProgressBar.TabIndex = 22;
            // 
            // importWorker
            // 
            this.importWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ImportWorker_OnDoWork);
            this.importWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ImportWorker_OnRunWorkerCompleted);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 295);
            this.Controls.Add(this.importProgressBar);
            this.Controls.Add(this.optionsPanel);
            this.Controls.Add(this.shapeCountLabel);
            this.Controls.Add(this.importButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainForm";
            this.Text = "Shapefile to SQL Import Tool";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.optionsPanel.ResumeLayout(false);
            this.optionsPanel.PerformLayout();
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel attributeMappingPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tableNameTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox createIndexCheckBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox shapeDataColumnNameTextBox;
        private System.Windows.Forms.CheckBox parallelCheckBox;
        private System.Windows.Forms.RadioButton geometryOption;
        private System.Windows.Forms.RadioButton geographyOption;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox sridTextBox;
        private System.Windows.Forms.Button importButton;
        private System.ComponentModel.BackgroundWorker shapefileLoaderWorker;
        private System.Windows.Forms.Label shapeCountLabel;
        private System.Windows.Forms.Panel optionsPanel;
        private System.Windows.Forms.ProgressBar importProgressBar;
        private System.ComponentModel.BackgroundWorker importWorker;
    }
}

