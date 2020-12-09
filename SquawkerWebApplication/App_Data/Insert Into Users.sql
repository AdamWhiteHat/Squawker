INSERT INTO [Users] 
([AspNetUserId],[CreationDate],[TimezoneOffset],[UserName],[Email],[FirstName],[Surname]) 
VALUES
('01bbfc93-d1c1-4e27-b55d-72a26f98fd7f',GETUTCDATE(),CAST(SYSDATETIMEOFFSET() AS datetimeoffset(0)),'Adaministrator','arakaska@gmail.com','Adam','White')


INSERT INTO [Squawks]
([UserId],[CreationDate],[Content],[Latitude],[Longitude])
VALUES
('1',GETUTCDATE(),'All lines are really just circles with infinite radius. Change my mind.',39.657081,-104.868080)

