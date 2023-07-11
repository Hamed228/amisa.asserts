using static amisa.asserts.ScriptAssert;

namespace amisa.assert.Tests
{
    public class UnitTest1
    {
        private const string mainScript = @"IF (EXISTS(SELECT * FROM [dbo].[FaktorHead] WHERE
SN_tbl_axFaktorHead = @SN_tbl_axFaktorHead))
BEGIN
    UPDATE
        [dbo].[FaktorHeadsave1]
            SET
                @SN_tbl_axFaktorHead	Int,
@Num1	Int,
@Num2	Int,
@Num3	Int
            WHERE
SN_tbl_axFaktorHead = @SN_tbl_axFaktorHead
END";

        [Fact]
        public void StartWith1()
        {
            StartWith("IF (EXISTS(SELECT * FROM [dbo].[FaktorHead] WHERE", mainScript);
        }
        [Fact]
        public void StartWith2()
        {
            StartWith("IF (   ExISts     (SELECT\n * FROM     [dbo].[FaktorHead] WHERE", mainScript);
        }
        [Fact]
        public void StartWith3()
        {
            var exp = Assert.Throws<Exception>(() => StartWith("IF (   EXISTS WOW \n    (SELECT\n * FROM     [dbo].[FaktorHead] WHERE", mainScript));
            Assert.NotNull(exp);
            Assert.Equal("can not find the word :'wow' from expect subScript :'wow' in actual subScript :'(select'", exp.Message);
        }

        [Fact]
        public void EndWith1()
        {
            EndWith(@"@Num3	Int
            WHERE
SN_tbl_axFaktorHead = @SN_tbl_axFaktorHead
END", mainScript);
        }
        [Fact]
        public void EndWith2()
        {
            EndWith("@Num3\tInt\r\n            WHERE\r\nSN_tbl_axFaktorHead = @SN_tbl_axFaktorHead\r\nEND", mainScript);
        }
        [Fact]
        public void EndWith3()
        {
            EndWith("@Num3  Int             WHERE   SN_tbl_axFaktorHead = @SN_tbl_axFaktorHead      END", mainScript);
        }
        [Fact]
        public void EndWithError()
        {
            var exp = Assert.Throws<Exception>(() => StartWith("IF (   EXISTS WOW \n    (SELECT\n * FROM     [dbo].[FaktorHead] WHERE", mainScript));
            Assert.NotNull(exp);
            Assert.Equal("can not find the word :'wow' from expect subScript :'wow' in actual subScript :'(select'", exp.Message);
        }
        [Fact]
        public void Equals1()
        {
            Equal(mainScript, mainScript);
        }
        [Fact]
        public void Equal2()
        {
            Equal(@"IF (EXISTS(SELECT * FROM [dbo].[FaktorHead] WHERE
SN_tbl_axFaktorHead = @SN_tbl_axFaktorHead))
BEGIN
    UPDATE
        [dbo].[FaktorHeadsave1]
            SET
                @SN_tbl_axFaktorHead	Int,
@Num1	Int,
@Num2	Int,
@Num3	Int
            WHERE
SN_tbl_axFaktorHead = @SN_tbl_axFaktorHead
END", mainScript);
        }
        [Fact]
        public void Equal3()
        {
            Equal("IF  (EXISTS(SELECT * FROM [dbo].[FaktorHead] WHERE SN_tbl_axFaktorHead = @SN_tbl_axFaktorHead)) BEGIN UPDATE [dbo].[FaktorHeadsave1] SET @SN_tbl_axFaktorHead	Int, @Num1	Int, @Num2	Int, @Num3	Int WHERE SN_tbl_axFaktorHead = @SN_tbl_axFaktorHead END", mainScript);
        }
        [Fact]
        public void Equal4()
        {
            Equal("if     (exists    (select     * from      [dbo].[faktorhead]     where sn_tbl_axfaktorhead =     @sn_tbl_axfaktorhead))     begin update     [dbo].[faktorheadsave1]      set       @sn_tbl_axfaktorhead	      int, @num1	int, @num2	int, @num3	int where sn_tbl_axfaktorhead = @sn_tbl_axfaktorhead end", mainScript);
        }
        [Fact]
        public void Equal5()
        {
            Equal("if     (exists    (select     * from      [dbo].[faktorhead]     where sn_tbl_axfaktorhead   " +
                "=     @sn_tbl_axfaktorhead))     " +
                "begin update     [dbo].[faktorheadsave1]      " +
                "set       @sn_tbl_axfaktorhead	      " +
                "int, @num1	int, @num2	int" +
                ", @num3	int where sn_tbl_axfaktorhead = @sn_tbl_axfaktorhead end", mainScript);
        }
        [Fact]
        public void EqualError()
        {
            var exp = Assert.Throws<Exception>(() => Equal("if     (exists    (select     *from      [dbo].[faktorhead]     where sn_tbl_axfaktorhead =     @sn_tbl_axfaktorhead))     begin update     [dbo].[faktorheadsave1]      set       @sn_tbl_axfaktorhead	      int, @num1	int, @num2	int, @num3	int where sn_tbl_axfaktorhead = @sn_tbl_axfaktorhead end", mainScript));
            Assert.NotNull(exp);
            Assert.Equal("can not find the word :'*from' from expect subScript :'*from' in actual subScript :'*'", exp.Message);
        }

        [Fact]
        public void FindInActual1()
        {
            AmisaScript script = new(mainScript);
            Assert.Equal(26, script.SubScript.Length);
            AmisaScript subScript = new("IF (EXISTS(SELECT * FROM [dbo].[FaktorHead] WHERE");
            Assert.Equal(6, subScript.SubScript.Length);
            AmisaScript.FindInActual(script, subScript.SubScript[0].Script!);
            Assert.Equal(25, script.SubScript.Length);
            AmisaScript.FindInActual(script, subScript.SubScript[1].Script!);
            Assert.Equal(24, script.SubScript.Length);
            AmisaScript.FindInActual(script, subScript.SubScript[2].Script!);
            Assert.Equal(23, script.SubScript.Length);
            AmisaScript.FindInActual(script, subScript.SubScript[3].Script!);
            Assert.Equal(22, script.SubScript.Length);
            AmisaScript.FindInActual(script, subScript.SubScript[4].Script!);
            Assert.Equal(21, script.SubScript.Length);
            AmisaScript.FindInActual(script, subScript.SubScript[5].Script!);
            Assert.Equal(20, script.SubScript.Length);
            Assert.Equal("sn_tbl_axfaktorhead", script.SubScript[0].Script);
        }

        [Fact]
        public void FindInActual2()
        {
            AmisaScript script = new(mainScript);
            Assert.Equal(26, script.SubScript.Length);
            AmisaScript subScript = new("IF\t(EXISTS(SELECT * FROM\n [dbo].[FaktorHead] WHERE");
            Assert.Equal(6, subScript.SubScript.Length);
            AmisaScript.FindInActual(script, subScript.SubScript[0].Script!);
            Assert.Equal(25, script.SubScript.Length);
            AmisaScript.FindInActual(script, subScript.SubScript[1].Script!);
            Assert.Equal(24, script.SubScript.Length);
            AmisaScript.FindInActual(script, subScript.SubScript[2].Script!);
            Assert.Equal(23, script.SubScript.Length);
            AmisaScript.FindInActual(script, subScript.SubScript[3].Script!);
            Assert.Equal(22, script.SubScript.Length);
            AmisaScript.FindInActual(script, subScript.SubScript[4].Script!);
            Assert.Equal(21, script.SubScript.Length);
            AmisaScript.FindInActual(script, subScript.SubScript[5].Script!);
            Assert.Equal(20, script.SubScript.Length);
            Assert.Equal("sn_tbl_axfaktorhead", script.SubScript[0].Script);
        }
        [Fact]
        public void FindInActual3()
        {
            AmisaScript script = new(mainScript);
            Assert.Equal(26, script.SubScript.Length);
            AmisaScript subScript = new(@"IF        (EXISTS(SELECT         * 
FROM
[dbo].[FaktorHead] WHERE");
            Assert.Equal(6, subScript.SubScript.Length);
            AmisaScript.FindInActual(script, subScript.SubScript[0].Script!);
            Assert.Equal(25, script.SubScript.Length);
            AmisaScript.FindInActual(script, subScript.SubScript[1].Script!);
            Assert.Equal(24, script.SubScript.Length);
            AmisaScript.FindInActual(script, subScript.SubScript[2].Script!);
            Assert.Equal(23, script.SubScript.Length);
            AmisaScript.FindInActual(script, subScript.SubScript[3].Script!);
            Assert.Equal(22, script.SubScript.Length);
            AmisaScript.FindInActual(script, subScript.SubScript[4].Script!);
            Assert.Equal(21, script.SubScript.Length);
            AmisaScript.FindInActual(script, subScript.SubScript[5].Script!);
            Assert.Equal(20, script.SubScript.Length);
            Assert.Equal("sn_tbl_axfaktorhead", script.SubScript[0].Script);
        }

        [Fact]
        public void FindWordls1()
        {
            string actual = "IF (EXISTS(SELECT * FROM [dbo].[FaktorHead] WHERE";
            string expect = "IF (EXISTS(SELECT * FROM [dbo].[FaktorHead] WHERE";
            AmisaSubScript.FindWords(ref expect, ref actual);
            Assert.Equal("", actual);
        }
        [Fact]
        public void FindWordls2()
        {
            string actual = "IF (EXISTS(SELECT * FROM [dbo].[FaktorHead] WHERE";
            string expect = "IF      (EXISTS      (SELECT    *      FROM     [dbo].[FaktorHead]     WHERE     ";
            AmisaSubScript.FindWords(ref expect, ref actual);
            Assert.Equal("", actual);
        }
        [Fact]
        public void FindWordls3()
        {
            string actual = "IF (EXISTS(SELECT * FROM [dbo].[FaktorHead] WHERE";
            string expect = "IF  WOOOW    (EXISTS      (SELECT    *      FROM     [dbo].[FaktorHead]     WHERE     ";
            var exp = Assert.Throws<Exception>(() => AmisaSubScript.FindWords(ref expect, ref actual));
            Assert.NotNull(exp);
            Assert.Equal("can not find the word :'WOOOW' from expect subScript :'WOOOW(EXISTS(SELECT*FROM[dbo].[FaktorHead]WHERE' in actual subScript :'(EXISTS(SELECT*FROM[dbo].[FaktorHead]WHERE'", exp.Message);
        }
        [Fact]
        public void FindWordls4()
        {
            string actual = "IF (EXISTS(SELECT * FROM [dbo].[FaktorHead] WHERE";
            string expect = "IF";
            AmisaSubScript.FindWords(ref expect, ref actual);
            Assert.Equal("(EXISTS(SELECT*FROM[dbo].[FaktorHead]WHERE", actual);
        }
        [Fact]
        public void FindWordls5()
        {
            string actual = "IF (EXISTS(SELECT * FROM [dbo].[FaktorHead] WHERE";
            string expect = "IF      (EXISTS      (SELECT    *      FROM ";
            AmisaSubScript.FindWords(ref expect, ref actual);
            Assert.Equal("[dbo].[FaktorHead]WHERE", actual);
        }
        [Fact]
        public void FindWordls6()
        {
            string actual = "IF (EXISTS(SELECT * FROM [dbo].[FaktorHead] WHERE";
            string expect = "IF  WOOOW    (EXISTS";
            var exp = Assert.Throws<Exception>(() => AmisaSubScript.FindWords(ref expect, ref actual));
            Assert.NotNull(exp);
            Assert.Equal("can not find the word :'WOOOW' from expect subScript :'WOOOW(EXISTS' in actual subScript :'(EXISTS(SELECT*FROM[dbo].[FaktorHead]WHERE'", exp.Message);
            Assert.Equal("(EXISTS(SELECT*FROM[dbo].[FaktorHead]WHERE", actual);
        }
    }
}