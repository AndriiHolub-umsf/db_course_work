-- 1. Игры (три популярных дисциплины)
INSERT INTO game (gname, description) VALUES
('Counter Strike 2', 'Шутер 5x5'),
('Dota 2', 'MOBA 5x5'),
('Valorant', 'Шутер 5x5');

-- 2. 50 игроков
INSERT INTO player (pname, dob, sex) VALUES
('Andrey "Artemis"',    '1995-01-01', 'M'), ('Ivan "Vortex"',      '1996-02-02', 'M'), ('Yuriy "Shadow"',     '1997-03-03', 'M'), ('Maksym "Lancer"',    '1998-04-04', 'M'), ('Vlad "Rocket"',      '1999-05-05', 'M'),
('Daniil "Blaze"',      '1995-06-06', 'M'), ('Nazar "Pyro"',        '1996-07-07', 'M'), ('Serhii "Voltage"',   '1997-08-08', 'M'), ('Stepan "Frost"',     '1998-09-09', 'M'), ('Oleksii "Phantom"',  '1999-10-10', 'M'),
('Denys "Raptor"',      '1995-11-11', 'M'), ('Roman "Berserk"',     '1996-12-12', 'M'), ('Vasyl "Zero"',       '1997-01-13', 'M'), ('Taras "Venom"',      '1998-02-14', 'M'), ('Artem "Mystic"',     '1999-03-15', 'M'),
('Kostya "Ghost"',      '1995-04-16', 'M'), ('Dmytro "Crush"',      '1996-05-17', 'M'), ('Vitalii "Wolf"',     '1997-06-18', 'M'), ('Illia "Thunder"',    '1998-07-19', 'M'), ('Anton "Flash"',      '1999-08-20', 'M'),
('Oleh "Havoc"',        '1995-09-21', 'M'), ('Yevhen "Falcon"',     '1996-10-22', 'M'), ('Andrii "Lucky"',     '1997-11-23', 'M'), ('Daniil "Quick"',     '1998-12-24', 'M'), ('Ihor "Nitro"',       '1999-01-25', 'M'),
('Maks "Zenith"',       '1995-02-26', 'M'), ('Pavlo "Sniper"',      '1996-03-27', 'M'), ('Bohdan "Trigger"',   '1997-04-28', 'M'), ('Gleb "Knight"',      '1998-05-29', 'M'), ('Kirill "Dagger"',    '1999-06-30', 'M'),
('Stanislav "Pulse"',   '1995-07-31', 'M'), ('Viktor "Rebel"',      '1996-08-01', 'M'), ('Andriy "Spirit"',    '1997-09-02', 'M'), ('Maxim "Tempest"',    '1998-10-03', 'M'), ('Borys "Inferno"',    '1999-11-04', 'M'),
('Oleksandr "Blade"',   '1995-12-05', 'M'), ('Yaroslav "Bullet"',   '1996-01-06', 'M'), ('Daniil "Cyber"',     '1997-02-07', 'M'), ('Serhii "Sonic"',     '1998-03-08', 'M'), ('Marko "Joker"',      '1999-04-09', 'M'),
('Vadym "Nova"',        '1995-05-10', 'M'), ('Artem "Storm"',       '1996-06-11', 'M'), ('Mykyta "Drake"',     '1997-07-12', 'M'), ('Petro "Hawk"',       '1998-08-13', 'M'), ('Artur "Spectre"',    '1999-09-14', 'M'),
('Orest "Zodiac"',      '1995-10-15', 'M'), ('Vitaliy "Striker"',   '1996-11-16', 'M'), ('Vladyslav "Pixel"',  '1997-12-17', 'M'), ('Oleksii "Boss"',     '1998-01-18', 'M'), ('Timur "Neo"',        '1999-02-19', 'M');

-- 3. 50 пользователей (логины)
INSERT INTO LOGIN (email, pwd, role, player_id) VALUES
('artemis@esport.com',  'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 1),   ('vortex@esport.com',   'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 2),   ('shadow@esport.com',   'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 3),   ('lancer@esport.com',  'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 4),   ('rocket@esport.com',   'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 5),
('blaze@esport.com',    'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 6),   ('pyro@esport.com',     'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 7),   ('voltage@esport.com',  'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 8),   ('frost@esport.com',   'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 9),   ('phantom@esport.com',  'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 10),
('raptor@esport.com',   'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 11),  ('berserk@esport.com',  'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 12),  ('zero@esport.com',     'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 13),  ('venom@esport.com',   'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 14),  ('mystic@esport.com',   'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 15),
('ghost@esport.com',    'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 16),  ('crush@esport.com',    'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 17),  ('wolf@esport.com',     'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 18),  ('thunder@esport.com', 'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 19),  ('flash@esport.com',    'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 20),
('havoc@esport.com',    'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 21),  ('falcon@esport.com',   'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 22),  ('lucky@esport.com',    'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 23),  ('quick@esport.com',   'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 24),  ('nitro@esport.com',    'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 25),
('zenith@esport.com',   'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 26),  ('sniper@esport.com',   'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 27),  ('trigger@esport.com',  'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 28),  ('knight@esport.com',  'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 29),  ('dagger@esport.com',   'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 30),
('pulse@esport.com',    'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 31),  ('rebel@esport.com',    'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 32),  ('spirit@esport.com',   'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 33),  ('tempest@esport.com', 'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 34),  ('inferno@esport.com',  'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 35),
('blade@esport.com',    'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 36),  ('bullet@esport.com',   'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 37),  ('cyber@esport.com',    'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 38),  ('sonic@esport.com',   'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 39),  ('joker@esport.com',    'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 40),
('nova@esport.com',     'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 41),  ('storm@esport.com',    'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 42),  ('drake@esport.com',    'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 43),  ('hawk@esport.com',    'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 44),  ('spectre@esport.com',  'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 45),
('zodiac@esport.com',   'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 46),  ('striker@esport.com',  'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 47),  ('pixel@esport.com',    'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 48),  ('boss@esport.com',    'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 49),  ('neo@esport.com',      'AQAAAAIAAYagAAAAEHqKp5/rENsI+5FqINEzSQ9Z+cjQ0OTFo+Q4tdL6mZnyb/r1g0PZ0eBsWaJYgzT8ag==', 'User', 50);

-- 4. 10 команд (названия — реальные киберспорт-стиль, капитаны каждый пятый)
INSERT INTO team (tname, captain_id) VALUES
('Kyiv Falcons', 1),  ('Odesa Legion', 6),   ('Dnipro Wolves', 11),   ('Lviv Titans', 16),  ('Kharkiv Phoenix', 21),
('Vinnytsia Blaze', 26), ('Zaporizhzhia Vortex', 31), ('Chernihiv Raptors', 36), ('Poltava Kings', 41), ('Ternopil Shock', 46);

-- 5. Составы команд (по 5 в каждую, капитан+4)
INSERT INTO player_team (tid, pid) VALUES
(1,1),(1,2),(1,3),(1,4),(1,5),
(2,6),(2,7),(2,8),(2,9),(2,10),
(3,11),(3,12),(3,13),(3,14),(3,15),
(4,16),(4,17),(4,18),(4,19),(4,20),
(5,21),(5,22),(5,23),(5,24),(5,25),
(6,26),(6,27),(6,28),(6,29),(6,30),
(7,31),(7,32),(7,33),(7,34),(7,35),
(8,36),(8,37),(8,38),(8,39),(8,40),
(9,41),(9,42),(9,43),(9,44),(9,45),
(10,46),(10,47),(10,48),(10,49),(10,50);

-- 6. Привязка команд к играм (условно 3 CS2, 3 Dota, 4 Valorant)
INSERT INTO game_team (tid, gname) VALUES
(1,'Counter Strike 2'),(2,'Counter Strike 2'),(3,'Counter Strike 2'),
(4,'Dota 2'),(5,'Dota 2'),(6,'Dota 2'),
(7,'Valorant'),(8,'Valorant'),(9,'Valorant'),(10,'Valorant');

-- 7. 10 турниров с названиями
INSERT INTO tournament (gid, name, date, max_teams, points_to_win, players_in_team)
VALUES
(1,'Kyiv Falcon Cup','2025-06-01',5,2,5),
(1,'Odesa Legion Open','2025-06-10',5,2,5),
(1,'Dnipro Wolves Clash','2025-06-20',5,2,5),
(2,'Lviv Titans Arena','2025-07-01',5,2,5),
(2,'Kharkiv Phoenix Fire','2025-07-10',5,2,5),
(2,'Vinnytsia Blaze Rush','2025-07-20',5,2,5),
(3,'Zaporizhzhia Vortex Blast','2025-08-01',5,2,5),
(3,'Chernihiv Raptors Showdown','2025-08-10',5,2,5),
(3,'Poltava Kings Masters','2025-08-20',5,2,5),
(1,'Ternopil Shock Final','2025-09-01',5,2,5);

-- 8. tournament_team — approved заявки (по 5 на каждый турнир, чередуем команды)
INSERT INTO tournament_team (tournament_id, team_id, status) VALUES
(1,1,'approved'),(1,2,'approved'),(1,3,'approved'),(1,4,'approved'),(1,5,'approved'),
(2,2,'approved'),(2,3,'approved'),(2,4,'approved'),(2,5,'approved'),(2,6,'approved'),
(3,3,'approved'),(3,4,'approved'),(3,5,'approved'),(3,6,'approved'),(3,7,'approved'),
(4,4,'approved'),(4,5,'approved'),(4,6,'approved'),(4,7,'approved'),(4,8,'approved'),
(5,5,'approved'),(5,6,'approved'),(5,7,'approved'),(5,8,'approved'),(5,9,'approved'),
(6,6,'approved'),(6,7,'approved'),(6,8,'approved'),(6,9,'approved'),(6,10,'approved'),
(7,1,'approved'),(7,3,'approved'),(7,5,'approved'),(7,7,'approved'),(7,9,'approved'),
(8,2,'approved'),(8,4,'approved'),(8,6,'approved'),(8,8,'approved'),(8,10,'approved'),
(9,1,'approved'),(9,4,'approved'),(9,7,'approved'),(9,10,'approved'),(9,3,'approved'),
(10,2,'approved'),(10,5,'approved'),(10,8,'approved'),(10,1,'approved'),(10,6,'approved');

-- 9. Матчи (по 3 на турнир, разнообразные пары)
INSERT INTO `match` (tournament_id, round, team_a_id, team_b_id, score_a, score_b, winner_id) VALUES
(1,1,1,2,2,1,1), (1,1,3,4,2,0,3), (1,2,1,3,2,0,1),
(2,1,2,3,1,2,3), (2,1,4,5,2,0,4), (2,2,3,4,2,1,3),
(3,1,3,4,2,0,3), (3,1,5,6,2,1,5), (3,2,3,5,2,1,3),
(4,1,4,5,2,0,4), (4,1,6,7,1,2,7), (4,2,4,7,2,1,4),
(5,1,5,6,1,2,6), (5,1,7,8,2,1,7), (5,2,6,7,2,0,6),
(6,1,6,7,2,1,6), (6,1,8,9,2,0,8), (6,2,6,8,2,1,6),
(7,1,1,3,2,1,1), (7,1,5,7,1,2,7), (7,2,1,7,2,0,1),
(8,1,2,4,2,0,2), (8,1,6,8,2,1,6), (8,2,2,6,1,2,6),
(9,1,1,4,2,1,1), (9,1,7,10,2,0,7), (9,2,1,7,2,1,1),
(10,1,2,5,1,2,5), (10,1,8,1,2,0,8), (10,2,5,8,2,1,5);

-- 10. Финалисты турниров
UPDATE tournament SET winner_id=1 WHERE id=1;
UPDATE tournament SET winner_id=3 WHERE id=2;
UPDATE tournament SET winner_id=3 WHERE id=3;
UPDATE tournament SET winner_id=4 WHERE id=4;
UPDATE tournament SET winner_id=6 WHERE id=5;
UPDATE tournament SET winner_id=6 WHERE id=6;
UPDATE tournament SET winner_id=1 WHERE id=7;
UPDATE tournament SET winner_id=6 WHERE id=8;
UPDATE tournament SET winner_id=1 WHERE id=9;
UPDATE tournament SET winner_id=5 WHERE id=10;