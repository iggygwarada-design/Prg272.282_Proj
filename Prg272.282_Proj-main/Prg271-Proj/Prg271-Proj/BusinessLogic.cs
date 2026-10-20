using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prg271_Proj
{
    internal class BusinessLogic
    {
        public bool ConditionsMet(string id, string name, string species, string age, string score)
        {
            bool met = true;

            // ID must look like WR-1234
            string[] parts = id.Split('-');
            if (parts.Length != 2 || parts[0].ToUpper() != "WR" || parts[1].Length != 4 || !parts[1].All(char.IsDigit))
            {
                met = false;
                MessageBox.Show("Error! Invalid animal ID ENTERED");
            }

            if (name.Length > 30 || name.Length < 1 || name.Contains("|")||string.IsNullOrWhiteSpace(name))
            {
                met = false;
                MessageBox.Show("Error! Invalid NAME ENTERED");
            }

            if (species.Length > 30 || species.Length < 1 || species.Contains("|")||string.IsNullOrWhiteSpace(species))
            {
                met = false;
                MessageBox.Show("Error! Invalid SPECIES ENTERED");
            }

            if (!int.TryParse(age, out int parsedAge) || parsedAge < 0 || parsedAge > 100||string.IsNullOrWhiteSpace(age))
            {
                met = false;
                MessageBox.Show("Error! Invalid Age ENTERED");
            }

            if (!int.TryParse(score, out int parsedScore) || parsedScore < 0 || parsedScore > 100||string.IsNullOrWhiteSpace(score))
            {
                met = false;
                MessageBox.Show("Error! Invalid Recovery Score ENTERED");
            }

            return met;
        }

        public string BuildSummary(List<Animal> animals)
        {
            int total = animals.Count;

            int ageSum = 0;
            int ageCount = 0;
            int scoreSum = 0;
            int scoreCount = 0;

            foreach (Animal animal in animals)
            {
                if (int.TryParse(animal.Age, out int parsedAge))
                {
                    ageSum += parsedAge;
                    ageCount++;
                }
                if (int.TryParse(animal.RecoveryScore, out int parsedScore))
                {
                    scoreSum += parsedScore;
                    scoreCount++;
                }
            }

            double avgAge = ageCount > 0 ? (double)ageSum / ageCount : 0;
            double avgScore = scoreCount > 0 ? (double)scoreSum / scoreCount : 0;

            int critical = animals.Count(a => a.Status == "Critical");
            int serious = animals.Count(a => a.Status == "Serious");
            int stable = animals.Count(a => a.Status == "Stable");
            int recovering = animals.Count(a => a.Status == "Recovering");
            int ready = animals.Count(a => a.Status == "Release-Ready");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Total animals: " + total);
            sb.AppendLine("Average age: " + avgAge.ToString("F1"));
            sb.AppendLine("Average recovery score: " + avgScore.ToString("F1"));
            sb.AppendLine();
            sb.AppendLine("Status counts:");
            sb.AppendLine("  Critical: " + critical);
            sb.AppendLine("  Serious: " + serious);
            sb.AppendLine("  Stable: " + stable);
            sb.AppendLine("  Recovering: " + recovering);
            sb.AppendLine("  Release-Ready: " + ready);

            return sb.ToString();
        }
    }
}