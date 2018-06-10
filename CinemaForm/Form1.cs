using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FilmsLibrary1;
using System.IO;

namespace CinemaForm
{
    public partial class Form1 : Form
    {
        private Cinema cinema;
        private string currentFile;
        private Form2 form2;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadDataGridView();
            form2 = new Form2();
        }
        private void loadDataGridView()
        {
            this.Text = "Cinema schedule";

            //changeStatusStrip("Initialized.");

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
        }
        private void loadDataGridFromFile(string filepath)
        {
            if (filepath != "")                       // загрузка з файлу
            {
                cinema = new Cinema(filepath);
                try
                {
                    List<string> list;
                    dataGridView1.Rows.Clear();
                    for (int i = 0; i < cinema.List.Count; ++i)
                    {
                        list = cinema.List[i].ToList();
                        if (list.Count == 3)
                        {
                            dataGridView1.Rows.Add(list[0], list[1], list[2], '-');
                        }
                        else
                        {
                            dataGridView1.Rows.Add(list[0], list[1], list[2], list[3]);
                        }
                    }
                }
                catch (Exception e)
                {

                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            currentFile = openFileDialog1.FileName;
            loadDataGridFromFile(currentFile);
        }
        private void saveToFile(string filename)
        {
            StringBuilder buffer = new StringBuilder();
            //for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            //{
            //    for (int j = 0; j < dataGridView1.Columns.Count; ++j)
            //    {
            //        buffer.Append(dataGridView1[j, i].Value.ToString());
            //        buffer.Append(',');
            //    }
            //    buffer.Length--;
            //    buffer.Append('\n');
            //}
            for (int i = 0; i < cinema.List.Count; ++i)
            {
                if (cinema.List[i].ToList().Count == 3)
                {
                    buffer.Append(cinema.List[i].ToList()[0]);
                    buffer.Append(cinema.List[i].ToList()[1]);
                    buffer.Append(cinema.List[i].ToList()[2]);
                    buffer.Append('\n');
                }
                else
                {
                    buffer.Append(cinema.List[i].ToList()[0] + ", ");
                    buffer.Append(cinema.List[i].ToList()[1] + ", ");
                    buffer.Append(cinema.List[i].ToList()[2] + ", ");
                    buffer.Append(cinema.List[i].ToList()[3]);
                    buffer.Append('\n');
                }
            }
            System.IO.File.WriteAllText(filename, buffer.ToString());
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentFile != null)
            {
                saveToFile(currentFile);
            }
            else
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "TXT files(*.txt)|*.txt|All files(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            String filename = saveFileDialog1.FileName;
            saveToFile(filename);
        }
        private void addDataGridViewRow(string name, string director, double budget, string composer = "-")
        {
            if (composer == "")
            {
                composer = "-";
            }
            dataGridView1.Rows.Add(name, director, budget, composer);
            if (composer == "-")
            {
                cinema.AddFilm(new Film(name, director, budget));
            }
            else
            {
                cinema.AddFilm(new MusicalFilm(name, director, budget, composer));
            }

            //changeStatusStrip("Book added.");
        }
        private void clearBoxesForm2()
        {
            form2.textBox1.Text = "";
            form2.textBox2.Text = "";
            form2.textBox3.Text = "";
            form2.textBox4.Text = "";
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            if (form2.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    addDataGridViewRow(form2.textBox1.Text, form2.textBox2.Text, Convert.ToDouble(form2.textBox3.Text), form2.textBox4.Text);
                }
                catch (FormatException eGap)
                {
                    MessageBox.Show("Incorrect data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                catch (Exception eGap)
                {
                    //changeStatusStrip(eGap.Message);
                }
            }
            clearBoxesForm2();
        }
        private void deleteRowDGV(int RowIndex)
        {
            dataGridView1.Rows.RemoveAt(RowIndex);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                deleteRowDGV(dataGridView1.CurrentRow.Index);
            }
            catch (Exception)
            {
                MessageBox.Show("Nothing to delete!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                form2.textBox1.Text = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
                form2.textBox2.Text = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
                form2.textBox3.Text = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
                form2.textBox4.Text = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
                if (form2.ShowDialog() == DialogResult.OK)
                {
                    if (form2.textBox1.Text == "" || form2.textBox2.Text == "" || form2.textBox3.Text == "" || form2.textBox4.Text == "")
                    {
                        throw new FormatException();
                    }
                    else
                    {
                        dataGridView1[0, dataGridView1.CurrentRow.Index].Value = form2.textBox1.Text;
                        dataGridView1[1, dataGridView1.CurrentRow.Index].Value = form2.textBox2.Text;
                        dataGridView1[2, dataGridView1.CurrentRow.Index].Value = Convert.ToDouble(form2.textBox3.Text);
                    }
                }
                clearBoxesForm2();
            }
            catch (FormatException)
            {
                MessageBox.Show("Incorrect data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            catch (Exception)
            {
                MessageBox.Show("Nothing to edit!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void clearDataGrid()
        {
            dataGridView1.Rows.Clear();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            clearDataGrid();
        }
    }
}
