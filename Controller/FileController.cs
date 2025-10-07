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
                string paragraph = _view.GetParagraph();
                _manager.CreateInputFile(paragraph);

                int size = _view.GetFragmentSize();
                _manager.FragmentFile(size);
                _view.DisplayFiles(_manager.GetFragmentFiles());

                string fileName = _view.GetFileNameToVerify();
                _manager.VerifyFileExistence(fileName);

                _manager.DefragmentFiles();

                bool isSame = _manager.CompareFiles();
                _view.DisplayComparisonResult(isSame);

                _view.AskForRestart(_manager);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"?? Error: {ex.Message}");
            }
        }
    }
}
