-- 1. Создать базу (если нужно)
CREATE DATABASE IF NOT EXISTS esportdb CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE esportdb;

-- 2. Игроки (player)
CREATE TABLE IF NOT EXISTS player (
    pid INT PRIMARY KEY AUTO_INCREMENT,
    pname VARCHAR(100) NOT NULL,
    dob DATE,
    sex VARCHAR(10),
    photo LONGBLOB
);

-- 3. Пользователи (LOGIN)
CREATE TABLE IF NOT EXISTS LOGIN (
    id INT PRIMARY KEY AUTO_INCREMENT,
    email VARCHAR(255) NOT NULL UNIQUE,
    pwd VARCHAR(255) NOT NULL,
    role VARCHAR(20) DEFAULT 'User',
    player_id INT,
    FOREIGN KEY (player_id) REFERENCES player(pid) ON DELETE SET NULL
);

-- 4. Игры (game)
CREATE TABLE IF NOT EXISTS game (
    id INT PRIMARY KEY AUTO_INCREMENT,
    gname VARCHAR(100) NOT NULL UNIQUE,
    photo LONGBLOB,
    description TEXT
);

-- 5. Команды (team)
CREATE TABLE IF NOT EXISTS team (
    id INT PRIMARY KEY AUTO_INCREMENT,
    tname VARCHAR(100) NOT NULL UNIQUE,
    photo LONGBLOB,
    captain_id INT,
    FOREIGN KEY (captain_id) REFERENCES player(pid) ON DELETE SET NULL
);

-- 6. Связь игрок-команда (player_team)
CREATE TABLE IF NOT EXISTS player_team (
    id INT PRIMARY KEY AUTO_INCREMENT,
    tid INT NOT NULL,
    pid INT NOT NULL,
    FOREIGN KEY (tid) REFERENCES team(id) ON DELETE CASCADE,
    FOREIGN KEY (pid) REFERENCES player(pid) ON DELETE CASCADE
);

-- 7. Связь команда-игра (game_team)
CREATE TABLE IF NOT EXISTS game_team (
    id INT PRIMARY KEY AUTO_INCREMENT,
    tid INT NOT NULL,
    gname VARCHAR(100) NOT NULL,
    FOREIGN KEY (tid) REFERENCES team(id) ON DELETE CASCADE,
    FOREIGN KEY (gname) REFERENCES game(gname) ON DELETE CASCADE
);


-- 8. Турниры (tournament)
CREATE TABLE IF NOT EXISTS tournament (
    id INT PRIMARY KEY AUTO_INCREMENT,
    gid INT NOT NULL,
    name VARCHAR(100) NOT NULL,
    date DATE NOT NULL,
    max_teams INT NOT NULL,
    points_to_win INT NOT NULL,
    players_in_team INT DEFAULT 1,
    winner_id INT DEFAULT NULL
);

-- 9. Связь команда-турнир (tournament_team) — если понадобится для турнирной сетки
CREATE TABLE IF NOT EXISTS tournament_team (
    id INT PRIMARY KEY AUTO_INCREMENT,
    tournament_id INT NOT NULL,
    team_id INT NOT NULL,
	status ENUM('pending','approved','rejected') NOT NULL DEFAULT 'pending',
    FOREIGN KEY (tournament_id) REFERENCES tournament(id) ON DELETE CASCADE,
    FOREIGN KEY (team_id) REFERENCES team(id) ON DELETE CASCADE
);

-- 10. Матчи (match)
CREATE TABLE IF NOT EXISTS `match` (
    id INT PRIMARY KEY AUTO_INCREMENT,
    tournament_id INT NOT NULL,
    round INT NOT NULL,
    team_a_id INT NOT NULL,
    team_b_id INT NOT NULL,
    score_a INT,
    score_b INT,
    winner_id INT,
    FOREIGN KEY (tournament_id) REFERENCES tournament(id) ON DELETE CASCADE,
    FOREIGN KEY (team_a_id) REFERENCES team(id) ON DELETE CASCADE,
    FOREIGN KEY (team_b_id) REFERENCES team(id) ON DELETE CASCADE,
    FOREIGN KEY (winner_id) REFERENCES team(id) ON DELETE SET NULL
);
