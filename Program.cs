using FileFragmentation.Controller;

namespace FileFragmentation
{
    class Program
    {
        static void Main(string[] args)
        {
            FileController controller = new FileController();
            controller.Start();
        }
    }
}
