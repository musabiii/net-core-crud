create table clients (
id int not null primary key identity,
name varchar(100) not null,
email varchar(150) not null unique,
phone varchar(20) null,
address varchar(100) null,
created_at datetime not null default current_timestamp);

insert into clients (name,email,phone,address)
values
('Bill Gates','bill@mail.ru','+1234567','New York ,USA'),
('Elon Mask','elon.mask@spacex.com','+12354','Florida ,USA'),
('Will Smith','will.smith@mail.ru','+98741','California ,USA'),
('Bob Marley','bob.marley@yandex.ru','+5642','Texas ,USA'),
('Christiano Ronaldo','cristiano@mail.ru','+554456','Manchester ,Spain'),
('Boris Jhonson','boris@mail.ru','+996633','London ,England');

select * from clients;