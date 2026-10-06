using System;
using System.Collections.Generic;

namespace Problem3_Queue;

public struct StudentRequest
{
    public string StudentNumber;
    public string StudentName;
    public string RequestType;
}

public static class Program
{
    public static void Main()
    {
        var requestQueue = new Queue<StudentRequest>();
        var running = true;

        while (running)
        {
            Console.WriteLine("\n====================================================");
            Console.WriteLine("                 STUDENT REQUEST QUEUE                ");
            Console.WriteLine("======================================================");
            Console.WriteLine("1. Add Request                                        ");
            Console.WriteLine("2. View Pending Requests                              ");
            Console.WriteLine("3. Process Request                                    ");
            Console.WriteLine("4. Exit                                               ");

            switch (ReadChoice())
            {
                case 1:
                    AddRequest(requestQueue);
                    break;
                case 2:
                    ViewRequests(requestQueue);
                    break;
                case 3:
                    ProcessRequest(requestQueue);
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

    private static void AddRequest(Queue<StudentRequest> requestQueue)
    {
        var request = new StudentRequest
        {
            StudentNumber = ReadRequired("Enter Student Number: "),
            StudentName = ReadRequired("Enter Student Name: "),
            RequestType = ReadRequired("Enter Request Type: ")
        };

        requestQueue.Enqueue(request);
        Console.WriteLine("Request added successfully!");
    }

    private static void ViewRequests(Queue<StudentRequest> requestQueue)
    {
        Console.WriteLine("\nREQUEST QUEUE");
        if (requestQueue.Count == 0)
        {
            Console.WriteLine("There are no pending requests.");
            return;
        }

        var position = 1;
        foreach (var request in requestQueue)
        {
            Console.WriteLine($"{position}. {request.StudentName} - {request.RequestType} (Student Number: {request.StudentNumber})");
            position++;
        }
    }

    private static void ProcessRequest(Queue<StudentRequest> requestQueue)
    {
        if (requestQueue.Count == 0)
        {
            Console.WriteLine("There are no pending requests to process.");
            return;
        }

        var request = requestQueue.Dequeue();
        Console.WriteLine($"Processing Request: {request.StudentName} - {request.RequestType}");
        Console.WriteLine("Request processed successfully!");
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
}
