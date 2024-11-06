using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.IO;

////Revision history:
/// Mahan Poor Hamidian     2024/11/5       Created Map Object until   LoadMapFromFile(Not finished)    

namespace MonsterHunterDLL
{

    public class Map
    {
        //constants
        const int MAX_WIDTH = 75;
        const int MAX_HEIGHT = 30;

        //private variables
        private int iWidth = 0;
        private int iHeight = 0;

        public string sValidationError = "";
        public string ValidationError
        {
            get
            {
                try
                {
                    // it returns the private variable
                    return sValidationError;

                }
                catch (Exception e)
                {
                    //unexpected errors
                    throw new Exception("Error: (ValidationErrorGetter)", e);
                }
            }
        }

        public int width
        {
            get { return iWidth; }

            private set
            {
                try
                {   //clean the previous error
                    sValidationError = "";

                    if (value < 0) //if value less than 0
                    {   //Set the validation error
                        sValidationError = "Width should be more than 0";
                    }
                    else
                    {
                        if (value > MAX_WIDTH) // value bigger than 75
                        {   //set the error
                            sValidationError = $"Width can never be more than {MAX_WIDTH}";
                        }
                        else
                        { //set the private iwidth to valeu
                            iWidth = value;
                        }
                    }
                }
                catch (Exception e)
                { //throw an execption need to be changed to
                    throw new Exception("An error occured in the DLL (ValidationErrorSetter", e);//
                }
            }
        }
        public int height
        {
            get { return iHeight; }


            private set
            {
                try
                {
                    sValidationError = "";//set the validatation error to empty

                    if (value < 0)// if less than 0
                    {
                        sValidationError = "Height should be more than 0";
                    }
                    else
                    {
                        if (value > MAX_HEIGHT)
                        { // if set height is more than MAX_HEIGHT...
                            sValidationError = $"Height can never be more than {MAX_HEIGHT}";
                        }
                        else
                        { //set the value to private variable
                            iHeight = value;
                        }
                    }
                }
                catch (Exception e)
                {   //unkown error...
                    throw new Exception("An error occured in the DLL (ValidationErrorSetter)", e);
                }
 
            }
        }

        public string[] mapNames //private set ,public get
        {
            get
            {
                return mapNames;
            }

            private set
            {
                mapNames = value;
            }
        }

        public Map()
        {
            try
            {
                LoadMapFile(); //it will try to load map from current directory and put it into mapNames[]
            }
            catch (Exception e)
            {
                sValidationError = "Error loading map files: " + e.Message;
            }
        }

       
        private void LoadMapFile()
        {// relative path to get the files of map from current diectorty
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.map");

            if (files.Length == 0)
            {
                sValidationError = "No .map file found in LoadMapFile";
            }
            else
            {
                mapNames = Array.ConvertAll(files, Path.GetFileName);
            }
        }

        public char[][] mapArray; //A two-dimension char array to hold the data contained in the selected map file

        //I finished on loadMapfFile thinking how to implement the method with parameters and the loop
        private void loadMapFromFile(string fileName, Hunter hunter, List<Monster> monsters)
        {
            foreach (string fileLine in System.IO.File.ReadAllLines(fileName))
            {
                //convert the string into a char array
                char[] fileLineArray = fileLine.ToCharArray();

                Array.Resize(ref mapArray, mapArray.Length + 1);

                
                mapArray[mapArray.GetUpperBound(0)] = fileLineArray;

                //loop into fileLineArray to find the player and the monsters
                for (int x = 0; x < fileLineArray.Length; x++)
                {
                    // if the actual char is a player save the x.
                    // Y (mapArray.GetUpperBound(0)) into the player object.
                }
            }
        }
    }
}
