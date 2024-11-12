using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MonsterHunterDLL
{
    //revision history
    ////// Mahan Poor Hamidian     2024/11/12       Created Sword Object
    public class Sword
    {
        //constants
        const int MIN_STRENGTH = 4;
        const int MAX_STRENGTH = 9;
        const int BREAK_CHANCE = 5;

        //variables
        int health = 0;

        /*
        // validation
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
        */
     

        public bool Attack()//byte damage) //the sword will inflict damage
        {
            bool isBroken = RandomSingleton.Next(1, BREAK_CHANCE + 1) == 1;
            return isBroken;
        }



    }
}
