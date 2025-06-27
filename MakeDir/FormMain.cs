using System.IO;

namespace MakeDir
{
    /// <summary>
    /// メインフォーム
    /// </summary>
    public partial class FormMain : Form
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 作業ディレクトリに DragEnter したときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxWorkDir_DragEnter(object sender, DragEventArgs e)
        {
            // データがなければ
            if (e.Data == null)
            {
                // 受け付けない
                e.Effect = DragDropEffects.None;
            }
            // ファイルをドロップする場合
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // コピーモードで受け付ける
                e.Effect = DragDropEffects.Copy;
            }
            // それ以外の場合
            else
            {
                // 受け付けない
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// 作業ディレクトリに DragDrop したときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxWorkDir_DragDrop(object sender, DragEventArgs e)
        {
            // データがない場合
            if (e.Data == null)
            {
                // 何もしない
                return;
            }

            // ファイルパスの配列を取得
            var files = (string[]?)e.Data.GetData(DataFormats.FileDrop);

            // ファイルパスがない場合
            if (files == null || files.Length == 0)
            {
                // 何もしない
                return;
            }

            // ファイルパスが複数ある場合
            if (files.Length > 1)
            {
                // 
                MessageBox.Show("ファイル/ディレクトリは一つまでしか指定できません");
                // 何もしない
                return;
            }

            // ファイルパスを取得
            var file = files[0];

            // ファイルパスがディレクトリの場合
            if (Directory.Exists(file))
            {
                // ディレクトリ情報を取得
                var dirInfo = new DirectoryInfo(file);
                // ディレクトリ名を作業ディレクトリに設定
                SetWorkDir(dirInfo.FullName);
            }
            // ファイルパスがファイルの場合
            else if (File.Exists(file))
            {
                // ファイル情報を取得
                var fileInfo = new FileInfo(file);
                // ファイルのディレクトリ名を作業ディレクトリに設定
                SetWorkDir(fileInfo.DirectoryName);
            }
        }

        /// <summary>
        /// 作業ディレクトリを設定
        /// </summary>
        /// <param name="workDir">ディレクトリ名</param>
        private void SetWorkDir(string? workDir)
        {
            // ディレクトリ名が null の場合、空文字列にする
            workDir ??= string.Empty;
            // 作業ディレクトリ名を設定
            textBoxWorkDir.Text = workDir;
        }

        /// <summary>
        /// 作業ディレクトリの選択ボタンを押したときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelectWorkDir_Click(object sender, EventArgs e)
        {
            // ディレクトリ選択ダイアログを生成
            using (var dialog = new FolderBrowserDialog())
            {
                // 選択パスに今の作業ディレクトリを設定
                dialog.SelectedPath = textBoxWorkDir.Text;

                // ディレクトリ選択ダイアログを表示
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    // 何もしない
                    return;
                }

                // 作業ディレクトリを設定
                SetWorkDir(dialog.SelectedPath);
            }
        }

        /// <summary>
        /// 作成ディレクトリに DragEnter したときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxSubDirs_DragEnter(object sender, DragEventArgs e)
        {
            // データがない場合
            if (e.Data == null)
            {
                // 受け付けない
                e.Effect = DragDropEffects.None;
            }
            // データがファイルの場合
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // コピーモードで受け付ける
                e.Effect = DragDropEffects.Copy;
            }
            // それ以外の場合
            else
            {
                // 受け付けない
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// 作成ディレクトリに DragDrop したときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxSubDirs_DragDrop(object sender, DragEventArgs e)
        {
            // データがない場合
            if (e.Data == null)
            {
                // 何もしない
                return;
            }

            // ファイルパスの配列を取得する
            var files = (string[]?)e.Data.GetData(DataFormats.FileDrop);

            // ファイルパスが null もしくは一つもない場合
            if (files == null || files.Length == 0)
            {
                // 何もしない
                return;
            }

            // ファイルが２つ以上の場合
            if (files.Length > 1)
            {
                // 
                MessageBox.Show("ファイルは一つまでしか指定できません");
                // 何もしない
                return;
            }

            // ファイルパスを取得
            var file = files[0];

            // ファイルが存在しない場合
            if (!File.Exists(file))
            {
                // 
                MessageBox.Show("テキストファイル以外は読み込めません: " + file);
                // 何もしない
                return;
            }

            try
            {
                // Reader を生成
                using (var reader = new StreamReader(file))
                {
                    // 作成ディレクトリをクリア
                    textBoxSubDirs.Text = string.Empty;

                    // 繰り返し
                    while (true)
                    {
                        // １行読込
                        var line = reader.ReadLine();
                        // 読み込めない場合
                        if (line == null)
                        {
                            // ループを抜ける
                            break;
                        }

                        // 作成ディレクトリに追記
                        textBoxSubDirs.Text += line + Environment.NewLine;
                    }
                }
            }
            // 例外
            catch (Exception ex)
            {
                MessageBox.Show($"テキストファイルを読み込めません: {ex.Message}");
            }
        }

        /// <summary>
        /// ディレクトリ作成ボタンを押したときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMakeDir_Click(object sender, EventArgs e)
        {
            // 作業ディレクトリ
            var workDir = textBoxWorkDir.Text;
            // 作成ディレクトリの配列
            var subDirs = textBoxSubDirs.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            // 作成カウント
            var count = 0;

            // 作成ディレクトリに対して繰り返し
            foreach (var subDir in subDirs)
            {
                // トリム
                subDir.Trim();

                // 作成ディレクトリのフルパスを取得
                var dirPath = workDir + Path.DirectorySeparatorChar + subDir;

                // ディレクトリが存在する場合
                if (Directory.Exists(dirPath))
                {
                    // 何もしない
                    continue;
                }

                try
                {
                    // 作成ディレクトリを作成する
                    Directory.CreateDirectory(dirPath);
                    // 作成カウントを増やす
                    count ++;
                }
                catch (Exception ex)
                {
                    // 
                    MessageBox.Show($"ディレクトリの作成に失敗しました: {ex.Message}");
                    // ループを抜ける
                    break;
                }
            }

            // 
            MessageBox.Show($"{count} 個のディレクトリを作成しました");
        }

        /// <summary>
        /// フォームを読み込んだときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            // 作業ディレクトリに現在のカレントディレクトリを設定
            SetWorkDir(Directory.GetCurrentDirectory());
        }
    }
}
