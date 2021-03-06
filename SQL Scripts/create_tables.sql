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
	user_id INT UNSIGNED NOT NULL,
	name VARCHAR(30) NOT NULL,
    date_added VARCHAR(10) NOT NULL,
    movie_id INT UNSIGNED
);

DROP TABLE IF EXISTS movie;
CREATE TABLE IF NOT EXISTS movie(
    movie_id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
	title VARCHAR(30) NOT NULL,
    description VARCHAR(200) NOT NULL,
    release_date VARCHAR(10) NOT NULL,
    rating FLOAT DEFAULT NULL,
    user_id INT UNSIGNED NOT NULL,
    list_name VARCHAR(30)
);