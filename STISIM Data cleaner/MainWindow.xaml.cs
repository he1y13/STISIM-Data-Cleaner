//Copyright <2017> <Dr. Alexander Eriksson>

//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
//to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
//and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
//WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Threading;
namespace STISIM_Data_cleaner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string fullpath = "";
        public void RunFiles(){
            
            string[] filePaths = Directory.GetFiles(@""+fullpath);
            int numberoffiles = filePaths.Length;
            int cnts=0;

            foreach (string filename in filePaths)
            {
                cnts++;
                if (!filename.Contains(".rar")||!filename.Contains("_processed")) { 
                List<string> lst = filterdata.readFile(filename);
                //System.IO.File.WriteAllLines(filename.Split('.')[0]+"_processed.txt", lst);
                int length = lst.Count;
                int cnt =0;
                using (TextWriter tw = new StreamWriter(filename.Split('.')[0] + "_processed.txt"))
                {
                    
                    foreach (String s in lst) {
                        cnt++;
                        if (cnt == 1) { } else { tw.WriteLine(s); }
                        
                        if (cnt == length - 3)
                        {
                            break;
                        }
                    }
                
                }
                
                }
                
            }
            currfile.Content = "Completed";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            currfile.Content = "WORK in Progress";//filename.Split('\\')[filename.Split('\\').Length-1];
            btn.Background = Brushes.Red;
            this.Cursor = Cursors.Wait;

            Thread.Sleep(5);
            RunFiles();
            currfile.Content = "FINISHED";//filename.Split('\\')[filename.Split('\\').Length-1];

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.InitialDirectory = "C:\\STISIM3\\DataCollection";
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames){
                    Console.WriteLine((filename));
                    string[] tmp = filename.Split('\\');
                    Array.Resize(ref tmp, tmp.Length - 1);
                    string pathz = String.Join("\\", tmp);
                    Console.WriteLine(pathz + "\\");
                    pathwin.Text= pathz;
                    fullpath = pathz;
                }

            }
        }
    }
}
