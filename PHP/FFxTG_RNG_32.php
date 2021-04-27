<?
namespace LHORwCD;

class bytesConverter
{
    // массив байт в строку
    public static function BytesArrayToString($bytes = array()){
        $arr = array_map("chr", $bytes);
        return implode($arr);
    }

    // преобразуем UINT в массив байт
    public static function INTToBytes($uint){
        return unpack("C*", pack("L", $uint));
    }
}

class FFxTG_RNG_32
{
    public $seed;
    public  function __construct($uintSeed) {
        $this->seed = $uintSeed;//bytesConverter::BytesArrayToString($uintSeed); // преобразуем UINT в массив байт
    }

    public  function SetSeed($_seed) {
        $this->seed = bytesConverter::BytesArrayToString($_seed); // преобразуем UINT в массив байт
    }
    public function Next()
    {
        $crcBytes     = bytesConverter::INTToBytes(crc32(bytesConverter::BytesArrayToString($this->seed)));   // crc32 от сида 4 байта
        $First4Bytes  = bytesConverter::INTToBytes(crc32(bytesConverter::BytesArrayToString([$crcBytes[1],$crcBytes[2]]))); // 2 первых байта от сида в crc32 = 4 байта
        $this->seed   = bytesConverter::INTToBytes(crc32(bytesConverter::BytesArrayToString([$crcBytes[3],$crcBytes[4]]))); // 2 послдних байта от сида  в crc32 = 4 байта
        $crc = intval($First4Bytes[1]);
        $crc = $crc << 8;
        $crc += $First4Bytes[2];
        $crc = $crc << 8;
        $crc += $First4Bytes[3];
        $crc = $crc << 8;
        $crc += $First4Bytes[4];
        $crc = $crc << 8;
        return ($crc / 4294967295) - intval($First4Bytes[1]);
    }

    public function Next_interval($min, $max)
        {
            $x = $this->Next();
            return (int)(($max - $min) * $x + $min);
        }
}
?>