using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnmyojiHelper.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Keyword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shikigami",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Rarity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shikigami", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Category = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bounty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ShikigamiId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bounty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bounty_Shikigami_ShikigamiId",
                        column: x => x.ShikigamiId,
                        principalTable: "Shikigami",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Battle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StageId = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Battle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Battle_Stage_StageId",
                        column: x => x.StageId,
                        principalTable: "Stage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "BountyClue",
                columns: table => new
                {
                    BountyId = table.Column<int>(nullable: false),
                    ClueId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BountyClue", x => new { x.BountyId, x.ClueId });
                    table.ForeignKey(
                        name: "FK_BountyClue_Bounty_BountyId",
                        column: x => x.BountyId,
                        principalTable: "Bounty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BountyClue_Clue_ClueId",
                        column: x => x.ClueId,
                        principalTable: "Clue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShikigamiBattle",
                columns: table => new
                {
                    ShikigamiId = table.Column<int>(nullable: false),
                    BattleId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShikigamiBattle", x => new { x.ShikigamiId, x.BattleId });
                    table.ForeignKey(
                        name: "FK_ShikigamiBattle_Battle_BattleId",
                        column: x => x.BattleId,
                        principalTable: "Battle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShikigamiBattle_Shikigami_ShikigamiId",
                        column: x => x.ShikigamiId,
                        principalTable: "Shikigami",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Battle_StageId",
                table: "Battle",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_Bounty_ShikigamiId",
                table: "Bounty",
                column: "ShikigamiId");

            migrationBuilder.CreateIndex(
                name: "IX_BountyClue_ClueId",
                table: "BountyClue",
                column: "ClueId");

            migrationBuilder.CreateIndex(
                name: "IX_ShikigamiBattle_BattleId",
                table: "ShikigamiBattle",
                column: "BattleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BountyClue");

            migrationBuilder.DropTable(
                name: "ShikigamiBattle");

            migrationBuilder.DropTable(
                name: "Bounty");

            migrationBuilder.DropTable(
                name: "Clue");

            migrationBuilder.DropTable(
                name: "Battle");

            migrationBuilder.DropTable(
                name: "Shikigami");

            migrationBuilder.DropTable(
                name: "Stage");
        }
    }
}
