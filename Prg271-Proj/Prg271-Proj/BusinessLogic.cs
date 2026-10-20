using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prg271_Proj
{
    internal class BusinessLogic
    {
        public bool ConditionsMet(string id, string Name, string species, string age)
{
    //checks all the requirements
    bool met = true;
    //checks the id
    string[] parts = id.Split('-');
    if (parts[0].ToUpper() != "WR" || parts[1].Length != 4 || !parts[1].All(char.IsDigit))
    {
        met = false;
        MessageBox.Show("Error! Invalid animal ID ENTERED");
    }

    //checks the name
    if (Name.Length > 30 || Name.Length < 1 || Name.Contains("|"))
    {
        met = false;
        MessageBox.Show("Error! Invalid NAME ENTERED");
    }

    //checks the Species
    if (species.Length > 30 || species.Length < 1 || species.Contains("|"))
    {
        met = false;
        MessageBox.Show("Error! Invalid SPECIES ENTERED");
    }

    //Check the age
    if (!int.TryParse(age, out int parsedAge) || int.Parse(age) > 100 || int.Parse(age) < 0)
    {
        met = false;
        MessageBox.Show("Error! Invalid Age ENTERED");
    }
    return met;
}
public void Clculate()
{

}
    }
}
