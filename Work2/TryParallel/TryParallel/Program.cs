﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using System.IO;

namespace TryParallel
{
    class Program
    {
        static void Main(string[] args)
        {
            //rotateImages();
            DateTime startTime = DateTime.Now;
            DateTime endTime;

            DirectoryInfo diMain = new DirectoryInfo(@"c://");

            DirectoryInfo[] diList = diMain.GetDirectories();

            Parallel.ForEach(diList, (Onedi) =>
             {
                 FileInfo[] fileList = Onedi.GetFiles("*.*", SearchOption.AllDirectories);
                 long size = 0;
                 foreach (FileInfo file in fileList)
                 {
                     size += file.Length;
                 }
                //  Console.WriteLine($"{Onedi.Name}--{size.ToString()}");
             }

                );
            endTime = DateTime.Now;
            Console.WriteLine($"elaspsed time : {endTime - startTime}");

            Console.Read();
        }

        private static void rotateImages()
        {
            // A simple source for demonstration purposes. Modify this path as necessary.
            String[] files = System.IO.Directory.GetFiles(@"C:\Users\Public\Pictures\Sample Pictures", "*.jpg");
            String newDir = @"C:\Users\Public\Pictures\Sample Pictures\Modified";
            System.IO.Directory.CreateDirectory(newDir);

            // Method signature: Parallel.ForEach(IEnumerable<TSource> source, Action<TSource> body)
            // Be sure to add a reference to System.Drawing.dll.
            Parallel.ForEach(files, (currentFile) =>
            {
                // The more computational work you do here, the greater 
                // the speedup compared to a sequential foreach loop.
                String filename = System.IO.Path.GetFileName(currentFile);
                var bitmap = new Bitmap(currentFile);

                bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                bitmap.Save(Path.Combine(newDir, filename));

                // Peek behind the scenes to see how work is parallelized.
                // But be aware: Thread contention for the Console slows down parallel loops!!!

                Console.WriteLine("Processing {0} on thread {1}", filename, Thread.CurrentThread.ManagedThreadId);
                //close lambda expression and method invocation
            });


            // Keep the console window open in debug mode.
            Console.WriteLine("Processing complete. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
