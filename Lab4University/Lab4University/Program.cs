using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4University
{
    class Program
    {
        public static void Validation() {
            using (var context = new UniversityDB()) {
                var student = new Student() { PIB = string.Empty };
                context.Students.Add(student);
                var validationErrors = context.GetValidationErrors().Where(vr => !vr.IsValid).SelectMany(vr => vr.ValidationErrors);

                foreach (var error in validationErrors) {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
        }

        public static void Audit()
        {
            using (var context = new UniversityDB())
            {
                var student = context.Students.Find(1); 
                // Change value directly in the DB 
                using (var contextDB = new UniversityDB()) {
                    contextDB.Database.ExecuteSqlCommand( "UPDATE Student SET PIB = PIB + ' DB' WHERE ID = 1");
                }
                // Change the current value in memory 
                student.PIB = student.PIB + " Memory";
                string value = context.Entry(student).Property(m => m.PIB).OriginalValue;
                Console.WriteLine(string.Format("Original Value : {0}", value));
                value = context.Entry(student).Property(m => m.PIB).CurrentValue;
                Console.WriteLine(string.Format("Current Value : {0}", value));
                value = context.Entry(student).GetDatabaseValues().GetValue<string>("PIB");
                Console.WriteLine(string.Format("DB Value : {0}", value));
            }
        }

        static void Main(string[] args)
        {
            System.Console.WriteLine("Валiдацiя:"); //2 завдання
            Validation();
            System.Console.WriteLine("Аудит"); //3 завдання
            Audit();
            Console.ReadKey();

            //Цей код робить міграцію, якщо в тебе вже є БД на сервері то вона буде викликати помилку бо вже є наявна БД, тому цю стрічку слід виконувати
            //якщо БД не існує
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<UniversityDB, Migrations.Configuration>());
        }
    }
}
