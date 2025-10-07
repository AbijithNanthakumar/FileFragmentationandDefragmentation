using FileFragmentation.Model;
using FileFragmentation.View;
using System;

namespace FileFragmentation.Controller
{
    public class FileController
    {
        private readonly FileView _view;
        private readonly FileManager _manager;

        public FileController()
        {
            _view = new FileView();
            _manager = new FileManager();
        }

        public void Start()
        {
            _manager.DeleteAllFiles();
            try
            {
                // Step 1: Create input file
                string paragraph = _view.GetParagraph();
                _manager.CreateInputFile(paragraph);

                // Step 2: Fragmentation
                int size = _view.GetFragmentSize();
                _manager.FragmentFile(size);
                _view.DisplayFiles(_manager.GetFragmentFiles());

                // Step 3: Verify existence
                string fileName = _view.GetFileNameToVerify();
                _manager.VerifyFileExistence(fileName);

                // Step 4: Defragmentation
                _manager.DefragmentFiles();

                // Step 5: Compare input and output
                bool isSame = _manager.CompareFiles();
                _view.DisplayComparisonResult(isSame);

                // Ask for restart
                _view.AskForRestart(_manager);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"?? Error: {ex.Message}");
            }
        }
    }
}
