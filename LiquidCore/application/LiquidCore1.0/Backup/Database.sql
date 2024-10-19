/*
SQLyog Enterprise - MySQL GUI v5.17
Host - 5.0.24-community-nt : Database - bayerwebsite
*********************************************************************
Server version : 5.0.24-community-nt
*/


SET NAMES utf8;

SET SQL_MODE='';

create database if not exists `liquidcore_test`;

USE `liquidcore_test`;

SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO';

/*Table structure for table `adg_authorizeddocumentsgroups` */

DROP TABLE IF EXISTS `adg_authorizeddocumentsgroups`;

CREATE TABLE `adg_authorizeddocumentsgroups` (
  `adg_id` int(11) NOT NULL auto_increment,
  `sta_id` int(11) NOT NULL default '0',
  `doc_id` int(11) NOT NULL default '0',
  `grp_id` int(11) NOT NULL default '0',
  `adg_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `adg_createdby` varchar(255) NOT NULL default '',
  `adg_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `adg_updatedby` varchar(255) NOT NULL default '',
  `adg_hidden` int(1) NOT NULL default '0',
  `adg_deleted` int(1) NOT NULL default '0',
  `adg_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`adg_id`),
  KEY `adg_1` (`adg_id`,`sta_id`,`doc_id`,`grp_id`,`adg_hidden`,`adg_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `adg_authorizeddocumentsgroups` */

LOCK TABLES `adg_authorizeddocumentsgroups` WRITE;

UNLOCK TABLES;

/*Table structure for table `adr_authorizeddocumentsroles` */

DROP TABLE IF EXISTS `adr_authorizeddocumentsroles`;

CREATE TABLE `adr_authorizeddocumentsroles` (
  `adr_id` int(11) NOT NULL auto_increment,
  `sta_id` int(11) NOT NULL default '0',
  `doc_id` int(11) NOT NULL default '0',
  `rol_id` int(11) NOT NULL default '0',
  `adr_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `adr_createdby` varchar(255) NOT NULL default '',
  `adr_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `adr_updatedby` varchar(255) NOT NULL default '',
  `adr_hidden` int(1) NOT NULL default '0',
  `adr_deleted` int(1) NOT NULL default '0',
  `adr_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`adr_id`),
  KEY `adr_1` (`adr_id`,`sta_id`,`doc_id`,`rol_id`,`adr_hidden`,`adr_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `adr_authorizeddocumentsroles` */

LOCK TABLES `adr_authorizeddocumentsroles` WRITE;

UNLOCK TABLES;

/*Table structure for table `amg_authorizedmodulesgroups` */

DROP TABLE IF EXISTS `amg_authorizedmodulesgroups`;

CREATE TABLE `amg_authorizedmodulesgroups` (
  `amg_id` int(11) NOT NULL auto_increment,
  `sta_id` int(11) NOT NULL default '0',
  `mod_id` int(11) NOT NULL default '0',
  `grp_id` int(11) NOT NULL default '0',
  `amg_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `amg_createdby` varchar(255) NOT NULL default '',
  `amg_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `amg_updatedby` varchar(255) NOT NULL default '',
  `amg_hidden` int(1) NOT NULL default '0',
  `amg_deleted` int(1) NOT NULL default '0',
  `amg_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`amg_id`),
  KEY `amg_1` (`amg_id`,`sta_id`,`mod_id`,`grp_id`,`amg_hidden`,`amg_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `amg_authorizedmodulesgroups` */

LOCK TABLES `amg_authorizedmodulesgroups` WRITE;

UNLOCK TABLES;

/*Table structure for table `amr_authorizedmodulesroles` */

DROP TABLE IF EXISTS `amr_authorizedmodulesroles`;

CREATE TABLE `amr_authorizedmodulesroles` (
  `amr_id` int(11) NOT NULL auto_increment,
  `sta_id` int(11) NOT NULL default '0',
  `mod_id` int(11) NOT NULL default '0',
  `rol_id` int(11) NOT NULL default '0',
  `amr_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `amr_createdby` varchar(255) NOT NULL default '',
  `amr_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `amr_updatedby` varchar(255) NOT NULL default '',
  `amr_hidden` int(1) NOT NULL default '0',
  `amr_deleted` int(1) NOT NULL default '0',
  `amr_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`amr_id`),
  KEY `amr_1` (`amr_id`,`sta_id`,`mod_id`,`rol_id`,`amr_hidden`,`amr_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `amr_authorizedmodulesroles` */

LOCK TABLES `amr_authorizedmodulesroles` WRITE;

UNLOCK TABLES;

/*Table structure for table `apg_authorizedpagesgroups` */

DROP TABLE IF EXISTS `apg_authorizedpagesgroups`;

CREATE TABLE `apg_authorizedpagesgroups` (
  `apg_id` int(11) NOT NULL auto_increment,
  `sta_id` int(11) NOT NULL default '0',
  `pag_id` int(11) NOT NULL default '0',
  `grp_id` int(11) NOT NULL default '0',
  `apg_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `apg_createdby` varchar(255) NOT NULL default '',
  `apg_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `apg_updatedby` varchar(255) NOT NULL default '',
  `apg_hidden` int(1) NOT NULL default '0',
  `apg_deleted` int(1) NOT NULL default '0',
  `apg_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`apg_id`),
  KEY `apg_1` (`apg_id`,`sta_id`,`pag_id`,`grp_id`,`apg_hidden`,`apg_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `apg_authorizedpagesgroups` */

LOCK TABLES `apg_authorizedpagesgroups` WRITE;

UNLOCK TABLES;

/*Table structure for table `apr_authorizedpagesroles` */

DROP TABLE IF EXISTS `apr_authorizedpagesroles`;

CREATE TABLE `apr_authorizedpagesroles` (
  `apr_id` int(11) NOT NULL auto_increment,
  `sta_id` int(11) NOT NULL default '0',
  `pag_id` int(11) NOT NULL default '0',
  `rol_id` int(11) NOT NULL default '0',
  `apr_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `apr_createdby` varchar(255) NOT NULL default '',
  `apr_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `apr_updatedby` varchar(255) NOT NULL default '',
  `apr_hidden` int(1) NOT NULL default '0',
  `apr_deleted` int(1) NOT NULL default '0',
  `apr_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`apr_id`),
  KEY `apr_1` (`apr_id`,`sta_id`,`pag_id`,`rol_id`,`apr_hidden`,`apr_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `apr_authorizedpagesroles` */

/*Table structure for table `asg_authorizedsitesgroups` */

DROP TABLE IF EXISTS `asg_authorizedsitesgroups`;

CREATE TABLE `asg_authorizedsitesgroups` (
  `asg_id` int(11) NOT NULL auto_increment,
  `sta_id` int(11) NOT NULL default '0',
  `sit_id` int(11) NOT NULL default '0',
  `grp_id` int(11) NOT NULL default '0',
  `asg_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `asg_createdby` varchar(255) NOT NULL default '',
  `asg_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `asg_updatedby` varchar(255) NOT NULL default '',
  `asg_hidden` int(1) NOT NULL default '0',
  `asg_deleted` int(1) NOT NULL default '0',
  `asg_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`asg_id`),
  KEY `asg_1` (`asg_id`,`sta_id`,`sit_id`,`grp_id`,`asg_hidden`,`asg_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `asg_authorizedsitesgroups` */

LOCK TABLES `asg_authorizedsitesgroups` WRITE;

UNLOCK TABLES;

/*Table structure for table `asr_authorizedsitesroles` */

DROP TABLE IF EXISTS `asr_authorizedsitesroles`;

CREATE TABLE `asr_authorizedsitesroles` (
  `asr_id` int(11) NOT NULL auto_increment,
  `sta_id` int(11) NOT NULL default '0',
  `sit_id` int(11) NOT NULL default '0',
  `rol_id` int(11) NOT NULL default '0',
  `asr_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `asr_createdby` varchar(255) NOT NULL default '',
  `asr_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `asr_updatedby` varchar(255) NOT NULL default '',
  `asr_hidden` int(1) NOT NULL default '0',
  `asr_deleted` int(1) NOT NULL default '0',
  `asr_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`asr_id`),
  KEY `asr_1` (`asr_id`,`sta_id`,`sit_id`,`rol_id`,`asr_hidden`,`asr_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `asr_authorizedsitesroles` */

LOCK TABLES `asr_authorizedsitesroles` WRITE;

UNLOCK TABLES;

/*Table structure for table `atg_authorizedtasksgroups` */

DROP TABLE IF EXISTS `atg_authorizedtasksgroups`;

CREATE TABLE `atg_authorizedtasksgroups` (
  `atg_id` int(11) NOT NULL auto_increment,
  `sta_id` int(11) NOT NULL default '0',
  `tas_id` int(11) NOT NULL default '0',
  `grp_id` int(11) NOT NULL default '0',
  `atg_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `atg_createdby` varchar(255) NOT NULL default '',
  `atg_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `atg_updatedby` varchar(255) NOT NULL default '',
  `atg_hidden` int(1) NOT NULL default '0',
  `atg_deleted` int(1) NOT NULL default '0',
  `atg_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`atg_id`),
  KEY `atg_1` (`atg_id`,`sta_id`,`tas_id`,`grp_id`,`atg_hidden`,`atg_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `atg_authorizedtasksgroups` */

LOCK TABLES `atg_authorizedtasksgroups` WRITE;

UNLOCK TABLES;

/*Table structure for table `atr_authorizedtasksroles` */

DROP TABLE IF EXISTS `atr_authorizedtasksroles`;

CREATE TABLE `atr_authorizedtasksroles` (
  `atr_id` int(11) NOT NULL auto_increment,
  `sta_id` int(11) NOT NULL default '0',
  `tas_id` int(11) NOT NULL default '0',
  `rol_id` int(11) NOT NULL default '0',
  `atr_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `atr_createdby` varchar(255) NOT NULL default '',
  `atr_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `atr_updatedby` varchar(255) NOT NULL default '',
  `atr_hidden` int(1) NOT NULL default '0',
  `atr_deleted` int(1) NOT NULL default '0',
  `atr_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`atr_id`),
  KEY `atr_1` (`atr_id`,`sta_id`,`tas_id`,`rol_id`,`atr_hidden`,`atr_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `atr_authorizedtasksroles` */

LOCK TABLES `atr_authorizedtasksroles` WRITE;

UNLOCK TABLES;

/*Table structure for table `doc_documents` */

DROP TABLE IF EXISTS `doc_documents`;

CREATE TABLE `doc_documents` (
  `doc_id` int(11) NOT NULL auto_increment,
  `sta_id` int(11) NOT NULL default '0',
  `lng_id` int(11) NOT NULL default '0',
  `sit_id` int(11) NOT NULL default '0',
  `pag_id` int(11) NOT NULL default '0',
  `doc_parentid` int(11) NOT NULL default '0',
  `doc_order` int(11) NOT NULL default '0',
  `doc_type` int(11) NOT NULL default '0',
  `doc_title` varchar(255) NOT NULL default '',
  `doc_alias` varchar(255) NOT NULL default '',
  `doc_description` longtext NOT NULL,
  `doc_contenttype` varchar(255) NOT NULL default '',
  `doc_contentsize` int(11) NOT NULL default '0',
  `doc_version` int(11) NOT NULL default '0',
  `doc_charset` varchar(255) NOT NULL default '',
  `doc_extension` varchar(10) NOT NULL default '',
  `doc_path` varchar(255) NOT NULL default '',
  `doc_checkoutusrid` int(11) NOT NULL default '0',
  `doc_checkoutdate` datetime NOT NULL default '2006-01-01 01:01:01',
  `doc_checkoutexpiredate` datetime NOT NULL default '2006-01-01 01:01:01',
  `doc_lastvieweddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `doc_lastviewedby` varchar(255) NOT NULL default '',
  `doc_isdirty` int(1) NOT NULL default '0',
  `doc_isdelivered` int(1) NOT NULL default '0',
  `doc_issigned` int(1) NOT NULL default '0',
  `doc_iscertified` int(1) NOT NULL default '0',
  `doc_deliverdate` datetime NOT NULL default '2006-01-01 01:01:01',
  `doc_deliverby` varchar(255) NOT NULL default '',
  `doc_deliverto` varchar(255) NOT NULL default '',
  `doc_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `doc_createdby` varchar(255) NOT NULL default '',
  `doc_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `doc_updatedby` varchar(255) NOT NULL default '',
  `doc_hidden` int(1) NOT NULL default '0',
  `doc_deleted` int(1) NOT NULL default '0',
  `doc_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`doc_id`),
  KEY `doc_1` (`doc_id`,`sta_id`,`lng_id`,`doc_parentid`),
  KEY `doc_2` (`doc_id`,`sta_id`,`lng_id`,`doc_parentid`,`doc_alias`,`doc_hidden`,`doc_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `doc_documents` */

LOCK TABLES `doc_documents` WRITE;

UNLOCK TABLES;

/*Table structure for table `grp_groups` */

DROP TABLE IF EXISTS `grp_groups`;

CREATE TABLE `grp_groups` (
  `grp_id` int(11) NOT NULL auto_increment,
  `sta_id` int(11) NOT NULL default '0',
  `lng_id` int(11) NOT NULL default '0',
  `grp_parentid` int(11) NOT NULL default '0',
  `grp_order` int(11) NOT NULL default '0',
  `grp_title` varchar(255) NOT NULL default '',
  `grp_alias` varchar(255) NOT NULL default '',
  `grp_description` longtext NOT NULL,
  `grp_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `grp_createdby` varchar(255) NOT NULL default '',
  `grp_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `grp_updatedby` varchar(255) NOT NULL default '',
  `grp_hidden` int(1) NOT NULL default '0',
  `grp_deleted` int(1) NOT NULL default '0',
  `grp_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`grp_id`),
  KEY `grp_1` (`grp_id`,`sta_id`,`lng_id`,`grp_parentid`,`grp_alias`,`grp_hidden`,`grp_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `grp_groups` */

/*Table structure for table `lng_language` */

DROP TABLE IF EXISTS `lng_language`;

CREATE TABLE `lng_language` (
  `lng_id` int(11) NOT NULL auto_increment,
  `lng_parentid` int(11) NOT NULL default '0',
  `lng_order` int(11) NOT NULL default '0',
  `lng_title` varchar(255) NOT NULL default '',
  `lng_alias` varchar(255) NOT NULL default '',
  `lng_description` longtext NOT NULL,
  `lng_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `lng_createdby` varchar(255) NOT NULL default '',
  `lng_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `lng_updatedby` varchar(255) NOT NULL default '',
  `lng_hidden` int(1) NOT NULL default '0',
  `lng_deleted` int(1) NOT NULL default '0',
  `lng_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`lng_id`),
  KEY `lng_1` (`lng_id`,`lng_parentid`),
  KEY `lng_2` (`lng_id`,`lng_parentid`,`lng_alias`,`lng_hidden`,`lng_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `lng_language` */

/*Table structure for table `mde_moduledefinitions` */

DROP TABLE IF EXISTS `mde_moduledefinitions`;

CREATE TABLE `mde_moduledefinitions` (
  `mde_id` int(11) NOT NULL auto_increment,
  `sta_id` int(11) NOT NULL default '0',
  `sit_id` int(11) NOT NULL default '0',
  `lng_id` int(11) NOT NULL default '0',
  `mde_parentid` int(11) NOT NULL default '0',
  `mde_order` int(11) NOT NULL default '0',
  `mde_title` varchar(255) NOT NULL default '',
  `mde_alias` varchar(255) NOT NULL default '',
  `mde_description` longtext NOT NULL,
  `mde_src` varchar(255) NOT NULL default '',
  `mde_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `mde_createdby` varchar(255) NOT NULL default '',
  `mde_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `mde_updatedby` varchar(255) NOT NULL default '',
  `mde_hidden` int(1) NOT NULL default '0',
  `mde_deleted` int(1) NOT NULL default '0',
  `mde_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`mde_id`),
  KEY `mde_1` (`mde_id`,`sit_id`,`sta_id`,`lng_id`,`mde_parentid`,`mde_alias`,`mde_hidden`,`mde_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `mde_moduledefinitions` */

/*Table structure for table `mod_modules` */

DROP TABLE IF EXISTS `mod_modules`;

CREATE TABLE `mod_modules` (
  `mod_id` int(11) NOT NULL auto_increment,
  `sta_id` int(11) NOT NULL default '0',
  `sit_id` int(11) NOT NULL default '0',
  `pag_id` int(11) NOT NULL default '0',
  `mde_id` int(11) NOT NULL default '0',
  `lng_id` int(11) NOT NULL default '0',
  `mod_parentid` int(11) NOT NULL default '0',
  `mod_order` int(11) NOT NULL default '0',
  `mod_title` varchar(255) NOT NULL default '',
  `mod_alias` varchar(255) NOT NULL default '',
  `mod_description` longtext NOT NULL,
  `mod_pane` varchar(255) NOT NULL default '',
  `mod_cachetime` int(11) NOT NULL default '0',
  `mod_theme` varchar(255) NOT NULL default '',
  `mod_skin` varchar(255) NOT NULL default '',
  `mod_editsrc` varchar(255) NOT NULL default '',
  `mod_secure` int(1) NOT NULL default '0',
  `mod_allpages` int(1) NOT NULL default '0',
  `mod_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `mod_createdby` varchar(255) NOT NULL default '',
  `mod_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `mod_updatedby` varchar(255) NOT NULL default '',
  `mod_hidden` int(1) NOT NULL default '0',
  `mod_deleted` int(1) NOT NULL default '0',
  `mod_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`mod_id`),
  KEY `mod_1` (`mod_id`,`sit_id`,`pag_id`,`sta_id`,`lng_id`,`mod_parentid`,`mod_alias`,`mod_hidden`,`mod_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `mod_modules` */

/*Table structure for table `obd_objectdata` */

DROP TABLE IF EXISTS `obd_objectdata`;

CREATE TABLE `obd_objectdata` (
  `obd_id` int(11) NOT NULL auto_increment,
  `sta_id` int(11) NOT NULL default '0',
  `lng_id` int(11) NOT NULL default '0',
  `sit_id` int(11) NOT NULL default '0',
  `pag_id` int(11) NOT NULL default '0',
  `mod_id` int(11) NOT NULL default '0',
  `obd_parentid` int(11) NOT NULL default '0',
  `obd_order` int(11) NOT NULL default '0',
  `obd_type` int(11) NOT NULL default '0',
  `obd_title` varchar(255) NOT NULL default '',
  `obd_alias` varchar(255) NOT NULL default '',
  `obd_description` longtext NOT NULL,
  `obd_varchar1` longtext NOT NULL default '',
  `obd_varchar2` longtext NOT NULL default '',
  `obd_varchar3` longtext NOT NULL default '',
  `obd_varchar4` longtext NOT NULL default '',
  `obd_varchar5` longtext NOT NULL default '',
  `obd_varchar6` longtext NOT NULL default '',
  `obd_varchar7` longtext NOT NULL default '',
  `obd_varchar8` longtext NOT NULL default '',
  `obd_varchar9` longtext NOT NULL default '',
  `obd_varchar10` longtext NOT NULL default '',
  `obd_varchar11` longtext NOT NULL default '',
  `obd_varchar12` longtext NOT NULL default '',
  `obd_varchar13` longtext NOT NULL default '',
  `obd_varchar14` longtext NOT NULL default '',
  `obd_varchar15` longtext NOT NULL default '',
  `obd_varchar16` longtext NOT NULL default '',
  `obd_varchar17` longtext NOT NULL default '',
  `obd_varchar18` longtext NOT NULL default '',
  `obd_varchar19` longtext NOT NULL default '',
  `obd_varchar20` longtext NOT NULL default '',
  `obd_varchar21` longtext NOT NULL default '',
  `obd_varchar22` longtext NOT NULL default '',
  `obd_varchar23` longtext NOT NULL default '',
  `obd_varchar24` longtext NOT NULL default '',
  `obd_varchar25` longtext NOT NULL default '',
  `obd_varchar26` longtext NOT NULL default '',
  `obd_varchar27` longtext NOT NULL default '',
  `obd_varchar28` longtext NOT NULL default '',
  `obd_varchar29` longtext NOT NULL default '',
  `obd_varchar30` longtext NOT NULL default '',
  `obd_varchar31` longtext NOT NULL default '',
  `obd_varchar32` longtext NOT NULL default '',
  `obd_varchar33` longtext NOT NULL default '',
  `obd_varchar34` longtext NOT NULL default '',
  `obd_varchar35` longtext NOT NULL default '',
  `obd_varchar36` longtext NOT NULL default '',
  `obd_varchar37` longtext NOT NULL default '',
  `obd_varchar38` longtext NOT NULL default '',
  `obd_varchar39` longtext NOT NULL default '',
  `obd_varchar40` longtext NOT NULL default '',
  `obd_varchar41` longtext NOT NULL default '',
  `obd_varchar42` longtext NOT NULL default '',
  `obd_varchar43` longtext NOT NULL default '',
  `obd_varchar44` longtext NOT NULL default '',
  `obd_varchar45` longtext NOT NULL default '',
  `obd_varchar46` longtext NOT NULL default '',
  `obd_varchar47` longtext NOT NULL default '',
  `obd_varchar48` longtext NOT NULL default '',
  `obd_varchar49` longtext NOT NULL default '',
  `obd_varchar50` longtext NOT NULL default '',
  `obd_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `obd_createdby` varchar(255) NOT NULL default '',
  `obd_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `obd_updatedby` varchar(255) NOT NULL default '',
  `obd_hidden` int(1) NOT NULL default '0',
  `obd_deleted` int(1) NOT NULL default '0',
  `obd_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`obd_id`),
  KEY `obd_1` (`obd_id`,`sta_id`,`lng_id`,`obd_parentid`,`sit_id`,`pag_id`,`obd_type`),
  KEY `obd_2` (`obd_id`,`sta_id`,`lng_id`,`obd_parentid`,`sit_id`,`pag_id`,`obd_type`,`obd_alias`,`obd_hidden`,`obd_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `obd_objectdata` */

/*Table structure for table `pag_pages` */

DROP TABLE IF EXISTS `pag_pages`;

CREATE TABLE `pag_pages` (
  `pag_id` int(11) NOT NULL auto_increment,
  `sta_id` int(11) NOT NULL default '0',
  `sit_id` int(11) NOT NULL default '0',
  `lng_id` int(11) NOT NULL default '0',
  `pag_parentid` int(11) NOT NULL default '0',
  `pag_order` int(11) NOT NULL default '0',
  `pag_title` varchar(255) NOT NULL default '',
  `pag_alias` varchar(255) NOT NULL default '',
  `pag_description` longtext NOT NULL,
  `pag_theme` varchar(255) NOT NULL default '',
  `pag_skin` varchar(255) NOT NULL default '',
  `pag_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `pag_createdby` varchar(255) NOT NULL default '',
  `pag_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `pag_updatedby` varchar(255) NOT NULL default '',
  `pag_hidden` int(1) NOT NULL default '0',
  `pag_deleted` int(1) NOT NULL default '0',
  `pag_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`pag_id`),
  KEY `pag_1` (`pag_id`,`sit_id`,`sta_id`,`lng_id`,`pag_parentid`,`pag_alias`,`pag_hidden`,`pag_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `pag_pages` */

/*Table structure for table `rol_roles` */

DROP TABLE IF EXISTS `rol_roles`;

CREATE TABLE `rol_roles` (
  `rol_id` int(11) NOT NULL auto_increment,
  `sta_id` int(11) NOT NULL default '0',
  `lng_id` int(11) NOT NULL default '0',
  `rol_parentid` int(11) NOT NULL default '0',
  `rol_order` int(11) NOT NULL default '0',
  `rol_title` varchar(255) NOT NULL default '',
  `rol_alias` varchar(255) NOT NULL default '',
  `rol_description` longtext NOT NULL,
  `rol_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `rol_createdby` varchar(255) NOT NULL default '',
  `rol_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `rol_updatedby` varchar(255) NOT NULL default '',
  `rol_hidden` int(1) NOT NULL default '0',
  `rol_deleted` int(1) NOT NULL default '0',
  `rol_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`rol_id`),
  KEY `rol_1` (`rol_id`,`sta_id`,`lng_id`,`rol_parentid`,`rol_alias`,`rol_hidden`,`rol_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `rol_roles` */

/*Table structure for table `sit_sites` */

DROP TABLE IF EXISTS `sit_sites`;

CREATE TABLE `sit_sites` (
  `sit_id` int(11) NOT NULL auto_increment,
  `sta_id` int(11) NOT NULL default '0',
  `lng_id` int(11) NOT NULL default '0',
  `sit_parentid` int(11) NOT NULL default '0',
  `sit_order` int(11) NOT NULL default '0',
  `sit_title` varchar(255) NOT NULL default '',
  `sit_alias` varchar(255) NOT NULL default '',
  `sit_description` longtext NOT NULL,
  `sit_theme` varchar(255) NOT NULL default '',
  `sit_skin` varchar(255) NOT NULL default '',
  `sit_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `sit_createdby` varchar(255) NOT NULL default '',
  `sit_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `sit_updatedby` varchar(255) NOT NULL default '',
  `sit_hidden` int(1) NOT NULL default '0',
  `sit_deleted` int(1) NOT NULL default '0',
  `sit_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`sit_id`),
  KEY `sit_1` (`sit_id`,`sta_id`,`lng_id`,`sit_parentid`,`sit_alias`,`sit_hidden`,`sit_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `sit_sites` */

/*Table structure for table `sta_status` */

DROP TABLE IF EXISTS `sta_status`;

CREATE TABLE `sta_status` (
  `sta_id` int(11) NOT NULL auto_increment,
  `lng_id` int(11) NOT NULL default '0',
  `sta_parentid` int(11) NOT NULL default '0',
  `sta_order` int(11) NOT NULL default '0',
  `sta_title` varchar(255) NOT NULL default '',
  `sta_alias` varchar(255) NOT NULL default '',
  `sta_description` longtext NOT NULL,
  `sta_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `sta_createdby` varchar(255) NOT NULL default '',
  `sta_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `sta_updatedby` varchar(255) NOT NULL default '',
  `sta_hidden` int(1) NOT NULL default '0',
  `sta_deleted` int(1) NOT NULL default '0',
  `sta_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`sta_id`),
  KEY `sta_1` (`sta_id`,`lng_id`,`sta_parentid`,`sta_alias`,`sta_hidden`,`sta_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `sta_status` */

/*Table structure for table `tas_tasks` */

DROP TABLE IF EXISTS `tas_tasks`;

CREATE TABLE `tas_tasks` (
  `tas_id` int(11) NOT NULL auto_increment,
  `sta_id` int(11) NOT NULL default '0',
  `lng_id` int(11) NOT NULL default '0',
  `tas_parentid` int(11) NOT NULL default '0',
  `tas_order` int(11) NOT NULL default '0',
  `tas_title` varchar(255) NOT NULL default '',
  `tas_alias` varchar(255) NOT NULL default '',
  `tas_description` longtext NOT NULL,
  `tas_theme` varchar(255) NOT NULL default '',
  `tas_skin` varchar(255) NOT NULL default '',
  `tas_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `tas_createdby` varchar(255) NOT NULL default '',
  `tas_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `tas_updatedby` varchar(255) NOT NULL default '',
  `tas_hidden` int(1) NOT NULL default '0',
  `tas_deleted` int(1) NOT NULL default '0',
  `tas_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`tas_id`),
  KEY `tas_1` (`tas_id`,`sta_id`,`lng_id`,`tas_parentid`,`tas_alias`,`tas_hidden`,`tas_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `tas_tasks` */

LOCK TABLES `tas_tasks` WRITE;

UNLOCK TABLES;

/*Table structure for table `ugr_usersgroups` */

DROP TABLE IF EXISTS `ugr_usersgroups`;

CREATE TABLE `ugr_usersgroups` (
  `ugr_id` int(11) NOT NULL auto_increment,
  `sta_id` int(11) NOT NULL default '0',
  `usr_id` int(11) NOT NULL default '0',
  `grp_id` int(11) NOT NULL default '0',
  `ugr_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `ugr_createdby` varchar(255) NOT NULL default '',
  `ugr_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `ugr_updatedby` varchar(255) NOT NULL default '',
  `ugr_hidden` int(1) NOT NULL default '0',
  `ugr_deleted` int(1) NOT NULL default '0',
  `ugr_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`ugr_id`),
  KEY `ugr_1` (`ugr_id`,`sta_id`,`usr_id`,`grp_id`,`ugr_hidden`,`ugr_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `ugr_usersgroups` */


/*Table structure for table `uro_usersroles` */

DROP TABLE IF EXISTS `uro_usersroles`;

CREATE TABLE `uro_usersroles` (
  `uro_id` int(11) NOT NULL auto_increment,
  `sta_id` int(11) NOT NULL default '0',
  `usr_id` int(11) NOT NULL default '0',
  `rol_id` int(11) NOT NULL default '0',
  `uro_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `uro_createdby` varchar(255) NOT NULL default '',
  `uro_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `uro_updatedby` varchar(255) NOT NULL default '',
  `uro_hidden` int(1) NOT NULL default '0',
  `uro_deleted` int(1) NOT NULL default '0',
  `uro_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`uro_id`),
  KEY `uro_1` (`uro_id`,`sta_id`,`usr_id`,`rol_id`,`uro_hidden`,`uro_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `uro_usersroles` */

/*Table structure for table `usr_users` */

DROP TABLE IF EXISTS `usr_users`;

CREATE TABLE `usr_users` (
  `usr_id` int(11) NOT NULL auto_increment,
  `sta_id` int(11) NOT NULL default '0',
  `lng_id` int(11) NOT NULL default '0',
  `ust_id` int(11) NOT NULL default '0',
  `usr_parentid` int(11) NOT NULL default '0',
  `usr_order` int(11) NOT NULL default '0',
  `usr_title` varchar(255) NOT NULL default '',
  `usr_alias` varchar(255) NOT NULL default '',
  `usr_description` longtext NOT NULL,
  `usr_loginname` varchar(255) NOT NULL default '',
  `usr_password` varchar(255) NOT NULL default '',
  `usr_signature` varchar(255) NOT NULL default '',
  `usr_firstname` varchar(255) NOT NULL default '',
  `usr_lastname` varchar(255) NOT NULL default '',
  `usr_tag` varchar(255) NOT NULL default '',
  `usr_co` varchar(255) NOT NULL default '',
  `usr_address` varchar(255) NOT NULL default '',
  `usr_postalcode` varchar(255) NOT NULL default '',
  `usr_city` varchar(255) NOT NULL default '',
  `usr_country` varchar(255) NOT NULL default '',
  `usr_phone1` varchar(255) NOT NULL default '',
  `usr_phone2` varchar(255) NOT NULL default '',
  `usr_phone3` varchar(255) NOT NULL default '',
  `usr_fax` varchar(255) NOT NULL default '',
  `usr_mobile` varchar(255) NOT NULL default '',
  `usr_url1` varchar(255) NOT NULL default '',
  `usr_url2` varchar(255) NOT NULL default '',
  `usr_mail1` varchar(255) NOT NULL default '',
  `usr_mail2` varchar(255) NOT NULL default '',
  `usr_picturepath` varchar(255) NOT NULL default '',
  `usr_signaturepath` varchar(255) NOT NULL default '',
  `usr_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `usr_createdby` varchar(255) NOT NULL default '',
  `usr_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `usr_updatedby` varchar(255) NOT NULL default '',
  `usr_hidden` int(1) NOT NULL default '0',
  `usr_deleted` int(1) NOT NULL default '0',
  `usr_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`usr_id`),
  KEY `usr_1` (`usr_id`,`sta_id`,`lng_id`,`usr_parentid`,`usr_loginname`,`usr_password`),
  KEY `usr_2` (`usr_id`,`sta_id`,`lng_id`,`usr_parentid`,`usr_loginname`,`usr_password`,`usr_hidden`,`usr_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `usr_users` */

/*Table structure for table `ust_usertypes` */

DROP TABLE IF EXISTS `ust_usertypes`;

CREATE TABLE `ust_usertypes` (
  `ust_id` int(11) NOT NULL auto_increment,
  `sta_id` int(11) NOT NULL default '0',
  `lng_id` int(11) NOT NULL default '0',
  `ust_parentid` int(11) NOT NULL default '0',
  `ust_order` int(11) NOT NULL default '0',
  `ust_title` varchar(255) NOT NULL default '',
  `ust_alias` varchar(255) NOT NULL default '',
  `ust_description` longtext NOT NULL,
  `ust_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `ust_createdby` varchar(255) NOT NULL default '',
  `ust_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `ust_updatedby` varchar(255) NOT NULL default '',
  `ust_hidden` int(1) NOT NULL default '0',
  `ust_deleted` int(1) NOT NULL default '0',
  `ust_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`ust_id`),
  KEY `ust_1` (`ust_id`,`sta_id`,`lng_id`,`ust_parentid`,`ust_alias`,`ust_hidden`,`ust_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `ust_usertypes` */

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;


ALTER TABLE `mod_modules` ADD COLUMN `mod_width` varchar(20) NOT NULL default '' AFTER `mod_editsrc`;                              
ALTER TABLE `mod_modules` ADD COLUMN `mod_height` varchar(20) NOT NULL default '' AFTER `mod_width`;                               
ALTER TABLE `mod_modules` ADD COLUMN `mod_editwidth` varchar(20) NOT NULL default '' AFTER `mod_height`;                               
ALTER TABLE `mod_modules` ADD COLUMN `mod_editheight` varchar(20) NOT NULL default '' AFTER `mod_editwidth`;

ALTER TABLE `doc_documents` ADD COLUMN `mod_id` int NOT NULL default 0 AFTER `pag_id`;  

ALTER TABLE mod_modules ADD mod_ssl INT NOT NULL DEFAULT 0;

DROP TABLE IF EXISTS `adg_authorizeddocumentsgroups`;

CREATE TABLE `adg_authorizeddocumentsgroups` (
  `adg_id` int(11) NOT NULL auto_increment,
  `sta_id` int(11) NOT NULL default '0',
  `doc_id` int(11) NOT NULL default '0',
  `grp_id` int(11) NOT NULL default '0',
  `adg_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `adg_createdby` varchar(255) NOT NULL default '',
  `adg_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `adg_updatedby` varchar(255) NOT NULL default '',
  `adg_hidden` int(1) NOT NULL default '0',
  `adg_deleted` int(1) NOT NULL default '0',
  `adg_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`adg_id`),
  KEY `adg_1` (`adg_id`,`sta_id`,`doc_id`,`grp_id`,`adg_hidden`,`adg_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `adr_authorizeddocumentsroles`;

CREATE TABLE `adr_authorizeddocumentsroles` (
  `adr_id` int(11) NOT NULL auto_increment,
  `sta_id` int(11) NOT NULL default '0',
  `doc_id` int(11) NOT NULL default '0',
  `rol_id` int(11) NOT NULL default '0',
  `adr_createddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `adr_createdby` varchar(255) NOT NULL default '',
  `adr_updateddate` datetime NOT NULL default '2006-01-01 01:01:01',
  `adr_updatedby` varchar(255) NOT NULL default '',
  `adr_hidden` int(1) NOT NULL default '0',
  `adr_deleted` int(1) NOT NULL default '0',
  `adr_ts` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`adr_id`),
  KEY `adr_1` (`adr_id`,`sta_id`,`doc_id`,`rol_id`,`adr_hidden`,`adr_deleted`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;


ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar1` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar2` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar3` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar4` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar5` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar6` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar7` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar8` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar9` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar10` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar11` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar12` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar13` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar14` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar15` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar16` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar17` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar18` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar19` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar20` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar21` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar22` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar23` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar24` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar25` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar26` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar27` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar28` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar29` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar30` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar31` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar32` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar33` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar34` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar35` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar36` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar37` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar38` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar39` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar40` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar41` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar42` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar43` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar44` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar45` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar46` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar47` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar48` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar49` longtext NOT NULL default '';
ALTER TABLE `obd_objectdata` MODIFY COLUMN `obd_varchar50` longtext NOT NULL default '';

