using static amisa.asserts.ScriptAssert;

namespace amisa.assert.Tests
{
    public class ContainsTest
    {
        [Fact]
        public void Contains1()
        {
            Contains("IF (EXISTS(SELECT * FROM [dbo].[FaktorHead] WHERE", "IF (EXISTS(SELECT * FROM [dbo].[FaktorHead] WHERE");
        }
        [Fact]
        public void Contains2()
        {
            Contains("IF (SELECT  FROM FaktorHead ", "IF (EXISTS(SELECT * FROM [dbo].[FaktorHead] WHERE");
        }
        [Fact]
        public void Contains3()
        {
            try
            {
                Contains("IF (SELECT  FROM FaktorHead * ", "IF (EXISTS(SELECT * FROM [dbo].[FaktorHead] WHERE");
            }
            catch (Exception exp)
            {
                Assert.Equal("fined this worlds :'*' but Can not fined this world: 'faktorhead'", exp.Message);
            }
        }
        [Fact]
        public void Contains4()
        {
            Contains("IF (EXISTS(SELECT * FROM [dbo].[              WHERE", 
                     "IF (EXISTS(SELECT * FROM [dbo].[FaktorHead]   WHERE");
        }
        [Fact]
        public void Contains5()
        {
            try
            {
                Contains("IF IF (EXISTS(SELECT * FROM [dbo].[ WHERE", "IF (EXISTS(SELECT * FROM [dbo].[FaktorHead] WHERE");
            }
            catch (Exception exp)
            {
                Assert.Equal("fined this worlds :'where,[dbo].[,from,*,(exists(select' but Can not fined this: 'if,if'", exp.Message);
            }
        }
        [Fact]
        public void Contains51()
        {
            try
            {
                Contains("IF IF IF (EXISTS(SELECT * FROM [dbo].[ WHERE", "IF IF (EXISTS(SELECT * FROM [dbo].[FaktorHead] WHERE");
            }
            catch (Exception exp)
            {
                Assert.Equal("fined this worlds :'where,[dbo].[,from,*,(exists(select,if' but Can not fined this: 'if,if'", exp.Message);
            }
        }
        [Fact]
        public void Contains53()
        {
            try
            {
                Contains("IF (EXISTS(SELECT * FROM IF [dbo].[ WHERE", "IF IF (EXISTS(SELECT * FROM [dbo].[FaktorHead] WHERE");
            }
            catch (Exception exp)
            {
                Assert.Equal("fined this worlds :'where,[dbo].[,if' but Can not fined this world: 'from'", exp.Message);
            }
        }
        [Fact]
        public void Contains6()
        {
            try
            {
                Contains("IF (EXISTS(SELECT * FROM [dbo].[ WHERE WHERE ", "IF (EXISTS(SELECT * FROM [dbo].[FaktorHead] WHERE");
            }
            catch (Exception exp)
            {
                Assert.Equal("fined this worlds :'where' but Can not fined this world: 'where'", exp.Message);
            }
        }
        [Fact]
        public void Contains7()
        {
            Contains("BEGIN " +
                "CREATE TABLE [dbo].[tbl_axKarbarLockData] ( " +
                "[SN_tbl_axKarbarLockData]\t[Int]\tIDENTITY (1, 1) NOT NULL, " +
                " [DataTimeS] DateTime CONSTRAINT [DF_tbl_axKarbarLockData_DataTimeS] " +
                "IF (NOT EXISTS (SELECT * FROM sys.columns WHERE object_id ", 
                
            @"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_axKarbarLockData]') AND type in (N'U'))
    BEGIN
        CREATE TABLE [dbo].[tbl_axKarbarLockData] (
        [SN_tbl_axKarbarLockData]	[Int]	IDENTITY (1, 1) NOT NULL,
        [GuidID] uniqueidentifier CONSTRAINT [DF_tbl_axKarbarLockData_GuidID] DEFAULT (newid()) NOT NULL,
        [DateTimeCI] DateTime CONSTRAINT [DF_tbl_axKarbarLockData_DateTimeCI] DEFAULT (1800/1/1) NOT NULL,
        [DateTimeCU] DateTime CONSTRAINT [DF_tbl_axKarbarLockData_DateTimeCU] DEFAULT (1800/1/1) NOT NULL,
        [CountCU] Int CONSTRAINT [DF_tbl_axKarbarLockData_CountCU]  NOT NULL,
        [Num] Int CONSTRAINT [DF_tbl_axKarbarLockData_Num]  NOT NULL,
        [SerialNumber] varchar(20) CONSTRAINT [DF_tbl_axKarbarLockData_SerialNumber] DEFAULT '' NOT NULL,
        [DataTimeS] DateTime CONSTRAINT [DF_tbl_axKarbarLockData_DataTimeS] DEFAULT (1800/1/1) NOT NULL,
        [GuidData] uniqueidentifier CONSTRAINT [DF_tbl_axKarbarLockData_GuidData] DEFAULT (newid()) NOT NULL,
        [Data] varchar(MAX) CONSTRAINT [DF_tbl_axKarbarLockData_Data]  NULL,
        [Decription] varchar(MAX) CONSTRAINT [DF_tbl_axKarbarLockData_Decription]  NULL
        CONSTRAINT [PK_tbl_axKarbarLockData] PRIMARY KEY CLUSTERED ([SN_tbl_axKarbarLockData] ASC),
        );
    END
ELSE
    BEGIN
        IF (NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[tbl_axKarbarLockData]') AND name = N'GuidID')) 
        BEGIN
            IF EXISTS(SELECT * FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID) = 'DF_tbl_axKarbarLockData_GuidID' ANd OBJECT_NAME(parent_object_id) = 'tbl_axKarbarLockData') ALTER TABLE [dbo].[tbl_axKarbarLockData] DROP CONSTRAINT [DF_tbl_axKarbarLockData_GuidID] 
            ALTER TABLE [dbo].[tbl_axKarbarLockData] ADD [GuidID] uniqueidentifier CONSTRAINT [DF_tbl_axKarbarLockData_GuidID] DEFAULT (newid()) NOT NULL
        END
        IF (NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[tbl_axKarbarLockData]') AND name = N'DateTimeCI')) 
        BEGIN
            IF EXISTS(SELECT * FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID) = 'DF_tbl_axKarbarLockData_DateTimeCI' ANd OBJECT_NAME(parent_object_id) = 'tbl_axKarbarLockData') ALTER TABLE [dbo].[tbl_axKarbarLockData] DROP CONSTRAINT [DF_tbl_axKarbarLockData_DateTimeCI] 
            ALTER TABLE [dbo].[tbl_axKarbarLockData] ADD [DateTimeCI] DateTime CONSTRAINT [DF_tbl_axKarbarLockData_DateTimeCI] DEFAULT (1800/1/1) NOT NULL
        END
        IF (NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[tbl_axKarbarLockData]') AND name = N'DateTimeCU')) 
        BEGIN
            IF EXISTS(SELECT * FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID) = 'DF_tbl_axKarbarLockData_DateTimeCU' ANd OBJECT_NAME(parent_object_id) = 'tbl_axKarbarLockData') ALTER TABLE [dbo].[tbl_axKarbarLockData] DROP CONSTRAINT [DF_tbl_axKarbarLockData_DateTimeCU] 
            ALTER TABLE [dbo].[tbl_axKarbarLockData] ADD [DateTimeCU] DateTime CONSTRAINT [DF_tbl_axKarbarLockData_DateTimeCU] DEFAULT (1800/1/1) NOT NULL
        END
        IF (NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[tbl_axKarbarLockData]') AND name = N'CountCU')) 
        BEGIN
            IF EXISTS(SELECT * FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID) = 'DF_tbl_axKarbarLockData_CountCU' ANd OBJECT_NAME(parent_object_id) = 'tbl_axKarbarLockData') ALTER TABLE [dbo].[tbl_axKarbarLockData] DROP CONSTRAINT [DF_tbl_axKarbarLockData_CountCU] 
            ALTER TABLE [dbo].[tbl_axKarbarLockData] ADD [CountCU] Int CONSTRAINT [DF_tbl_axKarbarLockData_CountCU]  NOT NULL
        END
        IF (NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[tbl_axKarbarLockData]') AND name = N'Num')) 
        BEGIN
            IF EXISTS(SELECT * FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID) = 'DF_tbl_axKarbarLockData_Num' ANd OBJECT_NAME(parent_object_id) = 'tbl_axKarbarLockData') ALTER TABLE [dbo].[tbl_axKarbarLockData] DROP CONSTRAINT [DF_tbl_axKarbarLockData_Num] 
            ALTER TABLE [dbo].[tbl_axKarbarLockData] ADD [Num] Int CONSTRAINT [DF_tbl_axKarbarLockData_Num]  NOT NULL
        END
        IF (NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[tbl_axKarbarLockData]') AND name = N'SerialNumber')) 
        BEGIN
            IF EXISTS(SELECT * FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID) = 'DF_tbl_axKarbarLockData_SerialNumber' ANd OBJECT_NAME(parent_object_id) = 'tbl_axKarbarLockData') ALTER TABLE [dbo].[tbl_axKarbarLockData] DROP CONSTRAINT [DF_tbl_axKarbarLockData_SerialNumber] 
            ALTER TABLE [dbo].[tbl_axKarbarLockData] ADD [SerialNumber] varchar(20) CONSTRAINT [DF_tbl_axKarbarLockData_SerialNumber] DEFAULT '' NOT NULL
        END
        IF (NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[tbl_axKarbarLockData]') AND name = N'DataTimeS')) 
        BEGIN
            IF EXISTS(SELECT * FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID) = 'DF_tbl_axKarbarLockData_DataTimeS' ANd OBJECT_NAME(parent_object_id) = 'tbl_axKarbarLockData') ALTER TABLE [dbo].[tbl_axKarbarLockData] DROP CONSTRAINT [DF_tbl_axKarbarLockData_DataTimeS] 
            ALTER TABLE [dbo].[tbl_axKarbarLockData] ADD [DataTimeS] DateTime CONSTRAINT [DF_tbl_axKarbarLockData_DataTimeS] DEFAULT (1800/1/1) NOT NULL
        END
        IF (NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[tbl_axKarbarLockData]') AND name = N'GuidData')) 
        BEGIN
            IF EXISTS(SELECT * FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID) = 'DF_tbl_axKarbarLockData_GuidData' ANd OBJECT_NAME(parent_object_id) = 'tbl_axKarbarLockData') ALTER TABLE [dbo].[tbl_axKarbarLockData] DROP CONSTRAINT [DF_tbl_axKarbarLockData_GuidData] 
            ALTER TABLE [dbo].[tbl_axKarbarLockData] ADD [GuidData] uniqueidentifier CONSTRAINT [DF_tbl_axKarbarLockData_GuidData] DEFAULT (newid()) NOT NULL
        END
        IF (NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[tbl_axKarbarLockData]') AND name = N'Data')) 
        BEGIN
            IF EXISTS(SELECT * FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID) = 'DF_tbl_axKarbarLockData_Data' ANd OBJECT_NAME(parent_object_id) = 'tbl_axKarbarLockData') ALTER TABLE [dbo].[tbl_axKarbarLockData] DROP CONSTRAINT [DF_tbl_axKarbarLockData_Data] 
            ALTER TABLE [dbo].[tbl_axKarbarLockData] ADD [Data] varchar(MAX) CONSTRAINT [DF_tbl_axKarbarLockData_Data]  NULL
        END
        IF (NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[tbl_axKarbarLockData]') AND name = N'Decription')) 
        BEGIN
            IF EXISTS(SELECT * FROM sys.objects WHERE type_desc LIKE '%CONSTRAINT' AND OBJECT_NAME(OBJECT_ID) = 'DF_tbl_axKarbarLockData_Decription' ANd OBJECT_NAME(parent_object_id) = 'tbl_axKarbarLockData') ALTER TABLE [dbo].[tbl_axKarbarLockData] DROP CONSTRAINT [DF_tbl_axKarbarLockData_Decription] 
            ALTER TABLE [dbo].[tbl_axKarbarLockData] ADD [Decription] varchar(MAX) CONSTRAINT [DF_tbl_axKarbarLockData_Decription]  NULL
        END
    END");
        }
    }
}