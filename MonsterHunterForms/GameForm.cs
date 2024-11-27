using MonsterHunterDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//revision history
////// Mahan Poor Hamidian     2024/11/27       Created GameForm 
////// Mahan Poor Hamidian     2024/11/27       Loadfromfile / drawing works! 
///   
namespace MonsterHunterForms
{
    public partial class GameForm : Form
    {
        public string PlayerName { get; private set; }
        public string SelectedMapName { get; private set; }
        public Hunter Hunter { get; private set; }

        public static Map map = new Map(); // a map
        //consts
        public const int SQUARE_SIZE = 50;
        static char[][] mapArray = new char[][] { };//map as arrays


        public GameForm(string playerName, string selectedMapName)
        {
            InitializeComponent();
            PlayerName = playerName;
            SelectedMapName = selectedMapName;

           
            Hunter = new Hunter { Name = PlayerName };
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            map.loadMapFromFile(SelectedMapName, Hunter, Monsters.monsters);
            DrawMap(map.mapArray);
        }

        private void DrawMap(char[][] map) // jagged array
        {
            //set the cursor to the top of the screen
            //Console.SetCursorPosition(0, 0);

            //Console.BackgroundColor = ConsoleColor.Yellow;

            //loop in the 1st dimensions of the array
            for (int Y = 0; Y < map.GetLength(0); Y++)
            {
                //loop in the 2nd dimension of the array
                for (int X = 0; X < map[Y].Length; X++)
                {
                    //If we're drawing the player
                    if (map[Y][X] == '#') // wall
                    {
                        PictureBox newWall = new PictureBox();
                        newWall.Name = "picWall-" + X + "-" + Y;
                        newWall.Image = Properties.Resources.wall;
                        newWall.Width = 50; //use constants
                        newWall.Height = 50; //use constants
                        newWall.SizeMode = PictureBoxSizeMode.StretchImage;
                        newWall.Left = X * 50; //use constant
                        newWall.Top = Y * 50; //use constant

                        this.Controls.Add(newWall);


                    }
                    if (map[Y][X] == 'H') // hunter
                    {
                        PictureBox newWall = new PictureBox();
                        newWall.Name = "Hunter-" + X + "-" + Y;
                        newWall.Image = Properties.Resources.Hunter;
                        newWall.Width = 50; //use constants
                        newWall.Height = 50; //use constants
                        newWall.SizeMode = PictureBoxSizeMode.StretchImage;
                        newWall.Left = X * 50; //use constant
                        newWall.Top = Y * 50; //use constant

                        this.Controls.Add(newWall);

                    }
                    if (map[Y][X] == 'h') //shield
                    {
                        PictureBox newWall = new PictureBox();
                        newWall.Name = "Hunter-" + X + "-" + Y;
                        newWall.Image = Properties.Resources.shield;
                        newWall.Width = 50; //use constants
                        newWall.Height = 50; //use constants
                        newWall.SizeMode = PictureBoxSizeMode.StretchImage;
                        newWall.Left = X * 50; //use constant
                        newWall.Top = Y * 50; //use constant

                        this.Controls.Add(newWall);

                    }
                    if (map[Y][X] == 'p') //potion
                    {
                        PictureBox newWall = new PictureBox();
                        newWall.Name = "Hunter-" + X + "-" + Y;
                        newWall.Image = Properties.Resources.potion;
                        newWall.Width = 50; //use constants
                        newWall.Height = 50; //use constants
                        newWall.SizeMode = PictureBoxSizeMode.StretchImage;
                        newWall.Left = X * 50; //use constant
                        newWall.Top = Y * 50; //use constant

                        this.Controls.Add(newWall);

                    }
                    if (map[Y][X] == 'w') //sword
                    {
                        PictureBox newWall = new PictureBox();
                        newWall.Name = "Hunter-" + X + "-" + Y;
                        newWall.Image = Properties.Resources.sword;
                        newWall.Width = 50; //use constants
                        newWall.Height = 50; //use constants
                        newWall.SizeMode = PictureBoxSizeMode.StretchImage;
                        newWall.Left = X * 50; //use constant
                        newWall.Top = Y * 50; //use constant

                        this.Controls.Add(newWall);

                    }
                    if (map[Y][X] == 'G') // Goal
                    {
                        PictureBox newWall = new PictureBox();
                        newWall.Name = "Hunter-" + X + "-" + Y;
                        newWall.Image = Properties.Resources.Goal;
                        newWall.Width = 50; //use constants
                        newWall.Height = 50; //use constants
                        newWall.SizeMode = PictureBoxSizeMode.StretchImage;
                        newWall.Left = X * 50; //use constant
                        newWall.Top = Y * 50; //use constant

                        this.Controls.Add(newWall);

                    }
                    if (map[Y][X] == 'x') // picaxke
                    {
                        PictureBox newWall = new PictureBox();
                        newWall.Name = "Hunter-" + X + "-" + Y;
                        newWall.Image = Properties.Resources.pickaxe;
                        newWall.Width = 50; //use constants
                        newWall.Height = 50; //use constants
                        newWall.SizeMode = PictureBoxSizeMode.StretchImage;
                        newWall.Left = X * 50; //use constant
                        newWall.Top = Y * 50; //use constant

                        this.Controls.Add(newWall);

                    }
                    if (map[Y][X] == 'M') // picaxke
                    {
                        PictureBox newWall = new PictureBox();
                        newWall.Name = "Hunter-" + X + "-" + Y;
                        newWall.Image = Properties.Resources.Monster;
                        newWall.Width = 50; //use constants
                        newWall.Height = 50; //use constants
                        newWall.SizeMode = PictureBoxSizeMode.StretchImage;
                        newWall.Left = X * 50; //use constant
                        newWall.Top = Y * 50; //use constant

                        this.Controls.Add(newWall);

                    }
                    //Draw the char at this pos in the array
                    //Console.Write(map[Y][X]);

                    //Go back to gray
                    //Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.WriteLine();
            }
            //draw player
//            picPlayer.Left = Hunter.X * SQUARE_SIZE;
//            picPlayer.Top = Hunter.Y * SQUARE_SIZE;
            //draw monster
            //picMonster1.Left = monster1X * SQUARE_SIZE;
            // picMonster1.Top = monster1Y * SQUARE_SIZE;

        }
    }
}
