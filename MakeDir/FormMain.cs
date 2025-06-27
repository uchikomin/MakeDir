using System.IO;

namespace MakeDir
{
    /// <summary>
    /// ���C���t�H�[��
    /// </summary>
    public partial class FormMain : Form
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��ƃf�B���N�g���� DragEnter �����Ƃ��̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxWorkDir_DragEnter(object sender, DragEventArgs e)
        {
            // �f�[�^���Ȃ����
            if (e.Data == null)
            {
                // �󂯕t���Ȃ�
                e.Effect = DragDropEffects.None;
            }
            // �t�@�C�����h���b�v����ꍇ
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // �R�s�[���[�h�Ŏ󂯕t����
                e.Effect = DragDropEffects.Copy;
            }
            // ����ȊO�̏ꍇ
            else
            {
                // �󂯕t���Ȃ�
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// ��ƃf�B���N�g���� DragDrop �����Ƃ��̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxWorkDir_DragDrop(object sender, DragEventArgs e)
        {
            // �f�[�^���Ȃ��ꍇ
            if (e.Data == null)
            {
                // �������Ȃ�
                return;
            }

            // �t�@�C���p�X�̔z����擾
            var files = (string[]?)e.Data.GetData(DataFormats.FileDrop);

            // �t�@�C���p�X���Ȃ��ꍇ
            if (files == null || files.Length == 0)
            {
                // �������Ȃ�
                return;
            }

            // �t�@�C���p�X����������ꍇ
            if (files.Length > 1)
            {
                // 
                MessageBox.Show("�t�@�C��/�f�B���N�g���͈�܂ł����w��ł��܂���");
                // �������Ȃ�
                return;
            }

            // �t�@�C���p�X���擾
            var file = files[0];

            // �t�@�C���p�X���f�B���N�g���̏ꍇ
            if (Directory.Exists(file))
            {
                // �f�B���N�g�������擾
                var dirInfo = new DirectoryInfo(file);
                // �f�B���N�g��������ƃf�B���N�g���ɐݒ�
                SetWorkDir(dirInfo.FullName);
            }
            // �t�@�C���p�X���t�@�C���̏ꍇ
            else if (File.Exists(file))
            {
                // �t�@�C�������擾
                var fileInfo = new FileInfo(file);
                // �t�@�C���̃f�B���N�g��������ƃf�B���N�g���ɐݒ�
                SetWorkDir(fileInfo.DirectoryName);
            }
        }

        /// <summary>
        /// ��ƃf�B���N�g����ݒ�
        /// </summary>
        /// <param name="workDir">�f�B���N�g����</param>
        private void SetWorkDir(string? workDir)
        {
            // �f�B���N�g������ null �̏ꍇ�A�󕶎���ɂ���
            workDir ??= string.Empty;
            // ��ƃf�B���N�g������ݒ�
            textBoxWorkDir.Text = workDir;
        }

        /// <summary>
        /// ��ƃf�B���N�g���̑I���{�^�����������Ƃ��̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelectWorkDir_Click(object sender, EventArgs e)
        {
            // �f�B���N�g���I���_�C�A���O�𐶐�
            using (var dialog = new FolderBrowserDialog())
            {
                // �I���p�X�ɍ��̍�ƃf�B���N�g����ݒ�
                dialog.SelectedPath = textBoxWorkDir.Text;

                // �f�B���N�g���I���_�C�A���O��\��
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    // �������Ȃ�
                    return;
                }

                // ��ƃf�B���N�g����ݒ�
                SetWorkDir(dialog.SelectedPath);
            }
        }

        /// <summary>
        /// �쐬�f�B���N�g���� DragEnter �����Ƃ��̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxSubDirs_DragEnter(object sender, DragEventArgs e)
        {
            // �f�[�^���Ȃ��ꍇ
            if (e.Data == null)
            {
                // �󂯕t���Ȃ�
                e.Effect = DragDropEffects.None;
            }
            // �f�[�^���t�@�C���̏ꍇ
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // �R�s�[���[�h�Ŏ󂯕t����
                e.Effect = DragDropEffects.Copy;
            }
            // ����ȊO�̏ꍇ
            else
            {
                // �󂯕t���Ȃ�
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// �쐬�f�B���N�g���� DragDrop �����Ƃ��̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxSubDirs_DragDrop(object sender, DragEventArgs e)
        {
            // �f�[�^���Ȃ��ꍇ
            if (e.Data == null)
            {
                // �������Ȃ�
                return;
            }

            // �t�@�C���p�X�̔z����擾����
            var files = (string[]?)e.Data.GetData(DataFormats.FileDrop);

            // �t�@�C���p�X�� null �������͈���Ȃ��ꍇ
            if (files == null || files.Length == 0)
            {
                // �������Ȃ�
                return;
            }

            // �t�@�C�����Q�ȏ�̏ꍇ
            if (files.Length > 1)
            {
                // 
                MessageBox.Show("�t�@�C���͈�܂ł����w��ł��܂���");
                // �������Ȃ�
                return;
            }

            // �t�@�C���p�X���擾
            var file = files[0];

            // �t�@�C�������݂��Ȃ��ꍇ
            if (!File.Exists(file))
            {
                // 
                MessageBox.Show("�e�L�X�g�t�@�C���ȊO�͓ǂݍ��߂܂���: " + file);
                // �������Ȃ�
                return;
            }

            try
            {
                // Reader �𐶐�
                using (var reader = new StreamReader(file))
                {
                    // �쐬�f�B���N�g�����N���A
                    textBoxSubDirs.Text = string.Empty;

                    // �J��Ԃ�
                    while (true)
                    {
                        // �P�s�Ǎ�
                        var line = reader.ReadLine();
                        // �ǂݍ��߂Ȃ��ꍇ
                        if (line == null)
                        {
                            // ���[�v�𔲂���
                            break;
                        }

                        // �쐬�f�B���N�g���ɒǋL
                        textBoxSubDirs.Text += line + Environment.NewLine;
                    }
                }
            }
            // ��O
            catch (Exception ex)
            {
                MessageBox.Show($"�e�L�X�g�t�@�C����ǂݍ��߂܂���: {ex.Message}");
            }
        }

        /// <summary>
        /// �f�B���N�g���쐬�{�^�����������Ƃ��̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMakeDir_Click(object sender, EventArgs e)
        {
            // ��ƃf�B���N�g��
            var workDir = textBoxWorkDir.Text;
            // �쐬�f�B���N�g���̔z��
            var subDirs = textBoxSubDirs.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            // �쐬�J�E���g
            var count = 0;

            // �쐬�f�B���N�g���ɑ΂��ČJ��Ԃ�
            foreach (var subDir in subDirs)
            {
                // �g����
                subDir.Trim();

                // �쐬�f�B���N�g���̃t���p�X���擾
                var dirPath = workDir + Path.DirectorySeparatorChar + subDir;

                // �f�B���N�g�������݂���ꍇ
                if (Directory.Exists(dirPath))
                {
                    // �������Ȃ�
                    continue;
                }

                try
                {
                    // �쐬�f�B���N�g�����쐬����
                    Directory.CreateDirectory(dirPath);
                    // �쐬�J�E���g�𑝂₷
                    count ++;
                }
                catch (Exception ex)
                {
                    // 
                    MessageBox.Show($"�f�B���N�g���̍쐬�Ɏ��s���܂���: {ex.Message}");
                    // ���[�v�𔲂���
                    break;
                }
            }

            // 
            MessageBox.Show($"{count} �̃f�B���N�g�����쐬���܂���");
        }

        /// <summary>
        /// �t�H�[����ǂݍ��񂾂Ƃ��̏���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            // ��ƃf�B���N�g���Ɍ��݂̃J�����g�f�B���N�g����ݒ�
            SetWorkDir(Directory.GetCurrentDirectory());
        }
    }
}
