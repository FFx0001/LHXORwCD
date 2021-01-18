using System;
using System.Text;

namespace LHXORwCDChiper
{
    class Program
    {
        static void Main(string[] args)
        {
            

            string value = "Разнообразный и богатый опыт консультация с широким активом способствует подготовки и реализации систем массового участия. Разнообразный и богатый опыт реализация намеченных плановых заданий требуют определения и уточнения систем массового участия. С другой стороны новая модель организационной деятельности позволяет оценить значение соответствующий условий активизации. С другой стороны рамки и место обучения кадров позволяет выполнять важные задания по разработке дальнейших направлений развития.";
            byte[] Message = Encoding.UTF8.GetBytes(value);
            byte[] Password = Encoding.UTF8.GetBytes("а23в34ау69");
            int BlockSize = 16; // размер блока 64 миксимум (если используется режим с блоками)
            int MaxDifficallity = 512;// сложность алгоритма (указать 1 если требуется минимальная)
            Console.WriteLine("Original text:");
            Console.WriteLine(Encoding.UTF8.GetString(Message));

            LHXORwCD _LHXORwCD = new LHXORwCD();

            // LinearChipher (LHXORwCD) алгоритм (не рекомендуется использовать в боевых целях в чистом виде, для каждого нового сообщения требуется новый пароль)
            byte[] EncryptedA = _LHXORwCD.LinearChipher(Message, Password, true, MaxDifficallity);
            Console.WriteLine("\nLinearChipher EnCrypted: " + _LHXORwCD.BytesToHex(EncryptedA, " "));

            byte[] DecryptedA = _LHXORwCD.LinearChipher(EncryptedA, Password, false, MaxDifficallity);
            Console.WriteLine("\nLinearChipher DeCrypted: " + Encoding.UTF8.GetString(DecryptedA));

            // SBLinearChipher (LHXORwCD + Swap Block Linear Chiper) алгоритм (не рекомендуется использовать в боевых целях в чистом виде, для каждого нового сообщения требуется новый пароль)
            // рекомендуется наименьший размер блока 1-4
            byte[] EncryptedB = _LHXORwCD.SBLinearChipher(Message, Password, true, MaxDifficallity);
            Console.WriteLine("\nSBLinearChipher EnCrypted: " + _LHXORwCD.BytesToHex(EncryptedB, " "));

            byte[] DecryptedB = _LHXORwCD.SBLinearChipher(EncryptedB, Password, false, MaxDifficallity);
            Console.WriteLine("\nSBLinearChipher DeCrypted: " + Encoding.UTF8.GetString(DecryptedB));

            // MLinearChipher (LHXORwCD + Morph Linear Chiper) алгоритм  (Рекомендуется использовать новый пароль на каждые 10 сообщений, а еще лучше новый пароль для каждого нового сообщения)
            byte[] EncryptedС = _LHXORwCD.MLinearChipher(Message, Password, true, MaxDifficallity);
            Console.WriteLine("\nMLinearChipher EnCrypted: " + _LHXORwCD.BytesToHex(EncryptedС, " "));

            byte[] DecryptedС = _LHXORwCD.MLinearChipher(EncryptedС, Password, false, MaxDifficallity);
            Console.WriteLine("\nMLinearChipher DeCrypted: " + Encoding.UTF8.GetString(DecryptedС));

            // SBMLinearChipher (LHXORwCD + Swap Block Morph Linear Chiper)  алгоритм (Рекомендуется использовать новый пароль на каждые 10 сообщений, а еще лучше новый пароль для каждого нового сообщения)
            // рекомендуется наименьший размер блока 1-4
            byte[] EncryptedD = _LHXORwCD.SBMLinearChipher(Message, Password, true, MaxDifficallity);
            Console.WriteLine("\nSBMLinearChipher EnCrypted: " + _LHXORwCD.BytesToHex(EncryptedD, " "));

            byte[] DecryptedD = _LHXORwCD.SBMLinearChipher(EncryptedD, Password, false, MaxDifficallity);
            Console.WriteLine("\nSBMLinearChipher DeCrypted: " + Encoding.UTF8.GetString(DecryptedD));

            // SBMCBCChipher (LHXORwCD + Swap Block Morph CBC Chiper)  алгоритм (Рекомендуется использовать новый пароль на каждые 10 сообщений, а еще лучше новый пароль для каждого нового сообщения)
            // рекомендуется размер блока 4 - 8 - 16
            byte[] EncryptedE = _LHXORwCD.SBMCBCChipher(Message, Password, true, BlockSize, MaxDifficallity);
            Console.WriteLine("\nSBMCBCChipher EnCrypted: " + _LHXORwCD.BytesToHex(EncryptedE, " "));

            byte[] DecryptedE = _LHXORwCD.SBMCBCChipher(EncryptedE, Password, false, BlockSize, MaxDifficallity);
            Console.WriteLine("\nSBMCBCChipher DeCrypted: " + Encoding.UTF8.GetString(DecryptedE));

            // DSBMCBCChipher (LHXORwCD + Double Swap Block Morph CBC Chiper) алгоритм (Рекомендуется использовать новый пароль на каждые 10 сообщений, а еще лучше новый пароль для каждого нового сообщения)
            // рекомендуется размер блока 8 - 16
            byte[] EncryptedF = _LHXORwCD.DSBMCBCChipher(Message, Password, true, BlockSize, MaxDifficallity);
            Console.WriteLine("\nDSBMCBCChipher EnCrypted: " + _LHXORwCD.BytesToHex(EncryptedF, " "));

            byte[] DecryptedF = _LHXORwCD.DSBMCBCChipher(EncryptedF, Password, false, BlockSize, MaxDifficallity);
            Console.WriteLine("\nDSBMCBCChipher DeCrypted: " + Encoding.UTF8.GetString(DecryptedF));


            Console.ReadKey();
        }
    }
}
