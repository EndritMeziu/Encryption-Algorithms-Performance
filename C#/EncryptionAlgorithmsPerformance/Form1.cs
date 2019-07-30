using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptionAlgorithmsPerformance
{
    public partial class Form1 : Form
    {
        double keyGeneration;
        List<double> encryptionTime = new List<double>();
        List<double> decryptionTime = new List<double>();
        double encryptionSum;
        double decryptionSum;
        double pct;

        public Form1()
        {
            InitializeComponent();

            panel3.Paint += dropShadow;

            cmbFile.Items.Add("1Kb");
            cmbFile.Items.Add("10Kb");
            cmbFile.Items.Add("100Kb");
            cmbFile.Items.Add("1000Kb");

            cmbAlgorithm.Items.Add("AES");
            cmbAlgorithm.Items.Add("DES");
            cmbAlgorithm.Items.Add("3DES");
            cmbAlgorithm.Items.Add("Blowfish");
            cmbAlgorithm.Items.Add("RC4");


        }
        Func<ChartPoint, string> labelPoint = chartpoint => string.Format("{0} ({1:P}", chartpoint.Y, chartpoint.Participation);
        
        private void dropShadow(object sender, PaintEventArgs e)
        {
            Panel panel = (Panel)sender;
            Color[] shadow = new Color[3];
            shadow[0] = Color.FromArgb(181, 181, 181);
            shadow[1] = Color.FromArgb(195, 195, 195);
            shadow[2] = Color.FromArgb(211, 211, 211);
            Pen pen = new Pen(shadow[0]);
            
            using (pen)
            {
                foreach (Panel p in panel.Controls.OfType<Panel>())
                {
                    Point pt = p.Location;
                    pt.Y += p.Height;
                    for (var sp = 0; sp < 3; sp++)
                    {
                        pen.Color = shadow[sp];
                        e.Graphics.DrawLine(pen, pt.X, pt.Y, pt.X + p.Width - 1, pt.Y);
                        e.Graphics.DrawLine(pen, p.Right + sp, p.Top + sp, p.Right + sp, p.Bottom + sp);
                        pt.Y++;
                    }
                }
            }
        }

        private void pnlFile_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(100, 0, 0, 0));

            e.Graphics.DrawLine(pen, 30, 100, 150, 100);
        }

        private void pnlAlgorithm_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(100, 0, 0, 0));

            e.Graphics.DrawLine(pen, 30, 100, 150, 100);
        }

        private void pnlKeySize_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(100, 0, 0, 0));

            e.Graphics.DrawLine(pen, 30, 100, 150, 100);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            

            
        }

        private void elementHost1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void cmbAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbAlgorithm.GetItemText(cmbAlgorithm.SelectedItem) == "AES")
            {
                cmbKeySize.Items.Clear();
                cmbKeySize.Items.Add("128");
                cmbKeySize.Items.Add("192");
                cmbKeySize.Items.Add("256");
            }
            else if (cmbAlgorithm.GetItemText(cmbAlgorithm.SelectedItem) == "DES")
            {
                cmbKeySize.Items.Clear();
                cmbKeySize.Items.Add("56");   
            }
            else if (cmbAlgorithm.GetItemText(cmbAlgorithm.SelectedItem) == "3DES")
            {
                cmbKeySize.Items.Clear();
                cmbKeySize.Items.Add("112");
                cmbKeySize.Items.Add("168");
            }
            else if (cmbAlgorithm.GetItemText(cmbAlgorithm.SelectedItem) == "Blowfish")
            {
                cmbKeySize.Items.Clear();
                cmbKeySize.Items.Add("32");
                cmbKeySize.Items.Add("128");
                cmbKeySize.Items.Add("256");
                cmbKeySize.Items.Add("448");
            }
            else if (cmbAlgorithm.GetItemText(cmbAlgorithm.SelectedItem) == "RC4")
            {
                cmbKeySize.Items.Clear();
                cmbKeySize.Items.Add("128");
                cmbKeySize.Items.Add("256");
                cmbKeySize.Items.Add("1024");
            }

        }

        private void cmbFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbFile.GetItemText(cmbFile.SelectedItem) == "1Kb")
            {
                FileStream fs = new FileStream("textfile.txt", FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                Random random = new Random();
                for (int i = 0; i < 1024; i++)
                {
                    sw.Write((char)('a'+random.Next(16)));
                    if (i % 8 == 0)
                    {
                        sw.Write(" ");
                        i++;
                    }
                }
                sw.Close();
                
            }
            else if (cmbFile.GetItemText(cmbFile.SelectedItem) == "10Kb")
            {
                FileStream fs = new FileStream("textfile.txt", FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                Random random = new Random();
                for (int i = 0; i < 1024*10; i++)
                {
                    sw.Write((char)('a' + random.Next(16)));
                    if (i % 8 == 0)
                    {
                        sw.Write(" ");
                        i++;
                    }
                }
                sw.Close();
            }
            else if (cmbFile.GetItemText(cmbFile.SelectedItem) == "100Kb")
            {
                FileStream fs = new FileStream("textfile.txt", FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                Random random = new Random();
                for (int i = 0; i < 1024*100; i++)
                {
                    sw.Write((char)('a' + random.Next(16)));
                    if (i % 8 == 0)
                    {
                        sw.Write(" ");
                        i++;
                    }
                }
                sw.Close();
            }
            else if (cmbFile.GetItemText(cmbFile.SelectedItem) == "1000Kb")
            {
                FileStream fs = new FileStream("textfile.txt", FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                Random random = new Random();
                for (int i = 0; i < 1024*1000; i++)
                {
                    sw.Write((char)('a' + random.Next(16)));
                    if (i % 8 == 0)
                    {
                        sw.Write(" ");
                        i++;
                    }
                }
                sw.Close();
            }
        }

        private void cmbKeySize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAlgorithm.GetItemText(cmbAlgorithm.SelectedItem) == "AES")
            {
                double encTime, decTime;
                encryptionSum = 0;
                decryptionSum = 0;
                AES.rijndael.KeySize = 128;


                long timestart = AES.nanoTime();
                for (int i = 0; i < 40; i++) { 
                    AES.rijndael.GenerateKey();
                }
                long timedone = AES.nanoTime();

                PerformanceCounter myAppCpu =
                new PerformanceCounter(
                    "Process", "% Processor Time", "EncryptionAlgorithmsPerformance", true);
                for (int i = 0; i < 40; i++)
                {
                    AES.encrypt();
                    AES.decrypt();
                    encryptionTime.Add(AES.encryptionTime);
                    decryptionTime.Add(AES.decryptionTime);
                }
                pct = myAppCpu.NextValue();
                for (int i=0;i<40;i++)
                {
                    encryptionSum += encryptionTime.ElementAt(i);
                    decryptionSum += decryptionTime.ElementAt(i);
                }

                encTime = encryptionSum / 40;
                decTime = decryptionSum / 40;


                keyGeneration = (timedone - timestart)/100;
                keyGeneration /= 100;
                keyGeneration /= 4;
                pieChart1.Series = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "EncryptTime: "+AES.encryptionTime+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encTime) },
                        
                    },
                    new PieSeries
                    {
                        Title = "DecryptTime: "+AES.decryptionTime+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(decTime) },
                        
                    },
                    new PieSeries
                    {
                        Title = "GenerateKey: "+keyGeneration+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(keyGeneration) },
                        
                    },
                    new PieSeries
                    {
                        Title = "EncryptLoad: "+pct+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(pct) },

                    },
                    new PieSeries
                    {
                        Title = "DecryptLoad: "+pct+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(pct) },

                    }

                };
                pieChart1.LegendLocation = LegendLocation.Bottom;
               
            }
        }

       


    }
    }

