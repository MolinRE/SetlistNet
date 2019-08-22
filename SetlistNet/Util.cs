using SetlistNet.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SetlistNet
{
    public static class Util
    {
        /// <summary>
        /// Converts given setlist to text view. Use this method to get text representation of the setlist.
        /// </summary>
        /// <param name="setlist">The setlist to represent.</param>
        /// <param name="useHtml">Whether or not to use HTML-code to add little extra beauty.</param>
        /// <returns>Text representation of the setlist</returns>
        public static string SetlistToText(Setlist setlist, bool useHtml = true)
        {
            var text = new StringBuilder();
            var venue = setlist.Venue;
            text.AppendLine($"[{setlist.GetEventDateTime("MMM dd yyyy", CultureInfo.GetCultureInfo("en-US"))}] {TagHelper.Href(setlist.Artist.UrlStats, setlist.Artist.Name)} setlist");
            text.AppendLine($"at {TagHelper.Href(venue.Url, $"{venue.Name}, {venue.City.Name}, {venue.City.Country.Name}")}");
            if (!string.IsNullOrEmpty(setlist.TourName))
            {
                text.AppendLine($"Tour: {setlist.Tour.Name}");
            }

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
                        text.Append(" (");
                        if (song.Cover != null)
                        {
                            text.AppendFormat("<i>{0}</i> cover", song.Cover.Name);
                            if (song.With != null)
                                text.AppendFormat(" w/ {0}", song.With.Name);
                        }
                        else
                            if (song.With != null)
                            text.AppendFormat("w/ {0}", song.With.Name);

                        text.Append(")");
                    }
                    if (!string.IsNullOrEmpty(song.Info))
                    {
                        text.AppendFormat(" ({0})", song.Info);
                    }
                    text.AppendLine();
                }
            }

            if (!string.IsNullOrEmpty(setlist.Info))
            {
                text.AppendLine();
                text.AppendLine($"<b>Note</b>: {setlist.Info}");
            }

            return text.ToString();
        }

        public static string SetlistsToText(Setlists setlists, int count = 7, bool useHtml = true)
        {
            var text = new StringBuilder();
            foreach (Setlist setlist in setlists.Take(count))
            {
                text.AppendFormat("[{0:dd.MM.yyyy}, {2}] {1}, {3}.",
                    setlist.GetEventDateTime(), setlist.Venue.City.Name, setlist.Venue.City.Country.Code, setlist.Venue.Name);
                if (!string.IsNullOrEmpty(setlist.TourName))
                {
                    text.AppendFormat(" ({0} tour)", setlist.Tour.Name);
                }
                if (!string.IsNullOrEmpty(setlist.Info))
                {
                    text.AppendFormat(". Note: {0}", setlist.Info);
                }
                text.AppendLine();
            }

            return text.ToString();
        }

        public static string SetlistsToTextHtml(Setlists setlists, int count = 7, bool useHtml = true)
        {
            var text = new StringBuilder();
            int year = DateTime.Now.Year;
            foreach (Setlist setlist in setlists.Take(count))
            {
                var date = setlist.GetEventDateTime();
                if (date.HasValue && date.Value.Year != year)
                {
                    year = date.Value.Year;
                    text.AppendLine($"<b>{year}</b>:");
                }

                text.AppendFormat("[{0:dd.MM}] {1} {2}.",
                    setlist.GetEventDateTime(), setlist.Venue.City.Name, setlist.Venue.City.Country.Code, setlist.Venue.Name);
                text.AppendLine();
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
