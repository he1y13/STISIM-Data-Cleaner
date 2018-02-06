using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace STISIM_Data_cleaner
{
    static class filterdata
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="variable"></param>
        /// <returns></returns>
        public static List<String> readFile(string filename)
        {
            var reader = new StreamReader(File.OpenRead(filename));
            List<String> Data = new List<String>();
            bool log = false;

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                var values1 = line.Split('\t');
                var values2 = line.Split(' ');
                if (log)
                {
                    Data.Add(line);
                }
                if (values[0].Contains("Block #1:"))
                {
                    log = true;
                }
                else if (values[0] == " Time to collision results (in-lane vehicles)") { log = false; }


            }
            return Data;
        }

        public static int determineSamplingRateAndDownSample(List<String> Data, int desiredRate)
        {
            List<String> downSampled = new List<String>();
            double sRate = 0;
            double counter = 0;
            double cntr = 0;
            bool first = true;
            double lastval = 0;
            foreach (String lst in Data)
            {
                double y = (Convert.ToDouble(lst.Split('\t')[0]));
                if (first)
                {
                    cntr = y;
                    first = false;
                }

                counter++;

                lastval = y;
            }
            sRate = Math.Round(counter / lastval - cntr);

            return (int)sRate;
        }
 


    }
}
