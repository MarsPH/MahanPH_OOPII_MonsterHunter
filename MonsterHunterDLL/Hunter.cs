using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//////revision history
///Mahan Poor Hamidian 2024/11/18 Created Hunter Object
namespace MonsterHunterDLL
{
    public class Hunter : Character
    {
        //variables
        private string _name;
        private int _score;

        //constants
        private const int MAX_NAME_CHAR = 20;
        private const int MAX_SCORE = 100000;

        public string Name //  name property which cannot hold more than 20 characters
        {
            get { return _name; }
            set
            {
                sValidationError = "";
                value = value.Trim();
                if (value == string.Empty)
                {
                    sValidationError = "Name cannot be empty";
                }
                else if (value.Length > MAX_NAME_CHAR)
                {
                    sValidationError = $"Name cannot be more then {MAX_NAME_CHAR} characters";
                }
                else
                {
                    _name = value;
                }

            }
        }

        public int Score //  A property to hold the score of the hunter (maximum 100 000, no negative value
        {
            get { return _score; }
            set
            {
                sValidationError = "";
                if ( value < 0)
                {
                    sValidationError = "Value cannot be less than 0";
                }
                else if (value > MAX_SCORE)
                {
                    sValidationError = $"The score is more then the allowed limit of {MAX_SCORE}";
                }
                else
                {
                    _score = value;
                }
            }
        }
    }
}
