using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using FileMove;

class Test
{
    public static void Main()
    {
        try
        {

            using (var db = new FileArchiveContext())
            {
                FileArchive tmp = db.FileArchives.Find(1);
                var tests = db.FileArchives;
                foreach (FileArchive test in tests)
                {

                    FolderArchiver(test);     // moves all folders from {archive.FolderPath} to {archive.FolderPath}\{archive.ArchiveName}\{yyyyMMdd}\
                    FileArchiver(test);        // moves all files from  {archive.FolderPath} to {archive.FolderPath}\{archive.ArchiveName}\{yyyyMMdd}\

                }

            }

        }
        catch (Exception e)
        {
            Console.WriteLine("The process failed: {0}", e.ToString());
        }
    }
    public static void FolderValidator(string path, string[] dirs)
    {
        try
        {
            if (Array.Exists(dirs, element => element == path))
            {
                //console.writeLine("Folder " + path + " already exists.");
            }
            else
            {
                Directory.CreateDirectory(path);
                //console.writeLine("Folder Created " + path);
            }
        }

        catch (Exception e)
        {
            Console.WriteLine("The process failed: {0}", e.ToString());
        }
    }

    public static void FolderArchiver(FileArchive archive)
    {
        try
        {
            string[] dirs = Directory.GetDirectories(archive.FolderPath);
            FolderValidator(archive.FolderPath + @"\" + archive.ArchiveName, dirs);  // validates archive folder with current date exists, if not creates it
            foreach (string dir in dirs)
            {
                if (dir != archive.FolderPath + @"\" + archive.ArchiveName)
                {
                    //console.writeLine(dir);
                    //console.writeLine(DateTime.Now.ToString("yyyyMMdd"));
                    //Directory.Move(@"C:\Test\test", @"c:\test\archive\test" );
                    ////console.writeLine(archive.FolderPath + @"\" + archive.ArchiveName + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + dir.Replace(archive.FolderPath + @"\", ""));
                    string[] FolderCheck = Directory.GetDirectories(archive.FolderPath + @"\" + archive.ArchiveName + @"\");
                    FolderValidator(archive.FolderPath + @"\" + archive.ArchiveName + @"\" + DateTime.Now.ToString("yyyyMMdd"), FolderCheck);
                    //console.writeLine("Moving " + dir + " to " + archive.FolderPath + @"\" + archive.ArchiveName + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + dir.Replace(archive.FolderPath + @"\", ""));
                    Directory.Move(dir, archive.FolderPath + @"\" + archive.ArchiveName + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + dir.Replace(archive.FolderPath + @"\", ""));
                    ArchiveLog log = new ArchiveLog();
                    log.OriginalPath = dir;
                    log.DestinationPath = archive.FolderPath + @"\" + archive.ArchiveName + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + dir.Replace(archive.FolderPath + @"\", "");
                    log.ArchiveDate = DateTime.Now;
                    log.FileArchiveId = archive.FileArchiveId;
                    WriteLog(log);
                    //console.writeLine(dir);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("The process failed: {0}", e.ToString());
        }


    }
    public static void FileArchiver(FileArchive archive)
    {
        try
        {
            string[] dirs = Directory.GetFiles(archive.FolderPath);
            foreach (string dir in dirs)
            {

                //console.writeLine(dir);
                //console.writeLine(DateTime.Now.ToString("yyyyMMdd"));
                //Directory.Move(@"C:\Test\test", @"c:\test\archive\test" );
                //console.writeLine(archive.FolderPath + @"\" + archive.ArchiveName + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + dir.Replace(archive.FolderPath + @"\", ""));
                string[] FolderCheck = Directory.GetDirectories(archive.FolderPath + @"\" + archive.ArchiveName + @"\");
                FolderValidator(archive.FolderPath + @"\" + archive.ArchiveName + @"\" + DateTime.Now.ToString("yyyyMMdd"), FolderCheck);
                //console.writeLine("Moving " + dir + " to " + archive.FolderPath + @"\" + archive.ArchiveName + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + dir.Replace(archive.FolderPath + @"\", ""));
                Directory.Move(dir, archive.FolderPath + @"\" + archive.ArchiveName + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + dir.Replace(archive.FolderPath + @"\", ""));
                ArchiveLog log = new ArchiveLog();
                log.OriginalPath = dir;
                log.DestinationPath = archive.FolderPath + @"\" + archive.ArchiveName + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + dir.Replace(archive.FolderPath + @"\", "");
                log.ArchiveDate = DateTime.Now;
                log.FileArchiveId = archive.FileArchiveId;
                WriteLog(log);
                //console.writeLine(dir);

            }
        }
        catch (Exception e)
        {
            Console.WriteLine("The process failed: {0}", e.ToString());
        }

    }
    public static void WriteLog(ArchiveLog log)
    {
        try
        {
            using (var db = new FileArchiveContext())
            {
                db.Add(log);
                db.SaveChanges();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("The process failed: {0}", e.ToString());
        }
    }

}