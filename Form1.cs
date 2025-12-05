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
                            listBoxTasks.Items.Add(GetShortZam(line));
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

        //укоротили длинные заметки для отображения
        private string GetShortZam(string note)
        {
            if (note.Length > 40)
                return note.Substring(0, 37) + "...";
            return note;
        }

        private void CountZam()
        {
            int count = notes.Count;

            labelText.Text = $"Список заметок: {count}";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string newwords = textBox1.Text.Trim();

            //пустая ли строка
            if (string.IsNullOrEmpty(newwords))
                MessageBox.Show("Введите текст заметки!");
            return;

            //добавляем заметку в список
            notes.Add(newwords);
            listBoxTasks.Items.Add(GetShortZam(newwords));

            textBox1.Clear();
            textBox1.Focus();

            //обновляем и сохраняем
            CountZam();
            SaveZam();
        }

        private void listBoxTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            //показываем полный текст выбранной заметки
            if (listBoxTasks.SelectedIndex != -1)
            {
                textBox1.Text = notes[listBoxTasks.SelectedIndex];
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxTasks.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите заметку для удаления!");
                return;
            }
        }

        private void labelText_Click(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }
    }
}