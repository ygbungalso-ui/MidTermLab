using System;
using System.Collections.Generic;

namespace Problem2_Dictionary;

public struct Student
{
    public string StudentNumber;
    public string Name;
    public string Program;
    public int YearLevel;
}

public static class Program
{
    public static void Main()
    {
        var students = new Dictionary<string, Student>(StringComparer.OrdinalIgnoreCase);
        var running = true;

        while (running)
        {
            Console.WriteLine("\n======================================================");
            Console.WriteLine("               STUDENT LOOKUP USING DICTIONARY          ");
            Console.WriteLine("========================================================");
            Console.WriteLine("1. Add Student                                          ");
            Console.WriteLine("2. Search Student                                       ");
            Console.WriteLine("3. Display All Students                                 ");
            Console.WriteLine("4. Exit                                                 ");

            switch (ReadChoice())
            {
                case 1:
                    AddStudent(students);
                    break;
                case 2:
                    SearchStudent(students);
                    break;
                case 3:
                    DisplayStudents(students);
                    break;
                case 4:
                    running = false;
                    Console.WriteLine("Program exited.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Enter a number from 1 to 4.");
                    break;
            }
        }
    }

    private static void AddStudent(Dictionary<string, Student> students)
    {
        var studentNumber = ReadRequired("Enter Student Number: ");
        if (students.ContainsKey(studentNumber))
        {
            Console.WriteLine("A student with that Student Number already exists.");
            return;
        }

        var student = new Student
        {
            StudentNumber = studentNumber,
            Name = ReadRequired("Enter Name: "),
            Program = ReadRequired("Enter Program: "),
            YearLevel = ReadYearLevel()
        };

        students.Add(student.StudentNumber, student);
        Console.WriteLine("Student added successfully!");
    }

    private static void SearchStudent(Dictionary<string, Student> students)
    {
        var studentNumber = ReadRequired("Enter Student Number to search: ");
        if (!students.TryGetValue(studentNumber, out var student))
        {
            Console.WriteLine("Student not found.");
            return;
        }

        Console.WriteLine("\nStudent Found!");
        PrintStudent(student);
    }

    private static void DisplayStudents(Dictionary<string, Student> students)
    {
        Console.WriteLine("\nSTUDENT RECORDS");
        if (students.Count == 0)
        {
            Console.WriteLine("No student records to display.");
            return;
        }

        foreach (var student in students.Values)
        {
            PrintStudent(student);
        }
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
