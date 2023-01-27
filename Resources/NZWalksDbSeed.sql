SET IDENTITY_INSERT Regions ON;
INSERT INTO Regions (Id, Code, Name, Area, Lat, Long, Population) 
VALUES (1, 'NRTHL', 'Northland Region', 13789, -35.3708304, 172.5717825, 194600);
INSERT INTO Regions (Id, Code, Name, Area, Lat, Long, Population) 
VALUES (2, 'AUCK', 'Auckland Region', 4894, -36.5253207, 173.7785704, 1718982);
INSERT INTO Regions (Id, Code, Name, Area, Lat, Long, Population) 
VALUES (3, 'WAIK', 'Waikato Region', 8970, -37.5144584, 174.5405128, 496700);
INSERT INTO Regions (Id, Code, Name, Area, Lat, Long, Population) 
VALUES (4, 'BAYP', 'Bay Of Plenty Region', 12230, -37.5328259, 175.7642701, 345400);
SET IDENTITY_INSERT Regions OFF;

SET IDENTITY_INSERT WalkDifficulty ON;
INSERT INTO WalkDifficulty (Id, Code)
VALUES (1, 'Easy');
INSERT INTO WalkDifficulty (Id, Code)
VALUES (2, 'Medium');
INSERT INTO WalkDifficulty (Id, Code)
VALUES (3, 'Hard');
SET IDENTITY_INSERT WalkDifficulty OFF;


SET IDENTITY_INSERT Walks ON;
INSERT INTO Walks (Id, Name, Length, WalkDifficultyId, RegionId)
VALUES (1, 'Waiotemarama Loop Track', 1.5 , 2, 1);
INSERT INTO Walks (Id, Name, Length, WalkDifficultyId, RegionId)
VALUES (2, 'Mt Eden Volcano Walk', 2 , 1, 2);
INSERT INTO Walks (Id, Name, Length, WalkDifficultyId, RegionId)
VALUES (3, 'One Tree Hill Walk', 3.5 , 1, 2);
INSERT INTO Walks (Id, Name, Length, WalkDifficultyId, RegionId)
VALUES (4, 'Lonely Bay', 1.2 , 1, 3);
INSERT INTO Walks (Id, Name, Length, WalkDifficultyId, RegionId)
VALUES (5, 'Mt Te Aroha To Wharawhara Track Walk', 32 , 3, 4);
INSERT INTO Walks (Id, Name, Length, WalkDifficultyId, RegionId)
VALUES (6, 'Rainbow Mountain Reserve Walk', 3.5 , 2, 4);
SET IDENTITY_INSERT Walks OFF;
