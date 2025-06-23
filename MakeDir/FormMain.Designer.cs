namespace MakeDir
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            label1 = new Label();
            textBoxWorkDir = new TextBox();
            buttonSelectWorkDir = new Button();
            buttonMakeDir = new Button();
            label2 = new Label();
            textBoxSubDirs = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(81, 15);
            label1.TabIndex = 0;
            label1.Text = "作業ディレクトリ";
            // 
            // textBoxWorkDir
            // 
            textBoxWorkDir.AllowDrop = true;
            textBoxWorkDir.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxWorkDir.Location = new Point(99, 6);
            textBoxWorkDir.Name = "textBoxWorkDir";
            textBoxWorkDir.ReadOnly = true;
            textBoxWorkDir.Size = new Size(461, 23);
            textBoxWorkDir.TabIndex = 0;
            textBoxWorkDir.DragDrop += textBoxWorkDir_DragDrop;
            textBoxWorkDir.DragEnter += textBoxWorkDir_DragEnter;
            // 
            // buttonSelectWorkDir
            // 
            buttonSelectWorkDir.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonSelectWorkDir.Location = new Point(566, 5);
            buttonSelectWorkDir.Name = "buttonSelectWorkDir";
            buttonSelectWorkDir.Size = new Size(32, 24);
            buttonSelectWorkDir.TabIndex = 1;
            buttonSelectWorkDir.Text = "...";
            buttonSelectWorkDir.UseVisualStyleBackColor = true;
            buttonSelectWorkDir.Click += buttonSelectWorkDir_Click;
            // 
            // buttonMakeDir
            // 
            buttonMakeDir.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonMakeDir.Location = new Point(523, 259);
            buttonMakeDir.Name = "buttonMakeDir";
            buttonMakeDir.Size = new Size(75, 23);
            buttonMakeDir.TabIndex = 3;
            buttonMakeDir.Text = "作成";
            buttonMakeDir.UseVisualStyleBackColor = true;
            buttonMakeDir.Click += buttonMakeDir_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 44);
            label2.Name = "label2";
            label2.Size = new Size(81, 15);
            label2.TabIndex = 4;
            label2.Text = "作成ディレクトリ";
            // 
            // textBoxSubDirs
            // 
            textBoxSubDirs.AllowDrop = true;
            textBoxSubDirs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxSubDirs.Location = new Point(12, 62);
            textBoxSubDirs.Multiline = true;
            textBoxSubDirs.Name = "textBoxSubDirs";
            textBoxSubDirs.ScrollBars = ScrollBars.Vertical;
            textBoxSubDirs.Size = new Size(586, 191);
            textBoxSubDirs.TabIndex = 2;
            textBoxSubDirs.DragDrop += textBoxSubDirs_DragDrop;
            textBoxSubDirs.DragEnter += textBoxSubDirs_DragEnter;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(610, 294);
            Controls.Add(textBoxSubDirs);
            Controls.Add(label2);
            Controls.Add(buttonMakeDir);
            Controls.Add(buttonSelectWorkDir);
            Controls.Add(textBoxWorkDir);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(390, 266);
            Name = "FormMain";
            Text = "MakeDir";
            Load += FormMain_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBoxWorkDir;
        private Button buttonSelectWorkDir;
        private Button buttonMakeDir;
        private Label label2;
        private TextBox textBoxSubDirs;
    }
}
