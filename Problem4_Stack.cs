using System;
using System.Collections.Generic;

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
    static void Main()
    {
        Student[] students = new Student[10];
        int studentCount = 0;
        Stack<Operation> operationHistory = new Stack<Operation>();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n==============================================");
            Console.WriteLine("       STUDENT RECORD MANAGEMENT");
            Console.WriteLine("==============================================");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Display All Students");
            Console.WriteLine("3. Update Student");
            Console.WriteLine("4. Delete Student");
            Console.WriteLine("5. View Operation History");
            Console.WriteLine("6. View Last Operation");
            Console.WriteLine("7. Remove Last Operation");
            Console.WriteLine("8. Exit");
            Console.Write("Enter choice: ");

            int choice;
            int.TryParse(Console.ReadLine(), out choice);

            switch (choice)
            {
                case 1:
                    if (studentCount == 10)
                    {
                        Console.WriteLine("The student list is full.");
                        break;
                    }

                    Student newStudent = new Student();

                    Console.Write("Enter Student Number: ");
                    newStudent.StudentNumber = Console.ReadLine() ?? "";

                    if (FindStudent(students, studentCount, newStudent.StudentNumber) != -1)
                    {
                        Console.WriteLine("That Student Number already exists.");
                        break;
                    }

                    Console.Write("Enter Name: ");
                    newStudent.Name = Console.ReadLine() ?? "";

                    Console.Write("Enter Program: ");
                    newStudent.Program = Console.ReadLine() ?? "";

                    newStudent.YearLevel = GetYearLevel();
                    students[studentCount] = newStudent;
                    studentCount++;

                    Operation addedOperation = new Operation();
                    addedOperation.Action = "Added";
                    addedOperation.StudentNumber = newStudent.StudentNumber;
                    addedOperation.StudentName = newStudent.Name;
                    operationHistory.Push(addedOperation);

                    Console.WriteLine("Student added successfully!");
                    break;

                case 2:
                    if (studentCount == 0)
                    {
                        Console.WriteLine("There are no student records.");
                    }
                    else
                    {
                        for (int i = 0; i < studentCount; i++)
                        {
                            ShowStudent(students[i]);
                        }
                    }
                    break;

                case 3:
                    Console.Write("Enter Student Number to update: ");
                    string updateNumber = Console.ReadLine() ?? "";
                    int updateIndex = FindStudent(students, studentCount, updateNumber);

                    if (updateIndex == -1)
                    {
                        Console.WriteLine("Student not found.");
                        break;
                    }

                    Console.Write("Enter new Name: ");
                    students[updateIndex].Name = Console.ReadLine() ?? "";

                    Console.Write("Enter new Program: ");
                    students[updateIndex].Program = Console.ReadLine() ?? "";

                    students[updateIndex].YearLevel = GetYearLevel();

                    Operation updatedOperation = new Operation();
                    updatedOperation.Action = "Updated";
                    updatedOperation.StudentNumber = students[updateIndex].StudentNumber;
                    updatedOperation.StudentName = students[updateIndex].Name;
                    operationHistory.Push(updatedOperation);

                    Console.WriteLine("Student updated successfully!");
                    break;

                case 4:
                    Console.Write("Enter Student Number to delete: ");
                    string deleteNumber = Console.ReadLine() ?? "";
                    int deleteIndex = FindStudent(students, studentCount, deleteNumber);

                    if (deleteIndex == -1)
                    {
                        Console.WriteLine("Student not found.");
                        break;
                    }

                    Operation deletedOperation = new Operation();
                    deletedOperation.Action = "Deleted";
                    deletedOperation.StudentNumber = students[deleteIndex].StudentNumber;
                    deletedOperation.StudentName = students[deleteIndex].Name;

                    for (int i = deleteIndex; i < studentCount - 1; i++)
                    {
                        students[i] = students[i + 1];
                    }

                    studentCount--;
                    students[studentCount] = new Student();
                    operationHistory.Push(deletedOperation);

                    Console.WriteLine("Student deleted successfully!");
                    break;

                case 5:
                    if (operationHistory.Count == 0)
                    {
                        Console.WriteLine("There are no recorded operations.");
                    }
                    else
                    {
                        Console.WriteLine("\nOPERATION HISTORY (newest first)");
                        int count = 1;

                        foreach (Operation operation in operationHistory)
                        {
                            Console.WriteLine(count + ". " + operation.Action + " " +
                                operation.StudentName + " (" + operation.StudentNumber + ")");
                            count++;
                        }
                    }
                    break;

                case 6:
                    if (operationHistory.Count == 0)
                    {
                        Console.WriteLine("There are no recorded operations.");
                    }
                    else
                    {
                        Operation lastOperation = operationHistory.Peek();
                        Console.WriteLine("Last Operation: " + lastOperation.Action + " " +
                            lastOperation.StudentName);
                    }
                    break;

                case 7:
                    if (operationHistory.Count == 0)
                    {
                        Console.WriteLine("There are no recorded operations.");
                    }
                    else
                    {
                        operationHistory.Pop();
                        Console.WriteLine("Last operation removed successfully!");
                    }
                    break;

                case 8:
                    running = false;
                    Console.WriteLine("Program exited.");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    static int FindStudent(Student[] students, int count, string number)
    {
        for (int i = 0; i < count; i++)
        {
            if (students[i].StudentNumber == number)
            {
                return i;
            }
        }

        return -1;
    }

    static int GetYearLevel()
    {
        int yearLevel = 0;

        while (yearLevel < 1 || yearLevel > 4)
        {
            Console.Write("Enter Year Level (1-4): ");
            int.TryParse(Console.ReadLine(), out yearLevel);
        }

        return yearLevel;
    }

    static void ShowStudent(Student student)
    {
        Console.WriteLine("\nStudent Number: " + student.StudentNumber);
        Console.WriteLine("Name: " + student.Name);
        Console.WriteLine("Program: " + student.Program);
        Console.WriteLine("Year Level: " + student.YearLevel);
    }
}
