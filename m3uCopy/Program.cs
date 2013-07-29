using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace m3uCopy {
    class Program  {
        private static Playlist playlist;

        static void Main(string[] args) {
            if (args.Length != 2) Program.ExitWithError("m3uCopy takes 2 arguments.");
                
            string playlist = args[0];
            string destination = args[1];

            if (!File.Exists(playlist)) Program.ExitWithError("PLAYLIST FILE doesn't exists!");
            if (!Directory.Exists(destination)) Program.ExitWithError("OUTPUT DIRECTORY doesn't exist!");

            Console.WriteLine("Enqueuing files...");
            Program.playlist = new Playlist(playlist, destination);

            Console.WriteLine("{0} {1} in queue.\nCopying...", Program.playlist.Count, (Program.playlist.Count == 1 ? "file" : "files"));
            Program.playlist.CopyFiles();
            Console.WriteLine("Done.");
        }

        static void ExitWithError(String text, int exitCode = 1) {
            Console.Error.WriteLine(text);
            Console.WriteLine();
            Program.WriteHelp();
            Environment.Exit(exitCode);
        }

        static void WriteHelp() {
            Console.WriteLine("Usage: m3uCopy [PLAYLIST FILE] [OUTPUT DIRECTORY]");
            Console.WriteLine("Copies files from PLAYLIST FILE to OUTPUT DIRECTORY.");
        }
    }
}
