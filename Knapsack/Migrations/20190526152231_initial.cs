using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Knapsack.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "details",
                columns: table => new
                {
                    DetailsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaxWorth = table.Column<int>(nullable: false),
                    ExecutionTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_details", x => x.DetailsId);
                });

            migrationBuilder.CreateTable(
                name: "execution_processes",
                columns: table => new
                {
                    ExecutionProcessId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurrentMaxWorth = table.Column<int>(nullable: false),
                    BestCombination = table.Column<string>(nullable: false),
                    CurrentItemsCombination = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_execution_processes", x => x.ExecutionProcessId);
                });

            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    ItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemName = table.Column<string>(maxLength: 50, nullable: false),
                    Worth = table.Column<int>(nullable: false),
                    Weight = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TaskName = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    PercentComplete = table.Column<int>(nullable: false),
                    DetailsId = table.Column<int>(nullable: false),
                    ExecutionProcessId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_tasks_details_DetailsId",
                        column: x => x.DetailsId,
                        principalTable: "details",
                        principalColumn: "DetailsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tasks_execution_processes_ExecutionProcessId",
                        column: x => x.ExecutionProcessId,
                        principalTable: "execution_processes",
                        principalColumn: "ExecutionProcessId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "taskItem",
                columns: table => new
                {
                    TaskId = table.Column<int>(nullable: false),
                    ItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_taskItem", x => new { x.TaskId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_taskItem_items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_taskItem_tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "tasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_taskItem_ItemId",
                table: "taskItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_DetailsId",
                table: "tasks",
                column: "DetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_ExecutionProcessId",
                table: "tasks",
                column: "ExecutionProcessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "taskItem");

            migrationBuilder.DropTable(
                name: "items");

            migrationBuilder.DropTable(
                name: "tasks");

            migrationBuilder.DropTable(
                name: "details");

            migrationBuilder.DropTable(
                name: "execution_processes");
        }
    }
}
