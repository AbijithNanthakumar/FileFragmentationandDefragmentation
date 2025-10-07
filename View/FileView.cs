using FileFragmentation.Model;
using FileFragmentation.Controller;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileFragmentation.View
{
    public class FileView
    {
        public string GetParagraph()
        {
            Console.WriteLine("Enter the paragraph (Type 'END' in a new line to finish):");
            StringBuilder sb = new StringBuilder();

            while (true)
            {
                string line = Console.ReadLine();
                if (line?.Trim().ToUpper() == "END")
                    break;
                sb.AppendLine(line); // preserve new lines
            }

            return sb.ToString();
        }

        public int GetFragmentSize()
        {
            Console.Write("\nEnter fragmentation size (e.g., 10): ");
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int size) && size > 0)
                    return size;
                Console.Write("Invalid size! Please enter a positive number: ");
            }
        }

        public void DisplayFiles(List<string> files)
        {
            Console.WriteLine("\n Created fragment files:");
            foreach (var file in files)
                Console.WriteLine(" - " + System.IO.Path.GetFileName(file));
            Console.WriteLine();
        }

        public string GetFileNameToVerify()
        {
            Console.Write("\nEnter file name to verify (e.g., 001.txt): ");
            return Console.ReadLine();
        }

        public void DisplayComparisonResult(bool isSame)
        {
            Console.WriteLine(isSame
                ? "\n SUCCESS: Input and output files match!"
                : "\n ERROR: Files do not match. Something went wrong!");
        }

        public void AskForRestart(FileManager manager)
        {
            Console.Write("\nDo you want to restart and delete all files? (y/n): ");
            string choice = Console.ReadLine()?.ToLower();

            if (choice == "y")
            {
                manager.DeleteAllFiles();
                Console.WriteLine("Restarting...\n");

                // ✅ Restart properly using Controller (not Main)
                new FileController().Start();
            }
            else
            {
                Console.WriteLine("Exiting program. Goodbye!");
            }
        }
    }
}
