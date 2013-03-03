﻿//REF:  http://www.minecraftwiki.net/wiki/Map_Item_Format
//      http://www.minecraftwiki.net/wiki/NBT_Format
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
            btnSave.Enabled = false;
            Logging.Log("Main Form loaded.");
        }

        private void frmMain_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            MessageBox.Show("TODO: this.");
            //TODO: Create About form, have button to open log
            e.Cancel = true;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Logging.Log("btnOpen clicked");
            BrowseDialog frmd = new BrowseDialog();
            BrowseDialog.BrowseForOptions bdOptions = BrowseDialog.BrowseForOptions.BrowseIncludeFiles;
            bdOptions = bdOptions | BrowseDialog.BrowseForOptions.UseNewUI;
            bdOptions = bdOptions | BrowseDialog.BrowseForOptions.NoNewFolderButton;
            bdOptions = bdOptions | BrowseDialog.BrowseForOptions.ReturnOnlyFileSystemDirectories;
            frmd.BrowseFor = bdOptions;
            frmd.Title = "Select a file or folder";
            if (frmd.ShowDialog(this) == DialogResult.OK)
            {
                Logging.Log("User selected " + frmd.Selected);
                if (Program.IsDirectory(frmd.Selected))
                {
                    //TODO: Create other interface for loading multiple maps
                }
                else
                {
                    MCMapItem MapItem = new MCMapItem(Program.UnGZIPFile(frmd.Selected));
                    Logging.Log("The scale is " + MapItem.Scale);
                    Logging.Log("The dimension is " + MapItem.Dimension);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Logging.Log("btnSave clicked");
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Logging.Log("Main Form closing...");
        }
    }
}
