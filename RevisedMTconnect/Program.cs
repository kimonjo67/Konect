using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RevisedMTconnect
{
    class Program
    {
        static void ThreadJob(Object P)
        {
            Printer PF = (Printer)P;
            PF.RunAsThread();
        }
        static void Main(string[] args)
        {
            CollectWallace();
        }

        static void CollectWallace()
        {
            List<Printer> PrinterList = new List<Printer>();
            
            //Added Printers
            Printer Wallace4187 = new Printer
            {
                baseUrl = "http://10.50.204.187:5000",
                SerialNumber = 9426,
                whatPrint = Printer.eWhatPrint.ChangedMultiple
            };

            Printer Wallace4179 = new Printer
            {
                baseUrl = "http://10.50.204.179:5000",
                SerialNumber = 9240,
                whatPrint = Printer.eWhatPrint.ChangedMultiple
            };

            Printer Wallace4186 = new Printer
            {
                baseUrl = "http://10.50.204.186:5000/",
                SerialNumber = 9425,
                whatPrint = Printer.eWhatPrint.ChangedMultiple
            };

            PrinterList.Add(Wallace4187);
            PrinterList.Add(Wallace4179);
            PrinterList.Add(Wallace4186);


            bool Onethread = false;

            if (Onethread == true) //Single thread solution
            {
                int i = 0;
                while (true) //infinite loop
                {
                    i = i + 1; //counter keep track if working
                    System.Threading.Thread.Sleep(1000); //wait 1 second
                    Console.WriteLine("Sleep=" + i);
                    foreach (Printer P in PrinterList)
                    {
                        Console.WriteLine("Printer Serial:" + P.SerialNumber + " IP=" + P.baseUrl);
                        P.PrintCurrentWithFunctions();
                    }

                }
            }
            else //Thread for each printer
            {
                foreach (Printer P in PrinterList)
                {
                    System.Threading.Thread newThread = new System.Threading.Thread(ThreadJob);
                    newThread.Start(P);
                    System.Threading.Thread.Sleep(100);
                }

            }

        }
    }
}
