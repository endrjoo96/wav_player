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

            DirectoryInfo directoryInfo = new DirectoryInfo(@"D:\");
            if (directoryInfo.Exists)
            {
                DirList.AfterSelect += DirList_AfterSelect;
                BuildTree(directoryInfo, DirList.Nodes);
            }
        }

        private void BuildTree(DirectoryInfo directoryInfo, TreeNodeCollection addInMe)
        {
            TreeNode curNode = addInMe.Add(directoryInfo.Name);
            FileInfo[] fileCollection = directoryInfo.GetFiles();
            DirectoryInfo[] dirCollection = directoryInfo.GetDirectories();
            try
            {
                foreach (FileInfo file in fileCollection)
                {
                    curNode.Nodes.Add(file.FullName, file.Name);
                }
                foreach (DirectoryInfo subdir in dirCollection)
                {
                    BuildTree(subdir, curNode.Nodes);
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
