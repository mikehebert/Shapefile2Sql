namespace Shapefile2Sql
{
    using System;
    using System.Threading;
    using System.Windows.Forms;

    using DotSpatial.Data;

    using Microsoft.Data.ConnectionUI;

    public partial class MainForm : Form
    {
        #region Constants and Fields

        private string connectionString;

        private Shapefile shapefile;

        #endregion

        #region Constructors and Destructors

        public MainForm()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        private void ConnectionStringButton_OnClick(object sender, EventArgs e)
        {
            using (var dialog = new DataConnectionDialog())
            {
                dialog.DataSources.Add(DataSource.SqlDataSource);
                dialog.ConnectionString = this.connectionString;

                if (DataConnectionDialog.Show(dialog) == DialogResult.OK)
                {
                    this.connectionStringTextBox.Text = dialog.DisplayConnectionString;
                    this.connectionString = dialog.ConnectionString;
                }
            }
        }

        private void LoadShapefile(string fileName)
        {
            using (var progessHandler = new OpenShapefileProgressForm())
            {
                progessHandler.Show();

                var t = new Thread(() => { this.shapefile = Shapefile.OpenFile(fileName, progessHandler); })
                    {
                       IsBackground = true 
                    };
                t.Start();
                t.Join();

                progessHandler.Close();
                MessageBox.Show(this.shapefile.GetType().ToString());
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

        #endregion
    }
}