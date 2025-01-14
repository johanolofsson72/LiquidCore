IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_asr_authorizedsitesroles_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[asr_authorizedsitesroles]'))
ALTER TABLE [dbo].[asr_authorizedsitesroles] DROP CONSTRAINT [FK_asr_authorizedsitesroles_lng_language]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_asr_authorizedsitesroles_rol_roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[asr_authorizedsitesroles]'))
ALTER TABLE [dbo].[asr_authorizedsitesroles] DROP CONSTRAINT [FK_asr_authorizedsitesroles_rol_roles]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_asr_authorizedsitesroles_sit_sites]') AND parent_object_id = OBJECT_ID(N'[dbo].[asr_authorizedsitesroles]'))
ALTER TABLE [dbo].[asr_authorizedsitesroles] DROP CONSTRAINT [FK_asr_authorizedsitesroles_sit_sites]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_asr_authorizedsitesroles_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[asr_authorizedsitesroles]'))
ALTER TABLE [dbo].[asr_authorizedsitesroles] DROP CONSTRAINT [FK_asr_authorizedsitesroles_sta_status]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_apr_authorizedpagesroles_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[apr_authorizedpagesroles]'))
ALTER TABLE [dbo].[apr_authorizedpagesroles] DROP CONSTRAINT [FK_apr_authorizedpagesroles_lng_language]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_apr_authorizedpagesroles_pag_pages]') AND parent_object_id = OBJECT_ID(N'[dbo].[apr_authorizedpagesroles]'))
ALTER TABLE [dbo].[apr_authorizedpagesroles] DROP CONSTRAINT [FK_apr_authorizedpagesroles_pag_pages]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_apr_authorizedpagesroles_rol_roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[apr_authorizedpagesroles]'))
ALTER TABLE [dbo].[apr_authorizedpagesroles] DROP CONSTRAINT [FK_apr_authorizedpagesroles_rol_roles]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_apr_authorizedpagesroles_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[apr_authorizedpagesroles]'))
ALTER TABLE [dbo].[apr_authorizedpagesroles] DROP CONSTRAINT [FK_apr_authorizedpagesroles_sta_status]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_adr_authorizeddocumentsroles_doc_documents]') AND parent_object_id = OBJECT_ID(N'[dbo].[adr_authorizeddocumentsroles]'))
ALTER TABLE [dbo].[adr_authorizeddocumentsroles] DROP CONSTRAINT [FK_adr_authorizeddocumentsroles_doc_documents]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_adr_authorizeddocumentsroles_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[adr_authorizeddocumentsroles]'))
ALTER TABLE [dbo].[adr_authorizeddocumentsroles] DROP CONSTRAINT [FK_adr_authorizeddocumentsroles_lng_language]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_adr_authorizeddocumentsroles_rol_roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[adr_authorizeddocumentsroles]'))
ALTER TABLE [dbo].[adr_authorizeddocumentsroles] DROP CONSTRAINT [FK_adr_authorizeddocumentsroles_rol_roles]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_adr_authorizeddocumentsroles_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[adr_authorizeddocumentsroles]'))
ALTER TABLE [dbo].[adr_authorizeddocumentsroles] DROP CONSTRAINT [FK_adr_authorizeddocumentsroles_sta_status]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_amr_authorizedmodulesroles_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[amr_authorizedmodulesroles]'))
ALTER TABLE [dbo].[amr_authorizedmodulesroles] DROP CONSTRAINT [FK_amr_authorizedmodulesroles_lng_language]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_amr_authorizedmodulesroles_mod_modules]') AND parent_object_id = OBJECT_ID(N'[dbo].[amr_authorizedmodulesroles]'))
ALTER TABLE [dbo].[amr_authorizedmodulesroles] DROP CONSTRAINT [FK_amr_authorizedmodulesroles_mod_modules]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_amr_authorizedmodulesroles_rol_roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[amr_authorizedmodulesroles]'))
ALTER TABLE [dbo].[amr_authorizedmodulesroles] DROP CONSTRAINT [FK_amr_authorizedmodulesroles_rol_roles]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_amr_authorizedmodulesroles_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[amr_authorizedmodulesroles]'))
ALTER TABLE [dbo].[amr_authorizedmodulesroles] DROP CONSTRAINT [FK_amr_authorizedmodulesroles_sta_status]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_set_settings_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[set_settings]'))
ALTER TABLE [dbo].[set_settings] DROP CONSTRAINT [FK_set_settings_lng_language]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_set_settings_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[set_settings]'))
ALTER TABLE [dbo].[set_settings] DROP CONSTRAINT [FK_set_settings_sta_status]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_uro_usersroles_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[uro_usersroles]'))
ALTER TABLE [dbo].[uro_usersroles] DROP CONSTRAINT [FK_uro_usersroles_lng_language]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_uro_usersroles_rol_roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[uro_usersroles]'))
ALTER TABLE [dbo].[uro_usersroles] DROP CONSTRAINT [FK_uro_usersroles_rol_roles]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_uro_usersroles_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[uro_usersroles]'))
ALTER TABLE [dbo].[uro_usersroles] DROP CONSTRAINT [FK_uro_usersroles_sta_status]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_uro_usersroles_usr_users]') AND parent_object_id = OBJECT_ID(N'[dbo].[uro_usersroles]'))
ALTER TABLE [dbo].[uro_usersroles] DROP CONSTRAINT [FK_uro_usersroles_usr_users]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_obd_objectdata_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[obd_objectdata]'))
ALTER TABLE [dbo].[obd_objectdata] DROP CONSTRAINT [FK_obd_objectdata_lng_language]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_obd_objectdata_mod_modules]') AND parent_object_id = OBJECT_ID(N'[dbo].[obd_objectdata]'))
ALTER TABLE [dbo].[obd_objectdata] DROP CONSTRAINT [FK_obd_objectdata_mod_modules]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_obd_objectdata_pag_pages]') AND parent_object_id = OBJECT_ID(N'[dbo].[obd_objectdata]'))
ALTER TABLE [dbo].[obd_objectdata] DROP CONSTRAINT [FK_obd_objectdata_pag_pages]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_obd_objectdata_sit_sites]') AND parent_object_id = OBJECT_ID(N'[dbo].[obd_objectdata]'))
ALTER TABLE [dbo].[obd_objectdata] DROP CONSTRAINT [FK_obd_objectdata_sit_sites]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_obd_objectdata_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[obd_objectdata]'))
ALTER TABLE [dbo].[obd_objectdata] DROP CONSTRAINT [FK_obd_objectdata_sta_status]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mdi_modelitems_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[mdi_modelitems]'))
ALTER TABLE [dbo].[mdi_modelitems] DROP CONSTRAINT [FK_mdi_modelitems_lng_language]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mdi_modelitems_mde_moduledefinitions]') AND parent_object_id = OBJECT_ID(N'[dbo].[mdi_modelitems]'))
ALTER TABLE [dbo].[mdi_modelitems] DROP CONSTRAINT [FK_mdi_modelitems_mde_moduledefinitions]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mdi_modelitems_mdl_model]') AND parent_object_id = OBJECT_ID(N'[dbo].[mdi_modelitems]'))
ALTER TABLE [dbo].[mdi_modelitems] DROP CONSTRAINT [FK_mdi_modelitems_mdl_model]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mdi_modelitems_sit_sites]') AND parent_object_id = OBJECT_ID(N'[dbo].[mdi_modelitems]'))
ALTER TABLE [dbo].[mdi_modelitems] DROP CONSTRAINT [FK_mdi_modelitems_sit_sites]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mdi_modelitems_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[mdi_modelitems]'))
ALTER TABLE [dbo].[mdi_modelitems] DROP CONSTRAINT [FK_mdi_modelitems_sta_status]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_pag_pages_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[pag_pages]'))
ALTER TABLE [dbo].[pag_pages] DROP CONSTRAINT [FK_pag_pages_lng_language]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_pag_pages_sit_sites]') AND parent_object_id = OBJECT_ID(N'[dbo].[pag_pages]'))
ALTER TABLE [dbo].[pag_pages] DROP CONSTRAINT [FK_pag_pages_sit_sites]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_pag_pages_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[pag_pages]'))
ALTER TABLE [dbo].[pag_pages] DROP CONSTRAINT [FK_pag_pages_sta_status]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mod_modules_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[mod_modules]'))
ALTER TABLE [dbo].[mod_modules] DROP CONSTRAINT [FK_mod_modules_lng_language]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mod_modules_mde_moduledefinitions]') AND parent_object_id = OBJECT_ID(N'[dbo].[mod_modules]'))
ALTER TABLE [dbo].[mod_modules] DROP CONSTRAINT [FK_mod_modules_mde_moduledefinitions]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mod_modules_pag_pages]') AND parent_object_id = OBJECT_ID(N'[dbo].[mod_modules]'))
ALTER TABLE [dbo].[mod_modules] DROP CONSTRAINT [FK_mod_modules_pag_pages]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mod_modules_sit_sites]') AND parent_object_id = OBJECT_ID(N'[dbo].[mod_modules]'))
ALTER TABLE [dbo].[mod_modules] DROP CONSTRAINT [FK_mod_modules_sit_sites]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mod_modules_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[mod_modules]'))
ALTER TABLE [dbo].[mod_modules] DROP CONSTRAINT [FK_mod_modules_sta_status]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_sit_sites_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[sit_sites]'))
ALTER TABLE [dbo].[sit_sites] DROP CONSTRAINT [FK_sit_sites_lng_language]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_sit_sites_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[sit_sites]'))
ALTER TABLE [dbo].[sit_sites] DROP CONSTRAINT [FK_sit_sites_sta_status]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_sta_status_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[sta_status]'))
ALTER TABLE [dbo].[sta_status] DROP CONSTRAINT [FK_sta_status_lng_language]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_rol_roles_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[rol_roles]'))
ALTER TABLE [dbo].[rol_roles] DROP CONSTRAINT [FK_rol_roles_lng_language]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_rol_roles_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[rol_roles]'))
ALTER TABLE [dbo].[rol_roles] DROP CONSTRAINT [FK_rol_roles_sta_status]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_doc_documents_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[doc_documents]'))
ALTER TABLE [dbo].[doc_documents] DROP CONSTRAINT [FK_doc_documents_lng_language]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_doc_documents_mod_modules]') AND parent_object_id = OBJECT_ID(N'[dbo].[doc_documents]'))
ALTER TABLE [dbo].[doc_documents] DROP CONSTRAINT [FK_doc_documents_mod_modules]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_doc_documents_pag_pages]') AND parent_object_id = OBJECT_ID(N'[dbo].[doc_documents]'))
ALTER TABLE [dbo].[doc_documents] DROP CONSTRAINT [FK_doc_documents_pag_pages]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_doc_documents_sit_sites]') AND parent_object_id = OBJECT_ID(N'[dbo].[doc_documents]'))
ALTER TABLE [dbo].[doc_documents] DROP CONSTRAINT [FK_doc_documents_sit_sites]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_doc_documents_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[doc_documents]'))
ALTER TABLE [dbo].[doc_documents] DROP CONSTRAINT [FK_doc_documents_sta_status]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mde_moduledefinitions_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[mde_moduledefinitions]'))
ALTER TABLE [dbo].[mde_moduledefinitions] DROP CONSTRAINT [FK_mde_moduledefinitions_lng_language]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mde_moduledefinitions_sit_sites]') AND parent_object_id = OBJECT_ID(N'[dbo].[mde_moduledefinitions]'))
ALTER TABLE [dbo].[mde_moduledefinitions] DROP CONSTRAINT [FK_mde_moduledefinitions_sit_sites]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mde_moduledefinitions_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[mde_moduledefinitions]'))
ALTER TABLE [dbo].[mde_moduledefinitions] DROP CONSTRAINT [FK_mde_moduledefinitions_sta_status]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mdl_model_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[mdl_model]'))
ALTER TABLE [dbo].[mdl_model] DROP CONSTRAINT [FK_mdl_model_lng_language]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mdl_model_sit_sites]') AND parent_object_id = OBJECT_ID(N'[dbo].[mdl_model]'))
ALTER TABLE [dbo].[mdl_model] DROP CONSTRAINT [FK_mdl_model_sit_sites]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mdl_model_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[mdl_model]'))
ALTER TABLE [dbo].[mdl_model] DROP CONSTRAINT [FK_mdl_model_sta_status]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ust_usertypes_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[ust_usertypes]'))
ALTER TABLE [dbo].[ust_usertypes] DROP CONSTRAINT [FK_ust_usertypes_lng_language]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ust_usertypes_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[ust_usertypes]'))
ALTER TABLE [dbo].[ust_usertypes] DROP CONSTRAINT [FK_ust_usertypes_sta_status]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_usr_users_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[usr_users]'))
ALTER TABLE [dbo].[usr_users] DROP CONSTRAINT [FK_usr_users_lng_language]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_usr_users_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[usr_users]'))
ALTER TABLE [dbo].[usr_users] DROP CONSTRAINT [FK_usr_users_sta_status]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_usr_users_ust_usertypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[usr_users]'))
ALTER TABLE [dbo].[usr_users] DROP CONSTRAINT [FK_usr_users_ust_usertypes]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[asr_authorizedsitesroles]') AND type in (N'U'))
DROP TABLE [dbo].[asr_authorizedsitesroles]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[apr_authorizedpagesroles]') AND type in (N'U'))
DROP TABLE [dbo].[apr_authorizedpagesroles]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[adr_authorizeddocumentsroles]') AND type in (N'U'))
DROP TABLE [dbo].[adr_authorizeddocumentsroles]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[amr_authorizedmodulesroles]') AND type in (N'U'))
DROP TABLE [dbo].[amr_authorizedmodulesroles]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[set_settings]') AND type in (N'U'))
DROP TABLE [dbo].[set_settings]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uro_usersroles]') AND type in (N'U'))
DROP TABLE [dbo].[uro_usersroles]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[obd_objectdata]') AND type in (N'U'))
DROP TABLE [dbo].[obd_objectdata]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mdi_modelitems]') AND type in (N'U'))
DROP TABLE [dbo].[mdi_modelitems]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pag_pages]') AND type in (N'U'))
DROP TABLE [dbo].[pag_pages]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lng_language]') AND type in (N'U'))
DROP TABLE [dbo].[lng_language]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mod_modules]') AND type in (N'U'))
DROP TABLE [dbo].[mod_modules]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sit_sites]') AND type in (N'U'))
DROP TABLE [dbo].[sit_sites]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sta_status]') AND type in (N'U'))
DROP TABLE [dbo].[sta_status]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rol_roles]') AND type in (N'U'))
DROP TABLE [dbo].[rol_roles]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[doc_documents]') AND type in (N'U'))
DROP TABLE [dbo].[doc_documents]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mde_moduledefinitions]') AND type in (N'U'))
DROP TABLE [dbo].[mde_moduledefinitions]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mdl_model]') AND type in (N'U'))
DROP TABLE [dbo].[mdl_model]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ust_usertypes]') AND type in (N'U'))
DROP TABLE [dbo].[ust_usertypes]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usr_users]') AND type in (N'U'))
DROP TABLE [dbo].[usr_users]

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lng_language]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[lng_language](
	[lng_id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[lng_parentid] [numeric](18, 0) NOT NULL,
	[lng_order] [numeric](18, 0) NOT NULL,
	[lng_title] [varchar](255) NOT NULL,
	[lng_alias] [varchar](255) NOT NULL,
	[lng_description] [text] NOT NULL,
	[lng_createddate] [datetime] NOT NULL,
	[lng_createdby] [varchar](255) NOT NULL,
	[lng_updateddate] [datetime] NOT NULL,
	[lng_updatedby] [varchar](255) NOT NULL,
	[lng_hidden] [int] NOT NULL,
	[lng_deleted] [int] NOT NULL,
	[lng_ts] [timestamp] NULL,
 CONSTRAINT [lng_language_PK] PRIMARY KEY NONCLUSTERED 
(
	[lng_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[adr_authorizeddocumentsroles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[adr_authorizeddocumentsroles](
	[adr_id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[sta_id] [numeric](18, 0) NOT NULL,
	[lng_id] [numeric](18, 0) NOT NULL,
	[doc_id] [numeric](18, 0) NOT NULL,
	[rol_id] [numeric](18, 0) NOT NULL,
	[adr_createddate] [datetime] NOT NULL,
	[adr_createdby] [varchar](255) NOT NULL,
	[adr_updateddate] [datetime] NOT NULL,
	[adr_updatedby] [varchar](255) NOT NULL,
	[adr_hidden] [int] NOT NULL,
	[adr_deleted] [int] NOT NULL,
	[adr_ts] [timestamp] NULL,
 CONSTRAINT [adr_authorizeddocumentsroles_PK] PRIMARY KEY NONCLUSTERED 
(
	[adr_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[apr_authorizedpagesroles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[apr_authorizedpagesroles](
	[apr_id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[sta_id] [numeric](18, 0) NOT NULL,
	[lng_id] [numeric](18, 0) NOT NULL,
	[pag_id] [numeric](18, 0) NOT NULL,
	[rol_id] [numeric](18, 0) NOT NULL,
	[apr_createddate] [datetime] NOT NULL,
	[apr_createdby] [varchar](255) NOT NULL,
	[apr_updateddate] [datetime] NOT NULL,
	[apr_updatedby] [varchar](255) NOT NULL,
	[apr_hidden] [int] NOT NULL,
	[apr_deleted] [int] NOT NULL,
	[apr_ts] [timestamp] NULL,
 CONSTRAINT [apr_authorizedpagesroles_PK] PRIMARY KEY NONCLUSTERED 
(
	[apr_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[doc_documents]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[doc_documents](
	[doc_id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[sta_id] [numeric](18, 0) NOT NULL,
	[lng_id] [numeric](18, 0) NOT NULL,
	[sit_id] [numeric](18, 0) NOT NULL,
	[pag_id] [numeric](18, 0) NOT NULL,
	[mod_id] [numeric](18, 0) NOT NULL,
	[doc_parentid] [numeric](18, 0) NOT NULL,
	[doc_order] [numeric](18, 0) NOT NULL,
	[doc_type] [numeric](18, 0) NOT NULL,
	[doc_title] [varchar](255) NOT NULL,
	[doc_alias] [varchar](255) NOT NULL,
	[doc_description] [text] NOT NULL,
	[doc_contenttype] [varchar](255) NOT NULL,
	[doc_contentsize] [int] NOT NULL,
	[doc_version] [int] NOT NULL,
	[doc_charset] [varchar](255) NOT NULL,
	[doc_extension] [varchar](10) NOT NULL,
	[doc_path] [varchar](255) NOT NULL,
	[doc_checkoutusrid] [int] NOT NULL,
	[doc_checkoutdate] [datetime] NOT NULL,
	[doc_checkoutexpiredate] [datetime] NOT NULL,
	[doc_lastvieweddate] [datetime] NOT NULL,
	[doc_lastviewedby] [varchar](255) NOT NULL,
	[doc_isdirty] [int] NOT NULL,
	[doc_isdelivered] [int] NOT NULL,
	[doc_issigned] [int] NOT NULL,
	[doc_iscertified] [int] NOT NULL,
	[doc_deliverdate] [datetime] NOT NULL,
	[doc_deliverby] [varchar](255) NOT NULL,
	[doc_deliverto] [varchar](255) NOT NULL,
	[doc_createddate] [datetime] NOT NULL,
	[doc_createdby] [varchar](255) NOT NULL,
	[doc_updateddate] [datetime] NOT NULL,
	[doc_updatedby] [varchar](255) NOT NULL,
	[doc_hidden] [int] NOT NULL,
	[doc_deleted] [int] NOT NULL,
	[doc_ts] [datetime] NULL,
 CONSTRAINT [doc_documents_PK] PRIMARY KEY NONCLUSTERED 
(
	[doc_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[asr_authorizedsitesroles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[asr_authorizedsitesroles](
	[asr_id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[sta_id] [numeric](18, 0) NOT NULL,
	[lng_id] [numeric](18, 0) NOT NULL,
	[sit_id] [numeric](18, 0) NOT NULL,
	[rol_id] [numeric](18, 0) NOT NULL,
	[asr_createddate] [datetime] NOT NULL,
	[asr_createdby] [varchar](255) NOT NULL,
	[asr_updateddate] [datetime] NOT NULL,
	[asr_updatedby] [varchar](255) NOT NULL,
	[asr_hidden] [int] NOT NULL,
	[asr_deleted] [int] NOT NULL,
	[asr_ts] [timestamp] NULL,
 CONSTRAINT [asr_authorizedsitesroles_PK] PRIMARY KEY NONCLUSTERED 
(
	[asr_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mod_modules]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mod_modules](
	[mod_id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[sta_id] [numeric](18, 0) NOT NULL CONSTRAINT [DF__mod_modul__sta_i__182C9B23]  DEFAULT ((0)),
	[sit_id] [numeric](18, 0) NOT NULL CONSTRAINT [DF__mod_modul__sit_i__1920BF5C]  DEFAULT ((0)),
	[pag_id] [numeric](18, 0) NOT NULL CONSTRAINT [DF__mod_modul__pag_i__1A14E395]  DEFAULT ((0)),
	[mde_id] [numeric](18, 0) NOT NULL CONSTRAINT [DF__mod_modul__mde_i__1B0907CE]  DEFAULT ((0)),
	[lng_id] [numeric](18, 0) NOT NULL CONSTRAINT [DF__mod_modul__lng_i__1BFD2C07]  DEFAULT ((0)),
	[mod_parentid] [numeric](18, 0) NOT NULL CONSTRAINT [DF__mod_modul__mod_p__1CF15040]  DEFAULT ((0)),
	[mod_order] [numeric](18, 0) NOT NULL CONSTRAINT [DF__mod_modul__mod_o__1DE57479]  DEFAULT ((0)),
	[mod_title] [varchar](255) NOT NULL CONSTRAINT [DF__mod_modul__mod_t__1ED998B2]  DEFAULT (''),
	[mod_alias] [varchar](255) NOT NULL CONSTRAINT [DF__mod_modul__mod_a__1FCDBCEB]  DEFAULT (''),
	[mod_description] [text] NOT NULL CONSTRAINT [DF__mod_modul__mod_d__20C1E124]  DEFAULT (''),
	[mod_createddate] [datetime] NOT NULL,
	[mod_createdby] [varchar](255) NOT NULL,
	[mod_updateddate] [datetime] NOT NULL,
	[mod_updatedby] [varchar](255) NOT NULL,
	[mod_hidden] [int] NOT NULL CONSTRAINT [DF__mod_modul__mod_h__2C3393D0]  DEFAULT ((0)),
	[mod_deleted] [int] NOT NULL CONSTRAINT [DF__mod_modul__mod_d__2D27B809]  DEFAULT ((0)),
	[mod_ts] [timestamp] NULL,
 CONSTRAINT [mod_modules_PK] PRIMARY KEY NONCLUSTERED 
(
	[mod_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[amr_authorizedmodulesroles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[amr_authorizedmodulesroles](
	[amr_id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[sta_id] [numeric](18, 0) NOT NULL,
	[lng_id] [numeric](18, 0) NOT NULL,
	[mod_id] [numeric](18, 0) NOT NULL,
	[rol_id] [numeric](18, 0) NOT NULL,
	[amr_createddate] [datetime] NOT NULL,
	[amr_createdby] [varchar](255) NOT NULL,
	[amr_updateddate] [datetime] NOT NULL,
	[amr_updatedby] [varchar](255) NOT NULL,
	[amr_hidden] [int] NOT NULL,
	[amr_deleted] [int] NOT NULL,
	[amr_ts] [timestamp] NULL,
 CONSTRAINT [amr_authorizedmodulesroles_PK] PRIMARY KEY NONCLUSTERED 
(
	[amr_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sit_sites]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[sit_sites](
	[sit_id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[sta_id] [numeric](18, 0) NOT NULL,
	[lng_id] [numeric](18, 0) NOT NULL,
	[sit_parentid] [numeric](18, 0) NOT NULL,
	[sit_order] [numeric](18, 0) NOT NULL,
	[sit_title] [varchar](255) NOT NULL,
	[sit_alias] [varchar](255) NOT NULL,
	[sit_description] [text] NOT NULL,
	[sit_createddate] [datetime] NOT NULL,
	[sit_createdby] [varchar](255) NOT NULL,
	[sit_updateddate] [datetime] NOT NULL,
	[sit_updatedby] [varchar](255) NOT NULL,
	[sit_hidden] [int] NOT NULL,
	[sit_deleted] [int] NOT NULL,
	[sit_ts] [timestamp] NULL,
 CONSTRAINT [sit_sites_PK] PRIMARY KEY NONCLUSTERED 
(
	[sit_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[set_settings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[set_settings](
	[set_id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[sta_id] [numeric](18, 0) NOT NULL,
	[lng_id] [numeric](18, 0) NOT NULL,
	[sit_id] [numeric](18, 0) NOT NULL,
	[pag_id] [numeric](18, 0) NOT NULL,
	[mod_id] [numeric](18, 0) NOT NULL,
	[set_parentid] [numeric](18, 0) NOT NULL,
	[set_order] [numeric](18, 0) NOT NULL,
	[set_title] [varchar](255) NOT NULL,
	[set_alias] [varchar](255) NOT NULL,
	[set_description] [text] NOT NULL,
	[set_value] [text] NOT NULL,
	[set_createddate] [datetime] NOT NULL,
	[set_createdby] [varchar](255) NOT NULL,
	[set_updateddate] [datetime] NOT NULL,
	[set_updatedby] [varchar](255) NOT NULL,
	[set_hidden] [int] NOT NULL,
	[set_deleted] [int] NOT NULL,
	[set_ts] [timestamp] NULL,
 CONSTRAINT [PK_set_settings] PRIMARY KEY CLUSTERED 
(
	[set_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[obd_objectdata]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[obd_objectdata](
	[obd_id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[sta_id] [numeric](18, 0) NOT NULL CONSTRAINT [DF__obd_objec__sta_i__300424B4]  DEFAULT ((0)),
	[lng_id] [numeric](18, 0) NOT NULL CONSTRAINT [DF__obd_objec__lng_i__30F848ED]  DEFAULT ((0)),
	[sit_id] [numeric](18, 0) NOT NULL CONSTRAINT [DF__obd_objec__sit_i__31EC6D26]  DEFAULT ((0)),
	[pag_id] [numeric](18, 0) NOT NULL CONSTRAINT [DF__obd_objec__pag_i__32E0915F]  DEFAULT ((0)),
	[mod_id] [numeric](18, 0) NOT NULL CONSTRAINT [DF__obd_objec__mod_i__33D4B598]  DEFAULT ((0)),
	[obd_parentid] [numeric](18, 0) NOT NULL CONSTRAINT [DF__obd_objec__obd_p__34C8D9D1]  DEFAULT ((0)),
	[obd_order] [numeric](18, 0) NOT NULL CONSTRAINT [DF__obd_objec__obd_o__35BCFE0A]  DEFAULT ((0)),
	[obd_type] [numeric](18, 0) NOT NULL CONSTRAINT [DF__obd_objec__obd_t__36B12243]  DEFAULT ((0)),
	[obd_title] [varchar](255) NOT NULL CONSTRAINT [DF__obd_objec__obd_t__37A5467C]  DEFAULT (''),
	[obd_alias] [varchar](255) NOT NULL CONSTRAINT [DF__obd_objec__obd_a__38996AB5]  DEFAULT (''),
	[obd_description] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_d__398D8EEE]  DEFAULT (''),
	[obd_varchar1] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__3A81B327]  DEFAULT (''),
	[obd_varchar2] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__3B75D760]  DEFAULT (''),
	[obd_varchar3] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__3C69FB99]  DEFAULT (''),
	[obd_varchar4] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__3D5E1FD2]  DEFAULT (''),
	[obd_varchar5] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__3E52440B]  DEFAULT (''),
	[obd_varchar6] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__3F466844]  DEFAULT (''),
	[obd_varchar7] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__403A8C7D]  DEFAULT (''),
	[obd_varchar8] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__412EB0B6]  DEFAULT (''),
	[obd_varchar9] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__4222D4EF]  DEFAULT (''),
	[obd_varchar10] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__4316F928]  DEFAULT (''),
	[obd_varchar11] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__440B1D61]  DEFAULT (''),
	[obd_varchar12] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__44FF419A]  DEFAULT (''),
	[obd_varchar13] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__45F365D3]  DEFAULT (''),
	[obd_varchar14] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__46E78A0C]  DEFAULT (''),
	[obd_varchar15] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__47DBAE45]  DEFAULT (''),
	[obd_varchar16] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__48CFD27E]  DEFAULT (''),
	[obd_varchar17] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__49C3F6B7]  DEFAULT (''),
	[obd_varchar18] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__4AB81AF0]  DEFAULT (''),
	[obd_varchar19] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__4BAC3F29]  DEFAULT (''),
	[obd_varchar20] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__4CA06362]  DEFAULT (''),
	[obd_varchar21] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__4D94879B]  DEFAULT (''),
	[obd_varchar22] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__4E88ABD4]  DEFAULT (''),
	[obd_varchar23] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__4F7CD00D]  DEFAULT (''),
	[obd_varchar24] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__5070F446]  DEFAULT (''),
	[obd_varchar25] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__5165187F]  DEFAULT (''),
	[obd_varchar26] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__52593CB8]  DEFAULT (''),
	[obd_varchar27] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__534D60F1]  DEFAULT (''),
	[obd_varchar28] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__5441852A]  DEFAULT (''),
	[obd_varchar29] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__5535A963]  DEFAULT (''),
	[obd_varchar30] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__5629CD9C]  DEFAULT (''),
	[obd_varchar31] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__571DF1D5]  DEFAULT (''),
	[obd_varchar32] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__5812160E]  DEFAULT (''),
	[obd_varchar33] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__59063A47]  DEFAULT (''),
	[obd_varchar34] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__59FA5E80]  DEFAULT (''),
	[obd_varchar35] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__5AEE82B9]  DEFAULT (''),
	[obd_varchar36] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__5BE2A6F2]  DEFAULT (''),
	[obd_varchar37] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__5CD6CB2B]  DEFAULT (''),
	[obd_varchar38] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__5DCAEF64]  DEFAULT (''),
	[obd_varchar39] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__5EBF139D]  DEFAULT (''),
	[obd_varchar40] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__5FB337D6]  DEFAULT (''),
	[obd_varchar41] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__60A75C0F]  DEFAULT (''),
	[obd_varchar42] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__619B8048]  DEFAULT (''),
	[obd_varchar43] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__628FA481]  DEFAULT (''),
	[obd_varchar44] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__6383C8BA]  DEFAULT (''),
	[obd_varchar45] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__6477ECF3]  DEFAULT (''),
	[obd_varchar46] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__656C112C]  DEFAULT (''),
	[obd_varchar47] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__66603565]  DEFAULT (''),
	[obd_varchar48] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__6754599E]  DEFAULT (''),
	[obd_varchar49] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__68487DD7]  DEFAULT (''),
	[obd_varchar50] [text] NOT NULL CONSTRAINT [DF__obd_objec__obd_v__693CA210]  DEFAULT (''),
	[obd_createddate] [datetime] NOT NULL,
	[obd_createdby] [varchar](255) NOT NULL,
	[obd_updateddate] [datetime] NOT NULL,
	[obd_updatedby] [varchar](255) NOT NULL,
	[obd_hidden] [int] NOT NULL CONSTRAINT [DF__obd_objec__obd_h__6A30C649]  DEFAULT ((0)),
	[obd_deleted] [int] NOT NULL CONSTRAINT [DF__obd_objec__obd_d__6B24EA82]  DEFAULT ((0)),
	[obd_ts] [timestamp] NULL,
 CONSTRAINT [obd_objectdata_PK] PRIMARY KEY NONCLUSTERED 
(
	[obd_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[uro_usersroles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[uro_usersroles](
	[uro_id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[sta_id] [numeric](18, 0) NOT NULL,
	[lng_id] [numeric](18, 0) NOT NULL,
	[usr_id] [numeric](18, 0) NOT NULL,
	[rol_id] [numeric](18, 0) NOT NULL,
	[uro_createddate] [datetime] NOT NULL,
	[uro_createdby] [varchar](255) NOT NULL,
	[uro_updateddate] [datetime] NOT NULL,
	[uro_updatedby] [varchar](255) NOT NULL,
	[uro_hidden] [int] NOT NULL,
	[uro_deleted] [int] NOT NULL,
	[uro_ts] [timestamp] NULL,
 CONSTRAINT [uro_usersroles_PK] PRIMARY KEY NONCLUSTERED 
(
	[uro_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mde_moduledefinitions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mde_moduledefinitions](
	[mde_id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[sta_id] [numeric](18, 0) NOT NULL,
	[sit_id] [numeric](18, 0) NOT NULL,
	[lng_id] [numeric](18, 0) NOT NULL,
	[mde_parentid] [numeric](18, 0) NOT NULL,
	[mde_order] [numeric](18, 0) NOT NULL,
	[mde_title] [varchar](255) NOT NULL,
	[mde_alias] [varchar](255) NOT NULL,
	[mde_description] [text] NOT NULL,
	[mde_src] [varchar](255) NOT NULL,
	[mde_createddate] [datetime] NOT NULL,
	[mde_createdby] [varchar](255) NOT NULL,
	[mde_updateddate] [datetime] NOT NULL,
	[mde_updatedby] [varchar](255) NOT NULL,
	[mde_hidden] [int] NOT NULL,
	[mde_deleted] [int] NOT NULL,
	[mde_ts] [timestamp] NULL,
 CONSTRAINT [mde_moduledefinitions_PK] PRIMARY KEY NONCLUSTERED 
(
	[mde_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pag_pages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[pag_pages](
	[pag_id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[sta_id] [numeric](18, 0) NOT NULL,
	[sit_id] [numeric](18, 0) NOT NULL,
	[lng_id] [numeric](18, 0) NOT NULL,
	[mdl_id] [numeric](18, 0) NOT NULL CONSTRAINT [DF_pag_pages_mdl_id]  DEFAULT ((0)),
	[pag_parentid] [numeric](18, 0) NOT NULL,
	[pag_order] [numeric](18, 0) NOT NULL,
	[pag_title] [varchar](255) NOT NULL,
	[pag_alias] [varchar](255) NOT NULL,
	[pag_description] [text] NOT NULL,
	[pag_createddate] [datetime] NOT NULL,
	[pag_createdby] [varchar](255) NOT NULL,
	[pag_updateddate] [datetime] NOT NULL,
	[pag_updatedby] [varchar](255) NOT NULL,
	[pag_hidden] [int] NOT NULL,
	[pag_deleted] [int] NOT NULL,
	[pag_ts] [timestamp] NULL,
 CONSTRAINT [pag_pages_PK] PRIMARY KEY NONCLUSTERED 
(
	[pag_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mdi_modelitems]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mdi_modelitems](
	[mdi_id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[sta_id] [numeric](18, 0) NOT NULL,
	[sit_id] [numeric](18, 0) NOT NULL,
	[lng_id] [numeric](18, 0) NOT NULL,
	[mdl_id] [numeric](18, 0) NOT NULL,
	[mde_id] [numeric](18, 0) NOT NULL,
	[mdi_parentid] [numeric](18, 0) NOT NULL,
	[mdi_order] [numeric](18, 0) NOT NULL,
	[mdi_contentpane] [varchar](50) NOT NULL,
	[mdi_createddate] [datetime] NOT NULL,
	[mdi_createdby] [varchar](255) NOT NULL,
	[mdi_updateddate] [datetime] NOT NULL,
	[mdi_updatedby] [varchar](255) NOT NULL,
	[mdi_hidden] [int] NOT NULL CONSTRAINT [DF_Table2_mdi_hidden]  DEFAULT ((0)),
	[mdi_deleted] [int] NOT NULL CONSTRAINT [DF_Table2_mdi_deleted]  DEFAULT ((0)),
	[mdi_ts] [timestamp] NULL,
 CONSTRAINT [PK_mdi_modelitems] PRIMARY KEY CLUSTERED 
(
	[mdi_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mdl_model]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[mdl_model](
	[mdl_id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[sta_id] [numeric](18, 0) NOT NULL,
	[sit_id] [numeric](18, 0) NOT NULL,
	[lng_id] [numeric](18, 0) NOT NULL,
	[mdl_parentid] [numeric](18, 0) NOT NULL,
	[mdl_order] [numeric](18, 0) NOT NULL,
	[mdl_title] [varchar](255) NOT NULL,
	[mdl_alias] [varchar](255) NOT NULL,
	[mdl_description] [text] NOT NULL,
	[mdl_createddate] [datetime] NOT NULL,
	[mdl_createdby] [varchar](255) NOT NULL,
	[mdl_updateddate] [datetime] NOT NULL,
	[mdl_updatedby] [varchar](255) NOT NULL,
	[mdl_hidden] [int] NOT NULL CONSTRAINT [DF_mdl_model_mdl_hidden]  DEFAULT ((0)),
	[mdl_deleted] [int] NOT NULL CONSTRAINT [DF_mdl_model_mdl_deleted]  DEFAULT ((0)),
	[mdl_ts] [timestamp] NULL,
 CONSTRAINT [PK_mdl_model] PRIMARY KEY CLUSTERED 
(
	[mdl_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ust_usertypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ust_usertypes](
	[ust_id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[sta_id] [numeric](18, 0) NOT NULL,
	[lng_id] [numeric](18, 0) NOT NULL,
	[ust_parentid] [numeric](18, 0) NOT NULL,
	[ust_order] [numeric](18, 0) NOT NULL,
	[ust_title] [varchar](255) NOT NULL,
	[ust_alias] [varchar](255) NOT NULL,
	[ust_description] [text] NOT NULL,
	[ust_createddate] [datetime] NOT NULL,
	[ust_createdby] [varchar](255) NOT NULL,
	[ust_updateddate] [datetime] NOT NULL,
	[ust_updatedby] [varchar](255) NOT NULL,
	[ust_hidden] [int] NOT NULL,
	[ust_deleted] [int] NOT NULL,
	[ust_ts] [timestamp] NULL,
 CONSTRAINT [ust_usertypes_PK] PRIMARY KEY NONCLUSTERED 
(
	[ust_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usr_users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[usr_users](
	[usr_id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[sta_id] [numeric](18, 0) NOT NULL,
	[lng_id] [numeric](18, 0) NOT NULL,
	[ust_id] [numeric](18, 0) NOT NULL,
	[usr_parentid] [numeric](18, 0) NOT NULL,
	[usr_order] [numeric](18, 0) NOT NULL,
	[usr_title] [varchar](255) NOT NULL,
	[usr_alias] [varchar](255) NOT NULL,
	[usr_description] [text] NOT NULL,
	[usr_loginname] [varchar](255) NOT NULL,
	[usr_password] [varchar](255) NOT NULL,
	[usr_createddate] [datetime] NOT NULL,
	[usr_createdby] [varchar](255) NOT NULL,
	[usr_updateddate] [datetime] NOT NULL,
	[usr_updatedby] [varchar](255) NOT NULL,
	[usr_hidden] [int] NOT NULL,
	[usr_deleted] [int] NOT NULL,
	[usr_ts] [timestamp] NULL,
 CONSTRAINT [usr_users_PK] PRIMARY KEY NONCLUSTERED 
(
	[usr_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rol_roles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[rol_roles](
	[rol_id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[sta_id] [numeric](18, 0) NOT NULL,
	[lng_id] [numeric](18, 0) NOT NULL,
	[rol_parentid] [numeric](18, 0) NOT NULL,
	[rol_order] [numeric](18, 0) NOT NULL,
	[rol_title] [varchar](255) NOT NULL,
	[rol_alias] [varchar](255) NOT NULL,
	[rol_description] [text] NOT NULL,
	[rol_createddate] [datetime] NOT NULL,
	[rol_createdby] [varchar](255) NOT NULL,
	[rol_updateddate] [datetime] NOT NULL,
	[rol_updatedby] [varchar](255) NOT NULL,
	[rol_hidden] [int] NOT NULL,
	[rol_deleted] [int] NOT NULL,
	[rol_ts] [timestamp] NULL,
 CONSTRAINT [rol_roles_PK] PRIMARY KEY NONCLUSTERED 
(
	[rol_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sta_status]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[sta_status](
	[sta_id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[lng_id] [numeric](18, 0) NOT NULL,
	[sta_parentid] [numeric](18, 0) NOT NULL,
	[sta_order] [numeric](18, 0) NOT NULL,
	[sta_title] [varchar](255) NOT NULL,
	[sta_alias] [varchar](255) NOT NULL,
	[sta_description] [text] NOT NULL,
	[sta_createddate] [datetime] NOT NULL,
	[sta_createdby] [varchar](255) NOT NULL,
	[sta_updateddate] [datetime] NOT NULL,
	[sta_updatedby] [varchar](255) NOT NULL,
	[sta_hidden] [int] NOT NULL,
	[sta_deleted] [int] NOT NULL,
	[sta_ts] [timestamp] NULL,
 CONSTRAINT [sta_status_PK] PRIMARY KEY NONCLUSTERED 
(
	[sta_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_adr_authorizeddocumentsroles_doc_documents]') AND parent_object_id = OBJECT_ID(N'[dbo].[adr_authorizeddocumentsroles]'))
ALTER TABLE [dbo].[adr_authorizeddocumentsroles]  WITH CHECK ADD  CONSTRAINT [FK_adr_authorizeddocumentsroles_doc_documents] FOREIGN KEY([doc_id])
REFERENCES [dbo].[doc_documents] ([doc_id])
GO
ALTER TABLE [dbo].[adr_authorizeddocumentsroles] CHECK CONSTRAINT [FK_adr_authorizeddocumentsroles_doc_documents]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_adr_authorizeddocumentsroles_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[adr_authorizeddocumentsroles]'))
ALTER TABLE [dbo].[adr_authorizeddocumentsroles]  WITH CHECK ADD  CONSTRAINT [FK_adr_authorizeddocumentsroles_lng_language] FOREIGN KEY([lng_id])
REFERENCES [dbo].[lng_language] ([lng_id])
GO
ALTER TABLE [dbo].[adr_authorizeddocumentsroles] CHECK CONSTRAINT [FK_adr_authorizeddocumentsroles_lng_language]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_adr_authorizeddocumentsroles_rol_roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[adr_authorizeddocumentsroles]'))
ALTER TABLE [dbo].[adr_authorizeddocumentsroles]  WITH CHECK ADD  CONSTRAINT [FK_adr_authorizeddocumentsroles_rol_roles] FOREIGN KEY([rol_id])
REFERENCES [dbo].[rol_roles] ([rol_id])
GO
ALTER TABLE [dbo].[adr_authorizeddocumentsroles] CHECK CONSTRAINT [FK_adr_authorizeddocumentsroles_rol_roles]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_adr_authorizeddocumentsroles_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[adr_authorizeddocumentsroles]'))
ALTER TABLE [dbo].[adr_authorizeddocumentsroles]  WITH CHECK ADD  CONSTRAINT [FK_adr_authorizeddocumentsroles_sta_status] FOREIGN KEY([sta_id])
REFERENCES [dbo].[sta_status] ([sta_id])
GO
ALTER TABLE [dbo].[adr_authorizeddocumentsroles] CHECK CONSTRAINT [FK_adr_authorizeddocumentsroles_sta_status]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_apr_authorizedpagesroles_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[apr_authorizedpagesroles]'))
ALTER TABLE [dbo].[apr_authorizedpagesroles]  WITH CHECK ADD  CONSTRAINT [FK_apr_authorizedpagesroles_lng_language] FOREIGN KEY([lng_id])
REFERENCES [dbo].[lng_language] ([lng_id])
GO
ALTER TABLE [dbo].[apr_authorizedpagesroles] CHECK CONSTRAINT [FK_apr_authorizedpagesroles_lng_language]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_apr_authorizedpagesroles_pag_pages]') AND parent_object_id = OBJECT_ID(N'[dbo].[apr_authorizedpagesroles]'))
ALTER TABLE [dbo].[apr_authorizedpagesroles]  WITH CHECK ADD  CONSTRAINT [FK_apr_authorizedpagesroles_pag_pages] FOREIGN KEY([pag_id])
REFERENCES [dbo].[pag_pages] ([pag_id])
GO
ALTER TABLE [dbo].[apr_authorizedpagesroles] CHECK CONSTRAINT [FK_apr_authorizedpagesroles_pag_pages]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_apr_authorizedpagesroles_rol_roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[apr_authorizedpagesroles]'))
ALTER TABLE [dbo].[apr_authorizedpagesroles]  WITH CHECK ADD  CONSTRAINT [FK_apr_authorizedpagesroles_rol_roles] FOREIGN KEY([rol_id])
REFERENCES [dbo].[rol_roles] ([rol_id])
GO
ALTER TABLE [dbo].[apr_authorizedpagesroles] CHECK CONSTRAINT [FK_apr_authorizedpagesroles_rol_roles]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_apr_authorizedpagesroles_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[apr_authorizedpagesroles]'))
ALTER TABLE [dbo].[apr_authorizedpagesroles]  WITH CHECK ADD  CONSTRAINT [FK_apr_authorizedpagesroles_sta_status] FOREIGN KEY([sta_id])
REFERENCES [dbo].[sta_status] ([sta_id])
GO
ALTER TABLE [dbo].[apr_authorizedpagesroles] CHECK CONSTRAINT [FK_apr_authorizedpagesroles_sta_status]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_doc_documents_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[doc_documents]'))
ALTER TABLE [dbo].[doc_documents]  WITH CHECK ADD  CONSTRAINT [FK_doc_documents_lng_language] FOREIGN KEY([lng_id])
REFERENCES [dbo].[lng_language] ([lng_id])
GO
ALTER TABLE [dbo].[doc_documents] CHECK CONSTRAINT [FK_doc_documents_lng_language]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_doc_documents_mod_modules]') AND parent_object_id = OBJECT_ID(N'[dbo].[doc_documents]'))
ALTER TABLE [dbo].[doc_documents]  WITH CHECK ADD  CONSTRAINT [FK_doc_documents_mod_modules] FOREIGN KEY([mod_id])
REFERENCES [dbo].[mod_modules] ([mod_id])
GO
ALTER TABLE [dbo].[doc_documents] CHECK CONSTRAINT [FK_doc_documents_mod_modules]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_doc_documents_pag_pages]') AND parent_object_id = OBJECT_ID(N'[dbo].[doc_documents]'))
ALTER TABLE [dbo].[doc_documents]  WITH CHECK ADD  CONSTRAINT [FK_doc_documents_pag_pages] FOREIGN KEY([pag_id])
REFERENCES [dbo].[pag_pages] ([pag_id])
GO
ALTER TABLE [dbo].[doc_documents] CHECK CONSTRAINT [FK_doc_documents_pag_pages]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_doc_documents_sit_sites]') AND parent_object_id = OBJECT_ID(N'[dbo].[doc_documents]'))
ALTER TABLE [dbo].[doc_documents]  WITH CHECK ADD  CONSTRAINT [FK_doc_documents_sit_sites] FOREIGN KEY([sit_id])
REFERENCES [dbo].[sit_sites] ([sit_id])
GO
ALTER TABLE [dbo].[doc_documents] CHECK CONSTRAINT [FK_doc_documents_sit_sites]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_doc_documents_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[doc_documents]'))
ALTER TABLE [dbo].[doc_documents]  WITH CHECK ADD  CONSTRAINT [FK_doc_documents_sta_status] FOREIGN KEY([sta_id])
REFERENCES [dbo].[sta_status] ([sta_id])
GO
ALTER TABLE [dbo].[doc_documents] CHECK CONSTRAINT [FK_doc_documents_sta_status]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_asr_authorizedsitesroles_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[asr_authorizedsitesroles]'))
ALTER TABLE [dbo].[asr_authorizedsitesroles]  WITH CHECK ADD  CONSTRAINT [FK_asr_authorizedsitesroles_lng_language] FOREIGN KEY([lng_id])
REFERENCES [dbo].[lng_language] ([lng_id])
GO
ALTER TABLE [dbo].[asr_authorizedsitesroles] CHECK CONSTRAINT [FK_asr_authorizedsitesroles_lng_language]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_asr_authorizedsitesroles_rol_roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[asr_authorizedsitesroles]'))
ALTER TABLE [dbo].[asr_authorizedsitesroles]  WITH CHECK ADD  CONSTRAINT [FK_asr_authorizedsitesroles_rol_roles] FOREIGN KEY([rol_id])
REFERENCES [dbo].[rol_roles] ([rol_id])
GO
ALTER TABLE [dbo].[asr_authorizedsitesroles] CHECK CONSTRAINT [FK_asr_authorizedsitesroles_rol_roles]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_asr_authorizedsitesroles_sit_sites]') AND parent_object_id = OBJECT_ID(N'[dbo].[asr_authorizedsitesroles]'))
ALTER TABLE [dbo].[asr_authorizedsitesroles]  WITH CHECK ADD  CONSTRAINT [FK_asr_authorizedsitesroles_sit_sites] FOREIGN KEY([sit_id])
REFERENCES [dbo].[sit_sites] ([sit_id])
GO
ALTER TABLE [dbo].[asr_authorizedsitesroles] CHECK CONSTRAINT [FK_asr_authorizedsitesroles_sit_sites]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_asr_authorizedsitesroles_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[asr_authorizedsitesroles]'))
ALTER TABLE [dbo].[asr_authorizedsitesroles]  WITH CHECK ADD  CONSTRAINT [FK_asr_authorizedsitesroles_sta_status] FOREIGN KEY([sta_id])
REFERENCES [dbo].[sta_status] ([sta_id])
GO
ALTER TABLE [dbo].[asr_authorizedsitesroles] CHECK CONSTRAINT [FK_asr_authorizedsitesroles_sta_status]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mod_modules_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[mod_modules]'))
ALTER TABLE [dbo].[mod_modules]  WITH CHECK ADD  CONSTRAINT [FK_mod_modules_lng_language] FOREIGN KEY([lng_id])
REFERENCES [dbo].[lng_language] ([lng_id])
GO
ALTER TABLE [dbo].[mod_modules] CHECK CONSTRAINT [FK_mod_modules_lng_language]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mod_modules_mde_moduledefinitions]') AND parent_object_id = OBJECT_ID(N'[dbo].[mod_modules]'))
ALTER TABLE [dbo].[mod_modules]  WITH CHECK ADD  CONSTRAINT [FK_mod_modules_mde_moduledefinitions] FOREIGN KEY([mde_id])
REFERENCES [dbo].[mde_moduledefinitions] ([mde_id])
GO
ALTER TABLE [dbo].[mod_modules] CHECK CONSTRAINT [FK_mod_modules_mde_moduledefinitions]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mod_modules_pag_pages]') AND parent_object_id = OBJECT_ID(N'[dbo].[mod_modules]'))
ALTER TABLE [dbo].[mod_modules]  WITH CHECK ADD  CONSTRAINT [FK_mod_modules_pag_pages] FOREIGN KEY([pag_id])
REFERENCES [dbo].[pag_pages] ([pag_id])
GO
ALTER TABLE [dbo].[mod_modules] CHECK CONSTRAINT [FK_mod_modules_pag_pages]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mod_modules_sit_sites]') AND parent_object_id = OBJECT_ID(N'[dbo].[mod_modules]'))
ALTER TABLE [dbo].[mod_modules]  WITH CHECK ADD  CONSTRAINT [FK_mod_modules_sit_sites] FOREIGN KEY([sit_id])
REFERENCES [dbo].[sit_sites] ([sit_id])
GO
ALTER TABLE [dbo].[mod_modules] CHECK CONSTRAINT [FK_mod_modules_sit_sites]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mod_modules_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[mod_modules]'))
ALTER TABLE [dbo].[mod_modules]  WITH CHECK ADD  CONSTRAINT [FK_mod_modules_sta_status] FOREIGN KEY([sta_id])
REFERENCES [dbo].[sta_status] ([sta_id])
GO
ALTER TABLE [dbo].[mod_modules] CHECK CONSTRAINT [FK_mod_modules_sta_status]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_amr_authorizedmodulesroles_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[amr_authorizedmodulesroles]'))
ALTER TABLE [dbo].[amr_authorizedmodulesroles]  WITH CHECK ADD  CONSTRAINT [FK_amr_authorizedmodulesroles_lng_language] FOREIGN KEY([lng_id])
REFERENCES [dbo].[lng_language] ([lng_id])
GO
ALTER TABLE [dbo].[amr_authorizedmodulesroles] CHECK CONSTRAINT [FK_amr_authorizedmodulesroles_lng_language]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_amr_authorizedmodulesroles_mod_modules]') AND parent_object_id = OBJECT_ID(N'[dbo].[amr_authorizedmodulesroles]'))
ALTER TABLE [dbo].[amr_authorizedmodulesroles]  WITH CHECK ADD  CONSTRAINT [FK_amr_authorizedmodulesroles_mod_modules] FOREIGN KEY([mod_id])
REFERENCES [dbo].[mod_modules] ([mod_id])
GO
ALTER TABLE [dbo].[amr_authorizedmodulesroles] CHECK CONSTRAINT [FK_amr_authorizedmodulesroles_mod_modules]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_amr_authorizedmodulesroles_rol_roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[amr_authorizedmodulesroles]'))
ALTER TABLE [dbo].[amr_authorizedmodulesroles]  WITH CHECK ADD  CONSTRAINT [FK_amr_authorizedmodulesroles_rol_roles] FOREIGN KEY([rol_id])
REFERENCES [dbo].[rol_roles] ([rol_id])
GO
ALTER TABLE [dbo].[amr_authorizedmodulesroles] CHECK CONSTRAINT [FK_amr_authorizedmodulesroles_rol_roles]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_amr_authorizedmodulesroles_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[amr_authorizedmodulesroles]'))
ALTER TABLE [dbo].[amr_authorizedmodulesroles]  WITH CHECK ADD  CONSTRAINT [FK_amr_authorizedmodulesroles_sta_status] FOREIGN KEY([sta_id])
REFERENCES [dbo].[sta_status] ([sta_id])
GO
ALTER TABLE [dbo].[amr_authorizedmodulesroles] CHECK CONSTRAINT [FK_amr_authorizedmodulesroles_sta_status]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_sit_sites_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[sit_sites]'))
ALTER TABLE [dbo].[sit_sites]  WITH CHECK ADD  CONSTRAINT [FK_sit_sites_lng_language] FOREIGN KEY([lng_id])
REFERENCES [dbo].[lng_language] ([lng_id])
GO
ALTER TABLE [dbo].[sit_sites] CHECK CONSTRAINT [FK_sit_sites_lng_language]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_sit_sites_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[sit_sites]'))
ALTER TABLE [dbo].[sit_sites]  WITH CHECK ADD  CONSTRAINT [FK_sit_sites_sta_status] FOREIGN KEY([sta_id])
REFERENCES [dbo].[sta_status] ([sta_id])
GO
ALTER TABLE [dbo].[sit_sites] CHECK CONSTRAINT [FK_sit_sites_sta_status]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_set_settings_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[set_settings]'))
ALTER TABLE [dbo].[set_settings]  WITH CHECK ADD  CONSTRAINT [FK_set_settings_lng_language] FOREIGN KEY([lng_id])
REFERENCES [dbo].[lng_language] ([lng_id])
GO
ALTER TABLE [dbo].[set_settings] CHECK CONSTRAINT [FK_set_settings_lng_language]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_set_settings_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[set_settings]'))
ALTER TABLE [dbo].[set_settings]  WITH CHECK ADD  CONSTRAINT [FK_set_settings_sta_status] FOREIGN KEY([sta_id])
REFERENCES [dbo].[sta_status] ([sta_id])
GO
ALTER TABLE [dbo].[set_settings] CHECK CONSTRAINT [FK_set_settings_sta_status]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_obd_objectdata_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[obd_objectdata]'))
ALTER TABLE [dbo].[obd_objectdata]  WITH CHECK ADD  CONSTRAINT [FK_obd_objectdata_lng_language] FOREIGN KEY([lng_id])
REFERENCES [dbo].[lng_language] ([lng_id])
GO
ALTER TABLE [dbo].[obd_objectdata] CHECK CONSTRAINT [FK_obd_objectdata_lng_language]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_obd_objectdata_mod_modules]') AND parent_object_id = OBJECT_ID(N'[dbo].[obd_objectdata]'))
ALTER TABLE [dbo].[obd_objectdata]  WITH CHECK ADD  CONSTRAINT [FK_obd_objectdata_mod_modules] FOREIGN KEY([mod_id])
REFERENCES [dbo].[mod_modules] ([mod_id])
GO
ALTER TABLE [dbo].[obd_objectdata] CHECK CONSTRAINT [FK_obd_objectdata_mod_modules]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_obd_objectdata_pag_pages]') AND parent_object_id = OBJECT_ID(N'[dbo].[obd_objectdata]'))
ALTER TABLE [dbo].[obd_objectdata]  WITH CHECK ADD  CONSTRAINT [FK_obd_objectdata_pag_pages] FOREIGN KEY([pag_id])
REFERENCES [dbo].[pag_pages] ([pag_id])
GO
ALTER TABLE [dbo].[obd_objectdata] CHECK CONSTRAINT [FK_obd_objectdata_pag_pages]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_obd_objectdata_sit_sites]') AND parent_object_id = OBJECT_ID(N'[dbo].[obd_objectdata]'))
ALTER TABLE [dbo].[obd_objectdata]  WITH CHECK ADD  CONSTRAINT [FK_obd_objectdata_sit_sites] FOREIGN KEY([sit_id])
REFERENCES [dbo].[sit_sites] ([sit_id])
GO
ALTER TABLE [dbo].[obd_objectdata] CHECK CONSTRAINT [FK_obd_objectdata_sit_sites]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_obd_objectdata_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[obd_objectdata]'))
ALTER TABLE [dbo].[obd_objectdata]  WITH CHECK ADD  CONSTRAINT [FK_obd_objectdata_sta_status] FOREIGN KEY([sta_id])
REFERENCES [dbo].[sta_status] ([sta_id])
GO
ALTER TABLE [dbo].[obd_objectdata] CHECK CONSTRAINT [FK_obd_objectdata_sta_status]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_uro_usersroles_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[uro_usersroles]'))
ALTER TABLE [dbo].[uro_usersroles]  WITH CHECK ADD  CONSTRAINT [FK_uro_usersroles_lng_language] FOREIGN KEY([lng_id])
REFERENCES [dbo].[lng_language] ([lng_id])
GO
ALTER TABLE [dbo].[uro_usersroles] CHECK CONSTRAINT [FK_uro_usersroles_lng_language]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_uro_usersroles_rol_roles]') AND parent_object_id = OBJECT_ID(N'[dbo].[uro_usersroles]'))
ALTER TABLE [dbo].[uro_usersroles]  WITH CHECK ADD  CONSTRAINT [FK_uro_usersroles_rol_roles] FOREIGN KEY([rol_id])
REFERENCES [dbo].[rol_roles] ([rol_id])
GO
ALTER TABLE [dbo].[uro_usersroles] CHECK CONSTRAINT [FK_uro_usersroles_rol_roles]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_uro_usersroles_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[uro_usersroles]'))
ALTER TABLE [dbo].[uro_usersroles]  WITH CHECK ADD  CONSTRAINT [FK_uro_usersroles_sta_status] FOREIGN KEY([sta_id])
REFERENCES [dbo].[sta_status] ([sta_id])
GO
ALTER TABLE [dbo].[uro_usersroles] CHECK CONSTRAINT [FK_uro_usersroles_sta_status]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_uro_usersroles_usr_users]') AND parent_object_id = OBJECT_ID(N'[dbo].[uro_usersroles]'))
ALTER TABLE [dbo].[uro_usersroles]  WITH CHECK ADD  CONSTRAINT [FK_uro_usersroles_usr_users] FOREIGN KEY([usr_id])
REFERENCES [dbo].[usr_users] ([usr_id])
GO
ALTER TABLE [dbo].[uro_usersroles] CHECK CONSTRAINT [FK_uro_usersroles_usr_users]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mde_moduledefinitions_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[mde_moduledefinitions]'))
ALTER TABLE [dbo].[mde_moduledefinitions]  WITH CHECK ADD  CONSTRAINT [FK_mde_moduledefinitions_lng_language] FOREIGN KEY([lng_id])
REFERENCES [dbo].[lng_language] ([lng_id])
GO
ALTER TABLE [dbo].[mde_moduledefinitions] CHECK CONSTRAINT [FK_mde_moduledefinitions_lng_language]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mde_moduledefinitions_sit_sites]') AND parent_object_id = OBJECT_ID(N'[dbo].[mde_moduledefinitions]'))
ALTER TABLE [dbo].[mde_moduledefinitions]  WITH CHECK ADD  CONSTRAINT [FK_mde_moduledefinitions_sit_sites] FOREIGN KEY([sit_id])
REFERENCES [dbo].[sit_sites] ([sit_id])
GO
ALTER TABLE [dbo].[mde_moduledefinitions] CHECK CONSTRAINT [FK_mde_moduledefinitions_sit_sites]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mde_moduledefinitions_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[mde_moduledefinitions]'))
ALTER TABLE [dbo].[mde_moduledefinitions]  WITH CHECK ADD  CONSTRAINT [FK_mde_moduledefinitions_sta_status] FOREIGN KEY([sta_id])
REFERENCES [dbo].[sta_status] ([sta_id])
GO
ALTER TABLE [dbo].[mde_moduledefinitions] CHECK CONSTRAINT [FK_mde_moduledefinitions_sta_status]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_pag_pages_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[pag_pages]'))
ALTER TABLE [dbo].[pag_pages]  WITH CHECK ADD  CONSTRAINT [FK_pag_pages_lng_language] FOREIGN KEY([lng_id])
REFERENCES [dbo].[lng_language] ([lng_id])
GO
ALTER TABLE [dbo].[pag_pages] CHECK CONSTRAINT [FK_pag_pages_lng_language]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_pag_pages_sit_sites]') AND parent_object_id = OBJECT_ID(N'[dbo].[pag_pages]'))
ALTER TABLE [dbo].[pag_pages]  WITH CHECK ADD  CONSTRAINT [FK_pag_pages_sit_sites] FOREIGN KEY([sit_id])
REFERENCES [dbo].[sit_sites] ([sit_id])
GO
ALTER TABLE [dbo].[pag_pages] CHECK CONSTRAINT [FK_pag_pages_sit_sites]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_pag_pages_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[pag_pages]'))
ALTER TABLE [dbo].[pag_pages]  WITH CHECK ADD  CONSTRAINT [FK_pag_pages_sta_status] FOREIGN KEY([sta_id])
REFERENCES [dbo].[sta_status] ([sta_id])
GO
ALTER TABLE [dbo].[pag_pages] CHECK CONSTRAINT [FK_pag_pages_sta_status]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mdi_modelitems_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[mdi_modelitems]'))
ALTER TABLE [dbo].[mdi_modelitems]  WITH CHECK ADD  CONSTRAINT [FK_mdi_modelitems_lng_language] FOREIGN KEY([lng_id])
REFERENCES [dbo].[lng_language] ([lng_id])
GO
ALTER TABLE [dbo].[mdi_modelitems] CHECK CONSTRAINT [FK_mdi_modelitems_lng_language]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mdi_modelitems_mde_moduledefinitions]') AND parent_object_id = OBJECT_ID(N'[dbo].[mdi_modelitems]'))
ALTER TABLE [dbo].[mdi_modelitems]  WITH CHECK ADD  CONSTRAINT [FK_mdi_modelitems_mde_moduledefinitions] FOREIGN KEY([mde_id])
REFERENCES [dbo].[mde_moduledefinitions] ([mde_id])
GO
ALTER TABLE [dbo].[mdi_modelitems] CHECK CONSTRAINT [FK_mdi_modelitems_mde_moduledefinitions]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mdi_modelitems_mdl_model]') AND parent_object_id = OBJECT_ID(N'[dbo].[mdi_modelitems]'))
ALTER TABLE [dbo].[mdi_modelitems]  WITH CHECK ADD  CONSTRAINT [FK_mdi_modelitems_mdl_model] FOREIGN KEY([mdl_id])
REFERENCES [dbo].[mdl_model] ([mdl_id])
GO
ALTER TABLE [dbo].[mdi_modelitems] CHECK CONSTRAINT [FK_mdi_modelitems_mdl_model]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mdi_modelitems_sit_sites]') AND parent_object_id = OBJECT_ID(N'[dbo].[mdi_modelitems]'))
ALTER TABLE [dbo].[mdi_modelitems]  WITH CHECK ADD  CONSTRAINT [FK_mdi_modelitems_sit_sites] FOREIGN KEY([sit_id])
REFERENCES [dbo].[sit_sites] ([sit_id])
GO
ALTER TABLE [dbo].[mdi_modelitems] CHECK CONSTRAINT [FK_mdi_modelitems_sit_sites]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mdi_modelitems_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[mdi_modelitems]'))
ALTER TABLE [dbo].[mdi_modelitems]  WITH CHECK ADD  CONSTRAINT [FK_mdi_modelitems_sta_status] FOREIGN KEY([sta_id])
REFERENCES [dbo].[sta_status] ([sta_id])
GO
ALTER TABLE [dbo].[mdi_modelitems] CHECK CONSTRAINT [FK_mdi_modelitems_sta_status]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mdl_model_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[mdl_model]'))
ALTER TABLE [dbo].[mdl_model]  WITH CHECK ADD  CONSTRAINT [FK_mdl_model_lng_language] FOREIGN KEY([lng_id])
REFERENCES [dbo].[lng_language] ([lng_id])
GO
ALTER TABLE [dbo].[mdl_model] CHECK CONSTRAINT [FK_mdl_model_lng_language]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mdl_model_sit_sites]') AND parent_object_id = OBJECT_ID(N'[dbo].[mdl_model]'))
ALTER TABLE [dbo].[mdl_model]  WITH CHECK ADD  CONSTRAINT [FK_mdl_model_sit_sites] FOREIGN KEY([sit_id])
REFERENCES [dbo].[sit_sites] ([sit_id])
GO
ALTER TABLE [dbo].[mdl_model] CHECK CONSTRAINT [FK_mdl_model_sit_sites]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mdl_model_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[mdl_model]'))
ALTER TABLE [dbo].[mdl_model]  WITH CHECK ADD  CONSTRAINT [FK_mdl_model_sta_status] FOREIGN KEY([sta_id])
REFERENCES [dbo].[sta_status] ([sta_id])
GO
ALTER TABLE [dbo].[mdl_model] CHECK CONSTRAINT [FK_mdl_model_sta_status]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ust_usertypes_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[ust_usertypes]'))
ALTER TABLE [dbo].[ust_usertypes]  WITH CHECK ADD  CONSTRAINT [FK_ust_usertypes_lng_language] FOREIGN KEY([lng_id])
REFERENCES [dbo].[lng_language] ([lng_id])
GO
ALTER TABLE [dbo].[ust_usertypes] CHECK CONSTRAINT [FK_ust_usertypes_lng_language]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ust_usertypes_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[ust_usertypes]'))
ALTER TABLE [dbo].[ust_usertypes]  WITH CHECK ADD  CONSTRAINT [FK_ust_usertypes_sta_status] FOREIGN KEY([sta_id])
REFERENCES [dbo].[sta_status] ([sta_id])
GO
ALTER TABLE [dbo].[ust_usertypes] CHECK CONSTRAINT [FK_ust_usertypes_sta_status]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_usr_users_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[usr_users]'))
ALTER TABLE [dbo].[usr_users]  WITH CHECK ADD  CONSTRAINT [FK_usr_users_lng_language] FOREIGN KEY([lng_id])
REFERENCES [dbo].[lng_language] ([lng_id])
GO
ALTER TABLE [dbo].[usr_users] CHECK CONSTRAINT [FK_usr_users_lng_language]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_usr_users_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[usr_users]'))
ALTER TABLE [dbo].[usr_users]  WITH CHECK ADD  CONSTRAINT [FK_usr_users_sta_status] FOREIGN KEY([sta_id])
REFERENCES [dbo].[sta_status] ([sta_id])
GO
ALTER TABLE [dbo].[usr_users] CHECK CONSTRAINT [FK_usr_users_sta_status]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_usr_users_ust_usertypes]') AND parent_object_id = OBJECT_ID(N'[dbo].[usr_users]'))
ALTER TABLE [dbo].[usr_users]  WITH CHECK ADD  CONSTRAINT [FK_usr_users_ust_usertypes] FOREIGN KEY([ust_id])
REFERENCES [dbo].[ust_usertypes] ([ust_id])
GO
ALTER TABLE [dbo].[usr_users] CHECK CONSTRAINT [FK_usr_users_ust_usertypes]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_rol_roles_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[rol_roles]'))
ALTER TABLE [dbo].[rol_roles]  WITH CHECK ADD  CONSTRAINT [FK_rol_roles_lng_language] FOREIGN KEY([lng_id])
REFERENCES [dbo].[lng_language] ([lng_id])
GO
ALTER TABLE [dbo].[rol_roles] CHECK CONSTRAINT [FK_rol_roles_lng_language]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_rol_roles_sta_status]') AND parent_object_id = OBJECT_ID(N'[dbo].[rol_roles]'))
ALTER TABLE [dbo].[rol_roles]  WITH CHECK ADD  CONSTRAINT [FK_rol_roles_sta_status] FOREIGN KEY([sta_id])
REFERENCES [dbo].[sta_status] ([sta_id])
GO
ALTER TABLE [dbo].[rol_roles] CHECK CONSTRAINT [FK_rol_roles_sta_status]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_sta_status_lng_language]') AND parent_object_id = OBJECT_ID(N'[dbo].[sta_status]'))
ALTER TABLE [dbo].[sta_status]  WITH CHECK ADD  CONSTRAINT [FK_sta_status_lng_language] FOREIGN KEY([lng_id])
REFERENCES [dbo].[lng_language] ([lng_id])
GO
ALTER TABLE [dbo].[sta_status] CHECK CONSTRAINT [FK_sta_status_lng_language]


/*
DELETE FROM obd_objectdata
DELETE FROM set_settings
DELETE FROM mod_modules
DELETE FROM pag_pages
DELETE FROM mdi_modelitems
DELETE FROM mdl_model
DELETE FROM mde_moduledefinitions
DELETE FROM sit_sites
DELETE FROM sta_status
DELETE FROM lng_language
*/

-- lng_language
INSERT INTO [lng_language]
           ([lng_parentid]
           ,[lng_order]
           ,[lng_title]
           ,[lng_alias]
           ,[lng_description]
           ,[lng_createddate]
           ,[lng_createdby]
           ,[lng_updateddate]
           ,[lng_updatedby]
           ,[lng_hidden]
           ,[lng_deleted])
     VALUES
           (0
           ,1
           ,'Svenska'
           ,'sv-SE'
           ,''
           ,GETDATE()
           ,'AutoScript'
           ,GETDATE()
           ,'AutoScript'
           ,0
           ,0)

-- sta_status
INSERT INTO [sta_status]
           ([lng_id]
           ,[sta_parentid]
           ,[sta_order]
           ,[sta_title]
           ,[sta_alias]
           ,[sta_description]
           ,[sta_createddate]
           ,[sta_createdby]
           ,[sta_updateddate]
           ,[sta_updatedby]
           ,[sta_hidden]
           ,[sta_deleted])
     VALUES
           (1
           ,0
           ,1
           ,'SystemDefault'
           ,'SystemDefault'
           ,'SystemDefault'
           ,GETDATE()
           ,'AutoScript'
           ,GETDATE()
           ,'AutoScript'
           ,0
           ,0)

-- sit_sites
INSERT INTO [sit_sites]
           ([sta_id]
           ,[lng_id]
           ,[sit_parentid]
           ,[sit_order]
           ,[sit_title]
           ,[sit_alias]
           ,[sit_description]
           ,[sit_createddate]
           ,[sit_createdby]
           ,[sit_updateddate]
           ,[sit_updatedby]
           ,[sit_hidden]
           ,[sit_deleted])
     VALUES
           (1
           ,1
           ,0
           ,1
           ,'SystemSite'
           ,'SystemSite'
           ,'SystemSite'
           ,GETDATE()
           ,'AutoScript'
           ,GETDATE()
           ,'AutoScript'
           ,0
           ,0)

-- mde_moduledefinitions
INSERT INTO [mde_moduledefinitions]
           ([sta_id]
           ,[sit_id]
           ,[lng_id]
           ,[mde_parentid]
           ,[mde_order]
           ,[mde_title]
           ,[mde_alias]
           ,[mde_description]
           ,[mde_src]
           ,[mde_createddate]
           ,[mde_createdby]
           ,[mde_updateddate]
           ,[mde_updatedby]
           ,[mde_hidden]
           ,[mde_deleted])
     VALUES
           (1
           ,1
           ,1
           ,0
           ,1
           ,'SystemModule1'
           ,'SystemModule1'
           ,'SystemModule1'
           ,'SystemModule1.ascx'
           ,GETDATE()
           ,'AutoScript'
           ,GETDATE()
           ,'AutoScript'
           ,0
           ,0)
INSERT INTO [mde_moduledefinitions]
           ([sta_id]
           ,[sit_id]
           ,[lng_id]
           ,[mde_parentid]
           ,[mde_order]
           ,[mde_title]
           ,[mde_alias]
           ,[mde_description]
           ,[mde_src]
           ,[mde_createddate]
           ,[mde_createdby]
           ,[mde_updateddate]
           ,[mde_updatedby]
           ,[mde_hidden]
           ,[mde_deleted])
     VALUES
           (1
           ,1
           ,1
           ,0
           ,3
           ,'SystemModule2'
           ,'SystemModule2'
           ,'SystemModule2'
           ,'SystemModule2.ascx'
           ,GETDATE()
           ,'AutoScript'
           ,GETDATE()
           ,'AutoScript'
           ,0
           ,0)

-- mdl_model
INSERT INTO [mdl_model]
           ([sta_id]
           ,[sit_id]
           ,[lng_id]
           ,[mdl_parentid]
           ,[mdl_order]
           ,[mdl_title]
           ,[mdl_alias]
           ,[mdl_description]
           ,[mdl_createddate]
           ,[mdl_createdby]
           ,[mdl_updateddate]
           ,[mdl_updatedby]
           ,[mdl_hidden]
           ,[mdl_deleted])
     VALUES
           (1
           ,1
           ,1
           ,0
           ,1
           ,'SystemModel1'
           ,'SystemModel1'
           ,'SystemModel1'
           ,GETDATE()
           ,'AutoScript'
           ,GETDATE()
           ,'AutoScript'
           ,0
           ,0)
INSERT INTO [mdl_model]
           ([sta_id]
           ,[sit_id]
           ,[lng_id]
           ,[mdl_parentid]
           ,[mdl_order]
           ,[mdl_title]
           ,[mdl_alias]
           ,[mdl_description]
           ,[mdl_createddate]
           ,[mdl_createdby]
           ,[mdl_updateddate]
           ,[mdl_updatedby]
           ,[mdl_hidden]
           ,[mdl_deleted])
     VALUES
           (1
           ,1
           ,1
           ,0
           ,3
           ,'SystemModel2'
           ,'SystemModel2'
           ,'SystemModel2'
           ,GETDATE()
           ,'AutoScript'
           ,GETDATE()
           ,'AutoScript'
           ,0
           ,0)

-- mdi_modelitems
INSERT INTO [mdi_modelitems]
           ([sta_id]
           ,[sit_id]
           ,[lng_id]
           ,[mdl_id]
           ,[mde_id]
           ,[mdi_parentid]
           ,[mdi_order]
           ,[mdi_contentpane]
           ,[mdi_createddate]
           ,[mdi_createdby]
           ,[mdi_updateddate]
           ,[mdi_updatedby]
           ,[mdi_hidden]
           ,[mdi_deleted])
     VALUES
           (1
           ,1
           ,1
           ,1
           ,1
           ,0
           ,1
           ,'SystemContentPane'
           ,GETDATE()
           ,'AutoScript'
           ,GETDATE()
           ,'AutoScript'
           ,0
           ,0)
INSERT INTO [mdi_modelitems]
           ([sta_id]
           ,[sit_id]
           ,[lng_id]
           ,[mdl_id]
           ,[mde_id]
           ,[mdi_parentid]
           ,[mdi_order]
           ,[mdi_contentpane]
           ,[mdi_createddate]
           ,[mdi_createdby]
           ,[mdi_updateddate]
           ,[mdi_updatedby]
           ,[mdi_hidden]
           ,[mdi_deleted])
     VALUES
           (1
           ,1
           ,1
           ,2
           ,2
           ,0
           ,3
           ,'SystemContentPane'
           ,GETDATE()
           ,'AutoScript'
           ,GETDATE()
           ,'AutoScript'
           ,0
           ,0)

-- pag_pages
INSERT INTO [pag_pages]
           ([sta_id]
           ,[sit_id]
           ,[lng_id]
           ,[mdl_id]
           ,[pag_parentid]
           ,[pag_order]
           ,[pag_title]
           ,[pag_alias]
           ,[pag_description]
           ,[pag_createddate]
           ,[pag_createdby]
           ,[pag_updateddate]
           ,[pag_updatedby]
           ,[pag_hidden]
           ,[pag_deleted])
     VALUES
           (1
           ,1
           ,1
           ,1
           ,0
           ,1
           ,'SystemPage1'
           ,'SystemPage1'
           ,'SystemPage1'
           ,GETDATE()
           ,'AutoScript'
           ,GETDATE()
           ,'AutoScript'
           ,0
           ,0)
INSERT INTO [pag_pages]
           ([sta_id]
           ,[sit_id]
           ,[lng_id]
           ,[mdl_id]
           ,[pag_parentid]
           ,[pag_order]
           ,[pag_title]
           ,[pag_alias]
           ,[pag_description]
           ,[pag_createddate]
           ,[pag_createdby]
           ,[pag_updateddate]
           ,[pag_updatedby]
           ,[pag_hidden]
           ,[pag_deleted])
     VALUES
           (1
           ,1
           ,1
           ,2
           ,0
           ,3
           ,'SystemPage2'
           ,'SystemPage2'
           ,'SystemPage2'
           ,GETDATE()
           ,'AutoScript'
           ,GETDATE()
           ,'AutoScript'
           ,0
           ,0)

-- mod_modules
INSERT INTO [mod_modules]
           ([sta_id]
           ,[sit_id]
           ,[pag_id]
           ,[mde_id]
           ,[lng_id]
           ,[mod_parentid]
           ,[mod_order]
           ,[mod_title]
           ,[mod_alias]
           ,[mod_description]
           ,[mod_createddate]
           ,[mod_createdby]
           ,[mod_updateddate]
           ,[mod_updatedby]
           ,[mod_hidden]
           ,[mod_deleted])
     VALUES
           (1
           ,1
           ,1
           ,1
           ,1
           ,0
           ,1
           ,'SystemModule1'
           ,'SystemModule1'
           ,'SystemModule1'
           ,GETDATE()
           ,'AutoScript'
           ,GETDATE()
           ,'AutoScript'
           ,0
           ,0)
INSERT INTO [mod_modules]
           ([sta_id]
           ,[sit_id]
           ,[pag_id]
           ,[mde_id]
           ,[lng_id]
           ,[mod_parentid]
           ,[mod_order]
           ,[mod_title]
           ,[mod_alias]
           ,[mod_description]
           ,[mod_createddate]
           ,[mod_createdby]
           ,[mod_updateddate]
           ,[mod_updatedby]
           ,[mod_hidden]
           ,[mod_deleted])
     VALUES
           (1
           ,1
           ,2
           ,2
           ,1
           ,0
           ,3
           ,'SystemModule2'
           ,'SystemModule2'
           ,'SystemModule2'
           ,GETDATE()
           ,'AutoScript'
           ,GETDATE()
           ,'AutoScript'
           ,0
           ,0)

-- obd_objectdata
INSERT INTO [obd_objectdata]
           ([sta_id]
           ,[lng_id]
           ,[sit_id]
           ,[pag_id]
           ,[mod_id]
           ,[obd_parentid]
           ,[obd_order]
           ,[obd_type]
           ,[obd_title]
           ,[obd_alias]
           ,[obd_description]
           ,[obd_varchar1]
           ,[obd_varchar2]
           ,[obd_varchar3]
           ,[obd_varchar4]
           ,[obd_varchar5]
           ,[obd_varchar6]
           ,[obd_varchar7]
           ,[obd_varchar8]
           ,[obd_varchar9]
           ,[obd_varchar10]
           ,[obd_createddate]
           ,[obd_createdby]
           ,[obd_updateddate]
           ,[obd_updatedby]
           ,[obd_hidden]
           ,[obd_deleted])
     VALUES
           (1
           ,1
           ,1
           ,1
           ,1
           ,0
           ,1
           ,1
           ,'SystemObjectData1'
           ,'SystemObjectData1'
           ,'SystemObjectData1'
           ,'Data1'
           ,'Data2'
           ,'Data3'
           ,'Data4'
           ,'Data5'
           ,'Data6'
           ,'Data7'
           ,'Data8'
           ,'Data9'
           ,'Data10'
           ,GETDATE()
           ,'AutoScript'
           ,GETDATE()
           ,'AutoScript'
           ,0
           ,0)
INSERT INTO [obd_objectdata]
           ([sta_id]
           ,[lng_id]
           ,[sit_id]
           ,[pag_id]
           ,[mod_id]
           ,[obd_parentid]
           ,[obd_order]
           ,[obd_type]
           ,[obd_title]
           ,[obd_alias]
           ,[obd_description]
           ,[obd_varchar1]
           ,[obd_varchar2]
           ,[obd_varchar3]
           ,[obd_varchar4]
           ,[obd_varchar5]
           ,[obd_varchar6]
           ,[obd_varchar7]
           ,[obd_varchar8]
           ,[obd_varchar9]
           ,[obd_varchar10]
           ,[obd_createddate]
           ,[obd_createdby]
           ,[obd_updateddate]
           ,[obd_updatedby]
           ,[obd_hidden]
           ,[obd_deleted])
     VALUES
           (1
           ,1
           ,1
           ,2
           ,2
           ,0
           ,1
           ,3
           ,'SystemObjectData2'
           ,'SystemObjectData2'
           ,'SystemObjectData2'
           ,'Data1'
           ,'Data2'
           ,'Data3'
           ,'Data4'
           ,'Data5'
           ,'Data6'
           ,'Data7'
           ,'Data8'
           ,'Data9'
           ,'Data10'
           ,GETDATE()
           ,'AutoScript'
           ,GETDATE()
           ,'AutoScript'
           ,0
           ,0)