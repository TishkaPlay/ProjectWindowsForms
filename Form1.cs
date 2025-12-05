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
            LoadZam();
            CountZam();
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

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(lines[i]))
                        {
                            notes.Add(lines[i]);
                            listBoxTasks.Items.Add(GetShortZam(lines[i], i));
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
        private string GetShortZam(string note, int index)
        {
            //добавим номер перед текстом
            string number = $"{index + 1}. ";

            //обрезаем длинный текст
            string shortText = note.Length > 40 ? note.Substring(0, 37) + "..." : note;

            return $"{number}{shortText}";
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
            {
                MessageBox.Show("Введите текст заметки!");
                return;
            }

            //добавляем заметку в список
            notes.Add(newwords);
            listBoxTasks.Items.Add(GetShortZam(newwords, notes.Count - 1)); //index новой заметки

            textBox1.Clear();
            textBox1.Focus();

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

            //получаем index выбранной заметки
            int selectedIndex = listBoxTasks.SelectedIndex;

            //и удаляем её
            notes.RemoveAt(selectedIndex);
            listBoxTasks.Items.RemoveAt(selectedIndex);

            for (int i = 0; i < listBoxTasks.Items.Count; i++)
            {
                listBoxTasks.Items[i] = GetShortZam(notes[i], i);
            }

            CountZam();
            SaveZam();
            textBox1.Clear();
        }

        private void labelText_Click(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveZam();
        }

        //увидел в инете и хотел попробовать реализовать, делаем двойной клик по заметки и редактируем её
        /*private void listBoxTasks_DoubleClick(object sender, KeyEventArgs e)
        {
            if (listBoxTasks.SelectedIndex != -1)
            {
                //получаем и сохраняем индекс выбранной заметки
                int selectedIndex = listBoxTasks.SelectedIndex;

                //если мы уже редактируем эту заметку - сохраняем изменения
                if (textBox1.Text != notes[selectedIndex] && textBox1.Text.Trim() != "")
                {
                    //сохраняем предыдущие изменения, если они были
                    notes[selectedIndex] = textBox1.Text.Trim();
                    listBoxTasks.Items[selectedIndex] = GetShortZam(notes[selectedIndex]);
                    SaveZam();
                    CountZam();
                }

                //показываем полный текст заметки для редактирования
                textBox1.Text = notes[selectedIndex];
                textBox1.Focus();
                textBox1.SelectAll();
                
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //находим какая заметка сейчас редактируется
                if (listBoxTasks.SelectedIndex != -1)
                {
                    int selectedIndex = listBoxTasks.SelectedIndex;

                    notes[selectedIndex] = textBox1.Text.Trim();
                    listBoxTasks.Items[selectedIndex] = GetShortZam(notes[selectedIndex]);

                    SaveZam();
                    CountZam();

                    textBox1.Clear();
                    MessageBox.Show("Изменения сохранены!");
                }
            }
        }*/

    }
}