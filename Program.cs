using System;
using System.IO;

class Test
{
    public static void Main()
    {
        try
        {
            string FolderToArchive = "c:\\test";
            string ArchiveFolderName = "archive";
            // Only get files that begin with the letter "c".
            FolderArchiver(FolderToArchive, ArchiveFolderName);     // moves all folders from {FolderToArchive} to {FolderToArchive}\{ArchiveFolderName}\{yyyyMMdd}\
            FileArchiver(FolderToArchive,ArchiveFolderName);        // moves all files from  {FolderToArchive} to {FolderToArchive}\{ArchiveFolderName}\{yyyyMMdd}\

        }
        catch (Exception e)
        {
            Console.WriteLine("The process failed: {0}", e.ToString());
        }
    }
    public static void FolderValidator(string path, string[] dirs)
    {

        if (Array.Exists(dirs, element => element == path))
        {
            Console.WriteLine("Folder " + path + " already exists.");
        }
        else
        {
            Directory.CreateDirectory(path);
            Console.WriteLine("Folder Created " + path);
        }
    }

    public static void FolderArchiver(string FolderToArchive, string ArchiveFolderName)
    {
        string[] dirs = Directory.GetDirectories(FolderToArchive);
        FolderValidator(FolderToArchive + @"\" + ArchiveFolderName, dirs);  // validates archive folder with current date exists, if not creates it
        foreach (string dir in dirs)
        {
            if (dir != FolderToArchive + @"\" + ArchiveFolderName)
            {
                Console.WriteLine(dir);
                Console.WriteLine(DateTime.Now.ToString("yyyyMMdd"));
                //Directory.Move(@"C:\Test\test", @"c:\test\archive\test" );
                Console.WriteLine(FolderToArchive + @"\" + ArchiveFolderName + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + dir.Replace(FolderToArchive + @"\", ""));
                string[] FolderCheck = Directory.GetDirectories(FolderToArchive + @"\" + ArchiveFolderName + @"\");
                FolderValidator(FolderToArchive + @"\" + ArchiveFolderName + @"\" + DateTime.Now.ToString("yyyyMMdd"), FolderCheck);
                Console.WriteLine("Moving " + dir + " to " + FolderToArchive + @"\" + ArchiveFolderName + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + dir.Replace(FolderToArchive + @"\", ""));
                Directory.Move(dir, FolderToArchive + @"\" + ArchiveFolderName + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + dir.Replace(FolderToArchive + @"\", ""));
                Console.WriteLine(dir);
            }
        }
    }
    public static void FileArchiver(string FolderToArchive, string ArchiveFolderName)
    {
        string[] dirs = Directory.GetFiles(FolderToArchive);
        foreach (string dir in dirs)
        {

                Console.WriteLine(dir);
                Console.WriteLine(DateTime.Now.ToString("yyyyMMdd"));
                //Directory.Move(@"C:\Test\test", @"c:\test\archive\test" );
                Console.WriteLine(FolderToArchive + @"\" + ArchiveFolderName + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + dir.Replace(FolderToArchive + @"\", ""));
                string[] FolderCheck = Directory.GetDirectories(FolderToArchive + @"\" + ArchiveFolderName + @"\");
                FolderValidator(FolderToArchive + @"\" + ArchiveFolderName + @"\" + DateTime.Now.ToString("yyyyMMdd"), FolderCheck);
                Console.WriteLine("Moving " + dir + " to " + FolderToArchive + @"\" + ArchiveFolderName + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + dir.Replace(FolderToArchive + @"\", ""));
                Directory.Move(dir, FolderToArchive + @"\" + ArchiveFolderName + @"\" + DateTime.Now.ToString("yyyyMMdd") + @"\" + dir.Replace(FolderToArchive + @"\", ""));
                Console.WriteLine(dir);

        }
    }
}