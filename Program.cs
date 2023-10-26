using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using ATIK_lab_1.Coding;

class Program
{
    static void Main()
    {
        Program program = new Program();
        program.MainCycle();
    }

    private void MainCycle()
    {
        while (true)
        {
            string compressedFileName = "compressed.zip"; // Путь к создаваемому архиву ZIP
            int countArg = 3;
            Console.WriteLine(" ВВедите режим работы программы (1 - кодировть, 0 - декодировать, 3 - кодировать Хофманом, 2 - выйти из программы)," +
                "\n имя файла который необходимо архивировать или деарзивировать имя файла в который" +
                "\n будет произведены действия. Сигнатура: <mode> <input_file> <output_file>");

            string? arg = Console.ReadLine();

            if (arg == null)
            {
                Console.WriteLine("Usage: <mode> <input_file> <output_file>");
                return;
            }

            String[] args = arg.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (args.Length != countArg)
            {
                Console.WriteLine("Usage: <mode> <input_file> <output_file>");
                return;
            }

            string mode = args[0];
            string inputFileName = args[1];
            string outputFileName = args[2];

            if (mode == "1")
                Encode.EncodeFile(inputFileName, outputFileName);
            else if (mode == "0")
                Decode.DecodeFile(inputFileName, outputFileName);
            else if (mode == "3")
                Decode.CompressData(inputFileName, compressedFileName);
            else if (mode == "2")
                return;
            else
                Console.WriteLine("Invalid mode. Use 'encode - 1' or 'decode - 0'.");
        }
    }
}
