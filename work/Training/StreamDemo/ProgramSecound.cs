namespace StreamDemo
{
    public class ProgramSecound
    {
        static void MainSecound()
        {
            /*
             absolute directory path: string value = @"..."
             Relative directory path: place the file in \bin\Debug
             */


            // Code Example One

            //FileStream fs = null;
            //StreamReader reader = null;

            //try
            //{

            //    string path = @"C:\Users\chmyzh1\Source\Repos\CS_CORE\work\Training\StreamDemo\text.txt";
            //    fs = new FileStream(path, FileMode.Open);
            //    reader = new StreamReader(fs);

            //    string content = reader.ReadToEnd();
            //    Console.WriteLine(content);
            //}
            //finally
            //{
            //    // Wichtig: Streams manuell schliessen!
            //    if (reader != null)
            //        reader.Close();

            //    if (fs != null)
            //        fs.Close();
            //}

            // Code Example Two

            //string source = @"c:\users\chmyzh1\source\repos\cs_core\work\training\streamdemo\text.txt";
            //string destination = @"c:\users\chmyzh1\source\repos\cs_core\work\training\streamdemo\copy.txt";

            //using (filestream input = new filestream(source, filemode.open))
            //using (filestream output = new filestream(destination, filemode.create))
            //{
            //    input.copyto(output);
            //}


            // Code Example Three

            string source = @"C:\Users\chmyzh1\Source\Repos\CS_CORE\work\Training\StreamDemo\text.txt";
            string destination = @"C:\Users\chmyzh1\Source\Repos\CS_CORE\work\Training\StreamDemo\copy.txt";

            using (FileStream input = new FileStream(source, FileMode.Open))
            using (FileStream output = new FileStream(destination, FileMode.Create))
            {
                input.CopyTo(output);
            }



        }
    }
}