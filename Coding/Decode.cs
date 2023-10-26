using System;
using System.Collections.Generic;
using System.Data.SharpZipLib;
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

        public static void CompressData(string inputFileName, string outputFileName)
        {
            using (FileStream inputFileStream = File.OpenRead(inputFileName))
            using (FileStream outputFileStream = File.Create(outputFileName))
            using (ZipOutputStream zipStream = new ZipOutputStream(outputFileStream))
            {
                ZipEntry entry = new ZipEntry(Path.GetFileName(inputFileName));
                entry.CompressionMethod = CompressionMethod.Deflated; // Используем метод сжатия Deflate (Хаффмана)
                zipStream.PutNextEntry(entry);

                byte[] buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = inputFileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    zipStream.Write(buffer, 0, bytesRead);
                }

                zipStream.CloseEntry();
                zipStream.IsStreamOwner = false; // Устанавливаем в false, чтобы не закрывать outputFileStream

                Console.WriteLine("Compression completed.");
            }
        }
    }
}
