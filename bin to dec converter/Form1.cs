using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bin_to_dec_converter
{
    public partial class Form1 : Form
    {
        private List<Button> myButtons = new List<Button>();
        private Button[,] myButtonsMas = new Button[8, 8];
        public Form1()
        {
            InitializeComponent();

            int l = 64;

            foreach (Button btn in this.Controls.OfType<Button>())
            {
                myButtons.Add(btn);

                if (btn.Name != "buttonСlear") // проверяем, что это не кнопка buttonOk
                {
                    btn.Click += button_Click; //приводим к типу и устанавливаем обработчик события
                    btn.BackColor = ColorTranslator.FromHtml("#fafafa"); // цвет фона белый
                    btn.Tag = 0;
                }
            }

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    myButtonsMas[x, y] = myButtons[l--];
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e) // кнопка очистки поля
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    myButtonsMas[x, y].Tag = 0;
                    if (Convert.ToInt32(comboBoxY.Text) == 8 && Convert.ToInt32(comboBoxX.Text) == 8)
                        textBoxOutPut.Text = "0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,";
                    else if (Convert.ToInt32(comboBoxY.Text) == 6 && Convert.ToInt32(comboBoxX.Text) == 4)
                        textBoxOutPut.Text = "0b00000000, 0b00000000, 0b00000000, 0b00000000, 0b00000000, 0b00000000,";
                    else if (Convert.ToInt32(comboBoxY.Text) == 8 && Convert.ToInt32(comboBoxX.Text) == 6)
                        textBoxOutPut.Text = "0b00000000, 0b00000000, 0b00000000, 0b00000000, 0b00000000, 0b00000000, 0b00000000, 0b00000000,";

                    if (y <= 7 - Convert.ToInt32(comboBoxY.Text) || x <= 7 - Convert.ToInt32(comboBoxX.Text))
                    {
                        myButtonsMas[x, y].BackColor = ColorTranslator.FromHtml("#f3f3f3"); // цвет фона серый
                    }
                    else
                    {
                        myButtonsMas[x, y].BackColor = ColorTranslator.FromHtml("#fafafa"); // цвет фона белый
                    }
                }
            }
        }

        private void binToHex() // перевод из двоичной в шестнадцатеричную систему
        {
            string strTime = "";
            string str = "";

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    strTime += myButtonsMas[x, y].Tag;
                }
                if (Convert.ToInt32(comboBoxY.Text) == 8 && Convert.ToInt32(comboBoxX.Text) == 8)
                {
                    strTime = String.Format("{0:X2}", Convert.ToUInt64(strTime, 2));
                    str += "0x" + strTime + ", ";
                }
                else if (Convert.ToInt32(comboBoxY.Text) == 6 && Convert.ToInt32(comboBoxX.Text) == 4 && y < 6)
                {
                    str += "0b" + strTime + ", ";
                }
                else if (Convert.ToInt32(comboBoxY.Text) == 8 && Convert.ToInt32(comboBoxX.Text) == 6)
                {
                    str += "0b" + strTime + ", ";
                }
                strTime = "";
            }
            textBoxOutPut.Text = str;
        }

        private void updatingTheFieldSize(object sender, EventArgs e) // обновить размер поля
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (Convert.ToInt32(comboBoxY.Text) == 8 && Convert.ToInt32(comboBoxX.Text) == 8)
                        textBoxOutPut.Text = "0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,";
                    else if (Convert.ToInt32(comboBoxY.Text) == 6 && Convert.ToInt32(comboBoxX.Text) == 4)
                        textBoxOutPut.Text = "0b00000000, 0b00000000, 0b00000000, 0b00000000, 0b00000000, 0b00000000,";
                    else if (Convert.ToInt32(comboBoxY.Text) == 8 && Convert.ToInt32(comboBoxX.Text) == 6)
                        textBoxOutPut.Text = "0b00000000, 0b00000000, 0b00000000, 0b00000000, 0b00000000, 0b00000000, 0b00000000, 0b00000000,";

                    if (y <= 7 - Convert.ToInt32(comboBoxY.Text) || x <= 7 - Convert.ToInt32(comboBoxX.Text)) // кнопка неактивна. 
                    {
                        myButtonsMas[x, y].Enabled = false;
                        myButtonsMas[x, y].BackColor = ColorTranslator.FromHtml("#f3f3f3"); // цвет фона серый
                    }
                    else // кнопка активна.
                    {
                        myButtonsMas[x, y].Enabled = true;
                        myButtonsMas[x, y].BackColor = ColorTranslator.FromHtml("#fafafa"); // цвет фона белый
                    }
                }
            }
        }

        private void button_Click(object sender, EventArgs e)
        {

            if ((sender as Button).BackColor != Color.Black)
            {
                (sender as Button).BackColor = Color.Black;
                (sender as Button).Tag = 1;
            }

            else
            {
                (sender as Button).BackColor = ColorTranslator.FromHtml("#fafafa"); // цвет фона белый
                (sender as Button).Tag = 0;
            }

            binToHex(); // перевод из BIN в HEX
        }
    }
}
