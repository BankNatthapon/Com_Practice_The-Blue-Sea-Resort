Delete Customer
Delete Booking
Select * from Customer
Select * from Booking
Select * from RoomType
Select * from Room
UPDATE Booking set Bk_SerialCode = '304293'
SELECT Booking.Bk_SerialCode,Customer.Cus_Id,Customer.Name,Customer.Tel as Phone,Customer.Email,CONVERT(varchar(11),Booking.Bk_Check_In,103) as CheckIn,CONVERT(varchar(11),Booking.Bk_Check_Out,103) as CheckOut,Booking.Bk_Night as Night,Booking.Bk_Total_Price as Price,Booking.Bk_Total_Room as TotalRoom,Booking.Room_Id FROM Customer 
INNER JOIN Booking ON Customer.Cus_Id = Booking.Cus_Id
INNER JOIN Room ON Booking.Room_Id = Room.Room_Id
INNER JOIN RoomType ON Room.Type_Id = RoomType.Type_Id

insert into Customer Values ('Thanawat pinya','0841776616','aom_joeyl3oy@hotmail.com')
insert into Booking Values (1,15/08/2559,18/08/2559,2,2600,1,1)
insert into BookDetail Values (1,1,1)
insert into Room Values (3,103,1,1)
insert into RoomType  Values ('Garden Room',1)

insert into Customer (Name,Tel,Email) Values ('thanawat','0841776616','Aom@hotmail.com')
insert into Booking (Bk_SerialCode,Bk_Check_In,Bk_Check_Out,Bk_Total_Room,Cus_id) Values ('854034','2550-01-01','2550-01-13',1,8)
insert into BookDetail (Bk_Id) select Bk_Id from Booking where Cus_Id = 8
Update Booking set Room_Id = (select TOP 1 Room_Id from Room where Room_Id not in (select Room_Id from Booking where Bk_Check_In >= '2550-02-01' and Bk_Check_Out <= '2550-02-23') and Room_Status = 0 and Type_Id = 1) where Cus_Id = 8

select * from Booking where Bk_Check_In >= '2550-01-01' and Bk_Check_Out <= '2550-01-31'
select TOP 1 Room_Id from Room where Room_Id not in (select Room_Id from Booking where Bk_Check_In >= CONVERT(datetime,'12/02/2016',103) and Bk_Check_Out <= CONVERT(datetime,'13/02/2016',103)) and Room_Status = 0 and Type_Id = 1
select TOP 1 Cus_Id from Customer ORDER BY Cus_Id DESC

insert into Customer (Name,Tel,Email)values ('Ohm','0841776616','Aom_joryl3oy@hotmai.com') insert into Booking (Bk_SerialCode,Bk_Check_In,Bk_Check_Out,Bk_Total_Room,Cus_id) values ('998858',CONVERT(datetime,'12/02/2016',103),CONVERT(datetime,'13/02/2016', 103),1,(select TOP 1 Cus_Id from Customer ORDER BY Cus_Id DESC)) Update Booking set Room_Id = 1 where Cus_Id = (select TOP 1 Cus_Id from Customer ORDER BY Cus_Id DESC)

SELECT Booking.Bk_SerialCode,Customer.Cus_Id,Customer.Name,Customer.Tel as Phone,Customer.Email,CONVERT(varchar(11),Booking.Bk_Check_In,103) as CheckIn,CONVERT(varchar(11),Booking.Bk_Check_Out,103) as CheckOut,Booking.Bk_Night as Night,Booking.Bk_Total_Price as Price,Booking.Bk_Total_Room as TotalRoom,RoomType.Type_Name,Room.Room_Number FROM Customer INNER JOIN Booking ON Customer.Cus_Id = Booking.Cus_Id INNER JOIN Room ON Booking.Room_Id = Room.Room_Id INNER JOIN RoomType ON Room.Type_Id = RoomType.Type_Id where Booking.Bk_SerialCode like '%%' and Customer.Name like '%Neoy%'

insert into RoomType ([Type_Name],[Type_Price],[Type_Detail],[Room_Amount],[Type_Pic]) values('', '', 'hgdfgh','0',  '/Content/Images/Room/room-beach-pool-villa01.jpg')

select COUNT(Room_Id) from Room where Type_Id = 1
Update RoomType set Room_Amount = (select COUNT(Room_Id) from Room where Type_Id = 1) where Type_Id = 1

select * from RoomType where Type_Id = (select Type_Id from Room where Room_Id not in (select Room_Id from Booking where Bk_Check_In >= CONVERT(datetime,'12/02/2016',103) and Bk_Check_Out <= CONVERT(datetime,'13/02/2016',103)) and Room_Status = 0)

select count(Room_Id) from Room where Room_Id not in (select Room_Id from Booking where Bk_Check_In >= CONVERT(datetime,'12/02/2016',103) and Bk_Check_Out <= CONVERT(datetime,'13/02/2016',103)) and Room_Status = 0 and Type_Id = 3
select DISTINCT Room.Type_Id,RoomType.* from Room INNER JOIN RoomType ON RoomType.Type_Id = Room.Type_Id where Room_Id not in (select Room_Id from Booking where Bk_Check_In >= CONVERT(datetime,'12/02/2558',103) and Bk_Check_Out <= CONVERT(datetime,'13/02/2558',103))
select DISTINCT Room.Type_Id,RoomType.* from Room INNER JOIN RoomType ON RoomType.Type_Id = Room.Type_Id where Room_Id not in (select Room_Id from Booking where Bk_Check_In >= CONVERT(datetime,'25/05/2017',103) and Bk_Check_Out <= CONVERT(datetime,'26/05/2017',103))
Select CONVERT(varchar(20),GETDATE(),103)
select GETDATE()
select CONVERT(varchar(20),GETDATE(),106)
Select CONVERT(varchar(20),Time,106) AS DiffDate from Review
delete Review