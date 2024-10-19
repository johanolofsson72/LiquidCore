

DELIMITER $$

DROP PROCEDURE IF EXISTS `sort_mde_modeldefinitions`$$

CREATE PROCEDURE `sort_mde_modeldefinitions`()
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i,o INT;
   DECLARE cur CURSOR FOR SELECT mde_id FROM mde_moduledefinitions WHERE mde_deleted = 0 ORDER BY sit_id, sta_id, lng_id, mde_parentid, mde_order;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
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

DROP PROCEDURE IF EXISTS `sort_mdi_modelitems`$$

CREATE PROCEDURE `sort_mdi_modelitems`()
BEGIN
   DECLARE done INT DEFAULT 0;
   DECLARE i,o INT;
   DECLARE cur CURSOR FOR SELECT mdi_id FROM mdi_modelitems WHERE mdi_deleted = 0 ORDER BY sit_id, mdl_id, sta_id, lng_id, mdi_parentid, mdi_order;
   DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1; 
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