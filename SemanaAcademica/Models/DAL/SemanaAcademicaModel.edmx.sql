
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/31/2016 17:55:21
-- Generated from EDMX file: C:\Users\Gabriel\Documents\GitHub\semanaTechSis\SemanaAcademica\Models\DAL\SemanaAcademicaModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SemanaAcademica];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Administrador_Pessoa]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Administrador] DROP CONSTRAINT [FK_Administrador_Pessoa];
GO
IF OBJECT_ID(N'[dbo].[FK_Colaborador_Pessoa]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Colaborador] DROP CONSTRAINT [FK_Colaborador_Pessoa];
GO
IF OBJECT_ID(N'[dbo].[FK_Convidado_Pessoa]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Convidado] DROP CONSTRAINT [FK_Convidado_Pessoa];
GO
IF OBJECT_ID(N'[dbo].[FK_Horario_Local]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Horario] DROP CONSTRAINT [FK_Horario_Local];
GO
IF OBJECT_ID(N'[dbo].[FK_Horarioevento_Evento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Horario] DROP CONSTRAINT [FK_Horarioevento_Evento];
GO
IF OBJECT_ID(N'[dbo].[FK_Matricula_Evento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Matricula] DROP CONSTRAINT [FK_Matricula_Evento];
GO
IF OBJECT_ID(N'[dbo].[FK_Matricula_Participante]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Matricula] DROP CONSTRAINT [FK_Matricula_Participante];
GO
IF OBJECT_ID(N'[dbo].[FK_Minicurso_Evento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Minicurso] DROP CONSTRAINT [FK_Minicurso_Evento];
GO
IF OBJECT_ID(N'[dbo].[FK_Oficina_Evento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Oficina] DROP CONSTRAINT [FK_Oficina_Evento];
GO
IF OBJECT_ID(N'[dbo].[FK_Palestra_Evento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Palestra] DROP CONSTRAINT [FK_Palestra_Evento];
GO
IF OBJECT_ID(N'[dbo].[FK_Participacao_Evento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Participacao] DROP CONSTRAINT [FK_Participacao_Evento];
GO
IF OBJECT_ID(N'[dbo].[FK_Participacao_Participante]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Participacao] DROP CONSTRAINT [FK_Participacao_Participante];
GO
IF OBJECT_ID(N'[dbo].[FK_Participante_Pessoa]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Participante] DROP CONSTRAINT [FK_Participante_Pessoa];
GO
IF OBJECT_ID(N'[dbo].[FK_TrabalhoVoluntario_Participante]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TrabalhoVoluntario] DROP CONSTRAINT [FK_TrabalhoVoluntario_Participante];
GO
IF OBJECT_ID(N'[dbo].[FK_Visita_Evento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Visita] DROP CONSTRAINT [FK_Visita_Evento];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Administrador]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Administrador];
GO
IF OBJECT_ID(N'[dbo].[Colaborador]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Colaborador];
GO
IF OBJECT_ID(N'[dbo].[Convidado]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Convidado];
GO
IF OBJECT_ID(N'[dbo].[Evento]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Evento];
GO
IF OBJECT_ID(N'[dbo].[Horario]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Horario];
GO
IF OBJECT_ID(N'[dbo].[Local]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Local];
GO
IF OBJECT_ID(N'[dbo].[Matricula]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Matricula];
GO
IF OBJECT_ID(N'[dbo].[Minicurso]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Minicurso];
GO
IF OBJECT_ID(N'[dbo].[Oficina]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Oficina];
GO
IF OBJECT_ID(N'[dbo].[Palestra]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Palestra];
GO
IF OBJECT_ID(N'[dbo].[Participacao]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Participacao];
GO
IF OBJECT_ID(N'[dbo].[Participante]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Participante];
GO
IF OBJECT_ID(N'[dbo].[Pessoa]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pessoa];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[TrabalhoVoluntario]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TrabalhoVoluntario];
GO
IF OBJECT_ID(N'[dbo].[Visita]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Visita];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Administrador'
CREATE TABLE [dbo].[Administrador] (
    [id_administrador] int IDENTITY(1,1) NOT NULL,
    [id_pessoa] int  NOT NULL
);
GO

-- Creating table 'Colaborador'
CREATE TABLE [dbo].[Colaborador] (
    [id_colaborador] int IDENTITY(1,1) NOT NULL,
    [id_pessoa] int  NOT NULL
);
GO

-- Creating table 'Convidado'
CREATE TABLE [dbo].[Convidado] (
    [id_convidado] int IDENTITY(1,1) NOT NULL,
    [id_pessoa] int  NOT NULL,
    [resumo_curriculo] varchar(max)  NULL
);
GO

-- Creating table 'Evento'
CREATE TABLE [dbo].[Evento] (
    [id_evento] int IDENTITY(1,1) NOT NULL,
    [nome] varchar(200)  NOT NULL,
    [descricao] varchar(max)  NULL
);
GO

-- Creating table 'Horario'
CREATE TABLE [dbo].[Horario] (
    [id_horario] int IDENTITY(1,1) NOT NULL,
    [hora_inicio] datetime  NOT NULL,
    [hora_fim] datetime  NOT NULL,
    [id_local] int  NOT NULL,
    [id_evento] int  NOT NULL
);
GO

-- Creating table 'Local'
CREATE TABLE [dbo].[Local] (
    [id_local] int IDENTITY(1,1) NOT NULL,
    [descricao] varchar(max)  NULL
);
GO

-- Creating table 'Matricula'
CREATE TABLE [dbo].[Matricula] (
    [id_matricula] int IDENTITY(1,1) NOT NULL,
    [id_participante] int  NOT NULL,
    [id_evento] int  NOT NULL
);
GO

-- Creating table 'Minicurso'
CREATE TABLE [dbo].[Minicurso] (
    [id_minicurso] int IDENTITY(1,1) NOT NULL,
    [id_evento] int  NOT NULL,
    [vagas] int  NULL
);
GO

-- Creating table 'Oficina'
CREATE TABLE [dbo].[Oficina] (
    [id_oficina] int IDENTITY(1,1) NOT NULL,
    [id_evento] int  NOT NULL,
    [vagas] int  NULL
);
GO

-- Creating table 'Palestra'
CREATE TABLE [dbo].[Palestra] (
    [id_palestra] int IDENTITY(1,1) NOT NULL,
    [id_evento] int  NOT NULL
);
GO

-- Creating table 'Participacao'
CREATE TABLE [dbo].[Participacao] (
    [id_participacao] int IDENTITY(1,1) NOT NULL,
    [hora_entrada] datetime  NULL,
    [hora_saida] datetime  NULL,
    [id_participante] int  NOT NULL,
    [id_evento] int  NOT NULL
);
GO

-- Creating table 'Participante'
CREATE TABLE [dbo].[Participante] (
    [id_participante] int IDENTITY(1,1) NOT NULL,
    [id_pessoa] int  NOT NULL,
    [registro] varchar(20)  NOT NULL,
    [matricula] bit  NOT NULL,
    [contribuicao] bit  NOT NULL,
    [universidade] varchar(100)  NULL,
    [curso] varchar(100)  NULL
);
GO

-- Creating table 'Pessoa'
CREATE TABLE [dbo].[Pessoa] (
    [id_pessoa] int IDENTITY(1,1) NOT NULL,
    [nome] varchar(200)  NOT NULL,
    [email] varchar(200)  NOT NULL,
    [telefone] varchar(20)  NOT NULL,
    [senha] varchar(50)  NOT NULL,
    [chave] uniqueidentifier  NOT NULL,
    [confirmado] bit  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'TrabalhoVoluntario'
CREATE TABLE [dbo].[TrabalhoVoluntario] (
    [id_trabalho] int IDENTITY(1,1) NOT NULL,
    [horas] int  NOT NULL,
    [id_participante] int  NOT NULL,
    [data_inicio] datetime  NOT NULL,
    [data_fim] datetime  NOT NULL
);
GO

-- Creating table 'Visita'
CREATE TABLE [dbo].[Visita] (
    [id_visita] int IDENTITY(1,1) NOT NULL,
    [id_evento] int  NOT NULL,
    [locomocao] varchar(max)  NULL,
    [contribuicao] varchar(max)  NULL,
    [vagas] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id_administrador] in table 'Administrador'
ALTER TABLE [dbo].[Administrador]
ADD CONSTRAINT [PK_Administrador]
    PRIMARY KEY CLUSTERED ([id_administrador] ASC);
GO

-- Creating primary key on [id_colaborador] in table 'Colaborador'
ALTER TABLE [dbo].[Colaborador]
ADD CONSTRAINT [PK_Colaborador]
    PRIMARY KEY CLUSTERED ([id_colaborador] ASC);
GO

-- Creating primary key on [id_convidado] in table 'Convidado'
ALTER TABLE [dbo].[Convidado]
ADD CONSTRAINT [PK_Convidado]
    PRIMARY KEY CLUSTERED ([id_convidado] ASC);
GO

-- Creating primary key on [id_evento] in table 'Evento'
ALTER TABLE [dbo].[Evento]
ADD CONSTRAINT [PK_Evento]
    PRIMARY KEY CLUSTERED ([id_evento] ASC);
GO

-- Creating primary key on [id_horario] in table 'Horario'
ALTER TABLE [dbo].[Horario]
ADD CONSTRAINT [PK_Horario]
    PRIMARY KEY CLUSTERED ([id_horario] ASC);
GO

-- Creating primary key on [id_local] in table 'Local'
ALTER TABLE [dbo].[Local]
ADD CONSTRAINT [PK_Local]
    PRIMARY KEY CLUSTERED ([id_local] ASC);
GO

-- Creating primary key on [id_matricula] in table 'Matricula'
ALTER TABLE [dbo].[Matricula]
ADD CONSTRAINT [PK_Matricula]
    PRIMARY KEY CLUSTERED ([id_matricula] ASC);
GO

-- Creating primary key on [id_minicurso] in table 'Minicurso'
ALTER TABLE [dbo].[Minicurso]
ADD CONSTRAINT [PK_Minicurso]
    PRIMARY KEY CLUSTERED ([id_minicurso] ASC);
GO

-- Creating primary key on [id_oficina] in table 'Oficina'
ALTER TABLE [dbo].[Oficina]
ADD CONSTRAINT [PK_Oficina]
    PRIMARY KEY CLUSTERED ([id_oficina] ASC);
GO

-- Creating primary key on [id_palestra] in table 'Palestra'
ALTER TABLE [dbo].[Palestra]
ADD CONSTRAINT [PK_Palestra]
    PRIMARY KEY CLUSTERED ([id_palestra] ASC);
GO

-- Creating primary key on [id_participacao] in table 'Participacao'
ALTER TABLE [dbo].[Participacao]
ADD CONSTRAINT [PK_Participacao]
    PRIMARY KEY CLUSTERED ([id_participacao] ASC);
GO

-- Creating primary key on [id_participante] in table 'Participante'
ALTER TABLE [dbo].[Participante]
ADD CONSTRAINT [PK_Participante]
    PRIMARY KEY CLUSTERED ([id_participante] ASC);
GO

-- Creating primary key on [id_pessoa] in table 'Pessoa'
ALTER TABLE [dbo].[Pessoa]
ADD CONSTRAINT [PK_Pessoa]
    PRIMARY KEY CLUSTERED ([id_pessoa] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [id_trabalho] in table 'TrabalhoVoluntario'
ALTER TABLE [dbo].[TrabalhoVoluntario]
ADD CONSTRAINT [PK_TrabalhoVoluntario]
    PRIMARY KEY CLUSTERED ([id_trabalho] ASC);
GO

-- Creating primary key on [id_visita] in table 'Visita'
ALTER TABLE [dbo].[Visita]
ADD CONSTRAINT [PK_Visita]
    PRIMARY KEY CLUSTERED ([id_visita] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [id_pessoa] in table 'Administrador'
ALTER TABLE [dbo].[Administrador]
ADD CONSTRAINT [FK_Administrador_Pessoa]
    FOREIGN KEY ([id_pessoa])
    REFERENCES [dbo].[Pessoa]
        ([id_pessoa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Administrador_Pessoa'
CREATE INDEX [IX_FK_Administrador_Pessoa]
ON [dbo].[Administrador]
    ([id_pessoa]);
GO

-- Creating foreign key on [id_pessoa] in table 'Colaborador'
ALTER TABLE [dbo].[Colaborador]
ADD CONSTRAINT [FK_Colaborador_Pessoa]
    FOREIGN KEY ([id_pessoa])
    REFERENCES [dbo].[Pessoa]
        ([id_pessoa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Colaborador_Pessoa'
CREATE INDEX [IX_FK_Colaborador_Pessoa]
ON [dbo].[Colaborador]
    ([id_pessoa]);
GO

-- Creating foreign key on [id_pessoa] in table 'Convidado'
ALTER TABLE [dbo].[Convidado]
ADD CONSTRAINT [FK_Convidado_Pessoa]
    FOREIGN KEY ([id_pessoa])
    REFERENCES [dbo].[Pessoa]
        ([id_pessoa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Convidado_Pessoa'
CREATE INDEX [IX_FK_Convidado_Pessoa]
ON [dbo].[Convidado]
    ([id_pessoa]);
GO

-- Creating foreign key on [id_evento] in table 'Horario'
ALTER TABLE [dbo].[Horario]
ADD CONSTRAINT [FK_Horarioevento_Evento]
    FOREIGN KEY ([id_evento])
    REFERENCES [dbo].[Evento]
        ([id_evento])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Horarioevento_Evento'
CREATE INDEX [IX_FK_Horarioevento_Evento]
ON [dbo].[Horario]
    ([id_evento]);
GO

-- Creating foreign key on [id_evento] in table 'Matricula'
ALTER TABLE [dbo].[Matricula]
ADD CONSTRAINT [FK_Matricula_Evento]
    FOREIGN KEY ([id_evento])
    REFERENCES [dbo].[Evento]
        ([id_evento])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Matricula_Evento'
CREATE INDEX [IX_FK_Matricula_Evento]
ON [dbo].[Matricula]
    ([id_evento]);
GO

-- Creating foreign key on [id_evento] in table 'Minicurso'
ALTER TABLE [dbo].[Minicurso]
ADD CONSTRAINT [FK_Minicurso_Evento]
    FOREIGN KEY ([id_evento])
    REFERENCES [dbo].[Evento]
        ([id_evento])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Minicurso_Evento'
CREATE INDEX [IX_FK_Minicurso_Evento]
ON [dbo].[Minicurso]
    ([id_evento]);
GO

-- Creating foreign key on [id_evento] in table 'Oficina'
ALTER TABLE [dbo].[Oficina]
ADD CONSTRAINT [FK_Oficina_Evento]
    FOREIGN KEY ([id_evento])
    REFERENCES [dbo].[Evento]
        ([id_evento])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Oficina_Evento'
CREATE INDEX [IX_FK_Oficina_Evento]
ON [dbo].[Oficina]
    ([id_evento]);
GO

-- Creating foreign key on [id_evento] in table 'Palestra'
ALTER TABLE [dbo].[Palestra]
ADD CONSTRAINT [FK_Palestra_Evento]
    FOREIGN KEY ([id_evento])
    REFERENCES [dbo].[Evento]
        ([id_evento])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Palestra_Evento'
CREATE INDEX [IX_FK_Palestra_Evento]
ON [dbo].[Palestra]
    ([id_evento]);
GO

-- Creating foreign key on [id_evento] in table 'Participacao'
ALTER TABLE [dbo].[Participacao]
ADD CONSTRAINT [FK_Participacao_Evento]
    FOREIGN KEY ([id_evento])
    REFERENCES [dbo].[Evento]
        ([id_evento])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Participacao_Evento'
CREATE INDEX [IX_FK_Participacao_Evento]
ON [dbo].[Participacao]
    ([id_evento]);
GO

-- Creating foreign key on [id_evento] in table 'Visita'
ALTER TABLE [dbo].[Visita]
ADD CONSTRAINT [FK_Visita_Evento]
    FOREIGN KEY ([id_evento])
    REFERENCES [dbo].[Evento]
        ([id_evento])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Visita_Evento'
CREATE INDEX [IX_FK_Visita_Evento]
ON [dbo].[Visita]
    ([id_evento]);
GO

-- Creating foreign key on [id_local] in table 'Horario'
ALTER TABLE [dbo].[Horario]
ADD CONSTRAINT [FK_Horario_Local]
    FOREIGN KEY ([id_local])
    REFERENCES [dbo].[Local]
        ([id_local])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Horario_Local'
CREATE INDEX [IX_FK_Horario_Local]
ON [dbo].[Horario]
    ([id_local]);
GO

-- Creating foreign key on [id_participante] in table 'Matricula'
ALTER TABLE [dbo].[Matricula]
ADD CONSTRAINT [FK_Matricula_Participante]
    FOREIGN KEY ([id_participante])
    REFERENCES [dbo].[Participante]
        ([id_participante])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Matricula_Participante'
CREATE INDEX [IX_FK_Matricula_Participante]
ON [dbo].[Matricula]
    ([id_participante]);
GO

-- Creating foreign key on [id_participante] in table 'Participacao'
ALTER TABLE [dbo].[Participacao]
ADD CONSTRAINT [FK_Participacao_Participante]
    FOREIGN KEY ([id_participante])
    REFERENCES [dbo].[Participante]
        ([id_participante])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Participacao_Participante'
CREATE INDEX [IX_FK_Participacao_Participante]
ON [dbo].[Participacao]
    ([id_participante]);
GO

-- Creating foreign key on [id_pessoa] in table 'Participante'
ALTER TABLE [dbo].[Participante]
ADD CONSTRAINT [FK_Participante_Pessoa]
    FOREIGN KEY ([id_pessoa])
    REFERENCES [dbo].[Pessoa]
        ([id_pessoa])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Participante_Pessoa'
CREATE INDEX [IX_FK_Participante_Pessoa]
ON [dbo].[Participante]
    ([id_pessoa]);
GO

-- Creating foreign key on [id_participante] in table 'TrabalhoVoluntario'
ALTER TABLE [dbo].[TrabalhoVoluntario]
ADD CONSTRAINT [FK_TrabalhoVoluntario_Participante]
    FOREIGN KEY ([id_participante])
    REFERENCES [dbo].[Participante]
        ([id_participante])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TrabalhoVoluntario_Participante'
CREATE INDEX [IX_FK_TrabalhoVoluntario_Participante]
ON [dbo].[TrabalhoVoluntario]
    ([id_participante]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------