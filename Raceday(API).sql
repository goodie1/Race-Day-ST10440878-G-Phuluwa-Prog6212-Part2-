Create database Raceday;
use Raceday;

Create table authentication(authentication_id int identity (1,1)primary key,
                            name varchar(250) unique not null,
                            surname varchar (250) unique not null,
                            email varchar (250) not null,
                            Cell_Number int not null,
                            role varchar (20) not null
                            check(role in ('Participant','Organizer')),
                            password varchar (100) not null);

Create table category(category_id int identity (1,1)primary key,
                      category_type varchar (250),
                      distance varchar (250),
                      age int,
                      role varchar (20) not null
                      check(role in ('Participant','Organizer')));

Create table event (event_id int identity (1,1) primary key,
                     event_name varchar (250)not null,
                     date varchar(50),
                     time varchar (50),
                     event_type varchar (250),
                     event_description varchar (250),
                     location varchar (250),
                     race_route varchar (250),
                     weather varchar (250),
                     role varchar (20) not null
                     check(role in ('Participant','Organizer')));

Create table enrollment (enrollment_Num int identity (1,1) primary key,
                         authentication_id int,
                         category_id int,
                         event_id int,
                         foreign Key(authentication_id) references authentication(authentication_id),
                         foreign Key(category_id) references category (category_id),
                         foreign key(event_id) references event(event_id), 
                         role varchar (20) not null
                         check(role in ('Participant','Organizer')));

Create table result (result_id int identity (1,1)primary key,
                     finish_time varchar (50),
                     race_Positions varchar(250),
                     Avg_pace varchar (20),
                     role varchar (20) not null
                     check(role in ('Participant','Organizer')));


INSERT INTO authentication(name, surname, email, Cell_Number, role, password)
VALUES
    ('TestUser1', 'TestSurname1', 'testuser1@example.com', 712345601, 'Participant', 'TestPassword1'),
    ('TestUser2', 'TestSurname2', 'testuser2@example.com', 712345602, 'Participant', 'TestPassword2'),
    ('TestUser3', 'TestSurname3', 'testuser3@example.com', 712345603, 'Participant', 'TestPassword3');

Insert into category
values ('teenager', '5 km', 16,'Organizer'),
       ('Adult', '10 km', 25,'Organizer'),
       ('Senior', '21 km', 50,'Organizer');

Insert into event 
values('Johannesburg Marathon', '2026-11-15', '07:00', 'Marathon', 'Annual city marathon', 'Johannesburg', 'FNB Stadium, proceed through Soweto', 'Sunny', 'Organizer'),
      ('Pretoria Charity Walk', '2026-11-20', '08:00', 'Charity', 'Walk to support local communities', 'Pretoria', 'Union Buildings through Arcadia', 'Cloudy', 'Organizer'),
      ('Cape Town Cycle Race', '2026-12-05', '06:30', 'Cycling', 'Annual cycling event', 'Cape Town', 'Cape Town Stadium along the Atlantic Seaboard towards Camps Bay', 'Clear', 'Participant');

Insert into enrollment
values (1, 1, 3, 'Participant'),
       (2, 2, 1, 'Participant'),
       (3, 3, 2, 'Participant');


select * from authentication;
Select * from category;
select * from event;
Select * from enrollment;
select * from result;

