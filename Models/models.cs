using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FileMove
{
    public class FileArchiveContext : DbContext
    {
        public DbSet<FileArchive> FileArchives { get; set; }
        public DbSet<ArchiveLog> ArchiveLog { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer();
    }

    public class FileArchive
    {
        public int FileArchiveId { get; set; }
        public string FolderPath { get; set; }
        public string ArchiveName { get; set; }
        public bool active { get; set; }
        public List<ArchiveLog> archiveLogs { get; set; }
    }

    public class ArchiveLog
    {
        public int ArchiveLogId { get; set; }
        public string OriginalPath { get; set; }
        public string DestinationPath { get; set; }
        public DateTime ArchiveDate { get; set; }

        public int FileArchiveId { get; set; }


    }

}