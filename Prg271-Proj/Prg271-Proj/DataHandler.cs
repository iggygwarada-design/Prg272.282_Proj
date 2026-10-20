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
    }
}
