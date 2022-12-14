--	All rights reserved to Alireza Poshtkohi (c) 1999-2023.
--	Email: arp@poshtkohi.info
--	Website: http://www.poshtkohi.info

USE [msdbb]


/*UPDATE bo SET bo.Title=bo1.Title,bo.EnglishCategory=bo1.EnglishCategory FROM books AS bo,books1 AS bo1
				WHERE bo1.ISBN=bo.ISBN;*/

UPDATE books SET Writer='' WHERE Writer='AUTHOR:NaN';
UPDATE books SET Writer='' WHERE Writer='AUTHORS:NaN';
UPDATE books SET ISBN='' WHERE ISBN='LANGUAGE:NaN';
UPDATE books SET Title='' WHERE Title='TITLE:NaN';
UPDATE books SET IsDeleted=1 WHERE ISBN='';