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
                MessageBox.Show("�t�@�C��/�f�B���N�g���͈�܂ł����w��ł��܂���");
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
                MessageBox.Show("�t�@�C���͈�܂ł����w��ł��܂���");
                return;
            }

            var file = files[0];

            if (!File.Exists(file))
            {
                MessageBox.Show("�e�L�X�g�t�@�C���ȊO�͓ǂݍ��߂܂���: " + file);
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
                MessageBox.Show($"�e�L�X�g�t�@�C����ǂݍ��߂܂���: {ex.Message}");
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
                    MessageBox.Show($"�f�B���N�g���̍쐬�Ɏ��s���܂���: {ex.Message}");
                    break;
                }
            }

            MessageBox.Show($"{count} �̃f�B���N�g�����쐬���܂���");
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            SetWorkDir(Directory.GetCurrentDirectory());
        }
    }
}
