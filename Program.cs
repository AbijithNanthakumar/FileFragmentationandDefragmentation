using FileFragmentation.Controller;

namespace FileFragmentation
{
    class Program
    {
        public static void Main(string[] args)
        {
            FileController controller = new FileController(); 
            controller.Start();
        }
    }
}
