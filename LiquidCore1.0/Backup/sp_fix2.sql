DELIMITER $$

CREATE PROCEDURE `delete_agg_aggregation`(xid INT)
BEGIN
START TRANSACTION;
  UPDATE agg_aggregation SET agg_deleted = 1 WHERE agg_id = xid;
COMMIT;
END $$
DELIMITER ;

DELIMITER $$

CREATE PROCEDURE `delete_doc_documents`(xid INT)
BEGIN
START TRANSACTION;
  UPDATE doc_documents SET doc_deleted = 1 WHERE doc_id = xid;
COMMIT;
END $$
DELIMITER ;

DELIMITER $$

CREATE PROCEDURE `delete_mdi_modelitems`(xid INT)
BEGIN
START TRANSACTION;
  UPDATE mdi_modelitems SET mdi_deleted = 1 WHERE mdi_id = xid;
COMMIT;
END $$
DELIMITER ;

DELIMITER $$

CREATE PROCEDURE `delete_mdl_model`(xid INT)
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i INT;
   DECLARE cur CURSOR FOR SELECT mdi_id FROM mdi_modelitems WHERE mdl_id = xid;
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
  UPDATE mdl_model SET mdl_deleted = 1 WHERE mdl_id = xid;
COMMIT;
END $$
DELIMITER ;

DELIMITER $$

CREATE PROCEDURE `delete_mod_modules`(xid INT)
BEGIN
START TRANSACTION;
  UPDATE amr_authorizedmodulesroles SET amr_deleted = 1 WHERE mod_id = xid;
  UPDATE agg_aggregation SET agg_deleted = 1 WHERE mod_id = xid;
  UPDATE set_settings SET set_deleted = 1 WHERE mod_id = xid;
  UPDATE mod_modules SET mod_deleted = 1 WHERE mod_id = xid;
COMMIT;
END $$
DELIMITER ;

DELIMITER $$

CREATE PROCEDURE `delete_obd_objectdata`(xid INT)
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i INT;
   DECLARE cur CURSOR FOR SELECT obd_id FROM obd_objectdata WHERE obd_parentid = xid;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
   OPEN cur;
   read_loop: LOOP
    FETCH cur INTO i;
    IF done THEN
      LEAVE read_loop;
    END IF;
    CALL delete_obd_objectdata(i);
    START TRANSACTION;
      UPDATE set_settings SET set_deleted = 1 WHERE obd_id = i;
      UPDATE obd_objectdata SET obd_deleted = 1 WHERE obd_id = i;
    COMMIT;
   END LOOP;
  CLOSE cur;
  START TRANSACTION;
    UPDATE set_settings SET set_deleted = 1 WHERE obd_id = xid;
    UPDATE obd_objectdata SET obd_deleted = 1 WHERE obd_id = xid;
  COMMIT;
END $$
DELIMITER ;

DELIMITER $$

CREATE PROCEDURE `delete_pag_pages`(xid INT)
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i INT;
   DECLARE cur CURSOR FOR SELECT pag_id FROM pag_pages WHERE pag_parentid = xid;
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
    UPDATE apr_authorizedpagesroles SET apr_deleted = 1 WHERE pag_id = xid;
    UPDATE set_settings SET set_deleted = 1 WHERE pag_id = xid;
    UPDATE pag_pages SET pag_deleted = 1 WHERE pag_id = xid;
  COMMIT;
END $$
DELIMITER ;

DELIMITER $$

CREATE PROCEDURE `delete_rol_roles`(xid INT)
BEGIN
START TRANSACTION;
  UPDATE asr_authorizedsitesroles SET asr_deleted = 1 WHERE rol_id = xid;
  UPDATE adr_authorizeddocumentsroles SET adr_deleted = 1 WHERE rol_id = xid;
  UPDATE amr_authorizedmodulesroles SET amr_deleted = 1 WHERE rol_id = xid;
  UPDATE apr_authorizedpagesroles SET apr_deleted = 1 WHERE rol_id = xid;
  UPDATE atr_authorizedtasksroles SET atr_deleted = 1 WHERE rol_id = xid;
  UPDATE uro_usersroles SET uro_deleted = 1 WHERE rol_id = xid;
  UPDATE rol_roles SET rol_deleted = 1 WHERE rol_id = xid;
COMMIT;
END $$
DELIMITER ;

DELIMITER $$

CREATE PROCEDURE `delete_set_settings`(xid INT)
BEGIN
START TRANSACTION;
  UPDATE set_settings SET set_deleted = 1 WHERE set_id = xid;
COMMIT;
$$
DELIMITER ;

DELIMITER $$

CREATE PROCEDURE `delete_sit_sites`(xid INT)
BEGIN
START TRANSACTION;
  UPDATE asr_authorizedsitesroles SET asr_deleted = 1 WHERE sit_id = xid;
  UPDATE set_settings SET set_deleted = 1 WHERE sit_id = xid;
  UPDATE sit_sites SET sit_deleted = 1 WHERE sit_id = xid;
COMMIT;
END $$
DELIMITER ;

DELIMITER $$

CREATE PROCEDURE `delete_sta_status`(xid INT)
BEGIN
START TRANSACTION;
  UPDATE sta_status SET sta_deleted = 1 WHERE sta_id = xid;
COMMIT;
END $$
DELIMITER ;

DELIMITER $$

CREATE PROCEDURE `delete_tas_tasks`(xid INT)
BEGIN
START TRANSACTION;
  UPDATE tas_tasks SET tas_deleted = 1 WHERE tas_id = xid;
COMMIT;
END $$
DELIMITER ;

DELIMITER $$

CREATE PROCEDURE `delete_usr_users`(xid INT)
BEGIN
START TRANSACTION;
  UPDATE uro_usersroles SET uro_deleted = 1 WHERE usr_id = xid;
  UPDATE usr_users SET usr_deleted = 1 WHERE usr_id = xid;
COMMIT;
END $$
DELIMITER ;

DELIMITER $$

CREATE PROCEDURE `delete_ust_usertypes`(xid INT)
BEGIN
START TRANSACTION;
  UPDATE ust_usertypes SET ust_deleted = 1 WHERE ust_id = xid;
COMMIT;
END $$
DELIMITER ;
