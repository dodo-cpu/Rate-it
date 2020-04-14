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
  `idcateegory` INT NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `idParent` INT NULL DEFAULT NULL COMMENT 'null by Parentcategory/ idcategory from childCategory\n',
  `timestamp` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP(),
  PRIMARY KEY (`idcateegory`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `rateit`.`topic`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `rateit`.`topic` (
  `idtopic` INT NOT NULL AUTO_INCREMENT,
  `category_idcateegory` INT NOT NULL,
  `name` VARCHAR(45) NOT NULL,
  `totalpoints` INT NULL DEFAULT 0,
  `totalrankings` INT NULL DEFAULT 0,
  `timestamp` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP(),
  PRIMARY KEY (`idtopic`),
  INDEX `fk_topic_category1_idx` (`category_idcateegory` ASC) ,
  CONSTRAINT `fk_topic_category1`
    FOREIGN KEY (`category_idcateegory`)
    REFERENCES `rateit`.`category` (`idcateegory`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `rateit`.`assesmentCriteria`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `rateit`.`assesmentCriteria` (
  `idassesmentCriteria` INT NOT NULL AUTO_INCREMENT,
  `category_idcateegory` INT NOT NULL,
  `name` VARCHAR(255) NOT NULL,
  `timestamp` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP(),
  PRIMARY KEY (`idassesmentCriteria`),
  INDEX `fk_assesmentCriteria_category_idx` (`category_idcateegory` ASC),
  CONSTRAINT `fk_assesmentCriteria_category`
    FOREIGN KEY (`category_idcateegory`)
    REFERENCES `rateit`.`category` (`idcateegory`)
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
  `assesmentCriteria_idassesmentCriteria` INT NOT NULL,
  `topic_idtopic` INT NOT NULL,
  `points` INT NULL,
  `timestamp` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP(),
  PRIMARY KEY (`idrating`),
  INDEX `fk_ratingTopic_assesmentCriteria1_idx` (`assesmentCriteria_idassesmentCriteria` ASC) ,
  INDEX `fk_ratingTopic_topic1_idx` (`topic_idtopic` ASC),
  CONSTRAINT `fk_ratingTopic_assesmentCriteria1`
    FOREIGN KEY (`assesmentCriteria_idassesmentCriteria`)
    REFERENCES `rateit`.`assesmentCriteria` (`idassesmentCriteria`)
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
  `assesmentCriteria_idassesmentCriteria` INT NOT NULL,
  `points` INT NOT NULL,
  `timestamp` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP(),
  PRIMARY KEY (`iduserrantings`),
  INDEX `fk_ratinguser_user1_idx` (`user_iduser` ASC),
  INDEX `fk_ratinguser_topic1_idx` (`topic_idtopic` ASC) ,
  INDEX `fk_ratinguser_assesmentCriteria1_idx` (`assesmentCriteria_idassesmentCriteria` ASC),
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
  CONSTRAINT `fk_ratinguser_assesmentCriteria1`
    FOREIGN KEY (`assesmentCriteria_idassesmentCriteria`)
    REFERENCES `rateit`.`assesmentCriteria` (`idassesmentCriteria`)
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
