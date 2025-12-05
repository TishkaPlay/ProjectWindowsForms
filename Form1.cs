using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SimpleTodoList
{
    public partial class MainForm : Form
    {
        //создаем список для заметок и где они будут хранится
        private List<string> notes = new List<string>();
        private string saveFile = "ThoughtSnap.txt";

        public MainForm()
        {
            InitializeComponent();
        }

        //нужно загрузить заметки из файла
        private void LoadZam()
        {
            try
            {
                if (File.Exists(saveFile))
                {
                    //читаем все строки
                    string[] lines = File.ReadAllLines(saveFile);

                    notes.Clear();
                    listBoxTasks.Items.Clear();

                    foreach (string line in lines)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            notes.Add(line);
                            listBoxTasks.Items.Add(GetShortNote(line));
                        }
                    }
                }
            }
            catch 
            {
                
            }
        }

        private void SaveZam()
        {
            try
            {
                //записываем все заметки в файл
                File.WriteAllLines(saveFile, notes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка! Не удалось сохранить: {ex.Message}");
            }
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {

        }

        private void listBoxTasks_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

        }

        private void labelText_Click(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }
    }
}