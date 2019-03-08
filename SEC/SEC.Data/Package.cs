using CK.Setup;
using CK.SqlServer.Setup;

namespace SEC.Data
{
    [SqlPackage(
    ResourcePath = "Resources",
    Schema = "SEC",
    Database = typeof(SqlDefaultDatabase),
    ResourceType = typeof(Package)),
    Versions("1.0.0")]
    public abstract class Package : SqlPackage
    {
        void StObjConstruct()
        {
        }
    }
}