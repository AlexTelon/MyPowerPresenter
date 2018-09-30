using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresenterCore
{
    /// <summary>
    /// Load presentations from files.
    /// </summary>
    public class LoadPresentation
    {
        public static string[] ValidFileEndings => new string[] { "md" };

        public static Presentation LoadFromFile(string file)
        {
            var contents = File.ReadAllLines(file);

            return CreatePresentationFromFileContents(contents);
        }

        private static bool IsTitle(string candidate) => candidate[0] == '#';


        public static Presentation CreatePresentationFromFileContents(IEnumerable<string> contents)
        {
            var presentation = new Presentation();

            var currentSlide = new Slide();

            List<string> data = contents.Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToList();

            for (int i = 0; i < data.Count(); i++)
            {
                var line = data[i];

                if (IsTitle(line))
                {
                    // take the title and everything up until the next title
                    var dataUntilNextSlide = data
                        .Skip(i)
                        .TakeWhile((item, index) => index == 0 ? true : !IsTitle(item));

                    var slide = CreateSlide(dataUntilNextSlide);

                    presentation.Add(slide);

                    i += dataUntilNextSlide.Count() - 1;
                } else
                {
                    throw new InvalidDataException($"Line {i}: Expected Title (line starting with #). \nCannot create a new slide without a title! ");
                }
            }

            return presentation;
        }

        private static Slide CreateSlide(IEnumerable<string> slideContents)
        {
            var slide = new Slide();

            // Expecting first a title
            if (slideContents.First().First() == '#')
            {
                slide.Title = slideContents.First().Substring(1);
            }

            // And after title the rest should be body
            slide.Body += string.Join("\n", slideContents.Skip(1));

            return slide;
        }


    }
}
