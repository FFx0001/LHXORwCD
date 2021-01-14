using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace LHXORwCDChiper
{
    public class Crc64
    {
        private static readonly ulong[] Table = {
            0x0000000000000000, 0x7ad870c830358979,
            0xf5b0e190606b12f2, 0x8f689158505e9b8b,
            0xc038e5739841b68f, 0xbae095bba8743ff6,
            0x358804e3f82aa47d, 0x4f50742bc81f2d04,
            0xab28ecb46814fe75, 0xd1f09c7c5821770c,
            0x5e980d24087fec87, 0x24407dec384a65fe,
            0x6b1009c7f05548fa, 0x11c8790fc060c183,
            0x9ea0e857903e5a08, 0xe478989fa00bd371,
            0x7d08ff3b88be6f81, 0x07d08ff3b88be6f8,
            0x88b81eabe8d57d73, 0xf2606e63d8e0f40a,
            0xbd301a4810ffd90e, 0xc7e86a8020ca5077,
            0x4880fbd87094cbfc, 0x32588b1040a14285,
            0xd620138fe0aa91f4, 0xacf86347d09f188d,
            0x2390f21f80c18306, 0x594882d7b0f40a7f,
            0x1618f6fc78eb277b, 0x6cc0863448deae02,
            0xe3a8176c18803589, 0x997067a428b5bcf0,
            0xfa11fe77117cdf02, 0x80c98ebf2149567b,
            0x0fa11fe77117cdf0, 0x75796f2f41224489,
            0x3a291b04893d698d, 0x40f16bccb908e0f4,
            0xcf99fa94e9567b7f, 0xb5418a5cd963f206,
            0x513912c379682177, 0x2be1620b495da80e,
            0xa489f35319033385, 0xde51839b2936bafc,
            0x9101f7b0e12997f8, 0xebd98778d11c1e81,
            0x64b116208142850a, 0x1e6966e8b1770c73,
            0x8719014c99c2b083, 0xfdc17184a9f739fa,
            0x72a9e0dcf9a9a271, 0x08719014c99c2b08,
            0x4721e43f0183060c, 0x3df994f731b68f75,
            0xb29105af61e814fe, 0xc849756751dd9d87,
            0x2c31edf8f1d64ef6, 0x56e99d30c1e3c78f,
            0xd9810c6891bd5c04, 0xa3597ca0a188d57d,
            0xec09088b6997f879, 0x96d1784359a27100,
            0x19b9e91b09fcea8b, 0x636199d339c963f2,
            0xdf7adabd7a6e2d6f, 0xa5a2aa754a5ba416,
            0x2aca3b2d1a053f9d, 0x50124be52a30b6e4,
            0x1f423fcee22f9be0, 0x659a4f06d21a1299,
            0xeaf2de5e82448912, 0x902aae96b271006b,
            0x74523609127ad31a, 0x0e8a46c1224f5a63,
            0x81e2d7997211c1e8, 0xfb3aa75142244891,
            0xb46ad37a8a3b6595, 0xceb2a3b2ba0eecec,
            0x41da32eaea507767, 0x3b024222da65fe1e,
            0xa2722586f2d042ee, 0xd8aa554ec2e5cb97,
            0x57c2c41692bb501c, 0x2d1ab4dea28ed965,
            0x624ac0f56a91f461, 0x1892b03d5aa47d18,
            0x97fa21650afae693, 0xed2251ad3acf6fea,
            0x095ac9329ac4bc9b, 0x7382b9faaaf135e2,
            0xfcea28a2faafae69, 0x8632586aca9a2710,
            0xc9622c4102850a14, 0xb3ba5c8932b0836d,
            0x3cd2cdd162ee18e6, 0x460abd1952db919f,
            0x256b24ca6b12f26d, 0x5fb354025b277b14,
            0xd0dbc55a0b79e09f, 0xaa03b5923b4c69e6,
            0xe553c1b9f35344e2, 0x9f8bb171c366cd9b,
            0x10e3202993385610, 0x6a3b50e1a30ddf69,
            0x8e43c87e03060c18, 0xf49bb8b633338561,
            0x7bf329ee636d1eea, 0x012b592653589793,
            0x4e7b2d0d9b47ba97, 0x34a35dc5ab7233ee,
            0xbbcbcc9dfb2ca865, 0xc113bc55cb19211c,
            0x5863dbf1e3ac9dec, 0x22bbab39d3991495,
            0xadd33a6183c78f1e, 0xd70b4aa9b3f20667,
            0x985b3e827bed2b63, 0xe2834e4a4bd8a21a,
            0x6debdf121b863991, 0x1733afda2bb3b0e8,
            0xf34b37458bb86399, 0x8993478dbb8deae0,
            0x06fbd6d5ebd3716b, 0x7c23a61ddbe6f812,
            0x3373d23613f9d516, 0x49aba2fe23cc5c6f,
            0xc6c333a67392c7e4, 0xbc1b436e43a74e9d,
            0x95ac9329ac4bc9b5, 0xef74e3e19c7e40cc,
            0x601c72b9cc20db47, 0x1ac40271fc15523e,
            0x5594765a340a7f3a, 0x2f4c0692043ff643,
            0xa02497ca54616dc8, 0xdafce7026454e4b1,
            0x3e847f9dc45f37c0, 0x445c0f55f46abeb9,
            0xcb349e0da4342532, 0xb1eceec59401ac4b,
            0xfebc9aee5c1e814f, 0x8464ea266c2b0836,
            0x0b0c7b7e3c7593bd, 0x71d40bb60c401ac4,
            0xe8a46c1224f5a634, 0x927c1cda14c02f4d,
            0x1d148d82449eb4c6, 0x67ccfd4a74ab3dbf,
            0x289c8961bcb410bb, 0x5244f9a98c8199c2,
            0xdd2c68f1dcdf0249, 0xa7f41839ecea8b30,
            0x438c80a64ce15841, 0x3954f06e7cd4d138,
            0xb63c61362c8a4ab3, 0xcce411fe1cbfc3ca,
            0x83b465d5d4a0eece, 0xf96c151de49567b7,
            0x76048445b4cbfc3c, 0x0cdcf48d84fe7545,
            0x6fbd6d5ebd3716b7, 0x15651d968d029fce,
            0x9a0d8ccedd5c0445, 0xe0d5fc06ed698d3c,
            0xaf85882d2576a038, 0xd55df8e515432941,
            0x5a3569bd451db2ca, 0x20ed197575283bb3,
            0xc49581ead523e8c2, 0xbe4df122e51661bb,
            0x3125607ab548fa30, 0x4bfd10b2857d7349,
            0x04ad64994d625e4d, 0x7e7514517d57d734,
            0xf11d85092d094cbf, 0x8bc5f5c11d3cc5c6,
            0x12b5926535897936, 0x686de2ad05bcf04f,
            0xe70573f555e26bc4, 0x9ddd033d65d7e2bd,
            0xd28d7716adc8cfb9, 0xa85507de9dfd46c0,
            0x273d9686cda3dd4b, 0x5de5e64efd965432,
            0xb99d7ed15d9d8743, 0xc3450e196da80e3a,
            0x4c2d9f413df695b1, 0x36f5ef890dc31cc8,
            0x79a59ba2c5dc31cc, 0x037deb6af5e9b8b5,
            0x8c157a32a5b7233e, 0xf6cd0afa9582aa47,
            0x4ad64994d625e4da, 0x300e395ce6106da3,
            0xbf66a804b64ef628, 0xc5bed8cc867b7f51,
            0x8aeeace74e645255, 0xf036dc2f7e51db2c,
            0x7f5e4d772e0f40a7, 0x05863dbf1e3ac9de,
            0xe1fea520be311aaf, 0x9b26d5e88e0493d6,
            0x144e44b0de5a085d, 0x6e963478ee6f8124,
            0x21c640532670ac20, 0x5b1e309b16452559,
            0xd476a1c3461bbed2, 0xaeaed10b762e37ab,
            0x37deb6af5e9b8b5b, 0x4d06c6676eae0222,
            0xc26e573f3ef099a9, 0xb8b627f70ec510d0,
            0xf7e653dcc6da3dd4, 0x8d3e2314f6efb4ad,
            0x0256b24ca6b12f26, 0x788ec2849684a65f,
            0x9cf65a1b368f752e, 0xe62e2ad306bafc57,
            0x6946bb8b56e467dc, 0x139ecb4366d1eea5,
            0x5ccebf68aecec3a1, 0x2616cfa09efb4ad8,
            0xa97e5ef8cea5d153, 0xd3a62e30fe90582a,
            0xb0c7b7e3c7593bd8, 0xca1fc72bf76cb2a1,
            0x45775673a732292a, 0x3faf26bb9707a053,
            0x70ff52905f188d57, 0x0a2722586f2d042e,
            0x854fb3003f739fa5, 0xff97c3c80f4616dc,
            0x1bef5b57af4dc5ad, 0x61372b9f9f784cd4,
            0xee5fbac7cf26d75f, 0x9487ca0fff135e26,
            0xdbd7be24370c7322, 0xa10fceec0739fa5b,
            0x2e675fb4576761d0, 0x54bf2f7c6752e8a9,
            0xcdcf48d84fe75459, 0xb71738107fd2dd20,
            0x387fa9482f8c46ab, 0x42a7d9801fb9cfd2,
            0x0df7adabd7a6e2d6, 0x772fdd63e7936baf,
            0xf8474c3bb7cdf024, 0x829f3cf387f8795d,
            0x66e7a46c27f3aa2c, 0x1c3fd4a417c62355,
            0x935745fc4798b8de, 0xe98f353477ad31a7,
            0xa6df411fbfb21ca3, 0xdc0731d78f8795da,
            0x536fa08fdfd90e51, 0x29b7d047efec8728,
        };

        public static ulong Compute(byte[] s, ulong crc = 0)
        {
            for (int j = 0; j < s.Length; j++)
            {
                crc = Crc64.Table[(byte)(crc ^ s[j])] ^ (crc >> 8);
            }

            return crc;
        }
    }

    public class FFxTG_RNG
    {
        private byte[] seed;
        public FFxTG_RNG(UInt32 seed)
        {
            if (seed < 0)
                throw new Exception("Bad seed");
            this.seed = BitConverter.GetBytes(seed).Take(4).ToArray();
        }

        public void SetSeed(UInt32 _seed)
        {
            this.seed = BitConverter.GetBytes(_seed).Take(4).ToArray(); ;
        }

        public float Next()
        {
            byte[] crcBytes = BitConverter.GetBytes(Crc64.Compute(this.seed));
            byte[] First4Bytes = crcBytes.Take(4).ToArray();
            this.seed = crcBytes.Skip(4).Take(4).ToArray();
            UInt32 crc = First4Bytes[0];
            crc = crc << 8;
            crc += First4Bytes[1];
            crc = crc << 8;
            crc += First4Bytes[2];
            crc = crc << 8;
            crc += First4Bytes[3];
            crc = crc << 8;
            return crc/ 4294967295f;
        }

        public int Next(int min, int max)
        {
            float x = this.Next();
            return (int)((max - min) * x + min);
        }
    }
    
    class LHXORwCD
    {
        // вычислить сид из массива байт
        private UInt32 GetSeedFromBytesArray(byte[] Password)
        {
            var SHA512Base = System.Security.Cryptography.SHA512.Create();
            byte[] hash = SHA512Base.ComputeHash(Password);
            UInt32 LSeed = hash[60];
            LSeed = LSeed << 8;
            LSeed += hash[61];
            LSeed = LSeed << 8;
            LSeed += hash[62];
            LSeed = LSeed << 8;
            LSeed += hash[63];
            return LSeed;
        }

        // создаем таблицу байтовых замен из 10 блоков по 25 элементов всех битов пдряд от 0-256
        private List<byte[]> GetMorphTable(FFxTG_RNG _FFxTG_RNG)
        {
            List<byte> list = new List<byte>();
            for(int i = 1; i<= 250;i++) { list.Add((byte)i); }
            
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _FFxTG_RNG.Next(0,(n + 1));
                byte value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            List<byte[]> mixed = new List<byte[]>();
            var block = new byte[list.Count / 10];
            n = 0;
            foreach (byte v in list)
            {
                block[n] = v;
                n++;
                if (n >= (list.Count/10))
                {
                    n = 0;
                    mixed.Add(block);
                    block = new byte[(list.Count/10)];
                }

            }
            return mixed;
        }

        // генерация длинного хеша на основе парольной фразы
        // алгоритм генерации использует нелинейное число раундов хеширования каждого последующего 32 битного фрагмента на основе вектора инициализации
        // нелинейная скорость работы функции
        private byte[] CreateLongHashSHAWithPassword(byte[] Password,int MinLength, FFxTG_RNG _FFxTG_RNG, int MaxCountRaund = 256 )
        {
            List<byte> LongHash = new List<byte>();
            var SHA512Base = System.Security.Cryptography.SHA512.Create();
            byte[] HashA = new byte[32];
            byte[] HashB = new byte[32];
            byte[] LastHash = new byte[64];
            LastHash = SHA512Base.ComputeHash(SHA512Base.ComputeHash(Password));
            while (LongHash.Count < MinLength)
            {
                int cRaunds = _FFxTG_RNG.Next(1,MaxCountRaund);
                for (int i = 0; i < cRaunds; i++)
                {
                    HashA = LastHash.Take(32).ToArray();
                    HashB = LastHash.Skip(32).Take(32).ToArray();
                    LastHash = SHA512Base.ComputeHash(HashB);
                }
                LongHash.AddRange(HashA);
            }
            byte[] arr = LongHash.ToArray();
            Array.Reverse(arr);// переворачиваем хеш задом наперед (в начале хеш более уязвим из за особенностей генерации - в перевернутом виде в комбинации с блочным смешиванием получим хороший результат перемешивания секрета)
            return arr;
        }

        // производит морф байт на подстановочную таблицу генерируется для каждого пароля отдельно
        private byte[] FlipBytesByMorphTable(byte[] bytes, List<byte[]> MorphTable, UInt32 seed)
        {
            FFxTG_RNG _FFxTG_RNG = new FFxTG_RNG(seed);
            List<byte> MorphedArray = new List<byte>();
            for(int i = 0; i < bytes.Count(); i++)
            {
                string text = ((int)bytes[i]).ToString();
                if (text.Length == 1) { text = "00" + text; }
                if (text.Length == 2) { text = "0" + text; }
                foreach (char c in text)
                {
                    MorphedArray.Add(MorphTable[Convert.ToInt32(((char)c).ToString())][_FFxTG_RNG.Next(0,24)]);
                }
            }
            return MorphedArray.ToArray();
        }

        // восстановить морфленные байты в исходный массив байт
        private byte[] RestoreBytesFromMorphed(byte[] MorphedBytes, List<byte[]> MorphTable)
        {
            List<byte> UnMorphedArray = new List<byte>();
            int n = 0;
            string ByteStr = "";
            for (int i = 0; i < MorphedBytes.Count(); i++)
            {
                ByteStr += getNumberFromByteInMTable(MorphedBytes[i], MorphTable).ToString();
                n++;
                if (n == 3)
                {
                    UnMorphedArray.Add((byte)Convert.ToInt32(ByteStr));
                    n = 0;
                    ByteStr = "";
                }
            }
            return UnMorphedArray.ToArray();
        }

        // получает исходный байт из байта найденного в подмножестве таблицы замены
        private int getNumberFromByteInMTable(byte b, List<byte[]> MorphTable)
        {
            for(int i = 0; i< MorphTable.Count();i++)
            {
                foreach (byte bt in MorphTable[i])
                {
                    if(bt == b) { return i; }
                }
            }
            return 0; // -1 выдает ошибку при неверном декдировании
        }

        // классический шифр вернама
        private byte[] VernamChifer(byte[] Message, byte[] Key)
        {
            List<byte> EncryptedMessage = new List<byte>();
            for(int i = 0; i < Message.Count(); i++)
            {
                EncryptedMessage.Add((byte)(Message[i]^Key[i]));
            }
            return EncryptedMessage.ToArray();
        }

        // разделяет массив на блоки
        private List<byte[]> SplitToBlocks(byte[] data, int BlockSize)
        {
            List<byte[]> Blocks = new List<byte[]>();
            List<byte> rawdata = new List<byte>();
            int s = 0;
            int datalength = data.Count()-1;
            while (s < data.Count()) { s += BlockSize; }

            for (int i = 0; i < s; i++)
            {
                if (i <= datalength)
                {
                    rawdata.Add(data[i]);
                }
                else
                {
                    rawdata.Add(0);
                }
                if (rawdata.Count() >= BlockSize)
                {
                    Blocks.Add(rawdata.ToArray());
                    rawdata.Clear();
                }
            }
            return Blocks;
        }

        // перемешивание блоков между собой
        private List<byte[]> RandomSwapBlocks(List<byte[]> Blocks, FFxTG_RNG _FFxTG_RNG)
        {
            Console.WriteLine("Count Blocks:" + Blocks.Count().ToString());
            List<byte[]> _Blocks = Blocks;
            int n = Blocks.Count();
            while (n > 1)
            {
                int k = _FFxTG_RNG.Next(0,n--);
                byte[] temp = _Blocks[n];
                _Blocks[n] = _Blocks[k];
                _Blocks[k] = temp;
            }
            return _Blocks;
        }

        // возрат перемешанных блоков в исходное состояние
        private List<byte[]> DeRandomSwapBlocks(List<byte[]> Blocks, FFxTG_RNG _FFxTG_RNG)
        {
            List<byte[]> _Blocks = Blocks;
            List<RIntPair> RPair = new List<RIntPair>();
            int n = Blocks.Count();
            while (n > 1)
            {
                int k = _FFxTG_RNG.Next(0, n--);
                var pair = new RIntPair();
                pair.k = k;
                pair.n = n;
                RPair.Add(pair);
            }
            int npair = RPair.Count()-1;
            for (int i = npair; i >= 0; i--)
            {
                byte[] temp = _Blocks[RPair[i].n];
                _Blocks[RPair[i].n] = _Blocks[RPair[i].k];
                _Blocks[RPair[i].k] = temp;
            }
            return _Blocks;
        }

        // шифрование дешифрование Блочный морфирующий Линейный алгоритм перетасовывает результат шифрования блоками без связки
        public byte[] BMLinearChipher(byte[] Message, byte[] Password, bool IsEncryptMode, int blockSize = 4, int MaxCountRaund = 512)
        {
            UInt32 seed = GetSeedFromBytesArray(Password);
            FFxTG_RNG _FFxTG_RNG = new FFxTG_RNG(seed);
            // генерация таблицы замен
            List<byte[]> MTable = GetMorphTable(_FFxTG_RNG); // ******
            if (IsEncryptMode)
            {
                // генерируем хеш длинной с морфленное сообщение
                byte[] LongHash = CreateLongHashSHAWithPassword(Password, Message.Count(), _FFxTG_RNG, MaxCountRaund);  // ******
                // нарезаем на блоки и перетасовываем сидом
                List<byte[]> EncodedBlocks = RandomSwapBlocks(SplitToBlocks(Message, blockSize), _FFxTG_RNG); // ******
                 byte[] Encoded =  VernamChifer(UnionBlocksToArray(EncodedBlocks), LongHash);
                // делаем морф исходного сообщения
                return FlipBytesByMorphTable(Encoded, MTable, GetSeedFromBytesArray(Encoding.UTF8.GetBytes(System.Guid.NewGuid().ToString()))); // внутренний рандом не связанный с общим счетчиком
            }
            else
            {
                byte[] DeMorphed = RestoreBytesFromMorphed(Message, MTable);
                // генерируем хеш длинной с морфленное сообщение
                byte[] LongHash = CreateLongHashSHAWithPassword(Password, DeMorphed.Count(), _FFxTG_RNG, MaxCountRaund);  // ******
                byte[] Decoded = VernamChifer(DeMorphed, LongHash);
                // нарезаем на блоки и перетасовываем сидом
                List<byte[]> DecodedMessageBlocks = DeRandomSwapBlocks(SplitToBlocks(Decoded, blockSize), _FFxTG_RNG); // ******
                return UnionBlocksToArray(DecodedMessageBlocks);
            }
        }

        // шифрование дешифрование Блочный Линейный алгоритм перетасовывает результат шифрования блоками без связки
        public byte[] BLinearChipher(byte[] Message, byte[] Password, bool IsEncryptMode, int blockSize = 4, int MaxCountRaund = 512)
        {
            UInt32 seed = GetSeedFromBytesArray(Password);
            FFxTG_RNG _FFxTG_RNG = new FFxTG_RNG(seed);
            if (IsEncryptMode)
            {
                // генерируем хеш длинной с морфленное сообщение
                byte[] LongHash = CreateLongHashSHAWithPassword(Password, Message.Count(), _FFxTG_RNG, MaxCountRaund);  // ******
                // нарезаем на блоки и перетасовываем сидом
                List<byte[]> EncodedBlocks = RandomSwapBlocks(SplitToBlocks(Message, blockSize), _FFxTG_RNG); // ******
                return VernamChifer(UnionBlocksToArray(EncodedBlocks), LongHash);
            }
            else
            {
                // генерируем хеш длинной с морфленное сообщение
                byte[] LongHash = CreateLongHashSHAWithPassword(Password, Message.Count(), _FFxTG_RNG, MaxCountRaund);  // ******
                byte[] Decoded = VernamChifer(Message, LongHash);
                // нарезаем на блоки и перетасовываем сидом
                List<byte[]> DecodedMessageBlocks = DeRandomSwapBlocks(SplitToBlocks(Decoded, blockSize), _FFxTG_RNG); // ******
                return UnionBlocksToArray(DecodedMessageBlocks);
            }
        }

        // шифрование дешифрование Линейный алгоритм только ксор на хеш
        public byte[] LinearChipher(byte[] Message, byte[] Password, bool IsEncryptMode, int MaxCountRaund = 512)
        {
            UInt32 seed = GetSeedFromBytesArray(Password);
            FFxTG_RNG _FFxTG_RNG = new FFxTG_RNG(seed);
            if (IsEncryptMode)
            {
                // генерируем хеш длинной с морфленное сообщение
                byte[] LongHash = CreateLongHashSHAWithPassword(Password, Message.Count(), _FFxTG_RNG, MaxCountRaund);  // ******
                return VernamChifer(Message, LongHash);
            }
            else
            {
                // генерируем хеш длинной с морфленное сообщение
                byte[] LongHash = CreateLongHashSHAWithPassword(Password, Message.Count(), _FFxTG_RNG, MaxCountRaund);  // ******
                return VernamChifer(Message, LongHash);
            }
        }

        // шифрование дешифрование Линейный алгоритм
        // Линейный алгоритм с морфированием сообщения
        public byte[] MLinearChipher(byte[] Message, byte[] Password, bool IsEncryptMode, int MaxCountRaund = 512)
        {
            UInt32 seed = GetSeedFromBytesArray(Password);
            FFxTG_RNG _FFxTG_RNG = new FFxTG_RNG(seed);
            // генерация таблицы замен
            List<byte[]> MTable = GetMorphTable(_FFxTG_RNG); // ******
            if (IsEncryptMode)
            {
                // генерируем хеш длинной с морфленное сообщение
                byte[] LongHash = CreateLongHashSHAWithPassword(Password, (Message.Count() * 3), _FFxTG_RNG, MaxCountRaund);  // ******
                // делаем морф исходного сообщения
                byte[] MorphedMessage = FlipBytesByMorphTable(Message, MTable, GetSeedFromBytesArray(Encoding.UTF8.GetBytes(System.Guid.NewGuid().ToString()))); // внутренний рандом не связанный с общим счетчиком
                return VernamChifer(MorphedMessage, LongHash);
            }
            else
            {
                // генерируем хеш длинной с морфленное сообщение
                byte[] LongHash = CreateLongHashSHAWithPassword(Password, Message.Count(), _FFxTG_RNG, MaxCountRaund);  // ******
                // Декодируем до морфленного сообщения
                byte[] MorphedMessage = VernamChifer(Message, LongHash);
                // делаем ДЕ морф исходного сообщения
                byte[] DeMorphedMessage = RestoreBytesFromMorphed(MorphedMessage.ToArray(), MTable);
                return DeMorphedMessage;
            }
        }

        // шифрование дешифрование CBC режим
        // с регулируемой вычислительной сложностью 1 еденица сложности соотносимо с 1 проходом операции sha512
        // рекомендуемые размеры блоков (чем короче средний размер открытых текстов тем более мелкие блоки рекомендуются, малые размеры блоков улчше скрывают особенности открытого текста) 4, 6, 8, 16, 32, 64
        public byte[] CBCChipher(byte[] Message, byte[] Password, bool IsEncryptMode, int blockSize=16, int MaxCountRaund = 512)
        {
            List<byte> _IV = new List<byte>();// не для прямого использования
            UInt32 seed = GetSeedFromBytesArray(Password);
            FFxTG_RNG _FFxTG_RNG = new FFxTG_RNG(seed);
            // генерация таблицы замен
            List<byte[]> MTable = GetMorphTable(_FFxTG_RNG); // ******
            // генерируем IV по средству RNG
            for (int i = 0; i < blockSize; i++) { _IV.Add((byte)_FFxTG_RNG.Next(0, 256)); } // ******
            byte[] IV = _IV.ToArray();

            if (IsEncryptMode)
            {
                // генерируем хеш длинной с морфленное сообщение
                byte[] LongHash = CreateLongHashSHAWithPassword(Password, (Message.Count() * 3), _FFxTG_RNG, MaxCountRaund);  // ******
                // делаем морф исходного сообщения
                byte[] MorphedMessage = FlipBytesByMorphTable(Message, MTable, GetSeedFromBytesArray(Encoding.UTF8.GetBytes(System.Guid.NewGuid().ToString()))); // внутренний рандом не связанный с общим счетчиком
                // нарезаем на блоки и перетасовываем сидом
                List<byte[]> MessageBlocks = RandomSwapBlocks(SplitToBlocks(MorphedMessage, blockSize), _FFxTG_RNG); // ******
                List<byte[]> LongHashBlocks = SplitToBlocks(LongHash, blockSize);
                List<byte[]> EncryptedBlocks = new List<byte[]>();
                // проход по связке блоков
                for (int i = 0; i < MessageBlocks.Count(); i++)
                {
                    byte[] MessageBlock = MessageBlocks[i];
                    byte[] LongHashBlock = LongHashBlocks[i];
                    byte[] EncodedBlockA = VernamChifer(MessageBlock, IV);
                    byte[] EncodedBlockB = VernamChifer(EncodedBlockA, LongHashBlock);
                    IV = EncodedBlockB;
                    EncryptedBlocks.Add(EncodedBlockB.ToArray());
                }
                return UnionBlocksToArray(EncryptedBlocks);
            }
            else
            {
                byte[] EncryptedMessage = Message;
                // генерируем хеш длинной с морфленное сообщение
                byte[] LongHash = CreateLongHashSHAWithPassword(Password, EncryptedMessage.Count(), _FFxTG_RNG, MaxCountRaund);  // ******
                List<byte[]> LongHashBlocks = SplitToBlocks(LongHash, blockSize);
                List<byte[]> EncryptedMessageBlocks = SplitToBlocks(EncryptedMessage, blockSize);
                List<byte[]> DecryptedBlocks = new List<byte[]>();
                // проход по связке блоков
                for (int i = 0; i < EncryptedMessageBlocks.Count(); i++)
                {
                    byte[] EncryptedMessageBlock = EncryptedMessageBlocks[i];
                    byte[] LongHashBlock = LongHashBlocks[i];
                    byte[] EncodedBlockA = VernamChifer(EncryptedMessageBlock, LongHashBlock);
                    byte[] DecodedBlockB = VernamChifer(EncodedBlockA, IV);
                    IV = EncryptedMessageBlock;
                    DecryptedBlocks.Add(DecodedBlockB.ToArray());
                }
                // нарезаем на блоки и перетасовываем сидом
                List<byte[]> MorphedMessageBlocks = DeRandomSwapBlocks(DecryptedBlocks, _FFxTG_RNG); // ******
                // делаем ДЕ морф исходного сообщения
                byte[] DeMorphedMessage = RestoreBytesFromMorphed(UnionBlocksToArray(MorphedMessageBlocks), MTable);
                return DeMorphedMessage;
            }
        }

        // Преобразовать массив блоков в массив байт
        public byte[] UnionBlocksToArray(List<byte[]> Blocks)
        {
            List<byte> Result = new List<byte>();
            for (int i = 0; i < Blocks.Count(); i++)
            {
                Result.AddRange(Blocks[i]);
            }
            return Result.ToArray();
        }

        public string ToBased64(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
        public byte[] FromBased64(string Based64String)
        {
            return Convert.FromBase64String(Based64String);
        }

        public byte[] GetSHA256(byte[] bytes)
        {
            return SHA256.Create().ComputeHash(bytes);
        }

        public  byte[] HexToByteArray(string _hex,string delimiter=" ")
        {
            string hex = _hex.Trim().Replace(delimiter, "");
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        public string BytesToHex(byte[] bytes, string Delimiter = "")
        {
            StringBuilder builder = new StringBuilder();
            int ln = bytes.Length;
            for (int i = 0; i < ln; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
                if (i < ln-1) { builder.Append(Delimiter); }
            }
            return builder.ToString();
        }
    }
    class RIntPair
    {
        public int k { get; set; }
        public int n { get; set; }
    }
}
