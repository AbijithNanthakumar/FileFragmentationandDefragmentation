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
        private readonly string fragmentFolder = "Fragments"; // ✅ Define this field

        public void CreateInputFile(string data)
        {
            File.WriteAllText(inputFile, data);
            Console.WriteLine("\nInput file created successfully.\n");
        }

        public void FragmentFile(int size)
        {
            if (!File.Exists(inputFile))
            {
                Console.WriteLine(" Input file not found.");
                return;
            }

            string content = File.ReadAllText(inputFile);
            int fileCount = (int)Math.Ceiling((double)content.Length / size);

            int padding = fileCount.ToString().Length;

            Directory.CreateDirectory(fragmentFolder);

            for (int i = 1; i <= fileCount; i++)
            {
                string fragment = new string(content
                    .Skip((i - 1) * size)
                    .Take(size)
                    .ToArray());

                string fileName = $"{i.ToString().PadLeft(padding, '0')}.txt";
                string fullPath = Path.Combine(fragmentFolder, fileName);

                File.WriteAllText(fullPath, fragment);
            }

            Console.WriteLine($"\n Fragmentation completed — {fileCount} file(s) created in \"{fragmentFolder}\" folder.");
        }

        public List<string> GetFragmentFiles()
        {
            if (!Directory.Exists(fragmentFolder))
                return new List<string>();

            var files = Directory.GetFiles(fragmentFolder, "*.txt")
                                 .OrderBy(f => f)
                                 .ToList();
            return files;
        }

        public void VerifyFileExistence(string fileName)
        {
            string fullPath = Path.Combine(fragmentFolder, fileName);

                if (File.Exists(fullPath))
                {
                    Console.WriteLine($"\n File '{fileName}' exists. Content:\n");
                    Console.WriteLine(File.ReadAllText(fullPath));
                    return;
                }
                else
                {
                    Console.WriteLine($"\n File '{fileName}' not found in '{fragmentFolder}' folder.\n");
                }
            
        }


        public void DefragmentFiles()
        {
            var files = GetFragmentFiles();
            if (files.Count == 0)
            {
                Console.WriteLine(" No fragments to defragment.");
                return;
            }

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
            if (!File.Exists(inputFile) || !File.Exists(outputFile))
                return false;

            string inputData = File.ReadAllText(inputFile);
            string outputData = File.ReadAllText(outputFile);
            return inputData.Equals(outputData);
        }

        public void DeleteAllFiles()
        {
            if (Directory.Exists(fragmentFolder))
            {
                string[] files = Directory.GetFiles(fragmentFolder);

                foreach (string file in files)
                {
                    try
                    {
                        File.Delete(file);
                        File.Delete(inputFile);
                        File.Delete(outputFile);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($" Error deleting {file}: {ex.Message}");
                    }
                }

                Console.WriteLine(" All files deleted from 'Fragments' folder.");
            }
            else
            {
                Console.WriteLine(" 'Fragments' folder not found.");
            }
        }
    }
}
