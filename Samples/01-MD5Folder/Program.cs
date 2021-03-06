﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace ActorsLifeForMe.MD5Folder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ready to start.");
            Console.ReadLine();

            ProcessFolder(@"N:\iPlayer Recordings\NDCOslo");

            Console.WriteLine("Completed.");
            Console.ReadLine();
        }

        private static void ProcessFolder(string folder)
        {
            var files = System.IO.Directory.GetFiles(folder);
            foreach (var filepath in files)
            {
                Console.WriteLine("Begin {0} : ", Path.GetFileName(filepath));
                Console.WriteLine("End {0} : {1}", Path.GetFileName(filepath), MD5FromFile(filepath));
            }
        }

        private static string MD5FromFile(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                }
            }
        }

        private static void WriteMD5ForFile(string filename)
        {
            var md5Hash = MD5FromFile(filename);

            System.IO.File.WriteAllText(filename + ".tpl.md5", md5Hash);
        }
    }
}
