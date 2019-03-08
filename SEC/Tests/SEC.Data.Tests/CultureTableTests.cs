using CK.SqlServer;
using Dapper;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

using static CK.Testing.DBSetupTestHelper;

namespace SEC.Data.Tests
{
    [TestFixture]
    public class CultureTableTests
    {
        private const int systemId = 1;

        private class ExtendedStdCultureInfo : StdCultureInfo
        {
            public ExtendedStdCultureInfo(int cId, int cLang)
            {
                CultureId = cId;
                LanguageCode = cLang;
            }

            public ExtendedStdCultureInfo(int cLang) => LanguageCode = cLang;
        }

        [Test]
        public async Task CreateCulture_withAdmissibleArguments_shouldNotThrowException_and_shouldCreateForesaidCulture()
        {
            using (var ctx = new SqlStandardCallContext())
            {
                var cTable = CK.Core.StObjModelExtension.Obtain<CultureTable>(TestHelper.StObjMap.StObjs);

                // #1. Checks if a culture already exists with the random Language Code.
                // If NOT, moves on. ELSE, repeat generation until objective is complete.
                var rdCode = await GenerateAvailableLanguageCode(ctx, cTable);

                // #2. Creates the Culture object that is next going to be inserted in database.
                var stdObj = new ExtendedStdCultureInfo(rdCode);

                // #3. Inserting the Culture object in database should not present issues.
                Func<Task> act = async () => await cTable.CreateCultureAsync(ctx, systemId, stdObj.LanguageCode);
                act.Should().NotThrow<SqlDetailedException>();

                // #4. Back to step 1.
                stdObj.LanguageCode = await GenerateAvailableLanguageCode(ctx, cTable);

                // #5. Inserts the aforesaid object and asserts that its id is greater than 1.
                var cId = await cTable.CreateCultureAsync(ctx, systemId, stdObj.LanguageCode);
                Assert.That(cId, Is.GreaterThan(1));
            }
        }

        [Test]
        public async Task CreateCulture_whenAlreadyExistingLanguageCode_shouldThrowSqlDetailedException()
        {
            using (var ctx = new SqlStandardCallContext())
            {
                var cTable = CK.Core.StObjModelExtension.Obtain<CultureTable>(TestHelper.StObjMap.StObjs);

                // #1. Prepares the function that is going to throw the SQLDetailedException.
                Func<Task> act = async () => await cTable.CreateCultureAsync(ctx, systemId, 0);

                // #2. Retrieves a supposed already-existing Culture object.
                // If does NOT exist, creates & insert a new one in database to next throw an
                // Exception by trying to re-insert it. ELSE, just tries to insert it in db.
                var stObj = await ctx[cTable].Connection.QueryFirstOrDefaultAsync<StdCultureInfo>
                    ("select * from SEC.tCulture where LanguageCode = 0;");
                if (stObj == null) await cTable.CreateCultureAsync(ctx, systemId, 0);

                // #3. Inserting the foresaid Culture Object should throw an SQLDetailedException.
                act.Should().Throw<SqlDetailedException>();
            }
        }

        [Test]
        public async Task DeleteCulture_whenAlreadyExistingCulture_shouldNotThrowException_and_shouldDeleteForesaidCulture()
        {
            using (var ctx = new SqlStandardCallContext())
            {
                var cTable = CK.Core.StObjModelExtension.Obtain<CultureTable>(TestHelper.StObjMap.StObjs);

                // #1. Creates the Culture object and then inserts it in database.
                var cId = await cTable.CreateCultureAsync(ctx, systemId, await GenerateAvailableLanguageCode(ctx, cTable));

                // #2. Deleting the foresaid Culture object should not present issues.
                Func<Task> act = async () => await cTable.DeleteCultureAsync(ctx, systemId, cId);
                act.Should().NotThrow<SqlDetailedException>();

                // #3. Asserts that deletion process has been successful.
                var stdObj = await ctx[cTable].Connection.QueryFirstOrDefaultAsync<StdCultureInfo>
                    ("select * from SEC.tCulture where CultureId = @cd;", new { cd = cId });
                Assert.That(stdObj, Is.Null);
            }
        }

        private async Task<int> GenerateAvailableLanguageCode(ISqlCallContext ctx, CultureTable cTable, int tried = 0)
        {
            var random = new Random();
            var rdCode = random.Next();
            while (rdCode == tried) random.Next();

            var exists = await ctx[cTable].Connection.QueryFirstOrDefaultAsync<int>
                ("select CultureId from SEC.tCulture where LanguageCode = @cd;", new { cd = rdCode });
            return (exists != 0) ? await GenerateAvailableLanguageCode(ctx, cTable, exists) : rdCode;
        }
    }
}
