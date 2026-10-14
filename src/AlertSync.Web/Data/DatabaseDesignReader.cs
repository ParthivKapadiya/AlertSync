using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AlertSync.Web.Data;

public sealed class TableDesign
{
    public string Name { get; init; } = string.Empty;

    public string EntityName { get; init; } = string.Empty;

    public bool IsIdentityTable { get; init; }

    public IReadOnlyList<ColumnDesign> Columns { get; init; } = [];

    public IReadOnlyList<IndexDesign> Indexes { get; init; } = [];

    public IReadOnlyList<ForeignKeyDesign> ForeignKeys { get; init; } = [];
}

public sealed class ColumnDesign
{
    public string Name { get; init; } = string.Empty;

    public string StoreType { get; init; } = string.Empty;

    public bool IsNullable { get; init; }

    public bool IsPrimaryKey { get; init; }

    public int? MaxLength { get; init; }

    public string? ForeignKeyTo { get; init; }
}

public sealed class IndexDesign
{
    public string Name { get; init; } = string.Empty;

    public string Columns { get; init; } = string.Empty;

    public bool IsUnique { get; init; }
}

public sealed class ForeignKeyDesign
{
    public string Columns { get; init; } = string.Empty;

    public string PrincipalTable { get; init; } = string.Empty;

    public string DeleteBehavior { get; init; } = string.Empty;
}

public class DatabaseDesignReader
{
    private readonly AlertSyncDbContext _db;

    public DatabaseDesignReader(AlertSyncDbContext db)
    {
        _db = db;
    }

    public IReadOnlyList<TableDesign> ReadTables()
    {
        var tables = new List<TableDesign>();

        foreach (var entity in _db.Model.GetEntityTypes())
        {
            var tableName = entity.GetTableName();
            if (string.IsNullOrEmpty(tableName))
            {
                continue;
            }

            var store = StoreObjectIdentifier.Table(tableName, entity.GetSchema());
            var foreignKeys = entity.GetForeignKeys().ToList();

            var columns = entity.GetProperties()
                .Select(property =>
                {
                    var columnName = property.GetColumnName(store);
                    if (columnName is null)
                    {
                        return null;
                    }

                    var foreignKey = foreignKeys.FirstOrDefault(key => key.Properties.Contains(property));
                    return new ColumnDesign
                    {
                        Name = columnName,
                        StoreType = property.GetColumnType(store),
                        IsNullable = property.IsNullable,
                        IsPrimaryKey = property.IsPrimaryKey(),
                        MaxLength = property.GetMaxLength(),
                        ForeignKeyTo = foreignKey?.PrincipalEntityType.GetTableName()
                    };
                })
                .Where(column => column is not null)
                .Cast<ColumnDesign>()
                .OrderByDescending(column => column.IsPrimaryKey)
                .ThenBy(column => column.Name)
                .ToList();

            tables.Add(new TableDesign
            {
                Name = tableName,
                EntityName = entity.ClrType.Name,
                IsIdentityTable = tableName.StartsWith("AspNet", StringComparison.Ordinal),
                Columns = columns,
                Indexes = entity.GetIndexes()
                    .Select(index => new IndexDesign
                    {
                        Name = index.GetDatabaseName() ?? index.Properties.First().Name,
                        Columns = string.Join(", ", index.Properties.Select(property => property.GetColumnName(store) ?? property.Name)),
                        IsUnique = index.IsUnique
                    })
                    .ToList(),
                ForeignKeys = foreignKeys
                    .Select(key => new ForeignKeyDesign
                    {
                        Columns = string.Join(", ", key.Properties.Select(property => property.GetColumnName(store) ?? property.Name)),
                        PrincipalTable = key.PrincipalEntityType.GetTableName() ?? key.PrincipalEntityType.Name,
                        DeleteBehavior = key.DeleteBehavior.ToString()
                    })
                    .ToList()
            });
        }

        return tables
            .OrderBy(table => table.IsIdentityTable)
            .ThenBy(table => table.Name)
            .ToList();
    }
}
