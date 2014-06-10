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
    class Sprite
    {
        public  int i, destX, destY, lastMovePos = 0, newPosX = 10, newPosY = 200, newPosW, newPosH, faceDirectionX, faceDirectionY, jumpHeight = 0;
        bool keyDown = false;
        public String lastDirection = "up", newDirection = "right";
        Graphics gfx;
        public Bitmap sprite;
        xmlParser parser;
        
        public Sprite(String spriteSrc, Graphics g )
        {
            sprite = new Bitmap(spriteSrc); //get the Image
            parser = new xmlParser("megaman-walkleft.xml"); //create an XML parse Obj.
            gfx = g;
            
        }
        //The keyDown gets the state id any key is being pressed
        //Also the direction in which the sprite is going to look is set here
        public void setDirection(String newDirection, int newDirectionX, int newDirectionY, bool keyStatus){
            this.keyDown = keyStatus;
            this.newDirection = newDirection;
            this.faceDirectionX = newDirectionX;
            this.faceDirectionY = newDirectionY;
        }

        //The function to move the Sprite. First it gets the diffrent states of the walk animantion for the specified direction 
        //Then it checks, if the direction changed to the one bevor. If the direction is the same, the lastMovePos will not be resetted, so the next 
        //state can be called.
        //After that it sets the coordinates for the new state and the counter lastMovePos gets set up. 
        public void getAnim()
        {
            List<String> moves = parser.getMoves(newDirection);

            if (this.keyDown)
            {
                
                if (!lastDirection.Equals(newDirection))
                {
                    lastMovePos = 0;
                }
                if (lastMovePos < moves.Count)
                {
                    int[] pos = parser.getCoordinates(moves[lastMovePos], newDirection);

                    this.destX = pos[0];
                    this.destY = pos[1];

                    this.newPosX += (pos[2] * faceDirectionX)/2;
                    this.newPosY += pos[3] * faceDirectionY;
                    Console.WriteLine("W: " + this.newPosX + " H: " + this.newPosY);

                    this.newPosW = pos[2];
                    this.newPosH = pos[3];

                    lastMovePos++;
                    this.lastDirection = newDirection;
                }
                else
                {
                    lastMovePos = 0;
                }
            }
        }
    }
}
