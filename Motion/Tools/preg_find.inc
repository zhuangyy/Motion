<?
/*
* Find files in a directory matching a pattern
*
*
* Paul Gregg <pgregg@pgregg.com>
* 20 March 2004,  Updated 20 April 2004
*
* Open Source Code:   If you use this code on your site for public
* access (i.e. on the Internet) then you must attribute the author and
* source web site: http://www.pgregg.com/projects/php/code/preg_find.phps
* Working example: http://www.pgregg.com/projects/php/code/preg_find_ex.phps
*
*/

define('PREG_FIND_RECURSIVE', 1);
define('PREG_FIND_DIRMATCH', 2);
define('PREG_FIND_FULLPATH', 4);
define('PREG_FIND_NEGATE', 8);
define('PREG_FIND_DIRONLY', 16);
define('PREG_FIND_RETURNASSOC', 32);

// PREG_FIND_RECURSIVE   - go into subdirectorys looking for more files
// PREG_FIND_DIRMATCH    - return directorys that match the pattern also
// PREG_FIND_DIRONLY     - return only directorys that match the pattern (no files)
// PREG_FIND_FULLPATH    - search for the pattern in the full path (dir+file)
// PREG_FIND_NEGATE      - return files that don't match the pattern
// PREG_FIND_RETURNASSOC - Instead of just returning a plain array of matches,
//                         return an associative array with file stats
// to use more than one simply seperate them with a | character



// Search for files matching $pattern in $start_dir.
// if args contains PREG_FIND_RECURSIVE then do a recursive search
// return value is an associative array, the key of which is the path/file
// and the value is the stat of the file.
function preg_find($pattern, $start_dir='.', $args=NULL) {


  $files_matched = array();

  $fh = opendir($start_dir);

  while (($file = readdir($fh)) !== false) {
    if (strcmp($file, '.')==0 || strcmp($file, '..')==0) continue;
    $filepath = $start_dir . '/' . $file;
    if (preg_match($pattern,
                   ($args & PREG_FIND_FULLPATH) ? $filepath : $file)) {
      $doadd =    is_file($filepath)
               || (is_dir($filepath) && ($args & PREG_FIND_DIRMATCH))
               || (is_dir($filepath) && ($args & PREG_FIND_DIRONLY));
      if ($args & PREG_FIND_DIRONLY && $doadd && !is_dir($filepath)) $doadd = false;
      if ($args & PREG_FIND_NEGATE) $doadd = !$doadd;
      if ($doadd) {
        if ($args & PREG_FIND_RETURNASSOC) { // return more than just the filenames
          $fileres = array();
          if (function_exists('stat')) {
            $fileres['stat'] = stat($filepath);
            $fileres['du'] = $fileres['stat']['blocks'] * $fileres['stat']['blksize'] / 8;
          }
          if (function_exists('fileowner')) $fileres['uid'] = fileowner($filepath);
          if (function_exists('filegroup')) $fileres['gid'] = filegroup($filepath);
          if (function_exists('filetype')) $fileres['filetype'] = filetype($filepath);
          if (function_exists('mime_content_type')) $fileres['mimetype'] = mime_content_type($filepath);
          if (function_exists('dirname')) $fileres['dirname'] = dirname($filepath);
          if (function_exists('basename')) $fileres['basename'] = basename($filepath);
          if (isset($fileres['uid']) && function_exists('posix_getpwuid ')) $fileres['owner'] = posix_getpwuid ($fileres['uid']);
          $files_matched[$filepath] = $fileres;
        } else
          array_push($files_matched, $filepath);
      }
    }
    if ( is_dir($filepath) && ($args & PREG_FIND_RECURSIVE) ) {
      $files_matched = array_merge($files_matched,
                                   preg_find($pattern, $filepath, $args));
    }
  }

  closedir($fh);
  return $files_matched;

}

?>
