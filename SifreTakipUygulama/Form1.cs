using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SifreTakipUygulama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Yazılan cümleyi oluşturan karakterler kullanılarak ve aralara özel karakterler serpiştirilereksadece size özel bir şifre üretir.","BİLGİ", MessageBoxButtons.OK , MessageBoxIcon.Question);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Seçilen zorluğa göre ASCII kod tablosundan rastgele karakterler seçilerek, istenen uzunlukta şifre oluşturulur", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = System.Windows.Forms.Application.StartupPath + "//Sifrelerim.txt";
                string writeText = textBox3.Text + " = " + textBox2.Text;
                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Close();

                File.AppendAllText(fileName, Environment.NewLine + writeText);
                MessageBox.Show("Kaydedildi");
                button3.Enabled = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Kaydedilemedi" + Environment.NewLine + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("a=@, S=5, G=6, g=9, l=1,  Z=2, B=8, O=0, T=7", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        int[] zorluk;
        private void button1_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            textBox2.Text = "";
            int sifre;
            string karakter = "";

            //zorluk = new int[] { 33, 47 };// ASCII kod tablosundaki özel karakter kodları #1
            //zorluk = new int[] { 48, 57 };// ASCII kod tablosundaki rakam karakter kodları
            //zorluk = new int[] { 58, 64 };// ASCII kod tablosundaki özel karakter kodları #2
            //zorluk = new int[] { 65, 90 };// ASCII kod tablosundaki Büyük harf karakter kodları
            //zorluk = new int[] { 91, 96 };// ASCII kod tablosundaki özel karakter kodları #3
            //zorluk = new int[] { 97, 122 };// ASCII kod tablosundaki Küçük harf karakter kodları


            Random rastgele = new Random();
            if (comboBox1.SelectedIndex >= 0)
            {
                switch (comboBox1.SelectedIndex)
                {
                    //Sadece Büyük Harf
                    case 0: zorluk = new int[] { 65, 90 }; break;
                    //Sadece Küçük Harf
                    case 1: zorluk = new int[] { 97, 122 }; break;
                    //Sadece Rakam
                    case 2: zorluk = new int[] { 48, 57 }; break;
                    //Rakam ve Özel Karakterler
                    case 3: zorluk = new int[] { 33, 64 }; break;
                    //Harfler Özel Karakterler
                    case 4: zorluk = new int[] { 58, 122 }; break;
                    //Tamamı Karışık
                    case 5: zorluk = new int[] { 33, 122 }; break;
                }

                for (int i = 0; i < numericUpDown1.Value; i++)
                {
                    sifre = rastgele.Next(zorluk[0], zorluk[1]); // ASCII tablosundan rastgele bir karakter seçiyoruz.
                    karakter += Convert.ToChar(sifre).ToString(); // Rastgele seçilmiş olan sayıyı harf ve özel karakterlere çeviriyoruz.
                }
                if (checkBox1.CheckState == CheckState.Checked)
                    textBox2.Text = SayiHarfDonusum(karakter);
                else
                    textBox2.Text = karakter;
            }
            else
            {
                MessageBox.Show("Zorluk seçmelisiniz.");
            }


        }

        string SayiHarfDonusum(string sifre)
        {
            return sifre.Replace("a", "@").Replace("S", "5").Replace("G", "6").Replace("g", "9").Replace("l", "1").Replace("Z", "2")
                .Replace("B", "8").Replace("O", "0").Replace("T", "7");
        }

        bool SayiMi(string text)
        {
            foreach (char chr in text)
            {
                if (!Char.IsNumber(chr)) return false;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            string cumle = textBox1.Text;
            string[] parcalar = cumle.Split(' ');
            string sifre = "", karakter = "";
            int random_Secim = 0;
            Random rastgele = new Random();
            zorluk = new int[] { 33, 47 };// ASCII kod tablosundaki özel karakter kodları #1

            for (int i = 0; i < parcalar.Count(); i++)
            {
                random_Secim = rastgele.Next(zorluk[0], zorluk[1]);
                karakter = Convert.ToChar(random_Secim).ToString();

                if (SayiMi(parcalar[i]))
                {
                    if (parcalar[i].Length > 3)
                        sifre += parcalar[i].Substring(0, 2) + karakter + parcalar[i].Substring(2, 2);
                }
                else
                {
                    if (rastgele.Next(0, 2) == 0)
                    {
                        sifre += parcalar[i].Substring(0, 1).ToLower();
                        if (rastgele.Next(0, 2) == 1)
                            sifre += karakter;
                    }
                    else
                    {
                        sifre += parcalar[i].Substring(0, 1).ToUpper();
                        if (rastgele.Next(0, 2) == 0)
                            sifre += karakter;
                    }
                }
            }
            sifre += karakter;

            if (checkBox1.CheckState == CheckState.Checked)
                textBox2.Text = SayiHarfDonusum(sifre);
            else
                textBox2.Text = sifre;
        }
    }
}
