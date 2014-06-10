using Sprites.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Sprites
{
    class xmlParser
    {
        public DataTable coordinates;
        public xmlParser(String xmlData)
        {
            coordinates = new DataTable();
            parseXML(xmlData);
            
        }

        //The function reads each Parent Node of the XML-file and saves the children as a row inside a Datatable
        void parseXML(String file)
        {  
           this.coordinates.Columns.Add("name", typeof(string));
           this.coordinates.Columns.Add("direction", typeof(string));
           this.coordinates.Columns.Add("x", typeof(string));
           this.coordinates.Columns.Add("y", typeof(string));
           this.coordinates.Columns.Add("w", typeof(string));
           this.coordinates.Columns.Add("h", typeof(string));

            using (XmlReader reader = new XmlTextReader(file))
            {
                while (reader.Read())
                {
                            if (reader.AttributeCount > 1)
                            {
                                this.coordinates.Rows.Add(reader.GetAttribute("name"),reader.GetAttribute("direction"),  reader.GetAttribute("x"), reader.GetAttribute("y"), reader.GetAttribute("w"), reader.GetAttribute("h"));

                            }            
                }
            }
        }
        //this function gets the diffrent Movement states(e.g Stand, left foot up, etc.), that are discripted in the XML file and saves them in a String List
        public List<String> getMoves(String direction)
        {
            List<String> moves = new List<String>();
            foreach (DataRow row in this.coordinates.Rows)
            {
                if (row["direction"].Equals(direction)) //Comparison if the direction ist right
                {
                    moves.Add(row["name"].ToString());
                }
            }
            return moves;
        }

        //This function gets four parameters(x/y position and height/width of the Sprite inside the image) for the diffrent states 
        public int[] getCoordinates(String posName, String direction){
            int[] pos = new int[4];
             foreach (DataRow row in this.coordinates.Rows)
                {
                    if (row["direction"].Equals(direction)) //Comparison if the direction ist right
                    {

                        if (row["name"].Equals(posName))
                        {

                            pos[0] = Convert.ToInt32(row["x"]);
                            pos[1] = Convert.ToInt32(row["y"]);
                            pos[2] = Convert.ToInt32(row["w"]);
                            pos[3] = Convert.ToInt32(row["h"]);
                        }
                    }
                }
             return pos;
        }
      
        
    }
}
