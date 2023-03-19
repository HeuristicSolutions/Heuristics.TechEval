;WITH cte
  AS( SELECT
   ROW_NUMBER() OVER (PARTITION BY MEMBER_ID, LABEL         
                             ORDER BY [MODIFIED_ON] DESC) AS rn
   FROM Member_Address) 
DELETE FROM cte
WHERE rn > 1;