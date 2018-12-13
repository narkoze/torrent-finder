using System;
using System.Collections.Generic;
using System.Linq;

namespace TM2
{
    class Converter
    {
        Dictionarys dictionarys = new Dictionarys();
        DateTime dateTime = DateTime.Now;
        public double ConvertToKiloBytes(string size)
        {
            double newSize = 0;

            if (size.EndsWith("TB") || size.EndsWith("GB") || size.EndsWith("MB") || size.EndsWith("KB"))
                double.TryParse(size.Substring(0, size.Length - 2), out newSize);

            if (size.EndsWith("TB"))
            {
                newSize *= 1024 * 1024 * 1024;
            }
            if (size.EndsWith("GB"))
            {
                newSize *= 1024 * 1024;
            }
            if (size.EndsWith("MB"))
            {
                newSize *= 1024;
            }

            return newSize;
        }
        public Dictionary<string, string> PlatformsToUri(string platforms)
        {
            string[] split = platforms.Split(',').Select(p => p.Trim()).ToArray();
            List<string> keys = new List<string>(dictionarys.Platforms.Keys);
            Dictionary<string, string> dic = new Dictionary<string, string>();

            foreach (var i in split)
            {
                foreach (var k in keys)
                {
                    if (i.Contains(k))
                    {
                        dic.Add(i, dictionarys.Platforms[k]);
                    }
                }
            }
            return dic;
        }
        public string MovieGenreEngToLv(string genres)
        {
            string[] split = genres.Split(',').Select(p => p.Trim()).ToArray();
            List<string> list = new List<string>(dictionarys.MovieGenres.Keys);
            string output = (from k in list from g in split where k == g select k).Aggregate("",
                (current, k) => current + dictionarys.MovieGenres[k].Trim() + ", ");
            return output.TrimEnd(' ').Trim(',');
        }
        public string MovieGenreRusToLv(string genres)
        {
            string[] split = genres.Split(',').Select(p => p.Trim()).ToArray();
            List<string> list = new List<string>(dictionarys.MovieGenresRus.Keys);
            string output = (from k in list from g in split where k == g select k).Aggregate("",
                (current, k) => current + dictionarys.MovieGenresRus[k].Trim() + ", ");
            return output.TrimEnd(' ').Trim(',');
        }
        public string MovieGenreLvToEng(string genre)
        {
            List<string> list = new List<string>(dictionarys.MovieGenres.Values);
            foreach (var k in list)
            {
                if (k == genre)
                    genre = dictionarys.MovieGenres.FirstOrDefault(x => x.Value == k).Key;
            }
            return genre;
        }
        public string MovieGenreLvToRus(string genre)
        {
            List<string> list = new List<string>(dictionarys.MovieGenresRus.Values);
            foreach (var k in list)
            {
                if (k == genre)
                    genre = dictionarys.MovieGenresRus.FirstOrDefault(x => x.Value == k).Key;
            }
            return genre;
        }
        public string GameGenreEngToLv(string genres)
        {
            string[] split = genres.Split(',').Select(p => p.Trim()).ToArray();
            List<string> list = new List<string>(dictionarys.GameGenres.Keys);
            string output = (from k in list from g in split where k == g select k).Aggregate("",
                (current, k) => current + dictionarys.GameGenres[k].Trim() + ", ");
            return output.TrimEnd(' ').Trim(',');
        }
        public string GameGenreLvToEng(string genre)
        {
            List<string> list = new List<string>(dictionarys.GameGenres.Values);
            foreach (var k in list)
            {
                if (k == genre)
                    genre = dictionarys.GameGenres.FirstOrDefault(x => x.Value == k).Key;
            }
            return genre;
        }
        public string SizeRussianToEng(string size)
        {
            var sizeParts = size.Split(' ');

            var list = new List<string>(dictionarys.SizeRussian.Keys);
            var output = "";
            foreach (var k in list)
            {
                if (k == sizeParts[1])
                    output = sizeParts[0] + " " + dictionarys.SizeRussian[k];
            }
            return output;
        }
        public string CategoryToParametersFano(string Category)
        {
            List<string> list = new List<string>(dictionarys.FanoParameters.Keys);
            var output = "";
            foreach (var k in list)
            {
                if (k == Category)
                    output = dictionarys.FanoParameters[k];
            }
            return output;
        }
        public string TypeFano(string input)
        {
            List<string> list = new List<string>(dictionarys.TypeFano.Keys);
            var output = "";
            foreach (var k in list)
            {
                if (k == input)
                    output = dictionarys.TypeFano[k];
            }
            return output;
        }
        public string DateFano(string date)
        {
            //Šodien, 13:14
            //Vakar, 16:31
            //Feb 1 2016, 22:25
            //Jan 31 2016, 19:39
            
            string day;
            string month;
            string year;

            if (date == "Jauns")
            {
                day = dateTime.Day.ToString();
                month = dateTime.Month.ToString();
                year = dateTime.Year.ToString();

                if (day.Length == 1)
                    day = "0" + day;
                if (month.Length == 1)
                    month = "0" + month;

                return day + "/" + month + "/" + year;
            }

            string[] dateParts = date.Split(',');

            switch (dateParts[0])
            {
                case "Šodien":
                    day = dateTime.Day.ToString();
                    month = dateTime.Month.ToString();
                    year = dateTime.Year.ToString();
                    break;
                case "Vakar":
                    DateTime yesterday = DateTime.Now.AddDays(-1);
                    month = yesterday.Month.ToString();
                    day = yesterday.Day.ToString();
                    year = yesterday.Year.ToString();
                    break;
                default:
                    string[] newDate = dateParts[0].Split(' '); //Jan 31 2016
                    month = newDate[0];
                    day = newDate[1];
                    year = newDate[2];
                    switch (month)
                    {
                        case "Jan":
                            month = "01";
                            break;
                        case "Feb":
                            month = "02";
                            break;
                        case "Mar":
                            month = "03";
                            break;
                        case "Apr":
                            month = "04";
                            break;
                        case "May":
                            month = "05";
                            break;
                        case "Jun":
                            month = "06";
                            break;
                        case "Jul":
                            month = "07";
                            break;
                        case "Aug":
                            month = "08";
                            break;
                        case "Sep":
                            month = "09";
                            break;
                        case "Oct":
                            month = "10";
                            break;
                        case "Nov":
                            month = "11";
                            break;
                        case "Dec":
                            month = "12";
                            break;
                    }

                    break;
            }
            if (day.Length == 1)
                day = "0" + day;
            if (month.Length == 1)
                month = "0" + month;
            string convertedDate = day + "/" + month + "/" + year;

            return convertedDate;
        }
        public string CategoryToParametersKinozal(string Category)
        {
            List<string> list = new List<string>(dictionarys.KinozalParameters.Keys);
            string output = "";
            foreach (var k in list)
            {
                if (k == Category)
                    output = dictionarys.KinozalParameters[k];
            }
            return output;
        }
        public string GenreToParametersKinozal(string Genre)
        {
            List<string> list = new List<string>(dictionarys.KinozalGenreParameters.Keys);
            string output = "";
            foreach (var k in list)
            {
                if (k == Genre)
                    output = dictionarys.KinozalGenreParameters[k];
            }
            return output;
        }
        public string TypeKinozal(string input)
        {
            List<string> list = new List<string>(dictionarys.TypeKinozal.Keys);
            string output = "";
            foreach (var k in list)
            {
                if (k.Contains(input))
                    output = dictionarys.TypeKinozal[k];
            }
            return output;
        }
        public string DateKinozal(string date)
        {
            string day;
            string month;
            string year;

            if (date == "сейчас")
            {
                day = dateTime.Day.ToString();
                month = dateTime.Month.ToString();
                year = dateTime.Year.ToString();

                return day + "/" + month + "/" + year;
            }

            string[] dateParts = date.Split('в');

            if (dateParts[0].Trim() == "сегодня")
            {
                day = dateTime.Day.ToString();
                month = dateTime.Month.ToString();
                year = dateTime.Year.ToString();
            }
            else if (dateParts[1].Trim() == "чера")
            {
                DateTime yesterday = DateTime.Now.AddDays(-1);
                month = yesterday.Month.ToString();
                day = yesterday.Day.ToString();
                year = yesterday.Year.ToString();
            }
            else
            {
                string[] newDate = dateParts[0].Split('.'); //01.02.2016
                day = newDate[0];
                month = newDate[1];
                year = newDate[2];
            }

            if (day.Length == 1)
                day = "0" + day;
            if (month.Length == 1)
                month = "0" + month;

            return day + "/" + month + "/" + year;
        }
        public string NameToTypeKinozal(string name, string type)
        {
            string output = "";

            List<string> gameList = new List<string>(dictionarys.TypeKinozalGames.Keys);
            foreach (var k in gameList)
            {
                if (name.Contains(k) && type.Trim() == "Spēle")
                    output = dictionarys.TypeKinozalGames[k];
            }

            List<string> programList = new List<string>(dictionarys.TypeKinozalProgramms.Keys);
            foreach (var k in programList)
            {
                if (name.Contains(k) && type.Trim() == "Programma")
                    output = dictionarys.TypeKinozalProgramms[k];
            }
            return output;
        }
        public string DateFilebase(string date)
        {
            var dateParts = date.Split('/');
            var convertedDate = dateParts[0] + "/" + dateParts[1] + "/20" + dateParts[2];

            return convertedDate;
        }
        public string GenreToParametersFilebase(string Genre)
        {
            List<string> list = new List<string>(dictionarys.FilebaseGenreParameters.Keys);
            string output = "";
            foreach (var k in list)
            {
                if (k == Genre)
                    output = dictionarys.FilebaseGenreParameters[k];
            }
            return output;
        }
        public string CategoryToParametersFilebase(string Category)
        {
            List<string> list = new List<string>(dictionarys.FilebaseParameters.Keys);
            string output = "";
            foreach (var k in list)
            {
                if (k == Category)
                    output = dictionarys.FilebaseParameters[k];
            }
            return output;
        }
        public string TypeFilebase(string input)
        {
            List<string> list = new List<string>(dictionarys.TypeFilebase.Keys);
            string output = "";
            foreach (var k in list)
            {
                if (k == input)
                    output = dictionarys.TypeFilebase[k];
            }
            return output;
        }
        public string NameToTypeFilebase(string name, string type)
        {
            string output = "";

            List<string> gameList = new List<string>(dictionarys.TypeFilebaseGames.Keys);
            foreach (var k in gameList)
            {
                if (name.Contains(k) && type.Trim() == "Spēle")
                    output = dictionarys.TypeFilebaseGames[k];
            }

            List<string> programList = new List<string>(dictionarys.TypeFilebaseProgramms.Keys);
            foreach (var k in programList)
            {
                if (name.Contains(k) && type.Trim() == "Programma")
                    output = dictionarys.TypeFilebaseProgramms[k];
            }
            return output;
        }

    }
}
