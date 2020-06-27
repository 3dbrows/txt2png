namespace txt2png.Tests
{
    /// <summary>
    ///     We use different fonts on Windows and Linux. Therefore, we expect different Base64 encodings of known strings,
    ///     depending on the OS and font.
    /// </summary>
    public static class LinuxTestConstants
    {
        public const string Base64OutputForWhiteBackground =
            "iVBORw0KGgoAAAANSUhEUgAAAAMAAAADCAMAAABh9kWNAAAAElBMVEX29vbT09Pd3d3Hx8cAAAA3Nzc91LFLAAAAEUlEQVQI12NgYGRiYGZhBWEAAIQAHI0KDvsAAAAASUVORK5CYII=";

        public const string Base64OutputForTransparentBackground =
            "iVBORw0KGgoAAAANSUhEUgAAAAMAAAADCAMAAABh9kWNAAAABlBMVEX///8AAABVwtN+AAAAAXRSTlMAQObYZgAAAA9JREFUCNdjYAABRkYQBgAAGgAF1l3aKwAAAABJRU5ErkJggg==";

        public const string Base64OutputForBlackBackground =
            "iVBORw0KGgoAAAANSUhEUgAAAAMAAAADCAMAAABh9kWNAAAAElBMVEUJCQksLCwiIiI4ODj////IyMhnGtzrAAAAEUlEQVQI12NgYGRiYGZhBWEAAIQAHI0KDvsAAAAASUVORK5CYII=";

        public const string Base64OutputForPunctuation =
            "iVBORw0KGgoAAAANSUhEUgAAATAAAAAUCAMAAAA9b5FVAAAABlBMVEX///8AAABVwtN+AAAAAXRSTlMAQObYZgAAAhhJREFUWMPtmG1TAyEMhNn//6d1rMBusuGgXp3RKR/a0wt5eQgL2tr9Ay/w+YfGefn/HBhSfZCqMYw+PzAmgT3En8Ce1ZOZTg/brL88kBuKZX30/BGc4CQiCmKQ3zpgElpTfxiB6qc3zUxXoLv5I84dwCwCjBoC87aLDJXr4dQAG68QPKn5LAYQHuyS0H+70MZOOC+AxegGmLpDXsgLXqWpUpFiHLAWzeUJa2C0xGEDbwMbnYM+vDV90AxKK81JurEE1vaARR+0bzUowQs72iveObDQ0WtgJGoOg9+uF8AQgrl5eU1VHLnBWM5E8lhdVsCohbx1oYKuEBDprAPITy02yiwdizjTtobOBNHhSB+AndqvlJTneA4sam0X59zya3ghkSWw3GHJx9QlSAP1LWR6atlhKUgJzC4gJR20FkNpe1r50hBm1sDi1rdWXmIJWMbtS9vYkhfWVUOqRoYzHFP4PBm9X9lE9Ky+ABYbRM5GtApYPpxu0DCv+Xrcs5TUGz6L7RawUhNEgvR0o4vVPI3c7ILMDzqsAsbfEON6nF1qM7D8lxBkFaKq0fRcxPLIfvJaoemOGPw63MM2Lqylctd2yMCyUsS21gPULFNEzgXtrekJMPVPQaoNWYU8AXY0zmeEgs6A7WZz5P/pgvGaEGVctLa4faUcC5GNmB5PpfEdic/nXwW2Hcxu6oti0r93XlbGe5yMNzAZH4rTAxQJtKzYAAAAAElFTkSuQmCC";

        public const string Base64OutputForRiverCrab =
            "iVBORw0KGgoAAAANSUhEUgAAACMAAAASCAMAAADmIZdjAAAABlBMVEX///8AAABVwtN+AAAAAXRSTlMAQObYZgAAAHRJREFUKM+NklEOgCAMQ3n3v7TIhK4MExY/tDRv3bC1q4ILqSuoQomXcfKZZF8Y5mdFLsoLQkqimyd3QlQmbTx4dvesXjJx5qRBfzz4UHhmzd3YsjT84JRXvfa1++VkhDHPnshX+9rOkJB0bIVxF/XHqdU9D+y6ALbk6nasAAAAAElFTkSuQmCC";
    }
}