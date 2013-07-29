using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace m3uCopy  {
    class Playlist : Queue<string[]> {
        private string playlist;
        private string destination;

        public Playlist(string playlist, string destination) {
            this.playlist = playlist;
            this.destination = destination;

            StreamReader reader = new StreamReader(playlist);
            string file;
            while ((file = reader.ReadLine()) != null) {
                this.Enqueue(file);
            }
        }

        public void Enqueue(string file) {
            if (!File.Exists(file)) {
                Console.Error.WriteLine("File '{0}' doesn't exist! Skipping.", file);
                return;
            }
            string filename = Path.GetFileName(file);
            string destFile = Path.Combine(destination, filename);
            if (File.Exists(destFile)) {
                Console.Error.WriteLine("File '{0}' already exists at destination! Skipping.", filename);
                return;
            }

            base.Enqueue(new string[2] {file, destFile});
        }

        public void CopyFiles() {
            string[] item;
            while (this.Count > 0) {
                item = this.Dequeue();
                Console.WriteLine("Copying: '{0}'", Path.GetFileName(item[0]));
                try {
                    File.Copy(item[0], item[1], false);
                } catch (Exception e) {
                    Console.Error.WriteLine("Copying file '{0}' failed!\nError message: '{1}'. Skipping.", item[0], e.ToString());
                }
      
            }
        }

    }
}
