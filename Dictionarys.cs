using System.Collections.Generic;

namespace TM2
{
    class Dictionarys
    {
        public readonly Dictionary<string, string> Platforms = new Dictionary<string, string>
        {
            {"Microsoft Windows", "/TM2;component/Images/Platforms/windows.png"},
            {"Windows Phone", "/TM2;component/Images/Platforms/windows.png"},
            {"Xbox", "/TM2;component/Images/Platforms/xbox.png"},
            {"PlayStation", "/TM2;component/Images/Platforms/playstation.png"},
            {"Linux", "/TM2;component/Images/Platforms/linux.png"},
            {"Wii", "/TM2;component/Images/Platforms/wii.png"},
            {"OS X", "/TM2;component/Images/Platforms/apple.png"},
            {"iOS", "/TM2;component/Images/Platforms/apple.png"},
            {"SteamOS", "/TM2;component/Images/Platforms/steam.png"},
            {"Google Chrome", "/TM2;component/Images/Platforms/chrome.png"}
        };
        public readonly Dictionary<string, string> MovieGenres = new Dictionary<string, string>
        {
            {"Action", "Asa sižeta"},
            {"Adventure", "Piedzīvojuma"},
            {"Animation", "Multfilma"},
            {"Biography", "Biogrāfijas"},
            {"Comedy", "Komēdija"},
            {"Crime", "Nozieguma"},
            {"Documentary", "Dokumentālā"},
            {"Drama", "Drāma"},
            {"Family", "Ģimenes"},
            {"Fantasy", "Fantāzijas"},
            {"Game-Show", "Spēļu šovs"},
            {"History", "Vēstures"},
            {"Horror", "Šausmu"},
            {"Music", "Mūzikas"},
            {"Musical", "Mūzikls"},
            {"Mystery", "Mistērija"},
            {"News", "Jaunumu"},
            {"Reality-TV", "Realitātes šovs"},
            {"Romance", "Romantika"},
            {"Sci-Fi", "Fantastika"},
            {"Sitcom", "Situācijas komēdija"},
            {"Sport", "Sporta"},
            {"Talk-Show", "Sarunu šovs"},
            {"Thriller", "Trilleris"},
            {"War", "Kara"},
            {"Western", "Vesterns"},
            {"Erotika", "Erotika"}
        };
        public readonly Dictionary<string, string> MovieGenresRus = new Dictionary<string, string>
        {
            {"детектив", "Detektīvs" },
            {"детский", "Bērnu" },
            {"для взрослых", "Erotika" },
            {"концерт", "Koncerts" },
            {"короткометражка", "Īss" },
            {"криминал", "Nozieguma" },
            {"приключения", "Piedzīvojuma"},
            {"аниме", "Multfilma"},
            {"мультфильм", "Multfilma" },
            {"биография", "Biogrāfijas"},
            {"комедия", "Komēdija"},
            {"документальный", "Dokumentālā"},
            {"драма", "Drāma"},
            {"семейный", "Ģimenes"},
            {"фэнтези", "Fantāzijas"},
            {"игра", "Spēļu šovs"},
            {"история", "Vēstures"},
            {"ужасы", "Šausmu"},
            {"музыка", "Mūzikas"},
            {"мюзикл", "Mūzikls"},
            {"новости", "Jaunumu"},
            {"реальное ТВ", "Realitātes šovs"},
            {"мелодрама", "Romantika"},
            {"фантастика", "Fantastika"},
            {"спорт", "Sporta"},
            {"ток-шоу", "Sarunu šovs"},
            {"триллер", "Trilleris"},
            {"боевик", "Bojeviks" },
            {"военный", "Kara"},
            {"вестерн", "Vesterns"},
            {"фильм-нуар", "Nuar"}
        };
        public readonly Dictionary<string, string> GameGenres = new Dictionary<string, string>
        {
            {"Action", "Asa sižeta"},
            {"Adventure", "Piedzīvojuma"},
            {"Casual", "Casual"},
            {"Indie", "Indie"},
            {"Massively Multiplayer", "Daudzspēlētāju"},
            {"Racing", "Braukšana"},
            {"RPG", "Lomu"},
            {"Simulation", "Simulācija"},
            {"Sports", "Sports"},
            {"Strategy", "Stratēģijas"},
            {"MMO", "Daudzspēlētāju online"},
            {"Online", "Online"},
            {"Puzzle", "Mīklu"},
            {"Shooter", "Šāvējs"}
        };

        public readonly Dictionary<string, string> SizeRussian = new Dictionary<string, string>
        {
            {"ТБ", "TB"},
            {"ГБ", "GB"},
            {"МБ", "MB"},
            {"КБ", "KB"},
            {"TB", "TB"},
            {"GB", "GB"},
            {"MB", "MB"},
            {"kB", "KB"}
        };

        public readonly Dictionary<string, string> FanoParameters = new Dictionary<string, string>
        {
            {"Erotika", "&c50=1&c9=1&c45=1" },
            {"Filmas", "&c20=1&c47=1&c17=1&c24=1&c52=1&c37=1&c53=1&c4=1&c30=1" },
            {"Multfilmas", "&c29=1&c27=1" },
            {"TV", "&c6=1&c33=1&c49=1&c25=1&c35=1&c32=1&c23=1" },
            {"Spēles", "&c7=1&c12=1&c34=1&c46=1&c43=1&c40=1&c51=1" },
            {"Mūzika", "&c5=1&c31=1&c48=1" },
            {"Videoklipi", "&c19=1" },
            {"Programmas", "&c1=1&c22=1" },
            {"Telefonam", "&c38=1" },
            {"Grāmatas", "&c44=1&c41=1" },
            {"Mācības", "&c42=1" }
        };

        public readonly Dictionary<string, string> TypeFano = new Dictionary<string, string>
        {
            {"Movies/Rus", "Filma (Krievu val.)"},
            {"Movies/Lat", "Filma (Latviešu val.)"},
            {"Movies/SD", "Filma (Angļu val.)"}, //SD
            {"Movies/HD", "Filma (Angļu val.)"}, //HD
            {"Movies/DVD-R", "Filma DVD-R"},
            {"Cartoons", "Multfilma"},
            {"3D", "Filma 3D"},
            {"Movies/DVD-R Lat", "Filma (Latviešu val.) DVD-R"},
            {"TV/SD", "TV"}, //SD
            {"TV/Rus", "TV (Krievu val.)"},
            {"TV/Lat", "TV (Latviešu val.)"},
            {"TV/HD", "TV"}, //HD
            {"TV/Facts", "TV"},
            {"TV/Sport", "TV"},
            {"Games/PC ISO", "Spēle PC"},
            {"Games/PC Rips", "Spēle PC"},
            {"Games/Xbox", "Spēle Xbox"},
            {"Games/PS", "Spēle PS"},
            {"Games/Wii", "Spēle Wii"},
            {"Games/Misc", "Spēlēm (Misc)"},
            {"Music", "Mūzika"},
            {"Music/HQ", "Mūzika"},
            {"Music Videos", "Videoklips"},
            {"Mobile", "Telefonam"},
            {"Appz/misc", "Programma"},
            {"Appz/PC ISO", "Programma"},
            {"Packs/Movies", "Filmu paka"},
            {"Packs/TV", "TV paka"},
            {"Packs/Games", "Spēļu paka"},
            {"Packs/Music", "Mūzika"},
            {"Packs/XXX", "Erotika"},
            {"A-Books", "Audiogrāmata"},
            {"E-Books", "Grāmata"},
            {"Study", "Mācības"},
            {"X-mas", "Ziemassvētki"},
            {"XXX", "Erotika"},
            {"Anime", "Multfilma"},
            {"Movies/CAM", "Filma (Angļu val.) CAM"},
            {"XXX/HD", "Erotika"}
        };

        public readonly Dictionary<string, string> KinozalGenreParameters = new Dictionary<string, string>
        {
            {"Komēdija", "&c=8" },
            {"Bojeviks/Kara", "&c=6" },
            {"Trilleris/Detektīvs", "&c=15" },
            {"Drāma", "&c=17" },
            {"Melodrāma", "&c=35" },
            {"Fantastika", "&c=13" },
            {"Fantāzijas", "&c=14" },
            {"Šausmu/Mistērija", "&c=24" },
            {"Piedzīvojuma", "&c=11" },
            {"Dokumentāla", "&c=18" },
            {"Sports", "&c=37" },
            {"Bērnu/Ģimenes", "&c=12" },
            {"Klasika", "&c=7" },
            {"Vēstures", "&c=9" }
        };

        public readonly Dictionary<string, string> KinozalParameters = new Dictionary<string, string>
        {
            {"Filmas0", "&c=1002" },
            {"Filmas1", "&c=1003" },
            {"Multfilmas", "&c=1003" },
            {"TV0", "&c=1001" },
            {"TV1", "&c=1006" },
            {"TV2", "&c=18" },
            {"Spēles", "&c=23" },
            {"Mūzika", "&c=1004" },
            {"Videoklipi", "&c=1" },
            {"Programmas", "&c=32" },
            {"Grāmatas0", "&c=2" },
            {"Grāmatas1", "&c=41" },
            {"Erotika", "&c=16" }
        };

        public readonly Dictionary<string, string> TypeKinozal = new Dictionary<string, string>
        {
            {"http://st.kinozal.tv/pic/cat/1.gif", "Videoklips"},
            {"http://st.kinozal.tv/pic/cat/2.gif", "Audiogrāmata"},
            {"http://st.kinozal.tv/pic/cat/3.gif", "Mūzika"},
            {"http://st.kinozal.tv/pic/cat/4.gif", "Mūzika"},
            {"http://st.kinozal.tv/pic/cat/5.gif", "Mūzika"},
            {"http://st.kinozal.tv/pic/cat/6.gif", "Filma - Bojeviks"},
            {"http://st.kinozal.tv/pic/cat/7.gif", "Filma - Klasiskā"},
            {"http://st.kinozal.tv/pic/cat/8.gif", "Filma - Komēdija"},
            {"http://st.kinozal.tv/pic/cat/9.gif", "Filma - Vēstures"},
            {"http://st.kinozal.tv/pic/cat/10.gif", "Filma - Krievu Kino"},
            {"http://st.kinozal.tv/pic/cat/11.gif", "Filma - Piedzīvojumu"},
            {"http://st.kinozal.tv/pic/cat/12.gif", "Filma - Bērnu/Ģimenes"},
            {"http://st.kinozal.tv/pic/cat/13.gif", "Filma - Fantastika"},
            {"http://st.kinozal.tv/pic/cat/14.gif", "Filma - Fantāzijas"},
            {"http://st.kinozal.tv/pic/cat/15.gif", "Filma - Trilleris/Detektīvs"},
            {"http://st.kinozal.tv/pic/cat/16.gif", "Erotika"},
            {"http://st.kinozal.tv/pic/cat/17.gif", "Filma - Drāma"},
            {"http://st.kinozal.tv/pic/cat/18.gif", "Filma - Dokumentālā"},
            {"http://st.kinozal.tv/pic/cat/19.gif", "Filma - Bērnu"},
            {"http://st.kinozal.tv/pic/cat/20.gif", "Multfilma"},
            {"http://st.kinozal.tv/pic/cat/21.gif", "Multfilma"},
            {"http://st.kinozal.tv/pic/cat/22.gif", "Multfilma"},
            {"http://st.kinozal.tv/pic/cat/23.gif", "Spēle"},
            {"http://st.kinozal.tv/pic/cat/24.gif", "Filma - Šausmu/Mistika"},
            {"http://st.kinozal.tv/pic/cat/25.gif", ""},
            {"http://st.kinozal.tv/pic/cat/26.gif", ""},
            {"http://st.kinozal.tv/pic/cat/27.gif", ""},
            {"http://st.kinozal.tv/pic/cat/28.gif", ""},
            {"http://st.kinozal.tv/pic/cat/29.gif", ""},
            {"http://st.kinozal.tv/pic/cat/30.gif", ""},
            {"http://st.kinozal.tv/pic/cat/31.gif", ""},
            {"http://st.kinozal.tv/pic/cat/32.gif", "Programma"},
            {"http://st.kinozal.tv/pic/cat/33.gif", "Filma - DVD"},
            {"http://st.kinozal.tv/pic/cat/34.gif", "Grafika"},
            {"http://st.kinozal.tv/pic/cat/35.gif", "Filma - Romantika"},
            {"http://st.kinozal.tv/pic/cat/36.gif", "Filma - Koncerts/Tv šovs"},
            {"http://st.kinozal.tv/pic/cat/37.gif", "Filma - Sporta"},
            {"http://st.kinozal.tv/pic/cat/38.gif", "Filma - Teātris/Opera/Balets"},
            {"http://st.kinozal.tv/pic/cat/39.gif", "Filma - Indiešu"},
            {"http://st.kinozal.tv/pic/cat/40.gif", "Dizains/Grafika"},
            {"http://st.kinozal.tv/pic/cat/41.gif", "Grāmata"},
            {"http://st.kinozal.tv/pic/cat/42.gif", "Mūzika"},
            {"http://st.kinozal.tv/pic/cat/43.gif", ""},
            {"http://st.kinozal.tv/pic/cat/44.gif", ""},
            {"http://st.kinozal.tv/pic/cat/45.gif", "TV"},
            {"http://st.kinozal.tv/pic/cat/46.gif", "TV"},
            {"http://st.kinozal.tv/pic/cat/47.gif", "Filma - Aziātu"},
            {"http://st.kinozal.tv/pic/cat/48.gif", "Filma - Koncerts"},
            {"http://st.kinozal.tv/pic/cat/49.gif", "TV"},
            {"http://st.kinozal.tv/pic/cat/50.gif", "TV"}
        };

        public readonly Dictionary<string, string> TypeKinozalGames = new Dictionary<string, string>
        {
            {"PC (Windows)", "Spēle PC"},
            {"PC (Mac)", "Spēle OS X"},
            {"Console (Xbox 360)", "Spēle Xbox"},
            {"Console  (Xbox 360)", "Spēle Xbox"},
            {"Console (PS2)", "Spēle PS"},
            {"Console (PS3)", "Spēle PS"},
            {"Console (PSP)", "Spēle PS"},
            {"Console (DS)", "Spēle Nintendo"},
            {"Console(PSX)", "Spēle PS"},
            {"PC (Multi)", "Spēle Multi"},
            {"Console (SNES)", "Spēle Nintendo"},
            {"Console (GBA)", "Spēle GameBoy"},
            {"Console (Dreamcast)", "Spēle Dreamcast"},
            {"Console (Wii)", "Spēle Wii"},
            {"Mobile (Android)", "Spēle Android"},
            {"Mobile (iOS)", "Spēle Apple"}
        };

        public readonly Dictionary<string, string> TypeKinozalProgramms = new Dictionary<string, string>
        {
            {"PC (Windows)", "Programma PC"},
            {"PC (Mac)", "Programma MAC"},
            {"Mobile (Android)", "Programma Android"},
            {"Mobile (iOS)", "Programma Apple"}
        };

        public readonly Dictionary<string, string> TypeFilebase = new Dictionary<string, string>
        {
            {"Мультики", "Multfilma"},
            {"ТВ Передача", "TV"},
            {"Спорт", "Filma - Sporta"},
            {"Боевик", "Filma - Bojeviks"},
            {"Комедия", "Filma - Komēdija"},
            {"Триллер", "Filma - Trilleris/Detektīvs"},
            {"Классика", "Filma - Klasika"},
            {"Исторический", "Filma - Vēsture"},
            {"Мистика", "Filma - Šausmu/Mistika"},
            {"Фантастика", "Filma - Fantastika"},
            {"Ужасы", "Filma - Šausmu/Mistika"},
            {"Драма", "Filma - Drāma"},
            {"Приключения", "Filma - Piedzīvojumu"},
            {"Детектив", "Filma - Trilleris/Detektīvs"},
            {"Концерт", "Filma - Koncerts"},
            {"Аниме", "Multfilma"},
            {"Мелодрама", "Filma - Romantika"},
            {"Док. Фильм", "Filma - Dokumentālā"},
            {"Сериал", "TV"},
            {"Фентези", "Filma - Fantāzijas"},
            {"Семейный", "Filma - Bērnu/Ģimenes"},
            {"Сказка", "Filma - Pasakas"},
            {"Катастрофа", "Filma - Katastrofu"},
            {"Игры", "Spēle  "},
            {"Программы", "Programma"},
            {"Видеоклип", "Videoklips"},
            {"Картинки", "Bildes"},
            {"Eng Музыка", "Mūzika"},
            {"Rus Музыка", "Mūzika"},
            {"Книги", "Grāmata"},
            {"Телефон", "Telefonam"},
            {"Военный", "Filma - Bojeviks"}
        };

        public readonly Dictionary<string, string> TypeFilebaseGames = new Dictionary<string, string>
        {
            {"PC", "Spēle PC"},
            {"PSx", "Spēle OS X"},
            {"PSP", "Spēle PS"},
            {"PS2", "Spēle PS"},
            {"PS3", "Spēle PS"},
            {"Xbox 360", "Spēle Xbox"},
            {"Xbox360", "Spēle Xbox"},
            {"Android", "Spēle Android"}
        };

        public readonly Dictionary<string, string> TypeFilebaseProgramms = new Dictionary<string, string>
        {
            {"PC", "Programma PC"},
            {"Android", "Programma Android"},
            {"Mac OS X", "Programma MAC"},
            {"MAC", "Programma MAC"},
            {"PC/Mac", "Programma Multi"}
        };

        public readonly Dictionary<string, string> FilebaseGenreParameters = new Dictionary<string, string>
        {
            {"Komēdija", "&c=comedy" },
            {"Bojeviks/Kara0", "&c=action" },
            {"Bojeviks/Kara1", "&c=war" },
            {"Trilleris/Detektīvs0", "&c=thriller" },
            {"Trilleris/Detektīvs1", "&c=detective" },
            {"Drāma", "&c=drama" },
            {"Melodrāma", "&c=epic" },
            {"Fantastika", "&c=sci-fi" },
            {"Fantāzijas", "&c=fantasy" },
            {"Šausmu/Mistērija0", "&c=horror" },
            {"Šausmu/Mistērija1", "&c=mystic" },
            {"Piedzīvojuma", "&c=adventure" },
            {"Dokumentāla", "&c=documental" },
            {"Sports", "&c=sport" },
            {"Bērnu/Ģimenes", "&c=family" },
            {"Klasika", "&c=classic" },
            {"Vēstures", "&c=history" }
        };

        public readonly Dictionary<string, string> FilebaseParameters = new Dictionary<string, string>
        {
            {"Multfilmas0", "&c=cartoons" },
            {"Multfilmas1", "&c=anime" },
            {"TV0", "&c=serials" },
            {"TV1", "&c=tv" },
            {"Spēles", "&c=games" },
            {"Mūzika0", "&c=eng-music" },
            {"Mūzika1", "&c=rus-music" },
            {"Videoklipi", "&c=videoclips" },
            {"Programmas", "&c=software" },
            {"Telefonam", "&c=phone" },
            {"Grāmatas", "&c=books" }
        };
    }
}