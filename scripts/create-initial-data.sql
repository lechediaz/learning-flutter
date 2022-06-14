USE PermisosDb;
GO

DELETE FROM UserRoles;

DELETE FROM Roles;

SET IDENTITY_INSERT Roles ON;
GO

INSERT INTO Roles
  ([Id],[Name])
VALUES
  (1 ,'EMPLOYEE')
  ,(2 ,'HUMAN_RESOURCES');
GO

SET IDENTITY_INSERT Roles OFF;
GO

DELETE FROM Users;
GO

SET IDENTITY_INSERT Users ON;
GO

INSERT INTO Users
  ([Id],[Name],[UserName])
VALUES
  (1 ,'Oscar Díaz' ,'oscar.diaz')
  ,(2 ,'Karen Rodríguez' ,'karen.rofriguez');
GO

SET IDENTITY_INSERT Users OFF;
GO

SET IDENTITY_INSERT UserRoles ON;
GO

INSERT INTO UserRoles
  ([Id],[UserId],[RoleId])
VALUES
  (1 ,1 ,1)
  ,(2 ,2 ,2);
GO

SET IDENTITY_INSERT UserRoles OFF;
GO
