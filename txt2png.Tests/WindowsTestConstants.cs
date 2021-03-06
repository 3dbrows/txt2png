﻿namespace txt2png.Tests
{
    /// <summary>
    ///     We use different fonts on Windows and Linux. Therefore, we expect different Base64 encodings of known strings,
    ///     depending on the OS and font.
    /// </summary>
    public static class WindowsTestConstants
    {
        public const string Base64OutputForWhiteBackground =
            "iVBORw0KGgoAAAANSUhEUgAAAAMAAAADCAMAAABh9kWNAAAAG1BMVEXr6+vU1NT///8qKioBAQHKyspTU1MYGBjf39+oCkrqAAAAFElEQVQI12NgYGRiYGZhZWBj5wAAAJYAJUsNACAAAAAASUVORK5CYII=";

        public const string Base64OutputForTransparentBackground =
            "iVBORw0KGgoAAAANSUhEUgAAAAMAAAADCAMAAABh9kWNAAAABlBMVEX///8AAABVwtN+AAAAAXRSTlMAQObYZgAAAA9JREFUCNdjYAACRkYwBgAAHgAF9d7waAAAAABJRU5ErkJggg==";

        public const string Base64OutputForBlackBackground =
            "iVBORw0KGgoAAAANSUhEUgAAAAMAAAADCAMAAABh9kWNAAAAG1BMVEUUFBQrKysAAADV1dX+/v41NTWsrKzn5+cgICA7Xr4VAAAAFElEQVQI12NgYGRiYGZhZWBj5wAAAJYAJUsNACAAAAAASUVORK5CYII=";

        public const string Base64OutputForPunctuation =
            "iVBORw0KGgoAAAANSUhEUgAAAT8AAAAVCAMAAAAHOBl9AAAABlBMVEX///8AAABVwtN+AAAAAXRSTlMAQObYZgAAAkBJREFUWMPtmNtywyAMRNn//+nONDHWaldc7DTpTMtDgzHociQEbmuThtmEv96ghFD0p6I+7ctHmgJEGMQ5BThHTRfUy6IRFZLKqGo7At+yojHnr7qFQguSReuas2uL/EDdx2vgGAWCrae22IVJccBuhpkXFII25Oe1aKxX8VVLEFyP/IwqsjzGA3keu8jpC1q25Qb90LNmmeFt2G9BrPkRjJpf7CV+jFkzmrNaPbvHTyR5LYTyhfya8jPLMOSXIhLWhFw4UnNQTNb4UUrr9pIoxT+PyXXwDlPXbENeUvGLdh7bsAPKQZYTh+v56/j58lfw6/YO+FGxdxxCzLb4pULCgGRj5MOH8w4qsQAqpsoky08odvCtcQwLevnB4fZOp/tGvKmkF7ycQNnYizFzfu4tdNQgXeQHG1ghaLNVDqbl/ZsAPlNAcXQ/MOcXU81fGAy/ovx5LXTVOgsR3FmZAPpkzX2X137/hgO6SVkznFCMFwt8K/OvEp/mO35sntQ8JJFqT3TFTir58SE74teW+an2Wf0z5Y/F6/6VS71F1jCOJptc83MoW8y/c+lq/t0/f3nQS1d+vUyP0KDczCOjQnGFEXY8yPdbXIUyfeQz+C4/cyj0/QnDD5iLH1kxuC2GGeX5G7+F+zCL9l+I4f7CYsdGbfBjobSbld+mwh1+u/8RuWzKuXpXROQ3eO349ZHBcXrDk6eeN/LDhWzY5eeiZG479xqK/g+3zWzvvj9TaK1EXYrSviO2/6aGlyfEf/ut7QvSywOui0/BgQAAAABJRU5ErkJggg==";

        public const string Base64OutputForRiverCrab =
            "iVBORw0KGgoAAAANSUhEUgAAACUAAAATCAMAAAAgYzSBAAAABlBMVEX///8AAABVwtN+AAAAAXRSTlMAQObYZgAAAHRJREFUKM+dkQEOwCAIA7n/f3pZdNSCM8uIGjXlLBLxP9hccU/Fc8pbyZaMuYTt/IGhmozJe3GEhoHSwHJC0GrFWCxmxuumUm1mylQky76iqmJInMVG1TtArdFaQWEpv6+qMU4sa0GjNJOwyYCCyp8sER/jAjdkAN4kV8s9AAAAAElFTkSuQmCC";
    }
}