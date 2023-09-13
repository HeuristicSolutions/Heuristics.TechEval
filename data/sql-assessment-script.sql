-- get the data we want to delete and put it into a temp table
SELECT 
  member_id, 
  Max(modified_on) modified_on INTO #mytemptable
FROM 
  member_address 
GROUP BY 
  member_id, 
  label 
HAVING 
  Count(label) > 1 
  AND Count(member_id) > 1

-- do a join in order to get the member_address_id (used in subquery below)
SELECT 
  member_address_id 
FROM 
  member_address ma 
  JOIN #mytemptable mtt
  ON ma.member_id = mtt.member_id 
  AND ma.modified_on = mtt.modified_on

-- delete data using the above subquery
DELETE FROM 
  member_address 
WHERE 
  member_address_id IN (
    SELECT 
      member_address_id 
    FROM 
      member_address ma 
      JOIN #mytemptable mtt
      ON ma.member_id = mtt.member_id 
      AND ma.modified_on = mtt.modified_on
  )
