using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileFragmentation.Model
{
    public class FileManager
    {
        private readonly string inputFile = "input.txt";
        private readonly string outputFile = "output.txt";

        public void CreateInputFile(string data)
        {
            File.WriteAllText(inputFile, data);
            Console.WriteLine("\n Input file created successfully.\n");
        }

        public void FragmentFile(int size)
        {
            string content = File.ReadAllText(inputFile);
            int fileCount = (int)Math.Ceiling((double)content.Length / size);

            // Determine padding dynamically based on file count
            int padding = fileCount >= 1000 ? 4 :
                          fileCount >= 100 ? 3 :
                          fileCount >= 10 ? 2 : 0;

            for (int i = 1; i <= fileCount; i++)
            {
                string fragment = new string(content.Skip((i - 1) * size).Take(size).ToArray());

                string fileName;
                if (padding > 0)
                    fileName = i.ToString($"D{padding}") + ".txt"; // Zero-padded
                else
                    fileName = $"{i}.txt"; // No zero padding (for <10 files)

                File.WriteAllText(fileName, fragment);
            }

            Console.WriteLine($"Fragmentation completed — {fileCount} files created.\n");
        }



        public List<string> GetFragmentFiles()
        {
            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "???.txt")
                                 .Where(f => Path.GetFileName(f) != "input.txt" &&
                                             Path.GetFileName(f) != "output.txt")
                                 .OrderBy(f => f)
                                 .ToList();
            return files;
        }

        public void VerifyFileExistence(string fileName)
        {
            if (File.Exists(fileName))
            {
                Console.WriteLine($" File '{fileName}' exists. Content:\n");
                Console.WriteLine(File.ReadAllText(fileName));
            }
            else
            { 
                Console.WriteLine(" File not found!\n");
            }
        }

        public void DefragmentFiles()
        {
            var files = GetFragmentFiles();
            string combinedData = "";

            foreach (var file in files)
            {
                combinedData += File.ReadAllText(file);
            }

            File.WriteAllText(outputFile, combinedData);
            Console.WriteLine("\n Defragmentation completed. Output file created: output.txt\n");
            Console.WriteLine(" Reassembled Content:\n");
            Console.WriteLine(combinedData + "\n");
        }

        public bool CompareFiles()
        {
            string inputData = File.ReadAllText(inputFile);
            string outputData = File.ReadAllText(outputFile);
            return inputData.Equals(outputData);
        }

        public void DeleteAllFiles()
        {
            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.txt"))
            {
                File.Delete(file);
            }
            Console.WriteLine("\n All files deleted.\n");
        }
    }
}
