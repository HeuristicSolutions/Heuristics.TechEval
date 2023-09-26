
-- The Join & Window function queries cover Task 6.1
-- Commenting out SELECT and uncommenting DELETE covers Task 6.2
-- The creation of a unique index covers Task 6.3

--DELETE Query using Window Functions--
--DELETE FROM dbo.MEMBER_ADDRESS
SELECT * FROM dbo.MEMBER_ADDRESS
WHERE MEMBER_ADDRESS_ID IN (
	-- Get the member address IDs that need to be deleted
	SELECT MEMBER_ADDRESS_ID
	FROM (
		-- Number the member address IDs such that 1 is the newest address record in a member's label, and N is the oldest
		SELECT 
			MEMBER_ADDRESS_ID, 
			ROW_NUMBER() OVER(PARTITION BY MEMBER_ID, LABEL ORDER BY MODIFIED_ON DESC) AS ROW_NUM
		FROM dbo.MEMBER_ADDRESS
	) AS T
	WHERE T.ROW_NUM > 1
)
;

--DELETE Query using JOINS--
--DELETE FROM dbo.MEMBER_ADDRESS
SELECT * FROM dbo.MEMBER_ADDRESS 
WHERE MEMBER_ADDRESS_ID IN (
	-- Get the member address IDs to delete because their timestamp is not the latest modified timestamp
	SELECT A.MEMBER_ADDRESS_ID
	FROM dbo.MEMBER_ADDRESS AS A
	LEFT JOIN (
		-- Get the last modified timestamp for a member's address label
		SELECT MEMBER_ID, LABEL, MAX(MODIFIED_ON) AS LAST_MODIFIED
		FROM dbo.MEMBER_ADDRESS
		GROUP BY MEMBER_ID, LABEL
	) AS LM ON A.MEMBER_ID = LM.MEMBER_ID AND A.LABEL = LM.LABEL AND A.MODIFIED_ON = LM.LAST_MODIFIED
	WHERE LM.LAST_MODIFIED IS NULL);

-- Creates a unique index that uses MEMBER_ID & LABEL to prevent duplicate records.
CREATE UNIQUE INDEX label_unique_index
ON dbo.MEMBER_ADDRESS (MEMBER_ID, LABEL)
;


-- Test index

-- A Query that looks for the specified ID. Two records with unique addresses should show.
SELECT * 
FROM dbo.MEMBER_ADDRESS
WHERE MEMBER_ID = 1454
;

-- Adding a new member address. Should Add the value. If ran a second time the value should not be added due to the Unique Index rules.
INSERT INTO dbo.MEMBER_ADDRESS 
(MEMBER_ID, LABEL, STREET_1, CITY, STATE, ZIP, CREATED_ON, MODIFIED_ON) 
VALUES 
(1454, 'Work', '279 E Saratoga St', 'Ferndale', 'MI', '48220', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
;
