using Elskom.Generic.Libs;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace EncryptionAlgorithmsPerformance
{
    public partial class Form1 : Form,IMessageFilter
    {
        double keyGeneration;
        List<double> encryptionTime = new List<double>();
        List<double> decryptionTime = new List<double>();
        double encryptionSum;
        double decryptionSum;

        ArrayList list_name = new ArrayList();
        double[] encryptionAlgorithmTime = new double[5];
        double[] decryptionAlgorithmTime = new double[5];
        double[] encLoadAlgorithmTime = new double[5];
        double[] decLoadAlgorithmTime = new double[5];
        double[] keyGenerationAlgorithmTime = new double[5];

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        public const int WM_LBUTTONDOWN = 0x0201;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private HashSet<Control> controlsToMove = new HashSet<Control>();



        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_LBUTTONDOWN &&
                 controlsToMove.Contains(Control.FromHandle(m.HWnd)))
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                return true;
            }
            return false;
        }



        public Form1()
        {
            InitializeComponent();
            using (var stream = File.OpenRead("icon.ico"))
            {
                this.Icon = new Icon(stream);
            }
            panel3.Paint += dropShadow;

            //inicializimi i combobox elementeve me vlera fillestare
            cmbFile.Items.Add("1Kb");
            cmbFile.Items.Add("10Kb");
            cmbFile.Items.Add("100Kb");
            cmbFile.Items.Add("1000Kb");

            cmbAlgorithm.Items.Add("AES");
            cmbAlgorithm.Items.Add("DES");
            cmbAlgorithm.Items.Add("3DES");
            cmbAlgorithm.Items.Add("Blowfish");
            cmbAlgorithm.Items.Add("RC4");

            cmbGraphType1.Items.Add("Encryption Time");
            cmbGraphType1.Items.Add("Decryption Time");
            cmbGraphType1.Items.Add("Key Generation");
            cmbGraphType1.Items.Add("Encryption Cpu Load");
            cmbGraphType1.Items.Add("Decryption Cpu Load");

            cmbGraphType1.SelectedItem = cmbGraphType1.Items[0];
            pictureBox1.Image = Properties.Resources.encrypttt;
           

            //vendosja e vlerave fillestare te atributeve 
            //te performances ne 0.0
            for(int i=0;i<5;i++)
            {
                encLoadAlgorithmTime[i] = 0.00;
                decLoadAlgorithmTime[i] = 0.00;
                encryptionAlgorithmTime[i] = 0.00;
                keyGenerationAlgorithmTime[i] = 0.00;
                decryptionAlgorithmTime[i] = 0.00;

            }

            Application.AddMessageFilter(this);

            controlsToMove.Add(this);
            controlsToMove.Add(this.pnlTopBar);



        }
        Func<ChartPoint, string> labelPoint = chartpoint => string.Format("{0} ({1:P}", chartpoint.Y, chartpoint.Participation);


        //Metoda per gjenerimin e dropshadow efektit rreth Panele-ve
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

            e.Graphics.DrawLine(pen, 50, 100, 190, 100);
        }

        private void pnlAlgorithm_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(100, 0, 0, 0));

            e.Graphics.DrawLine(pen, 50, 100, 190, 100);
        }

        private void pnlKeySize_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(100, 0, 0, 0));

            e.Graphics.DrawLine(pen, 50, 100, 190, 100);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            

            
        }

       

        

        //Mbushja e combobox-it cmbKeySize varesisht nga algoritmi i selektuar
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

        //Gjenerimi i vektorit tekstual testues varesisht nga madhesia e specifikuar
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
                lblFileText.Text = "1 Kb File Generated";
                
                
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
                lblFileText.Text = "10 Kb File Generated";
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
                lblFileText.Text = "100 Kb File Generated";
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
                lblFileText.Text = "1000 Kb File Generated";
            }
        }

        //Enkriptimi/Dekriptimi, llogaritje dhe gjenerimi i atributeve te performances
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

                //llogaritja e kohes se se gjenerimit te qelesit
                long timestart = AES.nanoTime();
                for (int i = 0; i < 40; i++) { 
                    AES.rijndael.GenerateKey();
                }
                long timedone = AES.nanoTime();

                //llogaritja e kohes se shfrytezimit te procesorit per enkriptimit
                Process tmpprocess = Process.GetCurrentProcess();
                double pct1 = tmpprocess.TotalProcessorTime.TotalMilliseconds;

                for (int i = 0; i < 40; i++)
                {
                    AES.encrypt();
                    encryptionTime.Add(AES.encryptionTime);
                }

                double pct2 = tmpprocess.TotalProcessorTime.TotalMilliseconds;
                double pctenc = (pct2 - pct1) / 40.00;

                //llogaritja e kohes se shfrytezimit te procesorit per enkriptimit
                pct1 = tmpprocess.TotalProcessorTime.TotalMilliseconds;
                for (int i = 0; i < 40; i++)
                {
                    AES.decrypt();
                    decryptionTime.Add(AES.decryptionTime);
                }
                pct2 = tmpprocess.TotalProcessorTime.TotalMilliseconds;
                double pctdec = (pct2 - pct1) / 40.00;

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

                //Vlerat e fituara te performances dhe gjenerimi i grafikave
                encryptionAlgorithmTime[0] = encTime;
                decryptionAlgorithmTime[0] = decTime;
                keyGenerationAlgorithmTime[0] = keyGeneration;
                encLoadAlgorithmTime[0] = pctenc * 100;
                decLoadAlgorithmTime[0] = pctdec * 100;
                Console.WriteLine();
                pieChart3.Series = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "EncryptTime: "+encryptionAlgorithmTime[0]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encTime) },
                        
                    },
                    new PieSeries
                    {
                        Title = "DecryptTime: "+decryptionAlgorithmTime[0]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(decTime) },
                        
                    },
                    new PieSeries
                    {
                        Title = "GenerateKey: "+keyGenerationAlgorithmTime[0]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(keyGeneration) },
                        
                    },
                    new PieSeries
                    {
                        Title = "EncryptLoad: "+encLoadAlgorithmTime[0]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encLoadAlgorithmTime[0]) },

                    },
                    new PieSeries
                    {
                        Title = "DecryptLoad: "+decLoadAlgorithmTime[0]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(decLoadAlgorithmTime[0]) },

                    }

                };
                pieChart3.LegendLocation = LegendLocation.Bottom;
                
                
                pieChart4.Series = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "AES: "+encryptionAlgorithmTime[0]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encryptionAlgorithmTime[0]) },

                    },
                    new PieSeries
                    {
                        Title = "DES: "+encryptionAlgorithmTime[1]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encryptionAlgorithmTime[1]) },

                    },
                    new PieSeries
                    {
                        Title = "3DES: "+encryptionAlgorithmTime[2]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encryptionAlgorithmTime[2] )},

                    },
                    new PieSeries
                    {
                        Title = "Blowfish: "+encryptionAlgorithmTime[3]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encryptionAlgorithmTime[3]) },

                    },
                    new PieSeries
                    {
                        Title = "RC4: "+encryptionAlgorithmTime[4]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encryptionAlgorithmTime[4]) },

                    }

                };
                pieChart4.LegendLocation = LegendLocation.Bottom;
                pieChart4.InnerRadius = 30;
                
            }
            else if(cmbAlgorithm.GetItemText(cmbAlgorithm.SelectedItem) == "DES")
            {
               
                double encTime, decTime;
                encryptionSum = 0;
                decryptionSum = 0;
                encryptionTime.Clear();
                decryptionTime.Clear();
                DES.objDes.KeySize = 64;

                //llogaritja e kohes se gjenerimit te qelesit
                long timestart = DES.nanoTime();
                for (int i = 0; i < 40; i++)
                {
                    DES.objDes.GenerateKey();
                }
                long timedone = DES.nanoTime();

                //llogaritja e kohes se shfrytezimit te procesorit per enkriptim
                Process tmpprocess = Process.GetCurrentProcess();
                double pct1 = tmpprocess.TotalProcessorTime.TotalMilliseconds;

                for (int i = 0; i < 40; i++)
                {
                    DES.encrypt();
                    encryptionTime.Add(DES.encryptionTime);
                }
                double pct2 = tmpprocess.TotalProcessorTime.TotalMilliseconds;
                double pctenc = (pct2 - pct1) / 40.00;
                //llogaritja e kohes se shfrytezimit te procesorit per dekriptim
                pct1 = tmpprocess.TotalProcessorTime.TotalMilliseconds;
                for (int i = 0; i < 40; i++)
                {  
                    DES.decrypt();
                    decryptionTime.Add(DES.decryptionTime);
                }
                pct2 = tmpprocess.TotalProcessorTime.TotalMilliseconds;
                double pctdec = (pct2 - pct1) / 40.00;
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

                //Vlerat e fituara te performances dhe gjenerimi i grafikave
                encryptionAlgorithmTime[1] = encTime;
                decryptionAlgorithmTime[1] = decTime;
                keyGenerationAlgorithmTime[1] = keyGeneration ;
                encLoadAlgorithmTime[1] = pctenc * 100;
                decLoadAlgorithmTime[1] = pctdec * 100;
                pieChart3.Series = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "EncryptTime: "+encryptionAlgorithmTime[1]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encTime) },

                    },
                    new PieSeries
                    {
                        Title = "DecryptTime: "+decryptionAlgorithmTime[1]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(decTime) },

                    },
                    new PieSeries
                    {
                        Title = "GenerateKey: "+keyGenerationAlgorithmTime[1]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(keyGeneration) },

                    },
                    new PieSeries
                    {
                        Title = "EncryptLoad: "+encLoadAlgorithmTime[1]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encLoadAlgorithmTime[1]) },

                    },
                    new PieSeries
                    {
                        Title = "DecryptLoad: "+decLoadAlgorithmTime[1]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(decLoadAlgorithmTime[1]) },

                    }

                };
                pieChart3.LegendLocation = LegendLocation.Bottom;

                pieChart4.Series = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "AES: "+encryptionAlgorithmTime[0]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encryptionAlgorithmTime[0]) },

                    },
                    new PieSeries
                    {
                        Title = "DES: "+encryptionAlgorithmTime[1]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encryptionAlgorithmTime[1]) },

                    },
                    new PieSeries
                    {
                        Title = "3DES: "+encryptionAlgorithmTime[2]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encryptionAlgorithmTime[2]) },

                    },
                    new PieSeries
                    {
                        Title = "Blowfish: "+encryptionAlgorithmTime[3]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( encryptionAlgorithmTime[3])},

                    },
                    new PieSeries
                    {
                        Title = "RC4: "+encryptionAlgorithmTime[4]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encryptionAlgorithmTime[4]) },

                    }

                };
                pieChart4.LegendLocation = LegendLocation.Bottom;
            }
            else if (cmbAlgorithm.GetItemText(cmbAlgorithm.SelectedItem) == "3DES")
            {
                double encTime, decTime;
                encryptionSum = 0;
                decryptionSum = 0;
                encryptionTime.Clear();
                decryptionTime.Clear();

                TripleDes.tdes.KeySize = Int32.Parse(cmbKeySize.GetItemText(cmbKeySize.SelectedItem));

                //llogaritja e kohes se gjenerimit te qelesit
                long timestart = TripleDes.nanoTime();
                for (int i = 0; i < 40; i++)
                {
                    TripleDes.tdes.GenerateKey();
                }
                long timedone = TripleDes.nanoTime();
                //llogaritja e kohes se shfrytezimit te procesorit per enkriptim
                Process tmpprocess = Process.GetCurrentProcess();
                double pct1 = tmpprocess.TotalProcessorTime.TotalMilliseconds;

                for (int i = 0; i < 40; i++)
                {
                    TripleDes.encrypt();
                    encryptionTime.Add(TripleDes.encryptionTime);
                }

                double pct2 = tmpprocess.TotalProcessorTime.TotalMilliseconds;
                double pctenc = (pct2 - pct1) / 40.00;

                //llogaritja e kohes se shfrytezimit te procesorit per dekriptim
                pct1 = tmpprocess.TotalProcessorTime.TotalMilliseconds;

                for (int i = 0; i < 40; i++)
                {
                    TripleDes.decrypt();
                    decryptionTime.Add(TripleDes.decryptionTime);
                }

                pct2 = tmpprocess.TotalProcessorTime.TotalMilliseconds;
                double pctdec = (pct2 - pct1) / 40.00;

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

                //Vlerat e fituara te performances dhe gjenerimi i grafikave
                encryptionAlgorithmTime[2] = encTime;
                decryptionAlgorithmTime[2] = decTime;
                keyGenerationAlgorithmTime[2] = keyGeneration;
                encLoadAlgorithmTime[2] = pctenc * 100;
                decLoadAlgorithmTime[2] = pctdec * 100;
                pieChart3.Series = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "EncryptTime: "+encryptionAlgorithmTime[2]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encTime) },

                    },
                    new PieSeries
                    {
                        Title = "DecryptTime: "+decryptionAlgorithmTime[2]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(decTime) },

                    },
                    new PieSeries
                    {
                        Title = "GenerateKey: "+keyGenerationAlgorithmTime[2]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(keyGeneration) },

                    },
                    new PieSeries
                    {
                        Title = "EncryptLoad: "+encLoadAlgorithmTime[2]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encLoadAlgorithmTime[2]) },

                    },
                    new PieSeries
                    {
                        Title = "DecryptLoad: "+decLoadAlgorithmTime[2]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(decLoadAlgorithmTime[2]) },

                    }

                };
                pieChart3.LegendLocation = LegendLocation.Bottom;

                pieChart4.Series = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "AES: "+encryptionAlgorithmTime[0]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( encryptionAlgorithmTime[0]) },

                    },
                    new PieSeries
                    {
                        Title = "DES: "+encryptionAlgorithmTime[1]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( encryptionAlgorithmTime[1]) },

                    },
                    new PieSeries
                    {
                        Title = "3DES: "+encryptionAlgorithmTime[2]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( encryptionAlgorithmTime[2])},

                    },
                    new PieSeries
                    {
                        Title = "Blowfish: "+encryptionAlgorithmTime[3]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( encryptionAlgorithmTime[3]) },

                    },
                    new PieSeries
                    {
                        Title = "RC4: "+encryptionAlgorithmTime[4]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( encryptionAlgorithmTime[4]) },

                    }

                };
                pieChart4.LegendLocation = LegendLocation.Bottom;
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
                //llogaritja e kohes se shfrytezimit te procesorit per enkriptim
                Process tmpprocess = Process.GetCurrentProcess();
                double pct1 = tmpprocess.TotalProcessorTime.TotalMilliseconds;
                byte[] inputBytes = File.ReadAllBytes("textfile.txt");
                byte[] encryptedBytes = File.ReadAllBytes("textfile.txt");
                long startTimeenc = nanoTime();
                
                for (int i = 0; i < 40; i++)
                {
                    encryptedBytes = b.EncryptECB(inputBytes);
                }
                FileStream fs = new FileStream("encryptedfile.txt", FileMode.Create, FileAccess.Write);
                fs.Write(encryptedBytes, 0, encryptedBytes.Length);
                fs.Close();
                long endTimeenc = nanoTime();
                //llogaritja e kohes se shfrytezimit te procesorit per dekriptim
                double pct2 = tmpprocess.TotalProcessorTime.TotalMilliseconds;
                double pctenc = (pct2 - pct1) / 40.00;
                
                encTime = (endTimeenc - startTimeenc) / 10;
                encTime /= 100;
                encTime /= 40;

                inputBytes = File.ReadAllBytes("encryptedfile.txt");
                pct1 = tmpprocess.TotalProcessorTime.TotalMilliseconds;
                long startTimedec = nanoTime();
                
                for (int i = 0; i < 40; i++)
                {
                    
                    encryptedBytes = b.Decrypt(inputBytes, System.Security.Cryptography.CipherMode.ECB);
                    
                }
                fs = new FileStream("decryptedfile.txt", FileMode.Create, FileAccess.Write);
                fs.Write(encryptedBytes, 0, encryptedBytes.Length);
                fs.Close();
                long endTimedec = nanoTime();
                pct2 = tmpprocess.TotalProcessorTime.TotalMilliseconds;
                double pctdec = (pct2 - pct1) / 40.00;
                

                decTime = (endTimedec - startTimedec) / 10;
                decTime /= 100;
                decTime /= 40;

                //Vlerat e fituara te performances dhe gjenerimi i grafikave
                encryptionAlgorithmTime[3] = encTime;
                decryptionAlgorithmTime[3] = decTime;
                keyGenerationAlgorithmTime[3] = keyGeneration;
                encLoadAlgorithmTime[3] = pctenc * 100;
                decLoadAlgorithmTime[3] = pctdec * 100;

                pieChart3.Series = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "EncryptTime: "+encryptionAlgorithmTime[3]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encTime) },

                    },
                    new PieSeries
                    {
                        Title = "DecryptTime: "+decryptionAlgorithmTime[3]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(decTime) },

                    },
                    new PieSeries
                    {
                        Title = "GenerateKey: "+keyGenerationAlgorithmTime[3]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(keyGeneration) },

                    },
                    new PieSeries
                    {
                        Title = "EncryptLoad: "+encLoadAlgorithmTime[3]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encLoadAlgorithmTime[3]) },

                    },
                    new PieSeries
                    {
                        Title = "DecryptLoad: "+decLoadAlgorithmTime[3]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(decLoadAlgorithmTime[3]) },

                    }

                };
                pieChart3.LegendLocation = LegendLocation.Bottom;

                pieChart4.Series = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "AES: "+encryptionAlgorithmTime[0]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( encryptionAlgorithmTime[0])},

                    },
                    new PieSeries
                    {
                        Title = "DES: "+encryptionAlgorithmTime[1]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( encryptionAlgorithmTime[1]) },

                    },
                    new PieSeries
                    {
                        Title = "3DES: "+encryptionAlgorithmTime[2]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( encryptionAlgorithmTime[2])},

                    },
                    new PieSeries
                    {
                        Title = "Blowfish: "+encryptionAlgorithmTime[3]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( encryptionAlgorithmTime[3])},

                    },
                    new PieSeries
                    {
                        Title = "RC4: "+encryptionAlgorithmTime[4]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( (encryptionAlgorithmTime[4])) },

                    }

                };
                pieChart4.LegendLocation = LegendLocation.Bottom;

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

                //llogaritja e kohes se gjenerimit te qelesit
                long timestart = RC4.nanoTime();
                for (int i = 0; i < 40; i++)
                {
                    RC4.GenerateKey(RC4.size);
                }
                long timedone = RC4.nanoTime();
                //llogaritja e kohes se shfrytezimit te procesorit per enkriptim
                Process tmpprocess = Process.GetCurrentProcess();
                double pct1 = tmpprocess.TotalProcessorTime.TotalMilliseconds;
                for (int i = 0; i < 40; i++)
                {
                    RC4.Encrypt();
                    encryptionTime.Add(RC4.encryptionTime);
                }
                double pct2 = tmpprocess.TotalProcessorTime.TotalMilliseconds;
                double pctenc = (pct2 - pct1) / 40.00;
                //llogaritja e kohes se shfrytezimit te procesorit per dekriptim
                pct1 = tmpprocess.TotalProcessorTime.TotalMilliseconds;
                for (int i = 0; i < 40; i++)
                {
                    RC4.Decrypt();
                    decryptionTime.Add(RC4.decryptionTime);
                }
                pct2 = tmpprocess.TotalProcessorTime.TotalMilliseconds;
                double pctdec = (pct2 - pct1) / 40.00;
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

                //Vlerat e fituara te performances dhe gjenerimi i grafikave
                encryptionAlgorithmTime[4] = encTime;
                decryptionAlgorithmTime[4] = decTime;
                keyGenerationAlgorithmTime[4] = keyGeneration;
                encLoadAlgorithmTime[4] = pctenc*100;
                decLoadAlgorithmTime[4] = pctdec*100;

                pieChart3.Series = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "EncryptTime: "+encryptionAlgorithmTime[4]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encTime) },

                    },
                    new PieSeries
                    {
                        Title = "DecryptTime: "+decryptionAlgorithmTime[4]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(decTime) },

                    },
                    new PieSeries
                    {
                        Title = "GenerateKey: "+keyGenerationAlgorithmTime[4]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(keyGeneration) },

                    },
                    new PieSeries
                    {
                        Title = "EncryptLoad: "+encLoadAlgorithmTime[4]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encLoadAlgorithmTime[4]) },

                    },
                    new PieSeries
                    {
                        Title = "DecryptLoad: "+decLoadAlgorithmTime[4]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(decLoadAlgorithmTime[4]) },

                    }

                };
                pieChart3.LegendLocation = LegendLocation.Bottom;

                pieChart4.Series = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "AES: "+encryptionAlgorithmTime[0]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( encryptionAlgorithmTime[0]) },

                    },
                    new PieSeries
                    {
                        Title = "DES: "+encryptionAlgorithmTime[1]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( encryptionAlgorithmTime[1])},

                    },
                    new PieSeries
                    {
                        Title = "3DES: "+encryptionAlgorithmTime[2]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( encryptionAlgorithmTime[2])},

                    },
                    new PieSeries
                    {
                        Title = "Blowfish: "+encryptionAlgorithmTime[3]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( encryptionAlgorithmTime[3]) },

                    },
                    new PieSeries
                    {
                        Title = "RC4: "+encryptionAlgorithmTime[4]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encryptionAlgorithmTime[4]) },

                    }

                };
                pieChart4.LegendLocation = LegendLocation.Bottom;



            }
        }

        //metoda per kthimin e kohes ne nanosekonda
        public static long nanoTime()
        {
            long nano = 10000L * Stopwatch.GetTimestamp();
            nano /= TimeSpan.TicksPerMillisecond;
            nano *= 100L;
            return nano;
        }

        private void cmbGraphType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        //mbyllja e programit me klikim ne X label
        private void lblExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Krahasimi i rezultateve mes algoritmeve te pershkruara
        private void cmbGraphType1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbGraphType1.GetItemText(cmbGraphType1.SelectedItem) == "Encryption Time")
            {
                pieChart4.Series = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "AES: "+encryptionAlgorithmTime[0]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( encryptionAlgorithmTime[0]) },

                    },
                    new PieSeries
                    {
                        Title = "DES: "+encryptionAlgorithmTime[1]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( encryptionAlgorithmTime[1])},

                    },
                    new PieSeries
                    {
                        Title = "3DES: "+encryptionAlgorithmTime[2]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( encryptionAlgorithmTime[2]) },

                    },
                    new PieSeries
                    {
                        Title = "Blowfish: "+encryptionAlgorithmTime[3]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( encryptionAlgorithmTime[3]) },

                    },
                    new PieSeries
                    {
                        Title = "RC4: "+encryptionAlgorithmTime[4]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( encryptionAlgorithmTime[4]) },

                    }

                };
                pieChart4.LegendLocation = LegendLocation.Bottom;
            }
            else if (cmbGraphType1.GetItemText(cmbGraphType1.SelectedItem) == "Decryption Time")
            {
                pieChart4.Series = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "AES: "+decryptionAlgorithmTime[0]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( decryptionAlgorithmTime[0]) },

                    },
                    new PieSeries
                    {
                        Title = "DES: "+decryptionAlgorithmTime[1]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( decryptionAlgorithmTime[1]) },

                    },
                    new PieSeries
                    {
                        Title = "3DES: "+decryptionAlgorithmTime[2]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( decryptionAlgorithmTime[2]) },

                    },
                    new PieSeries
                    {
                        Title = "Blowfish: "+decryptionAlgorithmTime[3]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( decryptionAlgorithmTime[3])},

                    },
                    new PieSeries
                    {
                        Title = "RC4: "+decryptionAlgorithmTime[4]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(decryptionAlgorithmTime[4]) },

                    }

                };
                pieChart4.LegendLocation = LegendLocation.Bottom;
            }
            else if (cmbGraphType1.GetItemText(cmbGraphType1.SelectedItem) == "Encryption Cpu Load")
            {
                pieChart4.Series = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "AES: "+encLoadAlgorithmTime[0]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( encLoadAlgorithmTime[0]) },

                    },
                    new PieSeries
                    {
                        Title = "DES: "+encLoadAlgorithmTime[1]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue(encLoadAlgorithmTime[1]) },

                    },
                    new PieSeries
                    {
                        Title = "3DES: "+encLoadAlgorithmTime[2]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( encLoadAlgorithmTime[2]) },

                    },
                    new PieSeries
                    {
                        Title = "Blowfish: "+encLoadAlgorithmTime[3]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( encLoadAlgorithmTime[3]) },

                    },
                    new PieSeries
                    {
                        Title = "RC4: "+encLoadAlgorithmTime[4]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( encLoadAlgorithmTime[4]) },

                    }

                };
                pieChart4.LegendLocation = LegendLocation.Bottom;
            }
            else if (cmbGraphType1.GetItemText(cmbGraphType1.SelectedItem) == "Decryption Cpu Load")
            {
                pieChart4.Series = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "AES: "+decLoadAlgorithmTime[0]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( decLoadAlgorithmTime[0]) },

                    },
                    new PieSeries
                    {
                        Title = "DES: "+decLoadAlgorithmTime[1]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( decLoadAlgorithmTime[1]) },

                    },
                    new PieSeries
                    {
                        Title = "3DES: "+decLoadAlgorithmTime[2]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( decLoadAlgorithmTime[2]) },

                    },
                    new PieSeries
                    {
                        Title = "Blowfish: "+decLoadAlgorithmTime[3]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( decLoadAlgorithmTime[3]) },

                    },
                    new PieSeries
                    {
                        Title = "RC4: "+decLoadAlgorithmTime[4]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( decLoadAlgorithmTime[4]) },

                    }

                };
                pieChart4.LegendLocation = LegendLocation.Bottom;
            }
            else if (cmbGraphType1.GetItemText(cmbGraphType1.SelectedItem) == "Key Generation")
            {
                pieChart4.Series = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "AES: "+keyGenerationAlgorithmTime[0]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( keyGenerationAlgorithmTime[0])},

                    },
                    new PieSeries
                    {
                        Title = "DES: "+keyGenerationAlgorithmTime[1]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( keyGenerationAlgorithmTime[1])},

                    },
                    new PieSeries
                    {
                        Title = "3DES: "+keyGenerationAlgorithmTime[2]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( keyGenerationAlgorithmTime[2] )},

                    },
                    new PieSeries
                    {
                        Title = "Blowfish: "+keyGenerationAlgorithmTime[3]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( keyGenerationAlgorithmTime[3]) },

                    },
                    new PieSeries
                    {
                        Title = "RC4: "+keyGenerationAlgorithmTime[4]+"(μs)",
                        Values = new ChartValues<ObservableValue> { new ObservableValue( keyGenerationAlgorithmTime[4]) },

                    }

                };
                pieChart4.LegendLocation = LegendLocation.Bottom;
            }
        }


        

    }
    }

