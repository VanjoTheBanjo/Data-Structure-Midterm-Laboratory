using System;

namespace Problem1
{
    struct Student
    {
        public string StudentNumber;
        public string Name;
        public string Program;
        public int YearLevel;
    }

    class Program
    {
        const int MaxStudents = 10;
        static Student[] students = new Student[MaxStudents];
        static int studentCount = 0;

        static void Main()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("========================================");
                Console.WriteLine("        STUDENT RECORD MANAGEMENT");
                Console.WriteLine("========================================");
                Console.WriteLine();
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Display All Students");
                Console.WriteLine("3. Search Student");
                Console.WriteLine("4. Update Student");
                Console.WriteLine("5. Delete Student");
                Console.WriteLine("6. Exit");
                Console.WriteLine();
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": AddStudent(); break;
                    case "2": DisplayAll(); break;
                    case "3": SearchStudent(); break;
                    case "4": UpdateStudent(); break;
                    case "5": DeleteStudent(); break;
                    case "6":
                        Console.WriteLine("Program exited.");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                Console.WriteLine();
            }
        }

        static int FindIndex(string studentNumber)
        {
            for (int i = 0; i < studentCount; i++)
            {
                if (students[i].StudentNumber == studentNumber)
                    return i;
            }
            return -1;
        }

        static int ReadYearLevel()
        {
            while (true)
            {
                Console.Write("Enter Year Level (1-4): ");
                if (int.TryParse(Console.ReadLine(), out int year) && year >= 1 && year <= 4)
                    return year;
                Console.WriteLine("Invalid year level. Enter 1, 2, 3, or 4.");
            }
        }

        static void PrintStudent(Student s)
        {
            Console.WriteLine("Student Number: " + s.StudentNumber);
            Console.WriteLine("Name: " + s.Name);
            Console.WriteLine("Program: " + s.Program);
            Console.WriteLine("Year Level: " + s.YearLevel);
        }

        static void AddStudent()
        {
            if (studentCount >= MaxStudents)
            {
                Console.WriteLine("Cannot add more students. Maximum of " + MaxStudents + " reached.");
                return;
            }

            Student s = new Student();
            Console.Write("Enter Student Number: ");
            s.StudentNumber = Console.ReadLine();

            if (FindIndex(s.StudentNumber) != -1)
            {
                Console.WriteLine("Student Number already exists.");
                return;
            }

            Console.Write("Enter Name: ");
            s.Name = Console.ReadLine();
            Console.Write("Enter Program: ");
            s.Program = Console.ReadLine();
            s.YearLevel = ReadYearLevel();

            students[studentCount] = s;
            studentCount++;
            Console.WriteLine();
            Console.WriteLine("Student added successfully!");
        }

        static void DisplayAll()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("            STUDENT RECORDS");
            Console.WriteLine("========================================");

            if (studentCount == 0)
            {
                Console.WriteLine("No student records found.");
                return;
            }

            for (int i = 0; i < studentCount; i++)
            {
                PrintStudent(students[i]);
                Console.WriteLine();
            }
        }

        static void SearchStudent()
        {
            Console.Write("Enter Student Number to search: ");
            int index = FindIndex(Console.ReadLine());
            Console.WriteLine();

            if (index == -1)
            {
                Console.WriteLine("Student not found.");
                return;
            }
            Console.WriteLine("Student Found!");
            PrintStudent(students[index]);
        }

        static void UpdateStudent()
        {
            Console.Write("Enter Student Number to update: ");
            int index = FindIndex(Console.ReadLine());

            if (index == -1)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            Console.Write("Enter New Name: ");
            students[index].Name = Console.ReadLine();
            Console.Write("Enter New Program: ");
            students[index].Program = Console.ReadLine();
            students[index].YearLevel = ReadYearLevel();

            Console.WriteLine();
            Console.WriteLine("Student updated successfully!");
        }

        static void DeleteStudent()
        {
            Console.Write("Enter Student Number to delete: ");
            int index = FindIndex(Console.ReadLine());

            if (index == -1)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            // Shift remaining records to the left
            for (int i = index; i < studentCount - 1; i++)
                students[i] = students[i + 1];

            students[studentCount - 1] = new Student();
            studentCount--;

            Console.WriteLine();
            Console.WriteLine("Student deleted successfully!");
        }
    }
}
