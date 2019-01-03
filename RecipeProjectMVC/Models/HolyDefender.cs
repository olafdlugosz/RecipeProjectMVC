using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeProjectMVC.Models
{
    public class HolyDefender : ValidationAttribute
    {
        private string _bannedAnimal = "adrian";

        public override bool IsValid(object value)
        {
            if(value != null)
            {
                var stringValue = value.ToString().ToLower();
                if (stringValue.Contains(_bannedAnimal))
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
