using System.Runtime.CompilerServices;
using System.Windows.Forms;
using myPongGame.Classes;

namespace myPongGame
{
    public partial class Form1 : Form
    {
        public int mode = 0;
        public int dbState = -1; // init state
        Game game;
        public Form1()
        {
            InitializeComponent();
            game = new Game(this);
            this.bestScore.Text = "������ ����: " + game.getBestScore();
        }

        private void startGame_Click(object sender, EventArgs e)
        {
            if (this.mode == 0)
            {
                mode = 1;
                // this.bestScore.Text = "������ ����: " + game.getBestScore();
                game.startGame();
            }
        }

        private void optionsButton_Click(object sender, EventArgs e)
        {
            if (this.mode == 0)
            {
                int[] gameSetting = game.getSetting();
                Settings setting = new Settings(gameSetting[0], gameSetting[1], gameSetting[2]);
                if (setting.ShowDialog() == DialogResult.OK)
                {
                    int[] newSetting = new int[3];
                    newSetting[0] = setting.botSetting.SelectedIndex;
                    newSetting[1] = setting.mapSetting.SelectedIndex;
                    newSetting[2] = setting.mapSizeSetting.SelectedIndex;
                    game.setSetting(newSetting, setting.host, setting.login, setting.password, setting.dbName);
                }

                // Scaling window to game field
                switch (setting.mapSizeSetting.SelectedIndex)
                {
                    case 0:
                        this.Size = new Size(818, 497);
                        this.CenterToScreen();
                        break;
                    case 1:
                        this.Size = new Size(1288, 737);
                        this.CenterToScreen();
                        break;
                    case 2:
                        this.Size = new Size(1938, 1048);
                        this.CenterToScreen();
                        break;
                }

                setting.Dispose();
            }
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionsButton_Click(sender, e);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            MessageBox.Show(this.Size.ToString());
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            String text = "����������� ������� W � S ��� ���������� ����������.\r\n���� ������ ������ ��� ����� ������ ����� � ������ ���������. � ��� ����� ���� ������" +
                "\r\n��� ������� ����� � ������� ��������� - ��� ������ ����� �� ��������.\r\n������������ ��� ����� ������ � �������� � ������� ������ �������!" +
                "\r\n\r\n";
            MessageBox.Show(text);
        }

        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            helpButton_Click(sender, e);
        }

        private void scoreButton_Click(object sender, EventArgs e)
        {
            this.bestScore.Text = "������ ����: " + game.getBestScore();
            MessageBox.Show(game.showBestPlayers());
        }

        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scoreButton_Click(sender, e);
        }

        private void �������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // �������� ��������� ����
            string filename = saveFileDialog1.FileName;
            // ��������� ����� � ����
            System.IO.File.WriteAllText(filename, game.showBestPlayers());
            MessageBox.Show("���� ��������");
        }
    }
}