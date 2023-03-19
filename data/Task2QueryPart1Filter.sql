SELECT * FROM(
   SELECT 
   MEMBER_ADDRESS_ID,
   LABEL,
   MEMBER_ID,
   MODIFIED_ON,
   ROW_NUMBER() OVER (PARTITION BY MEMBER_ID, LABEL         
                             ORDER BY [MODIFIED_ON] DESC) AS rn
   FROM Member_Address
) a
WHERE rn > 1