namespace Shapefile2Sql
{
    using System.Windows.Forms;

    using DotSpatial.Data;

    /// <summary>
    /// The open shapefile progress form.
    /// </summary>
    public partial class OpenShapefileProgressForm : Form, IProgressHandler
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenShapefileProgressForm"/> class.
        /// </summary>
        public OpenShapefileProgressForm()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Progress is the method that should receive a progress message.
        /// </summary>
        /// <param name="key">
        /// The message string without any information about the status of completion.
        /// </param>
        /// <param name="percent">
        /// An integer from 0 to 100 that indicates the condition for a status bar etc.
        /// </param>
        /// <param name="message">
        /// A string containing both information on what the process is, as well as its completion status.
        /// </param>
        public void Progress(string key, int percent, string message)
        {
            if (percent == 0 && this.progressBar1.Style != ProgressBarStyle.Marquee)
            {
                this.Invoke((MethodInvoker)(() => this.progressBar1.Style = ProgressBarStyle.Marquee));
            }
            else
            {
                this.Invoke((MethodInvoker)(() => this.progressBar1.Style = ProgressBarStyle.Continuous));
            }

            // The shapefile loader from DotSpatial reports "Ready." before it is done...
            if (message.Equals("Ready."))
            {
                this.Invoke((MethodInvoker)(() => this.label1.Text = "Please wait..."));
            }
            else
            {
                this.Invoke((MethodInvoker)(() => 
                    { 
                        this.label1.Text = message;
                        this.progressBar1.Value = percent;
                    }));
            }
        }

        #endregion
    }
}