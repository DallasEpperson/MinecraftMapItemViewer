using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinecraftMapItemViewer
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text = "Minecraft Map Item Viewer";
            lblFilename.Text = "No Map Item file selected!";
            Program.Log("Main Form loaded.");
        }

        private void frmMain_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            MessageBox.Show("turdburglar");
            //TODO: Create About form, have button to open log
            e.Cancel = true;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Program.Log("btnOpen clicked");
            BrowseDialog frmd = new BrowseDialog();
            BrowseDialog.BrowseForOptions bdOptions = BrowseDialog.BrowseForOptions.BrowseIncludeFiles;
            bdOptions = bdOptions | BrowseDialog.BrowseForOptions.UseNewUI;
            bdOptions = bdOptions | BrowseDialog.BrowseForOptions.NoNewFolderButton;
            bdOptions = bdOptions | BrowseDialog.BrowseForOptions.ReturnOnlyFileSystemDirectories;

            frmd.BrowseFor = bdOptions;
            frmd.Title = "Select a file or folder";
            if (frmd.ShowDialog(this) == DialogResult.OK)
            {
                Program.Log("User selected " + frmd.Selected);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Program.Log("btnSave clicked");
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.Log("Main Form closing...");
        }
    }
}
