-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema rateit
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema rateit
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `rateit` DEFAULT CHARACTER SET utf8 ;
USE `rateit` ;

-- -----------------------------------------------------
-- Table `rateit`.`user`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `rateit`.`user` (
  `iduser` INT NOT NULL AUTO_INCREMENT,
  `username` VARCHAR(255) NOT NULL,
  `password` VARCHAR(255) NOT NULL COMMENT 'no plaintext',
  `rights` INT NOT NULL DEFAULT 0 COMMENT '0 = normal user\n1 = admin\n',
  `timestamp` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP() COMMENT 'time user got insert\n',
  PRIMARY KEY (`iduser`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `rateit`.`category`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `rateit`.`category` (
  `idcategory` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `idParent` INT NULL DEFAULT NULL COMMENT 'null by Parentcategory/ idcategory from childCategory\n',
  `timestamp` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP(),
  PRIMARY KEY (`idcategory`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `rateit`.`topic`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `rateit`.`topic` (
  `idtopic` INT NOT NULL AUTO_INCREMENT,
  `category_idcategory` INT NOT NULL,
  `name` VARCHAR(45) NOT NULL,
  `totalpoints` INT NULL DEFAULT 0,
  `totalrankings` INT NULL DEFAULT 0,
  `timestamp` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP(),
  PRIMARY KEY (`idtopic`),
  INDEX `fk_topic_category1_idx` (`category_idcategory` ASC) ,
  CONSTRAINT `fk_topic_category1`
    FOREIGN KEY (`category_idcategory`)
    REFERENCES `rateit`.`category` (`idcategory`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `rateit`.`criterion`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `rateit`.`criterion` (
  `idcriterion` INT NOT NULL AUTO_INCREMENT,
  `category_idcategory` INT NOT NULL,
  `name` VARCHAR(255) NOT NULL,
  `timestamp` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP(),
  PRIMARY KEY (`idcriterion`),
  INDEX `fk_criterion_category_idx` (`category_idcategory` ASC),
  CONSTRAINT `fk_criterion_category`
    FOREIGN KEY (`category_idcategory`)
    REFERENCES `rateit`.`category` (`idcategory`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `rateit`.`comment`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `rateit`.`comment` (
  `idcomments` INT NOT NULL AUTO_INCREMENT,
  `topic_idtopic` INT NOT NULL,
  `user_iduser` INT NOT NULL,
  `comment` VARCHAR(500) NOT NULL,
  `timestamp` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP(),
  PRIMARY KEY (`idcomments`),
  INDEX `fk_comments_topic1_idx` (`topic_idtopic` ASC),
  INDEX `fk_comments_user1_idx` (`user_iduser` ASC),
  CONSTRAINT `fk_comments_topic1`
    FOREIGN KEY (`topic_idtopic`)
    REFERENCES `rateit`.`topic` (`idtopic`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_comments_user1`
    FOREIGN KEY (`user_iduser`)
    REFERENCES `rateit`.`user` (`iduser`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `rateit`.`ratingTopic`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `rateit`.`ratingTopic` (
  `idrating` INT NOT NULL AUTO_INCREMENT,
  `criterion_idcriterion` INT NOT NULL,
  `topic_idtopic` INT NOT NULL,
  `points` INT NULL,
  `timestamp` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP(),
  PRIMARY KEY (`idrating`),
  INDEX `fk_ratingTopic_criterion1_idx` (`criterion_idcriterion` ASC) ,
  INDEX `fk_ratingTopic_topic1_idx` (`topic_idtopic` ASC),
  CONSTRAINT `fk_ratingTopic_criterion1`
    FOREIGN KEY (`criterion_idcriterion`)
    REFERENCES `rateit`.`criterion` (`idcriterion`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_ratingTopic_topic1`
    FOREIGN KEY (`topic_idtopic`)
    REFERENCES `rateit`.`topic` (`idtopic`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `rateit`.`ratingUser`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `rateit`.`ratingUser` (
  `iduserrantings` INT NOT NULL AUTO_INCREMENT,
  `user_iduser` INT NOT NULL,
  `topic_idtopic` INT NOT NULL,
  `criterion_idcriterion` INT NOT NULL,
  `points` INT NOT NULL,
  `timestamp` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP(),
  PRIMARY KEY (`iduserrantings`),
  INDEX `fk_ratinguser_user1_idx` (`user_iduser` ASC),
  INDEX `fk_ratinguser_topic1_idx` (`topic_idtopic` ASC) ,
  INDEX `fk_ratinguser_criterion_idx` (`criterion_idcriterion` ASC),
  CONSTRAINT `fk_ratinguser_user1`
    FOREIGN KEY (`user_iduser`)
    REFERENCES `rateit`.`user` (`iduser`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_ratinguser_topic1`
    FOREIGN KEY (`topic_idtopic`)
    REFERENCES `rateit`.`topic` (`idtopic`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_ratinguser_criterion1`
    FOREIGN KEY (`criterion_idcriterion`)
    REFERENCES `rateit`.`criterion` (`idcriterion`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `rateit`.`topicInfo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `rateit`.`topicInfo` (
  `idtopicinfo` INT NOT NULL AUTO_INCREMENT,
  `topic_idtopic` INT NOT NULL,
  `name` VARCHAR(45) NULL,
  `value` VARCHAR(45) NULL,
  `timestamp` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP(),
  PRIMARY KEY (`idtopicinfo`),
  INDEX `fk_topicinfo_topic1_idx` (`topic_idtopic` ASC))
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;


/*
-- Query: SELECT * FROM rateit.user
LIMIT 0, 50000

-- Date: 2020-04-03 11:21
*/
INSERT INTO `user` (`iduser`,`username`,`password`,`rights`,`timestamp`) VALUES (1,'admin','7d4e3eec80026719639ed4dba68916eb94c7a49a053e05c8f9578fe4e5a3d7ea',1,'2020-03-27 15:10:07');
INSERT INTO `user` (`iduser`,`username`,`password`,`rights`,`timestamp`) VALUES (2,'testuser','7d4e3eec80026719639ed4dba68916eb94c7a49a053e05c8f9578fe4e5a3d7ea',0,'2020-03-27 15:14:12');
INSERT INTO `user` (`iduser`,`username`,`password`,`rights`,`timestamp`) VALUES (3,'Frieda','c0067d4af4e87f00dbac63b6156828237059172d1bbeac67427345d6a9fda484',0,'2020-03-27 15:14:12');
INSERT INTO `user` (`iduser`,`username`,`password`,`rights`,`timestamp`) VALUES (4,'Friedrich','c0067d4af4e87f00dbac63b6156828237059172d1bbeac67427345d6a9fda484',0,'2020-03-27 15:14:12');

/*
-- Query: SELECT * FROM rateit.category
LIMIT 0, 50000

-- Date: 2020-04-03 11:19
*/
INSERT INTO `category` (`idcategory`,`name`,`idParent`,`timestamp`) VALUES (1,'Bücher',NULL,'2020-03-27 15:15:40');
INSERT INTO `category` (`idcategory`,`name`,`idParent`,`timestamp`) VALUES (2,'Filme',NULL,'2020-03-27 15:15:40');
INSERT INTO `category` (`idcategory`,`name`,`idParent`,`timestamp`) VALUES (3,'Musik',NULL,'2020-03-27 15:15:40');
INSERT INTO `category` (`idcategory`,`name`,`idParent`,`timestamp`) VALUES (4,'Krimi',1,'2020-03-27 15:18:19');
INSERT INTO `category` (`idcategory`,`name`,`idParent`,`timestamp`) VALUES (5,'Sachbuch',1,'2020-03-27 15:18:19');
INSERT INTO `category` (`idcategory`,`name`,`idParent`,`timestamp`) VALUES (6,'Horror',2,'2020-03-27 15:18:19');
INSERT INTO `category` (`idcategory`,`name`,`idParent`,`timestamp`) VALUES (7,'Fantasy',2,'2020-03-27 15:18:19');
INSERT INTO `category` (`idcategory`,`name`,`idParent`,`timestamp`) VALUES (8,'Country',3,'2020-03-27 15:18:19');
INSERT INTO `category` (`idcategory`,`name`,`idParent`,`timestamp`) VALUES (9,'Pop',3,'2020-03-27 15:18:19');

/*
-- Query: SELECT * FROM rateit.topic
LIMIT 0, 50000

-- Date: 2020-04-03 11:20
*/
INSERT INTO `topic` (`idtopic`,`category_idcategory`,`name`,`totalpoints`,`totalrankings`,`timestamp`) VALUES (1,4,'Krimibuch_1',0,0,'2020-03-27 15:29:30');
INSERT INTO `topic` (`idtopic`,`category_idcategory`,`name`,`totalpoints`,`totalrankings`,`timestamp`) VALUES (2,4,'Die Vermissten',0,0,'2020-03-27 15:29:30');
INSERT INTO `topic` (`idtopic`,`category_idcategory`,`name`,`totalpoints`,`totalrankings`,`timestamp`) VALUES (3,5,'Sachbuch_1',0,0,'2020-03-27 15:29:30');
INSERT INTO `topic` (`idtopic`,`category_idcategory`,`name`,`totalpoints`,`totalrankings`,`timestamp`) VALUES (4,5,'Das historische Heidelberg',0,0,'2020-03-27 15:29:30');
INSERT INTO `topic` (`idtopic`,`category_idcategory`,`name`,`totalpoints`,`totalrankings`,`timestamp`) VALUES (5,6,'Horrorfilm_1',0,0,'2020-03-27 15:29:30');
INSERT INTO `topic` (`idtopic`,`category_idcategory`,`name`,`totalpoints`,`totalrankings`,`timestamp`) VALUES (6,6,'Conjuring – Die Heimsuchung',0,0,'2020-03-27 15:29:30');
INSERT INTO `topic` (`idtopic`,`category_idcategory`,`name`,`totalpoints`,`totalrankings`,`timestamp`) VALUES (7,7,'Fantasyfilm_1',0,0,'2020-03-27 15:29:30');
INSERT INTO `topic` (`idtopic`,`category_idcategory`,`name`,`totalpoints`,`totalrankings`,`timestamp`) VALUES (8,7,'Harry Potter und der Stein der Weisen',0,0,'2020-03-27 15:29:30');
INSERT INTO `topic` (`idtopic`,`category_idcategory`,`name`,`totalpoints`,`totalrankings`,`timestamp`) VALUES (9,8,'Countrysong_1',0,0,'2020-03-27 15:29:30');
INSERT INTO `topic` (`idtopic`,`category_idcategory`,`name`,`totalpoints`,`totalrankings`,`timestamp`) VALUES (10,8,'John Denver - Take Me Home, Country Roads ',0,0,'2020-03-27 15:29:30');
INSERT INTO `topic` (`idtopic`,`category_idcategory`,`name`,`totalpoints`,`totalrankings`,`timestamp`) VALUES (11,9,'Popsong_1',0,0,'2020-03-27 15:29:30');
INSERT INTO `topic` (`idtopic`,`category_idcategory`,`name`,`totalpoints`,`totalrankings`,`timestamp`) VALUES (12,9,'Michael Jackson - Thriller',0,0,'2020-03-27 15:29:30');

/*
-- Query: SELECT * FROM rateit.criterion
LIMIT 0, 50000

-- Date: 2020-04-03 11:18
*/
INSERT INTO `criterion` (`idcriterion`,`category_idcategory`,`name`,`timestamp`) VALUES (1,4,'Dramatik','2020-03-27 16:02:02');
INSERT INTO `criterion` (`idcriterion`,`category_idcategory`,`name`,`timestamp`) VALUES (2,4,'Spannung','2020-03-27 16:02:02');
INSERT INTO `criterion` (`idcriterion`,`category_idcategory`,`name`,`timestamp`) VALUES (3,4,'Schreibweise','2020-03-27 16:02:02');
INSERT INTO `criterion` (`idcriterion`,`category_idcategory`,`name`,`timestamp`) VALUES (4,5,'Vermitteltes Wissen','2020-03-27 16:02:02');
INSERT INTO `criterion` (`idcriterion`,`category_idcategory`,`name`,`timestamp`) VALUES (5,5,'Mehrwert','2020-03-27 16:02:02');
INSERT INTO `criterion` (`idcriterion`,`category_idcategory`,`name`,`timestamp`) VALUES (6,5,'Verständlich geschrieben','2020-03-27 16:02:02');
INSERT INTO `criterion` (`idcriterion`,`category_idcategory`,`name`,`timestamp`) VALUES (7,6,'Atmosphäre','2020-03-27 16:02:02');
INSERT INTO `criterion` (`idcriterion`,`category_idcategory`,`name`,`timestamp`) VALUES (8,6,'Geschichte','2020-03-27 16:02:02');
INSERT INTO `criterion` (`idcriterion`,`category_idcategory`,`name`,`timestamp`) VALUES (9,6,'Angst','2020-03-27 16:02:02');
INSERT INTO `criterion` (`idcriterion`,`category_idcategory`,`name`,`timestamp`) VALUES (10,7,'Effekte','2020-03-27 16:02:02');
INSERT INTO `criterion` (`idcriterion`,`category_idcategory`,`name`,`timestamp`) VALUES (11,7,'Spannung','2020-03-27 16:02:02');
INSERT INTO `criterion` (`idcriterion`,`category_idcategory`,`name`,`timestamp`) VALUES (12,7,'Kostüme','2020-03-27 16:02:02');
INSERT INTO `criterion` (`idcriterion`,`category_idcategory`,`name`,`timestamp`) VALUES (13,8,'Musik','2020-03-27 16:02:02');
INSERT INTO `criterion` (`idcriterion`,`category_idcategory`,`name`,`timestamp`) VALUES (14,8,'Lyrik','2020-03-27 16:02:02');
INSERT INTO `criterion` (`idcriterion`,`category_idcategory`,`name`,`timestamp`) VALUES (15,8,'Rythmus','2020-03-27 16:02:02');
INSERT INTO `criterion` (`idcriterion`,`category_idcategory`,`name`,`timestamp`) VALUES (16,9,'Rythmus','2020-03-27 16:02:02');
INSERT INTO `criterion` (`idcriterion`,`category_idcategory`,`name`,`timestamp`) VALUES (17,9,'Lyrik','2020-03-27 16:02:02');
INSERT INTO `criterion` (`idcriterion`,`category_idcategory`,`name`,`timestamp`) VALUES (18,9,'Musik','2020-03-27 16:02:02');

/*
-- Query: SELECT * FROM rateit.comment
LIMIT 0, 50000

-- Date: 2020-04-03 11:20
*/
INSERT INTO `comment` (`idcomments`,`topic_idtopic`,`user_iduser`,`comment`,`timestamp`) VALUES (1,8,3,'erster','2020-04-03 11:05:42');
INSERT INTO `comment` (`idcomments`,`topic_idtopic`,`user_iduser`,`comment`,`timestamp`) VALUES (2,12,4,'R.I.P King of Pop','2020-04-03 11:06:36');

/*
-- Query: SELECT * FROM rateit.ratingtopic
LIMIT 0, 50000

-- Date: 2020-04-03 11:20
*/
INSERT INTO `ratingtopic` (`idrating`,`criterion_idcriterion`,`topic_idtopic`,`points`,`timestamp`) VALUES (1,18,12,5,'2020-04-03 11:12:42');
INSERT INTO `ratingtopic` (`idrating`,`criterion_idcriterion`,`topic_idtopic`,`points`,`timestamp`) VALUES (2,17,12,3,'2020-04-03 11:12:42');
INSERT INTO `ratingtopic` (`idrating`,`criterion_idcriterion`,`topic_idtopic`,`points`,`timestamp`) VALUES (3,16,12,5,'2020-04-03 11:12:42');
INSERT INTO `ratingtopic` (`idrating`,`criterion_idcriterion`,`topic_idtopic`,`points`,`timestamp`) VALUES (4,12,8,4,'2020-04-03 11:12:42');
INSERT INTO `ratingtopic` (`idrating`,`criterion_idcriterion`,`topic_idtopic`,`points`,`timestamp`) VALUES (5,11,8,1,'2020-04-03 11:12:42');
INSERT INTO `ratingtopic` (`idrating`,`criterion_idcriterion`,`topic_idtopic`,`points`,`timestamp`) VALUES (6,10,8,2,'2020-04-03 11:12:42');

/*
-- Query: SELECT * FROM rateit.ratinguser
LIMIT 0, 50000

-- Date: 2020-04-03 11:20
*/
INSERT INTO `ratinguser` (`iduserrantings`,`user_iduser`,`topic_idtopic`,`criterion_idcriterion`,`points`,`timestamp`) VALUES (1,4,12,18,5,'2020-04-03 11:11:08');
INSERT INTO `ratinguser` (`iduserrantings`,`user_iduser`,`topic_idtopic`,`criterion_idcriterion`,`points`,`timestamp`) VALUES (2,4,12,16,3,'2020-04-03 11:11:08');
INSERT INTO `ratinguser` (`iduserrantings`,`user_iduser`,`topic_idtopic`,`criterion_idcriterion`,`points`,`timestamp`) VALUES (3,4,12,17,5,'2020-04-03 11:11:08');
INSERT INTO `ratinguser` (`iduserrantings`,`user_iduser`,`topic_idtopic`,`criterion_idcriterion`,`points`,`timestamp`) VALUES (4,3,8,12,4,'2020-04-03 11:11:08');
INSERT INTO `ratinguser` (`iduserrantings`,`user_iduser`,`topic_idtopic`,`criterion_idcriterion`,`points`,`timestamp`) VALUES (5,3,8,11,1,'2020-04-03 11:11:08');
INSERT INTO `ratinguser` (`iduserrantings`,`user_iduser`,`topic_idtopic`,`criterion_idcriterion`,`points`,`timestamp`) VALUES (6,3,8,10,2,'2020-04-03 11:11:08');

/*
-- Query: SELECT * FROM rateit.topicinfo
LIMIT 0, 50000

-- Date: 2020-04-03 11:21
*/
INSERT INTO `topicinfo` (`idtopicinfo`,`topic_idtopic`,`name`,`value`,`timestamp`) VALUES (1,1,'Verfilmt','Nein','2020-03-27 15:47:48');
INSERT INTO `topicinfo` (`idtopicinfo`,`topic_idtopic`,`name`,`value`,`timestamp`) VALUES (2,1,'Zusatzinfo','Beispiel Krimibuch','2020-03-27 15:47:48');
INSERT INTO `topicinfo` (`idtopicinfo`,`topic_idtopic`,`name`,`value`,`timestamp`) VALUES (3,2,'Verfilmt','Ja','2020-03-27 15:47:48');
INSERT INTO `topicinfo` (`idtopicinfo`,`topic_idtopic`,`name`,`value`,`timestamp`) VALUES (4,2,'Autor','Jemand','2020-03-27 15:47:48');
INSERT INTO `topicinfo` (`idtopicinfo`,`topic_idtopic`,`name`,`value`,`timestamp`) VALUES (5,2,'Veröffentlich','2000-12-12','2020-03-27 15:47:48');
INSERT INTO `topicinfo` (`idtopicinfo`,`topic_idtopic`,`name`,`value`,`timestamp`) VALUES (6,3,'Zusatzinfo','Beispiel Sachbuch','2020-03-27 15:47:48');
INSERT INTO `topicinfo` (`idtopicinfo`,`topic_idtopic`,`name`,`value`,`timestamp`) VALUES (7,4,'Thema','Städte','2020-03-27 15:47:48');
INSERT INTO `topicinfo` (`idtopicinfo`,`topic_idtopic`,`name`,`value`,`timestamp`) VALUES (8,4,'Autor','Menschlich','2020-03-27 15:47:48');
INSERT INTO `topicinfo` (`idtopicinfo`,`topic_idtopic`,`name`,`value`,`timestamp`) VALUES (9,5,'Zusatzinfo','Beispiel Horrorfilm','2020-03-27 15:47:48');
INSERT INTO `topicinfo` (`idtopicinfo`,`topic_idtopic`,`name`,`value`,`timestamp`) VALUES (10,6,'Veröffentlich','2013-08-01','2020-03-27 15:47:48');
INSERT INTO `topicinfo` (`idtopicinfo`,`topic_idtopic`,`name`,`value`,`timestamp`) VALUES (11,6,'Länge','132 min','2020-03-27 15:47:48');
INSERT INTO `topicinfo` (`idtopicinfo`,`topic_idtopic`,`name`,`value`,`timestamp`) VALUES (12,6,'FSK','16','2020-03-27 15:47:48');
INSERT INTO `topicinfo` (`idtopicinfo`,`topic_idtopic`,`name`,`value`,`timestamp`) VALUES (13,7,'Zusatzinfo','Beispiel Fantasyfilm','2020-03-27 15:47:48');
INSERT INTO `topicinfo` (`idtopicinfo`,`topic_idtopic`,`name`,`value`,`timestamp`) VALUES (14,8,'Veröffentlich','2001-11-22','2020-03-27 15:47:48');
INSERT INTO `topicinfo` (`idtopicinfo`,`topic_idtopic`,`name`,`value`,`timestamp`) VALUES (15,8,'Regisseur','Chris Columbus','2020-03-27 15:47:48');
INSERT INTO `topicinfo` (`idtopicinfo`,`topic_idtopic`,`name`,`value`,`timestamp`) VALUES (16,8,'Beschreibung','Der junge Waise Harry Potter wächst bei seine','2020-03-27 15:47:48');
INSERT INTO `topicinfo` (`idtopicinfo`,`topic_idtopic`,`name`,`value`,`timestamp`) VALUES (17,9,'Zusatzinfo','Beispiel Countrysong','2020-03-27 15:47:48');
INSERT INTO `topicinfo` (`idtopicinfo`,`topic_idtopic`,`name`,`value`,`timestamp`) VALUES (18,10,'Sprache','Englisch','2020-03-27 15:47:48');
INSERT INTO `topicinfo` (`idtopicinfo`,`topic_idtopic`,`name`,`value`,`timestamp`) VALUES (19,10,'Länge','3:18 min','2020-03-27 15:47:48');
INSERT INTO `topicinfo` (`idtopicinfo`,`topic_idtopic`,`name`,`value`,`timestamp`) VALUES (20,10,'Veröffentlich ','2013-04-05','2020-03-27 15:47:48');
INSERT INTO `topicinfo` (`idtopicinfo`,`topic_idtopic`,`name`,`value`,`timestamp`) VALUES (21,11,'Zusatzinfo','Beispiel Popsong ','2020-03-27 15:47:48');
INSERT INTO `topicinfo` (`idtopicinfo`,`topic_idtopic`,`name`,`value`,`timestamp`) VALUES (22,12,'Albumname','Thriller','2020-03-27 15:47:48');
INSERT INTO `topicinfo` (`idtopicinfo`,`topic_idtopic`,`name`,`value`,`timestamp`) VALUES (23,12,'Veröffentlich','1982','2020-03-27 15:47:48');
INSERT INTO `topicinfo` (`idtopicinfo`,`topic_idtopic`,`name`,`value`,`timestamp`) VALUES (24,12,'Auszeichnungen',' Grammy Award - Bestes Musikvideo','2020-03-27 15:47:48');
