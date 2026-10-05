using System;
using System.Collections.Generic;

namespace Problem4
{
    struct Student
    {
        public string StudentNumber;
        public string Name;
        public string Program;
        public int YearLevel;
    }

    struct Operation
    {
        public string Action;
        public string StudentNumber;
        public string StudentName;
    }

    class Program
    {
        const int MaxStudents = 10;
        static Student[] students = new Student[MaxStudents];
        static int studentCount = 0;

        static Stack<Operation> operationHistory = new Stack<Operation>();

        static void Main()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("========================================");
                Console.WriteLine("            OPERATION HISTORY");
                Console.WriteLine("========================================");
                Console.WriteLine();
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Display All Students");
                Console.WriteLine("3. Update Student");
                Console.WriteLine("4. Delete Student");
                Console.WriteLine("5. View Operation History");
                Console.WriteLine("6. View Last Operation");
                Console.WriteLine("7. Remove Last Operation");
                Console.WriteLine("8. Exit");
                Console.WriteLine();
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": AddStudent(); break;
                    case "2": DisplayAll(); break;
                    case "3": UpdateStudent(); break;
                    case "4": DeleteStudent(); break;
                    case "5": ViewHistory(); break;
                    case "6": ViewLast(); break;
                    case "7": RemoveLast(); break;
                    case "8":
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

        // ---------- Stack helpers ----------

        static void RecordOperation(string action, Student s)
        {
            Operation op = new Operation();
            op.Action = action;
            op.StudentNumber = s.StudentNumber;
            op.StudentName = s.Name;
            operationHistory.Push(op);
        }

        static void ViewHistory()
        {
            Console.WriteLine("OPERATION HISTORY");
            Console.WriteLine();

            if (operationHistory.Count == 0)
            {
                Console.WriteLine("No operations recorded.");
                return;
            }

            // Stack enumerates newest -> oldest, so reverse to show oldest first
            Operation[] ops = operationHistory.ToArray();
            Array.Reverse(ops);
            for (int i = 0; i < ops.Length; i++)
                Console.WriteLine((i + 1) + ". " + ops[i].Action + " " + ops[i].StudentName);
        }

        static void ViewLast()
        {
            if (operationHistory.Count == 0)
            {
                Console.WriteLine("No operations recorded.");
                return;
            }

            Operation op = operationHistory.Peek();   // does NOT remove
            Console.WriteLine("Last Operation: " + op.Action + " " + op.StudentName);
        }

        static void RemoveLast()
        {
            if (operationHistory.Count == 0)
            {
                Console.WriteLine("No operations recorded.");
                return;
            }

            operationHistory.Pop();   // removes the most recent (LIFO)
            Console.WriteLine("Last operation removed successfully!");
        }

        // ---------- Student operations (from Problem 1) ----------

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

            RecordOperation("Added", s);
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
                Console.WriteLine("Student Number: " + students[i].StudentNumber);
                Console.WriteLine("Name: " + students[i].Name);
                Console.WriteLine("Program: " + students[i].Program);
                Console.WriteLine("Year Level: " + students[i].YearLevel);
                Console.WriteLine();
            }
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

            RecordOperation("Updated", students[index]);
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

            Student removed = students[index];

            for (int i = index; i < studentCount - 1; i++)
                students[i] = students[i + 1];
            students[studentCount - 1] = new Student();
            studentCount--;

            RecordOperation("Deleted", removed);
            Console.WriteLine();
            Console.WriteLine("Student deleted successfully!");
        }
    }
}
