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
            int BlockSize = 16;
            int MaxDifficallity = 256;
            Console.WriteLine("Original text:");
            Console.WriteLine(Encoding.UTF8.GetString(Message));

            LHXORwCD _LHXORwCD = new LHXORwCD();
            byte[] Encrypted = _LHXORwCD.CBCChipher(Message, Password, true, BlockSize, MaxDifficallity);
            Console.WriteLine("EnCrypted: " + _LHXORwCD.BytesToHex(Encrypted, " "));

            byte[] Decrypted = _LHXORwCD.CBCChipher(Encrypted, Password, false, BlockSize, MaxDifficallity);
            Console.WriteLine("DeCrypted: " + Encoding.UTF8.GetString(Decrypted));

            //byte[] EncryptedB = _LHXORwCD.MLinearChipher(Message, Password, true, MaxDifficallity);
            //Console.WriteLine("EnCrypted: " + _LHXORwCD.BytesToHex(EncryptedB, " "));

            //byte[] DecryptedB = _LHXORwCD.MLinearChipher(EncryptedB, Password, false, MaxDifficallity);
            //Console.WriteLine("DeCrypted: " + Encoding.UTF8.GetString(DecryptedB));

            //byte[] EncryptedС = _LHXORwCD.LinearChipher(Message, Password, true, MaxDifficallity);
            //Console.WriteLine("EnCrypted: " + _LHXORwCD.BytesToHex(EncryptedС, " "));

            //byte[] DecryptedС = _LHXORwCD.LinearChipher(EncryptedС, Password, false, MaxDifficallity);
            //Console.WriteLine("DeCrypted: " + Encoding.UTF8.GetString(DecryptedС));

            //byte[] EncryptedС = _LHXORwCD.BLinearChipher(Message, Password, true, BlockSize, MaxDifficallity);
            //Console.WriteLine("EnCrypted: " + _LHXORwCD.BytesToHex(EncryptedС, " "));

            //byte[] DecryptedС = _LHXORwCD.BLinearChipher(EncryptedС, Password, false, BlockSize, MaxDifficallity);
            //Console.WriteLine("DeCrypted: " + Encoding.UTF8.GetString(DecryptedС));

            //byte[] EncryptedС = _LHXORwCD.BMLinearChipher(Message, Password, true, BlockSize, MaxDifficallity);
            //Console.WriteLine("EnCrypted: " + _LHXORwCD.BytesToHex(EncryptedС, " "));

            //byte[] DecryptedС = _LHXORwCD.BMLinearChipher(EncryptedС, Password, false, BlockSize, MaxDifficallity);
            //Console.WriteLine("DeCrypted: " + Encoding.UTF8.GetString(DecryptedС));

            Console.ReadKey();
        }
    }
}
