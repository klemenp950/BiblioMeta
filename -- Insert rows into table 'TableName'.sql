-- Insert rows into table 'TableName'
INSERT INTO AspNetRoles
( -- columns to insert data into
 [Id], [Name]
)
VALUES
( -- first row: values for the columns in the list above
 1, 'User'
),
( -- second row: values for the columns in the list above
 2, 'Admin'
),
(
 3, 'SuperAdmin'
)
-- add more rows here
GO