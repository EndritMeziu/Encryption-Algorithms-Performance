using Elskom.Generic.Libs;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
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
                cmbKeySize.Items.Add("64");   
            }
            else if (cmbAlgorithm.GetItemText(cmbAlgorithm.SelectedItem) == "3DES")
            {
                cmbKeySize.Items.Clear();
                cmbKeySize.Items.Add("128");
                cmbKeySize.Items.Add("192");
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
                encryptionTime.Clear();
                decryptionTime.Clear();
                AES.rijndael.KeySize = Int32.Parse(cmbKeySize.GetItemText(cmbKeySize.SelectedItem));


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
                        Title = "EncryptTime: "+encTime+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encTime) },
                        
                    },
                    new PieSeries
                    {
                        Title = "DecryptTime: "+decTime+"(μs)",
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
            else if(cmbAlgorithm.GetItemText(cmbAlgorithm.SelectedItem) == "DES")
            {
               
                double encTime, decTime;
                encryptionSum = 0;
                decryptionSum = 0;
                encryptionTime.Clear();
                decryptionTime.Clear();
                DES.objDes.KeySize = 64;


                long timestart = DES.nanoTime();
                for (int i = 0; i < 40; i++)
                {
                    DES.objDes.GenerateKey();
                }
                long timedone = DES.nanoTime();

                PerformanceCounter myAppCpu =
                new PerformanceCounter(
                    "Process", "% Processor Time", "EncryptionAlgorithmsPerformance", true);
                for (int i = 0; i < 40; i++)
                {
                    DES.encrypt();
                    DES.decrypt();
                    encryptionTime.Add(DES.encryptionTime);
                    decryptionTime.Add(DES.decryptionTime);
                }
                pct = myAppCpu.NextValue();
                for (int i = 0; i < 40; i++)
                {
                    encryptionSum += encryptionTime.ElementAt(i);
                    decryptionSum += decryptionTime.ElementAt(i);
                }

                encTime = encryptionSum / 40;
                decTime = decryptionSum / 40;


                keyGeneration = (timedone - timestart) / 100;
                keyGeneration /= 100;
                keyGeneration /= 4;
                pieChart1.Series = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "EncryptTime: "+encTime+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encTime) },

                    },
                    new PieSeries
                    {
                        Title = "DecryptTime: "+decTime+"(μs)",
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
            else if (cmbAlgorithm.GetItemText(cmbAlgorithm.SelectedItem) == "3DES")
            {
                double encTime, decTime;
                encryptionSum = 0;
                decryptionSum = 0;
                encryptionTime.Clear();
                decryptionTime.Clear();

                TripleDes.tdes.KeySize = Int32.Parse(cmbKeySize.GetItemText(cmbKeySize.SelectedItem));


                long timestart = TripleDes.nanoTime();
                for (int i = 0; i < 40; i++)
                {
                    TripleDes.tdes.GenerateKey();
                }
                long timedone = TripleDes.nanoTime();

                PerformanceCounter myAppCpu =
                new PerformanceCounter(
                    "Process", "% Processor Time", "EncryptionAlgorithmsPerformance", true);
                for (int i = 0; i < 40; i++)
                {
                    TripleDes.encrypt();
                    TripleDes.decrypt();
                    encryptionTime.Add(TripleDes.encryptionTime);
                    decryptionTime.Add(TripleDes.decryptionTime);
                }
                pct = myAppCpu.NextValue();
                for (int i = 0; i < 40; i++)
                {
                    encryptionSum += encryptionTime.ElementAt(i);
                    decryptionSum += decryptionTime.ElementAt(i);
                }

                encTime = encryptionSum / 40;
                decTime = decryptionSum / 40;


                keyGeneration = (timedone - timestart) / 100;
                keyGeneration /= 100;
                keyGeneration /= 4;
                pieChart1.Series = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "EncryptTime: "+encTime+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encTime) },

                    },
                    new PieSeries
                    {
                        Title = "DecryptTime: "+decTime+"(μs)",
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
            else if (cmbAlgorithm.GetItemText(cmbAlgorithm.SelectedItem) == "Blowfish")
            {
                double encTime, decTime;
                encryptionSum = 0;
                decryptionSum = 0;
                encryptionTime.Clear();
                decryptionTime.Clear();
                BlowFish b;
                if (cmbKeySize.GetItemText(cmbKeySize.SelectedItem) == "32")
                {
                    long startTimekey = nanoTime();
                    byte[] key = new byte[4];
                    for (int i = 0; i < 40; i++) {
                        key[0] = 23;
                        key[1] = 12;
                        key[2] = 26;
                        key[3] = 65;
                       
                    }
                    b = new BlowFish(key);
                    long endTimekey = nanoTime();
                    keyGeneration = (endTimekey - startTimekey)/100;
                    keyGeneration /= 10;
                    keyGeneration /= 4;
                    

                }
                else if(cmbKeySize.GetItemText(cmbKeySize.SelectedItem) == "128")  //256 448
                {
                    byte[] key = new byte[16];
                    long startTimekey = nanoTime();
                    for (int i = 0; i < 40; i++)
                    {
                        key[0] = 12; key[4] = 12; key[8] = 41;  key[12] = 32;
                        key[1] = 8;  key[5] = 62; key[9] = 52;  key[13] = 12;
                        key[2] = 42; key[6] = 22; key[10] = 9;  key[14] = 11;
                        key[3] = 13; key[7] = 18; key[11] = 11; key[15] = 12;
                    }
                    b = new BlowFish(key);
                    long endTimekey = nanoTime();
                    keyGeneration = (endTimekey - startTimekey) / 100;
                    keyGeneration /= 10;
                    keyGeneration /= 4;
                }
                else if (cmbKeySize.GetItemText(cmbKeySize.SelectedItem) == "256")  //256 448
                {
                    byte[] key = new byte[32];
                    long startTimekey = nanoTime();
                    for (int i = 0; i < 40; i++)
                    {
                        key[0] = 12; key[4] = 12; key[8] = 41;  key[12] = 32;
                        key[1] = 8;  key[5] = 62; key[9] = 52;  key[13] = 12;
                        key[2] = 42; key[6] = 22; key[10] = 9;  key[14] = 11;
                        key[3] = 13; key[7] = 18; key[11] = 11; key[15] = 12;

                        key[16] = 12; key[20] = 12; key[24] = 41; key[28] = 32;
                        key[17] = 8;  key[21] = 62; key[25] = 52; key[29] = 12;
                        key[18] = 42; key[22] = 22; key[26] = 9;  key[30] = 11;
                        key[19] = 13; key[23] = 18; key[27] = 11; key[31] = 12;
                    }
                    b = new BlowFish(key);
                    long endTimekey = nanoTime();
                    keyGeneration = (endTimekey - startTimekey) / 100;
                    keyGeneration /= 10;
                    keyGeneration /= 4;
                }
                else   
                {
                    byte[] key = new byte[56];
                    long startTimekey = nanoTime();
                    for (int i = 0; i < 40; i++)
                    {
                        key[0] = 12; key[4] = 12; key[8] = 41; key[12] = 32;
                        key[1] = 8; key[5] = 62; key[9] = 52; key[13] = 12;
                        key[2] = 42; key[6] = 22; key[10] = 9; key[14] = 11;
                        key[3] = 13; key[7] = 18; key[11] = 11; key[15] = 12;

                        key[16] = 12; key[20] = 12; key[24] = 41; key[28] = 32;
                        key[17] = 8; key[21] = 62; key[25] = 52; key[29] = 12;
                        key[18] = 42; key[22] = 22; key[26] = 9; key[30] = 11;
                        key[19] = 13; key[23] = 18; key[27] = 11; key[31] = 12;

                        key[32] = 42; key[34] = 22; key[36] = 9; key[38] = 11;
                        key[33] = 13; key[35] = 18; key[37] = 11; key[39] = 12;

                        key[40] = 12; key[44] = 12; key[48] = 41; key[52] = 32;
                        key[41] = 8; key[45] = 62; key[49] = 52; key[53] = 12;
                        key[42] = 42; key[46] = 22; key[50] = 9; key[54] = 11;
                        key[43] = 13; key[47] = 18; key[51] = 11; key[55] = 12;
                        
                    }
                    b = new BlowFish(key);
                    long endTimekey = nanoTime();
                    keyGeneration = (endTimekey - startTimekey) / 100;
                    keyGeneration /= 10;
                    keyGeneration /= 4;
                }
                long startTimeenc = nanoTime();
                for (int i = 0; i < 40; i++)
                {
                    byte[] inputBytes = File.ReadAllBytes("textfile.txt");
                    byte[] encryptedBytes = b.EncryptECB(inputBytes);
                    FileStream fs = new FileStream("encryptedfile.txt", FileMode.Create, FileAccess.Write);
                    fs.Write(encryptedBytes, 0, encryptedBytes.Length);
                    fs.Close();
                }
                long endTimeenc = nanoTime();
                encTime = (endTimeenc - startTimeenc) / 10;
                encTime /= 100;
                encTime /= 40;


                long startTimedec = nanoTime();
                for (int i = 0; i < 40; i++)
                {
                    byte[] inputBytes = File.ReadAllBytes("encryptedfile.txt");
                    byte[] encryptedBytes = b.Decrypt(inputBytes, System.Security.Cryptography.CipherMode.ECB);
                    FileStream fs = new FileStream("decryptedfile.txt", FileMode.Create, FileAccess.Write);
                    fs.Write(encryptedBytes, 0, encryptedBytes.Length);
                    fs.Close();
                }
                long endTimedec = nanoTime();
                decTime = (endTimedec - startTimedec) / 10;
                decTime /= 100;
                decTime /= 40;

                pieChart1.Series = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "EncryptTime: "+encTime+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encTime) },

                    },
                    new PieSeries
                    {
                        Title = "DecryptTime: "+decTime+"(μs)",
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
            else if (cmbAlgorithm.GetItemText(cmbAlgorithm.SelectedItem) == "RC4")
            {
                double encTime, decTime;
                ;
                encryptionSum = 0;
                decryptionSum = 0;
                encryptionTime.Clear();
                decryptionTime.Clear();
                RC4.size = Int32.Parse(cmbKeySize.GetItemText(cmbKeySize.SelectedItem));


                long timestart = RC4.nanoTime();
                for (int i = 0; i < 40; i++)
                {
                    RC4.GenerateKey(RC4.size);
                }
                long timedone = RC4.nanoTime();

                PerformanceCounter myAppCpu =
                new PerformanceCounter(
                    "Process", "% Processor Time", "EncryptionAlgorithmsPerformance", true);
                for (int i = 0; i < 40; i++)
                {
                    RC4.Encrypt();
                    RC4.Decrypt();
                    encryptionTime.Add(RC4.encryptionTime);
                    decryptionTime.Add(RC4.decryptionTime);
                }
                pct = myAppCpu.NextValue();
                for (int i = 0; i < 40; i++)
                {
                    encryptionSum += encryptionTime.ElementAt(i);
                    decryptionSum += decryptionTime.ElementAt(i);
                }

                encTime = encryptionSum / 40;
                decTime = decryptionSum / 40;


                keyGeneration = (timedone - timestart) / 100;
                keyGeneration /= 100;
                keyGeneration /= 4;
                pieChart1.Series = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "EncryptTime: "+encTime+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encTime) },

                    },
                    new PieSeries
                    {
                        Title = "DecryptTime: "+decTime+"(μs)",
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

        public static long nanoTime()
        {
            long nano = 10000L * Stopwatch.GetTimestamp();
            nano /= TimeSpan.TicksPerMillisecond;
            nano *= 100L;
            return nano;
        }




    }
    }

