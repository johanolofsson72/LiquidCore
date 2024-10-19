
DROP PROCEDURE IF EXISTS `delete_mde_moduledefinitions`;
DELIMITER $$

CREATE PROCEDURE `delete_mde_moduledefinitions`(mdeid INT)
BEGIN
START TRANSACTION;
  UPDATE mde_moduledefinitions SET mde_deleted = 1 WHERE mde_id = mdeid;
  CALL sort_mde_modeldefinitions();
COMMIT;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS `delete_agg_aggregation`;
DELIMITER $$

CREATE PROCEDURE `delete_agg_aggregation`(aggid INT)
BEGIN
START TRANSACTION;
  UPDATE agg_aggregation SET agg_deleted = 1 WHERE agg_id = aggid;
COMMIT;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS `delete_doc_documents`;
DELIMITER $$

CREATE PROCEDURE `delete_doc_documents`(docid INT)
BEGIN
START TRANSACTION;
  UPDATE doc_documents SET doc_deleted = 1 WHERE doc_id = docid;
COMMIT;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS `delete_mdi_modelitems`;
DELIMITER $$

CREATE PROCEDURE `delete_mdi_modelitems`(mdiid INT)
BEGIN
START TRANSACTION;
  UPDATE mdi_modelitems SET mdi_deleted = 1 WHERE mdi_id = mdiid;
COMMIT;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS `delete_mdl_model`;
DELIMITER $$

CREATE PROCEDURE `delete_mdl_model`(mdlid INT)
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i INT;
   DECLARE cur CURSOR FOR SELECT mdi_id FROM mdi_modelitems WHERE mdl_id = mdlid;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
   OPEN cur;
   read_loop: LOOP
    FETCH cur INTO i;
    IF done THEN
      LEAVE read_loop;
    END IF;
    START TRANSACTION;
      UPDATE mdi_modelitems SET mdi_deleted = 1 WHERE mdi_id = i;
    COMMIT;
   END LOOP;
  CLOSE cur;
START TRANSACTION;
  UPDATE mdl_model SET mdl_deleted = 1 WHERE mdl_id = mdlid;
  CALL sort_mdl_model();
  CALL sort_mdi_modelitems();
COMMIT;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS `delete_mod_modules`;
DELIMITER $$

CREATE PROCEDURE `delete_mod_modules`(modid INT)
BEGIN
START TRANSACTION;
  UPDATE amr_authorizedmodulesroles SET amr_deleted = 1 WHERE mod_id = modid;
  UPDATE agg_aggregation SET agg_deleted = 1 WHERE mod_id = modid;
  UPDATE set_settings SET set_deleted = 1 WHERE mod_id = modid;
  UPDATE mod_modules SET mod_deleted = 1 WHERE mod_id = modid;
  CALL sort_set_settings();
  CALL sort_mod_modules();
COMMIT;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS `delete_mod_modules_recursive`;
DELIMITER $$

CREATE PROCEDURE `delete_mod_modules_recursive`(modid INT)
BEGIN
START TRANSACTION;
  CALL delete_obd_objectdata_by_modid_recursive(modid);
  UPDATE amr_authorizedmodulesroles SET amr_deleted = 1 WHERE mod_id = modid;
  UPDATE agg_aggregation SET agg_deleted = 1 WHERE mod_id = modid;
  UPDATE set_settings SET set_deleted = 1 WHERE mod_id = modid;
  UPDATE mod_modules SET mod_deleted = 1 WHERE mod_id = modid;
  CALL sort_set_settings();
  CALL sort_mod_modules();
COMMIT;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS `delete_mod_modules_by_pagid_recursive`;
DELIMITER $$

CREATE PROCEDURE `delete_mod_modules_by_pagid_recursive`(pagid INT)
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i INT;
   DECLARE cur CURSOR FOR SELECT mod_id FROM mod_modules WHERE pag_id = pagid;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
   OPEN cur;
   read_loop: LOOP
    FETCH cur INTO i;
    IF done THEN
      LEAVE read_loop;
    END IF;
    CALL delete_obd_objectdata_by_modid_recursive(i);
    CALL delete_mod_modules(i);
   END LOOP;
  CLOSE cur;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS `delete_obd_objectdata`;
DELIMITER $$

CREATE PROCEDURE `delete_obd_objectdata`(obdid INT)
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i INT;
   DECLARE cur CURSOR FOR SELECT obd_id FROM obd_objectdata WHERE obd_parentid = obdid;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
   OPEN cur;
   read_loop: LOOP
    FETCH cur INTO i;
    IF done THEN
      LEAVE read_loop;
    END IF;
    CALL delete_obd_objectdata(i);
    START TRANSACTION;
      UPDATE set_settings SET set_deleted = 1 WHERE set_pointer = i;
      UPDATE obd_objectdata SET obd_deleted = 1 WHERE obd_id = i;
    COMMIT;
   END LOOP;
  CLOSE cur;
  START TRANSACTION;
    UPDATE set_settings SET set_deleted = 1 WHERE set_pointer = obdid;
    UPDATE obd_objectdata SET obd_deleted = 1 WHERE obd_id = obdid;
    CALL sort_obd_objectdata();
  COMMIT;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS `delete_obd_objectdata_by_modid_recursive`;
DELIMITER $$

CREATE PROCEDURE `delete_obd_objectdata_by_modid_recursive`(modid INT)
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i INT;
   DECLARE cur CURSOR FOR SELECT obd_id FROM obd_objectdata WHERE mod_id = modid;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
   OPEN cur;
   read_loop: LOOP
    FETCH cur INTO i;
    IF done THEN
      LEAVE read_loop;
    END IF;
    CALL delete_obd_objectdata(i);
   END LOOP;
  CLOSE cur;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS `delete_pag_pages`;
DELIMITER $$

CREATE PROCEDURE `delete_pag_pages`(pagid INT)
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i INT;
   DECLARE cur CURSOR FOR SELECT pag_id FROM pag_pages WHERE pag_parentid = pagid;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
   OPEN cur;
   read_loop: LOOP
    FETCH cur INTO i;
    IF done THEN
      LEAVE read_loop;
    END IF;
    CALL delete_pag_pages(i);
    START TRANSACTION;
      UPDATE apr_authorizedpagesroles SET apr_deleted = 1 WHERE pag_id = i;
      UPDATE set_settings SET set_deleted = 1 WHERE pag_id = i;
      UPDATE pag_pages SET pag_deleted = 1 WHERE pag_id = i;
    COMMIT;
   END LOOP;
  CLOSE cur;
  START TRANSACTION;
    UPDATE apr_authorizedpagesroles SET apr_deleted = 1 WHERE pag_id = pagid;
    UPDATE set_settings SET set_deleted = 1 WHERE pag_id = pagid;
    UPDATE pag_pages SET pag_deleted = 1 WHERE pag_id = pagid;
    CALL sort_pag_pages();
  COMMIT;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS `delete_pag_pages_recursive`;
DELIMITER $$

CREATE PROCEDURE `delete_pag_pages_recursive`(pagid INT)
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i INT;
   DECLARE cur CURSOR FOR SELECT pag_id FROM pag_pages WHERE pag_parentid = pagid;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
   OPEN cur;
   read_loop: LOOP
    FETCH cur INTO i;
    IF done THEN
      LEAVE read_loop;
    END IF;
    CALL delete_mod_modules_by_pagid_recursive(i);
    CALL delete_pag_pages(i);
    START TRANSACTION;
      UPDATE apr_authorizedpagesroles SET apr_deleted = 1 WHERE pag_id = i;
      UPDATE set_settings SET set_deleted = 1 WHERE pag_id = i;
      UPDATE pag_pages SET pag_deleted = 1 WHERE pag_id = i;
    COMMIT;
   END LOOP;
  CLOSE cur;
  CALL delete_mod_modules_by_pagid_recursive(pagid);
  START TRANSACTION;
    UPDATE apr_authorizedpagesroles SET apr_deleted = 1 WHERE pag_id = pagid;
    UPDATE set_settings SET set_deleted = 1 WHERE pag_id = pagid;
    UPDATE pag_pages SET pag_deleted = 1 WHERE pag_id = pagid;
    CALL sort_pag_pages();
  COMMIT;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS `delete_rol_roles`;
DELIMITER $$

CREATE PROCEDURE `delete_rol_roles`(rolid INT)
BEGIN
START TRANSACTION;
  UPDATE asr_authorizedsitesroles SET asr_deleted = 1 WHERE rol_id = rolid;
  UPDATE adr_authorizeddocumentsroles SET adr_deleted = 1 WHERE rol_id = rolid;
  UPDATE amr_authorizedmodulesroles SET amr_deleted = 1 WHERE rol_id = rolid;
  UPDATE apr_authorizedpagesroles SET apr_deleted = 1 WHERE rol_id = rolid;
  UPDATE atr_authorizedtasksroles SET atr_deleted = 1 WHERE rol_id = rolid;
  UPDATE uro_usersroles SET uro_deleted = 1 WHERE rol_id = rolid;
  UPDATE rol_roles SET rol_deleted = 1 WHERE rol_id = rolid;
  CALL sort_rol_roles();
COMMIT;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS `delete_set_settings`;
DELIMITER $$

CREATE PROCEDURE `delete_set_settings`(setid INT)
BEGIN
START TRANSACTION;
  UPDATE set_settings SET set_deleted = 1 WHERE set_id = setid;
  CALL sort_set_settings();
COMMIT;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS `delete_sit_sites`;
DELIMITER $$

CREATE PROCEDURE `delete_sit_sites`(sitid INT)
BEGIN
START TRANSACTION;
  UPDATE asr_authorizedsitesroles SET asr_deleted = 1 WHERE sit_id = sitid;
  UPDATE set_settings SET set_deleted = 1 WHERE sit_id = sitid;
  UPDATE sit_sites SET sit_deleted = 1 WHERE sit_id = sitid;
  CALL sort_sit_sites();
COMMIT;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS `delete_sta_status`;
DELIMITER $$

CREATE PROCEDURE `delete_sta_status`(staid INT)
BEGIN
START TRANSACTION;
  UPDATE sta_status SET sta_deleted = 1 WHERE sta_id = staid;
  CALL sort_sta_status();
COMMIT;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS `delete_tas_tasks`;
DELIMITER $$

CREATE PROCEDURE `delete_tas_tasks`(tasid INT)
BEGIN
START TRANSACTION;
  UPDATE tas_tasks SET tas_deleted = 1 WHERE tas_id = tasid;
COMMIT;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS `delete_usr_users`;
DELIMITER $$

CREATE PROCEDURE `delete_usr_users`(usrid INT)
BEGIN
START TRANSACTION;
  UPDATE uro_usersroles SET uro_deleted = 1 WHERE usr_id = usrid;
  UPDATE usr_users SET usr_deleted = 1 WHERE usr_id = usrid;
  CALL sort_usr_users();
COMMIT;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS `delete_ust_usertypes`;
DELIMITER $$

CREATE PROCEDURE `delete_ust_usertypes`(ustid INT)
BEGIN
START TRANSACTION;
  UPDATE ust_usertypes SET ust_deleted = 1 WHERE ust_id = ustid;
  CALL sort_ust_usertypes();
COMMIT;
END $$
DELIMITER ;


DROP TABLE IF EXISTS `adg_authorizeddocumentsgroups`;
DROP TABLE IF EXISTS `amg_authorizedmodulesgroups`;
DROP TABLE IF EXISTS `apg_authorizedpagesgroups`;
DROP TABLE IF EXISTS `asg_authorizedsitesgroups`;
DROP TABLE IF EXISTS `atg_authorizedtasksgroups`;
DROP TABLE IF EXISTS `adg_authorizeddocumentsgroups`;
DROP TABLE IF EXISTS `ugr_usersgroups`;
DROP TABLE IF EXISTS `grp_groups`;

