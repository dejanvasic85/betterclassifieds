CREATE TABLE [dbo].[Publication] (
    [PublicationId]     INT            IDENTITY (1, 1) NOT NULL,
    [Title]             NVARCHAR (50)  NULL,
    [Description]       NVARCHAR (MAX) NULL,
    [PublicationTypeId] INT            NULL,
    [ImageUrl]          NVARCHAR (255) NULL,
    [FrequencyType]     NVARCHAR (20)  NULL,
    [FrequencyValue]    NVARCHAR (20)  NULL,
    [Active]            BIT            NULL,
    [SortOrder]         INT            NULL,
    CONSTRAINT [PK_Publication] PRIMARY KEY CLUSTERED ([PublicationId] ASC),
    CONSTRAINT [FK_Publication_PublicationType] FOREIGN KEY ([PublicationTypeId]) REFERENCES [dbo].[PublicationType] ([PublicationTypeId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A string representing the frequency (weekly, daily, monthyl etc)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Publication', @level2type = N'COLUMN', @level2name = N'FrequencyType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The days of the week separated by ";" (for e.g. 1 - Monday 2-Tuesday) and 1;2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Publication', @level2type = N'COLUMN', @level2name = N'FrequencyValue';

