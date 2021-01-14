# LHXORwCD 

(Long Hash XOR With Control Difficality Chiper)
================================================================================================================================================================================

Симметричный алгоритм шифрования, основанный на операциях Hash и XOR при этом Hash функция выступает в роли генератора непредсказуемого шума в качестве ключа симметричного длине сообщения для последующей операции XOR. А так же алгоритм снабжен механизмом контроля вычислительной сложности, что позволяет гибко настраивать алгоритм на быстроту работы или же наоборот на усложнение возможности атаки подбором парольной фразы, реализации так же присутствует несколько режимов реализованных на базе этого линейного шифра.
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## Основные особенности симметричным алгоритма шифрования LHXORwCD: ##
0 - Алгоритм принимает и выдает результат сырыми байтами и является полностью бинарным, а значит с его помощью можно кодировать любую информацию в том числе и бинарные файлы.

1 - Алгоритм является симметричным и генерирует длинный Hash из массива байт парольной фразы многократным делением результата хеширования SHA512 пополам и использования правой части для генерации следующей последовательности хеша и левая часть используется как выход функции для генерации ключа длинной кратной длине сообщения по 32 байта за шаг, по окончании генерации ключ инвертируется.

2 - Алгоритм имеет (Защиту от полного перебора) контроль скорости вычисления Long Hash ключа на основе функции нелинейного количества повторений каждого из раундов хеширования за счет реализованного с нуля ГПСЧ (FFxTG_RNG) не имеющего периодов ослабления последовательности рандомизатора, сложность вычисления хеша задается максимальным числом раундов которое может выдать ГПСЧ (FFxTG_RNG) для каждого из раундом.

3 - Алгоритм умеет генерировать уникальный seed на основе переданного массива байт (парольной фразы) что влияет в итоге как на распределение раундов при создании ключа хеш функцией и как следствие значительно усложняет анализ.

4 - В результате описанных выше алгоритмов удалось добиться полной повторяемости генерируемого ключа лиш на основе введенной парольной фразы, при этом распределение байт в длинном ключе является полностью рандомным шумом. 

5 -  в заключении к базовому алгоритму после генерации ключа из парольной разы производится XOR байты открытого текста, в результате чего получается зашифрованное парольной фразой сообщение неотличимое от обычного шума с энтропией по Шеннону в 7.964810873916366 и на графике имеет плавную практически прямую линию энтропии, а значит в совокупности с шифром Вернама (XOR) является идеальным одноразовым блокнотом для кодирования, что гарантирует абсолютную крипто стойкость уже на базовом этапе (однако для противодействию различным техникам анализа было разработано несколько режимов эффективного кодирования).

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
## Основные режимы работы алгоритма шифрования LHXORwCD: ##

1 -  Линейное кодирование LinearChipher (Базовая реализация алгоритма без каких либо режимов, позволяет добиться высокой энтропии, но шифрование в этом режиме является единообразным с повторяемой комбинацией байт на выходе.).

    - из минусов:
        * При кодировании идентичных данных одним и тем же ключем выходной шифротекст не изменяется,
        что не очень хорошо, данное свойство позволит стороннему наблюдателю заметить колличество
        и позиции изменившихся бит при повторной отправки двух похожих сообщений закодированных 
        одним и тем же ключем, что может в долгосрочной перспективе  раскрыть по особенностям 
        изменений суть набираемых сообщений.
        
    - из плюсов:
        * все плюсы перечисленные в пункте 0 - (Линейное кодирование)
        * этап шифрования занимает нелинейное время за счет чего исключается тайминг атака на парольную фразу.
        * При изменении парольной фразы даже на 1 бит весь результат криптографической функции будет
        другим (шифротекст изменится полностью при неизменном исходном сообщении).
        
2 - Блочное линейное кодирование BLinearChipher (В основе алгоритма лежит линейное кодирование, но с добавлением блоков перемешиваемых в процессе, что позволяет частично скрыт особенности языка. )
    Исходное сообщение кодируется линейным алгоритмом после чего разделятся на блоки регулируемой длинны (для небольших сообщений рекомендуется ставить более мелкие размеры блока), после чего FFxTG_RNG (инициализируемый сидом от парольной фразы) производит перестановки блоков выходного шифротекста.

    - из минусов:
        * шифротекст не изменяется при повторном кодировании (возможность анализа изменений данных)
        * применение блочного перемешивания только частично скрывает факты модификации данных в повторяемых 
        сообщениях на одном ключе все еще будут видны изменения частей байт (что будет тем заметнее чем больше 
        используемый размер блока т.к. более короткие блоки хорошо перетасует короткие фразы 
        содержащиеся в открытом тексте.)
   
    - из плюсов:
        *  все плюсы перечисленные в пункте 1 - (Линейное кодирование)
        *  частичное сокрытие особенностей языка за счет перестановки выходных бит.
        
 3 - Линейное полиморфное кодирование MLinearChipher (в основе лежит базовое линейное кодирование)
    Исходное сообщение морфируется по специальной подстановочной таблице которая создается на основе seed и FFxTG_RNG генератора 
    (таблица имеет 10 колонок по 25 элементов каждая из которых заполняется случайными неповторяющимися в других колонках байтами от 0-256) после чего байты исходного поэлементно ищутся как номера колонок в таблице замен и рандомно выбирается байт из столбца соответствующему 1, 2, и 3 му элементу в байте шифротекста замещая в итоге 1 настоящий байт шифротекста на 3 рандомных сгенерированных на основе сида от пароля. В результате данной операции любое количество повторных шифрований одних и тех же данных с одним и тем же ключем дают полное изменение выходных байт шифротекста до неразличимого шума.
    
    - из минусов:
        * все еще не исключается возможность анализа линейным и дифференциальными методами.
        * требует больше затрат ресурсов компьютера.
        * объём шифротекста увеличивается 3 к 1.
    
    - из плюсов:
        * все плюсы перечисленные в пункте 1 - (Линейное кодирование)
        * полностью исключается анализ особенностей языка.
        * повторное шифрование приводит полному изменению шифротекста.
        * график энтропии выглядит как нелинейный шум.
 
 4 - Линейное Блочно полиморфное кодирование BMLinearChipher (в основе лежит Линейное полиморфное кодирование)
    В добавок к морфированию сообщения добавляется блочная перестанока выходного шифротекста 
    (размеры блоков влияют на качество перемешивания чем меньше размер блока тем больше блоков в итоге получится - для коротких сообщений рекомендуется устанавливать размеры блока от 2-8 ) 
    
    - из минусов:
        * все еще не исключается возможность анализа дифференциальным методом.
        * требует больше затрат ресурсов компьютера.
        * объём шифротекста увеличивается 3 к 1.
    
    - из плюсов:
        * все плюсы перечисленные в пункте 3 - (Линейное полиморфное кодирование)
        * За счет блочной перестановки становится невозможен линейный анализ.
        * полностью исключается анализ особенностей языка.
        * повторное шифрование приводит полному изменению шифротекста.
        * график энтропии выглядит как нелинейный шум.
        
 5 - Линейное Блочное CBC полиморфное кодирование со связыванием  BMCBCChipher (в основе лежит Линейное Блочно полиморфное кодирование)
    Изначально генерируется seed из парольной фразы подаваемый на FFxTG_RNG
    (экземпляр которого создается 1 раз и используется на всех последующих этапах (нарушение при декодировании итерации рандома хотя бы на 1 сводит всю расшифровку на нет), этот факт значительно усложняет попытки слепой атаки, на основе предыдущего алгоритма строится морф таблица, открытый текст морфится после чего разлагается вместе с ключем Long Hash на блоки указанного размера, производится их перемешивание входным Seed парольной фразы) после чего генерируется IV в размер блока (в реализации данного алгоритма размер блоков рекомендуется ставить в диапазоне от 8 - 16  (слишком короткие блоки снижают крипто стойкость в режиме связывания блоков получается низкая энтропия)),  производится XOR операция с морфленным сообщением, а после с Lhash соответствующим блоком ключа, результат шифрования записывается в массив и устанавливается как IV для следующей итерации по всем блокам ( CBC режим ). В результате на выходе получается высокоэнтропийная шумовая сглаженная последовательность, на конце имеется характерный для блочного наложения скат не превышающий 10% от общего колебания графика (данный факт не позволяет поанализироват шифротекст как линейным так и дифференциальными методами - фактический шум в гистограмме).
    
    - из минусов:
        * требует больше затрат ресурсов компьютера.
        * объём шифротекста увеличивается 3 к 1.
    
    - из плюсов:
        * все плюсы перечисленные в пункте 4 - (Линейное Блочно полиморфное кодирование)
        * За счет блочного связывания становится невозможен линейный и дифференциальный анализ.
        * График энтропии выглядит как более сглаженный шум с уменьшением энтропии в конце 
        (за счет этого сводятся на не любые попытки анализа - выходные данные являются кашей из на ложившихся 
        друг на друга предшествующих блоков)
        

 ## Примеры реализации кода на базе разработанного класса с методами шифрования ##
```csharp
    byte[] Message = Encoding.UTF8.GetBytes("кодируемое сообщение");
    byte[] Password = Encoding.UTF8.GetBytes("парольная фраза");
    int BlockSize = 16;
    int MaxDifficallity = 256;
    Console.WriteLine("Original text:");
    Console.WriteLine(Encoding.UTF8.GetString(Message));

    LHXORwCD _LHXORwCD = new LHXORwCD();
    // LinearChipher алгоритм
    byte[] EncryptedС = _LHXORwCD.LinearChipher(Message, Password, true, MaxDifficallity);
    Console.WriteLine("EnCrypted: " + _LHXORwCD.BytesToHex(EncryptedС, " "));
    byte[] DecryptedС = _LHXORwCD.LinearChipher(EncryptedС, Password, false, MaxDifficallity);
    Console.WriteLine("DeCrypted: " + Encoding.UTF8.GetString(DecryptedС));

    // BLinearChipher алгоритм
    byte[] EncryptedD = _LHXORwCD.BLinearChipher(Message, Password, true, BlockSize, MaxDifficallity);
    Console.WriteLine("EnCrypted: " + _LHXORwCD.BytesToHex(EncryptedD, " "));
    byte[] DecryptedD = _LHXORwCD.BLinearChipher(EncryptedD, Password, false, BlockSize, MaxDifficallity);
    Console.WriteLine("DeCrypted: " + Encoding.UTF8.GetString(DecryptedD));

    // MLinearChipher алгоритм
    byte[] EncryptedB = _LHXORwCD.MLinearChipher(Message, Password, true, MaxDifficallity);
    Console.WriteLine("EnCrypted: " + _LHXORwCD.BytesToHex(EncryptedB, " "));
    byte[] DecryptedB = _LHXORwCD.MLinearChipher(EncryptedB, Password, false, MaxDifficallity);
    Console.WriteLine("DeCrypted: " + Encoding.UTF8.GetString(DecryptedB));

    // BMLinearChipher алгоритм
    byte[] EncryptedE = _LHXORwCD.BMLinearChipher(Message, Password, true, BlockSize, MaxDifficallity);
    Console.WriteLine("EnCrypted: " + _LHXORwCD.BytesToHex(EncryptedE, " "));
    byte[] DecryptedE = _LHXORwCD.BMLinearChipher(EncryptedE, Password, false, BlockSize, MaxDifficallity);
    Console.WriteLine("DeCrypted: " + Encoding.UTF8.GetString(DecryptedE));

    // BMCBCChipher алгоритм
    byte[] Encrypted = _LHXORwCD.BMCBCChipher(Message, Password, true, BlockSize, MaxDifficallity);
    Console.WriteLine("EnCrypted: " + _LHXORwCD.BytesToHex(Encrypted, " "));
    byte[] Decrypted = _LHXORwCD.BMCBCChipher(Encrypted, Password, false, BlockSize, MaxDifficallity);
    Console.WriteLine("DeCrypted: " + Encoding.UTF8.GetString(Decrypted));
    ```
