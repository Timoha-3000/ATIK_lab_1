using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATIK_lab_1.Coding
{
    public static class Decode
    {
        public static void DecodeFile(string inputFileName, string outputFileName)
        {
            using (FileStream inputFileStream = File.OpenRead(inputFileName))
            using (BinaryReader reader = new BinaryReader(inputFileStream))
            using (FileStream outputFileStream = File.Create(outputFileName))
            {
                // Проверка сигнатуры
                byte[] signature = reader.ReadBytes(4);
                string signatureString = Encoding.ASCII.GetString(signature);

                if (signatureString != "MYF1")
                {
                    Console.WriteLine("Invalid signature.");
                    return;
                }

                // Чтение заголовка архива
                byte formatVersion = reader.ReadByte();
                byte contextCompressionCode = reader.ReadByte();
                byte contextFreeCompressionCode = reader.ReadByte();
                byte errorProtectionCode = reader.ReadByte();
                int originalFileSize = reader.ReadInt32();
                int additionalData = reader.ReadInt32();

                // Проверка кодов алгоритмов (в данном примере не выполняется)

                // Копирование данных из архива в выходной файл
                byte[] buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = reader.BaseStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    outputFileStream.Write(buffer, 0, bytesRead);
                }

                Console.WriteLine("Decoding completed.");
            }
        }
    }
}
