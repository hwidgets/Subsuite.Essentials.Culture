using CK.Core;
using CK.SqlServer;
using System.Threading.Tasks;

namespace SEC.Data
{
    /// <summary>
    /// This table centralizes every single <see cref="ICultureInfo"/>.
    /// </summary>
    [SqlTable("tCulture", Package = typeof(Package))]
    [Versions("1.0.0")]
    public abstract class CultureTable : SqlTable
    {
        void StObjConstruct()
        {
        }

        /// <summary>
        /// Creates a new <see cref="ICultureInfo"/>.
        /// </summary>
        /// <param name="ctx">Current <see cref="ISqlCallContext"/>.</param>
        /// <param name="actorId">Creator's id.</param>
        /// <param name="languageCode">See <see cref="Language.Code"/>.</param>
        /// <returns>An <see cref="int"/> which is the created-element id.</returns>
        [SqlProcedure("sCultureCreate")]
        public abstract Task<int> CreateCultureAsync(
            ISqlCallContext ctx,
            int actorId,
            int languageCode
        );

        /// <summary>
        /// Deletes an <see cref="ICultureInfo"/>.
        /// </summary>
        /// <param name="ctx">Current <see cref="ISqlCallContext"/>.</param>
        /// <param name="actorId">Deletor's id.</param>
        /// <param name="cultureId">See <see cref="ICultureInfo.CultureId"/>.</param>
        /// <returns></returns>
        [SqlProcedure("sCultureDelete")]
        public abstract Task<bool> DeleteCultureAsync(
            ISqlCallContext ctx,
            int actorId,
            int cultureId
        );
    }
}
