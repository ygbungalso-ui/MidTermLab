using System;

public struct Student
{
    public string StudentNumber;
    public string Name;
    public string Program;
    public int YearLevel;
}

public static class Program
{
    private const int Capacity = 10;

    public static void Main()
    {
        Student[] students = new Student[Capacity];
        var studentCount = 0;
        var running = true;

        while (running)
        {
            Console.WriteLine("\n=========================================");
            Console.WriteLine("          STUDENT RECORD MANAGEMENT"        );
            Console.WriteLine("===========================================");
            Console.WriteLine("1. Add Student                             ");
            Console.WriteLine("2. Display All Students                    ");
            Console.WriteLine("3. Search Student                          ");
            Console.WriteLine("4. Update Student                          ");
            Console.WriteLine("5. Delete Student                          ");
            Console.WriteLine("6. Exit                                    ");

            switch (ReadChoice())
            {
                case 1:
                    AddStudent(students, ref studentCount);
                    break;
                case 2:
                    DisplayStudents(students, studentCount);
                    break;
                case 3:
                    SearchStudent(students, studentCount);
                    break;
                case 4:
                    UpdateStudent(students, studentCount);
                    break;
                case 5:
                    DeleteStudent(students, ref studentCount);
                    break;
                case 6:
                    running = false;
                    Console.WriteLine("Program exited.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Enter a number from 1 to 6.");
                    break;
            }
        }
    }

    private static void AddStudent(Student[] students, ref int studentCount)
    {
        if (studentCount == Capacity)
        {
            Console.WriteLine("The student record list is full. You can store up to 10 students.");
            return;
        }

        var studentNumber = ReadRequired("Enter Student Number: ");
        if (FindStudent(students, studentCount, studentNumber) >= 0)
        {
            Console.WriteLine("A student with that Student Number already exists.");
            return;
        }

        students[studentCount] = new Student
        {
            StudentNumber = studentNumber,
            Name = ReadRequired("Enter Name: "),
            Program = ReadRequired("Enter Program: "),
            YearLevel = ReadYearLevel()
        };
        studentCount++;
        Console.WriteLine("Student added successfully!");
    }

    private static void DisplayStudents(Student[] students, int studentCount)
    {
        Console.WriteLine("\n=========================================");
        Console.WriteLine("              STUDENT RECORDS              ");
        Console.WriteLine("===========================================");
        if (studentCount == 0)
        {
            Console.WriteLine("No student records to display.");
            return;
        }

        for (var i = 0; i < studentCount; i++)
        {
            PrintStudent(students[i]);
        }
    }

    private static void SearchStudent(Student[] students, int studentCount)
    {
        var studentNumber = ReadRequired("Enter Student Number to search: ");
        var index = FindStudent(students, studentCount, studentNumber);
        if (index < 0)
        {
            Console.WriteLine("Student not found.");
            return;
        }

        Console.WriteLine("\nStudent Found!");
        PrintStudent(students[index]);
    }

    private static void UpdateStudent(Student[] students, int studentCount)
    {
        var studentNumber = ReadRequired("Enter Student Number to update: ");
        var index = FindStudent(students, studentCount, studentNumber);
        if (index < 0)
        {
            Console.WriteLine("Student not found.");
            return;
        }

        var student = students[index];
        student.Name = ReadRequired("Enter new Name: ");
        student.Program = ReadRequired("Enter new Program: ");
        student.YearLevel = ReadYearLevel();
        students[index] = student;
        Console.WriteLine("Student updated successfully!");
    }

    private static void DeleteStudent(Student[] students, ref int studentCount)
    {
        var studentNumber = ReadRequired("Enter Student Number to delete: ");
        var index = FindStudent(students, studentCount, studentNumber);
        if (index < 0)
        {
            Console.WriteLine("Student not found.");
            return;
        }

        for (var i = index; i < studentCount - 1; i++)
        {
            students[i] = students[i + 1];
        }

        studentCount--;
        students[studentCount] = default;
        Console.WriteLine("Student deleted successfully!");
    }

    private static int FindStudent(Student[] students, int studentCount, string studentNumber)
    {
        for (var i = 0; i < studentCount; i++)
        {
            if (string.Equals(students[i].StudentNumber, studentNumber, StringComparison.OrdinalIgnoreCase))
            {
                return i;
            }
        }

        return -1;
    }

    private static void PrintStudent(Student student)
    {
        Console.WriteLine($"Student Number: {student.StudentNumber}");
        Console.WriteLine($"Name: {student.Name}");
        Console.WriteLine($"Program: {student.Program}");
        Console.WriteLine($"Year Level: {student.YearLevel}");
        Console.WriteLine();
    }

    private static int ReadChoice()
    {
        Console.Write("\nEnter choice: ");
        return int.TryParse(Console.ReadLine(), out var choice) ? choice : -1;
    }

    private static string ReadRequired(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var value = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            Console.WriteLine("This field cannot be empty.");
        }
    }

    private static int ReadYearLevel()
    {
        while (true)
        {
            Console.Write("Enter Year Level (1-4): ");
            if (int.TryParse(Console.ReadLine(), out var yearLevel) && yearLevel >= 1 && yearLevel <= 4)
            {
                return yearLevel;
            }

            Console.WriteLine("Year Level must be 1, 2, 3, or 4.");
        }
    }
}
