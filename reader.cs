using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Pince_a_croute
{
    internal class reader
    {
        static double[][] numero_anodes =
        {
            new[] { 8, 9, 7.893 },
            new[] { 7, 10, 8.954 },
            new[] { 6, 11, 10.515 },
            new[] { 5, 12, 11.076 },
            new[] { 4, 13, 12.137 },
            new[] { 3, 14, 13.198 },
            new[] { 2, 15, 14.259 },
            new[] { 1, 16, 15.32 }
        };

        public static int read(string FileFullPath)
        {
            string[] csvLines = System.IO.File.ReadAllLines(FileFullPath);
            int size=csvLines.Length;
            // Students
            var detection = new List<detection>();

            // Split each row into column data
            for (int i = 1; i < csvLines.Length; i++)
            {
                detection st = new detection(csvLines[i]);
                detection.Add(st);
            }

            //for (int i = 0; i < detection.Count; i++)
            //{
            //    Console.WriteLine(detection[i]);
            //}

            Console.WriteLine("La taille est de :"+size);
            long count = 0;
            using (StreamReader r = new StreamReader(FileFullPath))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    count++;
                }
            }
            return (int)count;
        
            
        }

        public static void phase1(int size,string FileFullPath)
        {
            string st;
            var x = 0;
            string[] mots = null;
            string[][] Database_Of_Detection; // importer de la BD
            int[][] Coup_De_Pinces; // tableau des coups de pince

            Database_Of_Detection = detection.RectangularArrays.RectangularStringArray(size, 8);

            using (var br = new StreamReader(FileFullPath))
            {
                while (!ReferenceEquals(st = br.ReadLine(), null))
                {
                    mots = st.Split(';');
                    for (int i = 0; i < 7; i++) Database_Of_Detection[x][i] = mots[i];

                    x += 1;
                    Database_Of_Detection[x - 1][7] = x.ToString();
                }
            }

            //for (int i = 0; i < size; i++)
            //{

            //    Console.WriteLine(Database_Of_Detection[i][4]);

         
                
            //    }
                //Console.WriteLine((Database_Of_Detection[75000][5]));
            // return Database_Of_Detection[size][];

            int[][] bd; // ligne trier pour 5 point consecutif et nombre de point consecutif
            int[] distancetrie = new int[(size)]; // tableau des lignes en 7m et 15m

            var m = 0;
            
            string[][] cp; // tableau final

            for (int i = 1; i < (size); i++)
            {
                double extremite1 = double.Parse(Database_Of_Detection[i][4], CultureInfo.InvariantCulture);
                double extremite2 = double.Parse(Database_Of_Detection[i][4], CultureInfo.InvariantCulture);
                if ((extremite1 > 7.393) & (extremite2 < 15.82))
                {
                    distancetrie[m] = i;
                    m += 1;
                }
                
                
            }
        Console.WriteLine("LE NOUVEAUX NOMBRE DE LIGNES EST DE :"+m);
        Console.WriteLine(distancetrie[78]);
            bd = detection.RectangularArrays.RectangularIntArray(m, 8);
            cp = detection.RectangularArrays.RectangularStringArray(bd.Length, 8);
            Console.WriteLine("La longeur est de : " + bd.Length);


            int k = 0; // variable pour placer la ligne dans le tableau bd
            int n = 0; // variable du nombre de point consecutif
            double? tempo = 0.0;

            tempo = double.Parse(Database_Of_Detection[distancetrie[0]][4], CultureInfo.InvariantCulture);


            n = 0;

            for (int i = 1;
                 i < bd.Length;
                 i++)
                if ((double.Parse(Database_Of_Detection[distancetrie[i]][4], CultureInfo.InvariantCulture) - 0.4 < tempo) &
                    (double.Parse(Database_Of_Detection[distancetrie[i]][4], CultureInfo.InvariantCulture) + 0.4 > tempo) &
                    (distancetrie[i] == distancetrie[i - 1] + 1))
                {
                    n += 1;
                    tempo = double.Parse(Database_Of_Detection[distancetrie[i]][4], CultureInfo.InvariantCulture);
                    //Console.WriteLine(distancetrie[i] - 1);

                }
                else
                {
                    if (n > 4)
                    {
                        bd[k][0] = distancetrie[i - 1];
                        bd[k][1] = n;

                        k++;
                        tempo = double.Parse(Database_Of_Detection[distancetrie[i]][4], CultureInfo.InvariantCulture);
                        n = 1;
                    }

                    tempo = double.Parse(Database_Of_Detection[distancetrie[i]][4], CultureInfo.InvariantCulture);
                    n = 1;
                }
            TextWriter wt = new StreamWriter(@"O:Temporaire/Ismael/Databse.txt");
            for (int i = 0; i < bd.Length; i++)
            {
                wt.WriteLine(
                    Database_Of_Detection[i][0]);
            }
            wt.Close();
            TextWriter wt1 = new StreamWriter(@"O:Temporaire/Ismael/DatabaseTriee.txt");
            for (int i = 0; i < bd.Length; i++)
            {
                wt1.WriteLine("DECTIONID:"+Database_Of_Detection[i][0] +"\n"+"DISTANCE :"+
                              Database_Of_Detection[distancetrie[i]][4]+ "\n" + "COLUM NAME:" +Database_Of_Detection[distancetrie[i]][6]+"\n"+ Database_Of_Detection[distancetrie[i]][7]+ Database_Of_Detection[distancetrie[i]][1]+ Database_Of_Detection[distancetrie[i]][2]+ Database_Of_Detection[distancetrie[i]][3]+ Database_Of_Detection[distancetrie[i]][4]);
            }
            wt1.Close();
            //Console.WriteLine(Database_Of_Detection[distancetrie[7]][4]);
            Coup_De_Pinces = detection.RectangularArrays.RectangularIntArray(k, 2);

            /*
             *
             *
             *
             * etape 5
             *
             */

            n = 1; // variable du nombre de point consecutif
            k = 0;

            for (int j = 0; j < Coup_De_Pinces.Length; j++)
            for (int i = 0; i <= 10; i++)
                if ((double.Parse(Database_Of_Detection[bd[j][0] + i][4], CultureInfo.InvariantCulture) > 3.8) &
                    (double.Parse(Database_Of_Detection[bd[j][0] + i][4], CultureInfo.InvariantCulture) < 5.2))
                {
                    n += 1;
                    if (n > 1)
                    {
                        Coup_De_Pinces[k][0] = bd[j][0];
                        Coup_De_Pinces[k][1] = bd[j][1];
                        k += 1;
                        i = 10;
                    }
                }
                else
                {
                    n = 1;
                }

            //DERNIERE PHASE 
            var DestinationFolderReader = @"O:\Temporaire\Olivier Fortin\Fichier lecture csv";
            var datetime = DateTime.Now.ToString("yyyyMMddHHmmss");
            var FileExtension = ".csv"; //Provide the extension you like such as .txt or .csv

            var FileNamePart = "Detection"; //Datetime will be added to it
            var fileName =
                    $"{DestinationFolderReader}\\{FileNamePart + "_" + "TRAITER"}_{datetime}{FileExtension}"; //string encoding = "UTF-8";
            TextWriter writer = new StreamWriter(fileName);
            //  var entete= string.Format("{0};{1};{2};{3};{4};{5};{6}",numero_anodes,scope_time,time)
            writer.Write("numero_anode;emplacement;temps_passer;timestamp;numero_de_ligne\n");

            // writer.Write(writer.NewLine);
            for (int j = 0; j < k; j++)
                //WriteLine(BD[CP[j][0] - 1][4]);
                //WriteLine(" - ");
                //WriteLine(j);
                //WriteLine(k);
                for (int i = 0; i < 8; i++)
                    if ((double.Parse(Database_Of_Detection[Coup_De_Pinces[j][0] - 1][4], CultureInfo.InvariantCulture) >
                         numero_anodes[i][2] - 0.53) &
                        (double.Parse(Database_Of_Detection[Coup_De_Pinces[j][0] - 1][4], CultureInfo.InvariantCulture) <=
                         numero_anodes[i][2] + 0.53))
                    {
                        cp[j][0] = numero_anodes[i][0].ToString(CultureInfo.InvariantCulture); // numero de l<anode

                        //System.out.print(j);
                        //System.out.print(" - ");
                        writer.Write(cp[j][0] + "---" + numero_anodes[i][1]);
                        writer.Write(";");

                        cp[j][1] = Database_Of_Detection[Coup_De_Pinces[j][0] - 1][5];//Emplacement 
                        writer.Write(cp[j][1]);
                        writer.Write(";");

                        cp[j][2] = (Coup_De_Pinces[j][1] * 5).ToString(); // nombre de temps passer a l<anode
                        writer.Write(cp[j][2] + "sec");
                        writer.Write(";");



                        cp[j][3] = Database_Of_Detection[Coup_De_Pinces[j][0]][6];//Timestamp
                        writer.Write(cp[j][3]);
                        writer.Write(";");

                        cp[j][4] = (Database_Of_Detection[j][0]); //numero de la ligne
                        writer.Write(cp[j][4]);
                        writer.WriteLine(";");
                        i = 8;
                    }

            writer.Close();

                Console.WriteLine(k);
            Console.ReadLine();
        }










    }
}

