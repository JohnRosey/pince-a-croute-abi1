using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pince_a_croute
{
    public class detection
    {
        public string DetectionId { get; }
        public string reader_uwb_id { get; }
        public string tag_id { get; }
        public string tag_temperature { get; }
        public string distance { get; }
        public string location_name { get; }
        public string insert_timestamp { get; }

        public detection(string rowData)
        {
            string[] data = rowData.Split(';');
            this.DetectionId = data[0];
            this.reader_uwb_id = data[1];
            this.tag_id = data[2];
            this.tag_temperature = data[3];
            this.distance = (data[4]);
            this.location_name = data[5];
            this.insert_timestamp = data[6];


        }

   public override string ToString()
   {
       string str = $" {DetectionId} ; {reader_uwb_id} ;{tag_id} ; {tag_temperature} ; {distance} ; {location_name} ; {insert_timestamp} ";
       return str;
   }
   public static class RectangularArrays
   {

       public static string[][] RectangularStringArray(int size, int nombre)
       {
           string[][] newArray = new string[size][];
           for (int i = 0; i < size; i++)
           {
               newArray[i] = new string[nombre];
           }

           return newArray;
       }

       public static int[][] RectangularIntArray(int size, int nombre)
       {
           int[][] newArray = new int[size][];
           for (int i = 0; i < size; i++)
           {
               newArray[i] = new int[nombre];
           }

           return newArray;
       }

           
        }
    }
   

}
