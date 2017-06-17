using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using OnmyojiHelper;

namespace OnmyojiHelper.Migrations
{
    [DbContext(typeof(OnmyojiContext))]
    partial class OnmyojiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("OnmyojiHelper.Models.Battle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("StageId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("StageId");

                    b.ToTable("Battle");
                });

            modelBuilder.Entity("OnmyojiHelper.Models.Bounty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ShikigamiId");

                    b.HasKey("Id");

                    b.HasIndex("ShikigamiId");

                    b.ToTable("Bounty");
                });

            modelBuilder.Entity("OnmyojiHelper.Models.Clue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Keyword");

                    b.HasKey("Id");

                    b.ToTable("Clue");
                });

            modelBuilder.Entity("OnmyojiHelper.Models.Relations.BountyClue", b =>
                {
                    b.Property<int>("BountyId");

                    b.Property<int>("ClueId");

                    b.HasKey("BountyId", "ClueId");

                    b.HasIndex("ClueId");

                    b.ToTable("BountyClue");
                });

            modelBuilder.Entity("OnmyojiHelper.Models.Relations.ShikigamiBattle", b =>
                {
                    b.Property<int>("ShikigamiId");

                    b.Property<int>("BattleId");

                    b.Property<int>("Count");

                    b.HasKey("ShikigamiId", "BattleId");

                    b.HasIndex("BattleId");

                    b.ToTable("ShikigamiBattle");
                });

            modelBuilder.Entity("OnmyojiHelper.Models.Shikigami", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("Rarity");

                    b.HasKey("Id");

                    b.ToTable("Shikigami");
                });

            modelBuilder.Entity("OnmyojiHelper.Models.Stage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Category");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Stage");
                });

            modelBuilder.Entity("OnmyojiHelper.Models.Battle", b =>
                {
                    b.HasOne("OnmyojiHelper.Models.Stage", "Stage")
                        .WithMany("Battles")
                        .HasForeignKey("StageId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("OnmyojiHelper.Models.Bounty", b =>
                {
                    b.HasOne("OnmyojiHelper.Models.Shikigami", "Shikigami")
                        .WithMany()
                        .HasForeignKey("ShikigamiId");
                });

            modelBuilder.Entity("OnmyojiHelper.Models.Relations.BountyClue", b =>
                {
                    b.HasOne("OnmyojiHelper.Models.Bounty", "Bounty")
                        .WithMany("BountyClues")
                        .HasForeignKey("BountyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OnmyojiHelper.Models.Clue", "Clue")
                        .WithMany("BountyClues")
                        .HasForeignKey("ClueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OnmyojiHelper.Models.Relations.ShikigamiBattle", b =>
                {
                    b.HasOne("OnmyojiHelper.Models.Battle", "Battle")
                        .WithMany("ShikigamiBattles")
                        .HasForeignKey("BattleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OnmyojiHelper.Models.Shikigami", "Shikigami")
                        .WithMany("ShikigamiBattles")
                        .HasForeignKey("ShikigamiId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
