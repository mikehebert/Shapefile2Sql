namespace Shapefile2Sql
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    using Microsoft.Data.ConnectionUI;

    public partial class MainForm : Form
    {
        #region Constants and Fields

        private readonly ShapefileProcessor processor = new ShapefileProcessor();

        private readonly OpenShapefileProgressForm progressHandler = new OpenShapefileProgressForm();

        #endregion

        #region Constructors and Destructors

        public MainForm()
        {
            this.InitializeComponent();
            this.SetProcessorOptionsInterface();
        }

        #endregion

        #region Methods

        private void ConnectionStringButton_OnClick(object sender, EventArgs e)
        {
            using (var dialog = new DataConnectionDialog())
            {
                dialog.DataSources.Add(DataSource.SqlDataSource);
                dialog.ConnectionString = this.processor.ConnectionString;

                if (DataConnectionDialog.Show(dialog) == DialogResult.OK)
                {
                    this.connectionStringTextBox.Text = dialog.DisplayConnectionString;
                    this.processor.ConnectionString = dialog.ConnectionString;
                }
            }

            this.importButton.Enabled = this.processor.CanImport();
        }

        private void CreateMappingTable()
        {
            this.attributeMappingPanel.SuspendLayout();

            // Detach any existing event handlers
            foreach (TextBox textBox in this.attributeMappingPanel.Controls.OfType<TextBox>())
            {
                if (textBox.Tag is AttributeMapping)
                {
                    textBox.TextChanged -= this.MappedNameTextBox_OnTextChanged;
                }
            }

            this.attributeMappingPanel.Controls.Clear();
            this.attributeMappingPanel.RowStyles.Clear();

            // Add the title row to the attribute mapping panel
            var titleFont = new Font(
                "Microsoft Sans Serif", 8.0F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);

            this.attributeMappingPanel.RowStyles.Add(new RowStyle { Height = 18, SizeType = SizeType.Absolute });
            this.attributeMappingPanel.Controls.Add(
                new Label { Text = "Shape Attribute", Anchor = AnchorStyles.None, Font = titleFont }, 1, 0);
            this.attributeMappingPanel.Controls.Add(
                new Label { Text = "Column Name", Anchor = AnchorStyles.None, Font = titleFont }, 2, 0);

            // Add a row for each available attribute mapping
            for (int index = 0; index < this.processor.Mapping.Count; index++)
            {
                AttributeMapping mapItem = this.processor.Mapping[index];

                this.attributeMappingPanel.RowStyles.Add(new RowStyle { Height = 24, SizeType = SizeType.Absolute });

                this.attributeMappingPanel.Controls.Add(
                    new CheckBox { Anchor = AnchorStyles.None, Checked = true }, 0, index + 1);
                this.attributeMappingPanel.Controls.Add(
                    new TextBox { Enabled = false, Text = mapItem.ShapefileAttributeName, Anchor = AnchorStyles.None }, 
                    1, 
                    index + 1);

                var mappedNameTextBox = new TextBox
                    {
                       Text = mapItem.MappedColumnName, Anchor = AnchorStyles.None, Tag = mapItem 
                    };
                this.attributeMappingPanel.Controls.Add(mappedNameTextBox, 2, index + 1);
                mappedNameTextBox.TextChanged += this.MappedNameTextBox_OnTextChanged;
            }

            this.attributeMappingPanel.ResumeLayout(true);
        }

        private void GeographyOption_OnCheckedChanged(object sender, EventArgs e)
        {
            this.sridTextBox.Enabled = this.geographyOption.Checked;
            this.processor.SpatialDataType = this.geographyOption.Checked
                                                 ? SpatialDataType.Geography
                                                 : SpatialDataType.Geometry;
            this.importButton.Enabled = this.processor.CanImport();
        }

        private void ImportButton_OnClick(object sender, EventArgs e)
        {
            this.processor.Import();
        }

        private void LoadShapefile(string fileName)
        {
            this.progressHandler.Show();
            this.shapefileLoaderWorker.RunWorkerAsync(fileName);

            if (string.IsNullOrWhiteSpace(this.tableNameTextBox.Text))
            {
                this.tableNameTextBox.Text = Path.GetFileNameWithoutExtension(fileName);
            }
            else
            {
                this.processor.TableName = this.tableNameTextBox.Text;
            }
        }

        private void MappedNameTextBox_OnTextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                var mapItem = textBox.Tag as AttributeMapping;
                if (mapItem != null)
                {
                    mapItem.MappedColumnName = textBox.Text;
                }
            }
        }

        private void OpenShapefileButton_OnClick(object sender, EventArgs e)
        {
            if (this.OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.FileNameTextBox.Text = this.OpenFileDialog1.FileName;
                this.LoadShapefile(this.OpenFileDialog1.FileName);
            }
        }

        private void SetProcessorOptionsInterface()
        {
            this.createIndexCheckBox.Checked = this.processor.CreateSpatialIndex;
            this.parallelCheckBox.Checked = this.processor.ParallelImport;
            this.shapeDataColumnNameTextBox.Text = this.processor.ShapeDataColumnName;
            this.sridTextBox.Text = this.processor.Srid.ToString();
            if (this.processor.SpatialDataType == SpatialDataType.Geography)
            {
                this.geographyOption.Checked = true;
            }
            else
            {
                this.geometryOption.Checked = true;
            }
        }

        private void ShapeDataColumnNameTextBox_OnTextChanged(object sender, EventArgs e)
        {
            this.processor.ShapeDataColumnName = this.shapeDataColumnNameTextBox.Text;
            this.importButton.Enabled = this.processor.CanImport();
        }

        private void ShapefileLoaderWorker_OnDoWork(object sender, DoWorkEventArgs e)
        {
            this.progressHandler.Progress(string.Empty, 0, "Opening...");
            this.processor.LoadShapefile((string)e.Argument, this.progressHandler);
        }

        private void ShapefileLoaderWorker_OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.progressHandler.Hide();
            this.CreateMappingTable();
            this.importButton.Enabled = this.processor.CanImport();
        }

        private void SridTextBox_OnTextChanged(object sender, EventArgs e)
        {
            int srid;

            if (!int.TryParse(this.sridTextBox.Text, out srid))
            {
                MessageBox.Show("SRID must be a valid integer");
            }

            this.processor.Srid = srid;
            this.importButton.Enabled = this.processor.CanImport();
        }

        private void TableNameTextBox_OnTextChanged(object sender, EventArgs e)
        {
            this.processor.TableName = this.tableNameTextBox.Text;
            this.importButton.Enabled = this.processor.CanImport();
        }

        #endregion
    }
}