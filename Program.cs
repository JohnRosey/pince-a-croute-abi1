using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Console;


namespace Pince_a_croute
{
    public class Program
    {
        static void Main(string[] args)


        {
            //Declare Variables and provide values
            var FileNamePart = "Detection"; //Datetime will be added to it
            var DestinationFolderWriter = @"A:\Temporaire\Olivier Fortin\Fichier ecriture csv";
            var DestinationFolderReader = @"A:\Temporaire\Olivier Fortin\Fichier lecture csv";

            var FileDelimiter = ";"; //You can provide comma or pipe or whatever you like
            var FileExtension = ".csv"; //Provide the extension you like such as .txt or .csv

            var datetime = DateTime.Now.ToString("yyyyMMddMM");

            //var FileFullPath = $"{DestinationFolderWriter}\\{FileNamePart + "_" + "BRUTE"}_{datetime}{FileExtension}";
            var FileFullPath = @"A:\ABI1\PC\detection.csv";

            Console.WriteLine("Faites un choix entre  1 et 2:");
            Console.WriteLine("* 1 pour les dectections des dernieres 24 heures a partir de 8 H ");
            Console.WriteLine("* 2 pour les dectections du 13 au 14 Juillet ");
            int number = Convert.ToInt32(Console.ReadLine());
            switch (number)
                { 
                    case 1:
                        Console.WriteLine("Connexion a la BD en cours ...");
                        database.connection(datetime, $"{DestinationFolderWriter}\\{FileNamePart + "_" + "BRUTE"}_{datetime}{FileExtension}");
                        Console.WriteLine("Lecture du fichier en cours ...");
                        //
                        //reader.read(FileFullPath);
                        reader.phase1(size: reader.read(FileFullPath), FileFullPath);
                        //reader.phase2();
                        Console.ReadLine();
                        break;
                    case 2:
                    {
                       
                        Console.WriteLine("Connexion a la BD en cours ...");
                        database.connection2(datetime, $"{DestinationFolderWriter}\\{FileNamePart + "_" + "BRUTE"}_{datetime}{FileExtension}");
                        Console.WriteLine("Lecture du fichier en cours ...");
                        //
                        //reader.read(FileFullPath);
                        reader.phase1(size: reader.read(FileFullPath), FileFullPath);
                        //reader.phase2();
                        Console.ReadLine();

                        break;
                    }
                       
            }
                
            }
           
        }
        }
    

