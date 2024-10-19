DELIMITER $$

DROP PROCEDURE IF EXISTS `delete_agg_aggregation`$$

CREATE PROCEDURE `delete_agg_aggregation`(aggid INT)
BEGIN
DECLARE EXIT HANDLER FOR NOT FOUND ROLLBACK;
DECLARE EXIT HANDLER FOR SQLEXCEPTION ROLLBACK;
DECLARE EXIT HANDLER FOR SQLWARNING ROLLBACK; 
SET max_sp_recursion_depth=255;
START TRANSACTION;
  UPDATE agg_aggregation SET agg_deleted = 1 WHERE agg_id = aggid;
COMMIT;
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `delete_doc_documents`$$

CREATE PROCEDURE `delete_doc_documents`(docid INT)
BEGIN
DECLARE EXIT HANDLER FOR NOT FOUND ROLLBACK;
DECLARE EXIT HANDLER FOR SQLEXCEPTION ROLLBACK;
DECLARE EXIT HANDLER FOR SQLWARNING ROLLBACK;
SET max_sp_recursion_depth=255;
START TRANSACTION;
  UPDATE doc_documents SET doc_deleted = 1 WHERE doc_id = docid;
COMMIT;
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `delete_mde_moduledefinitions`$$

CREATE PROCEDURE `delete_mde_moduledefinitions`(mdeid INT)
BEGIN
DECLARE EXIT HANDLER FOR NOT FOUND ROLLBACK;
DECLARE EXIT HANDLER FOR SQLEXCEPTION ROLLBACK;
DECLARE EXIT HANDLER FOR SQLWARNING ROLLBACK;
SET max_sp_recursion_depth=255;
START TRANSACTION;
  UPDATE mde_moduledefinitions SET mde_deleted = 1 WHERE mde_id = mdeid;
  CALL sort_mde_modeldefinitions();
COMMIT;
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `delete_mdi_modelitems`$$

CREATE PROCEDURE `delete_mdi_modelitems`(mdiid INT)
BEGIN
DECLARE EXIT HANDLER FOR NOT FOUND ROLLBACK;
DECLARE EXIT HANDLER FOR SQLEXCEPTION ROLLBACK;
DECLARE EXIT HANDLER FOR SQLWARNING ROLLBACK;
SET max_sp_recursion_depth=255;
START TRANSACTION;
  UPDATE mdi_modelitems SET mdi_deleted = 1 WHERE mdi_id = mdiid;
COMMIT;
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `delete_mdl_model`$$

CREATE PROCEDURE `delete_mdl_model`(mdlid INT)
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i INT;
   DECLARE cur CURSOR FOR SELECT mdi_id FROM mdi_modelitems WHERE mdl_id = mdlid;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
   DECLARE EXIT HANDLER FOR SQLEXCEPTION ROLLBACK;
   DECLARE EXIT HANDLER FOR SQLWARNING ROLLBACK;
   SET max_sp_recursion_depth=255;
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
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `delete_mod_modules`$$

CREATE PROCEDURE `delete_mod_modules`(modid INT)
BEGIN
DECLARE EXIT HANDLER FOR NOT FOUND ROLLBACK;
DECLARE EXIT HANDLER FOR SQLEXCEPTION ROLLBACK;
DECLARE EXIT HANDLER FOR SQLWARNING ROLLBACK;
SET max_sp_recursion_depth=255;
START TRANSACTION;
  UPDATE amr_authorizedmodulesroles SET amr_deleted = 1 WHERE mod_id = modid;
  UPDATE agg_aggregation SET agg_deleted = 1 WHERE mod_id = modid;
  UPDATE set_settings SET set_deleted = 1 WHERE mod_id = modid;
  UPDATE mod_modules SET mod_deleted = 1 WHERE mod_id = modid;
  CALL sort_set_settings();
  CALL sort_mod_modules();
COMMIT;
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `delete_mod_modules_by_pagid_recursive`$$

CREATE PROCEDURE `delete_mod_modules_by_pagid_recursive`(pagid INT)
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i INT;
   DECLARE cur CURSOR FOR SELECT mod_id FROM mod_modules WHERE pag_id = pagid;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
   DECLARE EXIT HANDLER FOR SQLEXCEPTION ROLLBACK;
   DECLARE EXIT HANDLER FOR SQLWARNING ROLLBACK;
   SET max_sp_recursion_depth=255;
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
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `delete_mod_modules_recursive`$$

CREATE PROCEDURE `delete_mod_modules_recursive`(modid INT)
BEGIN
DECLARE EXIT HANDLER FOR NOT FOUND ROLLBACK;
DECLARE EXIT HANDLER FOR SQLEXCEPTION ROLLBACK;
DECLARE EXIT HANDLER FOR SQLWARNING ROLLBACK;
SET max_sp_recursion_depth=255;
START TRANSACTION;
  CALL delete_obd_objectdata_by_modid_recursive(modid);
  UPDATE amr_authorizedmodulesroles SET amr_deleted = 1 WHERE mod_id = modid;
  UPDATE agg_aggregation SET agg_deleted = 1 WHERE mod_id = modid;
  UPDATE set_settings SET set_deleted = 1 WHERE mod_id = modid;
  UPDATE mod_modules SET mod_deleted = 1 WHERE mod_id = modid;
  CALL sort_set_settings();
  CALL sort_mod_modules();
COMMIT;
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `delete_obd_objectdata`$$

CREATE PROCEDURE `delete_obd_objectdata`(obdid INT)
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i INT;
   DECLARE cur CURSOR FOR SELECT obd_id FROM obd_objectdata WHERE obd_parentid = obdid;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
   DECLARE EXIT HANDLER FOR SQLEXCEPTION ROLLBACK;
   DECLARE EXIT HANDLER FOR SQLWARNING ROLLBACK;
   SET max_sp_recursion_depth=255;
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
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `delete_obd_objectdata_by_modid_recursive`$$

CREATE PROCEDURE `delete_obd_objectdata_by_modid_recursive`(modid INT)
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i INT;
   DECLARE cur CURSOR FOR SELECT obd_id FROM obd_objectdata WHERE mod_id = modid;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
   DECLARE EXIT HANDLER FOR SQLEXCEPTION ROLLBACK;
   DECLARE EXIT HANDLER FOR SQLWARNING ROLLBACK;
   SET max_sp_recursion_depth=255;
   OPEN cur;
   read_loop: LOOP
    FETCH cur INTO i;
    IF done THEN
      LEAVE read_loop;
    END IF;
    CALL delete_obd_objectdata(i);
   END LOOP;
  CLOSE cur;
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `delete_pag_pages`$$

CREATE PROCEDURE `delete_pag_pages`(pagid INT)
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i INT;
   DECLARE cur CURSOR FOR SELECT pag_id FROM pag_pages WHERE pag_parentid = pagid;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
   DECLARE EXIT HANDLER FOR SQLEXCEPTION ROLLBACK;
   DECLARE EXIT HANDLER FOR SQLWARNING ROLLBACK;
   SET max_sp_recursion_depth=255;
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
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `delete_pag_pages_recursive`$$

CREATE PROCEDURE `delete_pag_pages_recursive`(pagid INT)
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i INT;
   DECLARE cur CURSOR FOR SELECT pag_id FROM pag_pages WHERE pag_parentid = pagid;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
   DECLARE EXIT HANDLER FOR SQLEXCEPTION ROLLBACK;
   DECLARE EXIT HANDLER FOR SQLWARNING ROLLBACK;
   SET max_sp_recursion_depth=255;
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
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `delete_rol_roles`$$

CREATE PROCEDURE `delete_rol_roles`(rolid INT)
BEGIN
DECLARE EXIT HANDLER FOR NOT FOUND ROLLBACK;
DECLARE EXIT HANDLER FOR SQLEXCEPTION ROLLBACK;
DECLARE EXIT HANDLER FOR SQLWARNING ROLLBACK;
SET max_sp_recursion_depth=255;
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
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `delete_set_settings`$$

CREATE PROCEDURE `delete_set_settings`(setid INT)
BEGIN
DECLARE EXIT HANDLER FOR NOT FOUND ROLLBACK;
DECLARE EXIT HANDLER FOR SQLEXCEPTION ROLLBACK;
DECLARE EXIT HANDLER FOR SQLWARNING ROLLBACK;
SET max_sp_recursion_depth=255;
START TRANSACTION;
  UPDATE set_settings SET set_deleted = 1 WHERE set_id = setid;
  CALL sort_set_settings();
COMMIT;
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `delete_sit_sites`$$

CREATE PROCEDURE `delete_sit_sites`(sitid INT)
BEGIN
DECLARE EXIT HANDLER FOR NOT FOUND ROLLBACK;
DECLARE EXIT HANDLER FOR SQLEXCEPTION ROLLBACK;
DECLARE EXIT HANDLER FOR SQLWARNING ROLLBACK;
SET max_sp_recursion_depth=255;
START TRANSACTION;
  UPDATE asr_authorizedsitesroles SET asr_deleted = 1 WHERE sit_id = sitid;
  UPDATE set_settings SET set_deleted = 1 WHERE sit_id = sitid;
  UPDATE sit_sites SET sit_deleted = 1 WHERE sit_id = sitid;
  CALL sort_sit_sites();
COMMIT;
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `delete_sta_status`$$

CREATE PROCEDURE `delete_sta_status`(staid INT)
BEGIN
DECLARE EXIT HANDLER FOR NOT FOUND ROLLBACK;
DECLARE EXIT HANDLER FOR SQLEXCEPTION ROLLBACK;
DECLARE EXIT HANDLER FOR SQLWARNING ROLLBACK;
SET max_sp_recursion_depth=255;
START TRANSACTION;
  UPDATE sta_status SET sta_deleted = 1 WHERE sta_id = staid;
  CALL sort_sta_status();
COMMIT;
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `delete_tas_tasks`$$

CREATE PROCEDURE `delete_tas_tasks`(tasid INT)
BEGIN
DECLARE EXIT HANDLER FOR NOT FOUND ROLLBACK;
DECLARE EXIT HANDLER FOR SQLEXCEPTION ROLLBACK;
DECLARE EXIT HANDLER FOR SQLWARNING ROLLBACK;
SET max_sp_recursion_depth=255;
START TRANSACTION;
  UPDATE tas_tasks SET tas_deleted = 1 WHERE tas_id = tasid;
COMMIT;
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `delete_usr_users`$$

CREATE PROCEDURE `delete_usr_users`(usrid INT)
BEGIN
DECLARE EXIT HANDLER FOR NOT FOUND ROLLBACK;
DECLARE EXIT HANDLER FOR SQLEXCEPTION ROLLBACK;
DECLARE EXIT HANDLER FOR SQLWARNING ROLLBACK;
SET max_sp_recursion_depth=255;
START TRANSACTION;
  UPDATE uro_usersroles SET uro_deleted = 1 WHERE usr_id = usrid;
  UPDATE usr_users SET usr_deleted = 1 WHERE usr_id = usrid;
  CALL sort_usr_users();
COMMIT;
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `delete_ust_usertypes`$$

CREATE PROCEDURE `delete_ust_usertypes`(ustid INT)
BEGIN
DECLARE EXIT HANDLER FOR NOT FOUND ROLLBACK;
DECLARE EXIT HANDLER FOR SQLEXCEPTION ROLLBACK;
DECLARE EXIT HANDLER FOR SQLWARNING ROLLBACK;
SET max_sp_recursion_depth=255;
START TRANSACTION;
  UPDATE ust_usertypes SET ust_deleted = 1 WHERE ust_id = ustid;
  CALL sort_ust_usertypes();
COMMIT;
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `sort_mde_modeldefinitions`$$

CREATE PROCEDURE `sort_mde_modeldefinitions`()
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i,o INT;
   DECLARE cur CURSOR FOR SELECT mde_id FROM mde_moduledefinitions WHERE mde_deleted = 0 ORDER BY sit_id, sta_id, lng_id, mde_parentid, mde_order;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
   SET max_sp_recursion_depth=255;
   SET o = 1;
   OPEN cur;
   read_loop: LOOP
    FETCH cur INTO i;
    IF done THEN
      LEAVE read_loop;
    END IF;
    UPDATE mde_moduledefinitions SET mde_order = o WHERE mde_id = i;
    SET o = o + 2;
   END LOOP;
  CLOSE cur;
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `sort_mdi_modelitems`$$

CREATE PROCEDURE `sort_mdi_modelitems`()
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i,o INT;
   DECLARE cur CURSOR FOR SELECT mdi_id FROM mdi_modelitems WHERE mdi_deleted = 0 ORDER BY sit_id, mdl_id, sta_id, lng_id, mdi_parentid, mdi_order;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
   SET max_sp_recursion_depth=255;
   SET o = 1;
   OPEN cur;
   read_loop: LOOP
    FETCH cur INTO i;
    IF done THEN
      LEAVE read_loop;
    END IF;
    UPDATE mdi_modelitems SET mdi_order = o WHERE mdi_id = i;
    SET o = o + 2;
   END LOOP;
  CLOSE cur;
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `sort_mod_modules`$$

CREATE PROCEDURE `sort_mod_modules`()
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i,o INT;
   DECLARE cur CURSOR FOR SELECT mod_id FROM mod_modules WHERE mod_deleted = 0 ORDER BY sit_id, pag_id, sta_id, lng_id, mod_parentid, mod_revision, mod_contentpane, mod_order;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
   SET max_sp_recursion_depth=255;
   SET o = 1;
   OPEN cur;
   read_loop: LOOP
    FETCH cur INTO i;
    IF done THEN
      LEAVE read_loop;
    END IF;
    UPDATE mod_modules SET mod_order = o WHERE mod_id = i;
    SET o = o + 2;
   END LOOP;
  CLOSE cur;
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `sort_obd_objectdata`$$

CREATE PROCEDURE `sort_obd_objectdata`()
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i,o INT;
   DECLARE cur CURSOR FOR SELECT obd_id FROM obd_objectdata WHERE obd_deleted = 0 ORDER BY sit_id, pag_id, mod_id, sta_id, lng_id, obd_parentid, obd_order;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
   SET max_sp_recursion_depth=255;
   SET o = 1;
   OPEN cur;
   read_loop: LOOP
    FETCH cur INTO i;
    IF done THEN
      LEAVE read_loop;
    END IF;
    UPDATE obd_objectdata SET obd_order = o WHERE obd_id = i;
    SET o = o + 2;
   END LOOP;
  CLOSE cur;
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `sort_pag_pages`$$

CREATE PROCEDURE `sort_pag_pages`()
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i,o INT;
   DECLARE cur CURSOR FOR SELECT pag_id FROM pag_pages WHERE pag_deleted = 0 ORDER BY sit_id, sta_id, lng_id, pag_parentid, pag_order;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
   SET max_sp_recursion_depth=255;
   SET o = 1;
   OPEN cur;
   read_loop: LOOP
    FETCH cur INTO i;
    IF done THEN
      LEAVE read_loop;
    END IF;
    UPDATE pag_pages SET pag_order = o WHERE pag_id = i;
    SET o = o + 2;
   END LOOP;
  CLOSE cur;
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `sort_rol_roles`$$

CREATE PROCEDURE `sort_rol_roles`()
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i,o INT;
   DECLARE cur CURSOR FOR SELECT rol_id FROM rol_roles WHERE rol_deleted = 0 ORDER BY sta_id, lng_id, rol_parentid, rol_order;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
   SET max_sp_recursion_depth=255;
   SET o = 1;
   OPEN cur;
   read_loop: LOOP
    FETCH cur INTO i;
    IF done THEN
      LEAVE read_loop;
    END IF;
    UPDATE rol_roles SET rol_order = o WHERE rol_id = i;
    SET o = o + 2;
   END LOOP;
  CLOSE cur;
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `sort_set_settings`$$

CREATE PROCEDURE `sort_set_settings`()
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i,o INT;
   DECLARE cur CURSOR FOR SELECT set_id FROM set_settings WHERE set_deleted = 0 ORDER BY sta_id, lng_id, set_parentid, set_order;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
   SET max_sp_recursion_depth=255;
   SET o = 1;
   OPEN cur;
   read_loop: LOOP
    FETCH cur INTO i;
    IF done THEN
      LEAVE read_loop;
    END IF;
    UPDATE set_settings SET set_order = o WHERE set_id = i;
    SET o = o + 2;
   END LOOP;
  CLOSE cur;
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `sort_sit_sites`$$

CREATE PROCEDURE `sort_sit_sites`()
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i,o INT;
   DECLARE cur CURSOR FOR SELECT sit_id FROM sit_sites WHERE sit_deleted = 0 ORDER BY sta_id, lng_id, sit_parentid, sit_order;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
   SET max_sp_recursion_depth=255;
   SET o = 1;
   OPEN cur;
   read_loop: LOOP
    FETCH cur INTO i;
    IF done THEN
      LEAVE read_loop;
    END IF;
    UPDATE sit_sites SET sit_order = o WHERE sit_id = i;
    SET o = o + 2;
   END LOOP;
  CLOSE cur;
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `sort_sta_status`$$

CREATE PROCEDURE `sort_sta_status`()
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i,o INT;
   DECLARE cur CURSOR FOR SELECT sta_id FROM sta_status WHERE sta_deleted = 0 ORDER BY lng_id, sta_parentid, sta_order;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
   SET max_sp_recursion_depth=255;
   SET o = 1;
   OPEN cur;
   read_loop: LOOP
    FETCH cur INTO i;
    IF done THEN
      LEAVE read_loop;
    END IF;
    UPDATE sta_status SET sta_order = o WHERE sta_id = i;
    SET o = o + 2;
   END LOOP;
  CLOSE cur;
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `sort_usr_users`$$

CREATE PROCEDURE `sort_usr_users`()
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i,o INT;
   DECLARE cur CURSOR FOR SELECT usr_id FROM usr_users WHERE usr_deleted = 0 ORDER BY sta_id, lng_id, usr_parentid, usr_order;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
   SET max_sp_recursion_depth=255;
   SET o = 1;
   OPEN cur;
   read_loop: LOOP
    FETCH cur INTO i;
    IF done THEN
      LEAVE read_loop;
    END IF;
    UPDATE usr_users SET usr_order = o WHERE usr_id = i;
    SET o = o + 2;
   END LOOP;
  CLOSE cur;
END$$

DELIMITER ;

DELIMITER $$

DROP PROCEDURE IF EXISTS `sort_ust_usertypes`$$

CREATE PROCEDURE `sort_ust_usertypes`()
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i,o INT;
   DECLARE cur CURSOR FOR SELECT ust_id FROM ust_usertypes WHERE ust_deleted = 0 ORDER BY sta_id, lng_id, ust_parentid, ust_order;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
   SET max_sp_recursion_depth=255;
   SET o = 1;
   OPEN cur;
   read_loop: LOOP
    FETCH cur INTO i;
    IF done THEN
      LEAVE read_loop;
    END IF;
    UPDATE ust_usertypes SET ust_order = o WHERE ust_id = i;
    SET o = o + 2;
   END LOOP;
  CLOSE cur;
END$$

DELIMITER ;

