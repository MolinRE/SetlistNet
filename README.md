
## setlist.fm API .NET Core library

C# library to interact with [setlist.fm API](http://api.setlist.fm/docs/index.html).
It's currently supports all methods listed in the API **REST Endpoints** section, such as Setlists Search or getting Artist by MBID.

## Usage

## Get the key

First of all, [apply](https://www.setlist.fm/settings/api) for the setlist.fm API key. It's needed for __all__ requests.

### Search for setlists

```csharp
SetlistApi api = new SetlistApi(apiKey);
Setlists setlists = api.SearchSetlists(new Setlist()
{
	// Search for Foo Fighters' setlists of 2004.
	Artist = new Artist("foo fighters"),
	EventDate = "00-00-2004"
});

if (setlists.Count > 0)
{
	// Setlist consists of Sets
	foreach (Set set in setlists[0].Sets)
	{
		// Set can be an encore
		if (set.EncoreSpecified)
			Console.WriteLine("---");
		// ...or can have special name
		if (!string.IsNullOrEmpty(set.Name))
			Console.WriteLine(set.Name);

		// Set consists of Songs
		foreach (Song song in set.Songs)
		{
			Console.WriteLine(song.Name);
		}
	}

	// You can also use this method for text representation of the setlist
	Util.SetlistToText(setlists[0]);
}
```

So, you must provide search criteria as _Setlist_ object in this case.

Every search method has a description which says what properties of given object will become part of the search query. Not all properties are necessary, for example: _Url_ or _Info_.

### Search for artists

```csharp
Artists artists = api.SearchArtists(new Artist("muse"));
foreach (Artist artist in artists)
{
	Console.Write(artist.Name);

	// There may be several performers with the same name - use "Disambiguation" property
	// to get SetlistFM notes and distinguish the right one.
	if (!string.IsNullOrEmpty(artist.Disambiguation))
		Console.Write(", " + artist.Disambiguation);

	// Or simply use this:
	Console.WriteLine(artist.NameWithDisambiguation);
	// It will show just the name of the Artist if there's no disambiguation.
}
```

Same here. If you want to search for artists (or anything else, because it works the same way), you must provide _Artist_ object with necessary properties.

### Internationalization

This feature listed as "experimental" by official documentation and does not work for all cities.
However, for the rest of it, you can get localized cities and countries names. The default language is English (`en`), but you can provide any of the languages Spanish (`es`), French (`fr`), German (`de`), Portuguese (`pt`), Turkish (`tr`), Italian (`it`) or Polish (`pl`).

```csharp
SetlistApi api = new SetlistApi(apiKey, "en");
var setlist = api.Setlist("23f6dc3b");
Console.WriteLine(setlist.Venue.City.Name + ", " + setlist.Venue.City.Country.Name);
// Output: New York, United States

// If you pass "es" to the constructor, you will get
api = new SetlistApi(apiKey, "es");
setlist = api.Setlist("23f6dc3b");
Console.WriteLine(setlist.Venue.City.Name + ", " + setlist.Venue.City.Country.Name);
// Output: Nueva York, Estados Unidos
```

More examples coming soon...

## Installation

Nuget: https://www.nuget.org/packages/SetlistNet/

## Tests

Coming soon.

## Issues and contributions
...are welcome.
