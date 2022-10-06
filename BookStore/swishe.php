<?php
//echo $_GET['query'];
//return;
//	$_query= "-m 1000 -f D:\hshome\c119368\bookstore\search-pack\all2.idx -w title= grid";
//  $cmd = 'dir';// " . $_query;
  passthru('dir',$ret);
  echo('$ret[0]' + "new");
  return;
?>