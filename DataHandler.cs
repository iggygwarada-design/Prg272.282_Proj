using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Prg271_Proj
{
    public class DataHandler
    {
        public List<Animal> animals = new List<Animal>();
 public void getAllData()
 {
     
     string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "animals.txt");
     if (!File.Exists(path))
         File.WriteAllText(path, string.Empty, new UTF8Encoding(false));
     animals.Clear();
     int skipped = 0;
     using (StreamReader read = new StreamReader(path))
     {

         string line;
         while ((line = read.ReadLine()) != null)
         {
             string[] parts = line.Split('|');
             if (parts.Length != 7 || !parts[0].StartsWith("WR-", StringComparison.OrdinalIgnoreCase))
             {
                 skipped++;
                 continue;
             }
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


 // Adds a new animal to the in-memory list only.
 public void AddAnimal(Animal animal)
 {
     if (animal == null)
         throw new ArgumentNullException(nameof(animal));

     if (animals.Any(a => string.Equals(a.AnimalID, animal.AnimalID, StringComparison.OrdinalIgnoreCase)))
         throw new InvalidOperationException("An animal with this ID already exists.");

     animals.Add(animal);
 }

 // Use one classification method for both Add and Update features.
 public static void ClassifyScore(int score, out string status, out string unit)
 {
     if (score <= 19) { status = "Critical"; unit = "Intensive Care Unit"; }
     else if (score <= 39) { status = "Serious"; unit = "High-Dependency Ward"; }
     else if (score <= 59) { status = "Stable"; unit = "Recovery Ward"; }
     else if (score <= 79) { status = "Recovering"; unit = "Outdoor Enclosure"; }
     else { status = "Release-Ready"; unit = "Pre-Release Camp"; }
 }

 public void updateFile()
    {
     using (StreamWriter writer = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "animals.txt")))
        {
         
         foreach (Animal item in animals)
            {
             string line = item.AnimalID + "|" + item.Name + "|" + item.Species + "|" + item.Age + "|" + item.RecoveryScore + "|" + item.Status + "|" + item.HousingUnit;
             writer.WriteLine(line);
            }

         }
    }

    public bool DeleteAnimal(string id)
{
    Animal found = animals.FirstOrDefault(a => string.Equals(a.AnimalID, id, StringComparison.OrdinalIgnoreCase));

    if (found == null)
    {
        return false;
    }

    animals.Remove(found);
    return true;
}

    public void SaveSummary(string summaryText)
    {
         string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "summary.txt");

        string content = "ANIMAL SUMMARY REPORT" + Environment.NewLine
                   + "Generated: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + Environment.NewLine
                   + Environment.NewLine
                   + summaryText;

        File.WriteAllText(path, content);
    }
 }
}
