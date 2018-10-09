using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WAV_player
{
    public partial class FileBrowser:Form
    {
        public FileBrowser()
        {
            InitializeComponent();
            FileList.Scrollable = true;
            FileList.View = View.Details;
            FileList.Columns.Add(new ColumnHeader{ Text = "", Name = "col" });
            FileList.HeaderStyle = ColumnHeaderStyle.None;
            FileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                string directoryInfo =drive.RootDirectory.FullName;
                if (Directory.Exists(directoryInfo))
                {
                    DirList.AfterSelect += DirList_AfterSelect;
                    DirList.BeforeExpand += DirList_BeforeExpand;
                    BuildTree(directoryInfo, DirList.Nodes);
                }
            }
        }

        private void DirList_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            //throw new NotImplementedException();
            BuildTree(e.Node.Text, e.Node.Nodes);
        }

        private void BuildTree(string directoryInfo, TreeNodeCollection addInMe)
        {
            string x = Path.GetFileName(directoryInfo);
            if ("".Equals(x))
                x = directoryInfo;
            TreeNode curNode = addInMe.Add(x);
            string[] dirCollection = Directory.GetDirectories(directoryInfo);
            try
            {
                foreach (string subdir in dirCollection)
                {
                    string[] sub = Directory.GetDirectories(subdir);
                    if (sub.Length == 0)
                        curNode.Nodes.Add(Path.GetFileName(subdir));
                }
            }catch (System.UnauthorizedAccessException ex)
            {
                Console.Write(ex.StackTrace);
            }
        }

        private void DirList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Name.EndsWith("wav"))
            {
                //do some stuff here
            }
        }
    }
}
