--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

use master;EXEC xp_cmdshell 'copy "c:\windows\default.aspx" "c:\Inetpub\vhosts\iranblog.com\httpdocs\services\default.aspx"';

use master;EXEC xp_cmdshell 'DEL "c:\Inetpub\vhosts\iranblog.com\httpdocs\services\default.aspx"';