using EightPuzzleSolverClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EightPuzzleSolverWinowsFormApplication
{
    public partial class CustomForm : Form
    {

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
            Close();
        }

        #endregion



        int Counter = 1;


        public List<int> startList { get; set; }
        public List<int> goalList { get; set; }
        public bool Saved { get; set; }



        public CustomForm()
        {
            InitializeComponent();
            startList = new List<int>();
            goalList = Arrays.GoalArray.ToList();
            StartRdBtn.Checked = true;
        }



        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.Enabled = false;

            ResultGrpBx.Controls["rButton" + Counter].Text = button.Text;
            Counter++;

            if (StartRdBtn.Checked)
            {
                startList.Add(button.Text == string.Empty ? 0 :  int.Parse(button.Text));
            }
            else if (GoalRdBtn.Checked)
            {
                goalList.Add(button.Text == string.Empty ? 0 : int.Parse(button.Text));
            }
        }
        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Counter = 1;

            if (StartRdBtn.Checked)
            {
                startList.Clear();
            }
            else if (GoalRdBtn.Checked)
            {
                goalList.Clear();
            }

            ClearResultPanel();

            // Enable all input buttons because selected list is empty
            for(int i = 1; i < 10; i++)
            {
                MainGrpBx.Controls["button" + i].Enabled = true;
            }
        }
        private void Radio_CheckChanged(object sender, EventArgs e)
        {
            if (StartRdBtn.Checked)
            {
                ReflectToResultPanel(startList);
                return;
            }

            ReflectToResultPanel(goalList);
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if(startList.Count != 9 || goalList.Count != 9)
            {
                MessageBox.Show("Please fill both start and goal matrix, then try again.",
                                "Alert",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }
            Saved = true;
            Close();
        }


        void UpdateButtonsPanel(List<int> list)
        {
            for (int i = 1; i < 10; i++)
            {
                int value;
                if (string.IsNullOrEmpty(MainGrpBx.Controls["button" + i].Text))
                {
                    value = 0;
                }
                else
                {
                    value = int.Parse(MainGrpBx.Controls["button" + i].Text);
                }

                if (list.Contains(value))
                {
                    MainGrpBx.Controls["button" + i].Enabled = false;
                    continue;
                }

                MainGrpBx.Controls["button" + i].Enabled = true;
            }
        }
        void ClearResultPanel()
        {
            for (int i = 1; i < 10; i++)
            {
                ResultGrpBx.Controls["rButton" + i].Text = string.Empty;
            }
        }
        void ReflectToResultPanel(List<int> list)
        {
            ClearResultPanel();            

            int number;

            for (int i = 1; i <= list.Count; i++)
            {
                number = list[i - 1];

                if (number == 0)
                {
                    continue;
                }

                ResultGrpBx.Controls["rButton" + i].Text = number.ToString();
            }

            UpdateButtonsPanel(list);
            Counter = list.Count + 1;
        }
    }
}
