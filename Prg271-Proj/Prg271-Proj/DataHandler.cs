using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prg271_Proj
{
    public class DataHandler
    {
        public List<Animal> animals = new List<Animal>();
 public void getAllData()
 {
     
     using (StreamReader read = new StreamReader("animal.txt"))
     {

         string line;
         while ((line = read.ReadLine()) != null)
         {
             string[] parts = line.Split('|');
             Animal curretAnimal = new Animal(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6]);
             animals.Add(curretAnimal);
         }
     }
     
 }

         public void editList(string id, string Name, string species, string age, string score, string status, string unit)
 {
     Animal find = animals.FirstOrDefault(a => a.AnimalID == id);
     //no need to check for null we took directly from table
     find.AnimalID = id;
     find.Age = age;
     find.Species = species;
     find.RecoveryScore = score;
     find.HousingUnit = unit;

 }

 public void updateFile()
 {
     using (StreamWriter writer = new StreamWriter("animal.txt"))
     {
         
         foreach (Animal item in animals)
         {
             string line = item.AnimalID + "|" + item.Name + "|" + item.Species + "|" + item.Age + "|" + item.RecoveryScore + "|" + item.Status + "|" + item.HousingUnit;
             writer.WriteLine(line);
         }

     }
 }
        
    }
}
