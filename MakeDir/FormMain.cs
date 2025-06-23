using System.IO;

namespace MakeDir
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void textBoxWorkDir_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data == null)
            {
                e.Effect = DragDropEffects.None;
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void textBoxWorkDir_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data == null)
            {
                return;
            }

            var files = (string[]?)e.Data.GetData(DataFormats.FileDrop);

            if (files == null || files.Length == 0)
            {
                return;
            }

            if (files.Length > 1)
            {
                MessageBox.Show("ファイル/ディレクトリは一つまでしか指定できません");
                return;
            }

            var file = files[0];

            if (Directory.Exists(file))
            {
                var dirInfo = new DirectoryInfo(file);
                SetWorkDir(dirInfo.FullName);
            }
            else if (File.Exists(file))
            {
                var fileInfo = new FileInfo(file);
                SetWorkDir(fileInfo.DirectoryName);
            }
        }

        private void SetWorkDir(string? workDir)
        {
            workDir ??= string.Empty;
            textBoxWorkDir.Text = workDir;
        }

        private void buttonSelectWorkDir_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();

            dialog.SelectedPath = textBoxWorkDir.Text;

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            SetWorkDir(dialog.SelectedPath);
        }

        private void textBoxSubDirs_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data == null)
            {
                e.Effect = DragDropEffects.None;
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void textBoxSubDirs_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data == null)
            {
                return;
            }

            var files = (string[]?)e.Data.GetData(DataFormats.FileDrop);

            if (files == null || files.Length == 0)
            {
                return;
            }

            if (files.Length > 1)
            {
                MessageBox.Show("ファイルは一つまでしか指定できません");
                return;
            }

            var file = files[0];

            if (!File.Exists(file))
            {
                MessageBox.Show("テキストファイル以外は読み込めません: " + file);
                return;
            }

            try
            {
                var reader = new StreamReader(file);

                while (true)
                {
                    var line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    textBoxSubDirs.Text += line + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"テキストファイルを読み込めません: {ex.Message}");
            }
        }

        private void buttonMakeDir_Click(object sender, EventArgs e)
        {
            var workDir = textBoxWorkDir.Text;
            var subDirs = textBoxSubDirs.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            var count = 0;

            foreach (var subDir in subDirs)
            {
                subDir.Trim();

                var dirPath = workDir + Path.DirectorySeparatorChar + subDir;

                if (Directory.Exists(dirPath))
                {
                    continue;
                }

                try
                {
                    Directory.CreateDirectory(dirPath);
                    count++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"ディレクトリの作成に失敗しました: {ex.Message}");
                    break;
                }
            }

            MessageBox.Show($"{count} 個のディレクトリを作成しました");
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            SetWorkDir(Directory.GetCurrentDirectory());
        }
    }
}
