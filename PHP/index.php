<?
namespace LHORwCD;
//require_once 'FFxTG_RNG.php';
require_once 'FFxTG_RNG_32.php';
echo "<pre>";
$rng =  new FFxTG_RNG_32(array(131,44,21,44));
for ($i = 0; $i < 50; $i++)
{
    echo $rng->Next_interval(0,1000000000000) . "<br>";
}
echo "</pre>";

?>