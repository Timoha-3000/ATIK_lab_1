using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATIK_lab_1.Coding
{
    public static class Encode
    {
        public static void EncodeFile(string inputFileName, string outputFileName)
        {
            using (FileStream inputFileStream = File.OpenRead(inputFileName))
            using (FileStream outputFileStream = File.Create(outputFileName))
            using (BinaryWriter writer = new BinaryWriter(outputFileStream))
            {
                // Заголовок архива
                writer.Write(Encoding.ASCII.GetBytes("MYF1")); // Сигнатура
                writer.Write((byte)0); // Версия формата
                writer.Write((byte)0); // Код сжатия контекста (без сжатия)
                writer.Write((byte)0); // Код сжатия без контекста (без сжатия)
                writer.Write((byte)0); // Код защиты от помех (без защиты)
                writer.Write((int)inputFileStream.Length); // Длина исходного файла
                writer.Write((int)0); // Дополнительные данные (в данном случае 0)

                // Копирование несжатых данных из входного файла
                inputFileStream.CopyTo(writer.BaseStream);

                Console.WriteLine("Encoding completed.");
            }
        }
    }
}
