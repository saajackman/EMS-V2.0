---Selecting Database---

use Training_20Feb_Mumbai
go

-----Creating Department Table-----

CREATE TABLE Department_174778
(
Dept_ID INT PRIMARY KEY IDENTITY NOT NULL,
Dept_Name VARCHAR(50) NOT NULL,
)


DROP TABLE  Department_174778

-----Creating Employee Table-----

CREATE TABLE Employee_174778
(
Emp_ID INT PRIMARY KEY IDENTITY NOT NULL,
Emp_First_Name VARCHAR(25) NOT NULL ,
Emp_Last_Name VARCHAR(25) NOT NULL,
Emp_Date_of_Birth DATE NOT NULL ,
Emp_Date_of_Joining DATE NOT NULL ,
Emp_Dept_ID INT FOREIGN KEY REFERENCES Department_174778 (Dept_ID),
Emp_Grade VARCHAR(2) NOT NULL FOREIGN KEY REFERENCES Grade_Master_174778 (Grade_Code),
--CHECK (Emp_Grade='M1' OR Emp_Grade='M2' OR Emp_Grade='M3' OR Emp_Grade='M7'),
Emp_Designation VARCHAR(50),
Emp_Basic INT NOT NULL,
Emp_Gender VARCHAR(1) NOT NULL CHECK (Emp_Gender='M' OR Emp_Gender='F'),
Emp_Marital_Status VARCHAR(1) NOT NULL  CHECK (Emp_Marital_Status='M' OR Emp_Marital_Status='S'),
Emp_Home_Address  VARCHAR(100) ,
Emp_Contact_Num VARCHAR(15)
)
DROP TABLE Grade_Master_174778

-----Creating UserMaster Table-----

CREATE TABLE User_Master_174778
(
UserID INT PRIMARY KEY IDENTITY,
UserName VARCHAR(15) NOT NULL , 
UserPassword VARCHAR(50) NOT NULL ,
UserType VARCHAR(2)
)

-----Creating GradeMaster Table-----

CREATE TABLE Grade_Master_174778
(
 Grade_Code VARCHAR(2) NOT NULL PRIMARY KEY ,
 Description VARCHAR(20) NOT NULL ,
 Min_Salary INT NOT NULL ,
 Max_Salary INT NOT NULL 
)


SELECT * FROM User_Master_174778

insert into User_Master_174778 Values('Sagar','1234','S')

SELECT * FROM Department_174778  

delete from Department_174778 where Dept_ID=108