USE moviedb;
SHOW DATABASES;

CREATE TABLE IF NOT EXISTS user(
	user_id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    user_name VARCHAR(30) NOT NULL,
    user_password VARCHAR(30) NOT NULL
);

DROP TABLE IF EXISTS movielist;
CREATE TABLE IF NOT EXISTS movielist(
    PRIMARY KEY(user_id, name, movie_id),
	name VARCHAR(30) NOT NULL,
    date_added VARCHAR(10) NOT NULL,
    movie_id INT UNSIGNED NOt NULL,
    user_id INT UNSIGNED NOt NULL
);

DROP TABLE IF EXISTS movie;
CREATE TABLE IF NOT EXISTS movie(
    movie_id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
	title VARCHAR(30) NOT NULL,
    description BLOB NOT NULL,
    release_date VARCHAR(10) NOT NULL,
    watched BIT not null,
    rating FLOAT not null
);