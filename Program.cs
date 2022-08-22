using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;


namespace Pince_a_croute
{
    public class Program
    {
        static void Main(string[] args)


        {
            //Declare Variables and provide values
            var FileNamePart = "Detection"; //Datetime will be added to it
            var DestinationFolderWriter = @"O:\Temporaire\Olivier Fortin\Fichier ecriture csv";
            var DestinationFolderReader = @"O:\Temporaire\Olivier Fortin\Fichier lecture csv";

            var FileDelimiter = ";"; //You can provide comma or pipe or whatever you like
            var FileExtension = ".csv"; //Provide the extension you like such as .txt or .csv

            var datetime = DateTime.Now.ToString("yyyyMMddHHmm");

            //var FileFullPath = $"{DestinationFolderWriter}\\{FileNamePart + "_" + "BRUTE"}_{datetime}{FileExtension}";
            var FileFullPath = @"A:\ABI1\PC\detection.csv";

            Console.WriteLine("Connexion a la BD en cours ...");
            database.connection(datetime, $"{DestinationFolderWriter}\\{FileNamePart + "_" + "BRUTE"}_{datetime}{FileExtension}");
            Console.WriteLine("Lecture du fichier en cours ...");
            //reader.read(FileFullPath);
            reader.phase1(size:reader.read(FileFullPath),FileFullPath);
            //reader.phase2();
            Console.ReadLine();

        }
    }
}
