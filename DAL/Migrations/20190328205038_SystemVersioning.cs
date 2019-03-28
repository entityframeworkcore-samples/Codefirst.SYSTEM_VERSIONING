using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;

namespace DAL.JecaestevezApp.Migrations
{
    public partial class SystemVersioning : Migration
    {
        List<string> tablesToUpdate = new List<string>
        {
            "Items"
        };
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            OnMigrationUpSystemVersioning(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            OnMigrationDownSystemVersioning(migrationBuilder);
        }

        private void OnMigrationUpSystemVersioning(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"CREATE SCHEMA History");
            foreach (var table in tablesToUpdate)
            {
                string alterStatement = $@"ALTER TABLE {table} ADD SysStartTime datetime2(0) GENERATED ALWAYS AS ROW START HIDDEN
         CONSTRAINT DF_{table}_SysStart DEFAULT GETDATE(), SysEndTime datetime2(0) GENERATED ALWAYS AS ROW END HIDDEN
         CONSTRAINT DF_{table}_SysEnd DEFAULT CONVERT(datetime2 (0), '9999-12-31 23:59:59'),
         PERIOD FOR SYSTEM_TIME (SysStartTime, SysEndTime)";
                migrationBuilder.Sql(alterStatement);
                alterStatement = $@"ALTER TABLE {table} SET (SYSTEM_VERSIONING = ON (HISTORY_TABLE = History.{table}));";
                migrationBuilder.Sql(alterStatement);
            }
        }
        private void OnMigrationDownSystemVersioning(MigrationBuilder migrationBuilder)
        {
            foreach (var table in tablesToUpdate)
            {
                string alterStatement = $@"ALTER TABLE {table} SET (SYSTEM_VERSIONING = OFF);";
                migrationBuilder.Sql(alterStatement);
                alterStatement = $@"ALTER TABLE {table} DROP PERIOD FOR SYSTEM_TIME";
                migrationBuilder.Sql(alterStatement);
                alterStatement = $@"ALTER TABLE {table} DROP DF_{table}_SysStart, DF_{table}_SysEnd";
                migrationBuilder.Sql(alterStatement);
                alterStatement = $@"ALTER TABLE {table} DROP COLUMN SysStartTime, COLUMN SysEndTime";
                migrationBuilder.Sql(alterStatement);
                alterStatement = $@"DROP TABLE History.{table}";
                migrationBuilder.Sql(alterStatement);
            }
            migrationBuilder.Sql($"DROP SCHEMA History");
        }
    }
}
