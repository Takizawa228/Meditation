using System;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.FileFormats;
using NAudio.CoreAudioApi;
using NAudio;
using NAudio.Wave.SampleProviders;

namespace meditation
{
    public partial class Form1 : Form
    {
        public void FileReader()
        {
            string fileExtension = ".wav";
            
            try
            {
                string[] files = Directory.GetFiles(DataBank.path, "*" + fileExtension).ToArray();
                foreach (string filePath in files)
                    comboBox1.Items.Add(Path.GetFileName(filePath));
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        public Form1()
        {
            InitializeComponent();
           
            timer1.Interval = DataBank.num;
            timer1.Enabled = false;
            timer1.Tick += Timer1_Tick;

            FileReader();
            
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.PerformStep();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            settings settings = new settings();
            settings.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedState = comboBox1.SelectedItem.ToString();
            string selectedFile = Path.Combine(DataBank.path, selectedState);

            

            SoundPlayer audio = new SoundPlayer(selectedFile);

            if (timer1.Enabled == false)
            {
                timer1.Start();
                audio.PlayLooping();

                button1.Text = "Стоп";
            }
            else
            {
                timer1.Stop();
                audio.Stop();

                button1.Text = "Старт";
                progressBar1.Value = 0;

            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            
        }
    }
}
