using System;
using System.IO;

namespace fileswork{
    class File{
        public string Path {get; set;}
        public string readFile(){
            try{
                using (StreamReader reader = new StreamReader(Path))
                {
                    string fileContents = reader.ReadToEnd();
                    return fileContents;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return "";
            }
        } 
        public File(string path){
            Path = path;
        }
    }
}