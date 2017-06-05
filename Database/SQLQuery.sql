insert into AdminAccount Values (1,'thanawat','0841776616','Thanawat','pinya','thanawat_pinya@outlook.com','7-5-2560 16:01:00')
select * from AdminAccount
insert into Customer Values (1,'000000','Thanawat','pinya','aom_joeyl3oy@hotmail.com','0835674551',14-7-2560,16-7-2560,'Room')
select * from Customer
select FirstName + ' ' + LastName as UserName from AdminAccount  where AdminID = 'thanawat' and Password='0841776616'
select FirstName + ' ' + LastName as UserName,Status from AdminAccount  where AdminID = 'thanawat' and Password='0841776616'
Update AdminAccount set Status = 'admin' where FirstName = 'Thanawat'
insert into AdminAccount Values (2,'thanawatS','0841776616','Thanawat','pinya','thanawat_pinya@outlook.com','2017-7-7 16:01:00','staff')

insert into AdminAccount (AdminID,[Password],FirstName,LastName,Email,Status) values ('Admintest','0841776616','Thanawat','pinya','thanawat_pinya@outlook.com','staff')
 select AdminAccount.*,Test.Number as Number  from AdminAccount inner join Test on AdminAccount.Status = Test.Status
 insert into Customer (SerialCode,FirstName,LastName,CheckIn,CheckOut,Email,Phone) values ('123456','thanawat','pinya','05/18/2017','05/18/2017','aom@hotmai.com','08417766166')
 select *,CONVERT(VARCHAR(10),GETDATE(),110) from AdminAccount

 update AdminAccount set FirstName = 'Thanawat', LastName = 'pinya555', AdminID = 'thanawat,thanawat', Email = 'thanawat_pinya@outlook.com', Status = 'admin' where AdminID = 'thanawat,thanawat' and FirstName = 'Thanawat'
 update AdminAccount set DateTime = CONVERT(VARCHAR(24),GETDATE(),121) where AdminID = 'thanawatS' and FirstName = 'Thanawat'
 
 select CONVERT(varchar(11),LastLogin,121) as DateTime from AdminAccount
 