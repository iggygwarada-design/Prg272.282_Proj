using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prg271_Proj
{
    public class Animal
    {
        public string AnimalID { get; set; }
 public string Name { get; set; }
 public string Species { get; set; }
 public string Age { get; set; }
 public string RecoveryScore { get; set; }
 public string Status { get; set; }
 public string HousingUnit { get; set; }
 public Animal(string id, string name, string species, string age, string score, string status, string unit)
 {
     AnimalID = id;
     Name = name;
     Species = species;
     Age = age;
     RecoveryScore = score;
     Status = status;
     HousingUnit = unit;
 }
    }
}
