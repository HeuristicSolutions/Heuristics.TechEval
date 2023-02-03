/* 
	SUB-TASK 1
	Identify the Members with duplicate addresses 
*/
SELECT
    [MEMBER_ID]
    ,[LABEL]
	,COUNT(*)
FROM [SqlAssessment].[dbo].[MEMBER_ADDRESS]
GROUP BY [MEMBER_ID], [LABEL]
HAVING COUNT(*) > 1

/*
	SUB-TASK 2
	Delete duplicate data that violates the business rule. 
	For each label with multiple addresses, keep the record with the most recent MODIFIED_ON date.
	----------------------------------------------------------------------------------------------
	[MEMBER_ADDRESS_ID] TO DELETE (Count 27)
	   1,    5, 1036, 1037, 1038, 1039, 1040, 1041, 1042, 1043,
	1044, 1045, 1046, 1047, 1048, 1049, 1050, 1051, 1052, 1053,
	1054, 1055, 1056, 1057, 1058, 1059, 1060
	-----------------------------------------------------------
	Since this is a small data set I would have prefered to use the IDs above to execute the delete statement, that way everyone knows what ids were deleted.
	But that wouldn't tell you much about how I code/think.
	So, the below statement will handle multiples and could be ran again.
	A couple drawbacks on the below statement is:
		1) Depending on the size of the duplication it could take a while
		2) Also, Depending on the sise of the data you may have an issue with live data (if the user deletes while the transaction is running)
		3) It may be harder to track down failures
	Other options would be batch loading the [MEMBER_ADDRESS_ID] into a cursour and itterating over that in order to "log" what batches complete and any that have an issue 
*/

BEGIN TRANSACTION
	DELETE FROM [SqlAssessment].[dbo].[MEMBER_ADDRESS]
	WHERE [MEMBER_ADDRESS_ID] IN(
		SELECT count_table.[MEMBER_ADDRESS_ID] FROM
			(SELECT 
			ROW_NUMBER() OVER(PARTITION BY main_table.[MEMBER_ID], main_table.[LABEL] ORDER BY main_table.[MODIFIED_ON] DESC) AS 'ROW_NUM', -- This allows us to keep only the most recent
			main_table.[MEMBER_ADDRESS_ID]
			FROM [SqlAssessment].[dbo].[MEMBER_ADDRESS] main_table
			INNER JOIN -- For the join we can use what we wrote above to identify the duplication, but this will only limit to the [MEMBER_ID] and [LABEL] and not have any [MODIFIED_ON] information
				(SELECT [MEMBER_ID], [LABEL]
				FROM [SqlAssessment].[dbo].[MEMBER_ADDRESS]
				GROUP BY [MEMBER_ID], [LABEL]
				HAVING COUNT(*) > 1) join_table ON join_table.[MEMBER_ID] = main_table.[MEMBER_ID] AND join_table.[LABEL] = main_table.[LABEL]
			) count_table
		WHERE count_table.[ROW_NUM] > 1); -- If there was more than one duplicate per line it would be removed as well
--ROLLBACK TRANSACTION;
COMMIT;



/*
	SUB-TASK 3
	Create a unique index preventing duplicates from being created in the future.
	------------------------------------------------------------------------------
	This will generate a Clustered Index and since there is no other indexes there is no reason not to use a Nonclustered Index
	My Naming convention is UI = UniqueIndex and then the columns in the index (and while it is not needed to be a unique name for the while DB it may make it easier if we add the table name as well)
	Whatever the standard is, it would be best to continue the pattern
*/

ALTER TABLE [SqlAssessment].[dbo].[MEMBER_ADDRESS]  
ADD CONSTRAINT UI_MEMBER_ID_LABEL UNIQUE ([MEMBER_ID], [LABEL]);  
