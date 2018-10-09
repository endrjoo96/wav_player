namespace WAV_player
{
    partial class FileBrowser
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
            this.FileList = new System.Windows.Forms.ListView();
            this.Ok_button = new System.Windows.Forms.Button();
            this.Cancel_button = new System.Windows.Forms.Button();
            this.DirList = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // FileList
            // 
            this.FileList.Location = new System.Drawing.Point(12, 275);
            this.FileList.Name = "FileList";
            this.FileList.Size = new System.Drawing.Size(233, 260);
            this.FileList.TabIndex = 0;
            this.FileList.UseCompatibleStateImageBehavior = false;
            // 
            // Ok_button
            // 
            this.Ok_button.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Ok_button.Location = new System.Drawing.Point(12, 541);
            this.Ok_button.Name = "Ok_button";
            this.Ok_button.Size = new System.Drawing.Size(114, 23);
            this.Ok_button.TabIndex = 1;
            this.Ok_button.Text = "OK";
            this.Ok_button.UseVisualStyleBackColor = true;
            // 
            // Cancel_button
            // 
            this.Cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_button.Location = new System.Drawing.Point(131, 541);
            this.Cancel_button.Name = "Cancel_button";
            this.Cancel_button.Size = new System.Drawing.Size(114, 23);
            this.Cancel_button.TabIndex = 2;
            this.Cancel_button.Text = "Anuluj";
            this.Cancel_button.UseVisualStyleBackColor = true;
            // 
            // DirList
            // 
            this.DirList.Location = new System.Drawing.Point(12, 13);
            this.DirList.Name = "DirList";
            this.DirList.Size = new System.Drawing.Size(233, 256);
            this.DirList.TabIndex = 3;
            // 
            // FileBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 576);
            this.Controls.Add(this.DirList);
            this.Controls.Add(this.Cancel_button);
            this.Controls.Add(this.Ok_button);
            this.Controls.Add(this.FileList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FileBrowser";
            this.Text = "Przeglądarka plików";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView FileList;
        private System.Windows.Forms.Button Ok_button;
        private System.Windows.Forms.Button Cancel_button;
        private System.Windows.Forms.TreeView DirList;
    }
}