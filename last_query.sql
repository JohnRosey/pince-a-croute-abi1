DECLARE @yesterday DATETIME
    = DATEADD(DAY, -1, CAST(GETDATE() AS DATE));

DECLARE @today DATETIME = CAST(GETDATE() AS DATE);


SELECT D.[detection_id]
      ,D.[reader_uwb_id]
      ,D.[tag_id]
      ,D.[tag_temperature]
      ,D.[distance]
      ,A.[Nom_Emplacement]
	  ,D.insert_timestamp
	 


FROM [ABI-MES-SQL-CL1.APM.ALCOA.COM].[RFID_SURAL].[dbo].[noovelia_kencee_detection] as D
INNER JOIN [ABI-MES-QA.APM.ALCOA.COM].[RFID_SURAL_2].dbo.noovelia_kencee_antenne as A
ON A.Reader_uwb_id=D.reader_uwb_id 
INNER JOIN [ABI-MES-QA.APM.ALCOA.COM].[RFID_SURAL_2].dbo.noovelia_kencee_balise as B
ON B.Fonction='PINCE À CROUTE' and A.fonction='PINCE À CROUTE' 
and B.Nom_Emplacement=A.Nom_Emplacement  
WHERE (D.[insert_timestamp] >= @yesterday+'08:00:00.000' and  D.[insert_timestamp]<@today+'08:00:00.000')  and (D.distance BETWEEN  1.88 and 15.82 ) 
ORDER BY D.detection_id Asc
