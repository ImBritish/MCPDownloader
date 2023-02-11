using System.Diagnostics;
using System.IO.Compression;
using System.Net;

namespace Polly
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "MCP Downloader";
            Console.Write("Enter a file path: ");
            string filePath = Console.ReadLine();
            WebClient wc = new WebClient();
            Console.WriteLine("Downloading...");
            wc.DownloadFile("http://www.modcoderpack.com/files/mcp918.zip", filePath + @"\1.8.8.zip");
            if (File.Exists(filePath + "/1.8.8.zip"))
            {
                ZipFile.ExtractToDirectory(filePath + @"\1.8.8.zip", filePath + @"\1.8.8");
                File.Delete(filePath + @"\1.8.8.zip");
                Console.WriteLine("Downloaded and extracted!");
                string newPath = filePath + @"\1.8.8\";
                if (File.Exists(newPath + "decompile.bat"))
                {
                    Console.WriteLine("Decompiling...");
                    Console.WriteLine(newPath + "decompile.bat");
                    //change the working directory to the decompile.bat directory
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.WorkingDirectory = newPath;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = "/C decompile.bat";
                    Console.Clear();
                    Process.Start(startInfo);
                    //wait for the decompile.bat to finish
                    Process[] pname = Process.GetProcessesByName("cmd");
                    pname[0].WaitForExit();
                    string Optifine = "https://raw.githubusercontent.com/Hexeption/Optifine-SRC/master/Optifine%20SRC%20Version%20%5B1.8.8%20HD%20U%20H8%5D.zip";
                    Console.Clear();
                    Console.WriteLine("Would you like optifine? (Y/N)");
                    if (Console.ReadKey().Equals(ConsoleKey.Y))
                    {
                        wc.DownloadFile(Optifine, filePath + @"\Optifine.zip");
                        if (File.Exists(filePath + @"\Optifine.zip"))
                        {
                            string[] files = Directory.GetFiles(filePath + @"\1.8.8\src\minecraft");
                            foreach (string file in files)
                            {
                                File.Delete(file);
                            }
                            ZipFile.ExtractToDirectory(filePath + @"\Optifine.zip", filePath + @"\1.8.8\src\minecraft");
                            File.Delete(filePath + @"\Optifine.zip");
                            Console.Clear();
                            Console.WriteLine("Optifine Downloaded Successfuly! Press any key to exit...");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Finished!");
                    }
                }
            }
            else
            {
                Console.WriteLine("Error File dosent exist");
            }
        }
    }
}