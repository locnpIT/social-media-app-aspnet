﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using social_media_app.Data;

#nullable disable

namespace social_media_app.Migrations
{
    [DbContext(typeof(SocialMediaContext))]
    [Migration("20241130211027_AddAllowNull")]
    partial class AddAllowNull
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("chats_users", b =>
                {
                    b.Property<int>("chatsId")
                        .HasColumnType("int");

                    b.Property<int>("usersId")
                        .HasColumnType("int");

                    b.HasKey("chatsId", "usersId");

                    b.HasIndex("usersId");

                    b.ToTable("chats_users");
                });

            modelBuilder.Entity("comments_liked", b =>
                {
                    b.Property<int>("likedCommentId")
                        .HasColumnType("int");

                    b.Property<int>("likedId")
                        .HasColumnType("int");

                    b.HasKey("likedCommentId", "likedId");

                    b.HasIndex("likedId");

                    b.ToTable("comments_liked");
                });

            modelBuilder.Entity("post_comments", b =>
                {
                    b.Property<int>("commentsId")
                        .HasColumnType("int");

                    b.Property<int>("postsId")
                        .HasColumnType("int");

                    b.HasKey("commentsId", "postsId");

                    b.HasIndex("postsId");

                    b.ToTable("post_comments");
                });

            modelBuilder.Entity("posts_liked", b =>
                {
                    b.Property<int>("likedId")
                        .HasColumnType("int");

                    b.Property<int>("likedPostId")
                        .HasColumnType("int");

                    b.HasKey("likedId", "likedPostId");

                    b.HasIndex("likedPostId");

                    b.ToTable("posts_liked");
                });

            modelBuilder.Entity("social_media_app.Models.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("chat_image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("chat_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("chats", (string)null);
                });

            modelBuilder.Entity("social_media_app.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("user_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("user_id");

                    b.ToTable("comments");
                });

            modelBuilder.Entity("social_media_app.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("chat_id")
                        .HasColumnType("int");

                    b.Property<string>("content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int?>("user_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("chat_id");

                    b.HasIndex("user_id");

                    b.ToTable("messages");
                });

            modelBuilder.Entity("social_media_app.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("caption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("createAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("user_id")
                        .HasColumnType("int");

                    b.Property<string>("video")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("user_id");

                    b.ToTable("posts", (string)null);
                });

            modelBuilder.Entity("social_media_app.Models.Reels", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("user_id")
                        .HasColumnType("int");

                    b.Property<string>("video")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("user_id");

                    b.ToTable("Reels");
                });

            modelBuilder.Entity("social_media_app.Models.Story", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("caption")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int?>("user_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("user_id");

                    b.ToTable("stories");
                });

            modelBuilder.Entity("social_media_app.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("firstName")
                        .HasColumnType("nvarchar(max)");

                    b.PrimitiveCollection<string>("followers")
                        .HasColumnType("nvarchar(max)");

                    b.PrimitiveCollection<string>("following")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("users_saved_post", b =>
                {
                    b.Property<int>("savedId")
                        .HasColumnType("int");

                    b.Property<int>("savedPostId")
                        .HasColumnType("int");

                    b.HasKey("savedId", "savedPostId");

                    b.HasIndex("savedPostId");

                    b.ToTable("users_saved_post");
                });

            modelBuilder.Entity("chats_users", b =>
                {
                    b.HasOne("social_media_app.Models.Chat", null)
                        .WithMany()
                        .HasForeignKey("chatsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("social_media_app.Models.User", null)
                        .WithMany()
                        .HasForeignKey("usersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("comments_liked", b =>
                {
                    b.HasOne("social_media_app.Models.Comment", null)
                        .WithMany()
                        .HasForeignKey("likedCommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("social_media_app.Models.User", null)
                        .WithMany()
                        .HasForeignKey("likedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("post_comments", b =>
                {
                    b.HasOne("social_media_app.Models.Comment", null)
                        .WithMany()
                        .HasForeignKey("commentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("social_media_app.Models.Post", null)
                        .WithMany()
                        .HasForeignKey("postsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("posts_liked", b =>
                {
                    b.HasOne("social_media_app.Models.User", null)
                        .WithMany()
                        .HasForeignKey("likedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("social_media_app.Models.Post", null)
                        .WithMany()
                        .HasForeignKey("likedPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("social_media_app.Models.Comment", b =>
                {
                    b.HasOne("social_media_app.Models.User", "user")
                        .WithMany("comments")
                        .HasForeignKey("user_id");

                    b.Navigation("user");
                });

            modelBuilder.Entity("social_media_app.Models.Message", b =>
                {
                    b.HasOne("social_media_app.Models.Chat", "chat")
                        .WithMany("messages")
                        .HasForeignKey("chat_id");

                    b.HasOne("social_media_app.Models.User", "user")
                        .WithMany("messages")
                        .HasForeignKey("user_id");

                    b.Navigation("chat");

                    b.Navigation("user");
                });

            modelBuilder.Entity("social_media_app.Models.Post", b =>
                {
                    b.HasOne("social_media_app.Models.User", "user")
                        .WithMany("posts")
                        .HasForeignKey("user_id");

                    b.Navigation("user");
                });

            modelBuilder.Entity("social_media_app.Models.Reels", b =>
                {
                    b.HasOne("social_media_app.Models.User", "user")
                        .WithMany("reels")
                        .HasForeignKey("user_id");

                    b.Navigation("user");
                });

            modelBuilder.Entity("social_media_app.Models.Story", b =>
                {
                    b.HasOne("social_media_app.Models.User", "user")
                        .WithMany("stories")
                        .HasForeignKey("user_id");

                    b.Navigation("user");
                });

            modelBuilder.Entity("users_saved_post", b =>
                {
                    b.HasOne("social_media_app.Models.User", null)
                        .WithMany()
                        .HasForeignKey("savedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("social_media_app.Models.Post", null)
                        .WithMany()
                        .HasForeignKey("savedPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("social_media_app.Models.Chat", b =>
                {
                    b.Navigation("messages");
                });

            modelBuilder.Entity("social_media_app.Models.User", b =>
                {
                    b.Navigation("comments");

                    b.Navigation("messages");

                    b.Navigation("posts");

                    b.Navigation("reels");

                    b.Navigation("stories");
                });
#pragma warning restore 612, 618
        }
    }
}
