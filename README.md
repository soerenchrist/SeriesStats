# SeriesStats

This is a simple application to display shows, movies and some statistics. It is not meant to be published and is not productive code.<br />
The intention of this Repository is mainly to play arount with Xamarin.Forms related stuff and frameworks.<br />
<br/>
The main Packages used to build this:

- [Prism.Forms](https://prismlibrary.com/docs/xamarin-forms/Getting-Started.html)
- [Xamarin.Froms.PancakeView](https://github.com/sthewissen/Xamarin.Forms.PancakeView)
- [MicroCharts](https://github.com/dotnet-ad/Microcharts)
- [Monkey-Cache](https://github.com/jamesmontemagno/monkey-cache)
- [Sharpnado.Presentation.Forms (Mainly the horizontal list view control)](https://github.com/roubachof/Sharpnado.Presentation.Forms)
- [Xamarin.Essentials](https://docs.microsoft.com/xamarin/essentials)
- [Mobile.BuildTools](https://github.com/dansiegel/Mobile.BuildTools)

The data that is displayed in the app is based on the APIs of
- [Trakt](https://trakt.tv): Used to load a users watched shows and build some statistics with it. User needs to log in with a Trakt user using OAuth
- [TheMovieDB](https://themoviedb.org): Used for show and movie images and detailed data.

Please refer to the Terms of Use for [TheMovieDB](https://www.themoviedb.org/documentation/api/terms-of-use) and [Trakt](https://trakt.tv/terms).

IF you find errors or you have some improvement ideas, feel free to create a pull request.

For your own development, you will need your own ClientIds for Trakt and TheMovieDB, as they are not provided in this repository.<br/>
To use them in the app, simply create a file named "secrets.json" inside the shared project.
The format looks like this:
```
{
    "TraktClientId": "Your trakt client id",
    "TraktClientSecret": "Your trakt client secret",
    "TmdbApiKey": "Your api key for TheMovieDB"
}
```

Thanks to [Mobile.BuildTools](https://github.com/dansiegel/Mobile.BuildTools) those secrets will be usable in the app.
