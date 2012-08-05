<?php

require_once('preg_find.inc');

$sig = pack('C*', 0xEF, 0xBB, 0xBF);
$matches = preg_find('/\.cs$/', '..', PREG_FIND_RECURSIVE);
foreach ($matches as $f) {
	//echo "$f\n";
	$t = file_get_contents($f);
	$n = filesize($f);
	$v = iconv('GB2312', 'UTF-8', $t);
	if ($n > strlen($v)) {
		$x = strlen($v);
		echo $f." convert failed. o($n) c($x)\n";
		file_put_contents($f.'.u8', $v);
	}
	else {
		file_put_contents($f, $sig.$v);
	}
}
?>