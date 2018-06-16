using SetlistNet.Models;
using System;
using System.Linq;
using System.Text;

namespace SetlistNet
{
    public static class Util
    {
        public static string SetlistToText(Setlist setlist, bool useHtml = true)
        {
            StringBuilder text = new StringBuilder();
            text.AppendFormat("<a href=\"{0}\">{1}</a> @ {2} ({3}, {4}), {5}\r\n", setlist.Artist.UrlStats, setlist.Artist.Name, setlist.Venue.Name, setlist.Venue.City.Name, setlist.Venue.City.Country.Code, setlist.GetEventDateTime("dd.MM.yy"));
            int count = 0;
            foreach (Set set in setlist.Sets)
            {
                if (set.EncoreSpecified || !string.IsNullOrEmpty(set.Name))
                {
                    text.AppendLine();
                    string setName = set.Name;
                    if (set.EncoreSpecified)
                        setName = "Encore " + set.Encore;
                    if (setName != " ")
                    {
                        if (useHtml)
                            text.AppendLine("<b>" + setName + "</b>");
                        else
                            text.AppendLine(setName);
                    }
                }

                foreach (Song song in set.Songs)
                {
                    text.AppendFormat("{0}. {1}", ++count, song.Name.Trim() == "" ? "Unknown" : song.Name);
                    if (song.Cover != null || song.With != null)
                    {
                        text.Append(" (<i>");
                        if (song.Cover != null)
                        {
                            text.AppendFormat("{0} cover", song.Cover.Name);
                            if (song.With != null)
                                text.AppendFormat(" w/ {0}", song.With.Name);
                        }
                        else
                            if (song.With != null)
                            text.AppendFormat("w/ {0}", song.With.Name);

                        text.Append("</i>)");
                    }
                    if (!string.IsNullOrEmpty(song.Info))
                        text.AppendFormat(", {0}.", song.Info);
                    text.AppendLine();
                }
            }
            return text.ToString();
        }

        public static string SetlistsToText(Setlists setlists, int count = 7, bool useHtml = true)
        {
            StringBuilder text = new StringBuilder();
            foreach (Setlist setlist in setlists.Take(count))
            {
                text.AppendFormat("[{0:dd.MM.yyyy}, {2}] {1}, {3}.",
                    setlist.GetEventDateTime(), setlist.Venue.City.Name, setlist.Venue.City.Country.Code, setlist.Venue.Name);
                if (!string.IsNullOrEmpty(setlist.TourName))
                    text.AppendFormat(" ({0})", setlist.Tour);
                if (!string.IsNullOrEmpty(setlist.Info))
                    text.AppendFormat(". {0}", setlist.Info);
                text.Append("\r\n");
            }
            return text.ToString();
        }

        // Parse the query string and return Setlist object used to search for setlists
        public static Setlist ParseQuery(string query)
        {
            if (query.Length == 0) return null;
            Setlist result = new Setlist();
            result.Artist = new Artist();
            string[] keywords = query.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (IsYear(keywords[keywords.Length - 1]))
            {
                result.EventDate = "00-00-" + keywords[keywords.Length - 1];
                result.Artist.Name = string.Join(" ", keywords.Take(keywords.Length - 1));
            }
            else
            {
                result.Venue = new Venue(new City(keywords[keywords.Length - 1]));
                if (keywords.Length > 2 && IsYear(keywords[keywords.Length - 2]))
                {
                    result.EventDate = "00-00-" + keywords[keywords.Length - 2];
                    result.Artist.Name = string.Join(" ", keywords.Take(keywords.Length - 2));
                }
                else
                {
                    result.Artist.Name = string.Join(" ", keywords.Take(keywords.Length - 1));
                }
            }

            return result;
        }

        // Simple check whether given string contains 4 digits
        public static bool IsYear(string p)
        {
            if (p.Length == 4)
                return p.All(c => char.IsDigit(c));
            else
                return false;
        }
    }
}
