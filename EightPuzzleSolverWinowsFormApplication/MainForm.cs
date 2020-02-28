using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using EightPuzzleSolverClassLibrary;
using System.Linq;

namespace EightPuzzleSolverWinowsFormApplication
{
    public partial class MainForm : Form
    {
        Game game;
        List<Node> ShortestPath;

        int PrintCounter = 0;


        #region Header Panel
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void HeaderPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        #endregion


        public MainForm()
        {
            InitializeComponent();
            ReflectToPanel(Arrays.StartArray);
        }


        Button GetEmptyBtn()
        {
            Control control;

            for(int i = 1; i < 10; i++)
            {
                control = GamePanel.Controls["button" + i];

                if (string.IsNullOrEmpty(control.Text) || control.Text.Equals("0"))
                {
                    return (Button)control;
                }
            }

            return null;
        }
        void MoveButton(Button button)
        {
            Button emptyBtn = GetEmptyBtn();

            string temp = button.Text;
            button.Text = string.Empty;
            emptyBtn.Text = temp;
        }
        void ReflectToPanel(int[] list)
        {
            string text;

            for (int i = 0; i < 9; i++)
            {
                text = list[i].ToString();

                if (text.Equals("0"))
                {
                    GamePanel.Controls["button" + (i + 1)].Text = string.Empty;
                    continue;
                }

                GamePanel.Controls["button" + (i + 1)].Text = list[i].ToString();
            }
        }
        List<int> GetStartListFromPanel()
        {
            List<int> startList = new List<int>();

            int number;
            string text;

            // Read start list from the panel
            for (int i = 1; i < 10; i++)
            {
                text = GamePanel.Controls["button" + i].Text;
                if (!string.IsNullOrEmpty(text))
                {
                    number = int.Parse(text);
                    startList.Add(number);
                    continue;
                }

                startList.Add(0);
            }

            return startList;
        }
        void WaitMode(bool state)
        {
            GamePanel.Enabled = !state;
            SolveBtn.Enabled = !state;
            CustomBtn.Enabled = !state;

            if (state)
            {
                ResultLbl.Text = "Solving... Please Wait!";
            }
        }


        private void SolveTimer_Tick(object sender, EventArgs e)
        {
            if(PrintCounter < ShortestPath.Count)
            {
                ReflectToPanel(ShortestPath[PrintCounter++].Array);
                return;
            }

            PrintCounter--;
            ResultLbl.Text = string.Format("Solved with {0} step", PrintCounter);
            PrintCounter = 0;
            WaitMode(false);
            SolveTimer.Stop();
        }


        private async void SolveBtn_Click(object sender, EventArgs e)
        {
            List<int> startList = GetStartListFromPanel();

            if (Arrays.GoalArray.SequenceEqual(startList))
            {
                MessageBox.Show("Squares already equal to goal!\nPlease shuffle and try again.",
                                "Alert",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Start a new game
            game = new Game(startList.ToArray(), Arrays.GoalArray);

            // Disable form ( escape from another click which may cause an error )
            WaitMode(true);

            try
            {
                await game.Solve();

                // Get the shortest path found
                ShortestPath = game.GetPath();
            }
            catch (Exception ex)
            {
                WaitMode(false);
                MessageBox.Show(ex.Message, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Start showing path step by step
            SolveTimer.Start();
        }
        private void CustomBtn_Click(object sender, EventArgs e)
        {
            ResultLbl.Text = string.Empty;

            CustomForm customForm = new CustomForm();
            customForm.ShowDialog();

            if (customForm.Saved)
            {
                customForm.goalList.CopyTo(Arrays.GoalArray);
                ReflectToPanel(customForm.startList.ToArray());
            }
        }
        private void GameButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            // Check if clicked button is not the empty one
            if (string.IsNullOrEmpty(button.Text))
            {
                return;
            }

            // Check if clicked button is movable
            Point emptyLocation = GetEmptyBtn().Location;

            if (button.Location.X == emptyLocation.X)
            {
                if (button.Location.Y - emptyLocation.Y < 200 && button.Location.Y - emptyLocation.Y > -200)
                {
                    MoveButton(button);
                }
            }

            if (button.Location.Y == emptyLocation.Y)
            {
                if (button.Location.X - emptyLocation.X < 200 && button.Location.X - emptyLocation.X > -200)
                {
                    MoveButton(button);
                }
            }
        }
        private void GameBtn_TextChanged(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (string.IsNullOrEmpty(button.Text) || button.Text.Equals("0"))
            {
                button.BackColor = Color.White;
                button.FlatAppearance.MouseOverBackColor = Color.White;
                button.FlatAppearance.MouseDownBackColor = Color.White;
                return;
            }

            button.BackColor = SystemColors.ActiveCaption;
            button.FlatAppearance.MouseOverBackColor = SystemColors.ActiveCaption;
            button.FlatAppearance.MouseDownBackColor = SystemColors.ActiveCaption;
        }
    }
}