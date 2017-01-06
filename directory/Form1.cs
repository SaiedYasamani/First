using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace directory
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtPath.Text = System.IO.Directory.GetCurrentDirectory();
        }

        /// <summary>
        /// function for get & load directories & files in given path.
        /// </summary>
        /// <param name="path"></param>
        private void GetDirectoryInfo(string path)
        {
            //define a directory info object:
            System.IO.DirectoryInfo oDirectoryInfo = new System.IO.DirectoryInfo(path);

            //clear & load directories & files in lists:
            lstDirectories.Items.Clear();
            lstFiles.Items.Clear();
            if (path.Length > 3) //chek if path is not root to add back link.
            {
                lstDirectories.Items.Add("..");
            }

            foreach (System.IO.DirectoryInfo oCurrentDirectory in oDirectoryInfo.GetDirectories())
            {
                lstDirectories.Items.Add(oCurrentDirectory);
            }

            foreach (System.IO.FileInfo oCurrentFile in oDirectoryInfo.GetFiles())
            {
                lstFiles.Items.Add(oCurrentFile);
            }
        }
        private void btnGo_Click(object sender, EventArgs e)
        {
            //check if inputed path to be valid:
            if (String.IsNullOrWhiteSpace(txtPath.Text))
            {
                MessageBox.Show(
                                text: "مسیر وارد شده معتبر نمی باشد.", 
                                caption: "خطا!", 
                                buttons: MessageBoxButtons.OK, 
                                defaultButton: MessageBoxDefaultButton.Button1, 
                                icon: MessageBoxIcon.Error, 
                                options: MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading
                               );
                txtPath.Text = String.Empty;
                txtPath.Focus();
                return;
            }

            //remove brfore & after spaces:
            txtPath.Text = txtPath.Text.Trim();

            //Call GetDirectoryInfo function:
            GetDirectoryInfo(txtPath.Text);
        }

        private void lstDirectories_DoubleClick(object sender, EventArgs e)
        {
            if (lstDirectories.SelectedItem != null) //check if selected item to be valid.
            {
                //set new path:
                if (lstDirectories.SelectedItem.ToString() == "..")
                {
                    int intLastIndex = txtPath.Text.LastIndexOf("\\");
                    txtPath.Text = txtPath.Text.Substring(0, intLastIndex);
                }
                else
                {
                    txtPath.Text = string.Format("{0}\\{1}", txtPath.Text, lstDirectories.SelectedItem.ToString());
                }

                //Call GetDirectoryInfo function
                GetDirectoryInfo(txtPath.Text);
            }
        }
    }
}
