/*CREATE TABLE books (
book_id int not null primary key auto_increment,
book_title VARCHAR(200) not null,
book_author VARCHAR(200) not null,
num_pages int,
published_date DATE
);

CREATE TABLE customers (
customer_id int not null primary key auto_increment,
customer_name VARCHAR(200) not null,
customer_phone VARCHAR(200) not null,
customer_email VARCHAR(200) not null
);

CREATE TABLE customer_book_checkout (
 customer_id int not null,
 book_id int not null,
 checkout_date DATE,
 foreign key (customer_id) references customers(customer_id),
 foreign key (book_id) references books(book_id)
)

insert into books (book_title, book_author, num_pages, published_date, available) 
values
("The One Tree", "Stephen R. Donaldons", 425, '1982-04-02', true);
*/

